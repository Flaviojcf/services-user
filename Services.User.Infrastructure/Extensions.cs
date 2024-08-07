﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Services.User.Domain.Entities;
using Services.User.Domain.Repositories;
using Services.User.Infrastructure.Persistance.MongoDb;
using Services.User.Infrastructure.Persistance.MongoDb.Repositories;
using UserEntity = Services.User.Domain.Entities.User;

namespace Services.User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddMongo()
                .AddRepositories();

            return services;
        }

        private static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var options = new MongoDbOptions();

                configuration.GetSection("Mongo").Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });

            services.AddTransient(sp =>
            {
                BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

                var options = sp.GetService<MongoDbOptions>();
                var mongoClient = sp.GetService<IMongoClient>();

                var database = mongoClient.GetDatabase(options.Database);

                ConfigureMongoDb();

                return database;
            });

            return services;
        }

        public static void ConfigureMongoDb()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(AggregateRoot)))
            {
                BsonClassMap.RegisterClassMap<AggregateRoot>(cm =>
                {
                    cm.AutoMap();
                    cm.UnmapMember(c => c._events);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(UserEntity)))
            {
                BsonClassMap.RegisterClassMap<UserEntity>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
