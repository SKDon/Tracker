using Tracker.ViewModels.User;

namespace Tracker.Services.Abstract
{
    public interface IAuthenticationService
    {
        bool Authenticate(SignIdModel user);
	    void AuthenticateForce(long usreId, bool createPersistentCookie);
	    void SignOut();
    }
}