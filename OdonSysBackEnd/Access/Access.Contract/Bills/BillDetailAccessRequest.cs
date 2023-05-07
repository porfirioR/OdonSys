using System;

namespace Access.Contract.Bills
{
    public record BillDetailAccessRequest(
        Guid ClientProcedureId,
        int ProducePrice,
        int FinalPrice
    );
}
