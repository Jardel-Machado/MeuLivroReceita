using AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository writeOnlyRepository;
    private readonly IUserReadOnlyRepository readOnlyRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly PasswordEncripter passwordEncripter;

    public RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository, IUserReadOnlyRepository readOnlyRepository, IMapper mapper, PasswordEncripter passwordEncripter, IUnitOfWork unitOfWork)
    {
        this.writeOnlyRepository = writeOnlyRepository;
        this.readOnlyRepository = readOnlyRepository;
        this.mapper = mapper;
        this.passwordEncripter = passwordEncripter;
        this.unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterUserJson> ExecuteAsync(RequestRegisterUserJson request)
    {
        await ValidateRequest(request);

        Domain.Entities.User user = mapper.Map<Domain.Entities.User>(request);

        user.Password = passwordEncripter.Encrypt(request.Password);

        await writeOnlyRepository.AddUserAsync(user);

        await unitOfWork.CommitAsync();

        return new ResponseRegisterUserJson 
        {
            Name = request.Name
        };
    }

    private async Task ValidateRequest(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExist = await readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)        
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("Email", ResourceMessagesException.EMAIL_ALREADY_REGISTERED));   

        if (!result.IsValid)
        {
            var errorNessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorNessages);
        }
    }
}
