using Community.CQRS.Abstractions;
using FluentValidation;

namespace Community.Validation.Decorator;

[Attributes.Decorator]
public class ValidateCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly IValidator<T> _validator;
    private readonly ICommandHandler<T> _handler;

    public ValidateCommandHandlerDecorator(IValidator<T> validator, ICommandHandler<T> handler)
    {
        _validator = validator;
        _handler = handler;
    }

    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid)
        {
            return _handler.HandleAsync(command, cancellationToken);
        }

        throw new ValidationException(validationResult.Errors);
    }
}