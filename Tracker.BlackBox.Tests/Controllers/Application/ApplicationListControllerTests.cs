using Tracker.BlackBox.Tests.Properties;
using Tracker.Controllers.Application;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.TestHelpers;
using Tracker.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Tracker.BlackBox.Tests.Controllers.Application
{
	[TestClass]
	public class ApplicationListControllerTests
	{
		[TestMethod]
		public void Test_List_ByAdmin()
		{
			using(var context = new CompositionHelper(
				Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString))
			{
				var controller = context.Kernel.Get<ApplicationListController>();

				var result = controller.List(10, 0, null);

				var data = (ApplicationListCollection)result.Data;

				data.Total.ShouldBeEquivalentTo(20);

				data.Data.Should().HaveCount(10);
			}
		}

		[TestMethod]
		public void Test_List_ByCarrier()
		{
			using(var context = new CompositionHelper(
				Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString, RoleType.Carrier))
			{
				var controller = context.Kernel.Get<ApplicationListController>();

				var result = controller.List(5, 0, null);

				var data = (ApplicationListCollection)result.Data;

				data.Total.ShouldBeEquivalentTo(10);

				data.Data.Should().HaveCount(5);
			}
		}

		[TestMethod]
		public void Test_List_ByForwarder()
		{
			using(var context = new CompositionHelper(
				Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString, RoleType.Forwarder))
			{
				var controller = context.Kernel.Get<ApplicationListController>();

				var result = controller.List(5, 0, null);

				var data = (ApplicationListCollection)result.Data;

				data.Total.ShouldBeEquivalentTo(6);

				data.Data.Should().HaveCount(5);
			}
		}
	}
}