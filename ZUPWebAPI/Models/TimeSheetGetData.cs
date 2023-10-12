namespace ZUPWebAPI.Models
{
    public class TimeSheetGetData : UserAuthenticationData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int idEmployee { get; set; }
        public int idUnit { get; set; }
    }
}
