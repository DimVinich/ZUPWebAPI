using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Models
{
    public class EmployeeDeleteData : EmployeeEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int idEmployeer { get; set; }
    }
}
