<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="AdminWebForms.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/bootstrap.min.js"></script>
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
                                    <th scope="col">Guid</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <div class="repeater-data" data-index="<%# Container.ItemIndex %>">
                            <tr>
                                <th scope="row"><%#Eval(nameof(DAL.Model.Tag.Id)) %></th>
                                <td><%#Eval(nameof(DAL.Model.Tag.Name)) %></td>
                                <td><%#Eval(nameof(DAL.Model.Tag.Guid)) %></td>
                                <td>
                                    <asp:Button Text="Delete" ID="btnDelete" OnClientClick="return confirm('Are you sure you want to delete this item?');" OnClick="btnDelete_Click" class="btn btn-danger btndelete" CommandArgument="<%#Eval(nameof(DAL.Model.Tag.Id)) %>" runat="server" >
                                    
                                        </asp:Button>

                                </td>
                            </tr>
                        </div>
                    </ItemTemplate>

                    <FooterTemplate>
                        </tbody>
                           </table>
                    </FooterTemplate>

                </asp:Repeater>
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                <div class="mt-3">
                    <label>Name:</label><br />
                    <asp:TextBox ID="tbName" runat="server"></asp:TextBox><br />
                    <label class="mt-3">Name in english:</label><br />
                    <asp:TextBox ID="tbNameEng" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text="Add" class="btn btn-outline-dark m-3" />

                </div>
            </div>
        </div>
    </div>

</asp:Content>
