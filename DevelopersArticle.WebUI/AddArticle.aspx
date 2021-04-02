<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddArticle.aspx.cs" Inherits="DevelopersArticle.WebUI.AddArticle" ViewStateMode="Enabled" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/AddArticleCss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="LblInfo" runat="server" CssClass="alert alert-info mt-3" Visible="false"></asp:Label>

    <div class="row card w-75 mx-auto mt-5">
        <div class="card-header">
            <asp:Label Text="Makale Ekle" runat="server" CssClass="card-title" Font-Size="Larger" Font-Bold="true" />
        </div>
        <div class="card-body ">

            <div class="form-group mt-3">
                <asp:Label Text="Makale Başlığı" runat="server" CssClass="input-group-text d-inline w-25" />
                <asp:TextBox ID="TxtbxArticleTitle" runat="server" CssClass="form-control w-25 d-inline"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Bu alan boş bırakılamaz." ControlToValidate="TxtbxArticleTitle" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label Text="Resim ekle" runat="server" CssClass="input-group-text d-inline" />
                <asp:FileUpload ID="FlUpldImageUpload" runat="server" CssClass="form-control-file w-25 d-inline" />
                <asp:RequiredFieldValidator ErrorMessage="*Bu alan Boş bırakılamaz" ControlToValidate="FlUpldImageUpload"
                    runat="server" Display="Dynamic" ForeColor="DarkRed" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.PNG|.JPG|.JPEG|.jfif|.JFIF)$"
                    ControlToValidate="FlUpldImageUpload" runat="server" ErrorMessage="dosya uzantısı .jpg, .png, .jpeg veya .jfif olmalıdır."
                    Display="Dynamic" ForeColor="DarkRed" />
            </div>
            <div class="form-group">
                <asp:Label Text="Makale" runat="server" CssClass="input-group-text txtAreaLabel" />
                <asp:TextBox runat="server" ID="TxtAreaArticleContent" Columns="50" Rows="5"
                    TextMode="MultiLine" CssClass="textarea form-control-plaintext w-100 d-inline" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Bu alan boş bırakılamaz." ControlToValidate="TxtAreaArticleContent" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">

                <asp:Label Text="Kategori seçimi (Birden fazla seçim yap&#305;labilir)" runat="server" CssClass="categorySelectLabel input-group-text" />
                <asp:ListBox runat="server" ID="LbCategories" SelectionMode="Multiple" CssClass="multiple-select" Width="30%"></asp:ListBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="LbCategories" InitialValue="" runat="server" ErrorMessage="*En az bir tane kategori seçilmelidir." ForeColor="DarkRed"></asp:RequiredFieldValidator>

            </div>

            <div class="form-group">
                <asp:Label Text="Yazar Seçiniz" runat="server" CssClass="developerSelectLabel input-group-text" />
                <asp:DropDownList ID="DdlDevelopers" runat="server" DataTextField="FullName" DataValueField="ObjectID" CssClass="form-control w-25">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Button Text="Oluştur" ID="BtnAddArticle" runat="server" OnClick="BtnAddArticle_Click" CssClass="btn btn-success float-right" />
            </div>
        </div>
    </div>

    <script>

        $(document).ready(function () {
            $('.multiple-select').select2();
        });
    </script>

</asp:Content>
