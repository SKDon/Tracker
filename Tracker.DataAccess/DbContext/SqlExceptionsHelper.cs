using System;
using System.Data.Linq;
using System.Data.SqlClient;
using Tracker.DataAccess.Contracts.Exceptions;

namespace Tracker.DataAccess.DbContext
{
	internal static class SqlExceptionsHelper
	{
		public static void Action(Action action)
		{
			Action(() =>
			{
				action();
				return (object)null;
			});
		}

		public static T Action<T>(Func<T> action)
		{
			try
			{
				return action();
			}
			catch(SqlException exception)
			{
				switch((SqlError)exception.Number)
				{
					case SqlError.ViolationOfConstraint:
						throw new DublicateException("Failed to add dublicate entity", exception);

					case SqlError.ViolationOfUniqueIndex:
						if(exception.Message.Contains("IX_User_Login"))
						{
							throw new DublicateLoginException("The login is occupied", exception);
						}

						throw new DublicateException("Failed to add or update dublicate entity", exception);

					case SqlError.DeleteStatementConflictedWihtConstraint:
						throw new DeleteConflictedWithConstraintException("Can't delete an entity", exception);
				}

				throw;
			}
		}

		public static void SaveChanges(this TrackerDataContext context)
		{
			Action(() => context.SubmitChanges(ConflictMode.FailOnFirstConflict));
		}
	}
}