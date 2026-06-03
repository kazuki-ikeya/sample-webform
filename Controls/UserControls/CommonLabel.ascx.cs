using System.Drawing;
using System.Web.UI;

namespace WebForm.Controls
{
    /// <summary>
    /// 必須表示と説明文表示に対応したラベルユーザーコントロールです。
    /// </summary>
    [ToolboxData("<{0}:CommonLabel runat=\"server\" Text=\"CommonLabel\"></{0}:CommonLabel>")]
    public partial class CommonLabel : UserControl
    {
        private static readonly Color DefaultRequiredMarkColor = Color.Blue;

        /// <summary>
        /// 表示するラベルテキストを取得または設定します。
        /// </summary>
        /// <value>
        /// ラベルに表示する文字列。
        /// </value>
        public string Text
        {
            get
            {
                return TextLabel.Text;
            }
            set
            {
                TextLabel.Text = value;
            }
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
                ApplyRequiredState();
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
                RequiredMarkLabel.ForeColor = value;
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
                ApplyDescription();
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Text = "CommonLabel";
        }

        /// <summary>
        /// コントロールの描画前に最終状態を適用します。
        /// </summary>
        /// <param name="e">イベントデータ。</param>
        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);

            ApplyRequiredState();
            ApplyDescription();
        }

        /// <summary>
        /// 必須表示の状態を適用します。
        /// </summary>
        private void ApplyRequiredState()
        {
            RequiredMarkLabel.Visible = Required;
            RequiredMarkLabel.ForeColor = RequiredMarkColor;
        }

        /// <summary>
        /// 説明文をツールチップに適用します。
        /// </summary>
        private void ApplyDescription()
        {
            string description = Description;
            Attributes["title"] = description;
            TextLabel.ToolTip = description;
            RequiredMarkLabel.ToolTip = description;
        }

    }
}
