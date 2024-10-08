﻿using Models.Character.Requests;
using Models.Character.Responses;

using DataAccess.Interfaces;

namespace Handlers.Character
{
    public class DeleteCharacterHandler
    {
        private readonly ICharacterRepository _repository;

        public DeleteCharacterHandler(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public virtual DeleteCharacterResponse Handle(DeleteCharacterRequest request)
        {
            var response = new DeleteCharacterResponse();

            try
            {
                _repository.Delete(request.UserId, request.CharacterId);
                response.Code = DeleteCharacterResponse.OK;
            }
            catch (ArgumentOutOfRangeException)
            {
                response.Code = DeleteCharacterResponse.NOT_EXISTS;
            }
            catch
            {
                response.Code = DeleteCharacterResponse.DB_ERROR;
            }

            return response;
        }
    }
}
