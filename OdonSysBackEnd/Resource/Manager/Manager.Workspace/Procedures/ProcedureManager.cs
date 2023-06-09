﻿using Access.Contract.ClientProcedure;
using Access.Contract.Procedure;
using Access.Contract.Users;
using Access.File.Contract;
using AutoMapper;
using Contract.Workspace.ClientProcedures;
using Contract.Workspace.Procedures;
using Microsoft.AspNetCore.Http;

namespace Manager.Workspace.Procedures
{
    internal class ProcedureManager : IProcedureManager
    {
        private readonly IProcedureAccess _procedureAccess;
        private readonly IClientProcedureAccess _clientProcedureAccess;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IMapper _mapper;
        private readonly IFileAccess _fileAccess;

        public ProcedureManager(
            IProcedureAccess procedureAccess,
            IMapper mapper,
            IClientProcedureAccess clientProcedureAccess,
            IUserDataAccess userDataAccess, IFileAccess fileAccess)
        {
            _procedureAccess = procedureAccess;
            _mapper = mapper;
            _clientProcedureAccess = clientProcedureAccess;
            _userDataAccess = userDataAccess;
            _fileAccess = fileAccess;
        }

        public async Task<ProcedureModel> CreateAsync(CreateProcedureRequest request)
        {
            var accessRequest = _mapper.Map<CreateProcedureAccessRequest>(request);
            var accessResponse = await _procedureAccess.CreateAsync(accessRequest);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<ProcedureModel> DeleteAsync(string id)
        {
            var accessResponse = await _procedureAccess.DeleteAsync(id);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<IEnumerable<ProcedureModel>> GetAllAsync()
        {
            var accessResponse = await _procedureAccess.GetAllAsync();
            return _mapper.Map<IEnumerable<ProcedureModel>>(accessResponse);
        }

        public async Task<ProcedureModel> GetByIdAsync(string id, bool active)
        {
            var accessResponse = await _procedureAccess.GetByIdAsync(id, active);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<ProcedureModel> UpdateAsync(UpdateProcedureRequest request)
        {
            var accessRequest = _mapper.Map<UpdateProcedureAccessRequest>(request);
            var accessResponse = await _procedureAccess.UpdateAsync(accessRequest);
            return _mapper.Map<ProcedureModel>(accessResponse);
        }

        public async Task<bool> ValidateIdNameAsync(string name)
        {
            return await _procedureAccess.ValidateIdNameAsync(name);
        }

        public async Task<IEnumerable<string>> ValidateProcedureTeethAsync(IEnumerable<string> ids)
        {
            return await _procedureAccess.ValidateProcedureTeethAsync(ids);
        }

        public async Task<IEnumerable<ProcedureModel>> GetProceduresByUserIdAsync(string id)
        {
            var userClientList = (await _userDataAccess.GetUserClientsByUserIdAsync(id)).Select(x => x.Id);
            var clientProceduresAccessResponse = await _clientProcedureAccess.GetClientProceduresByUserClientIdAsync(userClientList);
            var allProcedures = await _procedureAccess.GetAllAsync();
            var myProcedureIds = clientProceduresAccessResponse.Select(x => x.ProcedureId);
            var procedures = allProcedures.Where(x => myProcedureIds.Contains(x.Id));
            return _mapper.Map<IEnumerable<ProcedureModel>>(procedures);
        }

        public async Task<ClientProcedureModel> CreateClientProcedureAsync(CreateClientProcedureRequest request)
        {
            var userClient = await _userDataAccess.GetUserClientAsync(new UserClientAccessRequest(request.ClientId, request.UserId));
            userClient ??= await _userDataAccess.CreateUserClientAsync(new UserClientAccessRequest(request.ClientId, request.UserId));
            var urls = new List<string>();
            request.Files.ToList().ForEach(async file =>
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid()}-{Guid.NewGuid()}{extension}".ToLower();
                var url = await UploadFileAsync(file, fileName, request.UserId);
                urls.Add(url);
            });

            var accessRequest = new CreateClientProcedureAccessRequest(userClient.Id.ToString(), request.ProcedureId, urls);
            var accessResponse = await _clientProcedureAccess.CreateClientProcedureAsync(accessRequest);

            return new ClientProcedureModel(
                accessResponse.Id,
                accessResponse.ProcedureId,
                accessResponse.UserClientId
            );
        }

        public async Task<ClientProcedureModel> UpdateClientProcedureAsync(UpdateClientProcedureRequest request)
        {
            var accessRequest = new UpdateClientProcedureAccessRequest(request.UserClientId, request.ProcedureId);
            var accessResponse = await _clientProcedureAccess.UpdateClientProcedureAsync(accessRequest);
            return new ClientProcedureModel(
                accessResponse.Id,
                accessResponse.ProcedureId,
                accessResponse.UserClientId
            );
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string userClientId, string procedureId)
        {
            return await _clientProcedureAccess.CheckExistsClientProcedureAsync(userClientId, procedureId);
        }

        public async Task<bool> CheckExistsClientProcedureAsync(string clientProcedureId)
        {
            return await _clientProcedureAccess.CheckExistsClientProcedureAsync(clientProcedureId);
        }

        private async Task<string> UploadFileAsync(IFormFile file, string fileName, string folder)
        {
            using var stream = file.OpenReadStream();
            var fileAccessRequest = new UploadFileAccessRequest(stream, fileName, true, folder);
            var blobUrl = await _fileAccess.UploadAsync(fileAccessRequest);
            return blobUrl;
        }
    }
}
