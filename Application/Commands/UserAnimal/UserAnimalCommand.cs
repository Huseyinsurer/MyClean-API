using MediatR;

namespace Application.Commands.UserAnimals
{
    public class AssociateUserWithAnimalCommand : IRequest<bool>
    {
        public UserAnimalDto UserAnimal { get; }

        public AssociateUserWithAnimalCommand(UserAnimalDto userAnimal)
        {
            UserAnimal = userAnimal;
        }
    }

    public class DisassociateUserFromAnimalCommand : IRequest<bool>
    {
        public UserAnimalDto UserAnimal { get; }

        public DisassociateUserFromAnimalCommand(UserAnimalDto userAnimal)
        {
            UserAnimal = userAnimal;
        }
    }
}
