<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" UnobtrusiveValidationMode="None" CodeBehind="Update.aspx.cs" Inherits="AdminWebForms.Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-3.6.3.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Name:</asp:Label>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:TextBox ID="tbName" runat="server" class="form-control"></asp:TextBox>
    </div>

    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Owner:</asp:Label>
        <asp:DropDownList ID="ddlOwner" runat="server" class="form-control"></asp:DropDownList>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Status:</asp:Label>
        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control"></asp:DropDownList>
    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">City:</asp:Label>
        <asp:DropDownList ID="ddlCity" runat="server" class="form-control"></asp:DropDownList>
    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Address:</asp:Label>
        <asp:TextBox ID="tbAddress" runat="server" class="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAddress" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Price:</asp:Label>

        <asp:TextBox ID="tbPrice" runat="server" class="form-control"></asp:TextBox>
        <asp:CompareValidator runat="server" ErrorMessage="Must be currency" ControlToValidate="tbPrice" Type="Currency" ForeColor="Red" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPrice" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Maximum of adults:</asp:Label>
        <asp:TextBox ID="tbAdults" runat="server" class="form-control"></asp:TextBox>
        <asp:CompareValidator ForeColor="Red" runat="server" ErrorMessage="Must be int" ControlToValidate="tbAdults" Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="tbAdults" ErrorMessage="*" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Maximum of children:</asp:Label>

        <asp:TextBox ID="tbChildren" runat="server" class="form-control"></asp:TextBox>
        <asp:CompareValidator ForeColor="Red" runat="server" ErrorMessage="Must be int" ControlToValidate="tbChildren" Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="tbChildren" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Total rooms:</asp:Label>
        <asp:TextBox ID="tbTotalRooms" runat="server" class="form-control"></asp:TextBox>
        <asp:CompareValidator ForeColor="Red" runat="server" ErrorMessage="Must be int" ControlToValidate="tbTotalRooms" Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="tbTotalRooms" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Beach distance:</asp:Label>
        <asp:TextBox ID="tbBeachDistance" runat="server" class="form-control"></asp:TextBox>
        <asp:CompareValidator ForeColor="Red" runat="server" ErrorMessage="Must be int" ControlToValidate="tbBeachDistance" Type="Integer" Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="tbBeachDistance" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

    </div>
    <div class="mb-3">
        <asp:Label runat="server" class="form-label">Tags:</asp:Label>

        <asp:ListBox runat="server" SelectionMode="Multiple" ID="lbTags"></asp:ListBox>

    </div>
  

       <div class="container">
        <div class="form-group">
            <label class="btn btn-default">
                Upload slika
 <asp:FileUpload ID="uplImages" runat="server" CssClass="hidden" AllowMultiple="true" OnChange="handleFileSelect(this.files);" />
            </label>
            <div id="uplImageInfo"></div>
            <script>
                function handleFileSelect(files) {
                    for (var i = 0; i < files.length; i++) {
                        $span = $("<span class='label label-info'></span>").text(files[i].name);
                        $('#uplImageInfo').append($span);
                        $('#uplImageInfo').append("<br />");
                    }
                }
            </script>
        </div>
    </div>


    <asp:Repeater ID="repApartmentPictures" runat="server">
        <ItemTemplate>
            <div class="form-group">
                <div class="row">
                    <asp:HiddenField runat="server" ID="hidApartmentPictureId" Value='<%# Eval("ID") %>' />
                    <div class="col-md-3">
                        <a href="<%# Eval("Path") %>">
                            <asp:Image ID="imgApartmentPicture" runat="server" CssClass="img-thumbnail" ImageUrl='<%# Eval("Path") %>' />
                        </a>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:TextBox ID="txtApartmentPicture" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>'></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="btn btn-success">
                                Glavna slika
 <asp:CheckBox ID="cbIsRepresentative" runat="server" CssClass="is-representative-picture" Checked="false" />
                            </label>
                        </div>
                        <div class="form-group">
                            <label class="btn btn-danger">
                                <span class="glyphicon glyphicon-trash"></span>
                                <asp:CheckBox ID="cbDelete" runat="server" Checked="false" />
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>


    <script>
        $(function () {
            var repPicCheckboxes = $(".is-representative-picture > input[type=checkbox]");
            repPicCheckboxes.change(function () {
                currentCheckbox = this;
                if (currentCheckbox.checked) {
                    repPicCheckboxes.each(function () {
                        otherCheckbox = this
                        if (currentCheckbox != otherCheckbox && otherCheckbox.checked) {
                            otherCheckbox.checked = false;
                        }
                    })
                }
            });
        })
    </script>






    <div class="mb-3">
        <asp:Button class="btn btn-primary" OnClick="btnSubmit_Click" ID="btnSubmit" Text="Update" runat="server" type="submit" />
    </div>




</asp:Content>
