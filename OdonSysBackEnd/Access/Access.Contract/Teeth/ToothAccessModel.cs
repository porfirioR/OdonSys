using Utilities.Enums;

namespace Access.Contract.Teeth
{
    public record ToothAccessModel(
        string Id,
        int Number,
        string Name,
        Jaw Jaw,
        Quadrant Quadrant,
        DentalGroup Group
    );
}
