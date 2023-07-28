using Access.Sql.Entities;

namespace Access.Contract.Teeth
{
    public interface IToothAccessBuilder
    {
        ToothAccessModel MapToothToToothAccessModel(Tooth tooth);
    }
}
