using System.Data.Linq;
using System.Linq;
using Tracker.BlackBox.Tests.Properties;
using Tracker.Controllers.Awb;
using Tracker.DataAccess.DbContext;
using Tracker.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Tracker.BlackBox.Tests.Controllers
{
	[TestClass]
	public class AirWaybillControllerTests
	{
		private CompositionHelper _composition;
		private AirWaybillController _controller;
		private TrackerDataContext _db;

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
			_db.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_db = new TrackerDataContext(Settings.Default.MainConnectionString);

			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_controller = _composition.Kernel.Get<AirWaybillController>();
		}

		[TestMethod]
		public void Test_SetAirWaybill()
		{
			var application = _db.Applications.First(x => !x.AirWaybillId.HasValue);
			var airWaybill = _db.AirWaybills.First();

			_controller.SetAirWaybill(application.Id, airWaybill.Id);

			_db.Refresh(RefreshMode.OverwriteCurrentValues, application);

			Assert.AreEqual(airWaybill.Id, application.AirWaybillId);

			application.AirWaybillId = null;

			_db.SubmitChanges();
		}
	}
}