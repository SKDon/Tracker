﻿namespace Tracker.Core.Contracts.Common
{
	public interface IHttpClient
	{
		byte[] Get(string url);
	}
}