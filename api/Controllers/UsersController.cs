﻿using AutoMapper;
using JogandoBack.API.Data.Models.Filters;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Repositories.Users;
using JogandoBack.API.Data.Services.Users;
using JogandoBack.API.Data.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersService<UsersResponse, UsersRequest> _usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService<UsersResponse, UsersRequest> usersService, IMapper mapper,
            IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _usersService = usersService;
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PaginationFilter filter)
        {
            try
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                var usersEntity = _usersRepository.GetAll(validFilter);

                var response = _mapper.Map<List<UsersResponse>>(usersEntity);

                var totalRecords = response.Count;

                return Ok(new PagedResponse<List<UsersResponse>>(response, filter.PageNumber, filter.PageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_usersService.GetById(id));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(UsersRequest request)
        {
            try
            {
                var response = await _usersService.SaveAsync(request);

                return Created(HttpContext.Request.GetAbsoluteUri() + $"/{response.Id}", "Created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UsersRequest request)
        {
            try
            {
                await _usersService.UpdateAsync(id, request);

                return Ok("Updated.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _usersService.DeleteAsync(id);

                return Ok("Deleted.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
