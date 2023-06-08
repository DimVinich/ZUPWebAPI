namespace ZUPWebAPI.Entities
{
    public class MessageEntity
    {
        //      Нигде пока не используются и наверноне будет 
        //      Для предачи ошибки используем пока ErrorEntity

        public int code { get; set; }
        public string message { get; set; }
    }
}
