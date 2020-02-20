using System;
using System.Threading.Tasks;
using log4net;
using MassTransit;

namespace GDStore.BLL.Services.Observers
{
    public class MessagesConsumerObserver : IConsumeObserver
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MessagesConsumerObserver));

        public async Task PreConsume<T>(ConsumeContext<T> context) where T : class
        {
            log.Info($"{nameof(context.Message)} handler called");
            await context.CompleteTask; }

        public async Task PostConsume<T>(ConsumeContext<T> context) where T : class
        {
            await context.CompleteTask;
        }

        public async Task ConsumeFault<T>(ConsumeContext<T> context, Exception exception) where T : class
        {
            await context.CompleteTask;
        }
    }
}