using Tracker.DataAccess.Contracts.Enums;
using Tracker.ViewModels.User;

namespace Tracker.Services.Abstract
{
	public interface IUserService
	{
		UserListItem[] List(RoleType role);
		UserModel Get(RoleType role, long id);
		void Update(UserModel model);
		void Add(UserModel model);
	}
}