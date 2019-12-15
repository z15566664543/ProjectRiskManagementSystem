<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserSearch.aspx.vb" Inherits="ProjectRiskManagementSystem.UserSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<head runat="server">
    <title>ユーザー検索</title>
    <link href="~/Ext/Popup.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
</head>

<script>
    //戻り値
    function dataSet(data) {
        
        var rtnValue = new Object();
        for (i = 0; i <= 15; i++) {
            var userCd = document.getElementById("gdvUserList_hdnUserCd_" + i).value;
            if (userCd && userCd == data) {
                rtnValue.userCd = document.getElementById("gdvUserList_hdnUserCd_" + i).value;
                rtnValue.fullNm = document.getElementById("gdvUserList_hdnFullNm_" + i).value;
                rtnValue.branch = document.getElementById("gdvUserList_hdnBranchNm_" + i).value;
                rtnValue.sect = document.getElementById("gdvUserList_hdnSectNm_" + i).value;
                rtnValue.postCls = document.getElementById("gdvUserList_hdnPostclsNm_" + i).value;
                rtnValue.userId = document.getElementById("gdvUserList_hdnUserId_" + i).value;
                rtnValue.allSectName = document.getElementById("gdvUserList_hdnAllSectName_" + i).value;
                rtnValue.sectCd = document.getElementById("gdvUserList_hdnSectCd_" + i).value;
                rtnValue.sectId = document.getElementById("gdvUserList_hdnSectId_" + i).value;
                break;
            }
        }
        window.returnValue = rtnValue;
        this.close();
    }
</script>

<body>
    <form id="form1" runat="server">
        <table class="retrieval-master" cellpadding="4" rules="none" align=center>
            <tr class="retrieval-master-tr">
                <td colspan="3">ユーザー検索</td>
            </tr>
            <tr>
                <td>
                    <table>
                        <%--本支社ドロップダウンリスト--%>
                        <tr>
                            <td class="retrieval-label">本支社</td>
                            <td class="retrieval-contect">
                                <asp:DropDownList ID="ddlBranch" name="ddlBranch" runat="server" AutoPostBack="True" class="retrieval-droplist-width" DataTextField="A01M002_SECT_NM" DataValueField="A01M002_SECT_CD"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <%--部ドロップダウンリスト--%>
                        <tr>
                            <td class="retrieval-label">部</td>
                            <td class="retrieval-contect">
                                <asp:DropDownList ID="ddlDept" name="ddlDept" runat="server" AutoPostBack="True" class="retrieval-droplist-width" DataTextField="A01M002_SECT_NM" DataValueField="A01M002_SECT_CD"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td rowspan="2" align="center">
                    <%--表示ボタン--%>
                    <asp:Button ID="btnSearch" name="btnSearch" runat="server" Text="表示" />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <%--ユーザーテキストボックス--%>
                        <tr>
                            <td class="retrieval-label">ユーザーID</td>
                            <td class="retrieval-contect">
                                <asp:TextBox ID="txtUserCd" name="txtUserCd" runat="server" class="retrieval-textbox-width" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <%--ユーザー名称テキストボックス--%>
                        <tr>
                            <td class="retrieval-label">氏名</td>
                            <td class="retrieval-contect">
                                <asp:TextBox ID="txtFullName" name="txtFullName" runat="server" class="retrieval-textbox-width" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="400" valign="top">
                    <%--ユーザー一覧GridView--%>
                    <asp:GridView ID="gdvUserList" runat="server" AutoGenerateColumns="False" Class="retrieval-GridView" PageSize="15" AllowPaging="True" ShowHeaderWhenEmpty="true" >
                        <Columns>
                            <asp:TemplateField HeaderText="ユーザーID" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle">
                                <%--隠れ値--%>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="Select" OnClientClick='<%# "dataSet(" + Chr(39) + Eval("A01M010_USER_CD") + Chr(39) + ")"%>' Text='<%# Eval("A01M010_USER_CD")%>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnUserCd" runat="server"  Value='<%#Eval("A01M010_USER_CD")%>'/>
                                    <asp:HiddenField ID="hdnFullNm" runat="server"  Value='<%#Eval("A01M010_FULLNAME")%>'/>
                                    <asp:HiddenField ID="hdnBranchNm" runat="server"  Value='<%#Eval("BRANCH_NAME")%>'/>
                                    <asp:HiddenField ID="hdnSectNm" runat="server"  Value='<%#Eval("A01M002_SECT_NM")%>'/>
                                    <asp:HiddenField ID="hdnPostclsNm" runat="server"  Value='<%#Eval("A01M003_POSTCLS_NM")%>'/>
                                    <asp:HiddenField ID="hdnUserId" runat="server"  Value='<%#Eval("A01M010_ID")%>'/>
                                    <asp:HiddenField ID="hdnAllSectName" runat="server"  Value='<%#Eval("A01M002_ALLSECT_NM")%>'/>
                                    <asp:HiddenField ID="hdnSectCd" runat="server"  Value='<%#Eval("A01M002_SECT_CD")%>'/>
                                    <asp:HiddenField ID="hdnSectId" runat="server"  Value='<%#Eval("A01M002_ID")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="A01M010_FULLNAME" HeaderText="名前" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
                            <asp:BoundField DataField="BRANCH_NAME" HeaderText="本支社" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
                            <asp:BoundField DataField="A01M002_SECT_NM" HeaderText="所属" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
                            <asp:BoundField DataField="A01M003_POSTCLS_NM" HeaderText="役職" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
                        </Columns>
                        <PagerSettings Position="TopAndBottom" />
                        <PagerStyle HorizontalAlign="right" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
