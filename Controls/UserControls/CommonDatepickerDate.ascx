<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonDatepickerDate.ascx.cs" Inherits="WebForm.Controls.CommonDatepickerDate" %>

<link href="https://cdn.jsdelivr.net/npm/jquery-ui-dist@1.13.3/jquery-ui.min.css" rel="stylesheet" />
<script src="https://cdn.jsdelivr.net/npm/jquery@3.7.1/dist/jquery.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/jquery-ui-dist@1.13.3/jquery-ui.min.js"></script>

<asp:Panel ID="DateInputGroup" runat="server" CssClass="input-group">
    <asp:TextBox ID="DateTextBox" runat="server" CssClass="form-control" />
    <span class="input-group-text common-date-icon" title="カレンダーを開く" aria-hidden="true">&#128197;</span>
</asp:Panel>
<asp:Label ID="DateLabel" runat="server" Visible="false" CssClass="form-control-plaintext text-center w-100" />

<script>
    jQuery(function ($) {
        var input = $('#<%= DateTextBox.ClientID %>');
        input.datepicker({
            dateFormat: 'yy/mm/dd',
            monthNames: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
            monthNamesShort: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
            dayNames: ['日曜日', '月曜日', '火曜日', '水曜日', '木曜日', '金曜日', '土曜日'],
            dayNamesShort: ['日', '月', '火', '水', '木', '金', '土'],
            dayNamesMin: ['日', '月', '火', '水', '木', '金', '土'],
            currentText: '今日',
            closeText: '閉じる',
            prevText: '前',
            nextText: '次',
            beforeShowDay: function (date) {
                var holidayName = CommonDateControl.holidayName(date);
                if (holidayName || date.getDay() === 0) {
                    return [true, 'common-date-holiday', holidayName || '日曜日'];
                }
                if (date.getDay() === 6) {
                    return [true, 'common-date-saturday', '土曜日'];
                }
                return [true, '', ''];
            },
            onSelect: function () {
                CommonDateControl.applyInputColor(this);
            }
        });

        input.on('change', function () {
            CommonDateControl.applyInputColor(this);
        });
        input.closest('.input-group').find('.common-date-icon').on('click', function () {
            input.datepicker('show');
        });
        CommonDateControl.applyInputColor(input[0]);
    });
</script>
<style>
.ui-datepicker-title {
    display: flex;
    justify-content: center;
}

.ui-datepicker-year {
    order: 1;
    margin-left: 4px;
    margin-right: 4px;
}

.ui-datepicker-month {
    order: 2;
    margin-left: 4px;
    margin-right: 4px;
}
</style>