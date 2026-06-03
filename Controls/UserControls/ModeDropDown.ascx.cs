using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    /// <summary>
    /// 編集時はプルダウン、参照時はラベルを表示するユーザーコントロールです。
    /// </summary>
    /// <remarks>
    /// データソースの DataTable には、DataValueField と DataTextField に指定した列が必要です。
    /// </remarks>
    [ToolboxData("<{0}:ModeDropDown runat=\"server\"></{0}:ModeDropDown>")]
    public partial class ModeDropDown : UserControl
    {
        /// <summary>
        /// 項目値として使用するデータソースの列名を取得または設定します。
        /// </summary>
        /// <value>
        /// 内部 DropDownList の DataValueField。
        /// </value>
        public string DataValueField
        {
            get
            {
                return ModeDropDownList.DataValueField;
            }
            set
            {
                ModeDropDownList.DataValueField = value;
            }
        }

        /// <summary>
        /// 項目の表示文字列として使用するデータソースの列名を取得または設定します。
        /// </summary>
        /// <value>
        /// 内部 DropDownList の DataTextField。
        /// </value>
        public string DataTextField
        {
            get
            {
                return ModeDropDownList.DataTextField;
            }
            set
            {
                ModeDropDownList.DataTextField = value;
            }
        }

        /// <summary>
        /// ドロップダウンリストに表示するデータソースを取得または設定します。
        /// </summary>
        /// <value>
        /// DataValueField と DataTextField に指定した列を持つ DataTable。
        /// </value>
        public DataTable DataSource
        {
            get
            {
                return ViewState["DataSource"] as DataTable;
            }
            set
            {
                ViewState["DataSource"] = value;
                BindDataSource(value);
            }
        }

        /// <summary>
        /// 選択されている項目値を取得または設定します。
        /// </summary>
        /// <value>
        /// 選択されている DropDownList 項目の Value。
        /// </value>
        public string SelectedValue
        {
            get
            {
                return ModeDropDownList.SelectedValue;
            }
            set
            {
                ViewState["SelectedValue"] = value;
                EnsureDataBound();
                ApplySelectedValue(value);
                ApplyLabelText();
            }
        }

        /// <summary>
        /// 選択されている項目の表示文字列を取得します。
        /// </summary>
        /// <value>
        /// 選択されている DropDownList 項目の Text。
        /// </value>
        public string SelectedText
        {
            get
            {
                ListItem selectedItem = ModeDropDownList.SelectedItem;
                return selectedItem == null ? string.Empty : selectedItem.Text;
            }
        }

        /// <summary>
        /// 参照表示に切り替えるかどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// プルダウンを非表示にしてラベルを表示する場合は true。それ以外の場合は false。
        /// </value>
        public bool ReadOnly
        {
            get
            {
                object value = ViewState["ReadOnly"];
                return value != null && (bool)value;
            }
            set
            {
                ViewState["ReadOnly"] = value;
                ApplyReadOnlyState();
            }
        }

        /// <summary>
        /// Page.Load イベントを処理します。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベントデータ。</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EnsureDataBound();
            }
            else if (ReadOnly)
            {
                EnsureDataBound();
                ApplySelectedValue(ViewState["SelectedValue"] as string);
            }
        }

        /// <summary>
        /// コントロールの描画前に最終状態を適用します。
        /// </summary>
        /// <param name="e">イベントデータ。</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ViewState["SelectedValue"] = ModeDropDownList.SelectedValue;
            ApplyReadOnlyState();
        }

        /// <summary>
        /// ドロップダウンリストに項目が設定されていない場合、ViewState のデータソースをバインドします。
        /// </summary>
        private void EnsureDataBound()
        {
            if (ModeDropDownList.Items.Count == 0 && DataSource != null)
            {
                BindDataSource(DataSource);
            }
        }

        /// <summary>
        /// 指定された DataTable をドロップダウンリストにバインドします。
        /// </summary>
        /// <param name="dataSource">ドロップダウンリストに表示する DataTable。</param>
        private void BindDataSource(DataTable dataSource)
        {
            ModeDropDownList.Items.Clear();

            if (dataSource == null)
            {
                ApplyLabelText();
                return;
            }

            ValidateDataSource(dataSource);

            ModeDropDownList.DataSource = dataSource;
            ModeDropDownList.DataBind();

            ApplySelectedValue(ViewState["SelectedValue"] as string);
            ApplyLabelText();
        }

        /// <summary>
        /// DataTable に必要な列がすべて含まれていることを検証します。
        /// </summary>
        /// <param name="dataSource">検証対象の DataTable。</param>
        /// <exception cref="ArgumentException">DataValueField または DataTextField に指定した列が存在しない場合にスローされます。</exception>
        private void ValidateDataSource(DataTable dataSource)
        {
            if (!dataSource.Columns.Contains(DataValueField))
            {
                throw new ArgumentException("DataTable must contain a " + DataValueField + " column.", "dataSource");
            }

            if (!dataSource.Columns.Contains(DataTextField))
            {
                throw new ArgumentException("DataTable must contain a " + DataTextField + " column.", "dataSource");
            }
        }

        /// <summary>
        /// 指定された値を選択項目に適用します。
        /// </summary>
        /// <param name="selectedValue">選択する項目値。</param>
        private void ApplySelectedValue(string selectedValue)
        {
            if (selectedValue == null)
            {
                return;
            }

            ListItem item = ModeDropDownList.Items.FindByValue(selectedValue);
            if (item != null)
            {
                ModeDropDownList.ClearSelection();
                item.Selected = true;
            }
        }

        /// <summary>
        /// ReadOnly の表示状態を適用します。
        /// </summary>
        private void ApplyReadOnlyState()
        {
            ApplyLabelText();

            ModeDropDownList.Visible = !ReadOnly;
            ModeLabel.Visible = ReadOnly;
            ModeLabel.Style["display"] = "block";
            ModeLabel.Style["width"] = "100%";
            ModeLabel.Style["text-align"] = "center";
        }

        /// <summary>
        /// 選択中の項目テキストをラベルに適用します。
        /// </summary>
        private void ApplyLabelText()
        {
            ModeLabel.Text = SelectedText;
        }
    }
}
