using System.Collections.Immutable;
using ZUPWebAPI.Entities;
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
            User user = new User(-1, "Пользователь с таким логином не найден", userAuthenticationData.Login, userAuthenticationData.Password);

            var listFindUserEntity = userRepository.UserFindeByLogin(userAuthenticationData.Login);

            if (listFindUserEntity is null || listFindUserEntity.Count() == 0)
            {
                //     пользователь с таким логином не найден
                return user;
            }

            if(listFindUserEntity.Count() != 1)
            {
                // Слишком много пользоветелей с таким логином.
                user.Id = -1;
                user.Name = "Путаница пользователей с таким логином. Просьба обатиться с службу поддержки.";
                return user;
            }

            //  вернули только одного пользователя
            //  а проще низя ?     низя ((       
            UserEntity userEntity = listFindUserEntity.FirstOrDefault();
            user.Id = userEntity.id;
            user.Name = userEntity.name;
            user.Password = userEntity.password;

            //  кодим пароль в МД5, сравниваем.
            string userPassMD5;
            userPassMD5 = Md5.MD5Hash(user.Password);

            //  пароль не совпал
            if(userPassMD5 != userAuthenticationData.Password)
            {
                user.Id = -1;
                user.Name = "Не совпадение по имени и паролю.";
                return user;
            }

            //  польователь аудентифицирован нормально
            return user;
        }
    }
}
