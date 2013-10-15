<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Events.aspx.cs" Inherits="EventCalendar.Events" %>

<asp:Content runat="server" ID="EventContent" ContentPlaceHolderID="FeaturedContent">
    
    <h2>Events</h2> 
    <div class="block">
        <section class="calendar">
            <div class="custom-calendar-wrap">
			    <div id="custom-inner" class="custom-inner">
				    <div class="custom-header clearfix">
					    <nav>
						    <span id="custom-prev" class="custom-prev"></span>
						    <span id="custom-next" class="custom-next"></span>
					    </nav>
					    <h2  class="custom-month"><span id="custom-month"></span> <span id="custom-year" ></span></h2>
				    </div>
				    <div id="calendar" class="fc-calendar-container"></div>
			    </div>
                <div id="calpop"></div>
		    </div>
         </section>
    </div>
    
    
    <asp:Literal runat="server" ID="ltlData"></asp:Literal>
    
   
</asp:Content>
