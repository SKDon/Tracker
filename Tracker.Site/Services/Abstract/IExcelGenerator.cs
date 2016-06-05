using System.IO;

namespace Tracker.Services.Abstract
{
	public interface IExcelGenerator<in T>
	{
		MemoryStream Get(T[] rows, string language);
	}
}