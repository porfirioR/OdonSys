using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdonSysBackEnd.Models.Clients
{
    public class UpdateClientApiRequest : IValidatableObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Phone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            return results;
        }
    }
}
