﻿using Tracker.DataAccess.BlackBox.Tests.Properties;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.DbContext;
using Tracker.DataAccess.Repositories;
using Tracker.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Tracker.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class EmailMessageRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private EmailMessageRepository _messages;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_messages = new EmailMessageRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void Test_GetNext()
		{
			var partitionId = _fixture.Create<int>();

			var data = Add(partitionId);

			_messages.GetNext(EmailMessageState.New, partitionId + 1).Should().BeNull();

			_messages.GetNext(EmailMessageState.Failed, partitionId).Should().BeNull();

			var next = _messages.GetNext(EmailMessageState.New, partitionId);

			next.ShouldBeEquivalentTo(data, options => options.Excluding(x => x.Id));
		}

		private EmailMessageData Add(int partitionId)
		{
			var data = _fixture.Build<EmailMessageData>()
				.With(x => x.EmailSenderUserId, TestConstants.TestAdminUserId)
				.With(x => x.To, EmailMessageData.Join(_fixture.CreateMany<string>()))
				.With(x => x.CopyTo, EmailMessageData.Join(_fixture.CreateMany<string>()))
				.Without(x => x.Id)
				.Create();

			_messages.Add(
				partitionId,
				data.EmailSenderUserId,
				data.From,
				EmailMessageData.Split(data.To),
				EmailMessageData.Split(data.CopyTo),
				data.Subject,
				data.Body,
				data.IsBodyHtml,
				data.Files);

			return data;
		}

		[TestMethod]
		public void Test_SetState()
		{
			var partitionId = _fixture.Create<int>();

			Add(partitionId);

			var data = _messages.GetNext(EmailMessageState.New, partitionId);

			_messages.SetState(data.Id, EmailMessageState.Sent);

			_messages.GetNext(EmailMessageState.New, partitionId).Should().BeNull();

			var next = _messages.GetNext(EmailMessageState.Sent, partitionId);

			next.ShouldBeEquivalentTo(data);
		}
	}
}