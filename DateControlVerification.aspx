<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DateControlVerification.aspx.cs" Inherits="WebForm.DateControlVerification" %>
<%@ Register Src="~/Controls/UserControls/CommonDate.ascx" TagPrefix="uc" TagName="CommonDate" %>
<%@ Register Src="~/Controls/UserControls/CommonCalendarDate.ascx" TagPrefix="uc" TagName="CommonCalendarDate" %>
<%@ Register Src="~/Controls/UserControls/CommonDatepickerDate.ascx" TagPrefix="uc" TagName="CommonDatepickerDate" %>
<%@ Register Src="~/Controls/UserControls/CommonFlatpickrDate.ascx" TagPrefix="uc" TagName="CommonFlatpickrDate" %>
<%@ Register TagPrefix="cc" Namespace="WebForm.Controls" Assembly="WebForm" %>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent">日付コントロール検証</asp:Content>

<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <main class="container py-4">
        <h1 class="h3 mb-4">日付コントロール検証</h1>
        <div class="sample-form">
            <div class="row g-3">
                <div class="col-md-3">
                    <div class="field mb-2">
                        <cc:CustomLabel ID="StandardDateLabel" runat="server" AssociatedControlID="StandardDate" Text="1. 標準日付入力" Description="ブラウザ標準の input type=date です。" />
                        <uc:CommonDate ID="StandardDate" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="field mb-2">
                        <cc:CustomLabel ID="CalendarDateLabel" runat="server" AssociatedControlID="CalendarDate" Text="2. Calendar" Description="ASP.NET Calendar を使った日付入力です。" />
                        <uc:CommonCalendarDate ID="CalendarDate" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="field mb-2">
                        <cc:CustomLabel ID="DatePickerDateLabel" runat="server" AssociatedControlID="DatePickerDate" Text="3. datepicker" Description="jQuery UI datepicker を使った日付入力です。" />
                        <uc:CommonDatepickerDate ID="DatePickerDate" runat="server" />
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="field mb-2">
                        <cc:CustomLabel ID="FlatpickrDateLabel" runat="server" AssociatedControlID="FlatpickrDate" Text="4. flatpickr" Description="flatpickr を使った日付入力です。" />
                        <uc:CommonFlatpickrDate ID="FlatpickrDate" runat="server" />
                    </div>
                </div>
            </div>

            <div class="check-field form-check mt-3">
                <asp:CheckBox ID="CommonDateReadOnlyCheckBox" runat="server" Text="日付入力を参照表示にする" AutoPostBack="true" OnCheckedChanged="CommonDateReadOnlyCheckBox_CheckedChanged" CssClass="form-check-label" />
            </div>

            <div class="actions d-flex align-items-center gap-3 mt-3">
                <asp:Button ID="PostButton" runat="server" Text="送信" CssClass="btn btn-primary" OnClick="PostButton_Click" />
                <asp:Label ID="StatusLabel" runat="server" CssClass="text-muted" />
            </div>

            <div class="alert alert-light border mt-3">
                <asp:Label ID="MessageLabel" runat="server" />
            </div>
        </div>
    </main>
</asp:Content>
