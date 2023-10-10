namespace ZUPWebAPI.Models
{
    public class TimeSheetGetData : UserAuthenticationData
    {
        public int idEmploee { get; set; }
        public int idPost { get; set; }
    }
}
