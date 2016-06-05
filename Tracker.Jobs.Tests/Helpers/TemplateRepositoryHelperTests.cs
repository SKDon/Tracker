using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Jobs.Helpers;
using Tracker.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracker.Jobs.Tests.Helpers
{
	[TestClass]
	public class TemplateRepositoryHelperTests
	{
		private MockContainer _container;
		private TemplateRepositoryHelper _helper;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();
			_helper = _container.Create<TemplateRepositoryHelper>();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_False()
		{
			var templateData = _container.Create<EventTemplateData>();
			templateData.EnableEmailSend = false;

			_container.TemplateRepository.Setup(x => x.GetByEventType(EventType.ApplicationSetState)).Returns(templateData);

			var templateId = _helper.GetTemplateId(EventType.ApplicationSetState);

			templateId.Should().NotHaveValue();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_True()
		{
			var templateData = _container.Create<EventTemplateData>();
			templateData.EnableEmailSend = true;

			_container.TemplateRepository.Setup(x => x.GetByEventType(EventType.ApplicationSetState)).Returns(templateData);

			var templateId = _helper.GetTemplateId(EventType.ApplicationSetState);

			templateId.ShouldBeEquivalentTo(templateData.EmailTemplateId);
		}
	}
}