using ZUPWebAPI.Entities;
using ZUPWebAPI.Repositories;

namespace ZUPWebAPI.Services
{
    public class PostService
    {
        PostRepository postRepository = new PostRepository();
        MessageEntity messageEntity = new MessageEntity();

        public PostService()
        {
            messageEntity.code = -1;
            messageEntity.message = "Произошла не установленная ошибка. Обратитесь в сервис деск.";
        }

        //  Проверки на ошибки нужно будет делать более детальные, след. раз

        //  Создание должности
        public MessageEntity PostCreate(PostEntity postEntity)
        {
            int postId;
            try
            {
                postId = postRepository.PostCreate(postEntity);
            }
            catch (Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }

            if (postId < 1) 
            { 
                messageEntity.code = -1;
                messageEntity.message = "Создание должности не произошло. Обратитеьс в сервис деск.";
                return messageEntity;
            }

            messageEntity.code = postId;
            messageEntity.message = "Должность успешно создана";
            return messageEntity;
        }

        //  Изменение должности
        public MessageEntity PostChange(PostEntity postEntity)
        {
            int numberOfModifed;
            try
            {
                numberOfModifed = postRepository.PostChange(postEntity);
            }
            catch(Exception ex)
            {
                messageEntity.code = -1;
                messageEntity.message = ex.Message;
                return messageEntity;
            }

            if(numberOfModifed < 1)
            {
                messageEntity.code = -1;
                messageEntity.message = "Изменение должности не произошло. Обратитеьс в сервис деск.";
                return messageEntity;
            }
            messageEntity.code = numberOfModifed;
            messageEntity.message = "Должность успешно изменена";
            return messageEntity;
        }

        //  Удаление должности
        //public MessageEntity PostDelete(PostEntity postEntity)
        //{
        //    int numberOfModifed;

        //}

    }
}
