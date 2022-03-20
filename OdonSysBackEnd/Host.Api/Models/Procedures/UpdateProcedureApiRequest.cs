﻿using Contract.Procedure.Procedures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Host.Api.Models.Procedures
{
    public class UpdateProcedureApiRequest : IValidatableObject
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IEnumerable<string> ProcedureTeeth { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
            var validId = procedureManager.ValidateIdNameAsync(Id).GetAwaiter().GetResult();
            if (validId)
            {
                results.Add(new ValidationResult($"Identificador {Id} no existe."));
            }
            var invalidTeeth = procedureManager.ValidateProcedureTeethAsync(ProcedureTeeth).GetAwaiter().GetResult();
            if (invalidTeeth.Any())
            {
                foreach (var id in invalidTeeth)
                {
                    results.Add(new ValidationResult($"El identificador del diente {id} no existe."));
                }
            }
            return results;
        }
    }
}