using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMDal
{
    public class OrmUsersDal : IUsersDal
    {
        public Entities.User GetByLogin(string login)
        {
            var context = new DefaultDbContext();

            try
            {
                var user = context.Users.FirstOrDefault(item => item.Name == login);

                if (user == null)
                {
                    return null;
                }

                var entity = new Entities.User()
                {
                    Id = user.Id,
                    Age = user.Age,
                    Name = user.Name,
                    Phone = user.Phone,
                    Password = user.Password
                };

                return entity;
            }
            finally
            {
                context.Dispose();
            }
        }

        public Entities.User GetById(int id)
        {
            var context = new DefaultDbContext();

            try
            {
                var user = context.Users.FirstOrDefault(item => item.Id == id);

                if (user == null)
                {
                    return null;
                }

                var entity = new Entities.User()
                {
                    Id = user.Id,
                    Age = user.Age,
                    Name = user.Name,
                    Phone = user.Phone,
                    Password = user.Password
                };

                return entity;
            }
            finally
            {
                context.Dispose();
            }
        }

        public void Add (Entities.User newUser)
        {
            var context = new DefaultDbContext();

            try
            {
                var entity = new Users()
                {
                    Age = newUser.Age,
                    Name = newUser.Name,
                    Phone = newUser.Phone,
                    Password = newUser.Password,
                    CreationDate = DateTime.Now
                };

                context.Users.Add(entity);
                context.SaveChanges();
            }
            finally
            {
                context.Dispose();
            }
        }
    }
}