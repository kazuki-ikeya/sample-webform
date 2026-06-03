using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    /// <summary>
    /// DataTable をバインドできる再利用可能な DropDownList ユーザーコントロールです。
    /// </summary>
    /// <remarks>
    /// データソースの DataTable には、DataValueField と DataTextField に指定した列が必要です。
    /// </remarks>
    public partial class CommonDropDown : UserControl
    {
        /// <summary>
        /// ViewStateKeys
        /// </summary>
        public static class ViewStateKeys
        {
            public const string DataSource = "DataSource";
            public const string SelectedValue = "SelectedValue";
            public const string ReadOnly = "ReadOnly";
        }

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
                return CommonDropDownList.DataValueField;
            }
            set
            {
                CommonDropDownList.DataValueField = value;
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
                return CommonDropDownList.DataTextField;
            }
            set
            {
                CommonDropDownList.DataTextField = value;
            }
        }

        /// <summary>
        /// ドロップダウンリストに表示するデータソースを取得または設定します。
        /// </summary>
        /// <value>
        /// Value 列と Text 列を持つ DataTable。
        /// </value>
        public DataTable DataSource
        {
            get
            {
                return ViewState[ViewStateKeys.DataSource] as DataTable;
            }
            set
            {
                ViewState[ViewStateKeys.DataSource] = value;
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
                return CommonDropDownList.SelectedValue;
            }
            set
            {
                ViewState[ViewStateKeys.SelectedValue] = value;
                EnsureDataBound();
                ApplySelectedValue(value);
            }
        }

        /// <summary>
        /// ドロップダウンリストが有効かどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// ドロップダウンリストが有効な場合は true。それ以外の場合は false。
        /// </value>
        public bool Enabled
        {
            get
            {
                return CommonDropDownList.Enabled;
            }
            set
            {
                CommonDropDownList.Enabled = value;
            }
        }

        /// <summary>
        /// 選択値をユーザーが変更できない状態にするかどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// ポストバック時に選択値を利用可能なままユーザーによる変更を防ぐ場合は true。それ以外の場合は false。
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
                ApplySelectedValue(ViewState[ViewStateKeys.SelectedValue] as string);
            }
        }

        /// <summary>
        /// コントロールの描画前に最終状態を適用します。
        /// </summary>
        /// <param name="e">イベントデータ。</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ViewState[ViewStateKeys.SelectedValue] = CommonDropDownList.SelectedValue;
            ApplyReadOnlyState();
        }

        /// <summary>
        /// ドロップダウンリストに項目が設定されていない場合、ViewState のデータソースをバインドします。
        /// </summary>
        private void EnsureDataBound()
        {
            if (CommonDropDownList.Items.Count == 0 && DataSource != null)
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
            CommonDropDownList.Items.Clear();

            if (dataSource == null)
            {
                return;
            }

            ValidateDataSource(dataSource);

            CommonDropDownList.DataSource = dataSource;
            CommonDropDownList.DataBind();

            ApplySelectedValue(ViewState[ViewStateKeys.SelectedValue] as string);
        }

        /// <summary>
        /// DataTable に必要な列がすべて含まれていることを検証します。
        /// </summary>
        /// <param name="dataSource">検証対象の DataTable。</param>
        /// <exception cref="ArgumentException">Value 列または Text 列が存在しない場合にスローされます。</exception>
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

            ListItem item = CommonDropDownList.Items.FindByValue(selectedValue);
            if (item != null)
            {
                CommonDropDownList.ClearSelection();
                item.Selected = true;
            }
        }

        /// <summary>
        /// ReadOnly が true の場合にユーザー編集を防ぐクライアント側の動作を適用します。
        /// </summary>
        private void ApplyReadOnlyState()
        {
            if (ReadOnly)
            {
                CommonDropDownList.Attributes["aria-readonly"] = "true";
                CommonDropDownList.Attributes["onmousedown"] = "return false;";
                CommonDropDownList.Attributes["onkeydown"] = "return false;";
                CommonDropDownList.Attributes["onwheel"] = "return false;";
                CommonDropDownList.Style["pointer-events"] = "none";
            }
            else
            {
                CommonDropDownList.Attributes.Remove("aria-readonly");
                CommonDropDownList.Attributes.Remove("onmousedown");
                CommonDropDownList.Attributes.Remove("onkeydown");
                CommonDropDownList.Attributes.Remove("onwheel");
                CommonDropDownList.Style.Remove("pointer-events");
            }
        }
    }
}
