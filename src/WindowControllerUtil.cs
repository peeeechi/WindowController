using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;


namespace WindowController
{
    public static class WindowControllerUtil
    {
        /// <summary>
        /// エラーメッセージを取得します
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static WinApiException GetError(uint errorCode, [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string targetFile = "")
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

        public static Regex ConvertWildcard2Regex(string keyword)
        {
            return new Regex($"^{keyword.Replace("*", ".*").Replace("?", ".")}$");
        }

        public static bool IsMatchTitle(IWindowController controller, string title)
        {
            if (controller == null) return false;                
            
            var regex = WindowControllerUtil.ConvertWildcard2Regex(title);
            return regex.IsMatch(controller.GetWindowText());
        }

        public static bool IsWithinRectRange(IWindowController controller, RECT compared)
        {
            if (controller == null) return false;                
            
            var target = controller.GetRect();

            var targetCenter = target.center;
            var comparedCenter = compared.center;
            var widthDiff = compared.width - target.width;
            var heightDiff = compared.height - target.height;
            var centerDiffX = Math.Abs(comparedCenter.x - targetCenter.x);
            var centerDiffY = Math.Abs(comparedCenter.y - targetCenter.y);

            return (widthDiff >= 0
                && heightDiff >= 0
                && centerDiffX <= widthDiff
                && centerDiffY <= heightDiff);
        }
    }
}
