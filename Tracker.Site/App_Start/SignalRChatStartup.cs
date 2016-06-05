using Tracker;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRChatStartup))]

namespace Tracker
{
	public class SignalRChatStartup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
		}
	}
}