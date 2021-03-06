﻿namespace Tracker.Utilities
{
	public interface ISerializer
	{
		byte[] Serialize<T>(T data);
		T Deserialize<T>(byte[] data);
	}
}