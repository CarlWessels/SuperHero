using FastEndpoints;
using FluentValidation;

namespace SuperHero.API.Endpoints.Superhero.Get
{
    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.SuperheroId).NotEmpty();
        }
    }
}
