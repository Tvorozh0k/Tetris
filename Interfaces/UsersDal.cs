using Entities;
using System;

namespace Interfaces
{
    public interface IUsersDal
    {
        User GetById(int id);
        User GetByLogin(string login);
    }
}