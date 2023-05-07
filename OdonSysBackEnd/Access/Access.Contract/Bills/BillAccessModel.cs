using System;
using System.Collections.Generic;
using Utilities.Enums;

namespace Access.Contract.Bills
{
    public record BillAccessModel(
        Guid Id,
        string BillNumber,
        int Iva10,
        int TotalIva,
        int SubTotal,
        int Total,
        string Timbrado,
        BillStatus Status,
        Guid ClientId,
        IEnumerable<BillDetailAccessModel> Details
    );
}
