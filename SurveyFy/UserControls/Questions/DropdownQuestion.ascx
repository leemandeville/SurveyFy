<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropdownQuestion.ascx.cs" Inherits="Surveyfy.UserControls.Questions.DropDownQuestion" %>

<style>
span.radio {
    padding: 0px;
    width:100%;
}

span.radio > input[type="radio"] {
    margin: 8px -5px 7px 0px;
}

span.radio > label {
    float: left;
    margin-right: 5px;
    padding: 4px 0px 0px 10px;
}
    </style>

<span class="questionText">
    <asp:Label ID="QuestionLabel" runat="server" />
</span>
<div class="form-group">
    <asp:DropDownList ID="ScaleList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%" CssClass="radio" >
    </asp:DropDownList>
</div>