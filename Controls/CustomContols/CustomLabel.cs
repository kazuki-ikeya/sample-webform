using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    /// <summary>
    /// 必須表示と説明文表示に対応したラベルです。
    /// </summary>
    [ToolboxData("<{0}:CustomLabel runat=\"server\" Text=\"CustomLabel\"></{0}:CustomLabel>")]
    public class CustomLabel : Label
    {
        private static readonly Color DefaultRequiredMarkColor = Color.Blue;

        /// <summary>
        /// CustomLabel クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CustomLabel()
        {
            Text = "CustomLabel";
        }

        /// <summary>
        /// 必須マークを表示するかどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// 必須マークを表示する場合は true。それ以外の場合は false。
        /// </value>
        public bool Required
        {
            get
            {
                object value = ViewState["Required"];
                return value != null && (bool)value;
            }
            set
            {
                ViewState["Required"] = value;
            }
        }

        /// <summary>
        /// 必須マークの色を取得または設定します。
        /// </summary>
        /// <value>
        /// 必須マークの色。初期値は青です。
        /// </value>
        public Color RequiredMarkColor
        {
            get
            {
                object value = ViewState["RequiredMarkColor"];
                return value == null ? DefaultRequiredMarkColor : (Color)value;
            }
            set
            {
                ViewState["RequiredMarkColor"] = value;
            }
        }

        /// <summary>
        /// カーソルを当てたときに表示する説明文を取得または設定します。
        /// </summary>
        /// <value>
        /// ラベルに対する説明文。
        /// </value>
        public string Description
        {
            get
            {
                string value = ViewState["Description"] as string;
                return value ?? string.Empty;
            }
            set
            {
                ViewState["Description"] = value;
            }
        }

        /// <summary>
        /// HTML 属性を描画します。
        /// </summary>
        /// <param name="writer">HTML ライター。</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(ToolTip))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, Description);
            }

            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// ラベルを描画します。
        /// </summary>
        /// <param name="writer">HTML ライター。</param>
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (!Required)
            {
                return;
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "required-mark");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(RequiredMarkColor));
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, "4px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginRight, "4px");
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.WriteEncodedText("*");
            writer.RenderEndTag();
        }
    }
}
