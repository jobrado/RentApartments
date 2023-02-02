<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AdminWebForms.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <asp:Repeater ID="rptTags" runat="server">

                    <HeaderTemplate>
                        <table id="myTable" class="table table-striped-columns">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Address</th>
                                    <th scope="col">CreatedAt</th>
                                    <th scope="col">Email</th>
                                    <th scope="col">Phone Number</th>
                                    <th scope="col">Guid</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%#Eval(nameof(DAL.Model.User.Id)) %></th>
                            <td><%#Eval(nameof(DAL.Model.User.UserName)) %></td>
                            <td><%#Eval(nameof(DAL.Model.User.Address)) %></td>
                            <td><%#Eval(nameof(DAL.Model.User.CreatedAt)) %></td>
                            <td><%#Eval(nameof(DAL.Model.User.Email)) %></td>
                            <td><%#Eval(nameof(DAL.Model.User.PhoneNumber)) %></td>
                            <td><%#Eval(nameof(DAL.Model.User.Guid)) %></td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </tbody>
                           </table>
                    </FooterTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
