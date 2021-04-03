<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Developers.aspx.cs" Inherits="DevelopersArticle.WebUI.Developers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-start mt-4 ml-5 ">
        <asp:Label Text="Yazar Ekle" runat="server" CssClass="h2" />
    </div>
    <div class="row">
        <div class="col-8 border bg-light ml-5 mt-3 pl-4 pr-4 pt-4 pb-1 rounded">
            <div class="row ">
                <div class="col-sm">
                    <asp:Label Text="Yazar Adı :" runat="server" CssClass="col-6 col-sm-4 pr-0 pl-4" Font-Bold="true" Font-Size="Medium" />
                    <asp:TextBox ID="TxtYazarAdi" runat="server" CssClass="form-control col-4 col-sm-4 d-inline" ViewStateMode="Disabled" />
                </div>
                <div class="col-sm">
                    <asp:Label Text="Yazar Soyadı :" runat="server" CssClass="col-6 col-sm-4 pr-0 pl-4" Font-Bold="true" Font-Size="Medium" />
                    <asp:TextBox ID="TxtYazarSoyadi" runat="server" CssClass="form-control col-4 col-sm-4 d-inline" ViewStateMode="Disabled" />
                </div>
            </div>
            <div class="row mb-3 ">
                <div class="col">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtYazarAdi" CssClass="d-block ml-5" ErrorMessage="*Bu alan boş bırakılamaz" ValidationGroup="vgAddDeveloper" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                </div>
                <div class="col">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="TxtYazarSoyadi" runat="server" CssClass="ml-5" ErrorMessage="*Bu alan boş bırakılamaz." ValidationGroup="vgAddDeveloper" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:Label Text="Yazar Kullanıcı Adı :" runat="server" CssClass="col-6 col-sm-4 pr-0 pl-4" Font-Bold="true" Font-Size="Medium" />
                    <asp:TextBox ID="TxtYazarUsername" runat="server" CssClass="form-control col-4 col-sm-4 d-inline" ViewStateMode="Disabled" />
                </div>
                <div class="col">
                    <asp:Label Text="Kategori seçimi" runat="server" CssClass="d-inline ml-4" Width="130px" Font-Size="Medium" Font-Bold="true" />
                    <asp:ListBox runat="server" ID="LbCategories" SelectionMode="Multiple" CssClass="multiple-select" Width="50%"></asp:ListBox>
                </div>
            </div>
            <div class="row mb-3 ">
                <div class="col">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtYazarUsername" CssClass="d-block ml-5" ErrorMessage="*Bu alan boş bırakılamaz" ValidationGroup="vgAddDeveloper" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                </div>
                <div class="col">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="LbCategories" runat="server" CssClass="ml-5" ErrorMessage="*En az bir kategori seçilmelidir." ValidationGroup="vgAddDeveloper" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                </div>
            </div>

            <asp:Button Text="Ekle" runat="server" Font-Size="Larger" CssClass="btn btn-success d-inline pl-4 pr-4 float-right" OnClick="AddDeveloper_Click" ValidationGroup="vgAddDeveloper" />

        </div>
    </div>

    <div class="row align-items-start mt-4 ml-5 ">
        <asp:Label Text="Yazarlar" runat="server" CssClass="h2" />
    </div>
    <div class="row">
        <table class="table">
            <asp:Repeater ID="rptDevelopers" runat="server" OnItemDataBound="rptDevelopers_ItemDataBound" OnItemCommand="rptDevelopers_ItemCommand">
                <HeaderTemplate>
                    <tr>
                        <th scope="col"></th>
                        <th scope="col">İsim Soyisim</th>
                        <th scope="col">Kullanıcı Adı</th>
                        <th scope="col">Yazdığı Kategoriler</th>
                        <th scope="col">Yazı Sayısı</th>
                        <th scope="col">Düzenle/Sil</th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HiddenField ID="hfDeveloperId" runat="server" Value='<%# Eval("ObjectId") %>' />
                        </td>
                        <td><%# Eval("FullName") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td>
                            <asp:Repeater ID="rptDeveloperCategories" runat="server">
                                <ItemTemplate>
                                    <%# Container.DataItem  %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:Repeater ID="rptDeveloperArticles" runat="server">
                                <ItemTemplate>
                                    <%#  Container.DataItem %>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkbtn_Edit" runat="server" type="button" class="btn btn-outline-primary d-inline float-left" CommandArgument='<%# Eval("ObjectID") %>' CommandName="Edit" Text="Düzenle" OnClientClick='openModal();' />
                            <asp:Button Text="Sil" ID="btnYazarSil" CommandName="Delete" CausesValidation="false" CssClass="btn btn-outline-danger d-inline float-right" runat="server" CommandArgument='<%# Eval("ObjectID") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>

    <div class="modal fade" id="EditModal2" tabindex="-1" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Yazar Düzenle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfDeveloperId" runat="server" />
                    <div class="form-group">
                        <label class="col-form-label">Yazar Adı:</label>
                        <asp:TextBox ID="txtDevName" CssClass="form-control" runat="server" EnableViewState="false" />
                        <asp:RequiredFieldValidator ErrorMessage="*Boş bırakılamaz." ControlToValidate="txtDevName"
                            ValidationGroup="vgEditModal" runat="server" />

                        <label class="col-form-label">Yazar Soyadı:</label>
                        <asp:TextBox ID="txtDevLastname" CssClass="form-control" runat="server" EnableViewState="false" />
                        <asp:RequiredFieldValidator ErrorMessage="*Boş bırakılamaz." ControlToValidate="txtDevLastname"
                            ValidationGroup="vgEditModal" runat="server" />

                        <label class="col-form-label">Yazar Kullanıcı Adı:</label>
                        <asp:TextBox ID="txtDevUsername" CssClass="form-control" runat="server" EnableViewState="false" />
                        <asp:RequiredFieldValidator ErrorMessage="*Boş bırakılamaz." ControlToValidate="txtDevUsername"
                            ValidationGroup="vgEditModal" runat="server" />
                        <div class="alert alert-info" role="alert">
                            Yazarın Sadece Yazısı Bulunmayan Kategorileri değiştirilebilir. 
                        </div>
                         <asp:ListBox runat="server" ID="LbEditCategories" SelectionMode="Multiple" CssClass="multiple-select" Width="50%"></asp:ListBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:LinkButton Text="Değişiklikleri Kaydet" ID="btnModalSave" CssClass="btn btn-primary" runat="server" ValidationGroup="vgEditModal" OnClick="btnModalSave_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
            $('.multiple-select').select2();
        });
    </script>
</asp:Content>
