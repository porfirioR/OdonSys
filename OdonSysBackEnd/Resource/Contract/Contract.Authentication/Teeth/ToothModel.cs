using Utilities.Enums;

namespace Contract.Workspace.Teeth
{
    public class ToothModel
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public Jaw Jaw { get; set; }
        public Quadrant Quadrant { get; set; }
        public DentalGroup Group { get; set; }
    }
}
