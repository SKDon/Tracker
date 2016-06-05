using System.Collections.Generic;
using System.Linq;
using Tracker.DataAccess.Contracts.Helpers;
using Tracker.DataAccess.DbContext;

namespace Tracker.DataAccess.Helpers
{
	internal interface IApplicationRepositoryOrderer
	{
		IQueryable<Application> Order(IQueryable<Application> applications, IList<Order> orders);
	}
}