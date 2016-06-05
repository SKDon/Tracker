using Tracker.DataAccess.Contracts.Contracts.User;

namespace Tracker.Core.Contracts.Client
{
    public interface IClientPermissions
    {
        bool HaveAccessToClient(ClientData data);
    }
}