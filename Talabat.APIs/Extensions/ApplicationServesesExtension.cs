using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServesesExtension
    {
        public static IServiceCollection AddAppServeses(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped(typeof(IGenarciRepository<>), typeof(GenaricRepository<>));

            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(Parameter => Parameter.Value.Errors.Count() > 0)
                                                         .SelectMany(Parameter => Parameter.Value.Errors)
                                                         .Select(E => E.ErrorMessage).ToList();
                    var ValidationErrorModle = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(ValidationErrorModle);

                };
            });
            return Services;
        }
    }
}
