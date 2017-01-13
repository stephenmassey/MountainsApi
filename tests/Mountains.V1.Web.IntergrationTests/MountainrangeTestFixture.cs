﻿using Mountains.V1.Client;
using Mountains.V1.Client.Dtos;
using NUnit.Framework;
using System;

namespace Mountains.V1.Web.IntergrationTests
{
    [TestFixture]
    public sealed class MountainRangeTestFixture : TestFixtureBase
    {
        [Test]
        public void GetMountainRanges()
        {
            MountainsClient client = CreateMountainsClient();

            MountainRangeCollectionDto mountainRanges = client.GetMountainRangesAsync().Result;

            Assert.IsNotNull(mountainRanges);
            Assert.IsNotNull(mountainRanges.MountainRanges);
        }

        [Test]
        public void CreateMountainRange()
        {
            MountainsClient client = CreateMountainsClient();

            MountainRangeDto expectedMountainRange = CreateMountainRangeDto();

            MountainRangeDto actualMountainRange = client.CreateMountainRangeAsync(expectedMountainRange).Result;
            expectedMountainRange.Id = actualMountainRange.Id;

            Assert.IsNotNull(actualMountainRange);
            AssertIsEqual(expectedMountainRange, actualMountainRange);

            MountainRangeDto mountainRange = client.GetMountainRangeAsync(actualMountainRange.Id).Result;

            Assert.IsNotNull(mountainRange);
            AssertIsEqual(expectedMountainRange, mountainRange);
        }

        [Test]
        public void UpdateMountainRange()
        {
            MountainsClient client = CreateMountainsClient();

            MountainRangeDto expectedMountainRange = CreateMountainRangeDto();

            MountainRangeDto actualMountainRange = client.CreateMountainRangeAsync(expectedMountainRange).Result;
            expectedMountainRange.Id = actualMountainRange.Id;
            expectedMountainRange.Name = "Updated " + Guid.NewGuid().ToString();

            actualMountainRange = client.UpdateMountainRangeAsync(expectedMountainRange.Id, expectedMountainRange).Result;

            Assert.IsNotNull(actualMountainRange);
            AssertIsEqual(expectedMountainRange, actualMountainRange);

            MountainRangeDto mountainRange = client.GetMountainRangeAsync(actualMountainRange.Id).Result;

            Assert.IsNotNull(mountainRange);
            AssertIsEqual(expectedMountainRange, mountainRange);
        }

        [Test]
        public void DeleteMountainRange()
        {
            MountainsClient client = CreateMountainsClient();

            MountainRangeDto expectedMountainRange = CreateMountainRangeDto();
            MountainRangeDto actualMountainRange = client.CreateMountainRangeAsync(expectedMountainRange).Result;

            Assert.IsTrue(client.DeleteMountainRangeAsync(actualMountainRange.Id).Result);

            MountainRangeDto mountainRange = client.GetMountainRangeAsync(actualMountainRange.Id).Result;
            Assert.IsNull(mountainRange);
        }
    }
}
