<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CourseMapWeb.Login1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section3 section2">
        <div class="containerLogin container transparence">

            <div class="box boxlogin">
                <h3 class="quoteboxLogin">User<span class="yellow"> Login</span> </h3>
            </div>
            <div class="col-sm-12 boxlogin box label">

                <div class="clear"></div>

                <div class="col-sm-11">

                    <div class="form-group loginSpacing">
                        <!--<label for="Username" class="col-sm-12 loginLabels control-label">First Name </label>-->
                        <div class="col-sm-12 loginFields">
                            <asp:TextBox CssClass="form-control mandatory" ID="txtStudentId" runat="server"
                                placeholder="Type your Student ID..." required="required" />
                        </div>
                    </div>
                    <div class="form-group loginSpacing">
                        <!--<label for="inputPassword" class="col-sm-12 loginLabels control-label">Phone </label>-->
                        <div class="col-sm-12 loginFields">
                            <asp:TextBox CssClass="form-control mandatory" ID="txtPassword" runat="server"
                                placeholder="Type your password..." TextMode="Password" required="required" />
                        </div>

                    </div>

                </div>
                <!-------------end 6------------------>



                <div class="clear"></div>

                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-success mybtnLogin" OnClick="btnLogin_Click" />
                <div class="form-group loginSpacing">
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </div>

                <div class="form-group loginSpacing">
                    <label for="Username" class="forgotPass loginLabels control-label">Forgot password? </label>
                    <a href="#">click here.</a>

                </div>
                <!----end col 12---------->

            </div>

        </div>
    </section>

</asp:Content>
