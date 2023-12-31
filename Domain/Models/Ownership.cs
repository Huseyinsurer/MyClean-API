using Domain.Models.Animal;
using Domain.Models.User;
using System;

namespace Domain.Models
{
    public class Ownership
    {
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        public Guid AnimalId { get; set; }
        public AnimalModel Animal { get; set; }
    }
}