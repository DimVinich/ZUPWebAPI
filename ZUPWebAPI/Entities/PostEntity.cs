namespace ZUPWebAPI.Entities
{
    public class PostEntity
    {
        public int IdPost { get; set; }
        public string NPost { get; set; }
        public int IdExpense { get; set; }
        public int IdUnit { get; set;}
        public int IsChief {get; set; }
        public int NumberOfStaffUnits { get; set;}
    }
}
