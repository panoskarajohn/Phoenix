using Artist.Contracts;
using Artist.Contracts.Commands;
using FluentValidation;

namespace Artist.Application.Validation;

public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ArtistDescription).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.ArtistNickname).NotEmpty();
        
        RuleFor(x => x.BirthDate).LessThan(DateTime.UtcNow);
        RuleFor(x => x.BirthDate).NotEqual(default(DateTime));
    }
}