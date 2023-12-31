using Domain.Models;
using Domain.Models.User;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database
{
    public interface IMockDatabase
    {
        List<Dog> Dogs { get; set; }
        List<Cat> Cats { get; set; }
        List<UserModel> Users { get; set; }

        void SaveChanges();
    }
}
