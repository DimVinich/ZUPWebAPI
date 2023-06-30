using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Models
{
    public class EmployeeChangeData : EmployeeEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
