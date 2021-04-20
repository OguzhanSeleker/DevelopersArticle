<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Article.aspx.cs" Inherits="DevelopersArticle.WebUI.Article" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row card w-50 mx-auto mt-5 mb-5 p-3v active" id="divArticle" runat="server">

        <div class="card-body">
            <asp:HiddenField ID="hfArticleId" runat="server" />
            <div class="row">
                <div class="col mb-3">
                    <asp:Image ID="imgArticleImage" Style="height: 18rem;" CssClass="center" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-10">
                    <asp:Label ID="lblTitle" runat="server" CssClass="card-title mb-5 d-block" Font-Size="XX-Large" Font-Bold="true" />
                </div>
                <div class="col-2">
                    <asp:LinkButton ID="lbDelArticle" OnClick="lbDelArticle_Click" CssClass="d-inline float-right m-2" runat="server">
                    <i class="fas fa-trash fa-lg"></i>
                    </asp:LinkButton>
                    <asp:HyperLink ID="hlEditArticle" CssClass="d-inline float-right m-2" runat="server">
                    <i class="fas fa-edit fa-lg"></i>
                    </asp:HyperLink>
                </div>
            </div>

            <asp:Label ID="lblContent" CssClass="mt-3 ml-5 mr-3" Font-Size="Large" runat="server" />
            <div class="row">

                <asp:Label ID="lblWriterFullName" CssClass="ml-4 mt-2 text-muted float-right d-block" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblModifiedDate" CssClass="ml-4 mt-2 text-muted float-right d-block" runat="server" />
            </div>
            <asp:Repeater ID="rptArtCats" runat="server">
                <ItemTemplate>
                    <span class="badge bg-warning text-dark ml-2"><%# Container.DataItem %></span>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <div class="card-footer">
            <div class="row ">
                <div class="col">

                    <asp:Label Text="Yorumlar" runat="server" CssClass="h5 d-inline float-left ml-3 mt-2" />
                </div>
                <div class="col">
                    <asp:Button ID="btnShowCommentAdd" CssClass="btn btn-dark btn-sm d-inline float-right" runat="server" OnClientClick='ShowAddComment(); return false;' Text="Yorum Ekle" AutoPostBack="false" />
                </div>
            </div>
            <div class="row">
                <div class="col ml-5">
                    <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand">
                        <ItemTemplate>
                            <hr />
                            
                            <%# Eval("CommentContent") %> --
                            <span class="text-primary"><%# Eval("Developer.FullName") %>.</span>
                            <span class="text-muted"><%# Eval("CreatedDate") %></span>
                            <asp:LinkButton ID="lbDelComment" CommandName="Delete" CommandArgument='<%# Eval("ObjectID") %>' CssClass="d-inline ml-2" runat="server">
                    <i class="fas fa-trash"></i>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lbEditComment" CommandName="Edit" CommandArgument='<%# Eval("ObjectID") %>' CssClass="d-inline ml-2" runat="server" data-bs-toggle="modal" data-bs-target="#EditComment">
                    <i class="fas fa-edit"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <!-- Modal -->
            <div class="modal fade" id="EditComment" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="hfCommentId" runat="server" />
                            <div class="col">
                                <asp:Label CssClass="form-text d-inline" Text="Yorum :" runat="server" />
                                <asp:TextBox ID="tbEditContent" CssClass="form-control-sm w-75" TextMode="MultiLine" runat="server" />
                                <asp:RequiredFieldValidator ErrorMessage="*Bu alan boş bırakılamaz." Font-Size="Small" ForeColor="DarkRed" ControlToValidate="tbEditContent" runat="server" ValidationGroup="editModal" />
                            </div>
                            <div class="col">
                                <asp:Label CssClass="form-text d-inline" Text="Yorum Sahibi :" runat="server" />

                                <asp:DropDownList ID="ddlModalWriter" runat="server" DataTextField="FullName" DataValueField="ObjectID" CssClass="form-control d-inline w-50">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSaveChanges" runat="server" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" Text="Değişiklikleri Kaydet" ValidationGroup="editModal"/>
                        </div>
                    </div>
                </div>
            </div>


            <div class="row mt-3" id="comment" style="display: none;">
                <hr />
                <div class="col">
                    <asp:Label CssClass="form-text d-inline" Text="Yorum :" runat="server" />
                    <asp:TextBox ID="txtbxCommentContent" CssClass="form-control-sm w-50" TextMode="MultiLine" runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="*Bu alan boş bırakılamaz." Font-Size="Small" ForeColor="DarkRed"  ControlToValidate="txtbxCommentContent" runat="server" ValidationGroup="AddComment" />
                </div>
                <div class="col">
                    <asp:Label CssClass="form-text d-inline" Text="Yorum Sahibi :" runat="server" />

                    <asp:DropDownList ID="ddlCommentWriter" runat="server" DataTextField="FullName" DataValueField="ObjectID" CssClass="form-control d-inline w-50">
                    </asp:DropDownList>
                </div>
                <div class="col">
                    <asp:Button ID="btnAddComment" Text="Ekle" CssClass="btn btn-outline-primary btn-sm" runat="server" OnClick="btnAddComment_Click" ValidationGroup="AddComment" />
                </div>
            </div>

        </div>
    </div>
    <script>
        function ShowAddComment() {
            var x = document.getElementById("comment");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
    </script>
</asp:Content>
