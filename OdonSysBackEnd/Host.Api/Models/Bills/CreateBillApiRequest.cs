﻿using Contract.Admin.Clients;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

namespace Host.Api.Models.Bills
{
    public class CreateBillApiRequest : IValidatableObject
    {
        [Required]
        public string BillNumber { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese iva válido")]
        public int Iva10 { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese total iva válido")]
        public int TotalIva { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto sub total válido")]
        public int SubTotal { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Por favor ingrese un monto total válido")]
        public int Total { get; set; }
        [Required]
        public string Timbrado { get; set; }
        [Required]
        public BillStatus Status { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public IEnumerable<BillDetailApiRequest> BillDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var clientManager = (IClientManager)validationContext.GetService(typeof(IClientManager));
            _ = clientManager.GetByIdAsync(ClientId.ToString()).GetAwaiter().GetResult();
            var invoiceDetailSum = BillDetails.Select(x => x.FinalPrice).Sum();
            if (Total != invoiceDetailSum)
            {
                results.Add(new ValidationResult($"Valor total no coincide con los montos de procedimientos ingresados."));
            }
            if (Status != BillStatus.Nuevo)
            {
                results.Add(new ValidationResult($"Estado de nuevo tratamiento inválido."));
            }
            return results;
        }
    }
}
