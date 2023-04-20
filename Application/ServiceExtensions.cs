using Application.Features.Catalog.Commands.AddCategory;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Common.Behaviors;

namespace Application
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Добовляет компонетны урованя приложения
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
        }
    }
}
