using System.IO;

namespace CeleryArchitectureTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Amazon.DynamoDBv2;
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.Model;
    using AutoMapper;
    using CeleryArchitectureDemo;
    using MediatR;
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

            // TODO: override any classes that need to be replaced with fakes

            var rootContainer = services.BuildServiceProvider();
            ScopeFactory = rootContainer.GetService<IServiceScopeFactory>();
        }

        public static IAmazonDynamoDB Query()
        {
            using var scope = ScopeFactory.CreateScope();
            var client = scope.ServiceProvider.GetService<IAmazonDynamoDB>();
            return client;
        }

        public static IDynamoDBContext QueryContext()
        {
            return new DynamoDBContext(Query());
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

                beforeSend?.Invoke(serviceProvider);

                response = await serviceProvider.GetService<IMediator>().Send(message);

                afterSuccessfulSend?.Invoke(serviceProvider);
            }

            return response;
        }

        public static async Task CreateTable()
        {
            using var scope = ScopeFactory.CreateScope();
            var tableName = Configuration.GetValue<string>("Todo:TableName");
            var client = scope.ServiceProvider.GetService<IAmazonDynamoDB>();
            await client.CreateTableAsync(tableName, new List<KeySchemaElement>
            {
                new KeySchemaElement("Id", KeyType.HASH)
            }, new List<AttributeDefinition>
            {
                new AttributeDefinition("Id", ScalarAttributeType.S)
            }, new ProvisionedThroughput(3, 1));

            DescribeTableResponse tableStatus = null;
            var tries = 3;
            do
            {
                try
                {
                    tableStatus = await client.DescribeTableAsync(tableName);
                    tries--;
                }
                catch (ResourceNotFoundException)
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }

            } while (tableStatus != null && tableStatus.Table.TableStatus != TableStatus.ACTIVE && tries >= 0);
        }

        public static async Task DeleteTable()
        {
            using var scope = ScopeFactory.CreateScope();
            var tableName = Configuration.GetValue<string>("Todo:TableName");
            var client = scope.ServiceProvider.GetService<IAmazonDynamoDB>();
            try
            {
                await client.DeleteTableAsync(tableName);

                var tries = 3;
                var done = false;

                do
                {
                    try
                    {
                        await client.DescribeTableAsync(tableName);
                        await Task.Delay(TimeSpan.FromSeconds(2));
                        tries--;
                    }
                    catch (ResourceNotFoundException)
                    {
                        done = true;
                    }
                } while (!done && tries >= 0);
            }
            catch (ResourceNotFoundException)
            {
            }
        }
    }
}