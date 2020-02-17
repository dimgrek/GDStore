using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;

namespace GDStore.Alterations.Services.Services
{
    public interface IAlterationService
    {
        Task AddAlteration(AddAlterationCommand command);
        Task MakeAlteration(MakeAlterationCommand command);
    }
}
