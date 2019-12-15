<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Menu.Master" CodeBehind="Project.aspx.vb" Inherits="ProjectRiskManagementSystem.Project" %>

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

        // 関連オーダ選択
        function relateOrderSearch() {
            // オーダ検索ポップアップを呼び出し
            var selVal = window.showModalDialog("OrderSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // オーダ検索画面でオーダが選択された場合は、選択オーダを関連オーダテキストボックス(txtRelateOrderCd)にセットする
                if (document.getElementById('<%=txtRelateOrderCd.ClientID%>').value != "") {
                    // すでに関連オーダテキストボックスに値が存在する場合はカンマ(,)区切りで次の値を追記する
                    document.getElementById('<%=txtRelateOrderCd.ClientID%>').value = document.getElementById('<%=txtRelateOrderCd.ClientID%>').value + "," + selVal.orderCd
                } else {
                    document.getElementById('<%=txtRelateOrderCd.ClientID%>').value = selVal.orderCd
                }
            }
        }

        // オーダ選択
        function orderSelect() {
            // オーダ検索ポップアップを呼び出し
            var selVal = window.showModalDialog("OrderSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // オーダCD
                document.getElementById('<%=txtOrderCd.ClientID%>').value = selVal.orderCd
                //① 工事名称(txtOrderNm)：オーダ検索画面のhidOrderNm
                document.getElementById('<%=hdnOrderNm.ClientID%>').value = selVal.orderNm
                //② 顧客(txtCompyNm)：オーダ検索画面のhidCompyNm
                document.getElementById('<%=hdnCompyNm.ClientID%>').value = selVal.compyNm
                //③ 受注金額(txtJyuchuCrr)：オーダ検索画面のhidJyuchuCrr
                document.getElementById('<%=hdnJyuchuCrr.ClientID%>').value = selVal.jyuchuCrr
                //④ 納期日(txtNokiYmd)：オーダ検索画面のhidNokiYmd
                document.getElementById('<%=hdnNokiYmd.ClientID%>').value = selVal.nokiYmd
                //⑤ 受注部門(txtJyuchuSectNm)：オーダ検索画面のhidJyuchuSectNm
                document.getElementById('<%=hdnJyuchuSectNm.ClientID%>').value = selVal.allSectNm
                //⑥ 受注担当者(txtJyuchuUserNm)：オーダ検索画面のhidJyuchuUserNm
                document.getElementById('<%=hdnJyuchuUserNm.ClientID%>').value = selVal.fullName
            }
        }

        // 製造部門選択
        function sectSelectProductSect() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("SectionSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 部門検索画面で部門が選択された場合は、選択部門全名称（支社・部・グループ・チーム）を製造部門テキストボックス(txtProductSectNm)にセットする
                document.getElementById('<%=txtProductSectNm.ClientID%>').value = selVal.allSectName
                document.getElementById('<%=hdnProductSectId.ClientID%>').value = selVal.sectId
                document.getElementById('<%=hdnProductSectCd.ClientID%>').value = selVal.sectCd
            }
        }

        // 製造部門担当者選択
        function userSelectProductUser() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("UserSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 社員検索画面で社員が選択された場合は、社員氏名・役職を製造部門担当者テキストボックス(txtProductUserNm)にセットする
                document.getElementById('<%=txtProductUser.ClientID%>').value = selVal.fullNm + " " + selVal.postCls
                // 社員検索画面で社員が選択された場合は、選択社員の部門全名称（支社・部・グループ・チーム）を製造部門テキストボックス(txtProductSectNm)にセットする
                document.getElementById('<%=txtProductSectNm.ClientID%>').value = selVal.allSectName
                //  '製造部門担当者(txtProductUser)の所属コード(SECT_CD)
                document.getElementById('<%=hdnProductUserSectCd.ClientID%>').value = selVal.sectCd

                document.getElementById('<%=hdnProductUserId.ClientID%>').value = selVal.userId
                document.getElementById('<%=hdnProductUserCd.ClientID%>').value = selVal.userCd

                document.getElementById('<%=hdnProductSectId.ClientID%>').value = selVal.sectId
                document.getElementById('<%=hdnProductSectCd.ClientID%>').value = selVal.sectCd

            }
        }

        // 支社品質管理責任者選択
        function userSelectBranchQualityManager() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("UserSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 社員検索画面で社員が選択された場合は、部門全名称（支社・部・グループ・チーム）・役職・社員氏名を支社品質管理責任者テキストボックス(txtBranchQualityManager)にセットする
                document.getElementById('<%=txtBranchQualityManager.ClientID%>').value = selVal.allSectName + " " + selVal.postCls + " " + selVal.fullNm
            }
        }

        // 部品質管理責任者選択
        function userSelectSectionQualityManager() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("UserSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 社員検索画面で社員が選択された場合は、部門全名称（支社・部・グループ・チーム）・役職・社員氏名を部品質管理責任者テキストボックス(txtSectionQualityManager)にセットする
                document.getElementById('<%=txtSectionQualityManager.ClientID%>').value = selVal.allSectName + " " + selVal.postCls + " " + selVal.fullNm
            }
        }

        // グループ品質管理責任者選択
        function userSelectGroupQualityManager() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("UserSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 社員検索画面で社員が選択された場合は、部門全名称（支社・部・グループ・チーム）・役職・社員氏名を部品質管理責任者テキストボックス(txtGroupQualityManager)にセットする
                document.getElementById('<%=txtGroupQualityManager.ClientID%>').value = selVal.allSectName + " " + selVal.postCls + " " + selVal.fullNm
            }
        }

        // プロジェクト品質管理責任者選択
        function userSelectProjectQualityManager() {
            // ユーザー検索ポップアップを呼び出し
            var selVal = window.showModalDialog("UserSearch.aspx", "", "dialogWidth=725px;dialogHeight=540px");
            // 画面項目に選択値を設定する
            if (selVal) {
                // 社員検索画面で社員が選択された場合は、部門全名称（支社・部・グループ・チーム）・役職・社員氏名をプロジェクト品質管理責任者テキストボックス(txtProjectQualityManager)にセットする
                document.getElementById('<%=txtProjectQualityManager.ClientID%>').value = selVal.allSectName + " " + selVal.postCls + " " + selVal.fullNm
            }
        }

        //キャンセルボタンを押下
        function cancelConfirm() {
            var msg = '案件検索画面に移動します。よろしいですか？'
            if (window.confirm(msg)) {

                //window.location.href = 'ProjectList.aspx';
                return true;
            } else {
                return false;
            }
        }

        // 登録ボタンを押下
        function UpdateConfirm() {
            if (document.getElementById('<%=hdnIfOverwrite.ClientID%>').value == 1) {
                return true;
            }
            var msg = '案件を登録します。よろしいですか?'
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // (48) リスク予防検討会登録ボタンクリックイベント(btnRiskPreventionInput_Click)
        function RiskPreventionInputConfirm() {
            var msg = 'リスク予防・管理検討会新規登録画面に移動します。よろしいですか?'
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // (47) 案件削除ボタンクリックイベント(btnPjDel_Click)
        function DeleteConfirm() {
            var msg = '入力情報を全て削除して初期化します。よろしいですか？'

            if (document.getElementById('<%=txtPjNo.ClientID%>').value != "") {
                msg = '本案件を削除します。この案件に関連したリスク予防・管理検討会情報も削除されます。よろしいですか?'
            }
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
                document.getElementById('<%=btnPjInput.ClientID%>').click();

                return true;
            } else {
                return false;
            }
        }

        // 添付ファイル削除処理の確認メッセージ
        function deleteFileConfirm() {
            var msg = 'この添付ファイルを削除します。よろしいですか?'
            if (window.confirm(msg)) {
                return true;
            } else {
                return false;
            }
        }

        // 当該案件が別のユーザによって削除されています。案件検索画面に戻ります
        function updateError(msg) {
            alert(msg);
            window.location.href = 'ProjectList.aspx';
        }

        // 画面上にメッセージ表示
        function showMessage(msg) {
            alert(msg);
        }

        //ファイルアップロード
        function uploadCompleteSp1RiskManagementList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        }
        //ファイルアップロード
        function uploadCompleteSp1RiskCheckList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
          
        }
        //ファイルアップロード
        function uploadCompleteSp2RiskManagementList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
          
        }
        //ファイルアップロード
        function uploadCompleteSp2RiskCheckList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        }
        //ファイルアップロード
        function uploadCompletePpRiskManagementLis(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        }
        //ファイルアップロード
        function uploadCompletePpRiskCheckList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        }
        //ファイルアップロード
        function uploadCompleteDpRiskManagementList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        }
        //ファイルアップロード
        function uploadCompleteDpRiskCheckList(sender, args) {
            document.getElementById('<%=lnkUpdate.ClientID%>').click()
        }

        // ファイルアップロード後に、画面を再更新
        function uploadComplete(sender, args) {
            document.getElementById('<%=lnkUpdateAttach.ClientID%>').click()
        }

        // ファイルアップロードでエラーが発生した場合、エラーメッセージを表示する
        function uploadError(sender, args) {
            alert("ファイルアップロードにエラーが発生しました");
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate>
            <asp:HiddenField ID="hdnIfOverwrite" runat="server" />
            <asp:HiddenField ID="hdnProductSectId" runat="server" />
            <asp:HiddenField ID="hdnProductSectCd" runat="server" />
            <asp:HiddenField ID="hdnSp1File" runat="server" />

            <asp:HiddenField ID="hdnOrderNm" runat="server" />
            <asp:HiddenField ID="hdnCompyNm" runat="server" />
            <asp:HiddenField ID="hdnNokiYmd" runat="server" />
            <asp:HiddenField ID="hdnJyuchuCrr" runat="server" />
            <asp:HiddenField ID="hdnJyuchuSectNm" runat="server" />
            <asp:HiddenField ID="hdnJyuchuUserNm" runat="server" />

            <asp:HiddenField ID="hdnProductUserId" runat="server" />
            <asp:HiddenField ID="hdnProductUserCd" runat="server" />
            <asp:HiddenField ID="hdnModifiedOn" runat="server" />

            <asp:HiddenField ID="hdnProductUserSectCd" runat="server" />

            <asp:HiddenField ID="hdnSp1RiskManagementListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnSp1RiskManagementListIsUpdat" runat="server" />
            <asp:HiddenField ID="hdnSp2RiskManagementListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnSp2RiskManagementListIsUpdat" runat="server" />
            <asp:HiddenField ID="hdnPpRiskManagementListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnPpRiskManagementListIsUpdat" runat="server" />
            <asp:HiddenField ID="hdnDpRiskManagementListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnDpRiskManagementListIsUpdat" runat="server" />

            <asp:HiddenField ID="hdnSp1RiskCheckListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnSp1RiskCheckListIsUpdate" runat="server" />
            <asp:HiddenField ID="hdnSp2RiskCheckListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnSp2RiskCheckListIsUpdate" runat="server" />
            <asp:HiddenField ID="hdnPpRiskCheckListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnPpRiskCheckListIsUpdate" runat="server" />
            <asp:HiddenField ID="hdnDpRiskCheckListIsDelete" runat="server" />
            <asp:HiddenField ID="hdnDpRiskCheckListIsUpdate" runat="server" />

        <table style="width: 1000px;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td rowspan="5">
                                <td class="top-Headr-Title">◆◆◆基本情報◆◆◆</td>
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

                    </table>
                </td>
            </tr>
            <tr>
                <td class="section-title">
                      1.オーダ情報</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="tableContent">
                        <tr>
                            <td class="detail-header">案件番号</td>
                            <td colspan="3">
                                <app:ExtTextBox ID="txtPjNo" runat="server" MaxLength="10"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">プロセス<span style="color:red; ">*</span></td>
                            <td colspan="3" class="auto-style1">
                                <asp:RadioButtonList ID="rdoProcess" runat="server" DataTextField="PROCESS_NAME" DataValueField="PROCESS_NO" RepeatDirection="Horizontal" AutoPostBack="True" CssClass="noStyle">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trPjNameTemp" runat="server">
                            <td class="detail-header">工事名称(仮)<span style="color:red; ">*</span></td>
                            <td colspan="3">
                                <app:ExtTextBox ID="txtPjNameTemp" runat="server" MaxLength="100" Width="850px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr id="trCustomerName" runat="server">
                            <td class="detail-header">顧客<span style="color:red; ">*</span></td>
                            <td colspan="3">
                                <app:ExtTextBox ID="txtCustomerName" runat="server" MaxLength="60" Width="850px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">
                                <table style="width: 100%" class="noStyle">
                                    <tr>
                                        <td>オーダ</td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgOrderSearch" runat="server" Height="16px" ImageUrl="~/Resources/searchicon.png" Width="16px" OnClientClick="return orderSelect()" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 405px;">
                                <table class="noStyle" >
                                    <tr>
                                        <td>
                                            <app:ExtTextBox ID="txtOrderCd" runat="server" MaxLength="10"></app:ExtTextBox>
                                        </td>
                                        <td style="vertical-align: bottom">
                                            <asp:ImageButton runat="server" Height="16px" ImageUrl="~/Resources/deletecell.png" Width="16px" ID="btnDeleteOrderNo" />

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="detail-header-project">
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td>関連オーダ</td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgRelateOrderSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return relateOrderSearch()" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td >
                                <table class="noStyle" >
                                    <tr>
                                        <td>
                                            <app:ExtTextBox ID="txtRelateOrderCd" runat="server" MaxLength="79" Width="260px"></app:ExtTextBox>
                                        </td>
                                        <td class="vertical-align: bottom">
                                             <asp:ImageButton runat="server" Height="16px"  ImageUrl="~/Resources/deletecell.png" Width="16px" ID="btnRelateOrderDel"/>
                                        </td>
                                    </tr>
                                </table>
                                
                               
                            </td>
                        </tr>
                        <tr id="trOrderNm" runat="server">
                            <td class="detail-header">工事名称</td>
                            <td colspan="3">
                                <app:ExtTextBox ID="txtOrderNm" runat="server" MaxLength="60" Width="850px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr id="trCompyNm" runat="server">
                            <td class="detail-header">顧客</td>
                            <td colspan="3" class="auto-style2">
                                <app:ExtTextBox ID="txtCompyNm" runat="server" MaxLength="60" Width="850px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">顧客区分<span style="color:red; ">*</span></td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rdoCustomerType" runat="server" DataTextField="CUSTOMER_TYPE_NAME" DataValueField="CUSTOMER_TYPE_NO" RepeatDirection="Horizontal" CssClass="noStyle">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trJyuchuCrrNokiYmd" runat="server">
                            <td class="detail-header">受注金額</td>
                            <td style="padding-right: 5px; width: 405px">
                                <app:ExtTextBox ID="txtJyuchuCrr" runat="server" Style="margin-top: 0px; text-align:right " MaxLength="10" Width="396px" ></app:ExtTextBox>
                            </td>
                            <td class="detail-header-project">納期日</td>
                            <td>
                                <app:ExtTextBox ID="txtNokiYmd" runat="server" Style="text-align:right "  MaxLength="10" TextMode="Date" Width="260px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr id="trJyuchuSectNmJyuchuUserNm" runat="server">
                            <td class="detail-header">受注部門</td>
                            <td>
                                <app:ExtTextBox ID="txtJyuchuSectNm" runat="server" MaxLength="100" Width="396px"></app:ExtTextBox>
                            </td>
                            <td class="detail-header-project">受注担当者</td>
                            <td>
                                <app:ExtTextBox ID="txtJyuchuUserNm" runat="server" MaxLength="20" Width="260px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td>製造部門<span style="color:red; ">*</span></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgProductSectSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return sectSelectProductSect()" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 405px">
                                <app:ExtTextBox ID="txtProductSectNm" runat="server" MaxLength="100" Width="396px">※製造部門担当者の所属が自動セットされます</app:ExtTextBox>
                            </td>
                            <td class="detail-header-project">
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td nowrap="nowrap">製造部門担当者<span style="color:red; ">*</span></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgProductUserSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return userSelectProductUser()" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <app:ExtTextBox ID="txtProductUser" runat="server" MaxLength="60" Width="260px"></app:ExtTextBox>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="section-title">
                    <br />
                    2.支社間取引</td>
            </tr>
            <tr>
                <td >
                    <table style="width: 100%;" class="tableContent">
                        <tr>
                            <td class="detail-header">支社間取引の有無<span style="color:red; ">*</span></td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rdoBranchTransactionFlg" AutoPostBack="true" runat="server" RepeatColumns="2" CssClass="noStyle">
                                    <asp:ListItem Value="0">無</asp:ListItem>
                                    <asp:ListItem Value="2">有：自支社が支援支社</asp:ListItem>
                                    <asp:ListItem Value="1">有：自支社が窓口支社(営業及び製造を担当)</asp:ListItem>
                                    <asp:ListItem Value="3">有：自支社が窓口支社(営業のみ担当し、製造は支援支社)</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">
                                <asp:Label ID="lblSupportBranch" runat="server" Text="支援支社"></asp:Label></td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlBranchList" runat="server" DataTextField="A01M002_SECT_NM" DataValueField="A01M002_SECT_CD">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
               <tr>
                <td></td>
            </tr>
            <tr>
                <td class="section-title">
                    <br />
                    3.リスク管理情報</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="tableContent">
                        <tr>
                            <td  class="detail-header-width">
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td nowrap="nowrap">支社品質管理責任者<span style="color:red; ">*</span></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgBranchQualityManagerSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return userSelectBranchQualityManager()" /></td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <app:ExtTextBox ID="txtBranchQualityManager" runat="server" MaxLength="100" Width="565px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="detail-header-width">
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td nowrap="nowrap">部品質管理責任者<span style="color:red; ">*</span></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgSectionQualityManagerSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return userSelectSectionQualityManager()" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <app:ExtTextBox ID="txtSectionQualityManager" runat="server" MaxLength="100" Width="565px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header-width" >
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td nowrap="nowrap">グループ品質管理責任者<span style="color:red; ">*</span></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgGroupQualityManagerSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return userSelectGroupQualityManager()" /></td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <app:ExtTextBox ID="txtGroupQualityManager" runat="server" MaxLength="100" Width="565px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header-width">
                                <table style="width: 100%" class="noStyle" >
                                    <tr>
                                        <td  nowrap="nowrap">プロジェクト品質管理責任者<span style="color:red; ">*</span></td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="imgProjectQualityManagerSearch" runat="server" ImageUrl="~/Resources/SearchIcon.png" OnClientClick="return userSelectProjectQualityManager()" /></td>
                                    </tr>
                                </table>

                            </td>
                            <td>
                                <app:ExtTextBox ID="txtProjectQualityManager" runat="server" Width="565px" MaxLength="100"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header-width">リスク予防管理責任者</td>
                            <td>
                                <app:ExtTextBox ID="txtRiskPreventionManager" runat="server" MaxLength="100" Width="565px"></app:ExtTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">リスク予防管理対象</td>
                            <td>
                                <table class="noStyle" >
                                    <tr>
                                        <td colspan="3">
                                            <asp:CheckBox ID="chkRpm500MilFlg" runat="server" Text="500万以上" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 77px">
                                            <asp:CheckBox ID="chkRpmFirstProductFlg" runat="server" Text="初品" AutoPostBack="True" />
                                        </td>
                                        <td style="width: 160px">
                                            <asp:DropDownList ID="ddlFirstProduct" runat="server" DataTextField="SELECT_CONTENT" DataValueField="FIRST_PRODUCT_NO">
                                            </asp:DropDownList>
                                        </td>
                                        <td>理由：<app:ExtTextBox ID="txtRpmFirstProductCause" runat="server" MaxLength="100" Width="200px" Wrap="False"></app:ExtTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 77px">
                                            <asp:CheckBox ID="chkRpmSpecialProductFlg" runat="server" Text="特殊" AutoPostBack="True" />
                                        </td>
                                        <td style="width: 160px">
                                            <asp:DropDownList ID="ddlSpecialProduct" runat="server" DataTextField="SELECT_CONTENT" DataValueField="SPECIAL_PRODUCT_NO">
                                            </asp:DropDownList>
                                        </td>
                                        <td>理由：<app:ExtTextBox ID="txtRpmSpecialProductCause" runat="server" Width="200px" MaxLength="100"></app:ExtTextBox>
                                        </td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td class="detail-header">リスク予防管理区分</td>
                            <td>
                                <asp:RadioButtonList ID="rdoRpmType" runat="server" Width="590px" AutoPostBack="True" CssClass="noStyle">
                                    <asp:ListItem Value="0">A：リスク予防管理検討会にて高リスクと判断されたもの（リスク予防管理責任者：部品質管理担当者）</asp:ListItem>
                                    <asp:ListItem Value="1">B：２項連用範囲・適用対象に該当するがリスク対策済みと判断されたもの、又は２項適用範囲・適用対象に該当しないがグループ品質管理責任者が管理対象とすべきと判断したもの（同：グループ品質管理担当者）</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;" class="tableContent">
                        <tr>
                            <td class="detail-header">添付ファイル</td>
                            
                            <td colspan="3"  style="word-break:break-all">
                                    <asp:GridView runat="server" ID="grdProjectAttatch" AutoGenerateColumns="False" GridLines="None" ShowHeader="False" CssClass="noStyle">
                                        <Columns>

                                            <asp:HyperLinkField DataNavigateUrlFields="FILE_SEQ_NO" DataNavigateUrlFormatString="Project.aspx?fid={0}" DataTextField="ATTATCH_FILE_NAME" ControlStyle-Width ="758px"/>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnProjectAttatchDel" runat="server" Width="60" OnClientClick="return deleteFileConfirm()" CommandName="del" CommandArgument='<%#Eval("FILE_SEQ_NO")%>' Text="削除"  Enabled='<%#Eval("isEditAble")%>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BorderStyle="None" />
                                    </asp:GridView>
                                    <ajaxToolkit:AsyncFileUpload ID="fleProjectAttatch" runat="server" Width="819px" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError"/>
                                      <asp:LinkButton ID="lnkUpdate" runat="server" Text=""></asp:LinkButton>
                                 <asp:LinkButton ID="lnkUpdateAttach" runat="server" Text=""></asp:LinkButton>
                                </td>
                        </tr>
                        <tr>
                            <td class="detail-header">案件タイプ</td>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rdoProjectType" runat="server" DataTextField="PROJECT_TYPE_NAME" DataValueField="PROJECT_TYPE_NO" RepeatColumns="2" AutoPostBack="True" CssClass="noStyle">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="section-title">
                    <br />
                    4.リスク予防管理活動</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left; width: 650px;vertical-align: top">
                                <table class="tableContent" style="border-width:1px;width:100%;border-collapse:collapse;table-layout:fixed;" cellspacing="0">
                                    <tr style="height:0px;">
                                        <th scope="col" style="width:120px;height:0px;border:none;"></th>
                                        <th scope="col" style="width:100px;height:0px;border:none;"></th>
                                        <th scope="col"  style="height:0px;border:none;"></th>
                                        <th scope="col"  style="width:25px;height:0px;border:none;"></th>
                                        <th scope="col"  style="width:25px;height:0px;border:none;"></td>
                                    </tr>

                                    <tr class="GridViewHeaderStyle" style="height:33px;" align="center">
                                        <th scope="col" >プロセス</th>
                                        <th scope="col"  colspan="2" >リスク・不安要素検討表</th>
                                        <th scope="col"  >不要</th>
                                        <th scope="col"  >完了</td>
                                    </tr>
                                    <tr>
                                        <td id="tdSp1Pro" runat="server" rowspan="2">営業プロセス(原価)</td>
                                        <td id="tdSp1Risk" runat="server" style="height:25px;" >リスク管理表</td>
                                        <td id="tdSp1RiskFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkSp1RiskManagementList" runat="server" NavigateUrl="~/Project.aspx?file=SP1M"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="fleSp1RiskManagementList" runat="server" Width="300px" OnClientUploadComplete="uploadCompleteSp1RiskManagementList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnSp1RiskManagementListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td id="tdSp1NoNeedFlg" runat="server" rowspan="2">
                                            <asp:CheckBox ID="chkSp1NoNeedFlg" runat="server" AutoPostBack="True" />
                                        </td>
                                        <td rowspan="2">
                                            <asp:CheckBox ID="chkSp1CompleteFlg" runat="server" AutoPostBack="True" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td id="tdSp1Check" runat="server" style="height:25px;" >チェックリスト</td>
                                        <td id="tdSp1CheckFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkSp1RiskCheckList" runat="server" NavigateUrl="~/Project.aspx?file=SP1C"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="fleSp1RiskCheckList" runat="server" Width="300px" OnClientUploadComplete="uploadCompleteSp1RiskCheckList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnSp1RiskCheckListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  id="tdSp2Pro" runat="server"  rowspan="2">営業プロセス(見積)</td>
                                        <td  id="tdSp2Risk" runat="server" style="height:25px;">リスク管理表</td>
                                        <td id="tdSp2RiskFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkSp2RiskManagementList" runat="server" NavigateUrl="~/Project.aspx?file=SP2M"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="fleSp2RiskManagementList" runat="server" Width="300px" OnClientUploadComplete="uploadCompleteSp2RiskManagementList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnSp2RiskManagementListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()"/>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td id="tdSp2NoNeedFlg" runat="server" rowspan="2">
                                            <asp:CheckBox ID="chkSp2NoNeedFlg" runat="server" AutoPostBack="True" />
                                        </td>
                                        <td rowspan="2">
                                            <asp:CheckBox ID="chkSp2CompleteFlg" runat="server" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td id="tdSp2Check" runat="server" style="height:25px;">チェックリスト</td>
                                        <td id="tdSp2CheckFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkSp2RiskCheckList" runat="server" NavigateUrl="~/Project.aspx?file=SP2C"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="fleSp2RiskCheckList" runat="server" Width="300px" OnClientUploadComplete="uploadCompleteSp2RiskCheckList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnSp2RiskCheckListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()"/>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td id="tdPpPro" runat="server"  rowspan="2">購買プロセス</td>
                                        <td id="tdPpRisk" runat="server" style="height: 25px;">リスク管理表</td>
                                        <td id="tdPpRiskFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkPpRiskManagementList" runat="server" NavigateUrl="~/Project.aspx?file=PPM"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="flePpRiskManagementList" runat="server" Width="300px" OnClientUploadComplete="uploadCompletePpRiskManagementLis" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnPpRiskManagementListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdPpNoNeedFlg" runat="server" rowspan="2">
                                            <asp:CheckBox ID="chkPpNoNeedFlg" runat="server" AutoPostBack="True" />
                                        </td>
                                        <td rowspan="2">
                                            <asp:CheckBox ID="chkPpCompleteFlg" runat="server" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td id="tdPpCheck" runat="server" style="height: 25px;">チェックリスト</td>
                                        <td id="tdPpCheckFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkPpRiskCheckList" runat="server" NavigateUrl="~/Project.aspx?file=PPC"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="flePpRiskCheckList" runat="server" Width="300px" OnClientUploadComplete="uploadCompletePpRiskCheckList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnPpRiskCheckListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()"/>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="tdDpPro" runat="server" rowspan="2">設計・開発プロセス</td>
                                        <td id="tdDpRisk" runat="server" style="height: 25px;">リスク管理表</td>
                                        <td id="tdDpRiskFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkDpRiskManagementList" runat="server" NavigateUrl="~/Project.aspx?file=DPM"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="fleDpRiskManagementList" runat="server" Width="300px" OnClientUploadComplete="uploadCompleteDpRiskManagementList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnDpRiskManagementListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td id="tdDpNoNeedFlg" runat="server" rowspan="2">
                                            <asp:CheckBox ID="chkDpNoNeedFlg" runat="server" AutoPostBack="True" />
                                        </td>
                                        <td rowspan="2">
                                            <asp:CheckBox ID="chkDpCompleteFlg" runat="server" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td id="tdDpCheck" runat="server" style="height: 25px;">チェックリスト</td>
                                        <td id="tdDpCheckFile" runat="server">
                                            <table style="width: 100%;" class="noStyle">
                                                <tr>
                                                    <td style="width:320px;word-break:break-all">
                                                        <asp:HyperLink ID="lnkDpRiskCheckList" runat="server" NavigateUrl="~/Project.aspx?file=DPC"></asp:HyperLink>
                                                        <ajaxToolkit:AsyncFileUpload ID="fleDpRiskCheckList" runat="server" Width="300px" OnClientUploadComplete="uploadCompleteDpRiskCheckList" />

                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:Button ID="btnDpRiskCheckListDel" runat="server" Text="削除" Height="20px" OnClientClick="return deleteFileConfirm()" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: left; width:350px; vertical-align: top;">
                                <asp:GridView ID="gdvRiskPreventionList" runat="server" BorderWidth="1px" AutoGenerateColumns="False" Width="100%" PageSize="200" AllowPaging="True" ShowHeaderWhenEmpty="True" CssClass="tableContent">
                                    <Columns>
                                        <asp:BoundField DataField="OPEN_ROUND" HeaderText="回">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" Height="25px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OPEN_DATE" HeaderText="開催日" DataFormatString="{0:MM月dd日}">
                                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField DataNavigateUrlFields="PROJECT_NO,SEQ_NO" DataNavigateUrlFormatString="RiskPrevention.aspx?pj_no={0}&amp;seq_no={1}" DataTextField="REPORT_CATEGORY" HeaderText="議事録">
                                            <ItemStyle Width="100px" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                       <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" Height="33px"/>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; table-layout: fixed;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnPjInput" runat="server" OnClientClick="return UpdateConfirm()" Text="案件登録" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnPjDel" runat="server" OnClientClick="return DeleteConfirm()" Text="案件削除" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnRiskPreventionInput" runat="server" OnClientClick="return RiskPreventionInputConfirm()" Text="リスク予防検討会登録" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnPjCancel" runat="server" OnClientClick="return cancelConfirm()" Text="キャンセル" />
                            </td>
                        </tr>
                    </table>
                   
                </td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPjInput" />
            <asp:PostBackTrigger ControlID="btnPjDel" />
            <asp:PostBackTrigger ControlID="btnRiskPreventionInput" />
            <asp:PostBackTrigger ControlID="btnPjCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
