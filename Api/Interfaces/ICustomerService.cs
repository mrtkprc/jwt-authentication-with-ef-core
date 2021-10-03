using Api.Requests;
using Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Interfaces
{
    public interface ICustomerService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
