<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Menu.Master" CodeBehind="RiskPrevention.aspx.vb" Inherits="ProjectRiskManagementSystem.RiskPrevention" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register assembly="ProjectRiskManagementSystem" namespace="ProjectRiskManagementSystem.App" tagprefix="app" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <script type="text/javascript">

        // 開催案内ボタンクリックイベント
        function copyToClipBoard() {
            if (window.clipboardData) {
                var str = '件名：【' + document.getElementById('<%=txtOrderNm.ClientID%>').value + '】リスク予防管理検討会実施の件' + '\n' + '\n' +

                    '頭書の件、以下の通りリスク予防管理検討会を実施いたしますのでご参集のほどよろしくお願いいたします。　了' + '\n' + '\n' + '\n' +

                    '　　　　　　　　　　　　　　　記' + '\n' + '\n' +

                    '1. 開催日時　　　　　' + document.getElementById('<%=txtOpenDate.ClientID%>').value + '　' +
                                              document.getElementById('<%=txtOpenTime.ClientID%>').value + '　第' +
                                              document.getElementById('<%=txtOpenRound.ClientID%>').value + '回目\n' +
                    '2. 開催場所　　　　　' + document.getElementById('<%=txtOpenPlace.ClientID%>').value + '\n' +
                    '3. 対象工事名称　　　' + document.getElementById('<%=txtOrderNm.ClientID%>').value + '\n' +
                    '4. プロセス　　　　　' + document.getElementById('<%=hdnProcessName.ClientID%>').value + '　/　' +
                                              document.getElementById('<%=hdnDiscussPhaseName.ClientID%>').value + '\n' +
                    '5. その他　　　　　　詳細は、以下をご確認下さい。' + '\n' +
                    '　　　　　　　　　　 ' + 'http://' + window.location.host + window.location.pathname +
                                          '?pj_no=' + document.getElementById('<%=hdnRpPjNo.ClientID%>').value +
                                          '&seq_no=' + document.getElementById('<%=hdnRpSeqNo.ClientID%>').value 

                window.clipboardData.setData("Text", str);
                alert("開催案内文テキストをクリップボードにコピーしました");
            }
            else {
                alert("IEしか使えません");
            }
        }
        
        // リスク予防検討会削除処理の確認メッセージ
        function deleteConfirm() {
            var msg = '入力情報を全て削除して初期化します。よろしいですか？'

            // Session変数RpSeqNoの値がNULL・ブランクでない場合
            if (document.getElementById('<%=hdnRpSeqNo.ClientID%>').value != "")
            {
                msg = 'このリスク予防・管理検討会情報を削除します。よろしいですか？'
            }

            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // リスク予防検討会削除処理のEnd処理
        function deleteEnd(msg) {
            alert(msg);
            window.location.href = 'RiskPreventionList.aspx';
        }

        // リスク予防検討会キャンセル処理の確認メッセージ
        function cancelConfirm() {
            var msg = '案件画面に移動します。よろしいですか？'
            if (window.confirm(msg)) {

                window.location.href = 'Project.aspx?pj_no=' + document.getElementById('<%=hdnRpPjNo.ClientID%>').value;
                return false;
            } else {
                return false;
            }
        }

        // リスク予防検討会登録処理の確認メッセージ
        function updateConfirm() {
            if (document.getElementById('<%=hdnIfOverwrite.ClientID%>').value == 1) {
                return true;
            }
            var msg = 'リスク予防・管理検討会を登録します。よろしいですか?'
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // リスク予防検討会登録処理の確認メッセージ
        function overWriteConfirm(msg) {
            if (window.confirm(msg)) {
                document.getElementById('<%=hdnIfOverwrite.ClientID%>').value = 1;
                document.getElementById('<%=btnRiskPreventionInput.ClientID%>').click();
                return true;
            } else {
                return false;
            }
        }

        // 当該案件が別のユーザによって削除されています。案件検索画面に戻ります
        function updateError(msg) {
            alert(msg);
            window.location.href = 'RiskPreventionList.aspx';
        }

        // リスク予防検討会添付ファイル削除処理の確認メッセージ
        function deleteFileConfirm() {
            var msg = 'この添付ファイルを削除します。よろしいですか?'
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // 共通：エラーのため、前画面に戻る
        function errorBack(msg) {
            alert(msg);
            window.history.back(-1);
        }

        // 発信者選択アイコンクリックイベント
        function senderUserSearch() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("UserSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 表示形式：社員氏名 & " " & 所属部門全て & " " & 役職
                document.getElementById('<%=txtSenderUserFullName.ClientID%>').value = selVal.fullNm + " " + selVal.allSectName + " " + selVal.postCls;
                document.getElementById('<%=hdnSenderUserFullName.ClientID%>').value = selVal.fullNm + " " + selVal.allSectName + " " + selVal.postCls;
                document.getElementById('<%=hdnUserCD.ClientID%>').value = selVal.userCd;
                document.getElementById('<%=hdnUserID.ClientID%>').value = selVal.userId;
            }
            return false
        }

        // 発信者クリア
        function delSenderUserName() {
            document.getElementById('<%=txtSenderUserFullName.ClientID%>').value = "";
            document.getElementById('<%=hdnSenderUserFullName.ClientID%>').value = "";
            document.getElementById('<%=hdnUserCD.ClientID%>').value = "";
            document.getElementById('<%=hdnUserID.ClientID%>').value = "";
            return false
        }

        // ファイルアップロード後に、画面を再更新
        function uploadComplete(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click();
        }

        // ファイルアップロードでエラーが発生した場合、エラーメッセージを表示する
        function uploadError(sender, args) {
            alert("ファイルアップロードにエラーが発生しました");
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate>

            <asp:HiddenField ID="hdnModifiedOn" runat="server" />
            <asp:HiddenField ID="hdnRpSeqNo" runat="server" />
            <asp:HiddenField ID="hdnRpPjNo" runat="server" />

            <table style="width: 1000px;" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblPageTitle" runat="server" CssClass="top-Headr-Title" Text="◆◆◆ リスク予防管理検討会 開催通知 兼 レビュー依頼書 ◆◆◆"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height:35px;vertical-align:bottom">
                        <table style="width: 100%;">
                            <tr>
                                <td></td>
                                <td class="top-label">起票者</td>
                                <td class="top-contect">
                                    <asp:Label ID="lblCreatedUserName" runat="server"></asp:Label>
                                </td>
                                <td class="top-label">最終更新者</td>
                                <td class="top-contect">
                                    <asp:Label ID="lblModifiedUserName" runat="server"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="section-title">
                        0.報告区分・リスクの有無</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="tableContent" >
                            <tr>
                                <td class="detail-header">報告区分<span style="color:red; ">*</span></td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rdoReportCategory" RepeatDirection="Horizontal" AutoPostBack="True" runat="server" CssClass="noStyle">
                                        <asp:ListItem Value="開催通知">開催通知 兼 レビュー依頼書</asp:ListItem>
                                        <asp:ListItem Value="議事録">議事録</asp:ListItem>
                                        <asp:ListItem Value="リスクなし">リスクなしと判断</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">確認日</td>
                                <td colspan="3">
                                    <app:ExtTextBox ID="txtCheckDate" runat="server" MaxLength="20"></app:ExtTextBox>
                                    （部品質管理責任者がリスクなしと判断した場合の確認日）</td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

            <table id="tab1" runat="server" style="width: 1000px;">
                <tr>
                    <td class="section-title">
                        <br />
                        1.リスク予防管理検討会 対象工事</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td class="detail-header">工事名称</td>
                                <td>
                                    <app:ExtTextBox ID="txtOrderNm" runat="server" Enabled ="false" Width="385px" MaxLength="60" ></app:ExtTextBox>
                                </td>
                                <td class="detail-header">顧客</td>
                                <td>
                                    <app:ExtTextBox ID="txtCompyNm" runat="server" Enabled ="false" Width="385px" MaxLength="60" ></app:ExtTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">担当部門</td>
                                <td>
                                    <app:ExtTextBox ID="txtProductSectNm" runat="server" Enabled ="false" Width="385px" MaxLength="100" ></app:ExtTextBox>
                                </td>
                                <td class="detail-header">顧客区分</td>
                                <td>
                                    <app:ExtTextBox ID="txtCustomerTypeName" runat="server" Enabled ="false" Width="385px" MaxLength="20" ></app:ExtTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">プロセス<span style="color:red; ">*</span></td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rdoProcess" RepeatDirection="Horizontal" runat="server"  AutoPostBack="True" RepeatColumns="4" CssClass="noStyle">
                                        <asp:ListItem Value="1">选项A</asp:ListItem>
                                        <asp:ListItem Value="2">选项B</asp:ListItem>
                                        <asp:ListItem Value="3">选项C</asp:ListItem>
                                        <asp:ListItem Value="4">选项D</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:HiddenField ID="hdnProcessName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">検討段階<span style="color:red; ">*</span></td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rdoDiscussPhase" RepeatDirection="Horizontal" runat="server" RepeatColumns="4" AutoPostBack="True" CssClass="noStyle">
                                        <asp:ListItem Value="1">选项A</asp:ListItem>
                                        <asp:ListItem Value="2">选项B</asp:ListItem>
                                        <asp:ListItem Value="3">选项C</asp:ListItem>
                                        <asp:ListItem Value="4">选项D</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:HiddenField ID="hdnDiscussPhaseName" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table id="tab2" runat="server" style="width: 1000px;">
                <tr>
                    <td class="section-title">
                        <br />
                        2.リスク予防管理検討会 実施内容</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td class="detail-header">
                                    <table style="width: 100%" class="noStyle">
                                        <tr>
                                            <td>発信者</td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgSenderUserSearch" runat="server" Height="16px" ImageUrl="~/Resources/searchicon.png" Width="16px" OnClientClick="return senderUserSearch()"/></td>
                                        </tr>
                                    </table>

                                </td>
                                <td colspan="3">
                                    <table class="noStyle">
                                        <tr>
                                            <td style="width: 800px">
                                                <app:ExtTextBox ID="txtSenderUserFullName" runat="server" Width="800px" MaxLength="100" Enabled="False"></app:ExtTextBox>
                                            </td>
                                            <td style="vertical-align: bottom">
                                                <asp:ImageButton runat="server" Height="16px" ImageUrl="~/Resources/deletecell.png" Width="16px" OnClientClick="return delSenderUserName()" ID="imgSenderUserClear" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hdnUserCD" runat="server" />
                                    <asp:HiddenField ID="hdnUserID" runat="server" />
                                    <asp:HiddenField ID="hdnSenderUserFullName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header" style="valign:top">
                                    <table style="width: 100%" class="noStyle">
                                        <tr>
                                            <td>開催日時</td>
                                            <td style="text-align: right">
                                                <asp:ImageButton ID="imgOpenDateCalendar" runat="server" Height="16px" ImageUrl="~/Resources/calendaricon.png" Width="16px" /></td>
                                        </tr>
                                    </table>
                                    </td>
                                    <td colspan="3">

                                    <table align="left" class="noStyle">
                                        <tr>
                                            <td>
                                                <app:ExtTextBox ID="txtOpenDate" runat="server" MaxLength="11"></app:ExtTextBox>
                                                <div id="calendar" class="calendar" visible="false" runat="server">
                                                    <asp:Calendar ID="requestedDeliveryDateCalendar" runat="server" CssClass="tableContent" />
                                                </div>

                                            </td>
                                            <td>
                                                <app:ExtTextBox ID="txtOpenTime" runat="server" MaxLength="20"></app:ExtTextBox>
                                            </td>
                                            <td>&nbsp;&nbsp; 第<app:ExtTextBox ID="txtOpenRound" runat="server" MaxLength="5" Width="40px"></app:ExtTextBox>回目
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">開催場所</td>
                                <td colspan="3">
                                    <app:ExtTextBox ID="txtOpenPlace" runat="server" Width="286px" MaxLength="30"></app:ExtTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">レビューポイント</td>
                                <td colspan="3">
                                    <app:ExtTextBox ID="txtReviewPoint" TextMode="MultiLine" runat="server" Columns="100" Rows="8" MaxLength="2000"></app:ExtTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">レビュアー<br /> （出席予定者）</td>
                                <td colspan="3">
                                    <app:ExtTextBox ID="txtReviewerPlan" runat="server" Columns="100" Rows="5" TextMode="MultiLine" MaxLength="1000"></app:ExtTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">添付ファイル</td>
                                <td colspan="3" style="word-break:break-all">
                                    <asp:GridView runat="server" ID="grdRiskPreventionAttatch" AutoGenerateColumns="False" BorderStyle="None" GridLines="None" ShowHeader="False" CssClass="noStyle">
                                        <Columns>

                                            <asp:HyperLinkField DataNavigateUrlFields="FILE_SEQ_NO" DataNavigateUrlFormatString="RiskPrevention.aspx?fid={0}" DataTextField="ATTATCH_FILE_NAME" ControlStyle-Width ="758px"/>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnRiskPreventionAttatchDel" runat="server" Width="60" OnClientClick="return deleteFileConfirm()" CommandName="del" CommandArgument='<%#Eval("FILE_SEQ_NO")%>' Text="削除"  Enabled='<%#Eval("isEditAble")%>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BorderStyle="None" />
                                    </asp:GridView>
                                    <ajaxToolkit:AsyncFileUpload ID="lnkRiskPreventionAttatch" runat="server"  Width="819px" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError" CssClass="noStyle"/>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" Text=""></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">リスク・不安要素<br /> 検討表</td>
                                <td width="380px">
                                    <asp:Button ID="btnRiskManagementListDl" runat="server" Text="ダウンロード" />
                                </td>
                                <td class="detail-header">開催案内作成</td>
                                <td width="380px">
                                    <asp:Button ID="btnOpenGuidanceCreate" runat="server" OnClientClick="copyToClipBoard(); return false;" Text="開催案内" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>

            <table id="tab3" runat="server" style="width: 1000px;">
                <tr>
                    <td class="section-title">
                        <br />
                        3.議事録</td>
                </tr>
                <tr runat="server" id="trReviewerHeader">
                    <td class="section2-title">(1)出席者</td>
                </tr>

                <tr runat="server" id="trReviewerDetail">
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td class="detail-header">レビュアー<br /> （出席者）</td>
                                <td colspan="3">
                                    <app:ExtTextBox ID="txtReviewer" runat="server" Columns="100" Rows="5" TextMode="MultiLine" MaxLength="1000"></app:ExtTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="trReviewRemarkHeader">
                    <td class="section2-title">(2)レビューコメント</td>
                </tr>
                <tr runat="server" id="trReviewRemarkDetail">
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td>
                                    <app:ExtTextBox ID="txtReviewRemark" runat="server" Columns="117" Rows="30" TextMode="MultiLine" MaxLength="10000"></app:ExtTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="trRemarkHeader">
                    <td class="section2-title">(3)その他特記事項</td>
                </tr>

                <tr runat="server" id="trRemarkDetail">
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td>
                                    <app:ExtTextBox ID="txtRemark" runat="server" Columns="117" Rows="5" TextMode="MultiLine" MaxLength="1000"></app:ExtTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td>
                        <table style="width: 100%;table-layout: fixed;">
                            <tr>
                                <td style="text-align:center">
                                    <asp:Button ID="btnRiskPreventionInput" runat="server" Text="リスク予防検討会登録" OnClientClick="return updateConfirm()"/>
                                    <asp:HiddenField ID="hdnIfOverwrite" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Button ID="btnRiskPreventionDel" runat="server" Text="リスク予防検討会削除" OnClientClick="return deleteConfirm()"  style="align-content:center "/>
                                </td>
                                <td style="text-align:center">
                                    <asp:Button ID="btnRiskPreventionCancel" runat="server" Text="キャンセル" OnClientClick="return cancelConfirm()"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRiskManagementListDl" />
            <asp:PostBackTrigger ControlID="btnRiskPreventionInput" />
            <asp:PostBackTrigger ControlID="btnRiskPreventionDel" />
            <asp:PostBackTrigger ControlID="btnRiskPreventionCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

