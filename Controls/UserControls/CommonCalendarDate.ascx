<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonCalendarDate.ascx.cs" Inherits="WebForm.Controls.CommonCalendarDate" %>

<asp:Panel ID="DateInputPanel" runat="server" CssClass="common-calendar-date-host">
    <div class="input-group">
        <asp:TextBox ID="DateTextBox" runat="server" CssClass="form-control" />
        <asp:LinkButton ID="CalendarToggleButton" runat="server" CssClass="input-group-text common-date-icon text-decoration-none" ToolTip="カレンダーを開く" CausesValidation="false" OnClick="CalendarToggleButton_Click">&#128197;</asp:LinkButton>
    </div>
    <asp:Panel ID="CalendarPanel" runat="server" Visible="false" CssClass="common-calendar-date-popup shadow">
        <asp:Calendar
            ID="DateCalendar"
            runat="server"
            CssClass="table table-bordered bg-white text-center mb-0"
            DayNameFormat="Shortest"
            FirstDayOfWeek="Sunday"
            NextPrevFormat="ShortMonth"
            OnSelectionChanged="DateCalendar_SelectionChanged"
            OnVisibleMonthChanged="DateCalendar_VisibleMonthChanged"
            OnDayRender="DateCalendar_DayRender">
            <TitleStyle CssClass="table-primary fw-semibold" />
            <NextPrevStyle CssClass="px-2" />
            <DayHeaderStyle CssClass="table-light small" />
            <DayStyle CssClass="p-2" />
            <TodayDayStyle CssClass="border border-primary" />
            <SelectedDayStyle CssClass="bg-primary text-white" />
            <OtherMonthDayStyle CssClass="text-muted" />
        </asp:Calendar>
    </asp:Panel>
</asp:Panel>
<asp:Label ID="DateLabel" runat="server" Visible="false" CssClass="form-control-plaintext text-center w-100" />
