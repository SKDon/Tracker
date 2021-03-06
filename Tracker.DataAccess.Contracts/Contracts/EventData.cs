﻿using Tracker.DataAccess.Contracts.Enums;

namespace Tracker.DataAccess.Contracts.Contracts
{
	public sealed class EventData
	{
		public long Id { get; set; }
		public long? UserId { get; set; }
		public EventState State { get; set; }
		public byte[] Data { get; set; }
	}
}