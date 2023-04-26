using ZUPWebAPI.Models;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class UserService
    {
        UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        public User Authenticate(UserAuthenticationData userAuthenticationData)
        {
            var listFindUserEntity = userRepository.UserFindeByLogin(userAuthenticationData.Login);

            if (listFindUserEntity is null)
            {
                return new User(-1, "Пользователь с таким логином не найден", userAuthenticationData.Login, userAuthenticationData.Password);
            }

            if(listFindUserEntity.Count() != 1)
            {
                // Слишком много пользоветелей с таким логином.
            }

            //  всё норм будем формировать пользователя

            //  кодим пароль в МД5, сравниваем.

            //  всё плохо

            //  всё хорошо

            return null;
        }
    }
}
