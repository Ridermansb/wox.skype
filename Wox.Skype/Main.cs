using SKYPE4COMLib;
using System.Collections.Generic;
using Wox.Plugin;
using System.Linq;
using System;

namespace Wox.Skype
{
	public class Main : IPlugin
	{
		private ISkype _skype;

		public Main(ISkype skype)
		{
			_skype = skype;
		}
		public Main() 
			: this(null)
		{ 
		}

		public void Init(PluginInitContext context)
		{
			if (_skype == null)
			{
				_skype = new SkypeCOM();
			}
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
					Action = e => _skype.Open()
				});
				return results;
			}


			var queryName = query.ActionParameters[0].ToLower();

			_skype.getFriendsBy(queryName).ToList().ForEach(u =>
			{

				results.Add(new Result()
				{
					Title = u.FullName,
					SubTitle = u.StatusText,
					IcoPath = "Images\\plugin.png",
					Action = e =>
					{
						_skype.OpenMessage(u.UserName);
						return true;
					}
				});

			});

			return results;
		}
	}
}
