//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace WebForm.Controls
//{
//    /// <summary>
//    /// DataTable をバインドできる DropDownList カスタムコントロールです。
//    /// </summary>
//    /// <remarks>
//    /// データソースの DataTable には、DataValueField と DataTextField に指定した列が必要です。
//    /// </remarks>
//    [ToolboxData("<{0}:BaseDropDown runat=\"server\"></{0}:BaseDropDown>")]
//    public class BaseDropDown : DropDownList
//    {
//        private const string ReadOnlyScriptKey = "BaseDropDown_ReadOnlyScript";
//        private const string ReadOnlyFunctionName = "BaseDropDown_CancelReadOnlyEvent";
//        private const string ReadOnlyHandlerScript = "if (BaseDropDown_CancelReadOnlyEvent(typeof event !== 'undefined' ? event : window.event) === false) return false;";

//        /// <summary>
//        /// ViewStateKeys
//        /// </summary>
//        public static class ViewStateKeys
//        {
//            public const string ReadOnly = "ReadOnly";
//        }

//        /// <summary>
//        /// インスタンス初期化
//        /// </summary>
//        public BaseDropDown()
//        {

//        }


//        /// <summary>
//        /// 拡張プロパティ：Readonly
//        /// </summary>
//        /// <value>
//        /// 閲覧モードの場合はtrue、それ以外の場合は false。
//        /// </value>
//        public bool ReadOnly
//        {
//            get
//            {
//                object value = ViewState[ViewStateKeys.ReadOnly];
//                return value != null && (bool)value;
//            }
//            set
//            {
//                ViewState[ViewStateKeys.ReadOnly] = value;
//                ApplyReadOnlyState();
//            }
//        }

//        /// <summary>
//        /// Load イベント
//        /// </summary>
//        /// <param name="e">イベントデータ。</param>
//        protected override void OnLoad(EventArgs e)
//        {
//            base.OnLoad(e);
//        }

//        /// <summary>
//        /// コントロールの描画前に最終状態を適用します。
//        /// </summary>
//        /// <param name="e">イベントデータ。</param>
//        protected override void OnPreRender(EventArgs e)
//        {
//            base.OnPreRender(e);

//            ApplyReadOnlyState();
//        }

//        /// <summary>
//        /// ReadOnly設定時の挙動適用
//        /// </summary>
//        private void ApplyReadOnlyState()
//        {
//            if (ReadOnly)
//            {
//                RegisterReadOnlyScript();
//                Attributes["aria-readonly"] = "true";
//                AddReadOnlyHandler("onmousedown");
//                AddReadOnlyHandler("onkeydown");
//                AddReadOnlyHandler("onwheel");
//            }
//            else
//            {
//                Attributes.Remove("aria-readonly");
//                RemoveReadOnlyHandler("onmousedown");
//                RemoveReadOnlyHandler("onkeydown");
//                RemoveReadOnlyHandler("onwheel");
//            }
//        }

//        /// <summary>
//        /// ReadOnly 用のクライアント関数を登録します。
//        /// </summary>
//        private void RegisterReadOnlyScript()
//        {
//            if (Page == null || Page.ClientScript.IsClientScriptBlockRegistered(GetType(), ReadOnlyScriptKey))
//            {
//                return;
//            }

//            string script =
//                "function " + ReadOnlyFunctionName + "(event) {" +
//                "if (event && event.preventDefault) { event.preventDefault(); }" +
//                "return false;" +
//                "}";

//            Page.ClientScript.RegisterClientScriptBlock(GetType(), ReadOnlyScriptKey, script, true);
//        }

//        /// <summary>
//        /// 指定したイベント属性へ ReadOnly 用ハンドラを追加します。
//        /// </summary>
//        /// <param name="attributeName">イベント属性名。</param>
//        private void AddReadOnlyHandler(string attributeName)
//        {
//            string currentScript = Attributes[attributeName];
//            if (currentScript != null && currentScript.Contains(ReadOnlyHandlerScript))
//            {
//                return;
//            }

//            Attributes[attributeName] = AppendScript(currentScript, ReadOnlyHandlerScript);
//        }

//        /// <summary>
//        /// 指定したイベント属性から ReadOnly 用ハンドラだけを削除します。
//        /// </summary>
//        /// <param name="attributeName">イベント属性名。</param>
//        private void RemoveReadOnlyHandler(string attributeName)
//        {
//            string currentScript = Attributes[attributeName];
//            if (currentScript == null)
//            {
//                return;
//            }

//            string updatedScript = RemoveScript(currentScript, ReadOnlyHandlerScript);
//            if (updatedScript.Length == 0)
//            {
//                Attributes.Remove(attributeName);
//            }
//            else
//            {
//                Attributes[attributeName] = updatedScript;
//            }
//        }

//        /// <summary>
//        /// 既存スクリプトの後ろに指定したスクリプトを追加します。
//        /// </summary>
//        /// <param name="currentScript">既存スクリプト。</param>
//        /// <param name="scriptToAppend">追加するスクリプト。</param>
//        /// <returns>結合後のスクリプト。</returns>
//        private static string AppendScript(string currentScript, string scriptToAppend)
//        {
//            if (string.IsNullOrWhiteSpace(currentScript))
//            {
//                return scriptToAppend;
//            }

//            return currentScript.TrimEnd() + " " + scriptToAppend;
//        }

//        /// <summary>
//        /// 既存スクリプトから指定したスクリプトを削除します。
//        /// </summary>
//        /// <param name="currentScript">既存スクリプト。</param>
//        /// <param name="scriptToRemove">削除するスクリプト。</param>
//        /// <returns>削除後のスクリプト。</returns>
//        private static string RemoveScript(string currentScript, string scriptToRemove)
//        {
//            return currentScript.Replace(scriptToRemove, string.Empty).Trim();
//        }
//    }
//}
