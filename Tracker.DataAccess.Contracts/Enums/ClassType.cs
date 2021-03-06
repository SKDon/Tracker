﻿using Tracker.Utilities.Localization;

namespace Tracker.DataAccess.Contracts.Enums
{
	public enum ClassType
	{
		[DisplayNameLocalized(typeof(Resources.Enums), "Econom")]
		Econom = 1,

		[DisplayNameLocalized(typeof(Resources.Enums), "Comfort")]
		Comfort = 2,

		[DisplayNameLocalized(typeof(Resources.Enums), "Lux")]
		Lux = 3
	}
}