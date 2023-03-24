﻿using Contract.Workspace.Procedures;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Procedures
{
    public class CreateUserProcedureApiRequest : IValidatableObject
    {
        [Required]
        public string ProcedureId { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un número valido")]
        public int Price { get; set; }
        [Required]
        public bool Anhestesia { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
            _ = procedureManager.GetByIdAsync(ProcedureId, true).GetAwaiter().GetResult();
            return results;
        }
    }
}
