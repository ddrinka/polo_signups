using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoloSignups.Models
{
    public class PublicConfiguration
    {
        public string HostingClub { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public bool ACAMembershipRequired { get; set; }
        public string GoogleDocToken { get; set; }
        public string PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
