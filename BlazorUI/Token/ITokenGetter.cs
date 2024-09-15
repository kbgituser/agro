﻿using Agro.Model.Dto.User;
using Agro.Model.WebApi.Models;

namespace BlazorUI.Token
{
    public interface ITokenGetter
    {
        static AuthenticatedResponse TokenInfo { get; set; }

        Task<AuthenticatedResponse> GetTokenAsync(Login login);
    }
}