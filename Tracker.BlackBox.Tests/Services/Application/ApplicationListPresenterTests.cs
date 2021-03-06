﻿using System.Data.SqlClient;
using System.Linq;
using Tracker.BlackBox.Tests.Properties;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Services.Application;
using Tracker.TestHelpers;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Tracker.BlackBox.Tests.Services.Application
{
	[TestClass]
	public class ApplicationListPresenterTests
	{
		private CompositionHelper _context;
		private ApplicationListPresenter _presenter;


		[TestCleanup]
		public void TestCleanup()
		{
			_context.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString,
				RoleType.Forwarder);
			_context.Kernel.Bind<ApplicationListPresenter>().ToSelf();

			_presenter = _context.Kernel.Get<ApplicationListPresenter>();
		}

		[TestMethod]
		public void Test_FilterByCargoReceivedDaysShow()
		{
			long applicationId;
			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				applicationId = connection.Query<long>(
					"select TOP 1 [Id] from [dbo].[Application] where [StateId] <> @StateId and [ForwarderId] = @ForwarderId",
					new { StateId = TestConstants.CargoReceivedStateId, ForwarderId = TestConstants.TestForwarderId1 }).First();
			}

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				connection.Execute(
					"update [dbo].[Application] set [StateId] = @StateId, [StateChangeTimestamp] = GETUTCDATE() where [Id] = @Id",
					new { StateId = TestConstants.CargoReceivedStateId, Id = applicationId });
			}

			Assert.IsTrue(_presenter.List(TwoLetterISOLanguageName.English, forwarderId: TestConstants.TestForwarderId1)
				.Data.Any(x => x.Id == applicationId && x.StateId == TestConstants.CargoReceivedStateId));

			using(var connection = new SqlConnection(Settings.Default.MainConnectionString))
			{
				connection.Execute("update [dbo].[Application] set [StateChangeTimestamp] = GETUTCDATE() - 20 where [Id] = @Id",
					new { Id = applicationId });
			}

			Assert.IsFalse(_presenter.List(TwoLetterISOLanguageName.English, forwarderId: TestConstants.TestForwarderId1)
				.Data.Any(x => x.Id == applicationId));
		}
	}
}