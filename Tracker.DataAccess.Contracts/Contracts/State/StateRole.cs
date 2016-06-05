using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Contracts.State
{
	public sealed class StateRole
	{
		public long StateId { get; set; }
		public RoleType Role { get; set; }
	}
}