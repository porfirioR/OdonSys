using System;
using Utilities.Enums;

namespace Access.Contract.Teeth
{
    public class ToothAccessResponse
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public Jaw Jaw { get; set; }
        public Quadrant Quadrant { get; set; }
        public DentalGroup Group { get; set; }
    }
}
