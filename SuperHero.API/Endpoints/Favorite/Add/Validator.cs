using FastEndpoints;
using FluentValidation;

namespace SuperHero.API.Endpoints.Favorite.Add
{
    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.SuperheroId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
