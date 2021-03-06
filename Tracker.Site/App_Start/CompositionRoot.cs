﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Tracker.Core.AirWaybill;
using Tracker.Core.Contracts.AirWaybill;
using Tracker.Core.Contracts.Calculation;
using Tracker.Core.Contracts.Common;
using Tracker.Core.Contracts.Email;
using Tracker.Core.Contracts.Event;
using Tracker.Core.Contracts.State;
using Tracker.Core.Contracts.Users;
using Tracker.Core.Email;
using Tracker.Core.Event;
using Tracker.Core.Excel.Client;
using Tracker.Core.State;
using Tracker.Core.Users;
using Tracker.DataAccess.Contracts.Repositories;
using Tracker.DataAccess.Contracts.Repositories.Application;
using Tracker.DataAccess.DbContext;
using Tracker.DataAccess.Repositories.Application;
using Tracker.DataAccess.Repositories.User;
using Tracker.Jobs;
using Tracker.Utilities;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace Tracker
{
	internal static class CompositionRoot
	{
		private const string TrackerDataAccessDll = "Tracker.DataAccess.dll";

		private static long DefaultStateId
		{
			get { return ConfigurationManager.AppSettings["StateId-Default"].ToLong(); }
		}

		public static void BindServices(IKernel kernel, ILog mainLog)
		{
			kernel.Bind<ILog>().ToMethod(context => mainLog).InSingletonScope();

			kernel.Bind<IPasswordConverter>().To<PasswordConverter>().InThreadScope();

			kernel.Bind<IExcelClientCalculation>().To<ExcelClientCalculation>().InRequestScope();

			kernel.Bind<IAwbGtdHelper>().To<AwbGtdHelper>().InRequestScope();

			kernel.Bind<IAwbManager>().To<AwbManager>().InRequestScope();

			kernel.Bind<IApplicationStateManager>().To<ApplicationStateManager>().InRequestScope();

			kernel.Bind<IAwbStateManager>().To<AwbStateManager>().InRequestScope();

			kernel.Bind<IStateConfig>().To<StateConfig>().InRequestScope();

			kernel.Bind<IStateFilter>().To<StateFilter>().InRequestScope();

			kernel.Bind<IForwarderService>().To<ForwarderService>().InRequestScope();

			kernel.Bind<IEventFacade>().To<EventFacade>().InRequestScope();

			kernel.Bind<IPartitionConverter>().To<PartitionConverter>()
				.InSingletonScope()
				.WithConstructorArgument("partitionCount", CompositionJobsHelper.PartitionCount);

			kernel.Bind<ISerializer>().To<Serializer>().InThreadScope();

			kernel.Bind<IApplicationEditor>().To<ApplicationEditorWithEvent>();
			kernel.Bind<IApplicationEditor>().To<ApplicationEditor>()
				.WhenInjectedExactlyInto<ApplicationEditorWithEvent>()
				.WithConstructorArgument("defaultStateId", DefaultStateId);

			kernel.Bind<IMailSender>()
				.To<DbMailSender>()
				.InSingletonScope()
				.WithConstructorArgument("partitionId", CompositionJobsHelper.PartitionIdForOtherMails);

			var binded = CompositionRootHelper.BindDecorators(kernel, CompositionRootHelper.Decorators);

			kernel.Bind(scanner => scanner.FromThisAssembly()
				.IncludingNonePublicTypes()
				.Select(IsServiceType)
				.Excluding(binded)
				.Excluding<ApplicationEditorWithEvent>()
				.BindDefaultInterface()
				.Configure(binding => binding.InRequestScope()));
		}

		private static bool IsServiceType(Type type)
		{
			return type.IsClass && type.GetInterfaces().Any(intface => intface.Name == "I" + type.Name);
		}

		public static void BindDataAccess(IKernel kernel, string mainConnectionString, string filesConnectionString)
		{
			kernel.Bind(x => x.FromAssembliesMatching(TrackerDataAccessDll)
				.IncludingNonePublicTypes()
				.Select(IsServiceType)
				.Excluding<SqlProcedureExecutor>()
				.Excluding<ApplicationEditor>()
				.BindDefaultInterface()
				.Configure(syntax => syntax.InRequestScope()));

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", mainConnectionString);

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.WhenInjectedInto<ClientFileRepository>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", filesConnectionString);

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.WhenInjectedInto<ApplicationFileRepository>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", filesConnectionString);

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.WhenInjectedInto<AwbFileRepository>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", filesConnectionString);
		}

		public static void BindConnection(IKernel kernel, string connectionString)
		{
			kernel.Bind<IDbConnection>()
				.ToMethod(x => new SqlConnection(connectionString))
				.InRequestScope()
				.OnDeactivation(x => x.Close());
		}
	}
}