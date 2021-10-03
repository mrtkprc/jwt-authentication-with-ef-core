using Api.Entities;
using Api.Helpers;
using Api.Interfaces;
using Api.Requests;
using Api.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomersDbContext customersDbContext;
        public CustomerService(CustomersDbContext customersDbContext)
        {
            this.customersDbContext = customersDbContext;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var customer = customersDbContext.Customers.SingleOrDefault(customer => customer.Active && customer.Username == loginRequest.Username);

            if (customer == null)
            {
                return null;
            }
            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, customer.PasswordSalt);

            if (customer.Password != passwordHash)
            {
                return null;
            }

            var token = await Task.Run(() => TokenHelper.GenerateToken(customer));

            return new LoginResponse { Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName, Token = token };
        }
    }
}
