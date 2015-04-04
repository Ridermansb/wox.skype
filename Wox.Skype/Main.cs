using SKYPE4COMLib;
using System.Collections.Generic;
using Wox.Plugin;
using System.Linq;

namespace Wox.Skype
{
	public class Main : IPlugin
	{
		private SkypeClass _skype;
		public void Init(PluginInitContext context)
		{
			_skype = new SkypeClass();
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
						_skype.Client.Start();
						return true;


					}
				});
				return results;
			}


			var queryName = query.ActionParameters[0].ToLower();

			_skype.Friends.OfType<SKYPE4COMLib.User>()
				.Where(u => ((u.Aliases != null && !string.IsNullOrEmpty(u.Aliases) ? u.Aliases.ToLower().Contains(queryName) : false)
						|| (u.DisplayName != null && !string.IsNullOrEmpty(u.DisplayName) ? u.DisplayName.ToLower().Contains(queryName) : false)
						|| (u.FullName != null && !string.IsNullOrEmpty(u.FullName) ? u.FullName.ToLower().Contains(queryName) : false)
						|| (u.PhoneHome != null && !string.IsNullOrEmpty(u.PhoneHome) ? u.PhoneHome.ToLower().Contains(queryName) : false)
						|| (u.PhoneMobile != null && !string.IsNullOrEmpty(u.PhoneMobile) ? u.PhoneMobile.ToLower().Contains(queryName) : false)
						|| (u.PhoneOffice != null && !string.IsNullOrEmpty(u.PhoneOffice) ? u.PhoneOffice.ToLower().Contains(queryName) : false))
					)
				.OrderBy(u => u.FullName)
				.ToList()
				.ForEach(u =>
				{

					results.Add(new Result()
					{
						Title = u.FullName,
						SubTitle = u.MoodText,
						IcoPath = "Images\\plugin.png",
						Action = e =>
						{
							_skype.Client.OpenMessageDialog(u.Handle);
							return true;
						}
					});

				});


			return results;
		}
	}
}
