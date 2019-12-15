<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Menu.Master" CodeBehind="QualityProgressionList.aspx.vb" Inherits="ProjectRiskManagementSystem.QualityProgressionList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr style="height:39px">
            <td>
                <%--タイトル--%>
                <label class="List_TopHeadTitle">品質推進会検索結果</label>
                <br/><br/>
            </td>
        </tr>
        <tr style="height:590px">
            <td style="vertical-align : top;">
                <div style="height: 20px" runat="server" id="trHeader" visible="false"></div>
                <%--品質推進会一覧--%>
                <asp:GridView ID="gdvQualityProgressionList" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowHeaderWhenEmpty="True" PageSize="24" CssClass="List_GridViewStyle" Width="1000px">
                    <Columns>
                        <asp:TemplateField HeaderText="会議名称" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" Text='<%# Eval("CONFERENCE_NAME")%>' NavigateUrl='<%# "~/QualityProgression.aspx?progression_no=" & Eval("PROGRESSION_NO") %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="支社" DataField="BRANCH_NAME" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="部門" DataField="TARGET_SECT_NAME" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="対象案件オーダ" DataField="ORDER_CD" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="発信者" DataField="SENDER_USER_NAME" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="開催日時" DataField="OPEN_DATE" DataFormatString="{0: yyyy/MM/dd}" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="開催場所" DataField="OPEN_PLACE" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="議事録<br/>作成済み" DataField="CREATED" HtmlEncode="false" ItemStyle-CssClass="List_GridViewItemStyle" HeaderStyle-CssClass="List_GridViewPjHeaderStyle">
                            <ItemStyle HorizontalAlign="Center" />
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
