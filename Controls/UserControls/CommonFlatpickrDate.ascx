<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonFlatpickrDate.ascx.cs" Inherits="WebForm.Controls.CommonFlatpickrDate" %>

<link href="https://cdn.jsdelivr.net/npm/flatpickr@4.6.13/dist/flatpickr.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/flatpickr@4.6.13/dist/flatpickr.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/flatpickr@4.6.13/dist/l10n/ja.js"></script>

<asp:Panel ID="DateInputGroup" runat="server" CssClass="input-group">
    <asp:TextBox ID="DateTextBox" runat="server" CssClass="form-control" />
    <span class="input-group-text common-date-icon" title="カレンダーを開く" aria-hidden="true">&#128197;</span>
</asp:Panel>
<asp:Label ID="DateLabel" runat="server" Visible="false" CssClass="form-control-plaintext text-center w-100" />

<script>
    document.addEventListener('DOMContentLoaded', function () {
        var input = document.getElementById('<%= DateTextBox.ClientID %>');
        var picker = flatpickr(input, {
            locale: 'ja',
            dateFormat: 'Y/m/d',
            monthSelectorType: "static",
            allowInput: true,
            onChange: function () {
                CommonDateControl.applyInputColor(input);
            },
            onDayCreate: function (dObj, dStr, fp, dayElem) {
                var date = dayElem.dateObj;
                var holidayName = CommonDateControl.holidayName(date);
                if (holidayName || date.getDay() === 0) {
                    dayElem.classList.add('common-date-holiday');
                    dayElem.title = holidayName || '日曜日';
                } else if (date.getDay() === 6) {
                    dayElem.classList.add('common-date-saturday');
                    dayElem.title = '土曜日';
                }
            }
        });

        input.addEventListener('change', function () {
            CommonDateControl.applyInputColor(input);
        });
        input.closest('.input-group').querySelector('.common-date-icon').addEventListener('click', function () {
            picker.open();
        });
        CommonDateControl.applyInputColor(input);
    });
</script>

<style>
.flatpickr-current-month {
    display: flex;
    justify-content: center;
    align-items: baseline;
}

.flatpickr-current-month .numInputWrapper {
    order: 1; /* 年 */
}

.flatpickr-current-month .cur-month {
    order: 2; /* 月 */
}
</style>