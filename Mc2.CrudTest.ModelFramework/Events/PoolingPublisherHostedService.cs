using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.ModelFramework.Configuration;
using Microsoft.Extensions.Hosting;
 

namespace Mc2.CrudTest.ModelFramework.Events
{
    public class PoolingPublisherHostedService : IHostedService
    {
        private readonly Mc2CrudTestFrameworkConfiguration _configuration;
        private readonly IOutBoxEventItemRepository _outBoxEventItemRepository;
       
        private Timer _timer;
        private bool _started = false;

        public PoolingPublisherHostedService(Mc2CrudTestFrameworkConfiguration configuration,
            IOutBoxEventItemRepository outBoxEventItemRepository)
        {
            _configuration = configuration;
            _outBoxEventItemRepository = outBoxEventItemRepository;
          
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendOutBoxItems, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(_configuration.PoolingPublisher.SendOutBoxInterval));
            return Task.CompletedTask;
        }

        private void SendOutBoxItems(object state)
        {
            //            if (!_started)
            //            {
            //                var startTime = System.Diagnostics.Process.GetCurrentProcess().StartTime;
            //                var now = DateTime.Now;
            //                var runningTime = now - startTime;
            //
            //                if (runningTime.Seconds < 120)
            //                    return;
            //                else
            //                    _started = true;
            //            }

            _timer.Change(Timeout.Infinite, 0);
            var outboxItems =
                _outBoxEventItemRepository.GetOutBoxEventItemsForPublish(
                    _configuration.PoolingPublisher.SendOutBoxCount);

            foreach (var item in outboxItems)
            {
                var parceledMessage = new Parcel
                {
                    CorrelationId = item.AggregateId,
                    MessageBody = item.EventPayload,
                    MessageId = item.EventId.ToString(),
                    MessageName = item.EventName,
                    Route = $"{_configuration.ServiceId}.{item.EventName}",
                    Headers = new Dictionary<string, object>
                    {
                        
                        ["HappenedOn"] = item.HappenedOn.ToString(CultureInfo.CurrentCulture),
                        ["AggregateName"] = item.AggregateName,
                        ["AggregateTypeName"] = item.AggregateTypeName,
                        ["EventTypeName"] = item.EventTypeName,
                       
                    }
                };
                //_messageBus.SendEvent(parceledMessage);
                item.IsProcessed = true;
            }

            _outBoxEventItemRepository.MarkAsRead(outboxItems);
            _timer.Change(0, _configuration.PoolingPublisher.SendOutBoxInterval);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}