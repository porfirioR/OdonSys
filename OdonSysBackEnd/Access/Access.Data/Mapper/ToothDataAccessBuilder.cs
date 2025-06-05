using Access.Contract.Teeth;
using Access.Sql.Entities;

namespace Access.Data.Mapper;

internal sealed class ToothDataAccessBuilder : IToothDataAccessBuilder
{
    public ToothAccessModel MapToothToToothAccessModel(Tooth tooth) => new(
        tooth.Id.ToString(),
        tooth.Number,
        tooth.Name,
        tooth.Jaw,
        tooth.Quadrant,
        tooth.Group
    );
}
