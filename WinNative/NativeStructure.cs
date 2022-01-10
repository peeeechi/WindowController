using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowController
{

    /// <summary>
    /// POINT構造体は、ポイントのx座標とy座標を定義します
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/previous-versions/dd162805(v=vs.85)"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        /// <summary>
        /// ポイントのx座標
        /// </summary>
        public Int32 x;

        /// <summary>
        /// ポイントのy座標
        /// </summary>
        public Int32 y;

    }

    /// <summary>
    /// RECT構造は、左上隅と右下隅の座標によって矩形を定義します
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        /// <summary>
        /// 矩形の左上隅のx座標を指定します
        /// </summary>
        public Int32 left;
        /// <summary>
        /// 矩形の左上隅のy座標を指定します
        /// </summary>
        public Int32 top;
        /// <summary>
        /// 矩形の右下隅のx座標を指定します
        /// </summary>
        public Int32 right;
        /// <summary>
        /// 矩形の右下隅のy座標を指定します
        /// </summary>
        public Int32 bottom;
        /// <summary>
        /// 矩形の幅を取得します
        /// </summary>
        public Int32 width
        {
            get
            {
                return right - left;
            }
        }

        /// <summary>
        /// 矩形の高さを取得します
        /// </summary>
        public Int32 height
        {
            get
            {
                return bottom - top;
            }
        }
        /// <summary>
        /// 矩形の中心点を取得します
        /// </summary>
        public POINT center
        {
            get
            {
                return new POINT
                {
                    x = left + (width / 2),
                    y = top + (height / 2)
                };
            }
        }
    }

    /// <summary>
    /// ウィンドウ情報が含まれています
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-windowinfo"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        /// <summary>
        /// 構造体のサイズ（バイト単位）<br/>
        /// 呼び出し元は、このメンバーをsizeof（WINDOWINFO）に設定する必要があります
        /// </summary>
        public Int32 cbSize;
        /// <summary>
        /// ウィンドウの座標
        /// </summary>
        public RECT rcWindow;
        /// <summary>
        /// クライアントエリアの座標
        /// </summary>
        public RECT rcClient;
        /// <summary>
        /// ウィンドウスタイル
        /// </summary>
        //public UInt32 dwStyle;
        public WindowStyles dwStyle;

        /// <summary>
        /// 拡張ウィンドウスタイル<br/>
        /// 拡張ウィンドウスタイルの表については、拡張ウィンドウスタイルを参照してください<br/>
        /// </summary>
        //public UInt32 dwExStyle;
        public ExtendedWindowStyles dwExStyle;

        /// <summary>
        /// ウィンドウのステータス<br/>このメンバーがWS_ACTIVECAPTION(0x0001)の場合、ウィンドウはアクティブです<br/>それ以外の場合、このメンバーは(0x0000)です<br/>
        /// </summary>
        public UInt32 dwWindowStatus;

        /// <summary>
        /// ウィンドウの境界線の幅（ピクセル単位）
        /// </summary>
        public UInt32 cxWindowBorders;
        /// <summary>
        /// ウィンドウの境界線の高さ（ピクセル単位）
        /// </summary>
        public UInt32 cyWindowBorders;
        /// <summary>
        /// ウィンドウクラスアトム
        /// </summary>
        public UInt16 atomWindowType;
        /// <summary>
        /// ウィンドウを作成したアプリケーションのWindowsバージョン
        /// </summary>
        public UInt16 wCreatorVersion;
    }

    /// <summary>
    /// WH_MOUSEフックプロシージャMouseProcに渡されるマウスイベントに関する情報が含まれています<br/>
    /// "https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/ns-winuser-mousehookstruct?redirectedfrom=MSDN"
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCT
    {
        /// <summary>
        /// 画面座標でのカーソルのx座標とy座標
        /// </summary>
        public POINT pt;
        /// <summary>
        /// マウスイベントに対応するマウスメッセージを受信するウィンドウへのハンドル
        /// </summary>
        public int hwnd;
        /// <summary>
        /// ヒットテスト値。ヒットテスト値のリストについては、WM_NCHITTESTメッセージの説明を参照してください
        /// </summary>
        public int wHitTestCode;
        /// <summary>
        /// メッセージに関連する追加情報
        /// </summary>
        public UIntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEHOOKSTRUCTEX
    {
        public MOUSEHOOKSTRUCT mouseHookStruct;
        public int MouseData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public UIntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    /// <summary>
    /// CallWndRetProc のメッセージパラメータ<br/>
    /// "https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-cwpretstruct"
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CWPRETSTRUCT
    {
        /// <summary>
        /// メッセージ値で指定されたメッセージを処理したウィンドウプロシージャの戻り値
        /// </summary>
        public IntPtr lResult;
        /// <summary>
        /// メッセージに関する追加情報。正確な意味はメッセージ値によって異なります
        /// </summary>
        public IntPtr lParam;
        /// <summary>
        /// メッセージに関する追加情報。正確な意味はメッセージ値によって異なります
        /// </summary>
        public UIntPtr wParam;
        /// <summary>
        /// メッセージ
        /// </summary>
        public uint message;
        /// <summary>
        /// メッセージ値で指定されたメッセージを処理したウィンドウへのハンドル
        /// </summary>
        public IntPtr hwnd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CWPSTRUCT
    {
        public IntPtr lParam;
        public UIntPtr wParam;
        public uint message;
        public IntPtr hwnd;
    }

    /// <summary>
    /// WH_DEBUGのフックプロシージャへ渡されたデバック情報<br/>
    /// "https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/ns-winuser-debughookinfo?redirectedfrom=MSDN"
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DEBUGHOOKINFO
    {
        /// <summary>
        /// フィルタ関数を含むスレッドへのハンドル
        /// </summary>
        public UInt32 idThread;
        /// <summary>
        /// デバッグフィルター機能をインストールしたスレッドへのハンドル
        /// </summary>
        public UInt32 idThreadInstaller;
        /// <summary>
        /// DebugProcコールバック関数のlParamパラメーターでフックに渡される値
        /// </summary>
        public IntPtr lParam;
        /// <summary>
        /// DebugProcコールバック関数のwParamパラメーターでフックに渡される値
        /// </summary>
        public UIntPtr wParam;
        /// <summary>
        /// DebugProcコールバック関数のnCodeパラメーターでフックに渡される値
        /// </summary>
        public int code;
    }

    /// <summary>
    /// シミュレートされたマウスイベントに関する情報が含まれています<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput<br/>
    /// マウスイベント(mouse_eventの引数と同様のデータ)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        /// <summary>
        /// dwFlagsメンバーの値に応じて、マウスの絶対位置、または最後のマウスイベントが生成されてからのモーションの量<br/>絶対データは、マウスのx座標として指定されます<br/>相対データは、移動したピクセル数として指定されます
        /// </summary>
        public int dx;
        /// <summary>
        /// dwFlagsメンバーの値に応じて、マウスの絶対位置、または最後のマウスイベントが生成されてからのモーションの量<br/>絶対データは、マウスのy標として指定されます<br/>相対データは、移動したピクセル数として指定されます
        /// </summary>
        public int dy;
        /// <summary>
        /// dwFlagsにMOUSEEVENTF_WHEELが含まれている場合、mouseDataはホイールの動きの量を指定します<br/>正の値は、ホイールがユーザーから離れて前方に回転したことを示します<br/>負の値は、ホイールがユーザーに向かって後方に回転したことを示します<br/>ホイールの1回のクリックは、WHEEL_DELTAとして定義されます<br/>これは120です<br/>Windows Vista：dwFlagsにMOUSEEVENTF_HWHEELが含まれている場合、dwDataはホイールの動きの量を指定します<br/>正の値は、ホイールが右に回転したことを示します<br/>負の値は、ホイールが左に回転したことを示します<br/>ホイールの1回のクリックは、WHEEL_DELTAとして定義されます<br/>これは120です<br/>dwFlagsにMOUSEEVENTF_WHEEL、MOUSEEVENTF_XDOWN、またはMOUSEEVENTF_XUPが含まれていない場合、mouseDataはゼロである必要があります<br/>dwFlagsにMOUSEEVENTF_XDOWNまたはMOUSEEVENTF_XUPが含まれている場合、mouseDataはどのXボタンが押されたか離されたかを指定します<br/>この値は、次のフラグの任意の組み合わせにすることができます
        /// </summary>
        public uint mouseData;
        /// <summary>
        /// マウスの動きとボタンのクリックのさまざまな側面を指定するビットフラグのセット<br/>このメンバーのビットは、次の値の任意の適切な組み合わせにすることができます<br/>マウスボタンのステータスを指定するビットフラグは、進行中の状態ではなく、ステータスの変化を示すために設定されます<br/>たとえば、マウスの左ボタンを押したままにすると、最初に左ボタンを押したときにMOUSEEVENTF_LEFTDOWNが設定されますが、それ以降のモーションでは設定されません<br/>同様に、MOUSEEVENTF_LEFTUPは、ボタンが最初に離されたときにのみ設定されます<br/>MOUSEEVENTF_WHEELフラグとMOUSEEVENTF_XDOWNまたはMOUSEEVENTF_XUPフラグの両方をdwFlagsパラメーターで同時に指定することはできません<br/>どちらも、mouseDataフィールドを使用する必要があるためです
        /// </summary>
        public MouseEventFlgs dwFlags;
        /// <summary>
        /// イベントのタイムスタンプ（ミリ秒単位）<br/>このパラメータが0の場合、システムは独自のタイムスタンプを提供します
        /// </summary>
        public uint time;
        /// <summary>
        /// マウスイベントに関連付けられた追加の値<br/>アプリケーションはGetMessageExtraInfoを呼び出して、この追加情報を取得しま
        /// </summary>
        public UIntPtr dwExtraInfo;
        // public uint dwExtraInfo;
    }

    // キーボードイベント(keybd_eventの引数と同様のデータ)
    /// <summary>
    /// シミュレートされたキーボードイベントに関する情報が含まれています<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
    /// <br/> 備考: <br/>
    /// INPUT_KEYBOARDは、KEYEVENTF_UNICODEフラグを使用してテキスト入力されたかのように、手書き認識や音声認識などのキーボード入力以外の方法をサポートします<br/>場合KEYEVENTF_UNICODEが指定され、SendInputを送信WM_KEYDOWNまたはWM_KEYUPとフォアグラウンドスレッドのメッセージキューへのメッセージのwParamが等しくVK_PACKET<br/>一度のGetMessageまたはのPeekMessageはにメッセージを渡し、このメッセージを取得 TranslateMessageのポストWM_CHARの元々によって指定されたUnicode文字でメッセージをwScan<br/>このUnicode文字は、ANSIウィンドウに投稿されると、適切なANSI値に自動的に変換されます<br/>    KEYEVENTF_SCANCODEフラグを設定して、スキャンコードの観点からキーボード入力を定義します<br/>これは、現在使用されているキーボードに関係なく、物理的なキーストロークをシミュレートするのに役立ちます<br/>キーの仮想キー値は、現在のキーボードレイアウトまたは押された他のキーに応じて変わる可能性がありますが、スキャンコードは常に同じです
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        /// <summary>
        /// 仮想キーコード<br/>場合コードは254の範囲1内の値でなければならないdwFlagsパラメータのメンバを指定KEYEVENTF_UNICODE、wVkは0でなければなりません
        /// </summary>
        public short wVk;
        /// <summary>
        /// キーのハードウェアスキャンコード<br/>場合dwFlagsパラメータを指定KEYEVENTF_UNICODE、wScanは、フォアグラウンドアプリケーションに送信されるUnicode文字を指定します
        /// </summary>
        public short wScan;
        /// <summary>
        /// キーストロークのさまざまな側面を指定します
        /// </summary>
        public KeybdEventFlgs dwFlags;
        /// <summary>
        /// イベントのタイムスタンプ（ミリ秒単位）<br/>このパラメータがゼロの場合、システムは独自のタイムスタンプを提供します
        /// </summary>
        public int time;
        /// <summary>
        /// キーストロークに関連付けられた追加の値<br/>GetMessageExtraInfo関数を使用して、この情報を取得します
        /// </summary>
        public int dwExtraInfo;
    };

    // ハードウェアイベント
    /// <summary>
    /// キーボードまたはマウス以外の入力デバイスによって生成されたシミュレートされたメッセージに関する情報が含まれています<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-hardwareinput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        /// <summary>
        /// 入力ハードウェアによって生成されたメッセージ
        /// </summary>
        public int uMsg;
        /// <summary>
        /// 以下の下位ワードのlParamのためのパラメータuMsg
        /// </summary>
        public short wParamL;
        /// <summary>
        /// 上位ワードのlParamのためのパラメータuMsg
        /// </summary>
        public short wParamH;
    };

    // 各種イベント(SendInputの引数データ)
    //[StructLayout(LayoutKind.Sequential)]
    //public struct INPUT\
    //{
    //    /// <summary>
    //    /// 実態はuintだが、32bit/64bitでサイズが変わるため<br/>UIntPtr型を用いる必要がある
    //    /// </summary>
    //    [FieldOffset(0)] public uint type;
    //    [FieldOffset(8)] public MOUSEINPUT mi;
    //    [FieldOffset(8)] public KEYBDINPUT ki;
    //    [FieldOffset(8)] public HARDWAREINPUT hi;
    //    // [FieldOffset(4)] public MOUSEINPUT mi;
    //    // [FieldOffset(4)] public KEYBDINPUT ki;
    //    // [FieldOffset(4)] public HARDWAREINPUT hi;
    //};

    /// <summary>
    /// SendInputによって使用され、キーストローク、マウスの動き、マウスのクリックなどの入力イベントを合成するための情報を格納します<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        /// <summary>
        /// 入力イベントのタイプ
        /// 実態はuintだが、32bit/64bitでサイズが変わるため、UIntPtr型を用いる必要がある
        /// </summary>
        public UIntPtr type;
        public DUMMYUNIONNAME dUMMYUNIONNAME;
    };


    [StructLayout(LayoutKind.Explicit)] // Union
    public struct DUMMYUNIONNAME
    {
        /// <summary>
        /// シミュレートされたマウスイベントに関する情報
        /// </summary>
        [FieldOffset(0)]
        public MOUSEINPUT mi;
        /// <summary>
        /// シミュレートされたキーボードイベントに関する情報
        /// </summary>
        [FieldOffset(0)]
        public KEYBDINPUT ki;
        /// <summary>
        /// シミュレートされたハードウェアイベントに関する情報
        /// </summary>
        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    };

}
