using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Procedures
{
    public class UpdateProcedureApiRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
