﻿namespace JogandoBack.API.Data.Models.Responses
{
    public class LoginResponse
    {
        public UsersResponse User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
