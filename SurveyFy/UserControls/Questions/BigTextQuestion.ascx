<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BigTextQuestion.ascx.cs" Inherits="Surveyfy.UserControls.Questions.BigTextQuestion" %>

<span class="questionText">
    <asp:Label ID="QuestionLabel" runat="server" />
</span>
<div class="form-group">
    <asp:TextBox ID="ScaleTextBox" runat="server" TextMode="MultiLine">
    </asp:TextBox>
</div>