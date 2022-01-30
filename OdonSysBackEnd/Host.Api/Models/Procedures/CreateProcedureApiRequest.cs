using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Models.Procedures
{
    public class CreateProcedureApiRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string EstimatedSessions { get; set; }
        [Required]
        public IEnumerable<string> ProcedureTeeth { get; set; }
    }
}
