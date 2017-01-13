﻿using Mountains.V1.Client;
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
                Latitude = 4.65,
                Longitude = 5.3,
                Elevation = 6.5,
                Isolation = 0.4,
                Prominence = 5.5,
            };
        }

        protected MountainRangeDto CreateMountainRangeDto()
        {
            return new MountainRangeDto
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
            Assert.AreEqual(expectedMountain.Latitude, actualMountain.Latitude);
            Assert.AreEqual(expectedMountain.Longitude, actualMountain.Longitude);
            Assert.AreEqual(expectedMountain.Elevation, actualMountain.Elevation);
            Assert.AreEqual(expectedMountain.Isolation, actualMountain.Isolation);
            Assert.AreEqual(expectedMountain.Prominence, actualMountain.Prominence);
        }

        protected void AssertIsEqual(MountainRangeDto expectedMountainRange, MountainRangeDto actualMountainRange)
        {
            Assert.IsNotNull(expectedMountainRange);
            Assert.IsNotNull(actualMountainRange);

            Assert.AreEqual(expectedMountainRange.Id, actualMountainRange.Id);
            Assert.AreEqual(expectedMountainRange.Name, actualMountainRange.Name);
        }

        private Uri GetMountainsUri()
        {
            return new Uri("http://local.mountainsapi.awesome.com/v1/");
        }
    }
}
