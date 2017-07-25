using System.Collections.Generic;
using System.Linq;
using MediatR;
using UTT.Domain.Customers.Events;
using UTT.Domain.Customers.Repository;

namespace UTT.Domain.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;

        public CustomerService(ICustomerRepository customerRepository, 
                               IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public IEnumerable<Customer> GetAllActive()
        {
            return _customerRepository.GetAll().Where(c => c.Active);
        }

        public void Register(Customer customer)
        {
            if(!customer.IsValid())
                return;

            _customerRepository.Add(customer);
            _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "Welcome", "Hello!"));
        }

        public void Update(Customer customer)
        {
            if (customer.IsValid())
                return;

            _customerRepository.Update(customer);
            _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "You changed", "Take a look!"));
        }

        public void Inactivate(Customer customer)
        {
            if (customer.IsValid())
                return;

            customer.SetInactive();
            _customerRepository.Update(customer);
            _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "See you soon", "Take a look!"));
        }

        public void Remove(Customer customer)
        {
            _customerRepository.Remove(customer.Id);
            _mediator.Publish(new CustomerEmailNotification("admin@me.com", customer.Email, "Good bye", "Hasta la vista!"));
        }

        public void Dispose()
        {
            _customerRepository.Dispose();
        }
    }
}