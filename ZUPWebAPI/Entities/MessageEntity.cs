namespace ZUPWebAPI.Entities
{
    public class MessageEntity
    {
        //      Нигде пока не используются и наверноне будет 
        //      Для преедачи ошибки используем пока ErrorEntity

        public int code { get; set; }
        public string message { get; set; }
    }
}
