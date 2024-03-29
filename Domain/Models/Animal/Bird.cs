﻿using Domain.Models.Animal;

namespace Domain.Models
{
    public class Bird : AnimalModel
    {
        public bool CanFly { get; set; }
        public string Color { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
