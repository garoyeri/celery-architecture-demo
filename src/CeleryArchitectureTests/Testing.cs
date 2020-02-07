using System.IO;

namespace CeleryArchitectureTests
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using CeleryArchitectureDemo;
    using CeleryArchitectureDemo.Infrastructure;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Testing
    {
        public static IServiceScopeFactory ScopeFactory;
        public static IConfigurationRoot Configuration;

        static Testing()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var startup = new Startup(Configuration);
            var services = new ServiceCollection();

            startup.ConfigureServices(services);

            // provided in MVC, but not generally available
            services.AddScoped<TodoContextTransactionFilter>();

            // TODO: override any classes that need to be replaced with fakes

            var rootContainer = services.BuildServiceProvider();
            ScopeFactory = rootContainer.GetService<IServiceScopeFactory>();
        }

        public static DbSet<T> Query<T, TContext>()
            where T : class
            where TContext : DbContext
        {
            var scope = ScopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<TContext>();

            return dbContext.Set<T>();
        }

        public static async Task UsingContextAsync<TContext>(Func<TContext, Task> actions)
            where TContext : DbContext
        {
            using var scope = ScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<TContext>();
            await actions(dbContext);
            await dbContext.SaveChangesAsync();
        }

        public static T Map<T>(object source)
        {
            using var scope = ScopeFactory.CreateScope();
            var mapper = scope.ServiceProvider.GetService<IMapper>();
            return mapper.Map<T>(source);
        }

        public static void CheckMapperConfiguration()
        {
            using var scope = ScopeFactory.CreateScope();
            var mapper = scope.ServiceProvider.GetService<IMapper>();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public static async Task UsingScopeAsync(Func<IServiceScope, Task> actions)
        {
            using var scope = ScopeFactory.CreateScope();
            await actions(scope);
        }

        public static async Task<TResponse> Send<TResponse>(IRequest<TResponse> message,
            Action<IServiceProvider> beforeSend = null,
            Action<IServiceProvider> afterSuccessfulSend = null)
        {
            TResponse response = default;

            using (var scope = ScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var unitOfWork = serviceProvider.GetService<TodoContextTransactionFilter>();
                await unitOfWork.OnActionExecutionAsync(null, async () =>
                {
                    beforeSend?.Invoke(serviceProvider);

                    response = await serviceProvider.GetService<IMediator>().Send(message);

                    return null;
                });

                afterSuccessfulSend?.Invoke(serviceProvider);
            }

            return response;
        }

        public static Task Migrate<TContext>()
            where TContext : DbContext
        {
            return UsingContextAsync<TContext>(context => context.Database.MigrateAsync());
        }

        public static Task DeleteDatabase<TContext>()
            where TContext : DbContext
        {
            using var scope = ScopeFactory.CreateScope();
            var database = scope.ServiceProvider.GetService<TContext>().Database;
            return database.EnsureDeletedAsync();
        }

        // TodoContext shortcuts
        public static Task UsingContextAsync(Func<TodoContext, Task> actions) =>
            UsingContextAsync<TodoContext>(actions);

        public static DbSet<T> Query<T>()
            where T : class
        {
            return Query<T, TodoContext>();
        }
    }
}
