namespace Access.Contract.Bills
{
    public record BillDetailAccessModel(
        Guid Id,
        Guid HeaderBillId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice
    );
}
