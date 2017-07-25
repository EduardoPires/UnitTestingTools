using System;
using UTT.Domain.Core;
using UTT.Domain.Customers.Validations;

namespace UTT.Domain.Customers
{
    public class Customer : Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string Email { get; private set; }
        public bool Active { get; private set; }

        protected Customer() { }

        public Customer(Guid id, string name, string lastName, DateTime birthDate, string email, bool active, DateTime registerDate)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            Active = active;
            RegisterDate = registerDate;
        }

        public string FullName()
        {
            return Name + " " + LastName;
        }

        public bool IsSpecial()
        {
            return RegisterDate < DateTime.Now.AddYears(-3) && Active;
        }

        public void SetInactive()
        {
            Active = false;
        }

        public override bool IsValid()
        {
            ValidationResult = new CustomerSelfValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}