namespace ZUPWebAPI.Entities
{
    public class ErrorEntity
    {
        public int code { get; set; }
        public string message { get; set; }

        public ErrorEntity()
        {
            code = -1;
            message = "Ошибка не определена";
        }

        public ErrorEntity(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
