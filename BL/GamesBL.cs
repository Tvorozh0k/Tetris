using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class GamesBL : IGamesBL
    {
        private IGamesDal _dal;

        public List<Game> GetByUserId(int userId)
        {
            return _dal.GetByUserId(userId);
        }

        public GamesBL(IGamesDal dal)
        {
            _dal = dal;
        }

        public Game GetById(int id)
        {
            return _dal.GetById(id);
        }

        public void Add (Game game)
        {
            _dal.Add(game);
        }
    }
}