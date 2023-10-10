using System.Globalization;

namespace ZUPWebAPI.Entities
{
    public class TimeSheetEntity
    {
        public int idEmployee { get; set; }
        public DateTime Date { get; set; } 
        public string TypeDate { get; set; }
        public int Worked { get; set; }
    }
}
