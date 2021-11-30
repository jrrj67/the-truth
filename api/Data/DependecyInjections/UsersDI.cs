﻿using JogandoBack.API.Data.Repositories.Users;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services.Users;
using JogandoBack.API.Data.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace JogandoBack.API.Data.DependecyInjections
{
    public abstract class UsersDI
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IValidator<UsersRequest>, UsersValidator>();

            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IUsersService<UsersResponse, UsersRequest>, UsersService>();
        }
    }
}
