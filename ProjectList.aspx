<%@ Page Language="vb" MasterPageFile="~/Menu.Master" AutoEventWireup="true"  CodeBehind="ProjectList.aspx.vb" Inherits="ProjectRiskManagementSystem.ProjectList" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent"></asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table>
        <tr style="height:39px">
            <td>
                <%--タイトル--%>
                <label class="List_TopHeadTitle">案件検索結果</label>
                <br/><br/>
            </td>
        </tr>
        <tr style="height:590px">
            <td style="vertical-align : top;">
                <div style="height:20px" runat="server" id="trHeader" visible ="false"></div>
                    <%--案件検索結果一覧--%>
                <asp:GridView ID="gdvPjList" runat="server" AllowPaging="True" PageSize="22" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="List_GridViewStyle" Width="1000px">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text='<%# Eval("PROJECT_NO")%>' NavigateUrl='<%# "~/Project.aspx?pj_no=" & Eval("PROJECT_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="A01M002_SECT_NM" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="PRODUCT_SECT_NM" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="PROCESS_NAME" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="ORDER_CD" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="A01M009_ORDER_NM" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="A01M015_COMPY_NM" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="A01M014_JYUCHU_CRR" ItemStyle-HorizontalAlign="right" DataFormatString="{0:C}" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="A01M014_NOKI_YMD" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="CUSTOMER_TYPE_NAME" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="BRANCH_TRANSACTION_FLG" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="SUPPORT_BRANCH_NM" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RPM_500MIL_FLG" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RPM_FIRST_PRODUCT_FLG" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RPM_SPECIAL_PRODUCT_FLG" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RPM_TYPE" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="PROJECT_TYPE_NAME" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RP_P1_COUNT" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RP_P2_COUNT" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RP_P3_COUNT" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="RP_P4_COUNT" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>

                        <asp:BoundField DataField="PROJECT_COMPLETE_FLG" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="List_GridViewItemStyle">
                        </asp:BoundField>
                    </Columns> 
                    <HeaderStyle CssClass="List_GridViewHeaderStyle" Wrap="False" />
                    <RowStyle CssClass="List_GridViewRowStyle" Wrap="False" />
                    <PagerSettings Position="TopAndBottom" />
                    <PagerStyle HorizontalAlign="right"/>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
