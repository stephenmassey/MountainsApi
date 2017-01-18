using Mountains.V1.Client;
using Mountains.V1.Client.Dtos;
using NUnit.Framework;
using System;
using System.Linq;

namespace Mountains.V1.Web.IntergrationTests
{
    [TestFixture]
    public sealed class HikeTestFixture : TestFixtureBase
    {
        [Test]
        public void GetHikes()
        {
            MountainsClient client = CreateMountainsClient();

            HikeCollectionDto hikes = client.GetHikesAsync().Result;

            Assert.IsNotNull(hikes);
            Assert.IsNotNull(hikes.Hikes);
        }

        [Test]
        public void CreateHike()
        {
            MountainsClient client = CreateMountainsClient();

            HikeDto expectedHike = CreateHikeDto(client.CreateMountainAsync(CreateMountainDto()).Result.Id, client.CreateUserAsync(CreateUserDto()).Result.Id);

            HikeDto actualHike = client.CreateHikeAsync(expectedHike).Result;
            expectedHike.Id = actualHike.Id;

            Assert.IsNotNull(actualHike);
            AssertIsEqual(expectedHike, actualHike);

            HikeDto hike = client.GetHikeAsync(actualHike.Id).Result;

            Assert.IsNotNull(hike);
            AssertIsEqual(expectedHike, hike);
        }

        [Test]
        public void CreateHikeWithInvalidMountain()
        {
            MountainsClient client = CreateMountainsClient();

            Assert.IsNull(client.CreateHikeAsync(CreateHikeDto("0", client.CreateUserAsync(CreateUserDto()).Result.Id)).Result);
            Assert.IsNull(client.CreateHikeAsync(CreateHikeDto(null, client.CreateUserAsync(CreateUserDto()).Result.Id)).Result);
        }

        [Test]
        public void CreateHikeWithInvalidUser()
        {
            MountainsClient client = CreateMountainsClient();

            Assert.IsNull(client.CreateHikeAsync(CreateHikeDto(client.CreateMountainAsync(CreateMountainDto()).Result.Id, "0")).Result);
            Assert.IsNull(client.CreateHikeAsync(CreateHikeDto(client.CreateMountainAsync(CreateMountainDto()).Result.Id, null)).Result);
        }

        [Test]
        public void DeleteHike()
        {
            MountainsClient client = CreateMountainsClient();

            HikeDto expectedHike = CreateHikeDto(client.CreateMountainAsync(CreateMountainDto()).Result.Id, client.CreateUserAsync(CreateUserDto()).Result.Id);
            HikeDto actualHike = client.CreateHikeAsync(expectedHike).Result;

            Assert.IsTrue(client.DeleteHikeAsync(actualHike.Id).Result);

            HikeDto hike = client.GetHikeAsync(actualHike.Id).Result;
            Assert.IsNull(hike);
        }
    }
}
