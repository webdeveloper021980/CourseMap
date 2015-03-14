<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="CourseMaps.aspx.cs"
    Inherits="CourseMapWeb.Content.CourseMaps" %>

<%@ Register Assembly="BPOrgDiagram" Namespace="BasicPrimitives.OrgDiagram" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <script type="text/javascript" src="../Scripts/PrimitivesJs/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../Scripts/PrimitivesJs/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript" src="../Scripts/PrimitivesJs/json3.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Scripts/PrimitivesJs/ui-lightness/jquery-ui-1.10.2.custom.min.css" />
    <link href="../Scripts/PrimitivesJs/primitives.latest.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/PrimitivesJs/primitives.min.js"></script>
    <style type="text/css">
        #Form {
            width: 794px;
        }

        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 800px;
            height: 409px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelCourseMap"
        DisplayAfter="10" DynamicLayout="True">
        <ProgressTemplate>
            <div class="Loading">
                <img alt="Please Wait Contents are Loading...  "
                    src="../Images/loading.gif" class="LoadingImage" />

            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanelCourseMap" runat="server">
        <ContentTemplate>
            <cc1:OrgDiagramServerControl ID="treeViewCourseDiagram" runat="server" ConnectorType="Angular"
                ShowCheckBoxes="False" OrientationType="Top"
                Width="95%" Style="margin-right: 0; top: 48px; left: 11px; height: 506px;"
                Height="100%" MaximumColumnsInMatrix="8" DotItemsInterval="10" DotLevelShift="10"
                ChildrenPlacementType="Horizontal" OnTemplateButtonClick="treeViewCourseDiagram_TemplateButtonClick" MinimalVisibility="Line">
                <Buttons>
                    <cc1:Button Name="Edit" Icon="Pencil" />

                </Buttons>
            </cc1:OrgDiagramServerControl>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderCourseMap" runat="server" PopupControlID="pnlCourseMap" TargetControlID="btnClose"
                CancelControlID="btnClose" BackgroundCssClass="Background">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlCourseMap" runat="server" CssClass="Popup" align="center" Style="display: none">
                <iframe style="width: 700px; height: 355px;" id="iframePopup" runat="server"></iframe>
                <br />
                <asp:Button ID="btnClose" runat="server" Text="Close" />

            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../Scripts/PrimitivesJs/UserTemplates.js?1029"></script>
</asp:Content>
