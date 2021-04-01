<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DevelopersArticle.WebUI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-4 ml-3 mb-5">Son 1 Haftada oluşturulan Yazılar</h2>
    <div class="col-8 center">
        <asp:Repeater ID="rptArticleList" runat="server">
            <ItemTemplate>
                <%# Eval("ArticlePictureURL") %>
                <div class="card mb-3 w-75" >
                    <div class="row g-0">
                        <div class="col-md-4">
                            <asp:Image ID="imgArticle" runat="server" />
                        </div>
                        <div class="col-md-8">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("ArticleTitle") %></h5>
                                <p class="card-text"><%# Eval("ArticleContent") %></p>
                            </div>
                            <div class="card-footer">
                                <small class="text-muted float-left"> Yazar:<%# Eval("WriterId") %></small>
                                <small class="text-muted float-right"> Son Güncelleme Tarihi:<%# Eval("ModifiedDate") %></small>

                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
