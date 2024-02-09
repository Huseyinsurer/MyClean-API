using Domain.Models;
using System.Threading.Tasks;

public interface IUserAnimalRepository
{
    Task<bool> AssociateUserWithAnimal(Ownership ownership);
    Task<bool> DisassociateUserFromAnimal(Ownership ownership);
    Task<List<Ownership>> GetAllAssociatedAnimals();
}