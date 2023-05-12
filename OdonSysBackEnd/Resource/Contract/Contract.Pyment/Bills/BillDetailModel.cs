namespace Contract.Pyment.Bills
{
    public record BillDetailModel(
        Guid Id,
        Guid HeaderBillId,
        string Procedure,
        int ProcedurePrice,
        int FinalPrice
    );
}
