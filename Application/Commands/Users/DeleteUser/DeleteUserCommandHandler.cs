using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
    {
        private readonly IMockDatabase _mockDatabase;

        public DeleteUserCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämta användaren från databasen baserat på användar-ID
                var userToDelete = _mockDatabase.Users.FirstOrDefault(u => u.Id == request.UserId);

                if (userToDelete != null)
                {
                    // Ta bort användaren från databasen
                    _mockDatabase.Users.Remove(userToDelete);

                    // Spara ändringarna i databasen (beroende på din implementation)
                    // _mockDatabase.SaveChanges();

                    return new DeleteUserResult();
                }
                else
                {
                    // Användaren finns inte
                    // Här kan du välja att kasta ett undantag eller göra något annat
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Hantera fel om det uppstår
                // Logga eller kasta vidare exceptionen beroende på din implementering
                return null;
            }
        }
    }
}

