using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly MyRecipeBookDbContext dbContext;

    public UserRepository(MyRecipeBookDbContext dbContext)
    {
        this.dbContext = dbContext;
    }   

    public async Task<bool> ExistActiveUserWithEmail(string email)
    { 
       return await dbContext.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);      
    }
}
