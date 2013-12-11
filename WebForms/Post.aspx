<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Post.aspx.cs"
     Inherits="WebForms.Post" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    

    <asp:FormView ID="productDetails" runat="server" ItemType="Domain.Entities.Post"
                  SelectMethod ="GetPost" RenderOuterTable="false">
        <ItemTemplate>
            
            <div class="post">
                <h1><%# Item.Title %></h1>
                <div>
                    <p><img class="post_image" src="Images/Posts/<%# Item.Image %>" /></p>
                    <div>
                        <%# Item.Description %>
                    </div>
                    <div class="tags">
                        <asp:ListView ID="ListView2" runat="server" DataSource=<%# Item.Tags %> ItemType="Domain.Entities.Tag">
                            <ItemTemplate>
                                <span><a href="List.aspx?tag=<%# Item.UrlSlug %>"><%# Item.Name %></a></span>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <p>
                      <span class="author"><%# Item.Author.Login %></span>
                      <span class="date"><%# Item.PostedOn.ToShortDateString() %></span>
                   </p>
                    <p class="comments_count"><img src="Images/icon_comments.png" width="31" height="27" />
                        Comments (<%# CommentsCount(Item.Id) %>)</p>
                </div>
            </div>
        </ItemTemplate>
    </asp:FormView>

    <div class="comments">
        <asp:ListView ID="ListView1" runat="server" ItemType="Domain.Entities.Comment" SelectMethod="GetComments">
            <ItemTemplate>
                <div class="comment_item">
                    <p>
                        <span class="userName"><%# Item.User.Login %></span>
                        <span class="created"><%# Item.Created.ToShortDateString() %></span>
                    </p>
                    <p><%# Item.Text %></p>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>

</asp:Content>
