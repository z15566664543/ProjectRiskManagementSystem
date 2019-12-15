<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RiskPreventionList.aspx.vb" Inherits="ProjectRiskManagementSystem.RiskPreventionList" MasterPageFile="~/Menu.Master"%>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent"></asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table>
        <tr style="height:39px">
            <td>
                <%--タイトル--%>
                <label class="List_TopHeadTitle">リスク予防管理検討会検索結果</label>
                <br/><br/>
            </td>
        </tr>
        <tr style="height:590px">
            <td style="vertical-align : top;">
                <div style="height:20px" runat="server" id="trHeader" visible ="false"></div>
                <%--検索結果一覧GridView--%>
                <asp:GridView ID="gdvRiskPreventionList" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" AllowPaging="True" PageSize="24" CssClass="List_GridViewStyle" Width="1000px">
                    <Columns>
                        <asp:TemplateField HeaderText="案件番号" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text='<%# Eval("PROJECT_NO")%>' NavigateUrl='<%# "~/RiskPrevention.aspx?pj_no=" & Eval("PROJECT_NO") & "&seq_no=" & Eval("SEQ_NO")%>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="A01M002_SECT_NM" HeaderText="支社<br/>(起票支社)" HtmlEncode="false" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="PRODUCT_SECT_NM" HeaderText="製造部門" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="PROCESS_NAME" HeaderText="プロセス" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="DISCUSS_PHASE_NAME" HeaderText="検討段階" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="ORDER_CD" HeaderText="オーダ" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="A01M009_ORDER_NM" HeaderText="工事名称" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="A01M015_COMPY_NM" HeaderText="顧客" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="SENDER_USER_FULLNAME" HeaderText="発信者" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="OPEN_DATE" HeaderText="開催日時" DataFormatString="{0: yyyy/MM/dd}" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="OPEN_ROUND" HeaderText="回数" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="OPEN_PLACE" HeaderText="開催場所" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                        <asp:BoundField DataField="REVIEWER" HeaderText="議事録<br/>作成済み" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle" />
                    </Columns>
                    <HeaderStyle CssClass="List_GridViewHeaderStyle" Wrap="False" />
                    <RowStyle CssClass="List_GridViewRowStyle" Wrap="False"/>
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle HorizontalAlign="right"/>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>