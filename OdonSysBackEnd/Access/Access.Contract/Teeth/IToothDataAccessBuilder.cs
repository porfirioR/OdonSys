using Access.Sql.Entities;

namespace Access.Contract.Teeth
{
    public interface IToothDataAccessBuilder
    {
        ToothAccessModel MapToothToToothAccessModel(Tooth tooth);
    }
}
