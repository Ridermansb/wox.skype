using SKYPE4COMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wox.Skype
{
	public class SkypeCOM : ISkype
	{
		private SkypeClass _skype;
		public SkypeCOM()
		{
			_skype = new SkypeClass();
		}

		public bool Open()
		{
			_skype.Client.Start(Nosplash: true);
			return true;
		}


		[STAThread]
		public IList<SkypeUser> getFriendsBy(string query)
		{
			var list = new List<SkypeUser>();
			_skype.Friends.OfType<SKYPE4COMLib.User>()
				.Where(u => ((u.Aliases != null && !string.IsNullOrEmpty(u.Aliases) ? u.Aliases.ToLower().Contains(query) : false)
						|| (u.DisplayName != null && !string.IsNullOrEmpty(u.DisplayName) ? u.DisplayName.ToLower().Contains(query) : false)
						|| (u.FullName != null && !string.IsNullOrEmpty(u.FullName) ? u.FullName.ToLower().Contains(query) : false)
						|| (u.PhoneHome != null && !string.IsNullOrEmpty(u.PhoneHome) ? u.PhoneHome.ToLower().Contains(query) : false)
						|| (u.PhoneMobile != null && !string.IsNullOrEmpty(u.PhoneMobile) ? u.PhoneMobile.ToLower().Contains(query) : false)
						|| (u.PhoneOffice != null && !string.IsNullOrEmpty(u.PhoneOffice) ? u.PhoneOffice.ToLower().Contains(query) : false))
					)
				.ToList()
				.ForEach(u =>
				{
					list.Add(new SkypeUser
									{
										FullName = u.FullName
										,StatusText = u.MoodText
										,UserName = u.Handle
									});

				});

			return list;
		}

		[STAThread]
		public void OpenMessage(string userName)
		{
			 _skype.Client.OpenMessageDialog(userName);
		}
	}
}
