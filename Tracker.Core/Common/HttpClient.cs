﻿using Tracker.Core.Contracts.Common;

namespace Tracker.Core.Common
{
	public sealed class HttpClient : IHttpClient
	{
		private readonly System.Net.Http.HttpClient _client;

		public HttpClient()
		{
			_client = new System.Net.Http.HttpClient();
		}

		public byte[] Get(string url)
		{
			return _client.GetByteArrayAsync(url).Result;
		}
	}
}