﻿using System.Linq;
using Tracker.DataAccess.BlackBox.Tests.Properties;
using Tracker.DataAccess.Repositories.User;
using Tracker.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracker.DataAccess.BlackBox.Tests.Repositories.User
{
	[TestClass]
	public class BrokerRepositoryTests
	{
		private DbTestContext _context;
		private BrokerRepository _repository;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);

			_repository = new BrokerRepository(_context.Connection);
		}

		[TestMethod]
		public void Test_BrokerRepository_Get()
		{
			var all = _repository.GetAll();

			var broker = _repository.Get(TestConstants.TestBrokerId);

			var data = all.First(x => x.Id == TestConstants.TestBrokerId);

			data.ShouldBeEquivalentTo(broker);

			var byUserId = _repository.GetByUserId(broker.UserId);

			data.ShouldBeEquivalentTo(byUserId);
		}
	}
}