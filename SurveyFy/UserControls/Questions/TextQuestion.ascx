<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextQuestion.ascx.cs" Inherits="Surveyfy.UserControls.Questions.TextQuestion" %>

<span class="questionText">
    <asp:Label ID="QuestionLabel" runat="server" />
</span>
<div class="form-group">
    <asp:TextBox ID="ScaleTextBox" runat="server">
    </asp:TextBox>
</div>