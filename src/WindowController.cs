using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Threading.Thread;


namespace WindowController
{
    public interface IWindowController
    {
        /// <summary>
        /// Window Handle を取得します
        /// </summary>
        IntPtr HWnd { get; set; }
       
        /// <summary>
        /// クラス名を取得します
        /// </summary>
        /// <param name="hWnd">取得する</param>
        /// <param name="maxTextLength"></param>
        /// <returns></returns>
        string GetClassName(int maxTextLength = WindowControllerConst.MAX_TEXT_LENGTH);     

        /// <summary>
        /// Windowの矩形情報を取得します
        /// </summary>
        /// <returns></returns>
        RECT GetRect();

        /// <summary>
        /// このWindowのテキストを読み取ります
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        string GetWindowText(SendMessageTimeoutFlgs flags = SendMessageTimeoutFlgs.SMTO_BLOCK, UInt32 timeout = WindowControllerConst.TIMEOUT_MSEC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="timeout"></param>
        void SendClickMessage();

        void ClickEmulate(int? waitTimeMs = null);

        /// <summary>
        /// SendMessage によるテキスト入力を行います
        /// </summary>
        /// <param name="sendText"></param>
        /// <param name="flags"></param>
        /// <param name="timeout"></param>
        /// <exception cref="Exception"></exception>
        void SendSetTextMessage(string sendText, SendMessageTimeoutFlgs flags = SendMessageTimeoutFlgs.SMTO_NORMAL, UInt32 timeout = WindowControllerConst.TIMEOUT_MSEC);

        bool ToForeground();

        /// <summary>
        /// Window を起動しているプロセス名を取得
        /// </summary>
        /// <returns></returns>
        string GetProcessName();

        /// <summary>
        /// 自身のWindowHandleをもつWindowが存在する(=有効な場合)場合はtrue
        /// </summary>
        /// <returns></returns>
        bool Exist();

        WindowController GetParentWindow(GaFlags flags=GaFlags.GA_PARENT);

        WINDOWINFO GetWindowInfo();
    }

    public class WindowController: IWindowController
    {
        public WindowController() {}
        public WindowController(IntPtr hwnd) 
        {
            this._hWnd = hwnd;
        }


        protected IntPtr _hWnd = IntPtr.Zero;
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
        protected WinApiException GetError(uint errorCode, [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string targetFile = "")
        {
            StringBuilder st = new StringBuilder(500);
            uint errorMessageLength = NativeMethods.FormatMessage(FormatMessageFlgs.FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, errorCode, 0, st, (uint)st.Capacity, null);
            string message = "";
            if (errorMessageLength != 0)
            {
                message = $"{targetFile} line:{lineNumber} method: {methodName}{Environment.NewLine}{st.ToString()}";
                return new WinApiException(errorCode, message);
            }
            else
            {
                uint errorCode_ = NativeMethods.GetLastError();
                message = $"{targetFile} line:{lineNumber} method: {methodName}{Environment.NewLine}{st.ToString()}\r\n\tFormatMessage: {errorCode_,16}";
                return new WinApiException(errorCode, message);
            } 
        }

        /// <summary>
        /// クラス名を取得します
        /// </summary>
        /// <param name="hWnd">取得する</param>
        /// <param name="maxTextLength"></param>
        /// <returns></returns>
        public string GetClassName(int maxTextLength = WindowControllerConst.MAX_TEXT_LENGTH)
        {
            if (this._hWnd == IntPtr.Zero) return string.Empty;

            StringBuilder csb = new StringBuilder(maxTextLength);
            int retCode = NativeMethods.GetClassName(this._hWnd, csb, csb.Capacity);

            if (retCode == 0)
            {
                throw GetError(NativeMethods.GetLastError());
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
                throw GetError(NativeMethods.GetLastError());
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
        public string GetWindowText(SendMessageTimeoutFlgs flags = SendMessageTimeoutFlgs.SMTO_BLOCK, UInt32 timeout = WindowControllerConst.TIMEOUT_MSEC)
        {
            if (this._hWnd == IntPtr.Zero) return string.Empty;

            //var element = AutomationElement.FromHandle(this._hWnd);

            //var p = (ValuePattern)element.GetCurrentPattern(ValuePattern.Pattern);

            //return p.Current.Value;


            var ret = new IntPtr(0);
            int text_length = 0;
            var is_success = (int)NativeMethods.SendMessageTimeout(_hWnd, WindowsMessage.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero, flags, timeout, ref ret);
            // var is_success = (int)NativeMethods.SendNotifyMessage(_hWnd, WindowsMessage.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);

            //Debug.WriteLine($"ret: {ret}, return: {is_success}");

            if (is_success == 0)
            {
                throw GetError(NativeMethods.GetLastError());
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
                throw GetError(NativeMethods.GetLastError());
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
                throw GetError(NativeMethods.GetLastError());
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
        public void SendSetTextMessage(string sendText, SendMessageTimeoutFlgs flags = SendMessageTimeoutFlgs.SMTO_NORMAL, UInt32 timeout = WindowControllerConst.TIMEOUT_MSEC)
        {
            var ret = IntPtr.Zero;
            StringBuilder tsb = new StringBuilder(sendText);


            var retCode = NativeMethods.SendMessageTimeout(_hWnd, WindowsMessage.WM_SETTEXT, IntPtr.Zero, tsb, flags, timeout, ref ret);

            if (retCode.ToInt32() == 0)
            {
                throw GetError(NativeMethods.GetLastError());
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

        /// <summary>
        /// Window を起動しているプロセス名を取得
        /// </summary>
        /// <returns></returns>
        public string GetProcessName()
        {
            if (this.HWnd == IntPtr.Zero) return null;
            
            int threadId = NativeMethods.GetWindowThreadProcessId(this.HWnd, out UIntPtr pid);

            if (pid == UIntPtr.Zero) return null;

            var hnd = NativeMethods.OpenProcess((ProcessAccessRights.PROCESS_QUERY_INFORMATION|ProcessAccessRights.PROCESS_VM_READ), false, pid.ToUInt32());
            if (hnd == IntPtr.Zero)
            {
                throw GetError(NativeMethods.GetLastError());
            }

            try
            {
                var buffer = new StringBuilder(255);
                uint ret = NativeMethods.GetModuleBaseName(hnd, IntPtr.Zero, buffer, (uint)buffer.Capacity);
                if (ret == 0)
                {
                    throw GetError(NativeMethods.GetLastError());
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
            wi.cbSize = Marshal.SizeOf(wi);
            bool ret = NativeMethods.GetWindowInfo(_hWnd, ref wi);
            if (!ret)
            {
                throw GetError(NativeMethods.GetLastError());
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

        protected List<WindowController> FindChildren(Func<WindowController, bool> conditions)
        {
            List<WindowController> list = new List<WindowController>();
            if (this.HWnd == IntPtr.Zero)
                return list;
            

            EnumWindowsDelegate handler = null;
            handler = new EnumWindowsDelegate((hWnd, lpParam) => {
                if (hWnd == IntPtr.Zero) return true;
    
                var controller = new WindowController(hWnd);
                if (conditions(controller)) list.Add(controller);

                NativeMethods.EnumChildWindows(hWnd, handler, lpParam);    
                return true;
            });

            NativeMethods.EnumChildWindows(this.HWnd, handler, IntPtr.Zero);    

            return list;
        }

        public List<WindowController> FindChildrenByTitle(string title)
        {
            return this.FindChildren((controller) => WindowControllerUtil.IsMatchTitle(controller, title));
        }

        public List<WindowController> FindChildrenByRect(RECT rect, double scaleRate=1.0)
        {
            var target = new RECT
            {
                left = Convert.ToInt32((double)rect.left * scaleRate),
                top = Convert.ToInt32((double)rect.top * scaleRate),
                right = Convert.ToInt32((double)rect.right * scaleRate),
                bottom = Convert.ToInt32((double)rect.bottom * scaleRate),
            };

            return this.FindChildren((controller) => WindowControllerUtil.IsWithinRectRange(controller, rect));
        }
    }
}
