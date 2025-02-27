﻿using FluentValidation;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Repositories.Roles;
using JogandoBack.API.Data.Repositories.Users;
using Microsoft.AspNetCore.Http;

namespace JogandoBack.API.Data.Validators
{
    public class UsersValidator : AbstractValidator<UsersRequest>
    {
        private readonly IUsersRepository _usersRepository;

        private readonly IRolesRepository _rolesRepository;

        private readonly IHttpContextAccessor _httpContexAccessor;

        public UsersValidator(IHttpContextAccessor httpContexAccessor, IUsersRepository usersRepository, IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;

            _httpContexAccessor = httpContexAccessor;

            _rolesRepository = rolesRepository;

            int userId = GetUserIdFromPath(_httpContexAccessor.HttpContext.Request.Path.Value);

            RuleFor(field => field.Name)
                .MinimumLength(2)
                .MaximumLength(200)
                .NotNull();

            RuleFor(field => field.Password)
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(field => field.Email)
                .EmailAddress()
                .NotNull()
                .MaximumLength(100)
                .Must(email => _usersRepository.IsUniqueEmail(email, userId))
                    .WithMessage("Email already exists.");

            RuleFor(field => field.RoleId)
                .Must(roleId => _rolesRepository.Exists(roleId))
                    .WithMessage("Role doesn't exist.")
                .NotNull();
        }

        public int GetUserIdFromPath(string path)
        {
            int userId;
            int.TryParse(path.Replace("/api/users/", ""), out userId);
            return userId;
        }
    }
}
