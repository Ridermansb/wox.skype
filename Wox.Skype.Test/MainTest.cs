using NUnit.Framework;
using Wox.Plugin;
using FluentAssertions;

namespace Wox.Skype.Test
{
	[TestFixture]
	public class MainTest
	{
		[Test]
		public void EmptyContacts()
		{
			var main = new Main();
			main.Init(new PluginInitContext());

			var results = main.Query(new Query("skp a"));

			results.Should().NotBeEmpty();
		}


	}
}
