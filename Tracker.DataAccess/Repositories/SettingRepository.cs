﻿using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Enums;
using Tracker.DataAccess.Contracts.Exceptions;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.Utilities;

namespace Tracker.DataAccess.Repositories
{
	public sealed class SettingRepository : ISettingRepository
	{
		private readonly ISqlProcedureExecutor _executor;
		private readonly ISerializer _serializer;

		public SettingRepository(ISqlProcedureExecutor executor, ISerializer serializer)
		{
			_executor = executor;
			_serializer = serializer;
		}

		public Setting AddOrReplace(Setting setting)
		{
			try
			{
				var rowVersion = _executor.Query<byte[]>("[dbo].[Setting_Merge]", setting);

				return new Setting
				{
					Data = setting.Data,
					RowVersion = rowVersion,
					Type = setting.Type
				};
			}
			catch(DublicateException e)
			{
				throw new UpdateConflictException("Failed to merge setting " + setting.Type, e);
			}
		}

		public Setting Get(SettingType type)
		{
			return _executor.Query<Setting>("[dbo].[Setting_Get]", new { type });
		}

		public T GetData<T>(SettingType type)
		{
			return _serializer.Deserialize<T>(Get(type).Data);
		}
	}
}