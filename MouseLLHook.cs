using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowController
{
    public class MouseLLHook: Hook
    {
        public delegate void MouseButtonActionHandler(POINT p);
        public delegate void MouseWheelSpinHandler(int wheelAmount);

        public MouseLLHook(UInt32 threadId=0): base(HookType.WH_MOUSE_LL, threadId){}

        ~MouseLLHook()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            base.Dispose(disposing: false);
        }

        #region Events
        public event MouseButtonActionHandler OnLeftButtonDown;
        public event MouseButtonActionHandler OnLeftButtonUp;
        public event MouseButtonActionHandler OnRightButtonDown;
        public event MouseButtonActionHandler OnRightButtonUp;
        public event MouseButtonActionHandler OnMiddleButtonDown;
        public event MouseButtonActionHandler OnMiddleButtonUp;
        public event MouseButtonActionHandler OnMouseMove;
        public event MouseWheelSpinHandler OnWheelSpin;
        #endregion

        protected override IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                // マウスのイベントに紐付けられた次のメソッドを実行する。メソッドがなければ処理終了。
                return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
            }

            MSLLHOOKSTRUCT mouseHookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
            MouseMessage mouseMessage = (MouseMessage)wParam;

            // 解像度が変更されていた場合は正しい位置が取得できないため、
            // GetCursorPos を使用する
            POINT p = new POINT();
            NativeMethods.GetCursorPos(out p);

            switch (mouseMessage)
            {
                case MouseMessage.WM_LBUTTONDOWN:
                    this.OnLeftButtonDown?.Invoke(p);
                    // this.OnLeftButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_LBUTTONUP:
                    this.OnLeftButtonUp?.Invoke(p);
                    // this.OnLeftButtonUp?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MOUSEMOVE:
                   
                    this.OnMouseMove?.Invoke(p);
                    // this.OnMouseMove?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MOUSEWHEEL:
                    // 例
                    // ホイールの回転量はparam.mouseDataの値を見れば分かる。
                    // wheelAmountの値が2の場合、ホイールが上（手前から奥）へカクカクッと2段階回転したことを意味する。
                    // wheelAmountの値が-1の場合、ホイールが下（奥から手前）へカクッと1段階回転したことを意味する。
                    int wheelAmount = (mouseHookStruct.mouseData >> 16) / 120;
                    //this.OnWheelSpin?.Invoke(wheelAmount);
                    this.OnWheelSpin?.Invoke(mouseHookStruct.mouseData >> 16);

                    break;
                case MouseMessage.WM_RBUTTONDOWN:
                    this.OnRightButtonDown?.Invoke(p);
                    // this.OnRightButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_RBUTTONUP:
                    this.OnRightButtonUp?.Invoke(p);
                    // this.OnRightButtonUp?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MBUTTONDOWN:
                    this.OnMiddleButtonDown?.Invoke(p);
                    // this.OnMiddleButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MBUTTONUP:
                    this.OnMiddleButtonDown?.Invoke(p);
                    // this.OnMiddleButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                default:
                    break;
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
