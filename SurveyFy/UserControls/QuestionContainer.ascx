<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionContainer.ascx.cs" Inherits="Surveyfy.UserControls.QuestionContainer" %>

<div id="containerdiv" runat="server" class="item">
    <div class="row-fluid">
        <div class="col">
            <h1>Hello</h1>
            <asp:HiddenField ID="hdnRespondent" runat="server" />
            <asp:PlaceHolder ID="QuestionPlaceholder" runat="server" />
        </div>
    </div>
</div>
<%--<a type="button" href="#carouselExampleControls" class="btn btn-outline-secondary" data-slide="prev" >Back</a>
<a id="btnSubmit" href="#carouselExampleControls" class="btn btn-primary" data-slide="next" >Save</a>

<script>
    $(document).ready(function () {
        $("#btnSubmit").click(function () {
            $('[data-QID]').each(function () {
                //alert($(this).attr('data-QID') + ' ' + $(this).val());
                var type = $(this).attr('data-QType');
                var respondent = $("#" + '<%= hdnRespondent.ClientID %>').val();
                var value = '';
                var valueText = '';
                var questionId = $(this).attr('data-QID');
                switch (type) {
                    case '1':
                        value = $(':checked', this).val();
                        break;
                    case '2':
                        value = $(':checked', this).val();
                        break;
                    case '3':
                        value = $(':checked', this).val();
                        break;
                    case '4':
                        value = $(':checked', this).val();
                        break;
                    case '5':
                        value = $(this).val();
                        break;
                    case '6':
                        var chkArray = [];

                        /* look for all checkboes that have a class 'chk' attached to it and check if it was checked */
                        $(':checked', this).each(function () {
                            chkArray.push($(this).attr('value'));
                        });

                        /* we join the array separated by the comma */
                        valueText = chkArray.join(',');
                        break;
                    case '7':
                        var chkArray = [];

                        /* look for all checkboes that have a class 'chk' attached to it and check if it was checked */
                        $(':checked', this).each(function () {
                            chkArray.push($(this).attr('value'));
                        });

                        /* we join the array separated by the comma */
                        valueText = chkArray.join(',');
                        break;
                    case '8':
                        var chkArray = [];

                        /* look for all checkboes that have a class 'chk' attached to it and check if it was checked */
                        $(':checked', this).each(function () {
                            chkArray.push($(this).attr('value'));
                        });

                        /* we join the array separated by the comma */
                        valueText = chkArray.join(',');

                        break;
                    case '9':
                        valueText = $(this).val();
                        break;
                    case '10':
                        valueText = $(this).val();
                        break;
                }
                if ((value != null && value != '') || (valueText != null && valueText != '')) {
                    AddEmployee(questionId, respondent, value, valueText);
                }
            });
            window.location.replace("Default.aspx");
        });
    });

    function AddEmployee(questionId,respondentId,value,valueText) {
        jQuery.support.cors = true;
        var answer = {
            RespondentId: respondentId,
            QuestionId: questionId,
            Value: value,
            ValueText: valueText
        };

        $.ajax({
            url: 'http://api.surveyfy.co.uk/api/answers',
            type: 'POST',
            data: JSON.stringify(answer),
            contentType: "application/json",
            success: function (data) {
                console.log(data);
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        });
    }
</script>--%>