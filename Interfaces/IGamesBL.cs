using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IGamesBL
    {
        List<Game> GetByUserId(int userId); 

        Game GetById(int id);

        void Add(Game game);

    }
}