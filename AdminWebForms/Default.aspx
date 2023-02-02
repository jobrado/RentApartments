<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" UnobtrusiveValidationMode="None" CodeBehind="Default.aspx.cs" Inherits="AdminWebForms.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btnadd {
            margin-bottom: 211px;
            margin-right: 21%;
            margin-bottom: 5px;
            float: right;
        }

        .btndelete {
            z-index: 0;
            position: relative;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Button runat="server" Width="7%" class="btn btn-outline-dark btnadd" OnClick="btnAdd_Click" ID="btnAdd" Text="Add" />
    </div>
    <div class="col-md-6">
        <asp:Label runat="server" ID="lbluser" Text="Enter user id:" Visible="false">   </asp:Label>
        <asp:Label runat="server" ID="lblError" ForeColor="Red" Text="Enter user id!!!" Visible="false">   </asp:Label>

        <asp:RegularExpressionValidator runat="server" ControlToValidate="tbUser" ErrorMessage="Only numbers are allowed" ValidationExpression="^[0-9]+$">></asp:RegularExpressionValidator>
        <asp:TextBox runat="server" ID="tbUser" Visible="false"></asp:TextBox>
        <asp:Label runat="server" ID="lblStatus" Text="Enter new status:" Visible="false"></asp:Label>
        <asp:DropDownList AutoPostBack="true" runat="server" Visible="false" ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Value="Izaberi"></asp:ListItem>
            <asp:ListItem Value="Zauzeto"></asp:ListItem>
            <asp:ListItem Value="Slobodno"></asp:ListItem>
            <asp:ListItem Value="Rezervirano"></asp:ListItem>
        </asp:DropDownList>

    </div>
    <div class="container">

        <div class="row">
            <div class="col-md-6">
                <asp:Repeater ID="rptTags" runat="server">
                    <HeaderTemplate>
                        <table id="myTable" class="table table-striped-columns  table-hover">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Price</th>
                                    <th scope="col">City</th>
                                    <th scope="col">Address</th>
                                    <th scope="col">Status</th>
                                    <th scope="col">Person who reserved</th>
                                    <th scope="col">Adults</th>
                                    <th scope="col">Children</th>
                                    <th scope="col">Total rooms</th>
                                    <th scope="col">Beach distance</th>
                                    <th scope="col">Owner</th>
                                    <th scope="col">Update apartment</th>
                                    <th scope="col">Delete apartment</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%#Eval(nameof(DAL.Model.Apartment.IDApartment)) %></th>
                            <td><%#Eval(nameof(DAL.Model.Apartment.Name)) %></td>
                            <td><%#String.Format("{0:0.##}",Eval(nameof(DAL.Model.Apartment.Price))) %> €</td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.CityId)) %></td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.Address)) %></td>
                            <td>
                                <asp:LinkButton CommandArgument="<%#Eval(nameof(DAL.Model.Apartment.IDApartment)) %>" class="link-info btndelete" ID="lbStatus" OnClick="lbStatus_Click" runat="server">
                                <%#Eval(nameof(DAL.Model.Apartment.StatusId)) %>
                                </asp:LinkButton>
                            </td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.Ar)) %></td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.MaxAdults)) %></td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.MaxChildren)) %></td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.TotalRooms)) %></td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.BeachDistance)) %></td>
                            <td><%#Eval(nameof(DAL.Model.Apartment.OwnerId)) %></td>
                            <td>
                                <asp:Button Text="Update" ID="btnUpdate" OnClick="btnUpdate_Click" class="btn btn-outline-dark btndelete" CommandArgument="<%#Eval(nameof(DAL.Model.Apartment.IDApartment)) %>" runat="server" />
                            </td>
                            <td>
                                <asp:Button Text="Delete" ID="btnDelete" OnClick="btnDelete_Click" class="btn btn-danger btndelete" CommandArgument="<%#Eval(nameof(DAL.Model.Apartment.IDApartment)) %>" runat="server" />
                            </td>
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
