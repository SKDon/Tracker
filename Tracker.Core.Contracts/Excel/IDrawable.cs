﻿namespace Tracker.Core.Contracts.Excel
{
	public interface IDrawable
	{
		int Draw(int iRow);

		long Position { get; }
	}
}