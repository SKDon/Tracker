using Tracker.DataAccess.BlackBox.Tests.Properties;
using Tracker.DataAccess.Contracts.Contracts;
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
	public class TemplateRepositoryTests
	{
		private DbTestContext _context;
		private Fixture _fixture;
		private TemplateRepository _templates;

		[TestCleanup]
		public void TestCleanup()
		{
			_context.Cleanup();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new DbTestContext(Settings.Default.MainConnectionString);
			_fixture = new Fixture();

			var executor = new SqlProcedureExecutor(Settings.Default.MainConnectionString);
			_templates = new TemplateRepository(executor);
		}

		[TestMethod]
		public void Test_Set_New()
		{
			const EventType newEventType = (EventType)(-1);

			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForEvent(newEventType, TwoLetterISOLanguageName.English, false, localizationData);

			var data = _templates.GetByEventType(newEventType);

			data.EnableEmailSend.Should().BeFalse();

			var localization = _templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English);

			localization.ShouldBeEquivalentTo(localizationData);
		}

		[TestMethod]
		public void Test_Set_WithOtherLanguage()
		{
			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForEvent(EventType.ApplicationCreated, TwoLetterISOLanguageName.English, false, localizationData);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForEvent(EventType.ApplicationCreated, TwoLetterISOLanguageName.Italian, true, newLocalizationData);

			var data = _templates.GetByEventType(EventType.ApplicationCreated);

			data.EnableEmailSend.Should().BeTrue();

			_templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English)
				.ShouldBeEquivalentTo(localizationData);

			_templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.Italian)
				.ShouldBeEquivalentTo(newLocalizationData);
		}

		[TestMethod]
		public void Test_Set_WithSameLanguage()
		{
			var localizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForEvent(EventType.ApplicationCreated, TwoLetterISOLanguageName.English, false, localizationData);

			var newLocalizationData = _fixture.Create<EmailTemplateLocalizationData>();

			_templates.SetForEvent(EventType.ApplicationCreated, TwoLetterISOLanguageName.English, true, newLocalizationData);

			var data = _templates.GetByEventType(EventType.ApplicationCreated);

			data.EnableEmailSend.Should().BeTrue();

			var localization = _templates.GetLocalization(data.EmailTemplateId, TwoLetterISOLanguageName.English);

			localization.ShouldBeEquivalentTo(newLocalizationData);
		}
	}
}