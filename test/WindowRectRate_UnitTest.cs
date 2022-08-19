using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using WindowController;

namespace WindowControllerTest
{

    [TestClass]
    public class WindowRectRate_UnitTest
    {
        [TestMethod]
        public void Test_FromRect_PositiveX_PositiveY()
        {
            var parentRect = new RECT
            {
                left = 100,
                right = 200,
                top = 100,
                bottom = 200,
            };
            var childRect = new RECT
            {
                left = 110,
                right = 190,
                top = 110,
                bottom = 190,
            };

            var expected = new WindowRectRate
            {
                LeftRate = 0.1,
                TopRate = 0.1,
                RightRate = 0.9,
                BottomRate = 0.9,
            };

            var actual = WindowRectRate.FromRect(parentRect, childRect);

            Assert.AreEqual(expected, actual, $"{expected.LeftRate} -> {actual.LeftRate}");
        }
    }
}
