<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Menu.Master" CodeBehind="QualityProgression.aspx.vb" Inherits="ProjectRiskManagementSystem.QualityProgression" %>

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
                var str = '件名：【' + document.getElementById('<%=txtTargetSectName.ClientID%>').value + '】品質推進会議実施の件' + '\n' + '\n' +

                    '頭書の件、以下の通り品質推進会議を実施いたしますのでご参集のほどよろしくお願いいたします。　了' + '\n' + '\n' + '\n' +

                    '　　　　　　　　　　　　　　　記' + '\n' + '\n' +

                    '1. 開催日時　　　　　　　　　　　　' + document.getElementById('<%=txtOpenDate.ClientID%>').value + '　' +
                                                        document.getElementById('<%=txtOpenTime.ClientID%>').value + '　第' +
                                                        document.getElementById('<%=txtOpenRound.ClientID%>').value + '回目\n' +
                    '2. 開催場所　　　　　　　　　　　　' + document.getElementById('<%=txtOpenPlace.ClientID%>').value + '\n' +
                    '3. リスクフォロー対象案件　　　　　' + document.getElementById('<%=hdnIncompletePjList.ClientID%>').value + '\n' +
                    '4. その他　　　　　　　　　　　　　詳細は、以下をご確認下さい。' + '\n' +
                    '　　　　　　　　　　 　　　　　　　' + 'http://' + window.location.host + window.location.pathname +
                                                        '?progression_no=' + document.getElementById('<%=hdnQpNo.ClientID%>').value;
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

            // Session変数hdnQpNoの値がNULL・ブランクでない場合
            if (document.getElementById('<%=hdnQpNo.ClientID%>').value != "")
            {
                msg = 'この品質推進会情報を削除します。よろしいですか？'
            }

            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // 品質推進会議削除処理のEnd処理
        function deleteEnd(msg) {
            alert(msg);
            window.location.href = 'QualityProgressionList.aspx';
        }

        // 品質推進会議キャンセル処理の確認メッセージ
        function cancelConfirm() {
            var msg = '品質推進会議検索画面に移動します。よろしいですか？';
            if (window.confirm(msg)) {

                window.location.href = 'QualityProgressionList.aspx';
                return false;
            } else {
                return false;
            }
        }

        // 品質推進会議登録処理の確認メッセージ
        function updateConfirm() {
            if (document.getElementById('<%=hdnIfOverwrite.ClientID%>').value == 1) {
                return true;
            }
            var msg = '品質推進会を登録します。よろしいですか?'
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // 品質推進会議削除処理の確認メッセージ
        function overWriteConfirm(msg) {
            if (window.confirm(msg)) {
                document.getElementById('<%=hdnIfOverwrite.ClientID%>').value = 1;
                document.getElementById('<%=btnQualityProgressionInput.ClientID%>').click();
                return true;
            } else {
                return false;
            }
        }

        // 当該案件が別のユーザによって削除されています。案件検索画面に戻ります
        function updateError(msg) {
            alert(msg);
            window.location.href = 'QualityProgressionList.aspx';
        }

        // 品質推進会議添付ファイル削除処理の確認メッセージ
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
                document.getElementById('<%=txtSenderUserName.ClientID%>').value = selVal.fullNm + " " + selVal.allSectName + " " + selVal.postCls;
                document.getElementById('<%=hdnSenderUserName.ClientID%>').value = selVal.fullNm + " " + selVal.allSectName + " " + selVal.postCls;
                document.getElementById('<%=hdnSenderUserCD.ClientID%>').value = selVal.userCd;
                document.getElementById('<%=hdnSenderUserID.ClientID%>').value = selVal.userId; 
            }
            return false
        }

        // 発信者クリア
        function delSenderUserName() {

            document.getElementById('<%=txtSenderUserName.ClientID%>').value = "";
            document.getElementById('<%=hdnSenderUserName.ClientID%>').value = "";
            document.getElementById('<%=hdnSenderUserCD.ClientID%>').value = "";
            document.getElementById('<%=hdnSenderUserID.ClientID%>').value = ""; 
            return false
        }

        // リスク・不安要素検討表ダウンロード
        function download(param) {
            window.location = "QualityProgression.aspx" + param;
            return false
        }

        // ファイルアップロード後に、画面を再更新
        function uploadComplete(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        } 

        // ファイルアップロードでエラーが発生した場合、エラーメッセージを表示する
        function uploadError(sender, args) {
            alert("ファイルアップロードにエラーが発生しました");
        }

        // リスクフォロー対象案件削除ボタンクリック
        function delFollowProject(pjName) {
            var msg = '工事名称「' + pjName + '」をリスクフォロー対象案件から削除します。よろしいですか?';
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

    </script>
    <asp:UpdatePanel ID="panUpdate" runat="server" UpdateMode="Conditional">

        <ContentTemplate>

            <asp:HiddenField ID="hdnModifiedOn" runat="server" />
            <asp:HiddenField ID="hdnQpNo" runat="server" />

            <table style="width: 1000px;">
                <tr>
                    <td>
                        <asp:Label ID="lblPageTitle" runat="server" CssClass="top-Headr-Title" Text="◆◆◆ 品質推進会 開催通知 ◆◆◆"></asp:Label>
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
                        0.報告区分</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td class="detail-header">報告区分<span style="color:red; ">*</span></td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rdoReportCategory" RepeatDirection="Horizontal" AutoPostBack="True" runat="server" CssClass="noStyle">
                                        <asp:ListItem Value="開催通知">開催通知</asp:ListItem>
                                        <asp:ListItem Value="議事録">議事録</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

            <table id="tab1" runat="server" style="width: 1000px;">
                <tr>
                    <td class="section-title">
                        <br />
                        1.品質推進会 実施部門</td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" class="tableContent">
                            <tr>
                                <td class="detail-header">支社名</td>
                                <td>
                                    <app:ExtTextBox ID="txtBranchName" runat="server" Enabled ="False" Width="385px" MaxLength="20" ></app:ExtTextBox>
                                </td>
                                <td class="detail-header">対象部門</td>
                                <td>
                                    <asp:HiddenField ID="hdnTargetSectCD" runat="server" />
                                    <app:ExtTextBox ID="txtTargetSectName" runat="server" Enabled ="false" Width="385px" MaxLength="40" ></app:ExtTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">開催担当者</td>
                                <td>
                                    <asp:HiddenField ID="hdnOpenerUserID" runat="server" />
                                    <asp:HiddenField ID="hdnOpenerUserCD" runat="server" />
                                    <app:ExtTextBox ID="txtOpenerUserName" runat="server" Enabled ="false" Width="385px" MaxLength="100" ></app:ExtTextBox>
                                </td>
                                <td class="detail-header">会議名<span style="color:red; ">*</span></td>
                                <td>
                                    <app:ExtTextBox ID="txtConferenceName" runat="server" Width="385px" MaxLength="100" ></app:ExtTextBox>
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
                        2.品質推進会 実施内容</td>
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
                                            <td style="width:800px">
                                                <app:ExtTextBox ID="txtSenderUserName" runat="server" Width="800px" MaxLength="100" Enabled="False"></app:ExtTextBox>
                                            </td>
                                            <td style="vertical-align:bottom">
                                                <asp:ImageButton runat="server" Height="16px" ImageUrl="~/Resources/deletecell.png" Width="16px" OnClientClick="return delSenderUserName()" ID="imgSenderUserClear" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hdnSenderUserCD" runat="server" />
                                    <asp:HiddenField ID="hdnSenderUserID" runat="server" />
                                    <asp:HiddenField ID="hdnSenderUserName" runat="server" />
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
                                <td class="detail-header">リスクフォロー<br /> 対象案件</td>
                                <td colspan="3">
                                    <asp:GridView runat="server" ID="gdvIncompletePjList" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="tableContent">
                                        <Columns>

                                            <asp:BoundField HeaderText="案件番号" DataField="PROJECT_NO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width ="110px">
                                                <HeaderStyle Width="110px" Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="工事名称" DataField="ORDER_NM" ItemStyle-Width ="180px">
                                                <HeaderStyle Width="180px" Wrap="False"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="担当部門" DataField="PRODUCT_SECT_NM" ItemStyle-Width ="180px">
                                                <HeaderStyle Width="180px" Wrap="False"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="担当者" DataField="PRODUCT_USER_FULLNAME" ItemStyle-Width ="180px">
                                                <HeaderStyle Width="180px" Wrap="False"/>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="プロセス" DataField="PROCESS_NAME" ItemStyle-Width ="110px">
                                                <HeaderStyle Width="110px" Wrap="False"/>
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="検討表" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button runat="server" Text="DL" OnClientClick='<%# "download(" + Eval("URL").ToString + "); return false;"%>' Enabled='<%#Eval("canDownLoad")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" Wrap="False"/>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnFollowProjectDelete" runat="server" OnClientClick='<%# "return delFollowProject(" + Chr(39) + Eval("ORDER_NM_DISP").ToString + Chr(39) + "); "%>' CommandName="del" CommandArgument='<%#Eval("PROJECT_NO") %>' Text="削除"  Enabled='<%#Eval("isEditAble")%>'/>
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" Wrap="False"/>
                                            </asp:TemplateField>

                                        </Columns>
                                         <HeaderStyle CssClass="GridViewHeaderStyle" />
                                    </asp:GridView>
                                    <br />
                                    <asp:HiddenField ID="hdnIncompletePjList" runat="server" />
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
                                    <asp:GridView runat="server" ID="grdQualityProgressionAttatch" AutoGenerateColumns="False" BorderStyle="None" GridLines="None" ShowHeader="False" CssClass="noStyle">
                                        <Columns>

                                            <asp:HyperLinkField DataNavigateUrlFields="FILE_SEQ_NO" DataNavigateUrlFormatString="QualityProgression.aspx?fid={0}" DataTextField="ATTATCH_FILE_NAME" ControlStyle-Width ="758px"/>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnQualityProgressionAttatchDel" runat="server" Width="60" OnClientClick="return deleteFileConfirm()" CommandName="del" CommandArgument='<%#Eval("FILE_SEQ_NO")%>' Text="削除"  Enabled='<%#Eval("isEditAble")%>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BorderStyle="None" />
                                    </asp:GridView>
                                    <ajaxToolkit:AsyncFileUpload ID="lnkRiskPreventionAttatch" runat="server" Width="819px" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError" CssClass="noStyle"/>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" Text=""></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td class="detail-header">開催案内作成</td>
                                <td colspan="3">
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
                                <td colspan="3" class="auto-style2">
                                    <app:ExtTextBox ID="txtReviewer" runat="server" Columns="100" Rows="5" TextMode="MultiLine" MaxLength="1000"></app:ExtTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="trReviewRemarkHeader">
                    <td class="section2-title">(2)議事内容</td>
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
                <tr>
                    <td>
                        <table style="width: 100%;table-layout: fixed;">
                            <tr>
                                <td style="text-align:center">
                                    <asp:Button ID="btnQualityProgressionInput" runat="server" Text="品質推進会議登録" OnClientClick="return updateConfirm()"/>
                                    <asp:HiddenField ID="hdnIfOverwrite" runat="server" />
                                </td>
                                <td style="text-align:center">
                                    <asp:Button ID="btnQualityProgressionDel" runat="server" Text="品質推進会議削除" OnClientClick="return deleteConfirm()"/>
                                </td>
                                <td style="text-align:center">
                                    <asp:Button ID="btnQualityProgressionCancel" runat="server" Text="キャンセル" OnClientClick="return cancelConfirm()"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnQualityProgressionInput" />
            <asp:PostBackTrigger ControlID="btnQualityProgressionDel" />
            <asp:PostBackTrigger ControlID="btnQualityProgressionCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>