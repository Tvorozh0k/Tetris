using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class UsersBL : IUsersBL
    {
        private IUsersDal _dal;

        public UsersBL(IUsersDal dal)
        {
            _dal = dal;
        }

        public User GetByLogin(string login)
        {
            return _dal.GetByLogin(login);
        }

        public User GetById(int id)
        {
            return _dal.GetById(id);
        }
    }
}