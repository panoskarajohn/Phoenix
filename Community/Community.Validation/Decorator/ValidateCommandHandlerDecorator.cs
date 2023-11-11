using Community.CQRS.Abstractions;
using FluentValidation;

namespace Community.Validation.Decorator;

[Attributes.Decorator]
public class ValidateCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly IValidator<T> _validator;

    public ValidateCommandHandlerDecorator(IValidator<T> validator)
    {
        _validator = validator;
    }

    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid)
        {
            return Task.CompletedTask;
        }

        throw new ValidationException(validationResult.Errors);
    }
}