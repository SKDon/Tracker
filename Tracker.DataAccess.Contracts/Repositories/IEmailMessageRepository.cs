using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Repositories
{
	public interface IEmailMessageRepository
	{
		void Add(
			int partitionId,
			long? emailSenderUserId,
			string @from,
			string[] to,
			string[] copyTo,
			string subject,
			string body,
			bool isBodyHtml,
			byte[] files);

		EmailMessageData GetNext(EmailMessageState state, int partitionId);
		void SetState(long id, EmailMessageState state);
	}
}