using Access.Contract.Teeth;
using Contract.Workspace.Teeth;

namespace Manager.Workspace.Teeth
{
    internal class ToothManagerBuilder : IToothManagerBuilder
    {
        public ToothModel MapToothAccessModelToToothModel(ToothAccessModel accessModel) => new(
            accessModel.Id,
            accessModel.Number,
            accessModel.Name,
            accessModel.Jaw,
            accessModel.Quadrant,
            accessModel.Group
        );
    }
}
