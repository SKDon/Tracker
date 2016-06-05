using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Contracts
{
	public sealed class Setting
	{
		public SettingType Type { get; set; }
		public byte[] RowVersion { get; set; }
		public byte[] Data { get; set; }
	}
}