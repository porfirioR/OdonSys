using Utilities.Enums;

namespace Contract.Workspace.Teeth;

public record ToothModel(
    string Id,
    int Number,
    string Name,
    Jaw Jaw,
    Quadrant Quadrant,
    DentalGroup Group
);
