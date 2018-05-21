<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VerticalQuestion.ascx.cs" Inherits="Surveyfy.UserControls.Questions.VerticalQuestion" %>

<span class="questionText">
    <asp:Label ID="QuestionLabel" runat="server" />
</span>
<div class="form-group">
    <asp:RadioButtonList ID="ScaleList" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow" Width="100%" >
    </asp:RadioButtonList>
</div>