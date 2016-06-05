using Tracker.DataAccess.Contracts.Contracts;

namespace Tracker.Core.Contracts.Email
{
	public interface IMailSender
	{
		void Send(params EmailMessage[] messages);
	}
}
