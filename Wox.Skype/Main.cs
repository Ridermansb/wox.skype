using System.Collections.Generic;
using Wox.Plugin;

namespace Wox.Skype
{
	public class Main : IPlugin
    {
		public void Init(PluginInitContext context)
		{
			//throw new NotImplementedException();
		}

		public List<Result> Query(Query query)
		{
			List<Result> results = new List<Result>();
			results.Add(new Result()
			{
				Title = "Title",
				SubTitle = "Sub title",
				IcoPath = "Images\\plugin.png",  //relative path to your plugin directory
				Action = e =>
				{
					// after user select the item

					// return false to tell Wox don't hide query window, otherwise Wox will hide it automatically
					return false;
				}
			});
			return results;
		}
	}
}
