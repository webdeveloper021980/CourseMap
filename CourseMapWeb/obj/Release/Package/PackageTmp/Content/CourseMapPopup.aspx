<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CourseMapPopup.aspx.cs" Inherits="CourseMapWeb.Content.CourseMapPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/css/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Styles/ModalPopup/ModalPopup.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
       

        <div class="popup_Container">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                </div>
                <div class="TitlebarRight" onclick="cancel();">
                </div>
            </div>

        
                <div class="container transparence">
                    <div class="col-sm-12 box label">
                        <div class="clear"></div>
                        <div class="col-sm-6">
                            <p class="info1">Course Details</p>
                            <div class="form-group" id="divElective" runat="server">
                                <label for="Electives" class="col-sm-3 control-label">Electives</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlElectives" CssClass="select form-control mandatory fal" runat="server" required="required">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" id="divGrade" runat="server">
                                <label for="GradeReceived"  class="col-sm-3 control-label">Grade Received </label>
                                <div class="col-sm-9">

                                    <asp:DropDownList ID="ddlGrade" CssClass="select form-control mandatory fal" runat="server" required="required">
                                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                                         <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                         <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                         <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                         <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="CompleteIncomplete" class="col-sm-3 control-label">Complete/Incomplete</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlCompleteIncomplete" CssClass="select form-control mandatory fal" runat="server" required="required">
                                       <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                                         <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                                         <asp:ListItem Text="Incomplete" Value="Incomplete"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="clear"></div>

                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  CssClass="btn-success mybtnRegister mybtn centerx" />
                    <asp:Button ID="btnReview" runat="server" Text="Review" OnClick="btnReview_Click" CssClass="btn-success mybtnRegister mybtn centerx" />
                    <asp:Button ID="btnPrerequisites" runat="server" Text="Prerequisites" OnClick="btnPrerequisites_Click" CssClass="btn-success mybtnRegister mybtn centerx" />
                    <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" CssClass="btn-success mybtnRegister mybtn centerx" />

                </div>
           <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
