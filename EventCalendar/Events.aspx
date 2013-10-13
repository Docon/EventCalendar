<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Events.aspx.cs" Inherits="EventCalendar.Events" %>

<asp:Content runat="server" ID="EventContent" ContentPlaceHolderID="FeaturedContent">
    
    <h2>Events</h2> 
    <div class="block">
        <asp:Literal runat="server" ID="ltlResults"></asp:Literal>
        <section class="main">
        <div class="custom-calendar-wrap">
			<div id="custom-inner" class="custom-inner">
				<div class="custom-header clearfix">
					<nav>
						<span id="custom-prev" class="custom-prev"></span>
						<span id="custom-next" class="custom-next"></span>
					</nav>
					<h2 id="custom-month" class="custom-month"></h2>
					<h3 id="custom-year" class="custom-year"></h3>
				</div>
				<div id="calendar" class="fc-calendar-container"></div>
			</div>
		</div>
         </section>
        
        
    </div>
    
    
    <asp:Literal runat="server" ID="ltlData"></asp:Literal>
    
   
</asp:Content>
