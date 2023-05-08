using Utilities.Enums;

namespace Contract.Pyments.Bills
{
    public record HeaderBillRequest(
        string BillNumber,
        int Iva10,
        int TotalIva,
        int SubTotal,
        int Total,
        string Timbrado,
        BillStatus Status,
        Guid ClientId,
        IEnumerable<BillDetailRequest> BillDetails
    );
}
