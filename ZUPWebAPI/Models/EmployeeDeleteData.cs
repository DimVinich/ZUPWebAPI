using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Models
{
    public class EmployeeDeleteData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdEmployee { get; set; } 
    }
}
