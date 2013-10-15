<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Events.aspx.cs" Inherits="EventCalendar.Events" %>

<asp:Content runat="server" ID="EventContent" ContentPlaceHolderID="FeaturedContent">
    
    <h2>Events</h2> 
    <div class="block">
        <asp:Literal runat="server" ID="ltlResults"></asp:Literal>
        <section class="main">
        
            <div class="custom-calendar-wrap">
			    <div id="Div1" class="custom-inner">
				    <div class="custom-header clearfix">
					    <nav>
						    <span id="Span1" class="custom-prev"></span>
						    <span id="Span2" class="custom-next"></span>
					    </nav>
					    <h2  class="custom-month"><span id="Span3"></span> <span id="Span4" ></span></h2>
				    </div>
				    <div id="Div2" class="fc-calendar-container"></div>
			    </div>
                <div id="calpop"></div>
		    </div>
        
         </section>
    </div>
    
    
    <asp:Literal runat="server" ID="ltlData"></asp:Literal>
    
   
</asp:Content>
