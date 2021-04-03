<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditArticle.aspx.cs" Inherits="DevelopersArticle.WebUI.EditArticle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row card w-75 mx-auto mt-5" runat="server" id="divEdit">
        <div class="card-body ">
            <div class="form-group mt-3">
                <asp:Label Text="Makale Başlığı" runat="server" CssClass="input-group-text d-inline w-25" />
                <asp:TextBox ID="TxtbxArticleTitle" runat="server" CssClass="form-control w-25 d-inline"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Bu alan boş bırakılamaz." ControlToValidate="TxtbxArticleTitle" Display="Dynamic" ForeColor="DarkRed"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label Text="Mevcut Resim : " runat="server" />
                <asp:Image ID="imgCurrent" Style="height: 5rem;" runat="server" />

            </div>
            <div class="form-group">
                <asp:Label Text="Resim ekle (Boş bırakılabilir)" runat="server" CssClass="input-group-text d-inline" />
                <asp:FileUpload ID="FlUpldImageUpload" runat="server" CssClass="form-control-file w-25 d-inline" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.PNG|.JPG|.JPEG|.jfif|.JFIF)$"
                    ControlToValidate="FlUpldImageUpload" runat="server" ErrorMessage="dosya uzantısı .jpg, .png, .jpeg veya .jfif olmalıdır."
                    Display="Dynamic" ForeColor="DarkRed" />
            </div>
            <div class="form-group">
                <asp:Label Text="Makale" runat="server" CssClass="input-group-text txtAreaLabel" />
                <asp:TextBox runat="server" ID="TxtAreaArticleContent" Columns="50" Rows="8"
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
                <asp:Button Text="Değişiklikleri Kaydet" ID="BtnEditArticle" runat="server" OnClick="BtnEditArticle_Click" CssClass="btn btn-success float-right" />
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
            $('.multiple-select').select2();
        });
    </script>
</asp:Content>
