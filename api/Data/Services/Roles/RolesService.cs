﻿using api.Data.Entities;
using api.Data.Repositories;
using api.Data.Repositories.Users;
using api.Data.Requests;
using api.Data.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data.Services.Roles
{
    public class RolesService : IBaseService<RolesResponse, RolesRequest>
    {
        private readonly IBaseRepository<RolesEntity> _repository;
        private readonly IUsersRepository _userRepository;
        private readonly IMapper _mapper;

        public RolesService(IBaseRepository<RolesEntity> repository, IMapper mapper, IUsersRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<RolesResponse> GetAll()
        {
            var response = _repository.GetAll();
            return _mapper.Map<List<RolesResponse>>(response);
        }

        public RolesResponse GetById(int id)
        {
            var response = _repository.GetById(id);
            return _mapper.Map<RolesResponse>(response);
        }

        public async Task<RolesResponse> SaveAsync(RolesRequest request)
        {
            var requestModel = _mapper.Map<RolesEntity>(request);
            await _repository.SaveAsync(requestModel);
            return _mapper.Map<RolesResponse>(requestModel);
        }

        public async Task<RolesResponse> UpdateAsync(int id, RolesRequest request)
        {
            var requestModel = _mapper.Map<RolesEntity>(request);
            await _repository.UpdateAsync(id, requestModel);
            return _mapper.Map<RolesResponse>(requestModel);
        }

        public async Task DeleteAsync(int id)
        {
            var users = _userRepository.GetAll();

            if (users.Where(u => u.RoleId == id).Any())
            {
                throw new Exception("Can't delete role because it has a user using it.");
            }

            await _repository.DeleteAsync(id);
        }
    }
}
