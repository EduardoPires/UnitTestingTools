using UTT.Domain.Core;

namespace UTT.Domain.Customers.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByEmail(string email);
    }
}