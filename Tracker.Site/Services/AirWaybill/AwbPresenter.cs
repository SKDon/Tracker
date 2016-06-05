using System.Collections.ObjectModel;
using System.Linq;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Exceptions;
using Tracker.Core.Helpers;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Contracts.Awb;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Services.Abstract;
using Tracker.Utilities.Localization;
using Tracker.ViewModels.AirWaybill;
using Tracker.ViewModels.Application;

namespace Tracker.Services.AirWaybill
{
	internal sealed class AwbPresenter : IAwbPresenter
	{
		private readonly IAwbRepository _awbs;
		private readonly IBrokerRepository _brokers;
		private readonly IAwbFileRepository _files;
		private readonly IStateRepository _states;

		public AwbPresenter(
			IAwbRepository awbs,
			IAwbFileRepository files,
			IBrokerRepository brokers,
			IStateRepository states)
		{
			_awbs = awbs;
			_files = files;
			_brokers = brokers;
			_states = states;
		}

		public ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId, long? senderUserId, string language)
		{
			var data = _awbs.GetRange(take, skip, brokerId, senderUserId);
			var ids = data.Select(x => x.Id).ToArray();

			var aggregates = _awbs.GetAggregate(ids).ToDictionary(x => x.AirWaybillId, x => x);

			var states = _states.Get(language);
			var currentCulture = CultureProvider.GetCultureInfo();
			var awbFiles = GetFileInfo(ids, AwbFileType.AWB);
			var packingFiles = GetFileInfo(ids, AwbFileType.Packing);
			var drawFiles = GetFileInfo(ids, AwbFileType.Draw);
			var gtdFiles = GetFileInfo(ids, AwbFileType.GTD);
			var gtdAddFiles = GetFileInfo(ids, AwbFileType.GTDAdditional);
			var invoiceFiles = GetFileInfo(ids, AwbFileType.Invoice);
			var otherFiles = GetFileInfo(ids, AwbFileType.Other);

			var items = data.Select(x => new AirWaybillListItem
			{
				Id = x.Id,
				PackingFiles = GetFileInfo(packingFiles, x.Id),
				InvoiceFiles = GetFileInfo(invoiceFiles, x.Id),
				State = new ApplicationStateModel
				{
					StateName = states[x.StateId].LocalizedName,
					StateId = x.StateId
				},
				AWBFiles = GetFileInfo(awbFiles, x.Id),
				OtherFiles = GetFileInfo(otherFiles, x.Id),
				ArrivalAirport = x.ArrivalAirport,
				Bill = x.Bill,
				CreationTimestampLocalString = LocalizationHelper.GetDate(x.CreationTimestamp, currentCulture),
				DateOfArrivalLocalString = LocalizationHelper.GetDate(x.DateOfArrival, currentCulture),
				DateOfDepartureLocalString = LocalizationHelper.GetDate(x.DateOfDeparture, currentCulture),
				StateChangeTimestampLocalString = LocalizationHelper.GetDate(x.StateChangeTimestamp, currentCulture),
				DepartureAirport = x.DepartureAirport,
				GTD = x.GTD,
				GTDAdditionalFiles = GetFileInfo(gtdAddFiles, x.Id),
				GTDFiles = GetFileInfo(gtdFiles, x.Id),
				DrawFiles = GetFileInfo(drawFiles, x.Id),
				TotalCount = aggregates[x.Id].TotalCount,
				TotalWeight = aggregates[x.Id].TotalWeight,
				AdditionalCost = x.AdditionalCost,
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight,
				BrokerCost = x.BrokerCost,
				CustomCost = x.CustomCost,
				FlightCost = x.FlightCost,
				IsActive = x.IsActive
			}).ToArray();

			var total = _awbs.Count(brokerId, senderUserId);

			return new ListCollection<AirWaybillListItem> { Data = items, Total = total };
		}

		public AwbAdminModel Get(long id)
		{
			var data = _awbs.Get(id).FirstOrDefault();

			if(data == null) throw new EntityNotFoundException("Refarence: " + id);

			return AwbMapper.GetAdminModel(data);
		}

		public AwbSenderModel GetSenderAwbModel(long id)
		{
			var data = _awbs.Get(id).First();

			return AwbMapper.GetSenderModel(data);
		}

		public AirWaybillData GetData(long id)
		{
			return _awbs.Get(id).First();
		}

		public AirWaybillAggregate GetAggregate(long id)
		{
			return _awbs.GetAggregate(new[] { id }).First();
		}

		public BrokerData GetBroker(long brokerId)
		{
			return _brokers.Get(brokerId);
		}

		private ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> GetFileInfo(long[] ids, AwbFileType type)
		{
			return _files.GetInfo(ids, type);
		}

		private static FileInfo[] GetFileInfo(ReadOnlyDictionary<long, ReadOnlyCollection<FileInfo>> names, long awbId)
		{
			return names != null && names.ContainsKey(awbId) ? names[awbId].ToArray() : null;
		}
	}
}