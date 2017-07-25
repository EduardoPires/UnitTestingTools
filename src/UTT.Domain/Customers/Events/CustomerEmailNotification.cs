using MediatR;
using UTT.Domain.Core;

namespace UTT.Domain.Customers.Events
{
    public class CustomerEmailNotification : INotification
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Message { get; private set; }

        public CustomerEmailNotification(string @from, string to, string subject, string message)
        {
            From = @from;
            To = to;
            Subject = subject;
            Message = message;
        }
    }
}