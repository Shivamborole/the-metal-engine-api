namespace InvoicingAPI.Domain.Entities
{
    public class DeliveryChallanCounters
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public string DocumentType { get; set; }   // e.g. "Invoice", "DeliveryChallan", "RejectionNote"
        public int CurrentNumber { get; set; }      // auto-increment
        public string Prefix { get; set; }          // optional: DC, RN, INV
        public int Padding { get; set; }            // e.g. 5 → 00001
    }

}
