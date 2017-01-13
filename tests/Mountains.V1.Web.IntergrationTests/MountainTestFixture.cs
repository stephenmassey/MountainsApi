using Mountains.V1.Client;
using Mountains.V1.Client.Dtos;
using NUnit.Framework;
using System;
using System.Linq;

namespace Mountains.V1.Web.IntergrationTests
{
    [TestFixture]
    public sealed class MountainTestFixture : TestFixtureBase
    {
        [Test]
        public void GetMountains()
        {
            MountainsClient client = CreateMountainsClient();

            MountainCollectionDto mountains = client.GetMountainsAsync().Result;

            Assert.IsNotNull(mountains);
            Assert.IsNotNull(mountains.Mountains);
        }

        [Test]
        public void CreateMountain()
        {
            MountainsClient client = CreateMountainsClient();

            MountainDto expectedMountain = CreateMountainDto();

            MountainDto actualMountain = client.CreateMountainAsync(expectedMountain).Result;
            expectedMountain.Id = actualMountain.Id;

            Assert.IsNotNull(actualMountain);
            AssertIsEqual(expectedMountain, actualMountain);

            MountainDto mountain = client.GetMountainAsync(actualMountain.Id).Result;

            Assert.IsNotNull(mountain);
            AssertIsEqual(expectedMountain, mountain);
        }

        [Test]
        public void CreateMountainWithNullName()
        {
            MountainsClient client = CreateMountainsClient();

            MountainDto expectedMountain = CreateMountainDto();
            expectedMountain.Name = null;

            Assert.IsNull(client.CreateMountainAsync(expectedMountain).Result);
        }

        [Test]
        public void CreateMountainWithEmptyName()
        {
            MountainsClient client = CreateMountainsClient();

            MountainDto expectedMountain = CreateMountainDto();
            expectedMountain.Name = "";

            Assert.IsNull(client.CreateMountainAsync(expectedMountain).Result);
        }

        [Test]
        public void CreateMountainWithLongName()
        {
            MountainsClient client = CreateMountainsClient();

            MountainDto expectedMountain = CreateMountainDto();
            expectedMountain.Name = Enumerable.Range(1, 500).Select(x => "a").Aggregate((x, y) => x + y);

            Assert.IsNull(client.CreateMountainAsync(expectedMountain).Result);
        }

        [Test]
        public void UpdateMountain()
        {
            MountainsClient client = CreateMountainsClient();

            MountainDto expectedMountain = CreateMountainDto();

            MountainDto actualMountain = client.CreateMountainAsync(expectedMountain).Result;
            expectedMountain.Id = actualMountain.Id;
            expectedMountain.Name = "Updated " + Guid.NewGuid().ToString();

            actualMountain = client.UpdateMountainAsync(expectedMountain.Id, expectedMountain).Result;

            Assert.IsNotNull(actualMountain);
            AssertIsEqual(expectedMountain, actualMountain);

            MountainDto mountain = client.GetMountainAsync(actualMountain.Id).Result;

            Assert.IsNotNull(mountain);
            AssertIsEqual(expectedMountain, mountain);
        }

        [Test]
        public void DeleteMountain()
        {
            MountainsClient client = CreateMountainsClient();

            MountainDto expectedMountain = CreateMountainDto();
            MountainDto actualMountain = client.CreateMountainAsync(expectedMountain).Result;

            Assert.IsTrue(client.DeleteMountainAsync(actualMountain.Id).Result);

            MountainDto mountain = client.GetMountainAsync(actualMountain.Id).Result;
            Assert.IsNull(mountain);
        }
    }
}
