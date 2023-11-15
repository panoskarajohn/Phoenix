using Artist.Contracts;
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
    }
}