using Tracker.Core.Contracts.Common;

namespace Tracker.Core.Common
{
	public sealed class Holder<T> : IHolder<T>
	{
		private T _value;

		public Holder(T value)
		{
			_value = value;
		}

		public T Get()
		{
			return _value;
		}

		public void Set(T value)
		{
			_value = value;
		}
	}
}