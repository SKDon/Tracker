using Tracker.DataAccess.Contracts.Contracts.Application;
using Tracker.ViewModels.Application;

namespace Tracker.Services.Abstract
{
    public interface IApplicationListItemMapper
    {
        ApplicationListItem[] Map(ApplicationData[] data, string language);
    }
}
