using Microsoft.Identity.Client;
using System.Net.NetworkInformation;

namespace ZUPWebAPI.Models
{
    public class KPIValueGetData : UserAuthenticationData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int idEmployee { get; set; }
        public int idUnit { get; set; } 
    }
}
