<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorizontalCheckQuestion.ascx.cs" Inherits="Surveyfy.UserControls.Questions.HorizontalCheckQuestion" %>

<span class="questionText">
    <asp:Label ID="QuestionLabel" runat="server" />
</span>
<div class="form-group">
    <asp:CheckBoxList ID="ScaleList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%" >
    </asp:CheckBoxList>
</div>