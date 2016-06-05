using System;

namespace Tracker.Jobs.Application.Entities
{
	internal sealed class TextBulderException : Exception
	{
		public TextBulderException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
