﻿namespace MyRecipeBook.Domain.Repositories;
public interface IUnitOfWork
{
    Task CommitAsync();
}