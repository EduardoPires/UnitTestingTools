using System;
using System.Collections.Generic;

namespace UTT.Domain.Customers.Services
{
    public interface ICustomerService : IDisposable
    {
        IEnumerable<Customer> GetAllActive();
        void Register(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);
        void Inactivate(Customer customer);
    }
}