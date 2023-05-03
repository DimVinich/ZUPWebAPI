using ZUPWebAPI.Services;

namespace ZUPWebAPI.Models
{
    public class ExpenseListGet
    {

        public string Login { get; set; }
        public string Password { get; set; }

        public ExpenseListGet(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

//        User user = UserService

    }
}
