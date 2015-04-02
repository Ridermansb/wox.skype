using SKYPE4COMLib;
using System.Collections.Generic;
using Wox.Plugin;
using System.Linq;
using System.Diagnostics;

namespace Wox.Skype
{
	public class Main : IPlugin
	{
		private SkypeClass _skype;
		public void Init(PluginInitContext context)
		{
			_skype = new SkypeClass();
			_skype.Attach(6, true);
		}

		public List<Result> Query(Query query)
		{
			List<Result> results = new List<Result>();

			if (query.ActionParameters.Count == 0)
			{
				results.Add(new Result()
				{
					IcoPath = "Images\\app.png",
					Title = "Open skype",
					Action = e =>
					{
						try
						{
							_skype.Client.Start();
						}
						catch (System.Exception ex)
						{
							using (EventLog eventLog = new EventLog("Aplicativo"))
							{
								eventLog.Source = "Aplicativo";
								eventLog.WriteEntry(ex.Message, EventLogEntryType.Information, 101, 1);
							} 

							var cs = "Wox.Skype";
							if (!EventLog.SourceExists(cs))
								EventLog.CreateEventSource(cs, "Aplicativo");

							EventLog.WriteEntry(cs, ex.Message, EventLogEntryType.Error);
						}
						return false;


					}
				});
				return results;
			}


			var str = query.ActionParameters[0]; //.ToLower();

			//IEnumerable<SKYPE4COMLib.User> users = _skype.SearchForUsers(str).OfType<SKYPE4COMLib.User>(); // _skype.Friends.OfType<SKYPE4COMLib.User>();
			foreach (User u in _skype.SearchForUsers(str))
			{
				try
				{
					results.Add(new Result()
								{
									Title = u.DisplayName,
									SubTitle = u.MoodText,
									IcoPath = "Images\\plugin.png",  //relative path to your plugin directory
									Action = e =>
									{
										// after user select the item

										// return false to tell Wox don't hide query window, otherwise Wox will hide it automatically
										return false;
									}
								});
				}
				catch { }
			}

			//users
			//	.Where(u => u.OnlineStatus == TOnlineStatus.olsOnline
			//		&& (u.Aliases != null && !string.IsNullOrEmpty(u.Aliases) ? u.Aliases.ToLower().Contains(str) : false
			//			|| u.DisplayName != null && !string.IsNullOrEmpty(u.DisplayName) ? u.DisplayName.ToLower().Contains(str) : false
			//			|| u.FullName != null && !string.IsNullOrEmpty(u.FullName) ? u.FullName.ToLower().Contains(str) : false
			//			|| u.PhoneHome != null && !string.IsNullOrEmpty(u.PhoneHome) ? u.PhoneHome.ToLower().Contains(str) : false
			//			|| u.PhoneMobile != null && !string.IsNullOrEmpty(u.PhoneMobile) ? u.PhoneMobile.ToLower().Contains(str) : false
			//			|| u.PhoneOffice != null && !string.IsNullOrEmpty(u.PhoneOffice) ? u.PhoneOffice.ToLower().Contains(str) : false)
			//		)
			//	.OrderBy(u => u.FullName)
			//	.ToList()
			//	.ForEach(u =>
			//	{

			//		results.Add(new Result()
			//		{
			//			Title = u.DisplayName,
			//			SubTitle = u.MoodText,
			//			IcoPath = "Images\\plugin.png",  //relative path to your plugin directory
			//			Action = e =>
			//			{
			//				// after user select the item

			//				// return false to tell Wox don't hide query window, otherwise Wox will hide it automatically
			//				return false;
			//			}
			//		});

			//	});


			return results;
		}
	}
}
