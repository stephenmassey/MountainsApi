using Mountains.V1.Client;
using Mountains.V1.Client.Dtos;
using NUnit.Framework;
using System;

namespace Mountains.V1.Web.IntergrationTests
{
    public abstract class TestFixtureBase
    {
        protected MountainsClient CreateMountainsClient()
        {
            return new MountainsClient(GetMountainsUri());
        }

        protected MountainDto CreateMountainDto()
        {
            return new MountainDto
            {
                Name = Guid.NewGuid().ToString(),
            };
        }

        protected void AssertIsEqual(MountainDto expectedMountain, MountainDto actualMountain)
        {
            Assert.IsNotNull(expectedMountain);
            Assert.IsNotNull(actualMountain);

            Assert.AreEqual(expectedMountain.Id, actualMountain.Id);
            Assert.AreEqual(expectedMountain.Name, actualMountain.Name);
        }

        private Uri GetMountainsUri()
        {
            return new Uri("http://local.mountainsapi.awesome.com/v1/");
        }
    }
}
