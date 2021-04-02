<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="DevelopersArticle.WebUI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mt-4 ml-3 mb-5">Bir Geliştiricinin Günlükleri <i class="fa fa-smile-o" aria-hidden="true"></i></h2>
    <div class="row row-cols-5">
        <asp:Repeater ID="rptArticleList" runat="server">
            <ItemTemplate>
                <div class="rptcard card d-inline mt-4 ml-5 rounded h-100" style="width:18rem;">
                    <div class="card-header">
                        <asp:Image ImageUrl='<%# Eval("ImageUrl") %>' Style="height:10rem;" CssClass="center" runat="server" />
                    </div>

                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("ArticleTitle") %></h5>
                        <p class="card-text"><%# Eval("MakaleOzet") %></p>
                        <a href="..\Article.aspx?id=<%# Eval("ObjectID") %>" class="float-right mb-2"> Yazının Devamı <i class="fa fa-arrow-right" aria-hidden="true"></i></a>
                    </div>
                    <div class="card-footer">
                        <span class="text-muted d-block" style="font-size: 0.8rem;">Yazar: <%# Eval("Developer.FullName") %></span>
                        <span class="text-muted d-block" style="font-size: 0.8rem;">Yorum Sayısı: <%# Eval("Comments.Count") %></span>
                        <span class="text-muted " style="font-size: 0.8rem;">Son Güncelleme Tarihi: <%# Eval("ModifiedDate") %></span>

                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
