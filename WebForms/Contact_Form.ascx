<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact_Form.ascx.cs" Inherits="WebForms.Contact_Form" %>

<asp:Panel ID="Panel1" runat="server">
        <h2>Contact Form</h2>
	    <asp:Label ID="Label1" runat="server">Name: &nbsp;</asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the information requested."
             ControlToValidate="txtName"></asp:RequiredFieldValidator>
        <br />
        <span class="email_input"><asp:Label ID="Label2" runat="server">Email: &nbsp; </asp:Label></span>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter the information requested."
             ControlToValidate="txtEmail"></asp:RequiredFieldValidator>

        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid email address."
             ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">

        </asp:RegularExpressionValidator>
        <br />
        <asp:Label ID="Label3" runat="server">Subject:</asp:Label>
        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter the information requested."
             ControlToValidate="txtSubject"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="Label4" runat="server">Message:</asp:Label>
        <br />
        <textarea ID="txtMessage" runat="server" Rows="7" Columns="24"></textarea>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter a message."
             ControlToValidate="txtMessage"></asp:RequiredFieldValidator>
        <br />
        <asp:Button runat="server" ID="btnSend" Text="Send" OnClick="btnSend_Click" />
        <asp:Button runat="server" ID="btnReset" Text="Reset" OnClick="btnReset_Click" CausesValidation="false" />
</asp:Panel>