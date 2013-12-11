<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs"
     Inherits="WebForms.List" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ListView ID="ListView1" runat="server" SelectMethod="PostsByTag" ItemType="Domain.Entities.Post"> 
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

</asp:Content>
