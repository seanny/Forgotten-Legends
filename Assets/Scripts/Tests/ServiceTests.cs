using System;
using NUnit.Framework;
using Core.Services;
using UnityEngine;

namespace Tests
{
    public class ServiceTests
    {
        public static readonly string TEST_STRING = "The quick brown fox jumps over the lazy dog";
        public static readonly int TEST_INT = 420;
        public static readonly float TEST_FLOAT = 4.20f;
        public static readonly double TEST_DOUBLE = 4.20;
        
        [Test]
        public void GetService()
        {
            if (ServiceLocator.GetService<TestService>() == null)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
        
        [Test]
        public void GetInt()
        {
            if (ServiceLocator.GetService<TestService>().GetInt() != TEST_INT)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
        
        [Test]
        public void GetFloat()
        {
            if (ServiceLocator.GetService<TestService>().GetFloat() != TEST_FLOAT)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
        
        [Test]
        public void GetDouble()
        {
            if (ServiceLocator.GetService<TestService>().GetDouble() != TEST_DOUBLE)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
        
        [Test]
        public void GetString()
        {
            if (ServiceLocator.GetService<TestService>().GetString() != TEST_STRING)
            {
                Assert.Fail();
            }
            Assert.Pass();
        }
        
        [Test]
        public void EndAllServices()
        {
            try
            {
                ServiceLocator.EndAllServices();
            }
            catch (Exception e)
            {
                Debug.LogError($"Test Failed: {e.Message}");
                Assert.Fail();
            }
            Assert.Pass();
        }
    }
    
    public class TestService : IService
    {
        private int testInt = ServiceTests.TEST_INT;
        private float testFloat = ServiceTests.TEST_FLOAT;
        private double testDouble = ServiceTests.TEST_DOUBLE;
        private string testString = ServiceTests.TEST_STRING;

        public int GetInt() => testInt;
        public float GetFloat() => testFloat;
        public double GetDouble() => testDouble;
        public string GetString() => testString;

        public void OnStart()
        {
            
        }

        public void OnEnd()
        {
            
        }
    }
}