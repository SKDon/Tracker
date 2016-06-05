using Tracker.Services.Users;
using Tracker.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracker.Tests.Services.Users
{
	[TestClass]
	public class SenderServiceTests
	{
		private MockContainer _context;
		private SenderService _service;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();
			_service = _context.Create<SenderService>();
		}
	}
}