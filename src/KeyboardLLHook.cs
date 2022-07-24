using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowController
{
    public class KeyboardLLHook: Hook
    {

        public delegate void KeyboardEventHandler(int code);


        public KeyboardLLHook(UInt32 threadId=0): base(HookType.WH_KEYBOARD_LL, threadId){}

        ~KeyboardLLHook()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            base.Dispose(disposing: false);
        }

        #region Events
        public event KeyboardEventHandler OnKeyDownEvent;
        public event KeyboardEventHandler OnKeyUpEvent;
        #endregion

        protected override IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                // マウスのイベントに紐付けられた次のメソッドを実行する。メソッドがなければ処理終了。
                return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
            }

            KeyboardInputEventFlg flg = (KeyboardInputEventFlg)Enum.ToObject(typeof(KeyboardInputEventFlg), wParam.ToInt32());

            switch (flg)
            {
                case(KeyboardInputEventFlg.WM_KEYDOWN):
                case(KeyboardInputEventFlg.WM_SYSKEYDOWN):
                    var kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                    var vkCode = (int)kb.vkCode;
                    OnKeyDownEvent?.Invoke(vkCode);
                    break;
                case(KeyboardInputEventFlg.WM_KEYUP):
                case(KeyboardInputEventFlg.WM_SYSKEYUP):
                    kb = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                    vkCode = (int)kb.vkCode;
                    OnKeyUpEvent?.Invoke(vkCode);
                    break;
                default:
                    break;
            }

            return NativeMethods.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }
    }
}
