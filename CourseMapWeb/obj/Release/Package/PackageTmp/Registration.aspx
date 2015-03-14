<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs"
    Inherits="CourseMapWeb.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ComparePassword() {
            var password = $("#" + "<%=txtPassword.ClientID %>");
            var confirmaPassword = $("#" + "<%=txtConfirmPassword.ClientID %>");

            if (jQuery.trim(password.val()) != jQuery.trim(confirmaPassword.val())) {
                alert("Password doesnt match");
                return false;
            }
            return true;
        }
    </script>
    <asp:UpdateProgress ID="UpdateProgressRegistration" runat="server" AssociatedUpdatePanelID="UpdatePanelRegistration"
        DisplayAfter="10" DynamicLayout="True">
        <ProgressTemplate>
            <div class="Loading">
                <img alt="Please Wait Contents are Loading...  "
                    src="Images/loading.gif" class="LoadingImage" />

            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelRegistration" runat="server">
        <ContentTemplate>
            <section class="section3 section2">
                <div class="container transparence">

                    <div class="box">
                        <h3 class="quotebox">Register With <span class="yellow">CourseMap</span> </h3>
                    </div>
                    <div class="col-sm-12 box label">

                        <div class="clear"></div>
                        <div class="ff">
                            <p class="info1">Basic Info</p>


                            <div class="form-group">
                                <label for="fstName" class="col-sm-3 control-label">Student Id </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtStudentId" runat="server" MaxLength="25" CssClass="form-control mandatory"
                                        placeholder="Student ID" required="required"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="fstName" class="col-sm-3 control-label">First Name </label>
                                <div class="col-sm-9">

                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="form-control mandatory"
                                        placeholder="First Name" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="lname" class="col-sm-3 control-label">Last Name </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" CssClass="form-control mandatory"
                                        placeholder="Last Name" required="required"></asp:TextBox>
                                </div>

                            </div>

                            <div class="form-group">
                                <label for="email" class="col-sm-3 control-label">Email </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control mandatory" ID="txtEmail" runat="server"
                                        placeholder="Email " required="required" MaxLength="50" />
                                </div>

                            </div>

                            <div class="form-group">
                                <label for="School" class="col-sm-3 control-label">Institution</label>
                                <div class="col-sm-1">
                                    <asp:RadioButton ID="rbSchool" runat="server" Text="School" Checked="True" GroupName="sch"
                                        AutoPostBack="True" OnCheckedChanged="LoadSchoolCollage" />
                                    <asp:RadioButton ID="rbCollage" runat="server" Text="Collage" GroupName="sch"
                                        AutoPostBack="True" OnCheckedChanged="LoadSchoolCollage" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="Collage" class="col-sm-3 control-label">Institution Name</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlSchoolCollage" CssClass="select form-control mandatory fal" runat="server" required="required"
                                        AutoPostBack="True" OnSelectedIndexChanged="SchoolCollageSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="form-group">
                                <label for="Department" class="col-sm-3 control-label">Department </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlDepartment" CssClass="select form-control mandatory fal" runat="server" required="required"
                                        AutoPostBack="True" OnSelectedIndexChanged="DepartmentSelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="Major" class="col-sm-3 control-label">Major </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlMajor" CssClass="select form-control mandatory fal" runat="server" required="required">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="DOB" class="col-sm-3 control-label">DOB </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control mandatory" ID="txtDOB" runat="server"
                                        placeholder="DOB " required="required" MaxLength="10" />
                                    <ajaxToolkit:CalendarExtender runat="server" Enabled="True" TargetControlID="txtDOB"
                                        Format="MM/dd/yyyy" />
                                </div>

                            </div>
                            <div class="form-group">
                                <label for="inputPassword" class="col-sm-3 control-label">Password </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control mandatory" MaxLength="50" placeholder="Password" ID="txtPassword" runat="server"
                                        required="required" TextMode="Password" />

                                </div>

                            </div>

                            <div class="form-group">
                                <label for="inputPassword" class="col-sm-3 control-label">Retype Password </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control mandatory" MaxLength="50" ID="txtConfirmPassword" runat="server"
                                        required="required" placeholder="Retype Password" TextMode="Password" />

                                </div>

                            </div>

                            <div class="form-group">
                                <label for="Address" class="col-sm-3 control-label">Address 1 </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control" ID="txtAddress1" runat="server" MaxLength="500" />

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="Addres2" class="col-sm-3 control-label">Address 2 </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control" ID="txtAddress2" runat="server" MaxLength="500" />

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="City" class="col-sm-3 control-label">City </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control" ID="txtCity" runat="server" MaxLength="50" />

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="State" class="col-sm-3 control-label">State </label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="ddlState" CssClass="select form-control mandatory fal" runat="server" required="required">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <label for="ZipCode" class="col-sm-3 control-label">ZipCode </label>
                                <div class="col-sm-9">
                                    <asp:TextBox CssClass="form-control" ID="txtZipCode" runat="server" MaxLength="10" />

                                </div>
                            </div>


                            <div class="clear"></div>

                            <asp:Button ID="btnRegister" runat="server" Text="Register"
                                CssClass="btn-success mybtnRegister mybtn centerx" OnClick="btnRegister_Click"
                                OnClientClick="if(!ComparePassword()) return false;" />
                            <div class="form-group loginSpacing">
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </div>

                        </div>
                    </div>
                    <!----end col 12---------->

                </div>


            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
