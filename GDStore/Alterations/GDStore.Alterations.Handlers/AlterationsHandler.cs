using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;
using MassTransit;

namespace GDStore.Alterations.Handlers
{
    public class AlterationsHandler : IConsumer<AddAlterationCommand>
    {
        public AlterationsHandler()
        {
            
        }
        public Task Consume(ConsumeContext<AddAlterationCommand> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
