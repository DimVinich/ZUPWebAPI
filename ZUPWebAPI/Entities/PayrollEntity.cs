namespace ZUPWebAPI.Entities
{
    public class PayrollEntity
    {
        public string idDocZUP { get; set; }
        public int idEmployee { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string idTypePayroll { get; set; }
        public decimal Amount { get; set; } 
    }
}
