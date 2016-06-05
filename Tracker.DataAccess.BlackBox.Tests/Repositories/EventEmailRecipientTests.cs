using System.Linq;
using Tracker.DataAccess.BlackBox.Tests.Properties;
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
	public class EventEmailRecipientTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private EventEmailRecipient _recipients;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			var executor = new SqlProcedureExecutor(Settings.Default.MainConnectionString);
			_recipients = new EventEmailRecipient(executor);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void Test_Set_SetForEvent()
		{
			const EventType eventType = EventType.ApplicationCreated;

			var recipients = _fixture.CreateMany<RoleType>().ToArray();

			_recipients.Set(eventType, recipients);

			_recipients.GetRecipientRoles(eventType).ShouldAllBeEquivalentTo(recipients);
		}
	}
}