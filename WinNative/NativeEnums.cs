using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowController
{
    /// <summary>
    /// マウスの動きとボタンのクリックのさまざまな側面を制御します<br/>このパラメーターは、次の値の特定の組み合わせにすることができます<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event
    /// </summary>
    [Flags]
    public enum MouseEventFlgs : UInt32
    {
        /// <summary>
        /// DX及び DYのパラメータが正規化された絶対座標を含みます<br/>設定されていない場合、これらのパラメーターには相対データ（最後に報告された位置以降の位置の変化）が含まれます<br/>このフラグは、システムに接続されているマウスまたはマウスのようなデバイスの種類に関係なく、設定することも設定しないこともできます<br/>マウスの相対的な動きの詳細については、次の「備考」セクションを参照してください<br/>
        /// </summary>
        MOUSEEVENTF_ABSOLUTE = 0x8000,
        /// <summary>
        /// 左ボタンが下がっています<br/>
        /// </summary>
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        /// <summary>
        /// 左ボタンが上がっています<br/>
        /// </summary>
        MOUSEEVENTF_LEFTUP = 0x0004,
        /// <summary>
        /// 真ん中のボタンが下がっています<br/>
        /// </summary>
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,
        /// <summary>
        /// 真ん中のボタンが上がっています<br/>
        /// </summary>
        MOUSEEVENTF_MIDDLEUP = 0x0040,
        /// <summary>
        /// 動きが発生しました<br/>
        /// </summary>
        MOUSEEVENTF_MOVE = 0x0001,
        /// <summary>
        /// 右ボタンが下がっています<br/>
        /// </summary>
        MOUSEEVENTF_RIGHTDOWN = 0x0008,
        /// <summary>
        /// 右ボタンが上がっています<br/>
        /// </summary>
        MOUSEEVENTF_RIGHTUP = 0x0010,
        /// <summary>
        /// マウスにホイールがある場合は、ホイールが移動しています<br/>移動量はdwDataで指定されます
        /// </summary>
        MOUSEEVENTF_WHEEL = 0x0800,
        /// <summary>
        /// Xボタンが押されました<br/>
        /// </summary>
        MOUSEEVENTF_XDOWN = 0x0080,
        /// <summary>
        /// Xボタンが離されました<br/>
        /// </summary>
        MOUSEEVENTF_XUP = 0x0100,
        /// <summary>
        /// ホイールボタンが傾いています<br/>
        /// </summary>
        MOUSEEVENTF_HWHEEL = 0x01000,
    }

    /// <summary>
    /// キーストロークのさまざまな側面を指定します。このメンバーは、次の値の特定の組み合わせにすることができます
    /// </summary>
    [Flags]
    public enum KeybdEventFlgs: uint
    {
        /// <summary>
        /// 指定した場合、スキャンコードの前に値0xE0（224）のプレフィックスバイトがあります。
        /// </summary>
        KEYEVENTF_EXTENDEDKEY = 0x0001,
        /// <summary>
        /// 指定した場合、キーは解放されます。指定しない場合、キーが押されています。
        /// </summary>
        KEYEVENTF_KEYUP = 0x0002,
        /// <summary>
        /// 指定した場合、wScanはキーを識別し、wVkは無視されます。
        /// </summary>
        KEYEVENTF_SCANCODE = 0x0008,
        /// <summary>
        /// 指定した場合、システムはVK_PACKETキーストロークを合成します。wVkのパラメータはゼロでなければなりません。このフラグは、KEYEVENTF_KEYUPフラグとのみ組み合わせることができます。詳細については、「備考」セクションを参照してください。
        /// </summary>
        KEYEVENTF_UNICODE = 0x0004,
    }

    public enum MouseMessage
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208
    }

    /// <summary>
    /// SendMessage, SendMessageTimeoutで使用するメッセージ<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues#windows-messages
    /// </summary>
    public enum WindowsMessage : UInt32
    {
        /// <summary>
        /// ウィンドウのテキストを設定します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-settext
        /// </summary>
        WM_SETTEXT = 0x000C,
        /// <summary>
        /// ウィンドウに対応するテキストを、呼び出し元によって提供されたバッファーにコピーします<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-gettext 
        /// </summary>
        WM_GETTEXT = 0x000D,
        /// <summary>
        /// ウィンドウに関連付けられたテキストの長さを文字数で決定します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/winmsg/wm-gettextlength 
        /// </summary>
        WM_GETTEXTLENGTH = 0x000E,
        /// <summary>
        /// ユーザーがボタンをクリックするのをシミュレートします<br/>
        /// このメッセージにより、ボタンはWM_LBUTTONDOWNおよびWM_LBUTTONUPメッセージを受信し、ボタンの親ウィンドウはBN_CLICKED通知コードを受信します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/controls/bm-click
        /// </summary>
        BM_CLICK = 0x00F5,
    }

    /// <summary>
    /// 以下はウィンドウスタイルです<br/>
    /// ウィンドウが作成された後は、特に明記されている場合を除き、これらのスタイルを変更することはできません<br/>
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/winmsg/window-styles"/>
    public enum WindowStyles : UInt32
    {
        /// <summary>
        ///  ウィンドウには細い線の境界線があります<br/>
        /// </summary>
        WS_BORDER = 0x00800000,
        /// <summary>
        ///  ウィンドウにはタイトルバーがあります（WS_BORDERスタイルを含みます）<br/>
        /// </summary>
        WS_CAPTION = 0x00C00000,
        /// <summary>
        ///  ウィンドウは子ウィンドウです<br/>このスタイルのウィンドウにはメニューバーを含めることはできません<br/>このスタイルは、WS_POPUPスタイルでは使用できません<br/>
        /// </summary>
        WS_CHILD = 0x40000000,
        /// <summary>
        ///  WS_CHILDスタイルと同じです<br/>
        /// </summary>
        WS_CHILDWINDOW = 0x40000000,
        /// <summary>
        ///  親ウィンドウ内で描画が行われるときに子ウィンドウが占める領域を除外します<br/>このスタイルは、親ウィンドウを作成するときに使用されます<br/>
        /// </summary>
        WS_CLIPCHILDREN = 0x02000000,
        /// <summary>
        ///  子ウィンドウを相互に関連してクリップします<br/>つまり、特定の子ウィンドウがWM_PAINTメッセージを受信すると、WS_CLIPSIBLINGSスタイルは、更新される子ウィンドウの領域から他のすべての重複する子ウィンドウをクリップします<br/>場合はWS_CLIPSIBLINGSが指定されていないと、子ウィンドウが重なって隣の子ウィンドウのクライアント領域内に描画するために、子ウィンドウのクライアント領域内に描画するとき、それは、可能です<br/>
        /// </summary>
        WS_CLIPSIBLINGS = 0x04000000,
        /// <summary>
        ///  ウィンドウは最初は無効になっています<br/>無効にされたウィンドウは、ユーザーからの入力を受け取ることができません<br/>ウィンドウの作成後にこれを変更するには、EnableWindow関数を使用します<br/>
        /// </summary>
        WS_DISABLED = 0x08000000,
        /// <summary>
        ///  ウィンドウには、ダイアログボックスで通常使用されるスタイルの境界線があります<br/>このスタイルのウィンドウには、タイトルバーを含めることはできません<br/>
        /// </summary>
        WS_DLGFRAME = 0x00400000,
        /// <summary>
        ///  ウィンドウは、コントロールのグループの最初のコントロールです<br/>グループは、この最初のコントロールとその後に定義されたすべてのコントロールで構成され、WS_GROUPスタイルの次のコントロールまで続きます<br/>各グループの最初のコントロールは通常、ユーザーがグループ間を移動できるようにWS_TABSTOPスタイルを持っています<br/>その後、ユーザーは方向キーを使用して、キーボードのフォーカスをグループ内の1つのコントロールからグループ内の次のコントロールに変更できます<br/>このスタイルのオンとオフを切り替えて、ダイアログボックスのナビゲーションを変更できます<br/>ウィンドウの作成後にこのスタイルを変更するには、SetWindowLong関数を使用します<br/>
        /// </summary>
        WS_GROUP = 0x00020000,
        /// <summary>
        ///  ウィンドウには水平スクロールバーがあります<br/>
        /// </summary>
        WS_HSCROLL = 0x00100000,
        /// <summary>
        ///  ウィンドウは最初は最小化されています<br/>WS_MINIMIZEスタイルと同じです<br/>
        /// </summary>
        WS_ICONIC = 0x20000000,
        /// <summary>
        ///  ウィンドウは最初に最大化されます<br/>
        /// </summary>
        WS_MAXIMIZE = 0x01000000,
        /// <summary>
        ///  ウィンドウには最大化ボタンがあります<br/>WS_EX_CONTEXTHELPスタイルと組み合わせることはできません<br/>WS_SYSMENUのスタイルも指定する必要があります<br/>
        /// </summary>
        WS_MAXIMIZEBOX = 0x00010000,
        /// <summary>
        ///  ウィンドウは最初は最小化されています<br/>WS_ICONICスタイルと同じです<br/>
        /// </summary>
        WS_MINIMIZE = 0x20000000,
        /// <summary>
        ///  ウィンドウには最小化ボタンがあります<br/>WS_EX_CONTEXTHELPスタイルと組み合わせることはできません<br/>WS_SYSMENUのスタイルも指定する必要があります<br/>
        /// </summary>
        WS_MINIMIZEBOX = 0x00020000,
        /// <summary>
        ///  ウィンドウはオーバーラップしたウィンドウです<br/>重なったウィンドウには、タイトルバーと境界線があります<br/>WS_TILEDスタイルと同じです<br/>
        /// </summary>
        WS_OVERLAPPED = 0x00000000,

        /// <summary>
        ///  ウィンドウはオーバーラップしたウィンドウです<br/>WS_TILEDWINDOWスタイルと同じです<br/>
        /// </summary>
        WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),

        /// <summary>
        ///  ウィンドウはポップアップウィンドウです<br/>このスタイルは、WS_CHILDスタイルでは使用できません<br/>
        /// </summary>
        WS_POPUP = 0x80000000,

        /// <summary>
        ///  ウィンドウはポップアップウィンドウです<br/>WS_CAPTIONとWS_POPUPWINDOWスタイルは、ウィンドウメニューが表示されるように組み合わせる必要があります<br/>
        /// </summary>
        WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU),
        /// <summary>
        ///  ウィンドウにはサイズの境界線があります<br/>WS_THICKFRAMEスタイルと同じです<br/>
        /// </summary>
        WS_SIZEBOX = 0x00040000,
        /// <summary>
        ///  ウィンドウのタイトルバーにはウィンドウメニューがあります<br/>WS_CAPTIONのスタイルも指定する必要があります<br/>
        /// </summary>
        WS_SYSMENU = 0x00080000,
        /// <summary>
        ///  ウィンドウは、ユーザーがTabキーを押したときにキーボードフォーカスを受け取ることができるコントロールです<br/>Tabキーを押すと、キーボードのフォーカスがWS_TABSTOPスタイルの次のコントロールに変わります<br/>このスタイルのオンとオフを切り替えて、ダイアログボックスのナビゲーションを変更できます<br/>ウィンドウの作成後にこのスタイルを変更するには、SetWindowLong関数を使用します<br/>ユーザーが作成したウィンドウとモードレスダイアログがタブストップで機能するようにするには、メッセージループを変更してIsDialogMessage関数を呼び出します<br/>
        /// </summary>
        WS_TABSTOP = 0x00010000,

        /// <summary>
        ///  ウィンドウにはサイズの境界線があります<br/>WS_SIZEBOXスタイルと同じです<br/>
        /// </summary>
        WS_THICKFRAME = 0x00040000,

        /// <summary>
        ///  ウィンドウはオーバーラップしたウィンドウです<br/>重なったウィンドウには、タイトルバーと境界線があります<br/>WS_OVERLAPPEDスタイルと同じです<br/>
        /// </summary>
        WS_TILED = 0x00000000,

        /// <summary>
        ///  ウィンドウはオーバーラップしたウィンドウです<br/>WS_OVERLAPPEDWINDOWスタイルと同じです<br/>
        /// </summary>
        WS_TILEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX),

        /// <summary>
        ///  ウィンドウは最初に表示されます<br/>このスタイルは、ShowWindowまたはSetWindowPos関数を使用してオンとオフを切り替えることができます<br/>
        /// </summary>
        WS_VISIBLE = 0x10000000,

        /// <summary>
        ///  ウィンドウには垂直スクロールバーがあります<br/>
        /// </summary>
        WS_VSCROLL = 0x00200000,
    }

    /// <summary>
    /// 以下は、拡張ウィンドウスタイルです<br/>
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/winmsg/extended-window-styles"/>
    public enum ExtendedWindowStyles : UInt32
    {
        /// <summary>
        ///  ウィンドウはドラッグアンドドロップファイルを受け入れます<br/>
        /// </summary>
        WS_EX_ACCEPTFILES = 0x00000010,
        /// <summary>
        ///  ウィンドウが表示されているときに、トップレベルウィンドウをタスクバーに強制します<br/>
        /// </summary>
        WS_EX_APPWINDOW = 0x00040000,
        /// <summary>
        ///  窓には縁がくぼんだ境界線があります<br/>
        /// </summary>
        WS_EX_CLIENTEDGE = 0x00000200,
        /// <summary>
        ///  ダブルバッファリングを使用して、ウィンドウのすべての子孫を下から上にペイントする順序でペイントします<br/>下から上へのペイント順序により、子孫ウィンドウに半透明（アルファ）および透明（カラーキー）効果を持たせることができますが、子孫ウィンドウにもWS_EX_TRANSPARENTビットが設定されている場合に限ります<br/>ダブルバッファリングにより、ウィンドウとその子孫をちらつきなくペイントできます<br/>ウィンドウのクラススタイルがCS_OWNDCまたはCS_CLASSDCの場合、これは使用できません<br/>Windows 2000：このスタイルはサポートされていません<br/>
        /// </summary>
        WS_EX_COMPOSITED = 0x02000000,
        /// <summary>
        ///  ウィンドウのタイトルバーには疑問符が含まれています<br/>ユーザーが疑問符をクリックすると、カーソルがポインター付きの疑問符に変わります<br/>その後、ユーザーが子ウィンドウをクリックすると、子はWM_HELPメッセージを受け取ります<br/>子ウィンドウはメッセージを親ウィンドウプロシージャに渡す必要があります<br/>親ウィンドウプロシージャは、HELP_WM_HELPコマンドを使用してWinHelp関数を呼び出す必要があります<br/>ヘルプアプリケーションは、通常、子ウィンドウのヘルプを含むポップアップウィンドウを表示します<br/>WS_EX_CONTEXTHELPは、WS_MAXIMIZEBOXまたはWS_MINIMIZEBOXスタイルでは使用できません<br/>
        /// </summary>
        WS_EX_CONTEXTHELP = 0x00000400,

        /// <summary>
        ///  ウィンドウ自体には、ダイアログボックスのナビゲーションに参加する必要のある子ウィンドウが含まれています<br/>このスタイルが指定されている場合、Tabキー、矢印キー、またはキーボードニーモニックの処理などのナビゲーション操作を実行すると、ダイアログマネージャーはこのウィンドウの子に戻ります<br/>
        /// </summary>
        WS_EX_CONTROLPARENT = 0x00010000,
        /// <summary>
        ///  ウィンドウには二重の境界線があります<br/>オプションで、dwStyleパラメーターでWS_CAPTIONスタイルを指定することにより、タイトルバーを使用してウィンドウを作成できます<br/>
        /// </summary>
        WS_EX_DLGMODALFRAME = 0x00000001,
        /// <summary>
        ///  ウィンドウは階層化されたウィンドウです<br/>ウィンドウのクラススタイルがCS_OWNDCまたはCS_CLASSDCの場合、このスタイルは使用できません<br/>Windowsの8：WS_EX_LAYEREDスタイルがトップレベルウィンドウと子ウィンドウのためにサポートされています<br/>以前のWindowsバージョンは、トップレベルウィンドウに対してのみWS_EX_LAYEREDをサポートします<br/>
        /// </summary>
        WS_EX_LAYERED = 0x00080000,
        /// <summary>
        ///  シェル言語がヘブライ語、アラビア語、または読み取り順序の配置をサポートする別の言語である場合、ウィンドウの水平方向の原点は右端にあります<br/>水平方向の値を大きくすると、左に進みます<br/>
        /// </summary>
        WS_EX_LAYOUTRTL = 0x00400000,
        /// <summary>
        ///  ウィンドウには、一般的な左揃えのプロパティがあります<br/>これがデフォルトです<br/>
        /// </summary>
        WS_EX_LEFT = 0x00000000,
        /// <summary>
        ///  シェル言語がヘブライ語、アラビア語、または読み取り順序の配置をサポートする別の言語の場合、垂直スクロールバー（存在する場合）はクライアント領域の左側にあります<br/>他の言語の場合、スタイルは無視されます<br/>
        /// </summary>
        WS_EX_LEFTSCROLLBAR = 0x00004000,
        /// <summary>
        ///  ウィンドウテキストは、左から右への読み取り順序プロパティを使用して表示されます<br/>これがデフォルトです<br/>
        /// </summary>
        WS_EX_LTRREADING = 0x00000000,
        /// <summary>
        ///  ウィンドウはMDI子ウィンドウです<br/>
        /// </summary>
        WS_EX_MDICHILD = 0x00000040,
        /// <summary>
        ///  このスタイルで作成されたトップレベルウィンドウは、ユーザーがクリックしてもフォアグラウンドウィンドウにはなりません<br/>ユーザーがフォアグラウンドウィンドウを最小化または閉じたときに、システムはこのウィンドウをフォアグラウンドに移動しません<br/>ウィンドウは、プログラムによるアクセスや、ナレーターなどのアクセシブルなテクノロジーによるキーボードナビゲーションを介してアクティブ化しないでください<br/>ウィンドウをアクティブにするには、SetActiveWindowまたはSetForegroundWindow関数を使用します<br/>デフォルトでは、ウィンドウはタスクバーに表示されません<br/>ウィンドウをタスクバーに強制的に表示するには、WS_EX_APPWINDOWスタイルを使用します<br/>
        /// </summary>
        WS_EX_NOACTIVATE = 0x08000000,
        /// <summary>
        ///  ウィンドウは、ウィンドウレイアウトを子ウィンドウに渡しません<br/>
        /// </summary>
        WS_EX_NOINHERITLAYOUT = 0x00100000,
        /// <summary>
        ///  このスタイルで作成された子ウィンドウは、作成または破棄されたときに、WM_PARENTNOTIFYメッセージを親ウィンドウに送信しません<br/>
        /// </summary>
        WS_EX_NOPARENTNOTIFY = 0x00000004,
        /// <summary>
        ///  ウィンドウはリダイレクトサーフェスにレンダリングされません<br/>これは、目に見えるコンテンツがないウィンドウ、またはサーフェス以外のメカニズムを使用してビジュアルを提供するウィンドウ用です<br/>
        /// </summary>
        WS_EX_NOREDIRECTIONBITMAP = 0x00200000,
        /// <summary>
        ///  ウィンドウはオーバーラップしたウィンドウです
        /// </summary>
        WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
        /// <summary>
        ///  ウィンドウはパレットウィンドウで、コマンドの配列を表示するモードレスダイアログボックスです<br/>
        /// </summary>
        WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
        /// <summary>
        ///  ウィンドウには、一般的な「右揃え」プロパティがあります<br/>これはウィンドウクラスによって異なります<br/>このスタイルは、シェル言語がヘブライ語、アラビア語、または読み順の配置をサポートする別の言語である場合にのみ効果があります<br/>それ以外の場合、スタイルは無視されます<br/>静的コントロールまたは編集コントロールにWS_EX_RIGHTスタイルを使用すると、それぞれSS_RIGHTまたはES_RIGHTスタイルを使用した場合と同じ効果があります<br/>このスタイルをボタンコントロールで使用すると、BS_RIGHTおよびBS_RIGHTBUTTONスタイルを使用するのと同じ効果があります<br/>
        /// </summary>
        WS_EX_RIGHT = 0x00001000,
        /// <summary>
        ///  垂直スクロールバー（存在する場合）は、クライアント領域の右側にあります<br/>これがデフォルトです<br/>
        /// </summary>
        WS_EX_RIGHTSCROLLBAR = 0x00000000,
        /// <summary>
        ///  シェル言語がヘブライ語、アラビア語、または読み順の配置をサポートする別の言語の場合、ウィンドウのテキストは右から左への読み順のプロパティを使用して表示されます<br/>他の言語の場合、スタイルは無視されます<br/>
        /// </summary>
        WS_EX_RTLREADING = 0x00002000,
        /// <summary>
        ///  ウィンドウには、ユーザー入力を受け入れないアイテムに使用することを目的とした3次元の境界線スタイルがあります<br/>
        /// </summary>
        WS_EX_STATICEDGE = 0x00020000,
        /// <summary>
        ///  このウィンドウは、フローティングツールバーとして使用することを目的としています<br/>ツールウィンドウには、通常のタイトルバーよりも短いタイトルバーがあり、ウィンドウのタイトルは小さいフォントを使用して描画されます<br/>ツールウィンドウは、タスクバーまたはユーザーがALT + TABを押したときに表示されるダイアログには表示されません<br/>ツールウィンドウにシステムメニューがある場合、そのアイコンはタイトルバーに表示されません<br/>ただし、右クリックするか、ALT + SPACEと入力すると、システムメニューを表示できます<br/>
        /// </summary>
        WS_EX_TOOLWINDOW = 0x00000080,
        /// <summary>
        ///  ウィンドウは、最上位以外のすべてのウィンドウの上に配置し、ウィンドウが非アクティブ化されている場合でも、その上にとどまる必要があります<br/>このスタイルを追加または削除するには、SetWindowPos関数を使用します<br/>
        /// </summary>
        WS_EX_TOPMOST = 0x00000008,
        /// <summary>
        ///  ウィンドウの下の兄弟（同じスレッドによって作成されたもの）がペイントされるまで、ウィンドウはペイントされるべきではありません<br/>下にある兄弟ウィンドウのビットがすでにペイントされているため、ウィンドウは透明に見えます<br/>これらの制限なしに透過性を実現するには、SetWindowRgn関数を使用します<br/>
        /// </summary>
        WS_EX_TRANSPARENT = 0x00000020,
        /// <summary>
        ///  ウィンドウには、隆起したエッジのある境界線があります<br/>
        /// </summary>
        WS_EX_WINDOWEDGE = 0x00000100,
    }

    public enum GaFlags : UInt32
    {
        /// <summary>
        /// 親ウィンドウを取得します<br/> GetParent関数の場合のように、これには所有者は含まれません<br/>
        /// </summary>
        GA_PARENT = 0x0001,
        /// <summary>
        /// 親ウィンドウのチェーンをたどってルートウィンドウを取得します<br/>
        /// </summary>
        GA_ROOT = 0x0002,
        /// <summary>
        /// GetParentによって返された親ウィンドウと所有者ウィンドウのチェーンをたどって、所有されているルートウィンドウを取得します<br/>
        /// </summary>
        GA_ROOTOWNER = 0x0003
    }

    public enum SendMessageTimeoutFlgs : UInt32
    {
        /// <summary>
        /// 受信スレッドが応答しない、または「ハング」しているように見える場合、関数はタイムアウト期間が経過するのを待たずに戻ります<br/>
        /// </summary>
        SMTO_ABORTIFHUNG = 0x0002,

        /// <summary>
        /// 関数が戻るまで、呼び出し元のスレッドが他の要求を処理しないようにします<br/>
        /// </summary>
        SMTO_BLOCK = 0x0001,

        /// <summary>
        /// 呼び出し元のスレッドは、関数が戻るのを待っている間、他の要求を処理することを妨げられません<br/>
        /// </summary>
        SMTO_NORMAL = 0x0000,

        /// <summary>
        /// この関数は、受信スレッドがメッセージを処理している限り、タイムアウト期間を強制しません<br/>
        /// </summary>
        SMTO_NOTIMEOUTIFNOTHUNG = 0x0008,

        /// <summary>
        /// メッセージの処理中に受信ウィンドウが破壊された場合、または所有しているスレッドが停止した場合、関数は0を返す必要があります<br/>
        /// </summary>
        SMTO_ERRORONEXIT = 0x0020,

    }

    ///// <summary>
    ///// endInputによって使用され、キーストローク、マウスの動き、マウスのクリックなどの入力イベントを合成するための情報を格納します<br/>
    ///// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
    ///// </summary>
    //[Flags]
    ////public enum InputFlgs : UIntPtr
    //{
    //    /// <summary>
    //    /// イベントはマウスイベントです<br/>ユニオンのmi構造を使用します<br/>
    //    /// </summary>
    //    INPUT_MOUSE = 0,
    //    /// <summary>
    //    /// イベントはキーボードイベントです<br/>ユニオンのki構造を使用します<br/>
    //    /// </summary>
    //    INPUT_KEYBOARD = 1,
    //    /// <summary>
    //    /// イベントはハードウェアイベントです<br/>ユニオンのhi構造を使用します<br/>
    //    /// </summary>
    //    INPUT_HARDWARE = 2,
    //}


    /// <summary>
    /// endInputによって使用され、キーストローク、マウスの動き、マウスのクリックなどの入力イベントを合成するための情報を格納します<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input<br/>
    /// 実態はuintだが、32bit/64bitでサイズが変わるため。UIntPtr型を用いる必要がある
    /// </summary>    
    public static class InputFlgs
    {
        /// <summary>
        /// イベントはマウスイベントです<br/>ユニオンのmi構造を使用します<br/>
        /// </summary>
        public static readonly UIntPtr INPUT_MOUSE = new UIntPtr(0);

        /// <summary>
        /// イベントはキーボードイベントです<br/>ユニオンのki構造を使用します<br/>
        /// </summary>
        public static readonly UIntPtr INPUT_KEYBOARD = new UIntPtr(1);
        /// <summary>
        /// イベントはハードウェアイベントです<br/>ユニオンのhi構造を使用します<br/>
        /// </summary>
        public static readonly UIntPtr INPUT_HARDWARE = new UIntPtr(2);
    }

    public enum WindowLongParam
    {
        GWL_WNDPROC = -4,
        GWLP_HINSTANCE = -6,
        GWLP_HWNDPARENT = -8,
        GWL_ID = -12,
        GWL_STYLE = -16,
        GWL_EXSTYLE = -20,
        GWL_USERDATA = -21,
        DWLP_MSGRESULT = 0,
        DWLP_USER = 8,
        DWLP_DLGPROC = 4
    }

    [Flags]
    public enum SetWindowPosFlags : UInt32
    {
        SWP_ASYNCWINDOWPOS = 0x4000,
        SWP_DEFERERASE = 0x2000,
        SWP_DRAWFRAME = 0x0020,
        SWP_FRAMECHANGED = 0x0020,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOACTIVATE = 0x0010,
        SWP_NOCOPYBITS = 0x0100,
        SWP_NOMOVE = 0x0002,
        SWP_NOOWNERZORDER = 0x0200,
        SWP_NOREDRAW = 0x0008,
        SWP_NOREPOSITION = 0x0200,
        SWP_NOSENDCHANGING = 0x0400,
        SWP_NOSIZE = 0x0001,
        SWP_NOZORDER = 0x0004,
        SWP_SHOWWINDOW = 0x0040,
    }

    public enum HCBT : Int32
    {
        HCBT_MOVESIZE = 0,
        HCBT_MINMAX = 1,
        HCBT_QS = 2,
        HCBT_CREATEWND = 3,
        HCBT_DESTROYWND = 4,
        HCBT_ACTIVATE = 5,
        HCBT_CLICKSKIPPED = 6,
        HCBT_KEYSKIPPED = 7,
        HCBT_SYSCOMMAND = 8,
        HCBT_SETFOCUS = 9,
    }

    /// <summary>
    /// インストールするフック手順のタイプ<br/>このパラメーターは、次のいずれかの値になります<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
    /// </summary>
    public enum HookType : Int32
    {
        /// <summary>
        /// システムがメッセージを宛先ウィンドウプロシージャに送信する前にメッセージを監視するフックプロシージャをインストールします<br/>詳細については、CallWndProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_CALLWNDPROC = 4,
        /// <summary>
        /// メッセージが宛先ウィンドウプロシージャによって処理された後にメッセージを監視するフックプロシージャをインストールします<br/>詳細については、CallWndRetProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_CALLWNDPROCRET = 12,
        /// <summary>
        /// CBTアプリケーションに役立つ通知を受信するフックプロシージャをインストールします<br/>詳細については、CBTProcフック手順を参照してください<br/>
        /// </summary>
        WH_CBT = 5,
        /// <summary>
        /// 他のフックプロシージャのデバッグに役立つフックプロシージャをインストールします<br/>詳細については、DebugProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_DEBUG = 9,
        /// <summary>
        /// アプリケーションのフォアグラウンドスレッドがアイドル状態になりそうなときに呼び出されるフックプロシージャをインストールします<br/>このフックは、アイドル時間中に優先度の低いタスクを実行する場合に役立ちます<br/>詳細については、ForegroundIdleProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_FOREGROUNDIDLE = 11,
        /// <summary>
        /// メッセージキューに投稿されたメッセージを監視するフックプロシージャをインストールします<br/>詳細については、GetMsgProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_GETMESSAGE = 3,
        /// <summary>
        /// WH_JOURNALRECORDフックプロシージャ によって以前に記録されたメッセージを投稿するフックプロシージャをインストールします<br/>詳細については、JournalPlaybackProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_JOURNALPLAYBACK = 1,
        /// <summary>
        /// システムメッセージキューに投稿された入力メッセージを記録するフックプロシージャをインストールします<br/>このフックは、マクロの記録に役立ちます<br/>詳細については、JournalRecordProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_JOURNALRECORD = 0,
        /// <summary>
        /// キーストロークメッセージを監視するフックプロシージャをインストールします<br/>詳細については、KeyboardProcフック手順を参照してください<br/>
        /// </summary>
        WH_KEYBOARD = 2,
        /// <summary>
        /// 低レベルのキーボード入力イベントを監視するフックプロシージャをインストールします<br/>詳細については、LowLevelKeyboardProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_KEYBOARD_LL = 13,
        /// <summary>
        /// マウスメッセージを監視するフックプロシージャをインストールします<br/>詳細については、MouseProcフック手順を参照してください<br/>
        /// </summary>
        WH_MOUSE = 7,
        /// <summary>
        /// 低レベルのマウス入力イベントを監視するフックプロシージャをインストールします<br/>詳細については、LowLevelMouseProcフックプロシージャを参照してください<br/>
        /// </summary>
        WH_MOUSE_LL = 14,
        /// <summary>
        /// ダイアログボックス、メッセージボックス、メニュー、またはスクロールバーでの入力イベントの結果として生成されたメッセージを監視するフックプロシージャをインストールします<br/>詳細については、MessageProcフック手順を参照してください<br/>
        /// </summary>
        WH_MSGFILTER = -1,
        /// <summary>
        /// シェルアプリケーションに役立つ通知を受け取るフックプロシージャをインストールします<br/>詳細については、ShellProcフック手順を参照してください<br/>
        /// </summary>
        WH_SHELL = 10,
        /// <summary>
        /// ダイアログボックス、メッセージボックス、メニュー、またはスクロールバーでの入力イベントの結果として生成されたメッセージを監視するフックプロシージャをインストールします<br/>フックプロシージャは、呼び出し元のスレッドと同じデスクトップ内のすべてのアプリケーションについて、これらのメッセージを監視します<br/>詳細については、SysMsgProcフック手順を参照してください<br/>
        /// </summary>
        WH_SYSMSGFILTER = 6,
    }

    /// <summary>
    /// 書式設定オプション、およびlpSourceパラメーターの解釈方法<br/>dwFlagsの下位バイトは、関数が出力バッファーの改行を処理する方法を指定します<br/>下位バイトは、フォーマットされた出力行の最大幅を指定することもできます<br/>このパラメーターは、次の1つ以上の値にすることができます<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage
    /// </summary>
    [Flags]
    public enum FormatMessageFlgs
    {
        /// <summary>
        /// この関数は、フォーマットされたメッセージを保持するのに十分な大きさのバッファーを割り当て、割り当てられたバッファーへのポインターをlpBufferで指定されたアドレスに配置します<br/> lpBufferのパラメータは、へのポインタであるLPTSTR<br/>ポインタをLPTSTRにキャストする必要があります（たとえば (LPTSTR)&lpBuffer）<br/>nsizeの場合の パラメータは、最小数の指定TCHAR単位の出力メッセージ・バッファに割り当てます<br/>呼び出し元は、LocalFree 関数を使用して、バッファーが不要になったときにバッファーを解放する必要があります<br/>フォーマットされたメッセージの長さが128Kバイトを超える と、FormatMessageは失敗し、その後GetLastErrorを呼び出すと ERROR_MORE_DATAが返されます<br/>以前のバージョンのWindowsでは、この値はWindowsストアアプリのコンパイル時に使用できませんでした<br/>Windows 10以降、この値を使用できます<br/>Windows Server2003およびWindowsXP：<br/>フォーマットされたメッセージの長さが128Kバイトを超える場合、 FormatMessageはERROR_MORE_DATAのエラーで自動的に失敗しません<br/>
        /// </summary>
        FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,
        /// <summary>
        /// 引数パラメータがありませんたva_list 構造が、引数を表す値の配列へのポインタです<br/>このフラグは、64ビット整数値では使用できません<br/>64ビット整数を使用している場合は、va_list構造体を使用する必要があります<br/>
        /// </summary>
        FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000,
        /// <summary>
        /// lpSourceのパラメータは、検索するメッセージ・テーブル・リソース（単数または複数）を含むモジュールハンドルです<br/>このlpSourceハンドルがNULLの場合、現在のプロセスのアプリケーションイメージファイルが検索されます<br/>このフラグは、FORMAT_MESSAGE_FROM_STRINGと一緒に使用することはできません <br/>モジュールにメッセージテーブルリソースがない場合、関数はERROR_RESOURCE_TYPE_NOT_FOUNDで失敗し ます<br/>
        /// </summary>
        FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,
        /// <summary>
        /// lpSourceのパラメータは、メッセージ定義が含まれている、NULLで終わる文字列へのポインタです<br/>メッセージテーブルリソースのメッセージテキストと同様に、メッセージ定義には挿入シーケンスを含めることができます<br/>このフラグは、FORMAT_MESSAGE_FROM_HMODULEまたは FORMAT_MESSAGE_FROM_SYSTEMと一緒に使用することはできません <br/>
        /// </summary>
        FORMAT_MESSAGE_FROM_STRING = 0x00000400,
        /// <summary>
        /// 関数は、システムメッセージテーブルリソースで要求されたメッセージを検索する必要があります<br/>このフラグがFORMAT_MESSAGE_FROM_HMODULEで指定されている場合、lpSourceで指定されたモジュールにメッセージが見つからない場合、関数はシステムメッセージテーブルを検索します<br/>このフラグは、FORMAT_MESSAGE_FROM_STRINGと一緒に使用することはできません<br/>このフラグが指定されている場合、アプリケーションはGetLastError関数の結果を渡して、システム定義エラーのメッセージテキストを取得できます<br/>
        /// </summary>
        FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,
        /// <summary>
        /// %1などのメッセージ定義への挿入シーケンスは無視され、変更されずに出力バッファに渡されます<br/>このフラグは、後でフォーマットするためにメッセージをフェッチするのに役立ちます<br/>このフラグが設定されている場合、 Argumentsパラメーターは無視されます<br/>
        /// </summary>
        FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,
        /// <summary>
        /// 出力線幅の制限はありません<br/>この関数は、メッセージ定義テキストにある改行を出力バッファーに保管します<br/>
        /// </summary>
        FORMAT_MESSAGE_MAX_WIDTH_NO_MASK = 0x00000000,
        /// <summary>
        /// この関数は、メッセージ定義テキストの通常の改行を無視します<br/>この関数は、ハードコードされた改行をメッセージ定義テキストに出力バッファーに格納します<br/>この関数は改行を生成しません<br/>
        /// 下位バイトがFORMAT_MESSAGE_MAX_WIDTH_MASK以外のゼロ以外の値である場合 、出力行の最大文字数を指定します<br/>この関数は、メッセージ定義テキストの通常の改行を無視します<br/>この関数は、空白で区切られた文字列を改行で分割することはありません<br/>この関数は、ハードコードされた改行をメッセージ定義テキストに出力バッファーに格納します<br/>ハードコードされた改行は、％nエスケープシーケンスでコード化されます
        /// </summary>
        FORMAT_MESSAGE_MAX_WIDTH_MASK = 0x000000FF,
    }
}
