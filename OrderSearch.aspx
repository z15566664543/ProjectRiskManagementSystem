<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OrderSearch.aspx.vb" Inherits="ProjectRiskManagementSystem.OrderSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<head runat="server">
    <title>オーダ検索</title>
    <link href="~/Ext/Popup.css" type="text/css" rel="stylesheet" />
    <base target="_self">
</head>

<script>
    //戻り値
    function selectData(data) {
        var rtnValue = new Object();
        for (i = 0; i < 15; i++) {
            var secCd = document.getElementById("gdvOrderList_lnkOrderCd_" + i).value;
            if (secCd && secCd == data) {
                rtnValue.orderCd = document.getElementById("gdvOrderList_lnkOrderCd_" + i).value;
                rtnValue.orderNm = document.getElementById("gdvOrderList_lblOrderNm_" + i).value;
                rtnValue.compyNm = document.getElementById("gdvOrderList_lblCompyNm_" + i).value;
                rtnValue.jyuchuCrr = document.getElementById("gdvOrderList_lblJyuchuCrr_" + i).value;
                rtnValue.simekiriStat = document.getElementById("gdvOrderList_lblSimekiriStat_" + i).value;
                rtnValue.nokiYmd = document.getElementById("gdvOrderList_hdnNokiYmd_" + i).value;
                rtnValue.allSectNm = document.getElementById("gdvOrderList_hdnAllSectNm_" + i).value;
                rtnValue.fullName = document.getElementById("gdvOrderList_hdnFullName_" + i).value;
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
                <td colspan="3">オーダ検索</td>
            </tr>
            <tr>
                <td>
                    <table>
                        <%--本支社ドロップダウンリスト--%>
                        <tr>
                            <td class="retrieval-label">本支社</td>
                            <td class="retrieval-contect">
                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="True" Class="retrieval-droplist-width" DataTextField="A01M002_SECT_NM" DataValueField="A01M002_SECT_CD">
                                </asp:DropDownList>
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
                                <asp:DropDownList ID="ddlDept" runat="server" Class="retrieval-droplist-width" DataTextField="A01M002_SECT_NM" DataValueField="A01M002_SECT_CD"></asp:DropDownList>
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
                        <%--オーダテキストボックス--%>
                        <tr>
                            <td class="retrieval-label">オーダ</td>
                            <td class="retrieval-contect">
                                <asp:TextBox ID="txtOrderCd" name="txtOrderCd" runat="server" MaxLength="10" Class="retrieval-textbox-width"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table>
                        <%--オーダ名称テキストボックス--%>
                        <tr>
                            <td class="retrieval-label">オーダ名称</td>
                            <td class="retrieval-contect">
                                <asp:TextBox ID="txtOrderNm" name="txtOrderNm" runat="server" Class="retrieval-textbox-width" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="400px" valign="top">
                    <%--オーダ一覧GridView--%>
                    <asp:GridView ID="gdvOrderList" runat="server" AllowSorting="True" PageSize="15" AutoGenerateColumns="False" Class="retrieval-GridView" AllowPaging="True" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:TemplateField HeaderText="オーダNo." HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle">
                                <%--隠れ値--%>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandName="Select" OnClientClick='<%# "selectData(" + Chr(39) + Eval("A01M009_ORDER_CD") + Chr(39) + ")"%>' Text='<%# Eval("A01M009_ORDER_CD")%>'></asp:LinkButton>
                                    <asp:HiddenField ID="lnkOrderCd" runat="server"  Value='<%#Eval("A01M009_ORDER_CD")%>'/>
                                    <asp:HiddenField ID="lblOrderNm" runat="server"  Value='<%#Eval("A01M009_ORDER_NM")%>'/>
                                    <asp:HiddenField ID="lblCompyNm" runat="server"  Value='<%#Eval("A01M015_COMPY_NM")%>'/>
                                    <asp:HiddenField ID="lblJyuchuCrr" runat="server"  Value='<%#Eval("A01M014_JYUCHU_CRR")%>'/>
                                    <asp:HiddenField ID="lblSimekiriStat" runat="server"  Value='<%#Eval("A01M014_SIMEKIRI_STAT")%>'/>
                                    <asp:HiddenField ID="hdnNokiYmd" runat="server"  Value='<%#Eval("A01M014_NOKI_YMD")%>'/>
                                    <asp:HiddenField ID="hdnAllSectNm" runat="server"  Value='<%#Eval("A01M002_ALLSECT_NM")%>'/>
                                    <asp:HiddenField ID="hdnFullName" runat="server"  Value='<%#Eval("A01M010_FULLNAME")%>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="A01M009_ORDER_NM" HeaderText="オーダ" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle"/>
                            <asp:BoundField DataField="A01M015_COMPY_NM" HeaderText="顧客" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle"/>
                            <asp:BoundField DataField="A01M014_JYUCHU_CRR" HeaderText="受注金額" DataFormatString="{0:C}" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle"/>
                            <asp:BoundField DataField="A01M014_SIMEKIRI_STAT" HeaderText="締切状態" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="retrieval-GridViewItemStyle" HeaderStyle-CssClass="retrieval-GridViewHeaderStyle"/>
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
