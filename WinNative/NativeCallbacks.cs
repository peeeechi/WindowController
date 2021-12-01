using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowController
{

    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);

    /// <summary>
    /// SetWindowsHookEx関数で使用されるアプリケーション定義またはライブラリ定義のコールバック関数<br/>SendMessage関数が呼び出された後、システムはこの関数を呼び出します<br/>フックプロシージャはメッセージを調べることができます<br/>変更することはできません<br/>HOOKPROCのタイプは、このコールバック関数へのポインタを定義します<br/>CallWndRetProcは、アプリケーション定義またはライブラリ定義の関数名のプレースホルダーです<br/>
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-hookproc
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam">メッセージが現在のプロセスによって送信されるかどうかを指定します<br/>メッセージが現在のプロセスによって送信された場合、それはゼロ以外です<br/>それ以外の場合はNULLです<br/></param>
    /// <param name="lParam">メッセージに関する詳細を含むCWPRETSTRUCT構造体へのポインター<br/></param>
    /// <returns>nCodeがゼロ未満の場合、フックプロシージャは、CallNextHookExによって返される値を返す必要があります<br/>nCodeがゼロ以上の場合は、CallNextHookExを呼び出して、返される値を返すことを強くお勧めします<br/>そうしないと、WH_CALLWNDPROCRETフックをインストールした他のアプリケーションがフック通知を受信せず、結果として正しく動作しない可能性があります<br/>フックプロシージャがCallNextHookExを呼び出さない場合、戻り値はゼロである必要があります<br/></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    /// フック プロシージャがメッセージの処理方法を決定するために使用するコード<br/>nCodeが 0 未満の場合、フック プロシージャは、それ以上処理せずにCallNextHookEx関数にメッセージを渡す必要があり、CallNextHookExによって返される値を返す必要があります<br/>このパラメーターには、次のいずれかの値を指定できます<br/>
    /// "https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644988(v=vs.85)"
    /// </summary>
    /// <param name="nCode">フック プロシージャがメッセージの処理方法を決定するために使用するコード<br/>nCodeが 0 未満の場合、フック プロシージャは、それ以上処理せずにCallNextHookEx関数にメッセージを渡す必要があり、CallNextHookExによって返される値を返す必要があります<br/>このパラメーターには、次のいずれかの値を指定できます<br/>
    /// <br/>
    /// HC_ACTION(0): wParamおよびlParamパラメータには、マウス メッセージに関する情報が含まれています<br/>
    /// HC_NOREMOVE(3): wParamおよびlParamパラメーターにはマウス メッセージに関する情報が含まれ、マウス メッセージはメッセージ キューから削除されていません<br/>(アプリケーションは、PM_NOREMOVEフラグを指定して、PeekMessage関数を呼び出しました<br/>
    /// </param>
    /// <param name="wParam">マウス メッセージの識別子</param>
    /// <param name="lParam">構造体へのポインター</param>
    /// <returns>nCodeがゼロ未満の場合、フックプロシージャは、CallNextHookExによって返される値を返す必要があります<br/>nCodeがゼロ以上の場合は、CallNextHookExを呼び出して、返される値を返すことを強くお勧めします<br/>そうしないと、WH_CALLWNDPROCRETフックをインストールした他のアプリケーションがフック通知を受信せず、結果として正しく動作しない可能性があります<br/>フックプロシージャがCallNextHookExを呼び出さない場合、戻り値はゼロである必要があります<br/></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr MouseProcHookCallback(int nCode, IntPtr wParam, [MarshalAs(UnmanagedType.LPStruct), In] MOUSEHOOKSTRUCT lParam);

    /// <summary>
    /// SetWindowsHookEx関数で使用されるアプリケーション定義またはライブラリ定義のコールバック関数<br/>システムは、新しいマウス入力イベントがスレッド入力キューにポストされるたびに、この関数を呼び出します<br/>
    /// "https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)"
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam">マウスメッセージの識別子<br/>このパラメーターは、WM_LBUTTONDOWN、WM_LBUTTONUP、WM_MOUSEMOVE、WM_MOUSEWHEEL、WM_MOUSEHWHEEL、WM_RBUTTONDOWN、またはWM_RBUTTONUPのいずれかのメッセージになります</param>
    /// <param name="lParam">MSLLHOOKSTRUCT構造体へのポインター</param>
    /// <returns>nCodeがゼロ未満の場合、フックプロシージャは、CallNextHookExによって返される値を返す必要があります<br/>場合nCode社がゼロ以上で、かつフックプロシージャがメッセージを処理していなかった、非常にあなたが呼び出すことが推奨されCallNextHookExをし、それが返す値を返します<br/>そうしないと、WH_MOUSE_LLフックをインストールした他のアプリケーションがフック通知を受信せず、結果として正しく動作しない可能性があります<br/>フックプロシージャがメッセージを処理した場合、システムがメッセージをフックチェーンの残りの部分またはターゲットウィンドウプロシージャに渡さないように、ゼロ以外の値を返す場合があります<br/>備考:<br/>アプリケーションは、SetWindowsHookEx関数の呼び出しで、WH_MOUSE_LLフックタイプとフックプロシージャへのポインタを指定することにより、フックプロシージャをインストールします<br/>このフックは、それをインストールしたスレッドのコンテキストで呼び出されます<br/>呼び出しは、フックをインストールしたスレッドにメッセージを送信することによって行われます<br/>したがって、フックをインストールしたスレッドにはメッセージループが必要です<br/>マウス入力は、ローカルマウスドライバーまたはmouse_event関数の呼び出しから取得できます<br/>入力がmouse_eventの呼び出しからのものである場合、入力は「注入」されました<br/>ただし、WH_MOUSE_LLフックは別のプロセスに挿入されません<br/>代わりに、コンテキストはフックをインストールしたプロセスに戻り、元のコンテキストで呼び出されます<br/>次に、コンテキストはイベントを生成したアプリケーションに戻ります<br/>フックプロシージャは、次のレジストリキーのLowLevelHooksTimeout値で指定されたデータエントリよりも短い時間でメッセージを処理する必要があります<br/><code>HKEY_CURRENT_USER\Control Panel\Desktop</code>値はミリ秒単位です<br/>フック手順がタイムアウトした場合、システムはメッセージを次のフックに渡します<br/>ただし、Windows 7以降では、フックは呼び出されずにサイレントに削除されます<br/>フックが削除されているかどうかをアプリケーションが知る方法はありません<br/>注  デバッグフックは、このタイプの低レベルのマウスフックを追跡できません<br/>アプリケーションが低レベルのフックを使用する必要がある場合は、作業をワーカースレッドに渡し、すぐに戻る専用スレッドでフックを実行する必要があります<br/>アプリケーションが低レベルのフックを使用する必要があるほとんどの場合、代わりに生の入力を監視する必要があります<br/>これは、raw入力が、低レベルのフックよりも効果的に他のスレッドを対象とするマウスとキーボードのメッセージを非同期的に監視できるためです<br/></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr LowLevelMouseProcHookCallback(int nCode, IntPtr wParam, [MarshalAs(UnmanagedType.LPStruct), In] MSLLHOOKSTRUCT lParam);

    /// <summary>
    /// SetWindowsHookEx関数で使用されるアプリケーション定義またはライブラリ定義のコールバック関数<br/>SendMessage関数が呼び出された後、システムはこの関数を呼び出します<br/>フックプロシージャはメッセージを調べることができます<br/>変更することはできません<br/>
    /// HOOKPROCのタイプは、このコールバック関数へのポインタを定義します<br/>CallWndRetProcは、アプリケーション定義またはライブラリ定義の関数名のプレースホルダーです<br/>
    /// "https://docs.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-hookproc"
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam">メッセージが現在のプロセスによって送信されるかどうかを指定します<br/>メッセージが現在のプロセスによって送信された場合、それはゼロ以外です<br/>それ以外の場合はNULLです<br/></param>
    /// <param name="lParam">メッセージに関する詳細を含むCWPRETSTRUCT構造体へのポインター</param>
    /// <returns>nCodeがゼロ未満の場合、フックプロシージャは、CallNextHookExによって返される値を返す必要があります<br/>
    /// nCodeがゼロ以上の場合は、CallNextHookExを呼び出して、返される値を返すことを強くお勧めします<br/>そうしないと、WH_CALLWNDPROCRETフックをインストールした他のアプリケーションがフック通知を受信せず、結果として正しく動作しない可能性があります<br/>フックプロシージャがCallNextHookExを呼び出さない場合、戻り値はゼロである必要があります<br/></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CallWndRetProcHookCallback(int nCode, IntPtr wParam, [MarshalAs(UnmanagedType.LPStruct), In] CWPRETSTRUCT lParam);

    /// <summary>
    /// SetWindowsHookEx関数で使用されるアプリケーション定義またはライブラリ定義のコールバック関数<br/>システムは、ウィンドウプロシージャを呼び出す前にこの関数を呼び出して、スレッドに送信されたメッセージを処理します<br/>    HOOKPROCのタイプは、このコールバック関数へのポインタを定義します<br/>CallWndProcは、アプリケーション定義またはライブラリ定義の関数名のプレースホルダーです<br/>
    /// "https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644975(v=vs.85)"
    /// </summary>
    /// <param name="nCode">フックプロシージャがメッセージを処理する必要があるかどうかを指定します<br/>場合nCode社があるHC_ACTION、フックプロシージャがメッセージを処理しなければなりません<br/>場合nCode社がゼロ未満である、フックプロシージャは、メッセージを渡す必要がありCallNextHookExのさらなる処理なし関数によって返された値を返す必要がありCallNextHookExを</param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr CallWndProcProcHookCallback(int nCode, IntPtr wParam, [MarshalAs(UnmanagedType.LPStruct), In] CWPSTRUCT lParam);


    /// <summary>
    /// SetWindowsHookEx関数で使用されるアプリケーション定義またはライブラリ定義のコールバック関数<br/>システムは、任意のタイプのフックに関連付けられたフックプロシージャを呼び出す前に、この関数を呼び出します<br/>システムは、呼び出されるフックに関する情報をDebugProcフックプロシージャに渡します<br/>このプロシージャは、情報を調べて、フックの呼び出しを許可するかどうかを決定します<br/>    HOOKPROCのタイプは、このコールバック関数へのポインタを定義します<br/>DebugProcは、アプリケーション定義またはライブラリ定義の関数名のプレースホルダーです<br/>
    /// "https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644978(v=vs.85)"
    /// </summary>
    /// <param name="nCode">フックプロシージャがメッセージを処理する必要があるかどうかを指定します<br/>場合nCode社があるHC_ACTION、フックプロシージャがメッセージを処理しなければなりません<br/>場合nCode社がゼロ未満である、フックプロシージャは、メッセージを渡す必要がありCallNextHookExのさらなる処理なし関数によって返された値を返す必要がありCallNextHookExを</param>
    /// <param name="wParam">呼び出されようとしているフックのタイプ</param>
    /// <param name="lParam">宛先フック・プロシージャーに渡されるパラメーターを含むDEBUGHOOKINFO構造体へのポインター</param>
    /// <returns>システムがフックを呼び出さないようにするには、フックプロシージャがゼロ以外の値を返す必要があります<br/>それ以外の場合、フックプロシージャはCallNextHookExを呼び出す必要があります<br/></returns>
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr DebugProcHookCallback(int nCode, HookType wParam, [MarshalAs(UnmanagedType.LPStruct), In] DEBUGHOOKINFO lParam);

}
