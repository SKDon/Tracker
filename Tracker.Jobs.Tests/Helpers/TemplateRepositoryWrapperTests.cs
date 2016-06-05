using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.Jobs.Helpers;
using Tracker.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracker.Jobs.Tests.Helpers
{
	[TestClass]
	public class TemplateRepositoryWrapperTests
	{
		private MockContainer _container;
		private TemplateRepositoryHelper _templateRepositoryHelper;

		[TestInitialize]
		public void TestInitialize()
		{
			_container = new MockContainer();

			_templateRepositoryHelper = _container.Create<TemplateRepositoryHelper>();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_False()
		{
			var eventTemplate = _container.Create<EventTemplateData>();
			eventTemplate.EnableEmailSend = false;
			var eventType = _container.Create<EventType>();

			_container.TemplateRepository.Setup(x => x.GetByEventType(eventType)).Returns(eventTemplate);

			var template = _templateRepositoryHelper.GetTemplateId(eventType);

			template.Should().NotHaveValue();
		}

		[TestMethod]
		public void Test_GetByEventType_EnableEmailSend_True()
		{
			var eventTemplate = _container.Create<EventTemplateData>();
			eventTemplate.EnableEmailSend = true;
			var eventType = _container.Create<EventType>();

			_container.TemplateRepository.Setup(x => x.GetByEventType(eventType)).Returns(eventTemplate);

			var template = _templateRepositoryHelper.GetTemplateId(eventType);

			template.ShouldBeEquivalentTo(eventTemplate.EmailTemplateId);
		}
	}
}