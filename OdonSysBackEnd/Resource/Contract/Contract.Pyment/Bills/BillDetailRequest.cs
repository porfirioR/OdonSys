namespace Contract.Pyment.Bills
{
    public record BillDetailRequest(
        Guid ClientProcedureId,
        int ProducePrice,
        int FinalPrice
    );
}
