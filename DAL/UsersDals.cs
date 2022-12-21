using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class UsersDal : IUsersDal
    {
        private List<User> users = new List<User>() {
            new User() { Id = 1, Name = "MysticJR", Age = 27, Phone = "298243", Password = "5332fd419" },
            new User() { Id = 2, Name = "DIMON65", Age = 17, Phone = "434536", Password = "802adcbfd" },
            new User() { Id = 3, Name = "rollexx63", Age = 16, Phone = "489836", Password = "fcaf4d735" },
            new User() { Id = 4, Name = "crysoul", Age = 20, Phone = "258860", Password = "8544036b7" },
        };

        public User GetById(int id)
        {
            return users.FirstOrDefault(item => item.Id == id);
        }
        public User GetByLogin(string login)
        {
            return users.FirstOrDefault(item => item.Name == login);
        }

        public void Add (User user)
        {
            users.Add(user);
        }
    }
}