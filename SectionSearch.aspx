<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SectionSearch.aspx.vb" Inherits="ProjectRiskManagementSystem.SectionSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<head runat="server">
    <title>所属検索</title>
    <link href="~/Ext/Popup.css" type="text/css" rel="stylesheet" />
    <base target="_self" />
</head>

<script>
    //戻り値
    function dataSet(data) {

        var rtnValue = new Object();
        for (i = 0; i <= 15; i++) {
            var sectCd = document.getElementById("gdvSectList_hdnSectCd_" + i).value;
            if (sectCd && sectCd == data) {
                rtnValue.sectCd = document.getElementById("gdvSectList_hdnSectCd_" + i).value;
                rtnValue.branch = document.getElementById("gdvSectList_hdnBranch_" + i).value;
                rtnValue.sectNm = document.getElementById("gdvSectList_hdnSectNm_" + i).value;
                rtnValue.sectAlias = document.getElementById("gdvSectList_hdnSectAlias_" + i).value;
                rtnValue.sectId = document.getElementById("gdvSectList_hdnSectId_" + i).value;
                rtnValue.allSectName = document.getElementById("gdvSectList_hdnAllSectName_" + i).value;
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
                <td colspan="3">所属検索</td>
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
                        <%--所属テキストボックス--%>
                        <tr>
                            <td class="retrieval-label">コード</td>
                            <td class="retrieval-contect">
                                <asp:TextBox ID="txtSectCd" name="txtSectCd" runat="server" class="retrieval-textbox-width" MaxLength="8"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <%--所属名称テキストボックス--%>
                        <tr>
                            <td class="retrieval-label">所属名称</td>
                            <td class="retrieval-contect">
                                <asp:TextBox ID="txtSectNm" name="txtSectNm" runat="server" class="retrieval-textbox-width" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="400" valign="top">
                    <%--所属一覧GridView--%>
                    <asp:GridView ID="gdvSectList" runat="server" AutoGenerateColumns="False" Class="retrieval-GridView" PageSize="15" AllowPaging="True" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:TemplateField HeaderText="所属コード" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle">
                                <%--隠れ値--%>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="Select" OnClientClick='<%# "dataSet(" + Chr(39) + Eval("A01M002_SECT_CD") + Chr(39) + ")"%>' Text='<%# Eval("A01M002_SECT_CD")%>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdnSectCd" runat="server"  Value='<%#Eval("A01M002_SECT_CD")%>'/>
                                    <asp:HiddenField ID="hdnBranch" runat="server"  Value='<%#Eval("BRANCH_NM")%>'/>
                                    <asp:HiddenField ID="hdnSectNm" runat="server"  Value='<%#Eval("A01M002_SECT_NM")%>'/>
                                    <asp:HiddenField ID="hdnSectAlias" runat="server"  Value='<%#Eval("A01M002_SECT_ALIAS")%>'/>
                                    <asp:HiddenField ID="hdnSectId" runat="server"  Value='<%#Eval("A01M002_ID")%>'/>
                                    <asp:HiddenField ID="hdnAllSectName" runat="server"  Value='<%#Eval("A01M002_ALLSECT_NM")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BRANCH_NM" HeaderText="本支社" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
                            <asp:BoundField DataField="A01M002_SECT_NM" HeaderText="所属名称" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
                            <asp:BoundField DataField="A01M002_SECT_ALIAS" HeaderText="別名" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle" />
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
