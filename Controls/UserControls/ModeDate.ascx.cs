using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.Controls
{
    /// <summary>
    /// 編集時は日付入力、参照時はラベルを表示するユーザーコントロールです。
    /// </summary>
    [ToolboxData("<{0}:ModeDate runat=\"server\"></{0}:ModeDate>")]
    public partial class ModeDate : UserControl
    {
        private const string InputDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 外部から追加する日付検証を処理します。
        /// </summary>
        public event ServerValidateEventHandler CustomValidate;

        /// <summary>
        /// 入力値を取得または設定します。
        /// </summary>
        /// <value>
        /// 日付入力欄の文字列。
        /// </value>
        public string Text
        {
            get
            {
                return ModeDateTextBox.Text;
            }
            set
            {
                ModeDateTextBox.Text = value;
                ApplyLabelText();
            }
        }

        /// <summary>
        /// 日付値を取得または設定します。
        /// </summary>
        /// <value>
        /// 入力された日付。日付として解釈できない場合は null。
        /// </value>
        public DateTime? SelectedDate
        {
            get
            {
                DateTime parsedDate;
                if (TryParseText(out parsedDate))
                {
                    return parsedDate.Date;
                }

                return null;
            }
            set
            {
                ModeDateTextBox.Text = value.HasValue
                    ? value.Value.ToString(InputDateFormat, CultureInfo.InvariantCulture)
                    : string.Empty;
                ApplyLabelText();
            }
        }

        /// <summary>
        /// 参照表示時の日付書式を取得または設定します。
        /// </summary>
        /// <value>
        /// 表示用の日付書式。初期値は yyyy/MM/dd。
        /// </value>
        public string DisplayFormat
        {
            get
            {
                string value = ViewState["DisplayFormat"] as string;
                return string.IsNullOrEmpty(value) ? "yyyy/MM/dd" : value;
            }
            set
            {
                ViewState["DisplayFormat"] = value;
                ApplyLabelText();
            }
        }

        /// <summary>
        /// 参照表示に切り替えるかどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// 日付入力を非表示にしてラベルを表示する場合は true。それ以外の場合は false。
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
        /// 未来日付を入力エラーにするかどうかを取得または設定します。
        /// </summary>
        /// <value>
        /// 未来日付を許可しない場合は true。それ以外の場合は false。
        /// </value>
        public bool RejectFutureDate
        {
            get
            {
                object value = ViewState["RejectFutureDate"];
                return value != null && (bool)value;
            }
            set
            {
                ViewState["RejectFutureDate"] = value;
                ApplyValidationState();
            }
        }

        /// <summary>
        /// 未来日付入力時のエラーメッセージを取得または設定します。
        /// </summary>
        /// <value>
        /// 未来日付入力時のエラーメッセージ。
        /// </value>
        public string FutureDateErrorMessage
        {
            get
            {
                string value = ViewState["FutureDateErrorMessage"] as string;
                return string.IsNullOrEmpty(value) ? "未来の日付は入力できません。" : value;
            }
            set
            {
                ViewState["FutureDateErrorMessage"] = value;
                ApplyValidationState();
            }
        }

        /// <summary>
        /// バリデーション対象のグループを取得または設定します。
        /// </summary>
        /// <value>
        /// バリデーショングループ。
        /// </value>
        public string ValidationGroup
        {
            get
            {
                return FutureDateValidator.ValidationGroup;
            }
            set
            {
                FutureDateValidator.ValidationGroup = value;
            }
        }

        /// <summary>
        /// コントロールの描画前に最終状態を適用します。
        /// </summary>
        /// <param name="e">イベントデータ。</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ApplyReadOnlyState();
            ApplyValidationState();
        }

        /// <summary>
        /// ReadOnly の表示状態を適用します。
        /// </summary>
        private void ApplyReadOnlyState()
        {
            ApplyLabelText();

            ModeDateTextBox.Visible = !ReadOnly;
            ModeDateLabel.Visible = ReadOnly;
            ModeDateLabel.Style["display"] = "block";
            ModeDateLabel.Style["width"] = "100%";
            ModeDateLabel.Style["text-align"] = "center";
        }

        /// <summary>
        /// 入力値を表示用ラベルに適用します。
        /// </summary>
        private void ApplyLabelText()
        {
            DateTime parsedDate;
            ModeDateLabel.Text = TryParseText(ModeDateTextBox.Text, out parsedDate)
                ? parsedDate.ToString(DisplayFormat, CultureInfo.CurrentCulture)
                : ModeDateTextBox.Text;
        }

        /// <summary>
        /// 入力文字列を日付へ変換します。
        /// </summary>
        /// <param name="parsedDate">変換後の日付。</param>
        /// <returns>日付に変換できる場合は true。それ以外の場合は false。</returns>
        private bool TryParseText(out DateTime parsedDate)
        {
            return TryParseText(ModeDateTextBox.Text, out parsedDate);
        }

        /// <summary>
        /// 指定された文字列を日付へ変換します。
        /// </summary>
        /// <param name="text">入力文字列。</param>
        /// <param name="parsedDate">変換後の日付。</param>
        /// <returns>日付に変換できる場合は true。それ以外の場合は false。</returns>
        private static bool TryParseText(string text, out DateTime parsedDate)
        {
            return DateTime.TryParseExact(text, InputDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                || DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate);
        }

        /// <summary>
        /// 検証設定を適用します。
        /// </summary>
        private void ApplyValidationState()
        {
            FutureDateValidator.Enabled = (CustomValidate != null) && !ReadOnly;
            FutureDateValidator.ErrorMessage = FutureDateErrorMessage;
            FutureDateValidator.Text = FutureDateErrorMessage;
        }

        /// <summary>
        /// 外部から追加された検証を実行します。
        /// </summary>
        /// <param name="source">イベント発生元。</param>
        /// <param name="args">検証イベントデータ。</param>
        private void RaiseCustomValidate(object source, ServerValidateEventArgs args)
        {
            ServerValidateEventHandler handler = CustomValidate;
            if (handler != null)
            {
                handler(source, args);
            }
        }


        /// <summary>
        /// 検証を行います。
        /// </summary>
        /// <param name="source">イベント発生元。</param>
        /// <param name="args">検証イベントデータ。</param>
        protected void ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            RaiseCustomValidate(source, args);
        }
    }
}
