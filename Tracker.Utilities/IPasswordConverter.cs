﻿namespace Tracker.Utilities
{
    public interface IPasswordConverter
    {
        byte[] GetPasswordHash(string password, byte[] salt);
	    byte[] GenerateSalt();
    }
}
