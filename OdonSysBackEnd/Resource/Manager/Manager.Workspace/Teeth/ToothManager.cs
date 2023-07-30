using Access.Contract.Teeth;
using Contract.Workspace.Teeth;

namespace Manager.Workspace.Teeth
{
    internal sealed class ToothManager : IToothManager
    {
        private readonly IToothAccess _toothAccess;
        private readonly IToothManagerBuilder _toothManagerBuilder;

        public ToothManager(IToothAccess procedureAccess, IToothManagerBuilder toothManagerBuilder)
        {
            _toothAccess = procedureAccess;
            _toothManagerBuilder = toothManagerBuilder;
        }

        public async Task<IEnumerable<ToothModel>> GetAllAsync()
        {
            var accessModelList = await _toothAccess.GetAllAsync();
            var modelList = accessModelList.Select(_toothManagerBuilder.MapToothAccessModelToToothModel);
            return modelList;
        }
    }
}
