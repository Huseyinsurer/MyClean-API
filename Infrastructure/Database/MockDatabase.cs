// MockDatabase.cs
using System;
using System.Collections.Generic;
using Domain.Models;
using Domain.Models.User;

namespace Infrastructure.Database
{
    public class MockDatabase : IMockDatabase
    {
        public List<Dog> Dogs
        {
            get { return allDogs; }
            set { allDogs = value; }
        }

        private static List<Dog> allDogs = new()
        {
            new Dog { Id = Guid.NewGuid(), Name = "Björn"},
            new Dog { Id = Guid.NewGuid(), Name = "Patrik"},
            new Dog { Id = Guid.NewGuid(), Name = "Alfred"},
            new Dog { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestDogForUnitTests"}
        };

        public List<Cat> Cats
        {
            get { return allCats; }
            set { allCats = value; }
        }

        private static List<Cat> allCats = new()
        {
            new Cat { Id = Guid.NewGuid(), Name = "Håkan"},
            new Cat { Id = Guid.NewGuid(), Name = "Robert"},
            new Cat { Id = Guid.NewGuid(), Name = "Dani"},
            new Cat { Id = new Guid("12345678-1234-5678-1234-567812345678"), Name = "TestCatForUnitTests"}
        };

        public List<UserModel> Users
        {
            get { return allUsers; }
            set { allUsers = value; }
        }

        private static List<UserModel> allUsers = new()
        {
            new UserModel { Id = Guid.NewGuid(), Username = "user1", Userpassword = "password1" },
            new UserModel { Id = Guid.NewGuid(), Username = "user2", Userpassword = "password2" },
            // Lägg till fler användare här
        };

        // Lägg till andra entiteter om det behövs...

        // Övrig kod för MockDatabase
    }
}
