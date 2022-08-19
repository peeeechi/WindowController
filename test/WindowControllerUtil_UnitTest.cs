using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using WindowController;

namespace WindowControllerTest
{

    [TestClass]
    public class WindowControllerUtil_UnitTest
    {
        [TestMethod]
        public void Test_ConvertWildcard2Regex()
        {
            var expected = new Regex("^.*test.*$");
   
            var targetKeyword = "*test*";

            var actual = WindowControllerUtil.ConvertWildcard2Regex(targetKeyword);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [TestMethod]
        public void Test_IsMatchTitle()
        {
            var expected_matches = new List<string>
            {
                "test",
                "test__huga",
                "huga__test__huga",
            };

            var expected_not_matches = new List<string>
            {
                "t est",
                "__tes",
                "te",
            };
    
            var targetKeyword = "*test*";

            var regex = WindowControllerUtil.ConvertWildcard2Regex(targetKeyword);

            foreach (var text in expected_matches)
            {
                var mock = new Mock<IWindowController>();
                mock.Setup(o=> o.GetWindowText(SendMessageTimeoutFlgs.SMTO_BLOCK, 1000)).Returns(text);
                var mockController = mock.Object;
                Assert.IsTrue(WindowControllerUtil.IsMatchTitle(mockController, targetKeyword));      
                mock.Verify(o => o.GetWindowText(SendMessageTimeoutFlgs.SMTO_BLOCK, 1000), Times.Once);       
            }

            foreach (var text in expected_not_matches)
            {
                var mock = new Mock<IWindowController>();
                mock.Setup(o=> o.GetWindowText(SendMessageTimeoutFlgs.SMTO_BLOCK, 1000)).Returns(text);
                var mockController = mock.Object;
                Assert.IsFalse(WindowControllerUtil.IsMatchTitle(mockController, targetKeyword));      
                mock.Verify(o => o.GetWindowText(SendMessageTimeoutFlgs.SMTO_BLOCK, 1000), Times.Once);       
            }
        }

        [TestMethod]
        public void Test_IsWithinRectRange_PositiveSide()
        {
            var targetRect = new WindowController.RECT
            {
                top= 10,
                bottom = 110,
                left = 10,
                right = 110,
            };
            
            var withinRectList = new List<WindowController.RECT>{
                new WindowController.RECT{left=10, top=10, right=110, bottom=110},
                new WindowController.RECT{left=11, top=10, right=110, bottom=110},
                new WindowController.RECT{left=10, top=11, right=110, bottom=110},
                new WindowController.RECT{left=10, top=10, right=109, bottom=110},
                new WindowController.RECT{left=10, top=10, right=110, bottom=109},
            };

            var notWithinRectList = new List<WindowController.RECT>{
                new WindowController.RECT{left=9, top=9, right=111, bottom=111},
                new WindowController.RECT{left=9, top=10, right=110, bottom=110},
                new WindowController.RECT{left=10, top=9, right=110, bottom=110},
                new WindowController.RECT{left=10, top=10, right=111, bottom=110},
                new WindowController.RECT{left=10, top=10, right=110, bottom=111},
            };

            foreach (var rect in withinRectList)
            {
                var mock = new Mock<IWindowController>();
                mock.Setup(o=> o.GetRect()).Returns(rect);
                var mockController = mock.Object;
                Assert.IsTrue(WindowControllerUtil.IsWithinRectRange(mockController, targetRect));      
                mock.Verify(o => o.GetRect(), Times.Once);       
            }

            foreach (var rect in notWithinRectList)
            {
                var mock = new Mock<IWindowController>();
                mock.Setup(o=> o.GetRect()).Returns(rect);
                var mockController = mock.Object;
                Assert.IsFalse(WindowControllerUtil.IsWithinRectRange(mockController, targetRect));      
                mock.Verify(o => o.GetRect(), Times.Once);       
            }
        }

        [TestMethod]
        public void Test_IsWithinRectRange_NegativeSide()
        {
            var targetRect = new WindowController.RECT
            {
                top= -110,
                bottom = -10,
                left = -110,
                right = -10,
            };
            
            var withinRectList = new List<WindowController.RECT>{
                new WindowController.RECT{left=-110, top=-110, right=-10, bottom=-10},
                new WindowController.RECT{left=-109, top=-110, right=-10, bottom=-10},
                new WindowController.RECT{left=-110, top=-109, right=-10, bottom=-10},
                new WindowController.RECT{left=-110, top=-110, right=-11, bottom=-10},
                new WindowController.RECT{left=-110, top=-110, right=-10, bottom=-11},
                
            };

            var notWithinRectList = new List<WindowController.RECT>{
                new WindowController.RECT{left=-111, top=-111, right=-9, bottom=-9},
                new WindowController.RECT{left=-111, top=-110, right=-10, bottom=-10},
                new WindowController.RECT{left=-110, top=-111, right=-10, bottom=-10},
                new WindowController.RECT{left=-110, top=-110, right=-9, bottom=-10},
                new WindowController.RECT{left=-110, top=-110, right=-10, bottom=-9},
                
            };

            foreach (var rect in withinRectList)
            {
                var mock = new Mock<IWindowController>();
                mock.Setup(o=> o.GetRect()).Returns(rect);
                var mockController = mock.Object;
                Assert.IsTrue(WindowControllerUtil.IsWithinRectRange(mockController, targetRect));      
                mock.Verify(o => o.GetRect(), Times.Once);       
            }

            foreach (var rect in notWithinRectList)
            {
                var mock = new Mock<IWindowController>();
                mock.Setup(o=> o.GetRect()).Returns(rect);
                var mockController = mock.Object;
                Assert.IsFalse(WindowControllerUtil.IsWithinRectRange(mockController, targetRect));      
                mock.Verify(o => o.GetRect(), Times.Once);       
            }
        }
    }
}
