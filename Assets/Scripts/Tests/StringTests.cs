using System;
using NUnit.Framework;
using UnityEngine;
using Core.Utility;

namespace Tests
{
    public class StringTests
    {
        [Test]
        public void EscapeStringTest()
        {
            string expected = "Hello World,";
            string outString = StringUtility.EscapeString("Hello World~c~");
            if (outString == expected)
            {
                Debug.Log($"Test passed!{Environment.NewLine}Expected: {expected}{Environment.NewLine}Actual: {outString}");
                Assert.Pass();
            }
            else
            {
                Debug.LogError($"Test failed!{Environment.NewLine}Expected: {expected}{Environment.NewLine}Actual: {outString}");
                Assert.Fail();
            }
        }
    }
}
