using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowController
{
    public class MouseEmulator
    {
        [Flags]
        public enum ClickButton: uint
        {
            LeftButton      = 1 << 0,
            RightButton     = 1 << 1,
            MiddleButton    = 1 << 2,
        }

        /// <summary>
        /// 指定された位置へマウスポインタを移動させてクリックを行います
        /// </summary
        /// <param name="screanX">クリックするスクリーン座標のX値</param>
        /// <param name="screanY">クリックするスクリーン座標のY値</param>
        public static void MoveAndClick(int screanX, int screanY, ClickButton clickButton=ClickButton.LeftButton, int waitBetweenActionMsec = 100)
        {
            MouseEmulator.MoveTo(screanX, screanY);
            System.Threading.Thread.Sleep(waitBetweenActionMsec);
            MouseEmulator.Click(clickButton, waitBetweenActionMsec);
        }
        
        /// <summary>
        /// 現在のマウスポインタの位置をクリックします
        /// </summary>
        /// <param name="clickButton"></param>
        /// <param name="waitBetweenActionMsec"></param>
        public static void Click(ClickButton clickButton = ClickButton.LeftButton, int waitBetweenActionMsec = 100)
        {
            MouseEventFlgs downFlg = 0;
            MouseEventFlgs upFlg = 0;

            if (clickButton.HasFlag(ClickButton.LeftButton))
            {
                downFlg |= MouseEventFlgs.MOUSEEVENTF_LEFTDOWN;
                upFlg |= MouseEventFlgs.MOUSEEVENTF_LEFTUP;
            }

            if (clickButton.HasFlag(ClickButton.RightButton))
            {
                downFlg |= MouseEventFlgs.MOUSEEVENTF_RIGHTDOWN;
                upFlg |= MouseEventFlgs.MOUSEEVENTF_RIGHTUP;
            }

            if (clickButton.HasFlag(ClickButton.MiddleButton))
            {
                downFlg |= MouseEventFlgs.MOUSEEVENTF_MIDDLEDOWN;
                upFlg |= MouseEventFlgs.MOUSEEVENTF_MIDDLEUP;
            }

            MouseEmulator.Click(downFlg, upFlg, waitBetweenActionMsec);
        }

        /// <summary>
        /// 指定された位置へマウスポインタを移動させます
        /// </summary>
        /// <param name="screanX"></param>
        /// <param name="screanY"></param>
        /// <exception cref="Exception"></exception>
        public static void MoveTo(int screanX, int screanY)
        {
            bool isSuccess = NativeMethods.SetCursorPos(screanX, screanY);
            if (!isSuccess)
            {
                throw new Exception("入力はすでに別のスレッドによってブロックされています");
            }
        }

        private static void Click(MouseEventFlgs downFlg, MouseEventFlgs upFlg, int waitBetweenActionMsec = 100)
        {
            INPUT inp = new INPUT();

            // (2)マウスのボタンを押す
            inp.type = InputFlgs.INPUT_MOUSE;
            inp.dUMMYUNIONNAME.mi.dwFlags = downFlg;

            // マウス操作実行
            var ret = NativeMethods.SendInput(1, new INPUT[] { inp }, Marshal.SizeOf(inp));

            if (ret == 0)
            {
                throw new Exception("入力はすでに別のスレッドによってブロックされています");
            }
            System.Threading.Thread.Sleep(waitBetweenActionMsec);

            inp = new INPUT();
            inp.type = InputFlgs.INPUT_MOUSE;
            // inp[0].mi.dwFlags = MouseEventFlgs.MOUSEEVENTF_LEFTUP | MouseEventFlgs.MOUSEEVENTF_ABSOLUTE;
            inp.dUMMYUNIONNAME.mi.dwFlags = MouseEventFlgs.MOUSEEVENTF_LEFTUP;

            // マウス操作実行
            ret = NativeMethods.SendInput(1, new INPUT[] { inp }, Marshal.SizeOf(inp));

            if (ret == 0)
            {
                throw new Exception("入力はすでに別のスレッドによってブロックされています");
            }
        }

    }
}
