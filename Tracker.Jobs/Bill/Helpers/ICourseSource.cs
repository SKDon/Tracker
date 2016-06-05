namespace Tracker.Jobs.Bill.Helpers
{
	public interface ICourseSource
	{
		decimal GetEuroToRuble(string url);
	}
}