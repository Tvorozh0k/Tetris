using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMDal
{
    public class OrmGamesDal : IGamesDal
    {
        public List<Entities.Game> GetByUserId(int userId)
        {
            var context = new DefaultDbContext();

            try
            {
                var query = context.Games.Where(x => x.UserId == userId).ToList();

                var res = new List<Entities.Game>();

                foreach(var elem in query)
                {
                    var entity = new Entities.Game()
                    {
                        Id = elem.Id,
                        UserId = elem.UserId,
                        Score = elem.Score,
                        GameDate = elem.GameDate
                    };

                    res.Add(entity);
                }

                return res;
            }
            finally
            {
                context.Dispose();
            }
        }

        public Entities.Game GetById(int id)
        {
            var context = new DefaultDbContext();

            try
            {
                var game = context.Games.FirstOrDefault(item => item.Id == id);

                if (game == null)
                {
                    return null;
                }

                var entity = new Entities.Game()
                {
                    Id = game.Id,
                    UserId = game.UserId,
                    Score = game.Score,
                    GameDate = game.GameDate
                };

                return entity;
            }
            finally
            {
                context.Dispose();
            }
        }

        public void Add (Entities.Game newGame)
        {
            var context = new DefaultDbContext();

            try
            {
                var entity = new Games()
                {
                    Score = newGame.Score,
                    GameDate = newGame.GameDate,
                    UserId = newGame.UserId
                };

                context.Games.Add(entity);
                context.SaveChanges();
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}