using System.Threading.Tasks;
using GDStore.Alterations.Messages.Commands;

namespace GDStore.Alterations.Services
{
    public interface IAlterationService
    {
        Task AddAlteration(AddAlterationCommand command);
    }
}
