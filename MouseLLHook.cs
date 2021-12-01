using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowController
{
    public class MouseLLHook: IDisposable
    {
        public delegate void MouseButtonActionHandler(POINT p);
        public delegate void MouseWheelSpinHandler(int wheelAmount);

        public MouseLLHook()
        {
            _proc = new HOOKPROC(this.MouseHookCallback);
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                // メソッドをマウスのイベントに紐づける。
                _mouseHookId = NativeMethods.SetWindowsHookEx(HookType.WH_MOUSE_LL, _proc, NativeMethods.GetModuleHandle(currentModule.ModuleName), IntPtr.Zero);
            }
        }

        ~MouseLLHook()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: false);
        }

        private bool disposedValue;
        private IntPtr _mouseHookId = IntPtr.Zero;

        private HOOKPROC _proc;

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

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                // マウスのイベントに紐付けられた次のメソッドを実行する。メソッドがなければ処理終了。
                return NativeMethods.CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
            }

            MSLLHOOKSTRUCT mouseHookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
            MouseMessage mouseMessage = (MouseMessage)wParam;

            switch (mouseMessage)
            {
                case MouseMessage.WM_LBUTTONDOWN:
                    this.OnLeftButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_LBUTTONUP:
                    this.OnLeftButtonUp?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MOUSEMOVE:
                    this.OnMouseMove?.Invoke(mouseHookStruct.pt);
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
                    this.OnRightButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_RBUTTONUP:
                    this.OnRightButtonUp?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MBUTTONDOWN:
                    this.OnMiddleButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                case MouseMessage.WM_MBUTTONUP:
                    this.OnMiddleButtonDown?.Invoke(mouseHookStruct.pt);
                    break;
                default:
                    break;
            }

            return NativeMethods.CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、ファイナライザーをオーバーライドします
                // TODO: 大きなフィールドを null に設定します

                if (_mouseHookId != IntPtr.Zero)
                {
                    bool ret = NativeMethods.UnhookWindowsHookEx(this._mouseHookId);
                    if (!ret)
                    {
                        Console.WriteLine($"UnhookWindowsHookEx: {ret}");
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
