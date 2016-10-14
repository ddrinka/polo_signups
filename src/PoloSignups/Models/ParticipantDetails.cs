using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoloSignups.Models
{
    public class ParticipantDetails
    {
        public PersonDetails ParticipantInfo { get; set; }
        public PersonDetails GuardianInfo { get; set; }
        public string ClubLocation { get; set; }
        public string TeamName { get; set; }
    }
}
