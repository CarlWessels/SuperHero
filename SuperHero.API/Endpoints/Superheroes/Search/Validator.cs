using FastEndpoints;
using FluentValidation;

namespace SuperHero.API.Endpoints.Superheroes.Search
{
    public class Validator : Validator<Request>
    {
        public Validator()
        {
        }
    }
}
