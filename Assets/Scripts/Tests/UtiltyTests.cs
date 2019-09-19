using System;
using NUnit.Framework;
using UnityEngine;
using Core.Utility;

namespace Tests
{
    public class UtiltyTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void MouseCursorTests()
        {
            MouseCursor.LockCursor(true);
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Assert.Pass();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Assert.Fail();
            }

            MouseCursor.LockCursor(false);
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Assert.Pass();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Assert.Fail();
            }
            
            MouseCursor.ToggleCursor(false);
            Assert.Equals(Cursor.visible, false);
            
            MouseCursor.ToggleCursor(true);
            Assert.Equals(Cursor.visible, true);
        }

        [Test]
        public void StringTest()
        {
            string expected = "Hello World,";
            string outString = StringUtility.EscapeString("Hello World~c~");
            if (outString == expected)
            {
                Assert.Pass();
            }
            else
            {
                Debug.Log($"Test failed!{Environment.NewLine}Expected: {expected}{Environment.NewLine}Actual: {outString}");
                Assert.Fail();
            }
        }
    }
}
