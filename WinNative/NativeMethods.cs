using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowController
{

    /// <summary>
    /// User32.dllのラップ関数群
    /// </summary>
    public static class NativeMethods
    {
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32", CharSet =CharSet.Auto, SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = false)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);


        /// <summary>
        /// 画面座標でのマウスカーソルの位置を取得します
        /// </summary>
        /// <param name="lpPoint">カーソルの画面座標を受け取るPOINT構造体へのポインター</param>
        /// <returns>成功した場合はゼロ以外を返し、そうでない場合はゼロを返します<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します</returns>
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos([Out] out POINT lpPoint);

        /// <summary>
        /// ウィンドウをアクティブにします<br/>ウィンドウは、呼び出し元のスレッドのメッセージキューに接続する必要があります<br/>
        /// 備考: <br/>
        /// SetActiveWindow関数は、アプリケーションがバックグラウンドにある場合はウィンドウをアクティブにしません<br/>
        /// ウィンドウは、システムがウィンドウをアクティブにしたときに、そのアプリケーションがフォアグラウンドにある場合、前面(Z オーダーの上部) に表示されます<br/>
        /// hWndパラメータで識別されたウィンドウが呼び出し元のスレッドによって作成された場合、呼び出しスレッドのアクティブ ウィンドウの状態はhWndに設定されます<br/>
        /// それ以外の場合、呼び出し元スレッドのアクティブ ウィンドウの状態はNULLに設定されます
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setactivewindow"/> 
        /// <param name="hWnd">アクティブ化するトップレベルウィンドウへのハンドル</param>
        /// <returns>関数が成功した場合、戻り値は以前アクティブだったウィンドウへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError =true)]
        public static extern IntPtr SetActiveWindow([In] IntPtr hWnd);

        /// <summary>
        /// 指定されたポイントを含むウィンドウへのハンドルを取得します<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-windowfrompoint"/>
        /// <param name="point">チェックするポイント</param>
        /// <returns>戻り値は、ポイントを含むウィンドウへのハンドルです<br/>
        /// 指定されたポイントにウィンドウが存在しない場合、戻り値はNULLです<br/>
        /// ポイントが静的テキストコントロール上にある場合、戻り値は静的テキストコントロールの下のウィンドウへのハンドルです</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr WindowFromPoint([In] POINT point);

        /// <summary>
        /// クラス名とウィンドウ名が指定された文字列と一致するウィンドウへのハンドルを取得します<br/>この関数は、指定された子ウィンドウの次のウィンドウから始めて、子ウィンドウを検索します<br/>この関数は、大文字と小文字を区別する検索を実行しません<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowexa"/>
        /// <param name="hWndParent">子ウィンドウが検索される親ウィンドウへのハンドル<br/>hwndParentがNULLの場合、関数はデスクトップウィンドウを親ウィンドウとして使用します<br/>この関数は、デスクトップの子ウィンドウであるウィンドウを検索します<br/>hwndParentがHWND_MESSAGEの場合、関数はすべてのメッセージのみのウィンドウを検索します<br/></param>
        /// <param name="hWndChildAfter">子ウィンドウへのハンドル<br/>検索は、Zオーダーの次の子ウィンドウから始まります<br/>子ウィンドウは、子孫ウィンドウだけでなく、hwndParentの直接の子ウィンドウである必要があります<br/>        hwndChildAfterがNULLの場合、検索はhwndParentの最初の子ウィンドウから始まります<br/></param>
        /// <param name="lpszClass">RegisterClassまたはRegisterClassEx関数への以前の呼び出しによって作成されたクラス名またはクラスアトム<br/>アトムは、lpszClassの下位ワードに配置する必要があります<br/>上位ワードはゼロでなければなりません<br/>        lpszClassが文字列の場合、ウィンドウクラス名を指定します<br/>クラス名は、RegisterClassまたはRegisterClassExに登録されている任意の名前、または事前定義された制御クラス名のいずれか、またはMAKEINTATOM（0x8000）にすることができます<br/>この後者の場合、0x8000はメニュークラスのアトムです<br/>詳細については、このトピックの「備考」セクションを参照してください<br/></param>
        /// <param name="lpszWindow">ウィンドウ名（ウィンドウのタイトル）<br/>このパラメーターがNULLの場合、すべてのウィンドウ名が一致します<br/></param>
        /// <returns>関数が成功した場合、戻り値は、指定されたクラス名とウィンドウ名を持つウィンドウへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx([In] IntPtr hWndParent, [In] IntPtr hWndChildAfter, [MarshalAs(UnmanagedType.LPWStr), In] string lpszClass, [MarshalAs(UnmanagedType.LPWStr), In] string lpszWindow);
        // public static extern IntPtr FindWindowExA([In]IntPtr hWndParent, [In]IntPtr hWndChildAfter, [In]string lpszClass, [In]string lpszWindow);

        /// <summary>
        /// 指定されたウィンドウの祖先へのハンドルを取得します<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getancestor"/>
        /// <param name="hWnd">祖先を取得するウィンドウへのハンドル<br/>このパラメータがデスクトップウィンドウの場合、関数はNULLを返します<br/></param>
        /// <param name="gaFlags">取得する祖先<br/>このパラメーターは、次のいずれかの値になります<br/></param>
        /// <returns>戻り値は、祖先ウィンドウへのハンドルです<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetAncestor([In] IntPtr hWnd, [In] GaFlags gaFlags);

        /// <summary>
        /// フォアグラウンドウィンドウ（ユーザーが現在作業しているウィンドウ）へのハンドルを取得します<br/>システムは、フォアグラウンドウィンドウを作成するスレッドに、他のスレッドよりもわずかに高い優先度を割り当てます
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow"/>
        /// <returns>戻り値は、フォアグラウンドウィンドウへのハンドルです<br/>ウィンドウがアクティブ化を失っている場合など、特定の状況では、フォアグラウンドウィンドウがNULLになる可能性があります<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 各ウィンドウへのハンドルをアプリケーション定義のコールバック関数に渡すことにより、画面上のすべてのトップレベルウィンドウを列挙します<br/> EnumWindowsは、最後の最上位ウィンドウが列挙されるか、コールバック関数がFALSEを返すまで続きます<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-enumwindows"/>
        /// <param name="lpEnumFunc">アプリケーション定義のコールバック関数へのポインター<br/>詳細については、EnumWindowsProcを参照してください<br/></param>
        /// <param name="lParam">コールバック関数に渡されるアプリケーション定義の値<br/></param>
        /// <returns>関数が成功した場合、戻り値はゼロ以外です<br/>関数が失敗した場合、戻り値はゼロです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>EnumWindowsProcがゼロを返す場合、戻り値もゼロです<br/>この場合、コールバック関数はSetLastErrorを呼び出して、EnumWindowsの呼び出し元に返される意味のあるエラーコードを取得する必要があります<br/></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool EnumWindows([In] EnumWindowsDelegate lpEnumFunc, [In] IntPtr lParam);

        /// <summary>
        /// 各子ウィンドウへのハンドルをアプリケーション定義のコールバック関数に渡すことにより、指定された親ウィンドウに属する子ウィンドウを列挙します<br/> EnumChildWindowsは、最後の子ウィンドウが列挙されるか、コールバック関数がFALSEを返すまで続きます<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-enumchildwindows"/>
        /// <param name="hWndParent">子ウィンドウが列挙される親ウィンドウへのハンドル<br/>このパラメーターがNULLの場合、この関数はEnumWindowsと同等です<br/></param>
        /// <param name="lpEnumFunc">アプリケーション定義のコールバック関数へのポインター<br/>詳細については、EnumChildProcを参照してください<br/></param>
        /// <param name="lParam">コールバック関数に渡されるアプリケーション定義の値<br/></param>
        /// <returns>戻り値は使用されません<br/></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows([In] IntPtr hWndParent, [In] EnumWindowsDelegate lpEnumFunc, [In] IntPtr lParam);

        /// <summary>
        /// 指定されたウィンドウが属するクラスの名前を取得します<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getclassname"/>
        /// <param name="hWnd">ウィンドウへのハンドル、および間接的に、ウィンドウが属するクラス</param>
        /// <param name="lpClassName">クラス名の文字列</param>
        /// <param name="nMaxCount">lpClassNameバッファーの長さ（文字数）<br/>バッファは、終了ヌル文字を含めるのに十分な大きさである必要があります<br/>それ以外の場合、クラス名の文字列はnMaxCount-1文字に切り捨てられます</param>
        /// <returns>関数が成功した場合、戻り値はバッファーにコピーされた文字数であり、終了ヌル文字は含まれません<br/>関数が失敗した場合、戻り値はゼロです<br/>拡張エラー情報を取得するには、GetLastError関数を呼び出します<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName([In] IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpClassName, [In] int nMaxCount);

        /// <summary>
        /// 指定されたウィンドウのタイトルバーテキストの長さを文字数で取得します（ウィンドウにタイトルバーがある場合）<br/>指定されたウィンドウがコントロールの場合、関数はコントロール内のテキストの長さを取得します<br/>ただし、GetWindowTextLengthは、別のアプリケーションの編集コントロールのテキストの長さを取得できません<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha"/>
        /// <param name="hWnd">ウィンドウまたはコントロールへのハンドル<br/></param>
        /// <returns>関数が成功した場合、戻り値はテキストの長さ（文字数）です<br/>特定の条件下では、この値はテキストの長さよりも長くなる場合があります（備考を参照）<br/>ウィンドウにテキストがない場合、戻り値はゼロです<br/>関数の失敗は、戻り値がゼロで、GetLastErrorの結果がゼロ以外であることで示されます<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength([In] IntPtr hWnd);
        // public static extern int GetWindowTextLengthA([In] IntPtr hWnd);

        /// <summary>
        /// 指定されたウィンドウのタイトルバー（ある場合）のテキストをバッファにコピーします<br/>指定したウィンドウがコントロールの場合、コントロールのテキストがコピーされます<br/>ただし、GetWindowTextは、別のアプリケーションのコントロールのテキストを取得できません<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowtexta?redirectedfrom=MSDN"/>
        /// <param name="hWnd">テキストを含むウィンドウまたはコントロールへのハンドル</param>
        /// <param name="lpString">テキストを受信するバッファ<br/>文字列がバッファと同じかそれより長い場合、文字列は切り捨てられ、ヌル文字で終了します<br/></param>
        /// <param name="nMaxCount">ヌル文字を含む、バッファーにコピーする最大文字数<br/>テキストがこの制限を超えると、切り捨てられます<br/></param>
        /// <returns>関数が成功した場合、戻り値はコピーされた文字列の長さ（文字数）であり、終了ヌル文字は含まれません<br/>ウィンドウにタイトルバーまたはテキストがない場合、タイトルバーが空の場合、またはウィンドウまたはコントロールハンドルが無効な場合、戻り値はゼロです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>この関数は、別のアプリケーションの編集コントロールのテキストを取得できません<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText([In] IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpString, [In] int nMaxCount);
        /// <summary>
        /// 指定されたウィンドウを作成したスレッドの識別子と、オプションで、ウィンドウを作成したプロセスの識別子を取得します<br/>
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid"/>
        /// <param name="hWnd">ウィンドウへのハンドル</param>
        /// <param name="lpdwProcessId">プロセス識別子を受け取る変数へのポインタ<br/>このパラメーターがNULLでない場合、GetWindowThreadProcessIdはプロセスの識別子を変数にコピーします<br/>それ以外の場合は、そうではありません</param>
        /// <returns>戻り値は、ウィンドウを作成したスレッドの識別子です</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId([In] IntPtr hWnd, [Out] out int lpdwProcessId);

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得します<br/>
        /// https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowinfo
        /// </summary>
        /// <see cref="https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowinfo"/>
        /// <param name="hwnd">情報を取得するウィンドウへのハンドル</param>
        /// <param name="pwi">情報を受け取るためのWINDOWINFO構造体へのポインター<br/>この関数を呼び出す前に、cbSizeメンバーをsizeof（WINDOWINFO）に設定する必要があることに注意してください</param>
        /// <returns>関数が成功した場合、戻り値はゼロ以外です<br/>関数が失敗した場合、戻り値はゼロです<br/>拡張エラー情報を取得するには、GetLastError関数を呼び出します<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowInfo([In] IntPtr hwnd, [MarshalAs(UnmanagedType.LPStruct), In, Out] WINDOWINFO pwi);


        /// <summary>
        /// 指定されたメッセージを1つ以上のウィンドウに送信します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagetimeouta
        /// </summary>
        /// <param name="hWnd">ウィンドウプロシージャがメッセージを受信するウィンドウへのハンドル<br/>このパラメーターがHWND_BROADCAST（（HWND）0xffff）の場合、メッセージは、無効または非表示の所有されていないウィンドウを含む、システム内のすべての最上位ウィンドウに送信されます<br/>この関数は、各ウィンドウがタイムアウトするまで戻りません<br/>したがって、合計待機時間は、uTimeoutの値にトップレベルウィンドウの数を掛けた値までになる可能性があります<br/></param>
        /// <param name="Msg">送信するメッセージ<br/>システム提供メッセージのリストについては、「システム定義メッセージ」を参照してください<br/></param>
        /// <param name="wParam">追加のメッセージ固有の情報<br/></param>
        /// <param name="lParam">追加のメッセージ固有の情報<br/></param>
        /// <param name="fuFlags">この関数の動作<br/>このパラメーターは、次の1つ以上の値にすることができます<br/></param>
        /// <param name="uTimeout">タイムアウト期間の期間（ミリ秒単位）<br/>メッセージがブロードキャストメッセージの場合、各ウィンドウは完全なタイムアウト期間を使用できます<br/>たとえば、5秒のタイムアウト期間を指定し、メッセージの処理に失敗するトップレベルウィンドウが3つある場合、最大15秒の遅延が発生する可能性があります</param>
        /// <param name="lpdwResult">メッセージ処理の結果<br/>このパラメーターの値は、指定されたメッセージによって異なります</param>
        /// <returns>関数が成功した場合、戻り値はゼロ以外です<br/>SendMessageTimeoutは、HWND_BROADCASTが使用されている場合にタイムアウトする個々のウィンドウに関する情報を提供しません<br/>関数が失敗するかタイムアウトした場合、戻り値は0です<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>GetLastErrorがERROR_TIMEOUTを返す場合、関数はタイムアウトしました<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessageTimeout([In] IntPtr hWnd, [In] WindowsMessage Msg, [In] IntPtr wParam, [In] IntPtr lParam, [In] SendMessageTimeoutFlgs fuFlags, [In] UInt32 uTimeout, [In, Out] ref IntPtr lpdwResult);

        /// <summary>
        /// 指定されたメッセージを1つ以上のウィンドウに送信します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagetimeouta
        /// </summary>
        /// <param name="hWnd">ウィンドウプロシージャがメッセージを受信するウィンドウへのハンドル<br/>このパラメーターがHWND_BROADCAST（（HWND）0xffff）の場合、メッセージは、無効または非表示の所有されていないウィンドウを含む、システム内のすべての最上位ウィンドウに送信されます<br/>この関数は、各ウィンドウがタイムアウトするまで戻りません<br/>したがって、合計待機時間は、uTimeoutの値にトップレベルウィンドウの数を掛けた値までになる可能性があります<br/></param>
        /// <param name="Msg">送信するメッセージ<br/>システム提供メッセージのリストについては、「システム定義メッセージ」を参照してください<br/></param>
        /// <param name="wParam">追加のメッセージ固有の情報<br/></param>
        /// <param name="lParam">追加のメッセージ固有の情報<br/></param>
        /// <param name="fuFlags">この関数の動作<br/>このパラメーターは、次の1つ以上の値にすることができます<br/></param>
        /// <param name="uTimeout">タイムアウト期間の期間（ミリ秒単位）<br/>メッセージがブロードキャストメッセージの場合、各ウィンドウは完全なタイムアウト期間を使用できます<br/>たとえば、5秒のタイムアウト期間を指定し、メッセージの処理に失敗するトップレベルウィンドウが3つある場合、最大15秒の遅延が発生する可能性があります</param>
        /// <param name="lpdwResult">メッセージ処理の結果<br/>このパラメーターの値は、指定されたメッセージによって異なります</param>
        /// <returns>関数が成功した場合、戻り値はゼロ以外です<br/>SendMessageTimeoutは、HWND_BROADCASTが使用されている場合にタイムアウトする個々のウィンドウに関する情報を提供しません<br/>関数が失敗するかタイムアウトした場合、戻り値は0です<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>GetLastErrorがERROR_TIMEOUTを返す場合、関数はタイムアウトしました<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout([In] IntPtr hWnd, [In] WindowsMessage Msg, [In] IntPtr wParam, [In, Out] StringBuilder lParam, [In] SendMessageTimeoutFlgs fuFlags, [In] UInt32 uTimeout, [In, Out] ref IntPtr lpdwResult);

        /// <summary>
        /// 指定されたメッセージを1つまたは複数のウィンドウに送信します<br/> SendMessage関数は、指定されたウィンドウのウィンドウプロシージャを呼び出し、ウィンドウプロシージャがメッセージを処理するまで戻りません<br/>メッセージを送信してすぐに戻るには、SendMessageCallback関数またはSendNotifyMessage関数を使用します<br/>メッセージをスレッドのメッセージキューに投稿してすぐに返すには、PostMessage関数またはPostThreadMessage関数を使用します
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessage"/>
        /// <param name="hWnd">ウィンドウプロシージャがメッセージを受信するウィンドウへのハンドル<br/>このパラメータがHWND_BROADCAST（（HWND）0xffff）の場合、メッセージは、無効または非表示の所有されていないウィンドウ、オーバーラップしたウィンドウ、ポップアップウィンドウなど、システム内のすべての最上位ウィンドウに送信されます<br/>ただし、メッセージは子ウィンドウには送信されません</param>
        /// <param name="Msg">送信するメッセージ<br/>システム提供メッセージのリストについては、「システム定義メッセージ」(https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues)を参照してください<br/></param>
        /// <param name="wParam">追加のメッセージ固有の情報</param>
        /// <param name="lParam">追加のメッセージ固有の情報</param>
        /// <returns>戻り値は、メッセージ処理の結果を指定します<br/>送信されるメッセージによって異なります</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SendMessage([In] IntPtr hWnd, [In] WindowsMessage Msg, [In] IntPtr wParam, [In] IntPtr lParam);

        /// 指定されたメッセージを1つまたは複数のウィンドウに送信します<br/> SendMessage関数は、指定されたウィンドウのウィンドウプロシージャを呼び出し、ウィンドウプロシージャがメッセージを処理するまで戻りません<br/>メッセージを送信してすぐに戻るには、SendMessageCallback関数またはSendNotifyMessage関数を使用します<br/>メッセージをスレッドのメッセージキューに投稿してすぐに返すには、PostMessage関数またはPostThreadMessage関数を使用します
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessage"/>
        /// <param name="hWnd">ウィンドウプロシージャがメッセージを受信するウィンドウへのハンドル<br/>このパラメータがHWND_BROADCAST（（HWND）0xffff）の場合、メッセージは、無効または非表示の所有されていないウィンドウ、オーバーラップしたウィンドウ、ポップアップウィンドウなど、システム内のすべての最上位ウィンドウに送信されます<br/>ただし、メッセージは子ウィンドウには送信されません</param>
        /// <param name="Msg">送信するメッセージ<br/>システム提供メッセージのリストについては、「システム定義メッセージ」(https://docs.microsoft.com/en-us/windows/win32/winmsg/about-messages-and-message-queues)を参照してください<br/></param>
        /// <param name="wParam">追加のメッセージ固有の情報</param>
        /// <param name="lParam">追加のメッセージ固有の情報</param>
        /// <returns>戻り値は、メッセージ処理の結果を指定します<br/>送信されるメッセージによって異なります</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SendMessage([In] IntPtr hWnd, [In] WindowsMessage Msg, [In] IntPtr wParam, [In, Out] StringBuilder lParam);

        /// <summary>
        /// 呼び出し元のスレッドの最後のエラーコード値を取得します<br/>最後のエラーコードはスレッドごとに維持されます<br/>複数のスレッドが互いの最後のエラーコードを上書きすることはありません<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-getlasterror
        /// </summary>
        /// <returns>戻り値は、呼び出し元のスレッドの最後のエラーコードです<br/>最後のエラーコードを設定する各関数のドキュメントの「戻り値」セクションには、関数が最後のエラーコードを設定する条件が記載されています<br/>スレッドの最後のエラーコードを設定するほとんどの関数は、失敗したときにそれを設定します<br/>ただし、一部の関数は、成功したときに最後のエラーコードも設定します<br/>関数が最後のエラーコードを設定するように文書化されていない場合、この関数によって返される値は、設定された最新の最後のエラーコードにすぎません<br/>一部の関数は、成功時に最後のエラーコードを0に設定し、他の関数はそうではありません<br/></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern UInt32 GetLastError();

        /// <summary>
        /// メッセージ文字列をフォーマットします<br/>この関数には、入力としてメッセージ定義が必要です<br/>メッセージ定義は、関数に渡されたバッファーから取得できます<br/>これは、すでにロードされているモジュールのメッセージテーブルリソースから取得できます<br/>または、呼び出し元は、システムのメッセージテーブルリソースでメッセージ定義を検索するように関数に要求できます<br/>この関数は、メッセージ識別子と言語識別子に基づいて、メッセージテーブルリソース内のメッセージ定義を検索します<br/>この関数は、フォーマットされたメッセージテキストを出力バッファにコピーし、要求された場合は埋め込まれた挿入シーケンスを処理します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage
        /// </summary>
        /// <param name="dwFlags">書式設定オプション、およびlpSourceパラメーターの解釈方法<br/>dwFlagsの下位バイトは、関数が出力バッファーの改行を処理する方法を指定します<br/>下位バイトは、フォーマットされた出力行の最大幅を指定することもできます</param>
        /// <param name="lpSource">メッセージ定義の場所<br/>このパラメーターのタイプは、dwFlagsパラメーターの設定によって異なります<br/>FORMAT_MESSAGE_FROM_HMODULE(0x00000800): 検索するメッセージテーブルを含むモジュールへのハンドル<br/>FORMAT_MESSAGE_FROM_STRING(0x00000400): フォーマットされていないメッセージテキストで構成される文字列へのポインタ<br/>挿入物がスキャンされ、それに応じてフォーマットされます<br/></param>
        /// <param name="dwMessageId">要求されたメッセージのメッセージ識別子<br/>dwFlagsにFORMAT_MESSAGE_FROM_STRINGが含まれている場合、このパラメーターは無視されます</param>
        /// <param name="dwLanguageId">要求されたメッセージの言語識別子<br/>dwFlagsにFORMAT_MESSAGE_FROM_STRINGが含ま れている場合、このパラメーターは無視されます<br/>このパラメーターで特定のLANGIDを渡すと、 FormatMessageはそのLANGIDのメッセージのみを返します <br/>関数がそのLANGIDのメッセージを見つけることができない場合、 Last-Errorを ERROR_RESOURCE_LANG_NOT_FOUNDに設定します<br/>ゼロを渡すと、 FormatMessageは次の順序でLANGIDのメッセージを 検索します<br/>1. 言語中立<br/>2. スレッドのロケール値に基づくスレッドLANGID<br/>3. ユーザーのデフォルトのロケール値に基づく、ユーザーのデフォルトのLANGID<br/>4. システムのデフォルトのロケール値に基づくシステムのデフォルトのLANGID<br/>5. アメリカ英語<br/>FormatMessageは、先行するLANGIDのいずれのメッセージも検出しない 場合、存在する言語メッセージ文字列を返します<br/>それが失敗した場合は、ERROR_RESOURCE_LANG_NOT_FOUNDを返します<br/></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="Arguments"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint FormatMessage([In] FormatMessageFlgs dwFlags, [In] IntPtr lpSource, [In] uint dwMessageId,  [In] uint dwLanguageId, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpBuffer, [In] uint nSize, [MarshalAs(UnmanagedType.LPWStr), In] string Arguments);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint FormatMessage([In] FormatMessageFlgs dwFlags, [MarshalAs(UnmanagedType.LPStr), In] string lpSource, [In] uint dwMessageId, [In] uint dwLanguageId, [MarshalAs(UnmanagedType.LPWStr), Out] StringBuilder lpBuffer, [In] uint nSize, [MarshalAs(UnmanagedType.LPWStr), In] string Arguments);
        /// <summary>
        /// メッセージ文字列をフォーマットします<br/>この関数には、入力としてメッセージ定義が必要です<br/>メッセージ定義は、関数に渡されたバッファーから取得できます<br/>これは、すでにロードされているモジュールのメッセージテーブルリソースから取得できます<br/>または、呼び出し元は、システムのメッセージテーブルリソースでメッセージ定義を検索するように関数に要求できます<br/>この関数は、メッセージ識別子と言語識別子に基づいて、メッセージテーブルリソース内のメッセージ定義を検索します<br/>この関数は、フォーマットされたメッセージテキストを出力バッファにコピーし、要求された場合は埋め込まれた挿入シーケンスを処理します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-formatmessage
        /// </summary>
        /// <param name="lpSource">メッセージ定義の場所<br/>このパラメーターのタイプは、dwFlagsパラメーターの設定によって異なります<br/>FORMAT_MESSAGE_FROM_HMODULE(0x00000800): 検索するメッセージテーブルを含むモジュールへのハンドル<br/>FORMAT_MESSAGE_FROM_STRING(0x00000400): フォーマットされていないメッセージテキストで構成される文字列へのポインタ<br/>挿入物がスキャンされ、それに応じてフォーマットされます<br/></param>
        /// <param name="dwMessageId">要求されたメッセージのメッセージ識別子<br/>dwFlagsにFORMAT_MESSAGE_FROM_STRINGが含まれている場合、このパラメーターは無視されます</param>
        /// <param name="dwLanguageId">要求されたメッセージの言語識別子<br/>dwFlagsにFORMAT_MESSAGE_FROM_STRINGが含ま れている場合、このパラメーターは無視されます<br/>このパラメーターで特定のLANGIDを渡すと、 FormatMessageはそのLANGIDのメッセージのみを返します <br/>関数がそのLANGIDのメッセージを見つけることができない場合、 Last-Errorを ERROR_RESOURCE_LANG_NOT_FOUNDに設定します<br/>ゼロを渡すと、 FormatMessageは次の順序でLANGIDのメッセージを 検索します<br/>1. 言語中立<br/>2. スレッドのロケール値に基づくスレッドLANGID<br/>3. ユーザーのデフォルトのロケール値に基づく、ユーザーのデフォルトのLANGID<br/>4. システムのデフォルトのロケール値に基づくシステムのデフォルトのLANGID<br/>5. アメリカ英語<br/>FormatMessageは、先行するLANGIDのいずれのメッセージも検出しない 場合、存在する言語メッセージ文字列を返します<br/>それが失敗した場合は、ERROR_RESOURCE_LANG_NOT_FOUNDを返します<br/></param>
        /// <param name="lpBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="Arguments"></param>
        public static uint FormatMessage(string lpSource, uint dwMessageId, uint dwLanguageId, StringBuilder lpBuffer, uint nSize, string Arguments)
        {
            return FormatMessage(FormatMessageFlgs.FORMAT_MESSAGE_FROM_STRING, lpSource, dwMessageId, dwLanguageId, lpBuffer, nSize, Arguments);
        }

        /// <summary>
        /// アプリケーション定義のフックプロシージャをフックチェーンにインストールします<br/>フックプロシージャをインストールして、特定のタイプのイベントについてシステムを監視します<br/>これらのイベントは、特定のスレッド、または呼び出し元のスレッドと同じデスクトップ内のすべてのスレッドに関連付けられています<br/><br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        /// </summary>
        /// <param name="idHook">インストールするフック手順のタイプ<br/>このパラメーターは、次のいずれかの値になります<br/></param>
        /// <param name="lpfn">フックプロシージャへのポインタ<br/>場合のdwThreadIdパラメータがゼロであるか又は異なるプロセスによって作成されたスレッドの識別子を指定し、lpfnパラメータは、DLLにフックプロシージャを指していなければなりません<br/>それ以外の場合、lpfnは、現在のプロセスに関連付けられているコード内のフックプロシージャを指すことができます<br/></param>
        /// <param name="hMod">lpfnパラメータが指すフックプロシージャを含むDLLへのハンドル<br/>HMODのパラメータに設定する必要がNULL場合のdwThreadIdパラメータは、現在のプロセスによって、およびフックプロシージャは、現在のプロセスに関連付けられたコード内にある場合に作成されたスレッドを指定します<br/></param>
        /// <param name="dwThreadId">フックプロシージャが関連付けられるスレッドの識別子<br/>デスクトップアプリの場合、このパラメーターがゼロの場合、フックプロシージャは、呼び出し元のスレッドと同じデスクトップで実行されているすべての既存のスレッドに関連付けられます<br/>Windows Storeアプリについては、「備考」セクションを参照してください<br/></param>
        /// <returns>関数が成功した場合、戻り値はフックプロシージャへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx([In] HookType idHook, [MarshalAs(UnmanagedType.FunctionPtr), In] HOOKPROC lpfn, [In] IntPtr hMod, [In] IntPtr dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx([In] HookType idHook, [MarshalAs(UnmanagedType.FunctionPtr), In] MouseProcHookCallback lpfn, [In] IntPtr hMod, [In] IntPtr dwThreadId);
        /// <summary>
        /// アプリケーション定義のフックプロシージャをフックチェーンにインストールします<br/>フックプロシージャをインストールして、特定のタイプのイベントについてシステムを監視します<br/>これらのイベントは、特定のスレッド、または呼び出し元のスレッドと同じデスクトップ内のすべてのスレッドに関連付けられています<br/><br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        /// </summary>
        /// <param name="lpfn">SetWindowsHookEx関数で使用されるアプリケーション定義またはライブラリ定義のコールバック関数<br/>システムは、アプリケーションがGetMessage関数またはPeekMessage関数を呼び出し、マウス メッセージを処理する場合に、この関数を呼び出します</param>
        /// <param name="hMod">lpfnパラメータが指すフックプロシージャを含むDLLへのハンドル<br/>HMODのパラメータに設定する必要がNULL場合のdwThreadIdパラメータは、現在のプロセスによって、およびフックプロシージャは、現在のプロセスに関連付けられたコード内にある場合に作成されたスレッドを指定します<br/></param>
        /// <param name="dwThreadId">フックプロシージャが関連付けられるスレッドの識別子<br/>デスクトップアプリの場合、このパラメーターがゼロの場合、フックプロシージャは、呼び出し元のスレッドと同じデスクトップで実行されているすべての既存のスレッドに関連付けられます<br/>Windows Storeアプリについては、「備考」セクションを参照してください<br/></param>
        /// <returns>関数が成功した場合、戻り値はフックプロシージャへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        public static IntPtr SetWindowsHookEx(MouseProcHookCallback lpfn, IntPtr hMod, IntPtr dwThreadId)
        {
            return SetWindowsHookEx(HookType.WH_MOUSE, lpfn, hMod, dwThreadId);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx([In] HookType idHook, [MarshalAs(UnmanagedType.FunctionPtr), In] LowLevelMouseProcHookCallback lpfn, [In] IntPtr hMod, [In] IntPtr dwThreadId);
        /// <summary>
        /// アプリケーション定義のフックプロシージャをフックチェーンにインストールします<br/>フックプロシージャをインストールして、特定のタイプのイベントについてシステムを監視します<br/>これらのイベントは、特定のスレッド、または呼び出し元のスレッドと同じデスクトップ内のすべてのスレッドに関連付けられています<br/><br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        /// </summary>
        /// <param name="lpfn">フックプロシージャへのポインタ<br/>場合のdwThreadIdパラメータがゼロであるか又は異なるプロセスによって作成されたスレッドの識別子を指定し、lpfnパラメータは、DLLにフックプロシージャを指していなければなりません<br/>それ以外の場合、lpfnは、現在のプロセスに関連付けられているコード内のフックプロシージャを指すことができます<br/></param>
        /// <param name="hMod">lpfnパラメータが指すフックプロシージャを含むDLLへのハンドル<br/>HMODのパラメータに設定する必要がNULL場合のdwThreadIdパラメータは、現在のプロセスによって、およびフックプロシージャは、現在のプロセスに関連付けられたコード内にある場合に作成されたスレッドを指定します<br/></param>
        /// <param name="dwThreadId">フックプロシージャが関連付けられるスレッドの識別子<br/>デスクトップアプリの場合、このパラメーターがゼロの場合、フックプロシージャは、呼び出し元のスレッドと同じデスクトップで実行されているすべての既存のスレッドに関連付けられます<br/>Windows Storeアプリについては、「備考」セクションを参照してください<br/></param>
        /// <returns>関数が成功した場合、戻り値はフックプロシージャへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        public static IntPtr SetWindowsHookEx(LowLevelMouseProcHookCallback lpfn, IntPtr hMod, IntPtr dwThreadId)
        {
            return SetWindowsHookEx(HookType.WH_MOUSE_LL, lpfn, hMod, dwThreadId);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx([In] HookType idHook, [MarshalAs(UnmanagedType.FunctionPtr), In] CallWndProcProcHookCallback lpfn, [In] IntPtr hMod, [In] IntPtr dwThreadId);

        public static IntPtr SetWindowsHookEx(CallWndProcProcHookCallback lpfn, IntPtr hMod, IntPtr dwThreadId)
        {
            return SetWindowsHookEx(HookType.WH_CALLWNDPROC, lpfn, hMod, dwThreadId);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx([In] HookType idHook, [MarshalAs(UnmanagedType.FunctionPtr), In] CallWndRetProcHookCallback lpfn, [In] IntPtr hMod, [In] IntPtr dwThreadId);

        /// <summary>
        /// アプリケーション定義のフックプロシージャをフックチェーンにインストールします<br/>フックプロシージャをインストールして、特定のタイプのイベントについてシステムを監視します<br/>これらのイベントは、特定のスレッド、または呼び出し元のスレッドと同じデスクトップ内のすべてのスレッドに関連付けられています<br/><br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        /// </summary>
        /// <param name="lpfn">フックプロシージャへのポインタ<br/>場合のdwThreadIdパラメータがゼロであるか又は異なるプロセスによって作成されたスレッドの識別子を指定し、lpfnパラメータは、DLLにフックプロシージャを指していなければなりません<br/>それ以外の場合、lpfnは、現在のプロセスに関連付けられているコード内のフックプロシージャを指すことができます<br/></param>
        /// <param name="hMod">lpfnパラメータが指すフックプロシージャを含むDLLへのハンドル<br/>HMODのパラメータに設定する必要がNULL場合のdwThreadIdパラメータは、現在のプロセスによって、およびフックプロシージャは、現在のプロセスに関連付けられたコード内にある場合に作成されたスレッドを指定します<br/></param>
        /// <param name="dwThreadId">フックプロシージャが関連付けられるスレッドの識別子<br/>デスクトップアプリの場合、このパラメーターがゼロの場合、フックプロシージャは、呼び出し元のスレッドと同じデスクトップで実行されているすべての既存のスレッドに関連付けられます<br/>Windows Storeアプリについては、「備考」セクションを参照してください<br/></param>
        /// <returns>関数が成功した場合、戻り値はフックプロシージャへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        public static IntPtr SetWindowsHookEx(CallWndRetProcHookCallback lpfn, IntPtr hMod, IntPtr dwThreadId)
        {
            return SetWindowsHookEx(HookType.WH_CALLWNDPROCRET, lpfn, hMod, dwThreadId);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx([In] HookType idHook, [MarshalAs(UnmanagedType.FunctionPtr), In] DebugProcHookCallback lpfn, [In] IntPtr hMod, [In] IntPtr dwThreadId);

        /// <summary>
        /// アプリケーション定義のフックプロシージャをフックチェーンにインストールします<br/>フックプロシージャをインストールして、特定のタイプのイベントについてシステムを監視します<br/>これらのイベントは、特定のスレッド、または呼び出し元のスレッドと同じデスクトップ内のすべてのスレッドに関連付けられています<br/><br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
        /// </summary>
        /// <param name="lpfn">フックプロシージャへのポインタ<br/>場合のdwThreadIdパラメータがゼロであるか又は異なるプロセスによって作成されたスレッドの識別子を指定し、lpfnパラメータは、DLLにフックプロシージャを指していなければなりません<br/>それ以外の場合、lpfnは、現在のプロセスに関連付けられているコード内のフックプロシージャを指すことができます<br/></param>
        /// <param name="hMod">lpfnパラメータが指すフックプロシージャを含むDLLへのハンドル<br/>HMODのパラメータに設定する必要がNULL場合のdwThreadIdパラメータは、現在のプロセスによって、およびフックプロシージャは、現在のプロセスに関連付けられたコード内にある場合に作成されたスレッドを指定します<br/></param>
        /// <param name="dwThreadId">フックプロシージャが関連付けられるスレッドの識別子<br/>デスクトップアプリの場合、このパラメーターがゼロの場合、フックプロシージャは、呼び出し元のスレッドと同じデスクトップで実行されているすべての既存のスレッドに関連付けられます<br/>Windows Storeアプリについては、「備考」セクションを参照してください<br/></param>
        /// <returns>関数が成功した場合、戻り値はフックプロシージャへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        public static IntPtr SetWindowsHookEx(DebugProcHookCallback lpfn, IntPtr hMod, IntPtr dwThreadId)
        {
            return SetWindowsHookEx(HookType.WH_DEBUG, lpfn, hMod, dwThreadId);
        }


        public const int HC_ACTION = 0;


        /// <summary>
        /// SetWindowsHookEx関数によってフックチェーンにインストールされたフックプロシージャを削除します<br/>
        /// https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
        /// </summary>
        /// <param name="hhk">取り外すフックのハンドル<br/>このパラメーターは、SetWindowsHookExへの前回の呼び出しによって取得されたフックハンドルです<br/></param>
        /// <returns>関数が成功した場合、戻り値はゼロ以外です<br/>関数が失敗した場合、戻り値はゼロです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>備考:<br/>UnhookWindowsHookExが戻った後でも、フックプロシージャは別のスレッドによって呼び出された状態になる可能性があります<br/>フックプロシージャが同時に呼び出されていない場合、フックプロシージャはUnhookWindowsHookExが戻る直前に削除されます<br/></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// フック情報を現在のフックチェーンの次のフックプロシージャに渡します<br/>フックプロシージャは、フック情報を処理する前または後にこの関数を呼び出すことができます<br/>
        /// </summary>
        /// <param name="hhk">このパラメーターは無視されます</param>
        /// <param name="nCode">現在のフックプロシージャに渡されるフックコード<br/>次のフックプロシージャは、このコードを使用して、フック情報の処理方法を決定します<br/></param>
        /// <param name="wParam">wParamにの値は、現在のフックプロシージャに渡されます<br/>このパラメーターの意味は、現在のフックチェーンに関連付けられているフックのタイプによって異なります</param>
        /// <param name="lParam">lParamには現在のフックプロシージャに渡される値<br/>このパラメーターの意味は、現在のフックチェーンに関連付けられているフックのタイプによって異なります</param>
        /// <returns>この値は、チェーン内の次のフックプロシージャによって返されます<br/>現在のフックプロシージャもこの値を返す必要があります<br/>戻り値の意味は、フックの種類によって異なります<br/>詳細については、個々のフック手順の説明を参照してください<br/>備考:<br/>フック手順は、特定のフックタイプのチェーンにインストールされます<br/>CallNextHookExは、チェーン内の次のフックを呼び出します<br/>CallNextHookExの呼び出しはオプションですが、強くお勧めします<br/>そうしないと、フックがインストールされている他のアプリケーションがフック通知を受信せず、結果として正しく動作しない可能性があります<br/>通知が他のアプリケーションに表示されないようにする必要が絶対にない限り、CallNextHookExを呼び出す必要があります</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, Int32 nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 指定されたモジュールのモジュールハンドルを取得します<br/>モジュールは、呼び出しプロセスによってロードされている必要があります<br/>備考セクションで説明されている競合状態を回避するには、GetModuleHandleEx関数を使用します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlea
        /// </summary>
        /// <param name="lpModuleName">ロードされたモジュールの名前（.dllまたは.exeファイルのいずれか）<br/>ファイル名拡張子を省略すると、デフォルトのライブラリ拡張子.dllが追加されます<br/>ファイル名の文字列には、モジュール名に拡張子がないことを示す末尾の文字（<br/>）を含めることができます<br/>文字列はパスを指定する必要はありません<br/>パスを指定するときは、スラッシュ（/）ではなく、必ず円記号（\）を使用してください<br/>名前は、呼び出し元のプロセスのアドレス空間に現在マップされているモジュールの名前と（大文字と小文字が区別されないように）比較されます<br/>このパラメーターがNULLの場合、 GetModuleHandleは、呼び出しプロセスの作成に使用されたファイル（.exeファイル）へのハンドルを返します<br/>GetModuleHandle関数は、使用してロードされたモジュールのハンドル取得しませんLOAD_LIBRARY_AS_DATAFILEのフラグを<br/>詳細については、LoadLibraryExを参照してください<br/></param>
        /// <returns>関数が成功した場合、戻り値は指定されたモジュールへのハンドルです<br/>関数が失敗した場合、戻り値はNULLです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します <br/>備考:<br/>返されるハンドルはグローバルでも継承可能でもありません<br/>複製したり、別のプロセスで使用したりすることはできません<br/>場合lpModuleNameにはパスが含まれていないと同じベース名と拡張子を持つ複数のロードされたモジュールがあり、あなたが返されるモジュールのハンドルを予測することはできません<br/>この問題を回避するには、パスを指定するか、サイドバイサイドアセンブリを使用するか、GetModuleHandleExを使用してDLL名ではなくメモリの場所を指定します<br/>GetModuleHandle関数は、その参照カウントをインクリメントすることなく、マップされたモジュールへのハンドルを返します<br/>ただし、このハンドルがFreeLibrary関数に渡されると、マップされたモジュールの参照カウントが減少します<br/>そのため、返されたハンドル通らないのGetModuleHandleに FreeLibraryの機能を<br/>これを行うと、DLLモジュールが時期尚早にマップ解除される可能性があります<br/>この関数は、マルチスレッドアプリケーションで慎重に使用する必要があります<br/>この関数がハンドルを返すまでの間、モジュールハンドルが有効であるという保証はありません<br/>たとえば、スレッドがモジュールハンドルを取得したが、ハンドルを使用する前に、2番目のスレッドがモジュールを解放するとします<br/>システムが別のモジュールをロードすると、最近解放されたモジュールハンドルを再利用できます<br/>したがって、最初のスレッドには、意図したものとは異なるモジュールへのハンドルがあります</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        //[DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW", SetLastError = true)]
        //public static extern IntPtr GetModuleHandle(string moduleName);

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得します<br/>この関数は、指定されたオフセットの値を追加のウィンドウメモリに取得します<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowlongptra
        /// </summary>
        /// <param name="hWnd">ウィンドウへのハンドル、および間接的に、ウィンドウが属するクラス</param>
        /// <param name="nIndex">取得する値に対するゼロベースのオフセット<br/>有効な値は、ゼロから追加のウィンドウメモリのバイト数からLONG_PTRのサイズを引いた範囲です<br/>その他の値を取得するには、次のいずれかの値を指定します<br/>
        /// <br/>
        /// GWL_EXSTYLE(-20): 拡張ウィンドウスタイルを 取得します<br/>
        /// GWLP_HINSTANCE(-6): アプリケーションインスタンスへのハンドルを取得します<br/>
        /// GWLP_HWNDPARENT(-8): 親ウィンドウがある場合は、そのハンドルを取得します<br/>
        /// GWLP_ID(-12): ウィンドウの識別子を取得します<br/>
        /// GWL_STYLE(-16): ウィンドウスタイルを 取得します<br/>
        /// GWLP_USERDATA(-21): ウィンドウに関連付けられているユーザーデータを取得します<br/>このデータは、ウィンドウを作成したアプリケーションで使用することを目的としています<br/>その値は最初はゼロです<br/>
        /// GWLP_WNDPROC(-4): ウィンドウプロシージャへのポインタ、またはウィンドウプロシージャへのポインタを表すハンドルを取得します<br/>ウィンドウプロシージャを呼び出すには、CallWindowProc関数を使用する必要があります<br/>
        /// </param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThreadId();

        /// <summary>
        /// 指定されたウィンドウの外接する四角形の寸法を取得します<br/>寸法は、画面の左上隅を基準にした画面座標で示されます<br/>
        /// https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-getwindowrect
        /// </summary>
        /// <param name="hWnd">ウィンドウへのハンドル</param>
        /// <param name="lpRect">ウィンドウの左上隅と右下隅の画面座標を受け取るRECT構造体へのポインター</param>
        /// <returns>関数が成功した場合、戻り値はゼロ以外です<br/>関数が失敗した場合、戻り値はゼロです<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/></returns>
        [DllImport("user32.dll", CharSet =CharSet.Auto, SetLastError =true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect([In] IntPtr hWnd, [MarshalAs(UnmanagedType.LPStruct), Out] RECT lpRect);

        /// <summary>
        /// 子、ポップアップ、またはトップレベルウィンドウのサイズ、位置、およびZオーダーを変更します<br/>これらのウィンドウは、画面上の外観に従って順序付けられています<br/>最上位のウィンドウは最高ランクを受け取り、Zオーダーの最初のウィンドウです<br/>
        /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos
        /// </summary>
        /// <param name="hWnd">ウィンドウへのハンドル</param>
        /// <param name="hWndInsertAfter">Zオーダーで配置されたウィンドウの前にあるウィンドウへのハンドル<br/>このパラメーターは、ウィンドウハンドルまたは次のいずれかの値である必要があります</param>
        /// <param name="X">クライアント座標でのウィンドウの左側の新しい位置</param>
        /// <param name="Y">クライアント座標でのウィンドウ上部の新しい位置</param>
        /// <param name="cx">ウィンドウの新しい幅（ピクセル単位）</param>
        /// <param name="cy">ウィンドウの新しい高さ（ピクセル単位）</param>
        /// <param name="uFlags">ウィンドウのサイズ設定と配置のフラグ</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// キーストローク、マウスモーション、およびボタンクリックを合成します<br/>
        /// https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-sendinput<br/>
        /// キー操作、マウス操作をシミュレート(擬似的に操作する)
        /// </summary>
        /// <param name="nInputs"></param>
        /// <param name="pInputs"></param>
        /// <param name="cbsize"></param>
        /// <returns>この関数は、キーボードまたはマウスの入力ストリームに正常に挿入されたイベントの数を返します<br/>関数がゼロを返す場合、入力はすでに別のスレッドによってブロックされています<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>この関数は、UIPIによってブロックされると失敗します<br/>GetLastErrorも戻り値も、失敗がUIPIブロッキングによって引き起こされたことを示すものではないことに注意してください<br/>備考:<br/>この関数はUIPIの対象です<br/>アプリケーションは、同等以下の整合性レベルにあるアプリケーションにのみ入力を挿入できます<br/>SendInputの関数は、でイベント挿入入力直列キーボードやマウスの入力ストリームに構造<br/>これらのイベントは、（キーボードやマウスを用いて）ユーザによって、またはへのコールのいずれかによって挿入他のキーボードやマウスの入力イベントが点在されていないkeybd_event、MOUSE_EVENT、またはそれに他のコールSendInput<br/>この機能は、キーボードの現在の状態をリセットしません<br/>関数が呼び出されたときにすでに押されているキーは、この関数が生成するイベントに干渉する可能性があります<br/>この問題を回避するには、GetAsyncKeyState関数を使用してキーボードの状態を確認し、必要に応じて修正します<br/>タッチキーボードはwinnls.hで定義された代理マクロを使用して入力をシステムに送信するため、キーボードイベントフックのリスナーはタッチキーボードからの入力をデコードする必要があります<br/>詳細については、サロゲートと補足文字を参照してください<br/>アクセシビリティアプリケーションは、SendInputを使用して、シェルによって処理されるアプリケーション起動ショートカットキーに対応するキーストロークを挿入できます<br/>この機能は、他の種類のアプリケーションで機能することが保証されていません<br/></returns>
        [DllImport("user32.dll")]
        public extern static uint SendInput([In] int nInputs, [In] INPUT[] pInputs, [In] int cbsize);

        /// <summary>
        /// 指定した画面座標にカーソルを移動します<br/>新しい座標が最新のClipCursor関数呼び出しによって設定された画面の長方形内にない場合、システムはカーソルが長方形内にとどまるように座標を自動的に調整します<br/>
        /// https://docs.microsoft.com/ja-jp/windows/win32/api/winuser/nf-winuser-setcursorpos
        /// </summary>
        /// <param name="X">画面座標でのカーソルの新しいx座標</param>
        /// <param name="Y">画面座標でのカーソルの新しいy座標</param>
        /// <returns>成功した場合はゼロ以外を返し、そうでない場合はゼロを返します<br/>拡張エラー情報を取得するには、GetLastErrorを呼び出します<br/>備考:<br/>カーソルは共有リソースです<br/>ウィンドウは、カーソルがウィンドウのクライアント領域にある場合にのみカーソルを移動する必要があります<br/>呼び出しプロセスには、ウィンドウステーションへのWINSTA_WRITEATTRIBUTESアクセスが必要です<br/>SetCursorPosを呼び出すときは、入力デスクトップが現在のデスクトップである必要があります<br/>OpenInputDesktopを呼び出して、現在のデスクトップが入力デスクトップであるかどうかを確認します<br/>そうでない場合は、OpenInputDesktopから返されたHDESKを使用してSetThreadDesktopを呼び出し、そのデスクトップに切り替えます<br/></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool SetCursorPos([In] int X, [In] int Y);

        // 仮想キーコードをスキャンコードに変換
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        public extern static int MapVirtualKey(int wCode, int wMapType);
    }

}