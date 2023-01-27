using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptiBid.Microservices.Messaging.Receving.Models;

namespace OptiBid.Microservices.Messaging.Receving.MessageQueue
{
    public interface IMessageQueue
    {
        ValueTask WriteAsync(NotificationMessage message,
            CancellationToken cancellationToken = default);

        IAsyncEnumerable<NotificationMessage> ReadAll(CancellationToken cancellationToken = default);

        IObservable<NotificationMessage> GetBaseEntities(CancellationToken cancellationToken = default);

    }
}
