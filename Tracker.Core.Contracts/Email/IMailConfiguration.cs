using System.Net.Configuration;

namespace Tracker.Core.Contracts.Email
{
	public interface IMailConfiguration
	{
		SmtpSection GetConfiguration(long? userId);
	}
}