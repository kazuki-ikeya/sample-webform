using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    /// <summary>
    /// DataTable をバインドできる DropDownList カスタムコントロールです。
    /// </summary>
    /// <remarks>
    /// データソースの DataTable には、DataValueField と DataTextField に指定した列が必要です。
    /// </remarks>
    [ToolboxData("<{0}:BaseDropDown runat=\"server\"></{0}:BaseDropDown>")]
    public class CustomDropDown : DropDownList
    {
        /// <summary>
        /// ViewStateKeys
        /// </summary>
        public static class ViewStateKeys
        {
            public const string ReadOnly = "ReadOnly";
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        public CustomDropDown()
        {

        }

        /// <summary>
        /// 拡張プロパティ：Readonly
        /// </summary>
        /// <value>
        /// 閲覧モードの場合はtrue、それ以外の場合は false。
        /// </value>
        public bool ReadOnly
        {
            get
            {
                object value = ViewState[ViewStateKeys.ReadOnly];
                return value != null && (bool)value;
            }
            set
            {
                ViewState[ViewStateKeys.ReadOnly] = value;
                ApplyReadOnlyState();
            }
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// コントロールの描画前に最終状態を適用します。
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ApplyReadOnlyState();
        }

        /// <summary>
        /// ReadOnly設定時の挙動適用
        /// </summary>
        private void ApplyReadOnlyState()
        {
            if (ReadOnly)
            {
                Attributes["aria-readonly"] = "true";
                Attributes["onmousedown"] = "return false;";
                Attributes["onkeydown"] = "return false;";
                Attributes["onwheel"] = "return false;";
                Style["pointer-events"] = "none";
            }
            else
            {
                Attributes.Remove("aria-readonly");
                Attributes.Remove("onmousedown");
                Attributes.Remove("onkeydown");
                Attributes.Remove("onwheel");
                Style.Remove("pointer-events");
            }
        }

    }
}
