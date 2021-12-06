using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowController
{
    public abstract class Hook: IDisposable
    {

        public Hook(HookType hookType, UInt32 threadId=0)
        {
            _proc = new HOOKPROC(this.HookCallback);
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                // メソッドをマウスのイベントに紐づける。
                _hookId = NativeMethods.SetWindowsHookEx(hookType, _proc, NativeMethods.GetModuleHandle(currentModule.ModuleName), threadId);
            }
        }

        ~Hook()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: false);
        }

        protected bool disposedValue;
        protected IntPtr _hookId = IntPtr.Zero;

        private HOOKPROC _proc;
        protected abstract IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam);

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

                if (_hookId != IntPtr.Zero)
                {
                    bool ret = NativeMethods.UnhookWindowsHookEx(this._hookId);
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
