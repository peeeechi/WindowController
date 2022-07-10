using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Threading.Thread;


namespace WindowController
{
    public class WindowController
    {
        public WindowController() {}
        public WindowController(IntPtr hwnd) 
        {
            this._hWnd = hwnd;
        }
        const int MAX_TEXT_LENGTH = 500;
        const UInt32 TIMEOUT_MSEC = 1000;

        private IntPtr _hWnd = IntPtr.Zero;
        /// <summary>
        /// Window Handle を取得します
        /// </summary>
        public IntPtr HWnd
        { 
            get { return _hWnd; }
            set {_hWnd = value; }
        }

        /// <summary>
        /// エラーメッセージを取得します
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private string GetErrorString(uint errorCode, [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string targetFile = "")
        {
            StringBuilder st = new StringBuilder(500);
            uint errorMessageLength = NativeMethods.FormatMessage(FormatMessageFlgs.FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, errorCode, 0, st, (uint)st.Capacity, null);

            if (errorMessageLength != 0)
            {
                return $"{targetFile} line:{lineNumber} method: {methodName}{Environment.NewLine}{st.ToString()}";
            }
            else
            {
                uint errorCode_message = NativeMethods.GetLastError();
                return $"{targetFile} line:{lineNumber} method: {methodName}{Environment.NewLine}{st.ToString()}\r\n\tFormatMessage: {errorCode_message,16}";
            }
        }

        /// <summary>
        /// クラス名を取得します
        /// </summary>
        /// <param name="hWnd">取得する</param>
        /// <param name="maxTextLength"></param>
        /// <returns></returns>
        public string GetClassName(int maxTextLength = MAX_TEXT_LENGTH)
        {
            if (this._hWnd == IntPtr.Zero) return string.Empty;

            StringBuilder csb = new StringBuilder(maxTextLength);
            int retCode = NativeMethods.GetClassName(this._hWnd, csb, csb.Capacity);

            if (retCode == 0)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }

            return csb.ToString();
        }

        /// <summary>
        /// Windowの矩形情報を取得します
        /// </summary>
        /// <returns></returns>
        public RECT GetRect()
        {
            RECT ret = new RECT();
            // RECT ret;
            // if (_hWnd == IntPtr.Zero) return ret;
            bool isSuccess = NativeMethods.GetWindowRect(_hWnd, out ret);
            if (!isSuccess)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }
            return ret;
        }

        /// <summary>
        /// このWindowのテキストを読み取ります
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetWindowText(SendMessageTimeoutFlgs flags = SendMessageTimeoutFlgs.SMTO_BLOCK, UInt32 timeout = TIMEOUT_MSEC)
        {
            if (this._hWnd == IntPtr.Zero) return string.Empty;

            //var element = AutomationElement.FromHandle(this._hWnd);

            //var p = (ValuePattern)element.GetCurrentPattern(ValuePattern.Pattern);

            //return p.Current.Value;


            var ret = new IntPtr(0);
            int text_length = 0;
            var is_success = (int)NativeMethods.SendMessageTimeout(_hWnd, WindowsMessage.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero, flags, timeout, ref ret);

            //Debug.WriteLine($"ret: {ret}, return: {is_success}");

            if (is_success == 0)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
                // return null;
            }
            else
            {
                text_length = (int)ret + 1;
            }
            StringBuilder tsb = new StringBuilder(text_length);

            IntPtr retCode = NativeMethods.SendMessageTimeout(_hWnd, WindowsMessage.WM_GETTEXT, new IntPtr(text_length), tsb, flags, timeout, ref ret);

            if (is_success == 0)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }

            return tsb.ToString();


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="timeout"></param>
        public void SendClickMessage()
        {
            IntPtr ret = IntPtr.Zero;
            var retcode = NativeMethods.SendNotifyMessage(_hWnd, WindowsMessage.BM_CLICK, IntPtr.Zero, IntPtr.Zero);

            if (retcode.ToInt32() == 0)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }
        }

        public void ClickEmulate(int? waitTimeMs = null)
        {
            var rect = this.GetRect();

            if (waitTimeMs.HasValue)
            {
                Sleep(waitTimeMs.Value);
            }
            MouseEmulator.MoveAndClick(rect.center.x, rect.center.y);
        }

        /// <summary>
        /// SendMessage によるテキスト入力を行います
        /// </summary>
        /// <param name="sendText"></param>
        /// <param name="flags"></param>
        /// <param name="timeout"></param>
        /// <exception cref="Exception"></exception>
        public void SendSetTextMessage(string sendText, SendMessageTimeoutFlgs flags = SendMessageTimeoutFlgs.SMTO_NORMAL, UInt32 timeout = TIMEOUT_MSEC)
        {
            var ret = new IntPtr(0);
            StringBuilder tsb = new StringBuilder(sendText);


            var retCode = NativeMethods.SendMessageTimeout(_hWnd, WindowsMessage.WM_SETTEXT, IntPtr.Zero, tsb, flags, timeout, ref ret);

            if (retCode.ToInt32() == 0)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }

        }

        public bool ToForeground()
        {
            if (this.HWnd == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                return NativeMethods.SetForegroundWindow(this._hWnd);
            }
        }

        public string GetProcessName()
        {
            if (this.HWnd == IntPtr.Zero) return null;
            
            int threadId = NativeMethods.GetWindowThreadProcessId(this.HWnd, out UIntPtr pid);

            if (pid == UIntPtr.Zero) return null;

            // using (var process = Process.GetProcessById((int)pid.ToUInt32()))
            // {
            //     return process.ProcessName;                
            // }

            var hnd = NativeMethods.OpenProcess((ProcessAccessRights.PROCESS_QUERY_INFORMATION|ProcessAccessRights.PROCESS_VM_READ), false, pid.ToUInt32());
            if (hnd == IntPtr.Zero)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }

            try
            {
                var buffer = new StringBuilder(255);
                uint ret = NativeMethods.GetModuleBaseName(hnd, IntPtr.Zero, buffer, (uint)buffer.Capacity);
                if (ret == 0)
                {
                    throw new Exception(GetErrorString(NativeMethods.GetLastError()));
                }

                return buffer.ToString();
                // return buffer.ToString().ToLower();
            }
            finally
            {
                NativeMethods.CloseHandle(hnd);
            }
        }

        /// <summary>
        /// 自身のWindowHandleをもつWindowが存在する(=有効な場合)場合はtrue
        /// </summary>
        /// <returns></returns>
        public bool Exist()
        {
            return this._hWnd == IntPtr.Zero ? false : NativeMethods.IsWindow(this.HWnd);
        }

        public WindowController GetParentWindow(GaFlags flags=GaFlags.GA_PARENT)
        {
            var hwnd = NativeMethods.GetAncestor(this._hWnd, flags);

            if (hwnd == _hWnd || hwnd == IntPtr.Zero)
            {
                return null;
            }

            return new WindowController(hwnd);
        }

        public WINDOWINFO GetWindowInfo()
        {
            var wi = new WINDOWINFO();
            wi.cbSize = Marshal.SizeOf(wi);  // sizeof(WINDOWINFO);でもよいようだが sizeof()を使う場合は unsafe{}が必要
            bool ret = NativeMethods.GetWindowInfo(_hWnd, ref wi);
            if (!ret)
            {
                throw new Exception(GetErrorString(NativeMethods.GetLastError()));
            }
            return wi;
        }

        public static WindowController GetForegroundWindow()
        {
            var hWnd = NativeMethods.GetForegroundWindow();

            return (hWnd == IntPtr.Zero)? null : new WindowController(hWnd);
        }

        public static WindowController FindWindow(string title, string className=null)
        {
            var hWnd = NativeMethods.FindWindow(className, title);
            return (hWnd == IntPtr.Zero)? null : new WindowController(hWnd);
        }

        public static WindowController GetWindowFromPoint(POINT p)
        {
            var hWnd = NativeMethods.WindowFromPoint(p);

            if (hWnd == IntPtr.Zero) 
                return null;

            var ret = NativeMethods.ScreenToClient(hWnd, ref p);

            hWnd = NativeMethods.ChildWindowFromPoint(hWnd, p);

            return (hWnd == IntPtr.Zero) ? null : new WindowController(hWnd);
        }
    }
}
