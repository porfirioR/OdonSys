﻿using Contract.Workspace.Procedures;
using System.ComponentModel.DataAnnotations;

namespace Host.Api.Contract.Procedures;

public class CreateProcedureApiRequest : IValidatableObject
{
    [Required]
    [StringLength(60, ErrorMessage = "Longitud máxima de nombre es 60.")]
    public string Name { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Longitud máxima de nombre es 100.")]
    public string Description { get; set; }
    [Required]
    public int Price { get; set; }
    //public IEnumerable<string> ProcedureTeeth { get; set; }
    public bool XRays { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        var procedureManager = (IProcedureManager)validationContext.GetService(typeof(IProcedureManager));
        var validName = procedureManager.ValidateIdNameAsync(Name).GetAwaiter().GetResult();
        if (!validName)
        {
            results.Add(new ValidationResult($"Nombre {Name} ya esta en uso."));
        }
        //var invalidTeeth = procedureManager.ValidateProcedureTeethAsync(ProcedureTeeth).GetAwaiter().GetResult();
        //if (invalidTeeth.Any())
        //{
        //    foreach (var id in invalidTeeth)
        //    {
        //        results.Add(new ValidationResult($"El identificaor {id} no existe."));
        //    }
        //}
        return results;
    }
}
