using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Messaging.Receving.Utilities
{
    public static class ChannelExtentions
    {

    public static IObservable<T> ToObservable<T>(this ChannelReader<T> reader,
        CancellationToken cancellationToken = default)
    {
        var obs = Observable.Create(async (IObserver<T> o) =>
        {
            try
            {
                while (await reader.WaitToReadAsync(cancellationToken))
                while (reader.TryRead(out var item))
                    o.OnNext(item);
                o.OnCompleted();
            }
            catch (OperationCanceledException)
            {
                o.OnCompleted();
            }
            catch (Exception e)
            {
                o.OnError(e);
            }
        });
        return obs.Publish().RefCount();
    }

    }
}
