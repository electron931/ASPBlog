<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <div class="contact_message"><asp:Label ID="MessageLabel" Visible="false" runat="server"></asp:Label></div>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <asp:ListView ID="PostsList" runat="server" SelectMethod="Posts" ItemType="Domain.Entities.Post"> 
       <ItemTemplate>
           <div class="post_item">
               <h1><a href="Post.aspx?id=<%# Item.Id %>"><%# Item.Title  %></a></h1>
               <p><%# Item.ShortDescription %></p>
               <p>
                  <span class="author"><%# Item.Author.Login %></span>
                  <span class="date"><%# Item.PostedOn.ToShortDateString() %></span>
               </p>
               <p><a href="Post.aspx?id=<%# Item.Id %>">Read more...</a></p>
           </div>
       </ItemTemplate>
   </asp:ListView>

    <asp:DataPager runat="server" ID="Pager" PagedControlID="PostsList" PageSize="5">
        <Fields>
            <asp:NextPreviousPagerField ButtonType="Link" NextPageText="next" PreviousPageText="prev" FirstPageText="first"
                 LastPageText="last" />
        </Fields>
    </asp:DataPager>
</asp:Content>
