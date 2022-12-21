using Entities;
using System.Collections.Generic;
using System;

namespace Interfaces
{
    public interface IGamesDal
    {
        List<Game> GetByUserId(int userId);
        Game GetById(int id);

        void Add(Game game);
    }
}