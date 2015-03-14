<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="CourseMapView.aspx.cs" Inherits="CourseMapWeb.Content.CourseMapView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
      <section class="section2">
	
    <div class="container transparence">
     <div class="col-sm-12 box extra-work">
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <asp:GridView ID="GridViewCourseMaps" runat="server" Width="98%"  AutoGenerateColumns="False" DataKeyNames="CourseId" HorizontalAlign="Left" ShowHeaderWhenEmpty="True" OnRowCommand="GridViewCourseMaps_RowCommand" OnRowDataBound="GridViewCourseMaps_RowDataBound">
        <Columns>
            <asp:BoundField DataField="CourseId" HeaderText="Course Id" />
            <asp:BoundField DataField="CourseName" HeaderText="Course Name" />
            <asp:BoundField DataField="ElectiveName" HeaderText="Elective Name" />
            <asp:BoundField DataField="PassingGrade" HeaderText="Min Grade" />
            <asp:TemplateField HeaderText="Grade Received">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlGradeReceived"  runat="server" >
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                        <asp:ListItem Text="D" Value="D"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>


            <asp:BoundField DataField="Unit" HeaderText="Unit" />
            <asp:TemplateField HeaderText="Grade Received">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlStatus"  runat="server" >
                        <asp:ListItem Text="Select One" Value=""></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="Completed"></asp:ListItem>
                        <asp:ListItem Text="Incomplete" Value="Incomplete"></asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnI" runat="server" Text="I" CssClass="btn-success mybtnRegister mybtn centerx" CommandName="I"  UseSubmitBehavior="False"
                         CommandArgument='<%#Eval("CourseId")%>'/>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-success mybtnRegister mybtn centerx" CommandName="UpdateRow" 
                         CommandArgument='<%#Eval("CourseId")%>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="GradeRecieved" Visible="False" />
            <asp:BoundField DataField="CourseStatus" Visible="False" />
        </Columns>
    </asp:GridView>
         </div>
        </div></section>
</asp:Content>
