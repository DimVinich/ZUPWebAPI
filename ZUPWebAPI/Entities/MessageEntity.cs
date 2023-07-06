namespace ZUPWebAPI.Entities
{
    public class MessageEntity
    {
        //      Нигде пока не используются и наверноне будет 
        //      Для предачи ошибки используем пока ErrorEntity
        //      нифига не верно, используется практически везде. Нужно было вообче только на этой сущьности останавливаться.

        public int code { get; set; }
        public string message { get; set; }
    }
}
