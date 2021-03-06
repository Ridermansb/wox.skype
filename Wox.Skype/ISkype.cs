﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wox.Skype
{
	public interface ISkype
	{
		bool Open();

		IList<SkypeUser> getFriendsBy(string query);

		void OpenMessage(string userName);
	}
}
