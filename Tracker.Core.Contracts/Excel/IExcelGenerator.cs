using System.IO;

namespace Tracker.Core.Contracts.Excel
{
	public interface IExcelGenerator<in T>
	{
		MemoryStream Get(T[] rows, string language);
	}
}
