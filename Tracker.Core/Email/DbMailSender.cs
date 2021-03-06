﻿using Tracker.Core.Contracts.Email;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.Utilities;

namespace Tracker.Core.Email
{
	public sealed class DbMailSender : IMailSender
	{
		private readonly int _partitionId;
		private readonly IEmailMessageRepository _repository;
		private readonly ISerializer _serializer;

		public DbMailSender(int partitionId, IEmailMessageRepository repository, ISerializer serializer)
		{
			_partitionId = partitionId;
			_repository = repository;
			_serializer = serializer;
		}

		public void Send(params EmailMessage[] messages)
		{
			foreach(var message in messages)
			{
				var files = _serializer.Serialize(message.Files);

				_repository.Add(
					_partitionId,
					message.EmailSenderUserId,
					message.From,
					message.To,
					message.CopyTo,
					message.Subject,
					message.Body,
					message.IsBodyHtml,
					files);
			}
		}
	}
}