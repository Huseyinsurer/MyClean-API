using Domain.Models.Animal;

namespace Domain.Models
{
    public class Dog : AnimalModel
    {
        public string Breed { get; set; } = string.Empty;
        public int? Weight { get; set; }
    }
}