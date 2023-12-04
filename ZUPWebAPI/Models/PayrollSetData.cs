using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Models
{
    public class PayrollSetData : UserAuthenticationData
    {
        public List<PayrollEntity> payrolls { get; set; }
        public string idDocZup { get; set; }    
        public string? NomDocZup { get; set; }
        public DateTime? DateDocZup { get; set; }   = DateTime.Now;
    }
}
