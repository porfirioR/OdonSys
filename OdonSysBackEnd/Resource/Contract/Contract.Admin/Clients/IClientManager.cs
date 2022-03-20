﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Admin.Clients
{
    public interface IClientManager
    {
        Task<ClientModel> CreateAsync(CreateClientRequest request);
        Task<ClientModel> UpdateAsync(UpdateClientRequest request);
        Task<ClientModel> UpdateAsync(ClientModel request);
        Task<IEnumerable<ClientModel>> GetAllAsync();
        Task<ClientModel> GetByIdAsync(string id, bool active = true);
        Task DeleteAsync(string id);
    }
}