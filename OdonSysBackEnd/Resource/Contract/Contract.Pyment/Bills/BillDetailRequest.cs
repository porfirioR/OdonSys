namespace Contract.Pyments.Bills
{
    public record BillDetailRequest(
        Guid ClientProcedureId,
        int ProducePrice,
        int FinalPrice
    );
}
