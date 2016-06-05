using System.Linq;
using Tracker.DataAccess.BlackBox.Tests.Properties;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.DbContext;
using Tracker.DataAccess.Repositories;
using Tracker.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Tracker.DataAccess.BlackBox.Tests.Repositories
{
	[TestClass]
	public class TransitRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private TransitRepository _transitRepository;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			_transitRepository = new TransitRepository(new SqlProcedureExecutor(Settings.Default.MainConnectionString));
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestMethod]
		public void Test_TransitRepository_Add_Get()
		{
			var transit = CreateTestTransit();

			var actual = _transitRepository.Get(transit.Id).First();

			transit.ShouldBeEquivalentTo(actual);
		}

		[TestMethod]
		public void Test_TransitRepository_Update()
		{
			var oldData = CreateTestTransit();

			var newData = _fixture.Create<TransitData>();
			newData.CarrierId = TestConstants.TestCarrierId2;
			newData.Id = oldData.Id;
			newData.CityId = TestConstants.TestCityId2;

			_transitRepository.Update(newData);
			
			var actual = _transitRepository.Get(newData.Id).First();

			oldData.Should().NotBeSameAs(actual);
			newData.ShouldBeEquivalentTo(actual);
		}

		private TransitData CreateTestTransit()
		{
			var data = _fixture.Create<TransitData>();
			data.Id = 0;
			data.CarrierId = TestConstants.TestCarrierId1;
			data.CityId = TestConstants.TestCityId1;

			data.Id = _transitRepository.Add(data);

			return data;
		}
	}
}