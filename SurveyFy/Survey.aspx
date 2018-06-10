<%@ Page Title="Survey" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="SurveyFy.Survey" %>

<%@ Register src="UserControls/QuestionContainer.ascx" tagname="QuestionContainer" tagprefix="uc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row-fluid">
        <div class="col">
            <h2><%: Title %>.</h2>
            <h3>Your application description page.</h3>
            <asp:HiddenField ID="hdnRespondent" runat="server" />

            <div id="carouselExampleControls" class="carousel slide" data-wrap="false" data-interval="false" >
                <div class="carousel-inner" style="width:100%;">
                    <asp:Repeater ID="SectionRepeater" runat="server" OnItemDataBound="SectionRepeater_ItemDataBound">
                        <ItemTemplate>
<%--                            <div id="containerdiv" runat="server" class="item">--%>
<%--                                <asp:HiddenField ID="pidHiddenField" runat="server" Value='<%# Eval("Guid") %>' />--%>
                                <uc1:QuestionContainer ID="QuestionContainer1" runat="server" />
<%--                            </div>--%>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <a id="btnBack" type="button" href="#carouselExampleControls" class="btn btn-outline-secondary" data-slide="prev" >Back</a>
                <a id="btnSubmit" href="#carouselExampleControls" class="btn btn-primary" data-slide="next" >Save</a>
            </div>
        </div>
    </div>

    <script>
        jQuery.support.cors = true;
        $(document).ready(function () {

            $("#btnSubmit").click(function () {
                var promises = [];
                //promises = [];
                $('[data-QID]').each(function () {
                    //alert($(this).attr('data-QID') + ' ' + $(this).val());
                    var type = $(this).attr('data-QType');
                    var respondentId = $("#" + '<%= hdnRespondent.ClientID %>').val();
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
                        var answer = {
                            RespondentId: respondentId,
                            QuestionId: questionId,
                            Value: value,
                            ValueText: valueText
                        };

                        console.log(JSON.stringify(answer));

                        var request = $.ajax({
                            url: 'http://api.surveyfy.co.uk/api/answers', //'SurveyFySurvey.asmx/InsertAnswerAsync',
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify(answer), // Check this call.
                            async: true,
                            success: function (data) {
                                //console.log(JSON.stringify(answer));
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                //alert(xhr.status);
                                //alert(xhr.responseText);
                                alert(thrownError);
                            }
                        })
                        promises.push(request);
                    }
                });
                alert(promises);
                $.when.apply(null, promises).done(function () {
                    alert('All done');
                    //if ($('.carousel-inner .item:last').hasClass('active')) {
                    //    window.location.href = "/";
                    //    return false;
                    //}
                })

            });

            $('#btnBack').hide();

            $('#carouselExampleControls').bind('slid.bs.carousel', function (e) {
                var $this = $(this);

                $this.children('#btnSubmit').show();
                $this.children('#btnBack').show();

                if ($('.carousel-inner .item:last').hasClass('active')) {
                    $this.children('#btnSubmit').text('Submit');
                } else if ($('.carousel-inner .item:first').hasClass('active')) {
                    $this.children('#btnBack').hide();
                }
            });
        });
    </script>

</asp:Content>