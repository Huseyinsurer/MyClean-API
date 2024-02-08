using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
    {
        private readonly ApiMainContext _dbContext;

        public DeleteUserCommandHandler(ApiMainContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämta användaren från databasen baserat på användar-ID
                var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (userToDelete != null)
                {
                    // Ta bort användaren från databasen
                    _dbContext.Users.Remove(userToDelete);

                    // Spara ändringarna i databasen
                    await _dbContext.SaveChangesAsync(cancellationToken);

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

