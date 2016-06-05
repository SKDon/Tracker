using Tracker.DataAccess.Contracts.Helpers;

namespace Tracker.Core.Contracts.Common
{
	public sealed class ListCollection<T>
	{
		public T[] Data { get; set; }
		public long Total { get; set; }
		public Order[] Groups { get; set; }
	}
}