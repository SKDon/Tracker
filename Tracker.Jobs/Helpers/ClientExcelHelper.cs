using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tracker.Core.Contracts.Calculation;
using Tracker.Core.Helpers;
using Tracker.DataAccess.Contracts.Contracts;
using Tracker.DataAccess.Contracts.Contracts.User;
using Tracker.DataAccess.Contracts.Repositories.User;
using Tracker.Jobs.Helpers.Abstract;
using Tracker.Utilities;

namespace Tracker.Jobs.Helpers
{
	internal sealed class ClientExcelHelper : IClientExcelHelper
	{
		private readonly IClientRepository _clients;
		private readonly IExcelClientCalculation _excel;

		public ClientExcelHelper(
			IClientRepository clients,
			IExcelClientCalculation excel)
		{
			_clients = clients;
			_excel = excel;
		}

		public IReadOnlyDictionary<string, FileHolder> GetExcels(long clientId, string[] languages)
		{
			var clientData = _clients.Get(clientId);
			var name = GetName(clientData);

			var files = languages
				.Distinct()
				.ToDictionary(
					x => x,
					language =>
					{
						using(var stream = _excel.Get(clientId, language))
						{
							return new FileHolder
							{
								Data = stream.ToArray(),
								Name = name
							};
						}
					});

			return files;
		}

		private static string GetName(ClientData clientData)
		{
			var date = LocalizationHelper.GetDate(DateTimeProvider.Now, CultureInfo.GetCultureInfo(clientData.Language));
			var name = "calculation_" + date + "_" + clientData.Nic + ".xlsx";

			return name.EscapeFileName();
		}
	}
}