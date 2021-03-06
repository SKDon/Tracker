﻿using System.Configuration;
using Tracker.Core.Contracts.State;
using Tracker.Utilities;

namespace Tracker.Core.State
{
	public sealed class StateConfig : IStateConfig
	{
		public long CargoIsCustomsClearedStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-CargoIsCustomsCleared"].ToLong(); }
		}

		public long CargoIsFlewStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-CargoIsFlew"].ToLong(); }
		}

		public long CargoInStockStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-CargoInStock"].ToLong(); }
		}

		public long CargoAtCustomsStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-CargoAtCustoms"].ToLong(); }
		}

		public long CargoReceivedStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-CargoReceived"].ToLong(); }
		}

		public int CargoReceivedDaysToShow
		{
			get { return ConfigurationManager.AppSettings["CargoReceivedDaysToShow"].ToInt(); }
		}

		public long CargoOnTransitStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-CargoOnTransit"].ToLong(); }
		}

		//public long CreatedAWBStateId
		//{
		//	get { return ConfigurationManager.AppSettings["StateId-CreatedAWB"].ToLong(); }
		//}

		public long DefaultStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-Default"].ToLong(); }
		}

		public long[] AwbStates
		{
			get { return new[] {CargoIsCustomsClearedStateId, CargoIsFlewStateId, CargoAtCustomsStateId}; }
		}
	}
}