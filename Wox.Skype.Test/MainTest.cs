using NUnit.Framework;
using Wox.Plugin;
using FluentAssertions;
using Moq;
using System.Collections.Generic;

namespace Wox.Skype.Test
{
	[TestFixture]
	public class MainTest
	{
		private Main _main;
		private Mock<ISkype> _skype;

		[SetUp]
		public void Setup()
		{
			_skype = new Mock<ISkype>();
			_main = new Main(_skype.Object);
			_main.Init(new PluginInitContext());
		}

		[Test]
		public void OnQueryShouldCallSkypeFilter()
		{
			_skype.Setup(p => p.getFriendsBy(It.IsAny<string>())).Returns(new List<SkypeUser>());
			var results = _main.Query(new Query("skp a"));

			_skype.Verify(p => p.getFriendsBy("a"), Times.Once);
		}

		[Test]
		public void OnQueryWithForAExistentContactShouldReturnContact()
		{
			var contact = new SkypeUser { UserName = "<a username>", FullName = "<a full name>", StatusText = "<a status text>" };
			_skype.Setup(p => p.getFriendsBy(It.IsAny<string>())).Returns(new List<SkypeUser>(){ contact });
			var results = _main.Query(new Query("skp name"));


			results.Should().ContainSingle();
		}

		[Test]
		public void OnQueryWithForANonExistentContactShouldReturnContact()
		{
			var contact = new SkypeUser { UserName = "<a username>", FullName = "<a full name>", StatusText = "<a status text>" };
			_skype.Setup(p => p.getFriendsBy(It.IsAny<string>())).Returns(new List<SkypeUser>() { });
			var results = _main.Query(new Query("skp contact"));


			results.Should().BeEmpty();
		}
	}
}
