using System.Linq;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Event;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Helpers;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Utilities;

namespace Tracker.Core.Event
{
	public sealed class EventFacade : IEventFacade
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IClientRepository _clients;
		private readonly IPartitionConverter _converter;
		private readonly IEventRepository _events;
		private readonly IIdentityService _identity;
		private readonly ISenderRepository _senders;
		private readonly ISerializer _serializer;

		public EventFacade(
			IEventRepository events,
			ISerializer serializer,
			IPartitionConverter converter,
			IIdentityService identity,
			IApplicationRepository applications,
			IClientRepository clients,
			IAwbRepository awbs,
			ISenderRepository senders)
		{
			_events = events;
			_serializer = serializer;
			_converter = converter;
			_identity = identity;
			_applications = applications;
			_clients = clients;
			_awbs = awbs;
			_senders = senders;
		}

		public void Add<T>(long entityId, EventType type, EventState state, T obj)
		{
			var data = _serializer.Serialize(obj);

			AddImpl(entityId, type, state, data);
		}

		public void Add(long entityId, EventType type, EventState state)
		{
			AddImpl(entityId, type, state, null);
		}

		private void AddImpl(long entityId, EventType type, EventState state, byte[] data)
		{
			var currentUserId = TryGetUserIdByApplication(entityId, type)
			                    ?? TryGetUserIdByAwb(entityId, type)
			                    ?? TryGetUserIdByClient(entityId, type)
			                    ?? GetCurrentUserId();

			var partitionId = _converter.GetKey(entityId);

			var entityData = new EventDataForEntity
			{
				EntityId = entityId,
				Data = data
			};

			var bytes = _serializer.Serialize(entityData);

			_events.Add(partitionId, currentUserId, type, state, bytes);
		}

		private long? TryGetUserIdByClient(long entityId, EventType type)
		{
			if(!EventHelper.ClientEventTypes.Contains(type)) return null;

			var senderIdInEntity = _clients.Get(entityId).DefaultSenderId;

			return senderIdInEntity.HasValue
				? _senders.GetUserId(senderIdInEntity.Value)
				: (long?)null;
		}

		private long? GetCurrentUserId()
		{
			return _identity.IsAuthenticated ? _identity.Id : (long?)null;
		}

		private long? TryGetUserIdByAwb(long entityId, EventType type)
		{
			return EventHelper.AwbEventTypes.Contains(type)
				? _awbs.Get(entityId).Select(x => x.SenderUserId).FirstOrDefault()
				: null;
		}

		private long? TryGetUserIdByApplication(long entityId, EventType type)
		{
			if(!EventHelper.ApplicationEventTypes.Contains(type)) return null;

			var senderIdInEntity = _applications.Get(entityId).SenderId;

			return senderIdInEntity.HasValue
				? _senders.GetUserId(senderIdInEntity.Value)
				: (long?)null;
		}
	}
}