using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUsersBL
    {
        User GetById(int id);
        User GetByLogin(string login);

    }
}