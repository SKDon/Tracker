using Tracker.DataAccess.Contracts.Contracts.Application;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.Jobs.Application.Abstract
{
	public interface ITextBuilder
	{
		string GetText(string template, string language, EventType type, ApplicationData application, byte[] data);
	}
}