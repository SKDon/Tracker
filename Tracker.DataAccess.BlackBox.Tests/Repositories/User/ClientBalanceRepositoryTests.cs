using Tracker.DataAccess.BlackBox.Tests.Properties;
using Tracker.DataAccess.DbContext;
using Tracker.DataAccess.Repositories.User;
using Tracker.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Tracker.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class ClientBalanceRepositoryTests
	{
		private ClientBalanceRepository _repository;
		private DbTestContext _context;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_repository = new ClientBalanceRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		
		public void Test_SumBalance()
		{
			var first = _repository.SumBalance();
			var balance = _fixture.Create<decimal>();

			var old = _repository.GetBalance(TestConstants.TestClientId1);

			_repository.SetBalance(TestConstants.TestClientId1, balance);

			_repository.SumBalance().ShouldBeEquivalentTo(first + balance - old);
		}
	}
}
