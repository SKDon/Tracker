using System;
using Tracker.Areas.Admin.Models;

namespace Tracker.Areas.Admin.Serivices.Abstract
{
	public interface IBillManager
	{
		void Save(long applicationId, int number, BillModel model, DateTimeOffset saveDate, DateTimeOffset? sendDate);
		void Send(long applicationId, long userId);
	}
}