<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="CourseMapWeb.Content.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <section class="section2">
	
    <div class="container transparence">
    	
        <div class="col-sm-12 box extra-work">
        	<p class="pull-right">Welcome <span class="yellow">
                <asp:Label ID="lblUser" runat="server" Text=""></asp:Label></span>
        	<span class="toggle" onclick="return false;"><i class="fa fa-user"></i><i class="fa fa-angle-down"></i></span></p>
            <div class="clear"></div>
        	<h3 class="quotebox">Your<span class="yellow"> Coursemapp</span> at a glance </h3>
            		<div class="clearfix"></div>			
                                
                                <div class="gpa"><p>Gpa</p>
                                	<p><b>3.2</b></p>
                                </div>
                                <div class="clearfix"></div>
                                
            		<div class="list">
                    	
                        <p><a href="CourseMapView.aspx">Course LIst</a></p>
                        <p><a href="CourseMaps.aspx">Course Map</a></p>
                        <p><a href="../Login.aspx">Log Out</a></p>
                    
                    </div>
                    
        
        <article class="tab">
            <div class="col-lg-3">
            <p><a href="#">degree</a></p>
            </div>
            
            <div class="col-lg-3">
            <p><a href="#">core</a></p>
            </div>
            
            <div class="col-lg-3">
            <p><a href="#">concentration</a></p>
            </div>
            
            <div class="col-lg-3">
            <p><a href="#">ce</a></p>
            </div>
            	<div class="clearfix"></div>
        </article> 
        		<div class="clearfix"></div>
                
           
          <article>
          
          	
          	
            	<ul class="left">
                <li><h3><a href="#">Course ID</a></h3></li>
                 <li><p>317</p></li>
                <li><p class="other">Englist2</p></li>
                
                
                
                
                </ul>
                
                <ul  class="left">
                <li><h3><a href="#">requirement</a></h3></li>
                <li><p>Englist2</p></li>
                <li><p class="other">Englist2</p></li>
                
                </ul>
                
                <ul class="left">
                <li><h3><a href="#">Course</a></h3></li>
                <li><p>requirement2</p></li>
                <li><p class="other">Englist2</p></li>
                </ul>
                
                <ul class="left">
                <li><h3><a href="#">grade requirement</a></h3></li>
                <li><p>c</p></li>
                <li><p class="other">Englist2</p></li>
                </ul>
                
                <ul class="left">
                 <li><h3><a href="#">units</a></h3></li>
                <li><p>3</p></li>
                <li><p class="other">Englist2</p></li>
                
                </ul>
                
               
            
            
          
          </article> 
           
                
           
        
        </div>
    
    </div>
    

</section>
</asp:Content>
