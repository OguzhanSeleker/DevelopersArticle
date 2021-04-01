<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="DevelopersArticle.WebUI.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/CategoryCss.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div class="row gx-5">
        <div class="col">
            <asp:Label Text="Kategori Ekle" CssClass="p-3 border rounded bg-light" runat="server" />
        </div>
    </div>--%>

    <div class="row align-items-start mt-4 ml-5 ">
        <asp:Label Text="Kategori Ekle" runat="server" CssClass="h2" />
    </div>
    <div class="row">
        <div class="col-4 border bg-light ml-5 mt-3 p-4 rounded">
            <asp:Label Text="Kategori Adı :" runat="server" CssClass="col-6 col-sm-4 h5 pr-0 pl-4" />
            <asp:TextBox ID="TxtKategoriAdi" runat="server" CssClass="form-control col-6 col-sm-4 d-inline" ViewStateMode="Disabled" />

            <asp:Button Text="Ekle" runat="server" CssClass="btn btn-success d-inline pl-4 pr-4 float-right" OnClick="AddCategory_OnClick" ValidationGroup="vgAddCategory"/>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtKategoriAdi" CssClass="d-block text-center" ErrorMessage="*Bu alan boş bırakılamaz" ValidationGroup="vgAddCategory"></asp:RequiredFieldValidator>

        </div>
    </div>
    <div class="row align-items-start mt-4 ml-5 ">
        <asp:Label Text="Kategoriler" runat="server" CssClass="h2" />
    </div>
    <div class="row row-cols-5">
        <asp:Repeater ID="rptKategoriler" runat="server" OnItemDataBound="rptKategoriler_ItemDataBound" OnItemCommand="rptKategoriler_ItemCommand">
            <ItemTemplate>
                <div class="card ml-5 mt-3" style="width: 15rem; border-radius: 15px;">
                    <div class="card-header">
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("ObjectID").ToString() %>' />
                        <span class="d-block text-center h5">Kategori adı  </span>
                        <asp:Label Text='<%# Eval("CategoryName") %>' runat="server" CssClass="d-block text-center lead mb-3" />
                    </div>
                    <div class="card-body">
                        <asp:Label Text="Kategoride Yazanlar" runat="server" CssClass="lbltext d-block mt-2 text-center" />

                        <asp:Repeater ID="rptDevelopersInCategory" runat="server">
                            <ItemTemplate>
                                <asp:Label Text='<%# Container.DataItem %>' runat="server" CssClass="d-block text-center lead mb-3" />

                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Label Text="Kategoriye Ait Yazılar" runat="server" CssClass="lbltext d-block mt-1 text-center" />
                        <asp:Repeater ID="rptArticlesInCategory" runat="server">
                            <ItemTemplate>
                                <asp:Label Text='<%# Container.DataItem %>' runat="server" CssClass="d-block text-center lead mb-2" />
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                    <div class="card-footer">
                        <asp:LinkButton ID="lnkbtn_Edit" runat="server" type="button" class="btn btn-outline-primary d-inline float-left" CommandArgument='<%# Eval("ObjectID") %>' Text="Düzenle" OnClientClick='openModal();' />
                        <asp:Button Text="Sil" ID="btnKategoriSil" CommandName="Delete" CausesValidation="false" CssClass="btn btn-outline-danger d-inline float-right" runat="server" CommandArgument='<%# Eval("ObjectID") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="modal fade" id="EditModal" tabindex="-1" aria-labelledby="exampleModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Kategori Düzenle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="col-form-label">Kategori Adı:</label>
                        <asp:TextBox ID="txtEditModal" CssClass="form-control" runat="server" EnableViewState="false" />
                        <asp:HiddenField ID="hfCategoryId" runat="server" />
                        <asp:RequiredFieldValidator ErrorMessage="*Boş bırakılamaz." ControlToValidate="txtEditModal"
                             ValidationGroup="vgEditModal" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:LinkButton Text="Değişiklikleri Kaydet" ID="btnModalSave" CssClass="btn btn-primary" runat="server" ValidationGroup="vgEditModal" OnClick="btnKategoriDuzenle_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

