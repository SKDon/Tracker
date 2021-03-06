﻿using System;
using System.Linq;
using Tracker.Core.AirWaybill;
using Tracker.Core.Contracts.Exceptions;
using Tracker.DataAccess.Contracts.Contracts.Application;
using Tracker.DataAccess.Contracts.Contracts.Awb;
using Tracker.Services.AirWaybill;
using Tracker.TestHelpers;
using Tracker.ViewModels.AirWaybill;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tracker.Tests.Services.AirWaybill
{
	[TestClass]
	public class AwbManagerTests
	{
		private MockContainer _context;
		private AwbManager _manager;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = new MockContainer();

			_manager = _context.Create<AwbManager>();
		}

		[TestMethod]
		public void Test_AwbManager_Create()
		{
			var airWaybillId = _context.Create<long>();
			var applicationId = _context.Create<long>();
			var cargoIsFlewStateId = _context.Create<long>();
			var model = _context.Create<AwbAdminModel>();
			model.GTD = null;

			_context.StateConfig.Setup(x => x.CargoIsFlewStateId).Returns(cargoIsFlewStateId);
			_context.ApplicationAwbManager.Setup(x => x.SetAwb(applicationId, airWaybillId));
			_context.AwbRepository.Setup(x => x.Add(It.IsAny<AirWaybillEditData>(), cargoIsFlewStateId, TestConstants.TestAdminUserId))
				.Returns(() => airWaybillId);

			var airWaybillData = AwbMapper.GetData(model);

			_manager.Create(applicationId, airWaybillData, TestConstants.TestAdminUserId);

			_context.ApplicationAwbManager.Verify(x => x.SetAwb(applicationId, airWaybillId), Times.Once());
			_context.AwbRepository.Verify(x => x.Add(It.Is<AirWaybillEditData>(
				data => data.ArrivalAirport == model.ArrivalAirport
				        && data.Bill == model.Bill
				        && data.DepartureAirport == model.DepartureAirport
				        && data.BrokerId == model.BrokerId
				        && data.DateOfArrival == DateTimeOffset.Parse(model.DateOfArrivalLocalString)
				        && data.DateOfDeparture == DateTimeOffset.Parse(model.DateOfDepartureLocalString)
						&& data.GTD == null), cargoIsFlewStateId, TestConstants.TestAdminUserId),
				Times.Once());

			_context.StateConfig.Verify(x => x.CargoIsFlewStateId, Times.Once());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidLogicException))]
		public void Test_AwbManager_Create_WithGtd()
		{
			var data = _context.Create<AirWaybillData>();

			_manager.Create(It.IsAny<long>(), data, TestConstants.TestAdminUserId);
		}

		[TestMethod]
		public void Test_AwbManager_Delete()
		{
			var id = _context.Create<long>();
			var applications = _context.CreateMany<ApplicationData>().ToArray();
			_context.ApplicationRepository.Setup(x => x.GetByAirWaybill(id)).Returns(applications);
			_context.ApplicationEditor.Setup(x => x.SetAirWaybill(It.IsAny<long>(), null));
			_context.AwbRepository.Setup(x => x.Delete(id));

			_manager.Delete(id);

			_context.ApplicationRepository.Verify(x => x.GetByAirWaybill(id), Times.Once());
			_context.ApplicationEditor.Verify(
				x => x.SetAirWaybill(It.Is<long>(i => applications.Any(a => a.Id == i)), null),
				Times.Exactly(applications.Length));
			_context.AwbRepository.Verify(x => x.Delete(id), Times.Once());
		}

		[TestMethod]
		public void Test_AwbManager_Map_AirWaybillEditModel()
		{
			var model = _context.Create<AwbAdminModel>();
			var data = AwbMapper.GetData(model);

			model.ShouldBeEquivalentTo(data,
				options => options.ExcludingMissingProperties()
					.Excluding(x => x.GTD));

			data.DateOfArrival.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfArrivalLocalString));
			data.DateOfDeparture.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfDepartureLocalString));
			data.GTD.Should().BeNull();
		}

		[TestMethod]
		public void Test_AwbManager_Map_SenderAwbModel()
		{
			var model = _context.Create<AwbSenderModel>();
			var data = AwbMapper.GetData(model, TestConstants.TestSenderUserId);

			model.ShouldBeEquivalentTo(data, options => options.ExcludingMissingProperties());

			data.DateOfArrival.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfArrivalLocalString));
			data.DateOfDeparture.ShouldBeEquivalentTo(DateTimeOffset.Parse(model.DateOfDepartureLocalString));
			data.GTD.Should().BeNull();
			data.SenderUserId.ShouldBeEquivalentTo(TestConstants.TestSenderUserId);
		}
	}
}