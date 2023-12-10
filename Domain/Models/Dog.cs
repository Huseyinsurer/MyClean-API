using Domain.Models.Animal;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        public bool LikesToPlay { get; set; }

        public string Meow()
        {
            return "This animal meows";
        }
    }
}

