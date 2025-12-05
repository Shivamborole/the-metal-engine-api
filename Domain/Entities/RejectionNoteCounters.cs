using System;

namespace InvoicingAPI.Domain.Entities
{
    public class RejectionNoteCounters
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }

        public string DocumentType { get; set; }   // e.g. "RejectionNote"
        public int CurrentNumber { get; set; }      // auto-increment
        public string Prefix { get; set; }          // e.g. RN
        public int Padding { get; set; }            // e.g. 5 → 00001
    }
}
