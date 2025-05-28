using Access.Contract.Teeth;

namespace Contract.Workspace.Teeth;

public interface IToothManagerBuilder
{
    ToothModel MapToothAccessModelToToothModel(ToothAccessModel accessModel);
}
