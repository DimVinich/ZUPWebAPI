using ZUPWebAPI.Entities;

namespace ZUPWebAPI.Models
{
    public class PostChangeData : PostEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
