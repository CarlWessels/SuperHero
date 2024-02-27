using FastEndpoints;
using FluentValidation;
namespace SuperHero.API.Endpoints.Favorite.Get
{
    public class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
