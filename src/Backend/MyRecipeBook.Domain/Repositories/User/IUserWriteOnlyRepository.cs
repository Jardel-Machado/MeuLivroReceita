namespace MyRecipeBook.Domain.Repositories.User;
public interface IUserWriteOnlyRepository
{
    Task AddUserAsync(Entities.User user);
}
