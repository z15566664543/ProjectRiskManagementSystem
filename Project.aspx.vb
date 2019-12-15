Imports System.IO

Public Class Project
    Inherits System.Web.UI.Page

    Private ReadOnly SESSION_PPROJECT_ATTATCH As String = "SESSION_PROJECT_ATTATCH"
    Private ReadOnly SESSION_PPROJECT_ATTATCH_ADD As String = "SESSION_PROJECT_ATTATCH_ADD"
    Private ReadOnly SESSION_PPROJECT_ATTATCH_ADD_NAME As String = "SESSION_PROJECTATTATCH_ADD_NAME"
    Private ReadOnly SESSION_PPROJECT_ATTATCH_DELETE As String = "SESSION_PROJECT_ATTATCH_DELETE"
    Private ReadOnly TMP_SEQ_BEGIN As Integer = 10000

    ''' <summary>
    ''' 案件情報
    ''' </summary>
    ''' <remarks></remarks>
    Dim clsProject As New ProjectClass
    ''' <summary>
    ''' リスクテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtRiskPreventionNull As DataTable

    ''' <summary>
    ''' リスクのカラー
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GREY_COLOR As String = "#C0C0C0"

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Session("PjNo") = String.Empty

        If Not Request.QueryString("file") Is Nothing Then
            Dim strFileType As String
            strFileType = Request.QueryString("file").ToString

            Dim download As Byte()
            Dim fileName As String = String.Empty
            Dim strProjectNo As String = String.Empty

            If Session(SessionComm.PARA_RPPJ_NO) IsNot Nothing Then
                strProjectNo = Session(SessionComm.PARA_RPPJ_NO)
            End If

            Select Case strFileType
                Case "SP1M"
                    'ファイルダウンロード
                    If Session("sp1RiskManagementListFile") IsNot Nothing Then
                        download = Session("sp1RiskManagementListFile")
                        fileName = Session("sp1RiskManagementListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "0", "0", fileName, download)
                    End If

                Case "SP1C"
                    'ファイルダウンロード
                    If Session("sp1RiskCheckFile") IsNot Nothing Then
                        download = Session("sp1RiskCheckFile")
                        fileName = Session("sp1RiskCheckFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "0", "1", fileName, download)
                    End If

                Case "SP2M"
                    If Session("sp2RiskManagementListFile") IsNot Nothing Then
                        download = Session("sp2RiskManagementListFile")
                        fileName = Session("sp2RiskManagementListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "1", "0", fileName, download)
                    End If

                Case "SP2C"
                    'ファイルダウンロード                  
                    If Session("sp2RiskCheckListFile") IsNot Nothing Then
                        download = Session("sp2RiskCheckListFile")
                        fileName = Session("sp2RiskCheckListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "1", "1", fileName, download)
                    End If
                Case "PPM"
                    'ファイルダウンロード
                    If Session("ppRiskManagementListFile") IsNot Nothing Then
                        download = Session("ppRiskManagementListFile")
                        fileName = Session("ppRiskManagementListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "2", "0", fileName, download)
                    End If
                Case "PPC"
                    'ファイルダウンロード
                    If Session("ppRiskCheckListFile") IsNot Nothing Then
                        download = Session("ppRiskCheckListFile")
                        fileName = Session("ppRiskCheckListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "2", "1", fileName, download)
                    End If

                Case "DPM"
                    'ファイルダウンロード
                    If Session("dpRiskManagementListFile") IsNot Nothing Then
                        download = Session("dpRiskManagementListFile")
                        fileName = Session("dpRiskManagementListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "3", "0", fileName, download)
                    End If


                Case "DPC"
                    'ファイルダウンロード
                    If Session("dpRiskManagementListFile") IsNot Nothing Then
                        download = Session("dpRiskCheckListFile")
                        fileName = Session("dpRiskCheckListFileName")
                    Else
                        ProjectEntity.GetRiskManagementListFile(strProjectNo, "3", "1", fileName, download)
                    End If

                Case "PA1"
                    '新規ファイル場合
                    If Session("projectAttatchFile1") IsNot Nothing Then
                        download = Session("projectAttatchFile1")
                        fileName = Session("projectAttatchFile1Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile1")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA2"
                    '新規ファイル場合
                    If Session("projectAttatchFile2") IsNot Nothing Then
                        download = Session("projectAttatchFile2")
                        fileName = Session("projectAttatchFile2Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile2")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA3"
                    '新規ファイル場合
                    If Session("projectAttatchFile3") IsNot Nothing Then
                        download = Session("projectAttatchFile3")
                        fileName = Session("projectAttatchFile3Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile3")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA4"
                    '新規ファイル場合
                    If Session("projectAttatchFile4") IsNot Nothing Then
                        download = Session("projectAttatchFile4")
                        fileName = Session("projectAttatchFile4Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile4")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA5"
                    '新規ファイル場合
                    If Session("projectAttatchFile5") IsNot Nothing Then
                        download = Session("projectAttatchFile5")
                        fileName = Session("projectAttatchFile5Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile5")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA6"
                    '新規ファイル場合
                    If Session("projectAttatchFile6") IsNot Nothing Then
                        download = Session("projectAttatchFile6")
                        fileName = Session("projectAttatchFile6Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile6")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA7"
                    '新規ファイル場合
                    If Session("projectAttatchFile7") IsNot Nothing Then
                        download = Session("projectAttatchFile7")
                        fileName = Session("projectAttatchFile7Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile7")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA8"
                    '新規ファイル場合
                    If Session("projectAttatchFile8") IsNot Nothing Then
                        download = Session("projectAttatchFile8")
                        fileName = Session("projectAttatchFile8Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile8")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA9"
                    '新規ファイル場合
                    If Session("projectAttatchFile9") IsNot Nothing Then
                        download = Session("projectAttatchFile9")
                        fileName = Session("projectAttatchFile9Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile9")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
                Case "PA10"
                    '新規ファイル場合
                    If Session("projectAttatchFile10") IsNot Nothing Then
                        download = Session("projectAttatchFile10")
                        fileName = Session("projectAttatchFile10Name")
                    Else
                        '既存ファイル場合
                        Dim clsFile As AttatchFileClass
                        clsFile = Session("AttatchFile10")
                        'ファイル取得
                        ProjectEntity.GetProjectAttatchFile(strProjectNo, clsFile.file_seq_no, fileName, download)
                    End If
            End Select

            'ファイルダウンロード
            If download IsNot Nothing Then
                DownLoadFile(download, fileName)
            End If

            Return
        End If

        If Not Request.QueryString("fid") Is Nothing Then

            Dim seq = CInt(Request.QueryString("fid").ToString())

            Dim hasError = False

            If seq < TMP_SEQ_BEGIN Then

                ' ファイルはDBに存在している場合
                Dim attathcDt As DataTable
                attathcDt = ProjectEntity.GetProjectAttatch(Session(SessionComm.PARA_RPPJ_NO).ToString,
                                                      Request.QueryString("fid").ToString())
                If attathcDt.Rows.Count > 0 Then

                    DownLoadFile(attathcDt.Rows(0).Item("ATTATCH_FILE"), attathcDt.Rows(0).Item("ATTATCH_FILE_NAME").ToString)
                Else
                    hasError = True
                End If

            Else

                ' ファイルは新規登録した（SESSIONに格納）場合
                Dim fileNames As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD_NAME)
                Dim files As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD)
                DownLoadFile(files(seq - TMP_SEQ_BEGIN), fileNames(seq - TMP_SEQ_BEGIN))

            End If

            If hasError Then
                ' ファイルが存在しません。削除された可能性があります
                Dim msg As String
                msg = MessageComm.GetMessageContext("1", "111", Nothing)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)
            End If

            Return
        End If

        If Not Me.IsPostBack Then
            GetProjectInfo()
        End If

        'Read Only
        Me.txtPjNo.Enabled = False
        Me.txtOrderCd.Enabled = False
        Me.txtRelateOrderCd.Enabled = False
        Me.txtOrderNm.Enabled = False
        Me.txtCompyNm.Enabled = False
        Me.txtJyuchuCrr.Enabled = False
        Me.txtNokiYmd.Enabled = False
        Me.txtJyuchuSectNm.Enabled = False
        Me.txtJyuchuUserNm.Enabled = False
        Me.txtProductSectNm.Enabled = False
        Me.txtRiskPreventionManager.Enabled = False
        Me.txtProductUser.Enabled = False

    End Sub
    ''' <summary>
    ''' 初期化
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitialPage()

        ' 一時ファイルをクリア
        Session(SESSION_PPROJECT_ATTATCH_DELETE) = New ArrayList()
        Session(SESSION_PPROJECT_ATTATCH_ADD) = New ArrayList()
        Session(SESSION_PPROJECT_ATTATCH_ADD_NAME) = New ArrayList()

        ''0. ヘッダ
        '① 起票者ラベル(lblCreatedUserName)Session変数UserNameの値をセットする
        Me.lblCreatedUserName.Text = Session(SessionComm.SESSION_USER_NAME)
        '② 最終更新者ラベル(lblModifiedUserName)ブランクをセットする
        Me.lblModifiedUserName.Text = String.Empty
        '1. オーダ情報
        '① 案件番号テキストボックス(txtPjNo)ブランクをセットする　※採番は案件登録の直前に行う
        Me.txtPjNo.Text = String.Empty

        '② プロセスラジオボタン(rdoProcess)PROCESS_Mテーブルとバインドする
        Dim strSql As String = "SELECT PROCESS_NO, PROCESS_NAME FROM PROCESS_M ORDER BY PROCESS_NO"
        MethodComm.SetRadioListValue(Me.rdoProcess, strSql)

        '③ 工事名称（仮）テキストボックス(txtPjNameTemp)ブランクをセットする
        Me.txtPjNameTemp.Text = String.Empty

        '④ 顧客テキストボックス(txtCustomerName)ブランクをセットする
        Me.txtCustomerName.Text = String.Empty

        '⑤ オーダテキストボックス(txtOrderCd)ブランクをセットする
        Me.txtOrderCd.Text = String.Empty

        '⑥ 関連オーダテキストボックス(txtRelateOrderCd)ブランクをセットする
        Me.txtRelateOrderCd.Text = String.Empty

        '⑦ 工事名称テキストボックス(txtOrderNm)ブランクをセットする 
        Me.txtOrderNm.Text = String.Empty
        '工事名称行自体を非表示にする
        trOrderNm.Visible = False

        '⑧ 顧客テキストボックス(txtCompyNm)ブランクをセットする
        Me.txtCompyNm.Text = String.Empty
        ' 顧客行自体を非表示にする
        Me.trCompyNm.Visible = False
        '⑨ 顧客区分ラジオボタン(rdoCustomerType)CUSTOMER_TYPE_Mテーブルとバインドする
        strSql = "SELECT CUSTOMER_TYPE_NO, CUSTOMER_TYPE_NAME FROM CUSTOMER_TYPE_M ORDER BY CUSTOMER_TYPE_NO"
        MethodComm.SetRadioListValue(Me.rdoCustomerType, strSql)

        '⑩ 受注金額テキストボックス(txtJyuchuCrr)ブランクをセットする 
        Me.txtJyuchuCrr.Text = String.Empty

        '⑪ 納期日テキストボックス(txtNokiYmd)ブランクをセットする
        Me.txtNokiYmd.Text = String.Empty

        '受注金額行・納期日行自体を非表示にする
        Me.trJyuchuCrrNokiYmd.Visible = False
        '⑫ 受注部門テキストボックス(txtJyuchuSectNm)ブランクをセットする
        Me.txtJyuchuSectNm.Text = String.Empty

        '⑬ 受注担当者テキストボックス(txtJyuchuUserNm)ブランクをセットする
        Me.txtJyuchuUserNm.Text = String.Empty

        '受注部門・受注担当者行を非表示にする
        Me.trJyuchuSectNmJyuchuUserNm.Visible = False
        '⑭ 製造部門テキストボックス(txtProductSectNm)「※製造部門担当者の所属が自動セットされます」を表示する
        Me.txtProductSectNm.Text = "※製造部門担当者の所属が自動セットされます"

        '⑮ 製造部門担当者(txtProductUser)ブランクをセットする
        Me.txtProductUser.Text = String.Empty

        '2. 支社間取引
        '① 支社間取引の有無ラジオボタン(rdoBranchTransactionFlg)        いずれの選択肢も未チェックとする()
        rdoBranchTransactionFlg.ClearSelection()

        '② 支援支社ドロップダウンリスト(ddlBranchList)
        strSql = "SELECT A01M002_SECT_NM, A01M002_SECT_CD FROM A01ADMIN.A01M002_SECTION  "
        strSql += "WHERE (A01M002_APLEND_YMD = 21001231) AND ((A01M002_ORGLYR_CD = '01') OR (A01M002_ORGLYR_CD = '02'))  "
        strSql += "ORDER BY A01M002_SECT_CD "
        MethodComm.SetDropDownListValue(Me.ddlBranchList, strSql, True)

        '3. リスク管理情報
        '① 支社品質管理責任者テキストボックス(txtBranchQualityManager)ブランクをセットする
        Me.txtBranchQualityManager.Text = String.Empty

        '② 部品質管理責任者テキストボックス(txtSectionQualityManager)ブランクをセットする
        Me.txtSectionQualityManager.Text = String.Empty

        '③ グループ品質管理責任者テキストボックス(txtGroupQualityManager)ブランクをセットする
        Me.txtGroupQualityManager.Text = String.Empty

        '④ プロジェクト品質管理責任者テキストボックス(txtProjectQualityManager)ブランクをセットする
        Me.txtProjectQualityManager.Text = String.Empty

        '⑤ リスク予防管理責任者テキストボックス(txtRiskPreventionManager)ブランクをセットする
        Me.txtRiskPreventionManager.Text = String.Empty

        '⑥ リスク予防管理対象 500万円以上チェックボックス(chkRpm500MilFlg)未チェックとする
        Me.chkRpm500MilFlg.Checked = False

        '⑦ リスク予防管理対象 初品チェックボックス(chkRpmFirstProductFlg)未チェックとする
        Me.chkRpmFirstProductFlg.Checked = False

        '⑧ リスク予防管理対象 初品理由ドロップダウンリスト(ddlFirstProduct) FIRST_PRODUCT_Mテーブルとバインドする
        strSql = "SELECT FIRST_PRODUCT_NO, SELECT_CONTENT FROM FIRST_PRODUCT_M ORDER BY FIRST_PRODUCT_NO "
        MethodComm.SetDropDownListValue(Me.ddlFirstProduct, strSql, True)

        '⑨ リスク予防管理対象 初品理由テキストボックス(txtRpmFirstProductCause)ブランクをセットする
        Me.txtRpmFirstProductCause.Text = String.Empty

        '⑩ リスク予防管理対象 特殊チェックボックス(chkRpmSpecialProductFlg)未チェックとする
        Me.chkRpmSpecialProductFlg.Checked = False
        '⑪ リスク予防管理対象 特殊理由ドロップダウンリスト(ddlSpecialProduct)SPECIAL_PRODUCT_Mテーブルとバインドする
        strSql = "SELECT SPECIAL_PRODUCT_NO, SELECT_CONTENT FROM SPECIAL_PRODUCT_M ORDER BY SPECIAL_PRODUCT_NO  "
        MethodComm.SetDropDownListValue(Me.ddlSpecialProduct, strSql, True)

        '⑫ リスク予防管理対象 特殊理由テキストボックス(txtRpmSpecialProductCause)ブランクをセットする
        Me.txtRpmSpecialProductCause.Text = String.Empty

        'リスク予防管理対象のコントロール使用可設定
        SetRmpControlEnabled()

        '⑬ リスク予防管理区分ラジオボタン(rboRpmType)いずれの選択肢も未チェックとする
        Me.rdoRpmType.ClearSelection()

        ''⑭ 添付ファイル ファイル参照ボタン(fleProjectAttatch(N))　※N=10 fleProjectAttatch1のみブランク状態で添付ファイル欄左端に表示する
        Dim dt As DataTable
        Dim isEditAble As Boolean = True

        dt = GetRiskPreventionAttachNull()
        Dim column As DataColumn = New DataColumn("isEditAble")
        dt.Columns.Add(column)
        For Each row In dt.Rows
            row("isEditAble") = isEditAble.ToString
        Next

        grdProjectAttatch.DataSource = dt
        grdProjectAttatch.DataBind()
        Session(SESSION_PPROJECT_ATTATCH) = dt

        ''⑮ 添付ファイル 削除ボタン(btnProjectAttatchDel(N))　※N=10 すべてのボタンを非表示にする
        If (dt.Rows.Count >= 10) Then
            fleProjectAttatch.Visible = False
        Else
            fleProjectAttatch.Visible = True
        End If

        fleProjectAttatch.Enabled = True

        '⑰ 案件タイプラジオボタン(rdoProjectType)rdoProjectType PROJECT_TYPE_Mテーブルとバインドする
        strSql = "SELECT PROJECT_TYPE_NO, PROJECT_TYPE_NAME FROM PROJECT_TYPE_M ORDER BY PROJECT_TYPE_NO "
        MethodComm.SetRadioListValue(Me.rdoProjectType, strSql)

        ''4. リスク予防管理活動
        'リスク不安要素検討表
        '① 営業プロセス(原価)
        'リスク管理表欄
        '参照ボタン(fleSp1RiskManagementList)のみブランク状態で表示する
        Me.fleSp1RiskManagementList.Visible = True
        Me.btnSp1RiskManagementListDel.Visible = False

        Me.lnkSp1RiskManagementList.Text = String.Empty
        Me.lnkSp1RiskCheckList.Text = String.Empty

        '参照ボタン(fleSp1RiskCheckList)のみブランク状態で表示する
        Me.fleSp1RiskCheckList.Visible = True
        Me.btnSp1RiskCheckListDel.Visible = False

        '不要チェックボックス(chkSp1NoNeedFlg)
        '未チェックとする
        Me.chkSp1NoNeedFlg.Checked = False

        '完了チェックボックス(chkSp1CompleteFlg)
        '未チェックとする
        Me.chkSp1CompleteFlg.Checked = False

        '② 営業プロセス(見積)
        'リスク管理表欄
        '参照ボタン(fleSp2RiskManagementList)のみブランク状態で表示する
        Me.fleSp2RiskManagementList.Visible = True
        Me.btnSp2RiskManagementListDel.Visible = False

        Me.lnkSp2RiskManagementList.Text = String.Empty
        Me.lnkSp2RiskCheckList.Text = String.Empty

        '参照ボタン(fleSp2RiskCheckList)のみブランク状態で表示する
        Me.fleSp2RiskCheckList.Visible = True
        Me.btnSp2RiskCheckListDel.Visible = False

        '不要チェックボックス(chkSp2NoNeedFlg)
        '未チェックとする
        Me.chkSp2NoNeedFlg.Checked = False

        '完了チェックボックス(chkSp2CompleteFlg)
        '未チェックとする
        Me.chkSp2CompleteFlg.Checked = False

        '③ 購買プロセス
        'リスク管理表欄
        '参照ボタン(fleSp3RiskManagementList)のみブランク状態で表示する
        Me.flePpRiskManagementList.Visible = True
        Me.btnPpRiskManagementListDel.Visible = False

        Me.lnkPpRiskManagementList.Text = String.Empty
        Me.lnkPpRiskCheckList.Text = String.Empty

        '参照ボタン(fleSp3RiskCheckList)のみブランク状態で表示する
        Me.flePpRiskCheckList.Visible = True
        Me.btnPpRiskCheckListDel.Visible = False

        '不要チェックボックス(chkSp3NoNeedFlg)
        '未チェックとする
        Me.chkPpNoNeedFlg.Checked = False

        '完了チェックボックス(chkSp3CompleteFlg)
        '未チェックとする
        Me.chkPpCompleteFlg.Checked = False

        '④ 設計・開発プロセス
        'リスク管理表欄
        '参照ボタン(fleSp4RiskManagementList)のみブランク状態で表示する
        'チェックリスト欄
        Me.fleDpRiskManagementList.Visible = True
        Me.btnDpRiskManagementListDel.Visible = False

        Me.lnkDpRiskManagementList.Text = String.Empty
        Me.lnkDpRiskCheckList.Text = String.Empty

        '参照ボタン(fleSp4RiskCheckList)のみブランク状態で表示する
        Me.fleDpRiskCheckList.Visible = True
        Me.btnDpRiskCheckListDel.Visible = False

        '不要チェックボックス(chkSp4NoNeedFlg)
        '未チェックとする
        Me.chkDpNoNeedFlg.Checked = False
        '完了チェックボックス(chkSp4CompleteFlg)
        '未チェックとする
        Me.chkDpCompleteFlg.Checked = False

        'リスク不安要素検討表テーブルのカラー設定
        SetRiskManagementRowColor()

        'リスク予防・管理検討会実施履歴
        '⑤ リスク予防・管理検討会議事録リスト(lblOpenRound,lblOpenDate,lnkRiskPrevention)回・開催日・議事録欄いずれもブランクとする
        '空のリスク予防・管理検討会実施履歴情報
        gdvRiskPreventionList.DataSource = GetRiskPreventionTableNull()
        gdvRiskPreventionList.DataBind()
        'フッタ
        '① リスク予防検討会登録ボタン(btnRiskPreventionInput)のEnabledプロパティをFalseに設定する
        Me.btnRiskPreventionInput.Enabled = False

        '② 最終更新日時隠し項目(hdnModifiedOn)ブランクをセットする
        Me.hdnModifiedOn.Value = String.Empty

        '初品選択肢ドロップダウンリスト(ddlFirstProduct)のEnabledをFalseにセットする
        Me.ddlFirstProduct.Enabled = False
        '初品理由テキストボックス(txtRpmFirstProductCause)のEnabledをFalseにセットする
        Me.txtRpmFirstProductCause.Enabled = False
        '特殊選択肢ドロップダウンリスト(ddlSpecialProduct)のEnabledをFlaseにセットする
        Me.ddlSpecialProduct.Enabled = False
        '特殊理由テキストボックス(txtRpmSpecialProductCause)のEnabledをFlaseにセットする
        Me.txtRpmSpecialProductCause.Enabled = False

    End Sub

    ''' <summary>
    ''' アクセス権を判断する
    ''' </summary>
    ''' <param name="clsProject">案件クラス</param>
    ''' <returns>アクセス権：True:編集,False:参照</returns>
    ''' <remarks></remarks>
    Private Function GeteditAble(ByVal clsProject As ProjectClass) As Boolean

        Dim a01m01SectCd = clsProject.a01m01SectCd
        Dim createdUserId = clsProject.createdUserId
        Dim isEditAble = False

        '呼出対象案件に対するユーザアクセス権を設定する
        If Session(SessionComm.SESSION_ADMIN_FLG) = "1" Then
            '① Session変数AdminFlgの値が1
            'アクセス権を編集にセットする
            isEditAble = True
        ElseIf Left(Session(SessionComm.SESSION_USER_SECT_CD).ToString, 2) = "1" Then
            '② Session変数UserSectCdの値の上2桁目が1
            'アクセス権を参照にセットする
            isEditAble = False
        ElseIf Left(Session(SessionComm.SESSION_USER_SECT_CD).ToString, 2) <> Left(a01m01SectCd, 2) Then
            '③ Session変数UserSectCdの値の上2桁目と、A01ADMIN.A01M010_USER.A01M010_SECT_CDの上2桁目が異なる
            'アクセス権を参照にセットする
            isEditAble = False
        ElseIf Session(SessionComm.SESSION_USER_ID).ToString = createdUserId Then
            '④ Session変数UserIdの値と、PROJECT.CREATED_USER_IDの値が一致
            'アクセス権を編集にセットする
            isEditAble = True
        ElseIf (Left(Session(SessionComm.SESSION_USER_SECT_CD).ToString, 2) = Left(a01m01SectCd, 2)) _
            And Session(SessionComm.SESSION_USER_ID).ToString <> createdUserId _
            And Convert.ToInt32(Session(SessionComm.SESSION_USER_SECT_CD).ToString) < 300 Then
            '⑤ Session変数UserSectCdの値の上2桁目と、A01ADMIN.A01M010_USER.A01M010_SECT_CDの上2桁目が一致、かつ
            'Session変数UserIdの値と、PROJECT.CREATED_USER_IDの値が異なる、かつ
            'Session変数UserPostClsCdの値が300以下
            'アクセス権を編集にセットする
            isEditAble = True
        Else
            '⑥ ①～⑤の条件に該当しない場合
            'アクセス権を参照にセットする
            isEditAble = False
        End If
        Return isEditAble

    End Function
    ''' <summary>
    ''' ページロードイベント(Page_Load)　
    ''' ※登録済み案件の参照・修正（検索結果画面またはリスク予防検討会画面から表示された）の場合
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetProjectInfo()
        Dim strProjectNo As String = String.Empty
        Dim isEditenable As Boolean = False

        'ファイルSessionをクリア
        Session("projectAttatchFile1") = Nothing
        Session("projectAttatchFile2") = Nothing
        Session("projectAttatchFile3") = Nothing
        Session("projectAttatchFile4") = Nothing
        Session("projectAttatchFile5") = Nothing
        Session("projectAttatchFile6") = Nothing
        Session("projectAttatchFile7") = Nothing
        Session("projectAttatchFile8") = Nothing
        Session("projectAttatchFile9") = Nothing
        Session("projectAttatchFile10") = Nothing
        Session("sp1RiskManagementListFile") = Nothing
        Session("sp1RiskCheckFile") = Nothing
        Session("sp2RiskManagementListFile") = Nothing
        Session("sp2RiskCheckListFile") = Nothing
        Session("ppRiskManagementListFile") = Nothing
        Session("ppRiskCheckListFile") = Nothing
        Session("dpRiskManagementListFile") = Nothing
        Session("dpRiskCheckListFile") = Nothing

        ' 一時ファイルをクリア
        Session(SESSION_PPROJECT_ATTATCH_DELETE) = New ArrayList()
        Session(SESSION_PPROJECT_ATTATCH_ADD) = New ArrayList()
        Session(SESSION_PPROJECT_ATTATCH_ADD_NAME) = New ArrayList()

        'リストファイルを初期化
        Me.hdnSp1RiskManagementListIsUpdat.Value = String.Empty
        Me.hdnSp1RiskManagementListIsDelete.Value = String.Empty
        Me.hdnSp1RiskCheckListIsUpdate.Value = String.Empty
        Me.hdnSp1RiskCheckListIsDelete.Value = String.Empty
        Me.hdnSp2RiskManagementListIsUpdat.Value = String.Empty
        Me.hdnSp2RiskManagementListIsDelete.Value = String.Empty
        Me.hdnSp2RiskCheckListIsUpdate.Value = String.Empty
        Me.hdnSp2RiskCheckListIsDelete.Value = String.Empty
        Me.hdnPpRiskManagementListIsUpdat.Value = String.Empty
        Me.hdnPpRiskManagementListIsDelete.Value = String.Empty
        Me.hdnPpRiskCheckListIsUpdate.Value = String.Empty
        Me.hdnPpRiskCheckListIsDelete.Value = String.Empty
        Me.hdnDpRiskManagementListIsUpdat.Value = String.Empty
        Me.hdnDpRiskManagementListIsDelete.Value = String.Empty

        'クエリストリングpj_noから案件番号を取得する
        If Not Request.QueryString(SessionComm.PARA_RPPJ_NO) Is Nothing Then
            strProjectNo = Request.QueryString(SessionComm.PARA_RPPJ_NO).ToString()
            Session(SessionComm.PARA_RPPJ_NO) = strProjectNo
        ElseIf Session("PjNo") IsNot Nothing Then
            If Session("PjNo").ToString <> String.Empty Then
                strProjectNo = Session("PjNo").ToString
                Session(SessionComm.PARA_RPPJ_NO) = Session("PjNo").ToString

            End If
        End If
        '対象案件がDB内に存在するか確認するため、以下のSQLを発行する
        If Not ProjectEntity.CheckProjectIsExits(strProjectNo, clsProject) Then
            '取得レコードが0件の場合は、画面上にメッセージ「案件番号：xxxxxxxxxx の案件を表示できません。削除された可能性があります」を表示し、
            'xxxxxxxxxxには、PROJECT.PROJECT_NOをセットする
            If strProjectNo <> String.Empty Then
                Dim msg As String
                Dim tList As List(Of String) = New List(Of String)
                tList.Add(strProjectNo)
                msg = MessageComm.GetMessageContext("2", "006", tList)
                Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                        "",
                                                        "showMessage('" & msg & "');",
                                                        True)
            End If

            'プランクの案件画面を表示するため、"(1) ページロードイベント(Page_Load)　※新規登録（メニュー画面の案件登録ボタンから表示された）の場合"を実行する
            InitialPage()
            Return
        End If

        InitialPage()

        '呼出対象案件に対するユーザアクセス権を設定する
        isEditenable = GeteditAble(clsProject)

        ''取得したSQL結果から案件画面の各項目に値をセットする
        '0. ヘッダ
        '① 起票者ラベル(lblCreatedUserName)
        'PROJECT.CREATED_USER_NAMEの値をセットする
        Me.lblCreatedUserName.Text = clsProject.lblCreatedUserName
        '② 最終更新者ラベル(lblModifiedUserName)
        'PROJECT.MODIFIED_USER_NAMEの値をセットする
        Me.lblModifiedUserName.Text = clsProject.lblModifiedUserName

        '1. オーダ情報
        '① 案件番号テキストボックス
        'PROJECT.PROJECT_NOの値をセットする
        Me.txtPjNo.Text = clsProject.txtPjNo

        '② プロセスラジオボタン(rdoProcess)
        'PROJECT.PROCESS_NOの値と等しい値を持つ選択肢をチェックする
        Me.rdoProcess.SelectedValue = clsProject.rdoProcess
        'アクセス権が参照の場合は、rdoProcessのEnabledプロパティをFalseにセットする
        Me.rdoProcess.Enabled = isEditenable

        '③ 工事名称（仮）テキストボックス(txtPjNameTemp)
        'PROJECT.PROJECT_NAME_TEMPの値をセットする
        Me.txtPjNameTemp.Text = clsProject.txtPjNameTemp
        'アクセス権が参照の場合は、txtPjNameTempのEnabledプロパティをFalseにセットする
        Me.txtPjNameTemp.Enabled = isEditenable

        '④ 顧客テキストボックス(txtCustomerName)
        'PROJECT.CUSTOMER_NAMEの値をセットする
        Me.txtCustomerName.Text = clsProject.txtCustomerName
        'アクセス権が参照の場合は、txtCustomerNameのEnabledプロパティをFalseにセットする
        Me.txtCustomerName.Enabled = isEditenable

        '⑤ オーダテキストボックス(txtOrderCd,imgOrderSearch)
        'PROJECT.ORDER_CDの値をセットする
        Me.txtOrderCd.Text = clsProject.txtOrderCd
        'アクセス権が参照の場合は、txtOrderCdのEnabledプロパティをFalseにセットする
        Me.txtOrderCd.Enabled = False
        'アクセス権が参照の場合は、オーダ検索アイコン(imgOrderSearch)のEnabledプロパティをFalseにセットする
        Me.imgOrderSearch.Enabled = isEditenable
        Me.btnDeleteOrderNo.Enabled = isEditenable

        'PROJECT.ORDER_CDの値がNULL・ブランクでない場合は以下の処理を実行する
        If Me.txtOrderCd.Text = String.Empty Then
            '※オーダテキストボックスの値が削除された場合は以下の項目の値行を非表示にする。	
            '非表示にする項目			
            '① 工事名称行(txtOrderNm)	
            Me.trOrderNm.Visible = False
            '② 顧客行(txtCompyNm)		
            Me.trCompyNm.Visible = False
            '③ 受注金額、納期日行(txtJyuchuCrr,txtNokiYmd)	
            Me.trJyuchuCrrNokiYmd.Visible = False
            '④ 受注部門、受注担当者行(txtJyuchuSectNm,txtJyuchuUserNm)			
            Me.trJyuchuSectNmJyuchuUserNm.Visible = False

            '初期表示の項目を再表示する			
            '① 工事名称（仮）行(txtPjNameTemp)			
            Me.trPjNameTemp.Visible = True
            '② 顧客行(txtCustomerName)
            Me.trCustomerName.Visible = True

        Else

            '初期表示の項目を非表示にする			
            '① 工事名称（仮）行(txtPjNameTemp)		
            Me.trPjNameTemp.Visible = False
            '② 顧客行(txtCustomerName)		
            Me.trCustomerName.Visible = False

            '初期非表示の項目を表示する			
            '① 工事名称行(txtOrderNm)		
            Me.trOrderNm.Visible = True
            '② 顧客行(txtCompyNm)		
            Me.trCompyNm.Visible = True
            '③ 受注金額、納期日行(txtJyuchuCrr,txtNokiYmd)
            Me.trJyuchuCrrNokiYmd.Visible = True
            '④ 受注部門、受注担当者行(txtJyuchuSectNm,txtJyuchuUserNm)			
            Me.trJyuchuSectNmJyuchuUserNm.Visible = True
        End If

        'I. オーダ関連の情報を取得するため以下のSQLを発行する
        Dim relateOrderInfo As New RelateOrderClass
        relateOrderInfo = ProjectEntity.GetRelateOrderInfo(clsProject.txtOrderCd)

        '⑥ 関連オーダテキストボックス(txtRelateOrderCd,imgRelateOrderSearch)
        'PROJECT.RELATE_ORDER_CDの値をセットする
        Me.txtRelateOrderCd.Text = clsProject.txtRelateOrderCd
        'アクセス権が参照の場合は、txtRelateOrderCdのEnabledプロパティをFalseにセットする
        Me.txtRelateOrderCd.Enabled = False
        'アクセス権が参照の場合は、オーダ検索アイコン(imgRelateOrderSearch)のEnabledプロパティをFalseにセットする
        Me.imgRelateOrderSearch.Enabled = isEditenable
        Me.btnRelateOrderDel.Enabled = isEditenable

        '⑦ 工事名称テキストボックス(txtOrderNm)
        'I.のA.A01M009_ORDER_NMの値をセットする
        Me.txtOrderNm.Text = relateOrderInfo.a01m009OrderNm
        'アクセス権が参照の場合は、txtOrderNmのEnabledプロパティをFalseにセットする
        Me.txtOrderNm.Enabled = False

        '⑧ 顧客テキストボックス(txtCompyNm)
        'I.のE.A01M015_COMPY_NMの値をセットする
        Me.txtCompyNm.Text = relateOrderInfo.a01m015CompyNm
        'アクセス権が参照の場合は、txtCompyNmのEnabledプロパティをFalseにセットする
        Me.txtCompyNm.Enabled = False

        '⑨ 顧客区分ラジオボタン(rdoCustomerType)
        'PROJECT.CUSTOMER_TYPE_NOの値と等しい値を持つ選択肢をチェックする
        Me.rdoCustomerType.SelectedValue = clsProject.rdoCustomerType
        'アクセス権が参照の場合は、rdoCustomerTypeのEnabledプロパティをFalseにセットする
        Me.rdoCustomerType.Enabled = isEditenable

        '⑩ 受注金額テキストボックス(txtJyuchuCrr)
        'I.のB.A01M014_JYUCHU_CRRの値をセットする
        If relateOrderInfo.a01m014JyuchuCrr <> String.Empty Then
            Me.txtJyuchuCrr.Text = String.Format("{0:C}", Int32.Parse(relateOrderInfo.a01m014JyuchuCrr))
        End If

        'アクセス権が参照の場合は、txtJyuchuCrrのEnabledプロパティをFalseにセットする
        Me.txtJyuchuCrr.Enabled = False

        '⑪ 納期日テキストボックス(txtNokiYmd)
        'I.のB.A01M014_NOKI_YMDの値をセットする
        If relateOrderInfo.a01m014NokiYmd <> String.Empty Then
            Me.txtNokiYmd.Text = Format(Date.Parse(Format(CInt(relateOrderInfo.a01m014NokiYmd), "0000/00/00")), "yyyy/MM/dd")
        End If
        'アクセス権が参照の場合は、txtNokiYmdのEnabledプロパティをFalseにセットする
        Me.txtNokiYmd.Enabled = False

        '⑫ 受注部門テキストボックス(txtJyuchuSectNm)
        'I.のC.A01M002_ALLSECT_NMの値をセットする
        Me.txtJyuchuSectNm.Text = relateOrderInfo.a01m002AllsectNm
        'アクセス権が参照の場合は、txtJyuchuSectNmのEnabledプロパティをFalseにセットする
        Me.txtJyuchuSectNm.Enabled = False

        '⑬ 受注担当者テキストボックス(txtJyuchuUserNm)
        'I.のD.A01M010_FULLNAMEの値をセットする
        Me.txtJyuchuUserNm.Text = relateOrderInfo.a01m010Fullname
        'アクセス権が参照の場合は、txtJyuchuUserNmのEnabledプロパティをFalseにセットする
        Me.txtJyuchuUserNm.Enabled = False

        '⑭ 製造部門テキストボックス(txtProductSectNm,imgProductSectSearch)
        'PROJECT.PRODUCT_SECT_NMの値をセットする
        Me.txtProductSectNm.Text = clsProject.txtProductSectNm
        'TODO PROJECT.PRODUCT_SECT_NMの値がNull・ブランクの場合は「※製造部門担当者の所属が自動セットされます」をセットする

        'アクセス権が参照の場合は、txtProductSectNmのEnabledプロパティをFalseにセットする
        Me.txtProductSectNm.Enabled = False
        'アクセス権が参照の場合は、部門検索アイコン(imgProductSectSearch)のEnabledプロパティをFalseにセットする
        Me.imgProductSectSearch.Enabled = isEditenable

        '⑮ 製造部門担当者(txtProductUser,imgProductUserSearch)
        'PROJECT.PRODUCT_USER_FULLNAMEの値をセットする
        Me.txtProductUser.Text = clsProject.txtProductUser
        'アクセス権が参照の場合は、txtProductUserのEnabledプロパティをFalseにセットする
        Me.txtProductUser.Enabled = False
        'アクセス権が参照の場合は、社員検索アイコン(imgProductUserSearch)のEnabledプロパティをFalseにセットする
        Me.imgProductUserSearch.Enabled = isEditenable

        '2. 支社間取引
        '① 支社間取引の有無ラジオボタン(rdoBranchTransactionFlg)
        'PROJECT.BRANCH_TRANSACTION_FLGの値と等しい値を持つ選択肢をチェックする
        Me.rdoBranchTransactionFlg.SelectedValue = clsProject.rdoBranchTransactionFlg

        'アクセス権が参照の場合は、rdoBranchTransactionFlgのEnabledプロパティをFalseにセットする
        Me.rdoBranchTransactionFlg.Enabled = isEditenable

        If Me.rdoBranchTransactionFlg.SelectedItem.Text = "有：自支社が支援支社" Then
            '「有：自支社が支援支社」を選択した場合は、支援支社ラベル(lblSupportBranch)のTextを「窓口支社」に変更する
            Me.lblSupportBranch.Text = "窓口支社"
        Else
            '「有：自支社が支援支社」以外を選択した場合は、支援支社ラベル(lblSupportBranch)のTextを「支援支社」に変更する
            Me.lblSupportBranch.Text = "支援支社"
        End If

        '② 支援支社ドロップダウンリスト(ddlBranchList)
        'PROJECT.SUPPORT_BRANCH_IDの値と等しい値を持つ選択肢を選択状態にする
        If clsProject.ddlBranchListId.Trim <> String.Empty Then
            Me.ddlBranchList.SelectedValue = clsProject.ddlBranchListId
        End If
        'アクセス権が参照の場合は、ddlBranchListのEnabledプロパティをFalseにセットする
        Me.ddlBranchList.Enabled = isEditenable

        If Me.rdoBranchTransactionFlg.SelectedItem.Text = "無" Then
            'ddlBranchListのEnabledプロパティをFalseにセットする
            Me.ddlBranchList.Enabled = False
        End If
        '3. リスク管理情報
        '① 支社品質管理責任者テキストボックス(txtBranchQualityManager,imgBranchQualityManagerSearch)
        'PROJECT.BRANCH_QUALITY_MANAGERの値をセットする
        Me.txtBranchQualityManager.Text = clsProject.txtBranchQualityManager
        'アクセス権が参照の場合は、txtBranchQualityManagerのEnabledプロパティをFalseにセットする
        Me.txtBranchQualityManager.Enabled = isEditenable
        'アクセス権が参照の場合は、社員検索アイコン(imgBranchQualityManagerSearch)のEnabledプロパティをFalseにセットする
        Me.imgBranchQualityManagerSearch.Enabled = isEditenable

        '② 部品質管理責任者テキストボックス(txtSectionQualityManager,imgSectionQualityManagerSearch)
        'PROJECT.SECTION_QUALITY_MANAGERの値をセットする
        Me.txtSectionQualityManager.Text = clsProject.txtSectionQualityManager
        'アクセス権が参照の場合は、txtSectionQualityManagerのEnabledプロパティをFalseにセットする
        Me.txtSectionQualityManager.Enabled = isEditenable
        'アクセス権が参照の場合は、社員検索アイコン(imgSectionQualityManagerSearch)のEnabledプロパティをFalseにセットする
        Me.imgSectionQualityManagerSearch.Enabled = isEditenable

        '③ グループ品質管理責任者テキストボックス(txtGroupQualityManager,imgGroupQualityManagerSearch)
        'PROJECT.GROUP_QUALITY_MANAGERの値をセットする
        Me.txtGroupQualityManager.Text = clsProject.txtGroupQualityManager
        'アクセス権が参照の場合は、txtGroupQualityManagerのEnabledプロパティをFalseにセットする
        Me.txtGroupQualityManager.Enabled = isEditenable
        'アクセス権が参照の場合は、社員検索アイコン(imgGroupQualityManagerSearch)のEnabledプロパティをFalseにセットする
        Me.imgGroupQualityManagerSearch.Enabled = isEditenable

        '④ プロジェクト品質管理責任者テキストボックス(txtProjectQualityManager,imgProjectQualityManagerSearch)
        'PROJECT.PROJECT_QUALITY_MANAGERの値をセットする
        Me.txtProjectQualityManager.Text = clsProject.txtProjectQualityManager
        'アクセス権が参照の場合は、txtProjectQualityManagerのEnabledプロパティをFalseにセットする
        Me.txtProjectQualityManager.Enabled = isEditenable
        'アクセス権が参照の場合は、社員検索アイコン(imgProjectQualityManagerSearch)のEnabledプロパティをFalseにセットする
        Me.imgProjectQualityManagerSearch.Enabled = isEditenable

        '⑤ リスク予防管理責任者テキストボックス(txtRiskPreventionManager)
        'PROJECT.RISK_PREVENTION_MANAGERの値をセットする
        Me.txtRiskPreventionManager.Text = clsProject.txtRiskPreventionManager
        'アクセス権が参照の場合は、txtRiskPreventionManagerのEnabledプロパティをFalseにセットする
        Me.txtRiskPreventionManager.Enabled = False

        '⑥ リスク予防管理対象 500万円以上チェックボックス(chkRpm500MilFlg)
        'PROJECT.RPM_500MIL_FLGの値が1の場合はチェックする
        If clsProject.chkRpm500MilFlg = "1" Then
            Me.chkRpm500MilFlg.Checked = True
        End If
        'アクセス権が参照の場合は、chkRpm500MilFlgのEnabledプロパティをFalseにセットする
        Me.chkRpm500MilFlg.Enabled = isEditenable

        '⑦ リスク予防管理対象 初品チェックボックス(chkRpmFirstProductFlg)
        'PROJECT.RPM_FIRST_PRODUCT_FLGの値が1の場合はチェックする
        If clsProject.chkRpmFirstProductFlg = "1" Then
            Me.chkRpmFirstProductFlg.Checked = True
        End If
        'アクセス権が参照の場合は、chkRpmFirstProductFlgのEnabledプロパティをFalseにセットする
        Me.chkRpmFirstProductFlg.Enabled = isEditenable

        '⑧ リスク予防管理対象 初品理由ドロップダウンリスト(ddlFirstProduct)
        'PROJECT.FIRST_PRODUCT_NOの値と等しい値を持つ選択肢を選択状態にする
        Me.ddlFirstProduct.SelectedValue = clsProject.ddlFirstProduct

        '⑨ リスク予防管理対象 初品理由テキストボックス(txtRpmFirstProductCause)
        'PROJECT.RPM_FIRST_PRODUCT_CAUSEの値をセットする
        Me.txtRpmFirstProductCause.Text = clsProject.txtRpmFirstProductCause


        '⑩ リスク予防管理対象 特殊チェックボックス(chkRpmSpecialProductFlg)
        'PROJECT.RPM_SPECIAL_PRODUCT_FLGの値が1の場合はチェックする
        If clsProject.chkRpmSpecialProductFlg = "1" Then
            Me.chkRpmSpecialProductFlg.Checked = True
        End If
        'アクセス権が参照の場合は、chkRpmSpecialProductFlgのEnabledプロパティをFalseにセットする
        Me.chkRpmSpecialProductFlg.Enabled = isEditenable

        '⑪ リスク予防管理対象 特殊理由ドロップダウンリスト(ddlSpecialProduct)
        'PROJECT.SPECIAL_PRODUCT_NOの値と等しい値を持つ選択肢を選択状態にする
        Me.ddlSpecialProduct.SelectedValue = clsProject.ddlSpecialProduct
        '⑫ リスク予防管理対象 特殊理由テキストボックス(txtRpmSpecialProductCause)
        'PROJECT.RPM_SPECIAL_PRODUCT_CAUSEの値をセットする
        Me.txtRpmSpecialProductCause.Text = clsProject.txtRpmSpecialProductCause


        'リスク予防管理対象のコントロール使用可設定
        SetRmpControlEnabled()

        If Not isEditenable Then
            'アクセス権が参照の場合は、ddlFirstProductのEnabledプロパティをFalseにセットする
            Me.ddlFirstProduct.Enabled = False

            'アクセス権が参照の場合は、txtRpmFirstProductCauseのEnabledプロパティをFalseにセットする
            Me.txtRpmFirstProductCause.Enabled = False

            'アクセス権が参照の場合は、ddlSpecialProductのEnabledプロパティをFalseにセットする
            Me.ddlSpecialProduct.Enabled = False

            'アクセス権が参照の場合は、txtRpmSpecialProductCauseのEnabledプロパティをFalseにセットする
            Me.txtRpmSpecialProductCause.Enabled = False
        End If

        '非表示コントロール設定
        Me.hdnProductSectId.Value = clsProject.hdnProductSectId
        Me.hdnProductSectCd.Value = clsProject.hdnProductSectCd

        Me.hdnProductUserId.Value = clsProject.hdnProductUserId
        Me.hdnProductUserCd.Value = clsProject.hdnProductUserCd

        '⑬ リスク予防管理区分ラジオボタン(rboRpmType)
        'PROJECT.RPM_TYPEの値と等しい値を持つ選択肢をチェックする
        If clsProject.rdoRpmType = "A" Then
            Me.rdoRpmType.SelectedIndex = 0
        Else
            Me.rdoRpmType.SelectedIndex = 1
        End If

        'アクセス権が参照の場合は、rdoRpmTypeのEnabledプロパティをFalseにセットする
        Me.rdoRpmType.Enabled = isEditenable

        '⑭ 添付ファイル(lnkProjectAttatch(N),btnProjectAttatchDel(N),btnProjectAttatchAdd)
        'II. 添付ファイルの情報を取得する
        Dim dt As DataTable

        dt = ProjectEntity.GetProjectAttatchInfo(strProjectNo)

        Dim column As DataColumn = New DataColumn("isEditAble")
        dt.Columns.Add(column)

        For Each row In dt.Rows
            row("isEditAble") = isEditenable.ToString
        Next

        grdProjectAttatch.DataSource = dt
        grdProjectAttatch.DataBind()
        Session(SESSION_PPROJECT_ATTATCH) = dt
        fleProjectAttatch.Enabled = isEditenable
        If (dt.Rows.Count >= 10) Then
            fleProjectAttatch.Visible = False
        Else
            fleProjectAttatch.Visible = True
        End If

        '⑮ 案件タイプラジオボタン(rdoProjectType)
        'PROJECT_TYPE_Mテーブルとバインドする
        'PROJECT.PROJECT_TYPE_NOの値と等しい値を持つ選択肢をチェックする
        If clsProject.rdoProjectType <> String.Empty Then
            Me.rdoProjectType.SelectedValue = clsProject.rdoProjectType
        End If

        'アクセス権が参照の場合は、rdoProjectTypeのEnabledプロパティをFalseにセットする
        Me.rdoProjectType.Enabled = isEditenable

        '4. リスク予防管理活動
        'リスク不安要素検討表
        '① 営業プロセス(原価) (lnk/btn Sp1RiskManagementList, lnk/btn Sp1RiskCheckList)
        'III-1.1. リスク管理表の情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 0, 0, clsProject)

        'III-1.2. チェックリストの情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 0, 1, clsProject)

        'リスク・不安要素検討表欄に、III-1で取得したファイル名すべてをリンク付きで表示する。また、それぞれの右隣りに更新ボタンを表示する
        If clsProject.fleSp1RiskManagementList <> String.Empty Then
            Me.lnkSp1RiskManagementList.Text = clsProject.fleSp1RiskManagementList
            Me.fleSp1RiskManagementList.Visible = False
            Me.btnSp1RiskManagementListDel.Visible = True

            Me.hdnSp1RiskManagementListIsUpdat.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnSp1RiskManagementListDel.Enabled = isEditenable
        Else
            'Me.lnkSp1RiskManagementList.Visible = False
            Me.fleSp1RiskManagementList.Visible = True
            Me.btnSp1RiskManagementListDel.Visible = False

            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.fleSp1RiskManagementList.Enabled = isEditenable
        End If

        If clsProject.fleSp1RiskCheckList <> String.Empty Then
            Me.lnkSp1RiskCheckList.Text = clsProject.fleSp1RiskCheckList
            Me.fleSp1RiskCheckList.Visible = False
            Me.btnSp1RiskCheckListDel.Visible = True

            Me.hdnSp1RiskCheckListIsUpdate.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnSp1RiskCheckListDel.Enabled = isEditenable
        Else
            'Me.lnkSp1RiskCheckList.Visible = False
            Me.fleSp1RiskCheckList.Visible = True
            Me.btnSp1RiskCheckListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.fleSp1RiskCheckList.Enabled = isEditenable
        End If

        '② 営業プロセス(見積) (lnk/btn Sp2RiskManagementList, lnk/btn Sp2RiskCheckList)
        'III-2.1. リスク管理表の情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 1, 0, clsProject)
        'III-2.2. チェックリストの情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 1, 1, clsProject)

        'リスク・不安要素検討表欄に、III-1で取得したファイル名すべてをリンク付きで表示する。また、それぞれの右隣りに更新ボタンを表示する
        If clsProject.fleSp2RiskManagementList <> String.Empty Then
            Me.lnkSp2RiskManagementList.Text = clsProject.fleSp2RiskManagementList
            Me.fleSp2RiskManagementList.Visible = False
            Me.btnSp2RiskManagementListDel.Visible = True

            Me.hdnSp2RiskManagementListIsUpdat.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnSp2RiskManagementListDel.Enabled = isEditenable
        Else
            'Me.lnkSp2RiskManagementList.Visible = False
            Me.fleSp2RiskManagementList.Visible = True
            Me.btnSp2RiskManagementListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.fleSp2RiskManagementList.Enabled = isEditenable
        End If

        If clsProject.fleSp2RiskCheckList <> String.Empty Then
            Me.lnkSp2RiskCheckList.Text = clsProject.fleSp2RiskCheckList
            Me.fleSp2RiskCheckList.Visible = False
            Me.btnSp2RiskCheckListDel.Visible = True

            Me.hdnSp2RiskCheckListIsUpdate.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnSp2RiskCheckListDel.Enabled = isEditenable
        Else
            ' Me.lnkSp2RiskCheckList.Visible = False
            Me.fleSp2RiskCheckList.Visible = True
            Me.btnSp2RiskCheckListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.fleSp2RiskCheckList.Enabled = isEditenable
        End If

        '③ 購買プロセス (lnk/btn Sp3RiskManagementList, lnk/btn Sp3RiskCheckList)
        'III-3.1. リスク管理表の情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 2, 0, clsProject)
        'III-3.2. チェックリストの情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 2, 1, clsProject)

        'リスク・不安要素検討表欄に、III-1で取得したファイル名すべてをリンク付きで表示する。また、それぞれの右隣りに更新ボタンを表示する
        If clsProject.flePpRiskManagementList <> String.Empty Then
            Me.lnkPpRiskManagementList.Text = clsProject.flePpRiskManagementList
            Me.flePpRiskManagementList.Visible = False
            Me.btnPpRiskManagementListDel.Visible = True

            Me.hdnPpRiskManagementListIsUpdat.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnPpRiskManagementListDel.Enabled = isEditenable
        Else
            ' Me.lnkPpRiskManagementList.Visible = False
            Me.flePpRiskManagementList.Visible = True
            Me.btnPpRiskManagementListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.flePpRiskManagementList.Enabled = isEditenable
        End If

        If clsProject.flePpRiskCheckList <> String.Empty Then
            Me.lnkPpRiskCheckList.Text = clsProject.flePpRiskCheckList
            Me.flePpRiskCheckList.Visible = False
            Me.btnPpRiskCheckListDel.Visible = True

            Me.hdnPpRiskCheckListIsUpdate.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnPpRiskCheckListDel.Enabled = isEditenable
        Else
            ' Me.lnkPpRiskCheckList.Visible = False
            Me.flePpRiskCheckList.Visible = True
            Me.btnPpRiskCheckListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.flePpRiskCheckList.Enabled = isEditenable
        End If

        '④ 設計・開発プロセス (lnk/btn Sp4RiskManagementList, lnk/btn Sp4RiskCheckList)
        'III-4.1. リスク管理表の情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 3, 0, clsProject)
        'III-4.2. チェックリストの情報を取得するため以下のSQLを発行する
        ProjectEntity.GetRiskManagementListInfo(strProjectNo, 3, 1, clsProject)

        'リスク・不安要素検討表欄に、III-1で取得したファイル名すべてをリンク付きで表示する。また、それぞれの右隣りに更新ボタンを表示する
        If clsProject.fleDpRiskManagementList <> String.Empty Then
            Me.lnkDpRiskManagementList.Text = clsProject.fleDpRiskManagementList
            Me.fleDpRiskManagementList.Visible = False
            Me.btnDpRiskManagementListDel.Visible = True

            Me.hdnDpRiskManagementListIsUpdat.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnDpRiskManagementListDel.Enabled = isEditenable
        Else
            ' Me.lnkDpRiskManagementList.Visible = False
            Me.fleDpRiskManagementList.Visible = True
            Me.btnDpRiskManagementListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.fleDpRiskManagementList.Enabled = isEditenable
        End If

        If clsProject.fleDpRiskCheckList <> String.Empty Then
            Me.lnkDpRiskCheckList.Text = clsProject.fleDpRiskCheckList
            Me.fleDpRiskCheckList.Visible = False
            Me.btnDpRiskCheckListDel.Visible = True

            Me.hdnDpRiskCheckListIsUpdate.Value = "1"
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.btnDpRiskCheckListDel.Enabled = isEditenable
        Else
            ' Me.lnkDpRiskCheckList.Visible = False
            Me.fleDpRiskCheckList.Visible = True
            Me.btnDpRiskCheckListDel.Visible = False
            'アクセス権が参照の場合は、btnSp1Risk Management/Check ListのEnabledプロパティをFalseにセットする
            Me.fleDpRiskCheckList.Enabled = isEditenable
        End If

        '不要・完了
        '① 営業プロセス(原価) (chkSp1 NoNeed/Complete Flg)
        'Ⅳ-1.1. 不要情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 0, 0, clsProject)

        'FLAGの値が1の場合はchkSp1NoNeedFlgをチェックし、       
        '3. リスク予防管理活動の営業プロセス(原価)行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
        If clsProject.chkSp1NoNeedFlg = "1" Then
            Me.chkSp1NoNeedFlg.Checked = True
        End If
        '
        'Ⅳ-1.2. 完了情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 0, 1, clsProject)

        'FLAGの値が1の場合はchkSp1CompleteFlgをチェックする
        If clsProject.chkSp1CompleteFlg = "1" Then
            Me.chkSp1CompleteFlg.Checked = True
        End If

        'アクセス権が参照の場合は、chkSp1 NoNeed/Complete FlgのEnabledプロパティをFalseにセットする
        Me.chkSp1NoNeedFlg.Enabled = isEditenable
        Me.chkSp1CompleteFlg.Enabled = isEditenable

        '② 営業プロセス(見積) (chkSp2 NoNeed/Complete Flg)
        'Ⅳ-2.1. 不要情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 1, 0, clsProject)

        'FLAGの値が1の場合はchkSp2NoNeedFlgをチェックし、
        '3. リスク予防管理活動の営業プロセス(見積)行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
        If clsProject.chkSp2NoNeedFlg = "1" Then
            Me.chkSp2NoNeedFlg.Checked = True
        End If

        'Ⅳ-2.2. 完了情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 1, 1, clsProject)

        'FLAGの値が1の場合はchkSp2CompleteFlgをチェックする
        If clsProject.chkSp2CompleteFlg = "1" Then
            Me.chkSp2CompleteFlg.Checked = True
        End If
        'アクセス権が参照の場合は、chkSp2 NoNeed/Complete FlgのEnabledプロパティをFalseにセットする
        Me.chkSp2NoNeedFlg.Enabled = isEditenable
        Me.chkSp2CompleteFlg.Enabled = isEditenable

        '③ 購買プロセス (chkSp3 NoNeed/Complete Flg)
        'Ⅳ-3.1. 不要情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 2, 0, clsProject)

        'FLAGの値が1の場合はchkSp3NoNeedFlgをチェックし、
        '3. リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
        If clsProject.chkPpNoNeedFlg = "1" Then
            Me.chkPpNoNeedFlg.Checked = True
        End If
        'Ⅳ-3.2. 完了情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 2, 1, clsProject)

        'FLAGの値が1の場合はchkSp3CompleteFlgをチェックする
        If clsProject.chkPpCompleteFlg = "1" Then
            Me.chkPpCompleteFlg.Checked = True
        End If
        'アクセス権が参照の場合は、chkSp3 NoNeed/Complete FlgのEnabledプロパティをFalseにセットする
        Me.chkPpNoNeedFlg.Enabled = isEditenable
        Me.chkPpCompleteFlg.Enabled = isEditenable

        '④ 設計・開発プロセス (chkSp4 NoNeed/Complete Flg)
        'Ⅳ-4.1. 不要情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 3, 0, clsProject)

        'FLAGの値が1の場合はchkSp4NoNeedFlgをチェックし、
        '3. リスク予防管理活動の設計・開発プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
        If clsProject.chkDpNoNeedFlg = "1" Then
            Me.chkDpNoNeedFlg.Checked = True
        End If
        'Ⅳ-4.2. 完了情報を取得するため以下のSQLを発行する
        ProjectEntity.GetNoNeedCompleteInfo(strProjectNo, 3, 1, clsProject)

        'FLAGの値が1の場合はchkSp4CompleteFlgをチェックする
        If clsProject.chkDpCompleteFlg = "1" Then
            Me.chkDpCompleteFlg.Checked = True
        End If
        'アクセス権が参照の場合は、chkSp4 NoNeed/Complete FlgのEnabledプロパティをFalseにセットする
        Me.chkDpNoNeedFlg.Enabled = isEditenable
        Me.chkDpCompleteFlg.Enabled = isEditenable

        'リスク不安要素検討表テーブルのカラー設定
        SetRiskManagementRowColor()

        'リスク予防・管理検討会実施履歴
        '① リスク予防・管理検討会議事録リスト(lblOpenRound,lblOpenDate,lnkRiskPrevention)
        'リスク予防・管理検討会議事録欄に、取得したすべてのレコードをリンク付きで表示する
        Dim dtRiskPre As New DataTable
        dtRiskPre = ProjectEntity.GetRiskPreventionList(strProjectNo)


        If dtRiskPre.Rows.Count = 0 Then
            Me.gdvRiskPreventionList.DataSource = GetRiskPreventionTableNull()
            Me.gdvRiskPreventionList.DataBind()
        ElseIf dtRiskPre.Rows.Count < 8 Then

            '空白行
            For i = dtRiskPre.Rows.Count + 1 To 8
                Dim drNew As DataRow
                drNew = dtRiskPre.NewRow
                dtRiskPre.Rows.Add(drNew)
            Next
            Me.gdvRiskPreventionList.DataSource = dtRiskPre
            Me.gdvRiskPreventionList.DataBind()
        Else
            Me.gdvRiskPreventionList.DataSource = dtRiskPre
            Me.gdvRiskPreventionList.DataBind()
        End If

        'フッタ
        '① 案件登録ボタン(btnPjInput)
        'アクセス権が参照の場合は、btnPjInputのEnabledプロパティをFalseにセットする
        Me.btnPjInput.Enabled = isEditenable
        '② 案件削除ボタン(btnPjDel)
        'アクセス権が参照の場合は、btnPjDelのEnabledプロパティをFalseにセットする
        Me.btnPjDel.Enabled = isEditenable
        '③ リスク予防検討会登録ボタン(btnRiskPreventionInput)
        'アクセス権が参照の場合は、btnRiskPreventionInputのEnabledプロパティをFalseにセットする
        Me.btnRiskPreventionInput.Enabled = isEditenable

        '④ 最終更新日時隠し項目(hdnModifiedOn)
        'PROJECT.MODIFIED_ONの値をセットする
        Me.hdnModifiedOn.Value = clsProject.hdnModifiedOn

    End Sub

    ''' <summary>
    ''' (2.1) オーダテキストボックス変更イベント(txtOrderCd_TextChanged)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub txtOrderCd_TextChanged(sender As Object, e As EventArgs) Handles txtOrderCd.TextChanged

        If Me.txtOrderCd.Text = String.Empty Then
            '※オーダテキストボックスの値が削除された場合は以下の項目の値を削除し、行を非表示にする。初期表示行のを再表示する	
            '値の削除を行う項目			
            '① 工事名称(txtOrderNm)
            Me.txtOrderNm.Text = String.Empty
            '② 顧客(txtCompyNm)	
            Me.txtCompyNm.Text = String.Empty
            '③ 受注金額(txtJyuchuCrr)	
            Me.txtJyuchuCrr.Text = String.Empty
            '④ 納期日(txtNokiYmd)		
            Me.txtNokiYmd.Text = String.Empty
            '⑤ 受注部門(txtJyuchuSectNm)		
            Me.txtJyuchuSectNm.Text = String.Empty
            '⑥ 受注担当者(txtJyuchuUserNm)			
            Me.txtJyuchuUserNm.Text = String.Empty
            '非表示にする項目			
            '① 工事名称行(txtOrderNm)	
            Me.trOrderNm.Visible = False
            '② 顧客行(txtCompyNm)		
            Me.trCompyNm.Visible = False
            '③ 受注金額、納期日行(txtJyuchuCrr,txtNokiYmd)	
            Me.trJyuchuCrrNokiYmd.Visible = False
            '④ 受注部門、受注担当者行(txtJyuchuSectNm,txtJyuchuUserNm)			
            Me.trJyuchuSectNmJyuchuUserNm.Visible = False

            '初期表示の項目を再表示する			
            '① 工事名称（仮）行(txtPjNameTemp)			
            Me.trPjNameTemp.Visible = True
            '② 顧客行(txtCustomerName)
            Me.trCustomerName.Visible = True

            Return
        End If

        '初期表示の項目を非表示にする			
        '① 工事名称（仮）行(txtPjNameTemp)		
        Me.trPjNameTemp.Visible = False
        '② 顧客行(txtCustomerName)		
        Me.trCustomerName.Visible = False

        '初期非表示の項目を表示する			
        '① 工事名称行(txtOrderNm)		
        Me.trOrderNm.Visible = True
        '② 顧客行(txtCompyNm)		
        Me.trCompyNm.Visible = True
        '③ 受注金額、納期日行(txtJyuchuCrr,txtNokiYmd)
        Me.trJyuchuCrrNokiYmd.Visible = True
        '④ 受注部門、受注担当者行(txtJyuchuSectNm,txtJyuchuUserNm)			
        Me.trJyuchuSectNmJyuchuUserNm.Visible = True

        'オーダ検索画面で選択したオーダ行に埋め込んだ値を以下の項目にセットする			
        '① 工事名称(txtOrderNm)：オーダ検索画面のhidOrderNm	
        Me.txtOrderNm.Text = Me.hdnOrderNm.Value

        '② 顧客(txtCompyNm)：オーダ検索画面のhidCompyNm
        Me.txtCompyNm.Text = Me.hdnCompyNm.Value

        '③ 受注金額(txtJyuchuCrr)：オーダ検索画面のhidJyuchuCrr		
        If Me.hdnJyuchuCrr.Value <> String.Empty Then
            Me.txtJyuchuCrr.Text = String.Format("{0:C}", Int32.Parse(Me.hdnJyuchuCrr.Value))

        End If

        '④ 納期日(txtNokiYmd)：オーダ検索画面のhidNokiYmd			
        If Me.hdnNokiYmd.Value <> String.Empty Then
            Me.txtNokiYmd.Text = Format(Date.Parse(Format(CInt(Me.hdnNokiYmd.Value), "0000/00/00")), "yyyy/MM/dd")
        End If
        '⑤ 受注部門(txtJyuchuSectNm)：オーダ検索画面のhidJyuchuSectNm			
        Me.txtJyuchuSectNm.Text = Me.hdnJyuchuSectNm.Value

        '⑥ 受注担当者(txtJyuchuUserNm)：オーダ検索画面のhidJyuchuUserNm
        Me.txtJyuchuUserNm.Text = Me.hdnJyuchuUserNm.Value


    End Sub

    ''' <summary>
    ''' (5.1) 製造部門担当者テキストボックス変更イベント(txtProductUser_TextChanged)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub txtProductUser_TextChanged(sender As Object, e As EventArgs) Handles txtProductUser.TextChanged
        '所属コード(SECT_CD)
        Dim strSectCd As String

        '製造部門担当者所属コード
        strSectCd = Me.hdnProductUserSectCd.Value

        '製造部門担当者テキストボックスがNull・ブランクの場合は何もしない
        If Me.txtProductUser.Text = String.Empty Then
            Return
        End If

        '※ユーザ検索画面でユーザが選択された場合は製造部門担当者にもとづく各管理責任者をセットする
        '① 支社品質管理責任者テキストボックス(txtBranchQualityManager)
        '製造部門担当者(txtProductUser)の所属コード(SECT_CD)上2桁を取得する
        Dim strSectCdTemp As String
        strSectCdTemp = strSectCd.Substring(0, 2)

        ' 支社品質管理責任者取得
        Dim userManager As UserManagerClass = New UserManagerClass
        userManager = ProjectEntity.GetBranchQualityManagerInfo(strSectCdTemp)

        '支社品質管理責任者テキストボックスにSQL結果をセットする
        If userManager.AllsectNm <> String.Empty Or userManager.Fullname <> String.Empty Then
            Me.txtBranchQualityManager.Text = userManager.AllsectNm & " " & userManager.PostclsNm & " " & userManager.Fullname
        End If

        '② 部品質管理責任者テキストボックス(txtSectionQualityManager)
        '製造部門担当者(txtProductUser)の所属コード(SECT_CD)上4桁を取得する
        strSectCdTemp = strSectCd.Substring(0, 4)

        '部品質管理責任者取得
        userManager = ProjectEntity.GetSectionQualityManagerInfo(strSectCdTemp)

        '部品質管理責任者テキストボックスにSQL結果をセットする
        If userManager.AllsectNm <> String.Empty Or userManager.Fullname <> String.Empty Then
            Me.txtSectionQualityManager.Text = userManager.AllsectNm & " " & userManager.PostclsNm & " " & userManager.Fullname
        End If

        '③ グループ品質管理責任者テキストボックス(txtGroupQualityManager)
        '製造部門担当者(txtProductUser)の所属コード(SECT_CD)上6桁を取得する
        strSectCdTemp = strSectCd.Substring(0, 6)
        'グループ品質管理責任者取得
        userManager = ProjectEntity.GetGroupQualityManagerInfo(strSectCdTemp)
        'グループ品質管理責任者テキストボックスにSQL結果をセットする
        If userManager.AllsectNm <> String.Empty Or userManager.Fullname <> String.Empty Then
            Me.txtGroupQualityManager.Text = userManager.AllsectNm & " グループ長 " & userManager.Fullname
        End If
    End Sub

    ''' <summary>
    ''' 支社管理取引の有無ラジオボタン変更イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rdoBranchTransactionFlg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoBranchTransactionFlg.SelectedIndexChanged
        If Me.rdoBranchTransactionFlg.SelectedItem.Text = "有：自支社が支援支社" Then
            '「有：自支社が支援支社」を選択した場合は、支援支社ラベル(lblSupportBranch)のTextを「窓口支社」に変更する
            Me.lblSupportBranch.Text = "窓口支社"
        Else
            '「有：自支社が支援支社」以外を選択した場合は、支援支社ラベル(lblSupportBranch)のTextを「支援支社」に変更する
            Me.lblSupportBranch.Text = "支援支社"
        End If

        '「無」を選択した場合は
        If Me.rdoBranchTransactionFlg.SelectedItem.Text = "無" Then
            '支援支社ドロップダウンリスト(ddlBranchList)を入力不可およびその値をブランクとする
            Me.ddlBranchList.SelectedIndex = 0
            Me.ddlBranchList.Enabled = False
        Else
            Me.ddlBranchList.Enabled = True
        End If
    End Sub

    '(9) リスク予防管理区分選択イベント(rdoRpmType_CheckedChanged)
    Protected Sub rdoRpmType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoRpmType.SelectedIndexChanged
        ' Aを選択した場合
        If rdoRpmType.SelectedValue = "0" Then
            '部品質管理責任者テキストボックス(txtSectionQualityManager)の値をリスク予防管理責任者テキストボックス(txtRiskPreventionManager)にセットする
            Me.txtRiskPreventionManager.Text = Me.txtSectionQualityManager.Text
        Else
            'Bを選択した場合
            'グループ品質管理責任者テキストボックス(txtGroupQualityManager)の値をリスク予防管理責任者テキストボックス(txtRiskPreventionManager)にセットする
            Me.txtRiskPreventionManager.Text = Me.txtGroupQualityManager.Text
        End If

    End Sub

    ''' <summary>
    ''' (11) 添付ファイル 追加
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub AddProjectAttatchControl()
    '    '添付ファイル参照ブタンIndex
    '    Dim controlIndex As Integer

    '    controlIndex = GetProjectAttatchIndex()
    '    '添付ファイル ファイル参照ボタン(fleProjectAttatch(N))を下に追加表示する
    '    Select Case controlIndex + 1
    '        Case 1
    '            Me.fleProjectAttatch1.Visible = True
    '        Case 2
    '            Me.fleProjectAttatch2.Visible = True
    '        Case 3
    '            Me.fleProjectAttatch3.Visible = True
    '        Case 4
    '            Me.fleProjectAttatch4.Visible = True
    '        Case 5
    '            Me.fleProjectAttatch5.Visible = True
    '        Case 6
    '            Me.fleProjectAttatch6.Visible = True
    '        Case 7
    '            Me.fleProjectAttatch7.Visible = True
    '        Case 8
    '            Me.fleProjectAttatch8.Visible = True
    '        Case 9
    '            Me.fleProjectAttatch9.Visible = True
    '        Case 10
    '            Me.fleProjectAttatch10.Visible = True
    '    End Select

    'End Sub

    ''' <summary>
    ''' (11) 添付ファイル使用可制御
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub SetProjectAttatchControlEnable(ByVal isEnabled As Boolean)
    '    '添付ファイル参照ブタンIndex
    '    Dim controlIndex As Integer

    '    controlIndex = GetProjectAttatchIndex()
    '    '添付ファイル ファイル参照ボタン(fleProjectAttatch(N))
    '    Select Case controlIndex + 1
    '        Case 1
    '            Me.fleProjectAttatch1.Enabled = isEnabled
    '        Case 2
    '            Me.fleProjectAttatch2.Enabled = isEnabled
    '        Case 3
    '            Me.fleProjectAttatch3.Enabled = isEnabled
    '        Case 4
    '            Me.fleProjectAttatch4.Enabled = isEnabled
    '        Case 5
    '            Me.fleProjectAttatch5.Enabled = isEnabled
    '        Case 6
    '            Me.fleProjectAttatch6.Enabled = isEnabled
    '        Case 7
    '            Me.fleProjectAttatch7.Enabled = isEnabled
    '        Case 8
    '            Me.fleProjectAttatch8.Enabled = isEnabled
    '        Case 9
    '            Me.fleProjectAttatch9.Enabled = isEnabled
    '        Case 10
    '            Me.fleProjectAttatch10.Enabled = isEnabled
    '    End Select

    'End Sub
    ''' <summary>
    ''' (12) 案件タイプ選択イベント(rdoProjectType_CheckedChanged)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rdoProjectType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoProjectType.SelectedIndexChanged

        Select Case Me.rdoProjectType.SelectedValue
            Case 0
                '① 「システム開発・改修」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)を未チェックとする
                Me.chkSp1NoNeedFlg.Checked = False
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)を未チェックとする
                Me.chkSp2NoNeedFlg.Checked = False
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)を未チェックとする
                Me.chkPpNoNeedFlg.Checked = False
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)を未チェックとする
                Me.chkDpNoNeedFlg.Checked = False
            Case 1
                '② 「システム保守・運用 新規」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)を未チェックとする
                Me.chkSp1NoNeedFlg.Checked = False
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)を未チェックとする
                Me.chkSp2NoNeedFlg.Checked = False
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)を未チェックとする
                Me.chkPpNoNeedFlg.Checked = False
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)をチェックする
                Me.chkDpNoNeedFlg.Checked = True
            Case 2
                '③ 「システム保守・運用 継続」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)をチェックする
                Me.chkSp1NoNeedFlg.Checked = True
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)をチェックする
                Me.chkSp2NoNeedFlg.Checked = True
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)をチェックする
                Me.chkPpNoNeedFlg.Checked = True
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)をチェックする
                Me.chkDpNoNeedFlg.Checked = True
            Case 3
                '④ 「インフラ構築（含む運用構築）」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)を未チェックとする
                Me.chkSp1NoNeedFlg.Checked = False
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)を未チェックとする
                Me.chkSp2NoNeedFlg.Checked = False
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)を未チェックとする
                Me.chkPpNoNeedFlg.Checked = False
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)を未チェックとする
                Me.chkDpNoNeedFlg.Checked = False
            Case 4
                '⑤ 「インフラ保守 新規」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)を未チェックとする
                Me.chkSp1NoNeedFlg.Checked = False
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)を未チェックとする
                Me.chkSp2NoNeedFlg.Checked = False
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)を未チェックとする
                Me.chkPpNoNeedFlg.Checked = False
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)をチェックする
                Me.chkDpNoNeedFlg.Checked = True
            Case 5
                '⑥ 「インフラ保守 継続」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)をチェックする
                Me.chkSp1NoNeedFlg.Checked = True
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)をチェックする
                Me.chkSp2NoNeedFlg.Checked = True
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)をチェックする
                Me.chkPpNoNeedFlg.Checked = True
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)をチェックする
                Me.chkDpNoNeedFlg.Checked = True
            Case 6
                '⑦ 「機器販売」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)をチェックする
                Me.chkSp1NoNeedFlg.Checked = True
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)をチェックする
                Me.chkSp2NoNeedFlg.Checked = True
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)をチェックする
                Me.chkPpNoNeedFlg.Checked = True
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)をチェックする
                Me.chkDpNoNeedFlg.Checked = True
            Case 7
                '⑧ 「ソリューション案件」が選択された場合
                '営業プロセス(原価)不要チェックボックス(chkSp1NoNeedFlg)を未チェックとする
                Me.chkSp1NoNeedFlg.Checked = False
                '営業プロセス(見積)不要チェックボックス(chkSp2NoNeedFlg)を未チェックとする
                Me.chkSp2NoNeedFlg.Checked = False
                '購買プロセス不要チェックボックス(chkSp3NoNeedFlg)を未チェックとする
                Me.chkPpNoNeedFlg.Checked = False
                '設計・開発プロセス不要チェックボックス(chkSp4NoNeedFlg)を未チェックとする
                Me.chkDpNoNeedFlg.Checked = False
        End Select

        'リスク不安要素検討表テーブルのカラー設定
        SetRiskManagementRowColor()

    End Sub

    ''' <summary>
    ''' (25) 案件登録ボタンクリックイベント(btnPjInput_Click)　
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnPjInput_Click(sender As Object, e As EventArgs) Handles btnPjInput.Click

        'エラーチェック
        If Not CheckInputValue() Then
            Return
        End If

        '案件情報設定
        SetProjectValue()

        ' 新規追加したファイルの取得
        'Dim fileNames As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD_NAME)
        'Dim files As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD)


        '※案件のINSERTかUPDATEかの判定は、案件番号テキストボックス(txtPjNo)に値がセットされているか(->UPDATE)、いないか(->INSERT)で判定する
        If Me.txtPjNo.Text.Trim = String.Empty Then
            '(25) 新規登録の場合

            '案件番号テキストボックス(txtPjNo)に採番した案件番号をセットする
            Me.txtPjNo.Text = ProjectEntity.InsertProjectInfo(clsProject, _
                                                              Session(SessionComm.SESSION_USER_SECT_CD).ToString, _
                                                              Session(SessionComm.SESSION_USER_ID).ToString(), _
                                                              Session(SessionComm.SESSION_USER_ID).ToString, _
                                                              Session(SessionComm.SESSION_USER_NAME).ToString)

            '登録処理でエラーが発生した場合
            If Me.txtPjNo.Text.Trim = String.Empty Then
                Dim msgErr As String
                msgErr = MessageComm.GetMessageContext("2", "010", Nothing)

                Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                        "",
                                                        "showMessage('" & msgErr & "');",
                                                        True)
                Return
            End If

            'リスク予防検討会登録ボタン(btnRiskPreventionInput)のEnabledプロパティをTrueに設定する
            Me.btnRiskPreventionInput.Enabled = True

            '画面上にメッセージ「案件の登録が完了しました。案件番号：xxxxxxxxxx」を表示する。
            'xxxxxxxxxxに案件番号テキストボックス(txtPjNo)の値をセットする
            Dim msg As String
            Dim tList As List(Of String) = New List(Of String)
            tList.Add(Me.txtPjNo.Text.Trim)

            msg = MessageComm.GetMessageContext("2", "007", tList)
            Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                    "",
                                                    "showMessage('" & msg & "');",
                                                    True)
        Else
            '(46) 修正登録(UPDATE)の場合

            '当該案件が別のユーザによって削除されていないか確認する
            Dim iniCount As Integer = 0
            iniCount = ProjectEntity.GetProjectCount(Me.txtPjNo.Text.Trim)

            'PROJECT_CNTの値が0の場合
            If iniCount = 0 Then
                Dim msgErr As String
                msgErr = MessageComm.GetMessageContext("2", "008", Nothing)

                Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                        "",
                                                        "updateError('" & msgErr & "');",
                                                        True)

                'Menu.masterのContentPlaceHolder内にProjectList.aspxを表示する
                Return
            End If

            ' 当該案件が別のユーザによって更新されていないか確認する
            Dim strUpdate As String = String.Empty
            'DB内の案件の最終更新日時を取得する
            strUpdate = ProjectEntity.GetProjectUpdateDate(Me.txtPjNo.Text.Trim)

            'SQLで取得したMODIFIED_ONの値と最終更新日時隠し項目(hdnModifiedOn)の値を比較する
            If Not hdnIfOverwrite.Value = "1" And strUpdate <> Me.hdnModifiedOn.Value Then
                '比較した値が異なる場合は以下の確認メッセージを表示する()
                '「当該案件の情報はすでに他のユーザによって更新されています。このまま登録処理を続けますか？」
                Dim msgErr As String
                msgErr = MessageComm.GetMessageContext("2", "009", Nothing)

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "overWriteConfirm('" & msgErr & "');", True)
                Return
            End If

            'UPDATE処理
            Dim bolRun As Boolean
            bolRun = ProjectEntity.UpdateProjectInfo(clsProject, _
                                                    Session(SessionComm.SESSION_USER_SECT_CD).ToString, _
                                                    Session(SessionComm.SESSION_USER_CD).ToString(), _
                                                    Session(SessionComm.SESSION_USER_ID).ToString, _
                                                    Session(SessionComm.SESSION_USER_NAME).ToString, _
                                                    Session(SESSION_PPROJECT_ATTATCH_DELETE))
            'UPDATE処理でエラーが発生した場合
            If Not bolRun Then
                Dim msgErr As String
                msgErr = MessageComm.GetMessageContext("2", "010", Nothing)

                Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                        "",
                                                        "showMessage('" & msgErr & "');",
                                                        True)
                Return
            End If

            '画面上にメッセージ「案件の登録が完了しました。案件番号：xxxxxxxxxx」を表示する。
            'xxxxxxxxxxに案件番号テキストボックス(txtPjNo)の値をセットする
            Dim msg As String
            Dim tList As List(Of String) = New List(Of String)
            tList.Add(Me.txtPjNo.Text.Trim)

            msg = MessageComm.GetMessageContext("2", "007", tList)

            Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                    "",
                                                    "showMessage('" & msg & "');",
                                                    True)
        End If

        'ページロードイベント(Page_Load)　
        Session("PjNo") = Me.txtPjNo.Text.Trim
        GetProjectInfo()

    End Sub

    ''' <summary>
    ''' (47) 案件削除ボタンクリックイベント(btnPjDel_Click)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnPjDel_Click(sender As Object, e As EventArgs) Handles btnPjDel.Click

        'OKが選択された場合は、以下のDELETE SQL文を発行する
        Dim strPjNo As String
        Dim strUserCd As String
        strPjNo = Me.txtPjNo.Text.Trim
        strUserCd = Session(SessionComm.SESSION_USER_ID)
        Dim bolRun As Boolean
        ' 案件番号テキストボックス(txtPjNo)の値がNULL・ブランクの場合
        If strPjNo = String.Empty Then
            'ファイルSessionをクリア
            Session("projectAttatchFile1") = Nothing
            Session("projectAttatchFile2") = Nothing
            Session("projectAttatchFile3") = Nothing
            Session("projectAttatchFile4") = Nothing
            Session("projectAttatchFile5") = Nothing
            Session("projectAttatchFile6") = Nothing
            Session("projectAttatchFile7") = Nothing
            Session("projectAttatchFile8") = Nothing
            Session("projectAttatchFile9") = Nothing
            Session("projectAttatchFile10") = Nothing
            Session("sp1RiskManagementListFile") = Nothing
            Session("sp1RiskCheckFile") = Nothing
            Session("sp2RiskManagementListFile") = Nothing
            Session("sp2RiskCheckListFile") = Nothing
            Session("ppRiskManagementListFile") = Nothing
            Session("ppRiskCheckListFile") = Nothing
            Session("dpRiskManagementListFile") = Nothing
            Session("dpRiskCheckListFile") = Nothing

            'OKが選択された場合は、"(1) ページロードイベント(Page_Load)　※新規登録（メニュー画面の案件登録ボタンから表示）の場合"を実行する
            InitialPage()
            Return
        End If

        bolRun = ProjectEntity.DeleteProjectInfo(strPjNo, strUserCd)
        If bolRun Then
            '画面上にメッセージ「案件の削除が完了しました。案件番号：xxxxxxxxxx」を表示する。
            'xxxxxxxxxxに案件番号テキストボックス(txtPjNo)の値をセットする
            Dim msg As String
            Dim tList As List(Of String) = New List(Of String)
            tList.Add(Me.txtPjNo.Text.Trim)

            msg = MessageComm.GetMessageContext("2", "005", tList)

            Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                    "",
                                                    "showMessage('" & msg & "');",
                                                    True)
            'Menu.masterのContentPlaceHolder内にProjectList.aspxを表示する
            Response.Write("<script language=javascript>window.location.href='ProjectList.aspx'</script>")
        Else
            '画面上にメッセージ「案件の削除でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            Dim msgErr As String
            msgErr = MessageComm.GetMessageContext("2", "011", Nothing)

            Page.ClientScript.RegisterStartupScript(Me.GetType(),
                                                    "",
                                                    "showMessage('" & msgErr & "');",
                                                    True)
        End If

    End Sub

    ''' <summary>
    ''' (48) リスク予防検討会登録ボタンクリックイベント(btnRiskPreventionInput_Click)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRiskPreventionInput_Click(sender As Object, e As EventArgs) Handles btnRiskPreventionInput.Click

        '画面上にメッセージ「リスク予防・管理検討会新規登録画面に移動します。よろしいですか？」を表示する
        'OKが選択された場合は、以下の処理を実行する
        'Session変数RpPjNoに案件番号テキストボックス(txtPjNo)の値をセットする
        Session(SessionComm.SESSION_RP_PJ_NO) = Me.txtPjNo.Text
        'Session変数RpProcessNoにプロセスラジオボタン(rdoProcess)の値をセットする
        Session(SessionComm.SESSION_RP_PROCESS_NO) = Me.rdoProcess.SelectedValue
        'Session変数RpOrderCdにオーダテキストボックス(txtOrderCd)の値をセットする
        Session(SessionComm.SESSION_ORDER_CD) = Me.txtOrderCd.Text
        'Session変数RpProductSectNmに製造部門テキストボックス(txtProductSectNm)の値をセットする
        Session(SessionComm.SESSION_RP_PRODUCT_SECT_NM) = Me.txtProductSectNm.Text
        'Session変数RpCustomerTypeに顧客区分ラジオボタン(rdoCustomerType)の選択値Textの値をセットする
        Session(SessionComm.SESSION_RP_CUSTOMER_TYPE) = Me.rdoCustomerType.SelectedItem.Text

        'Menu.masterのContentPlaceHolder内にRiskPrevention.aspxを表示する
        Response.Write("<script language=javascript>window.location.href='RiskPrevention.aspx'</script>")
    End Sub

    ''' <summary>
    ''' (49) キャンセルボタンクリックイベント(btnPjCancel_Click)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnPjCancel_Click(sender As Object, e As EventArgs) Handles btnPjCancel.Click
        '画面上にメッセージ「案件検索画面に移動します。よろしいですか？」を表示する	
        'キャンセルが選択された場合は処理を中断する()
        'OKが選択された場合は、Menu.masterのContentPlaceHolder内にProjectList.aspxを表示する	
        Response.Write("<script language=javascript>window.location.href='ProjectList.aspx'</script>")
    End Sub

    ' ''' <summary>
    ' ''' 添付ファイルインデックス取得
    ' ''' </summary>
    ' ''' <returns>添付ファイルインデックス</returns>
    ' ''' <remarks></remarks>
    'Private Function GetProjectAttatchIndex() As Integer
    '    Dim buttonIndex As Integer = 0

    '    If Me.lnkProjectAttatch1.Text <> String.Empty And Me.lnkProjectAttatch1.Visible Then
    '        buttonIndex = 1
    '    End If
    '    If Me.lnkProjectAttatch2.Text <> String.Empty And Me.lnkProjectAttatch2.Visible Then
    '        buttonIndex = 2
    '    End If
    '    If Me.lnkProjectAttatch3.Text <> String.Empty And Me.lnkProjectAttatch3.Visible Then
    '        buttonIndex = 3
    '    End If
    '    If Me.lnkProjectAttatch4.Text <> String.Empty And Me.lnkProjectAttatch4.Visible Then
    '        buttonIndex = 4
    '    End If
    '    If Me.lnkProjectAttatch5.Text <> String.Empty And Me.lnkProjectAttatch5.Visible Then
    '        buttonIndex = 5
    '    End If
    '    If Me.lnkProjectAttatch6.Text <> String.Empty And Me.lnkProjectAttatch6.Visible Then
    '        buttonIndex = 6
    '    End If
    '    If Me.lnkProjectAttatch7.Text <> String.Empty And Me.lnkProjectAttatch7.Visible Then
    '        buttonIndex = 7
    '    End If
    '    If Me.lnkProjectAttatch8.Text <> String.Empty And Me.lnkProjectAttatch8.Visible Then
    '        buttonIndex = 8
    '    End If
    '    If Me.lnkProjectAttatch9.Text <> String.Empty And Me.lnkProjectAttatch9.Visible Then
    '        buttonIndex = 9
    '    End If
    '    If Me.lnkProjectAttatch10.Text <> String.Empty And Me.lnkProjectAttatch10.Visible Then
    '        buttonIndex = 10
    '    End If
    '    Return buttonIndex
    'End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>True：正常　False：エラー</returns>
    ''' <remarks></remarks>
    Private Function CheckInputValue() As Boolean
        Dim retValue As Boolean = True
        Dim strM As String = String.Empty
        '① プロセスラジオボタン(rdoProcess)	
        If Me.rdoProcess.SelectedIndex < 0 Then
            'いずれの選択肢も未チェックの場合は、メッセージ「プロセスを選択してください」を表示して処理を中断する
            strM = MessageComm.GetMessageContext("1", "007", Nothing) & "\n"
            retValue = False
        End If
        '② 工事名称（仮）テキストボックス(txtPjNameTemp)	
        If String.IsNullOrEmpty(Me.txtOrderCd.Text.Trim) And String.IsNullOrEmpty(Me.txtPjNameTemp.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「工事名称（仮）を入力してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "008", Nothing) & "\n"
            retValue = False
        End If

        '③ 顧客テキストボックス(txtCustomerName)	
        If String.IsNullOrEmpty(Me.txtOrderCd.Text.Trim) And String.IsNullOrEmpty(Me.txtCustomerName.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「顧客を入力してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "009", Nothing) & "\n"
            retValue = False

        End If
        '④ 顧客区分ラジオボタン(rdoCustomerType)	
        If Me.rdoCustomerType.SelectedIndex < 0 Then
            'いずれの選択肢も未チェックの場合は、メッセージ「顧客区分を選択してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "010", Nothing) & "\n"
            retValue = False
        End If

        '⑤ 製造部門テキストボックス(txtProductSecNm)	
        If String.IsNullOrEmpty(Me.txtProductSectNm.Text.Trim) Or Me.txtProductSectNm.Text = "※製造部門担当者の所属が自動セットされます" Then
            '値がNULL・ブランクの場合は、メッセージ「製造部門を選択してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "011", Nothing) & "\n"
            retValue = False
        End If

        '⑥ 製造部門担当者テキストボックス(txtProductUserNm)	
        If String.IsNullOrEmpty(Me.txtProductUser.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「製造部門担当者を選択または入力してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "012", Nothing) & "\n"
            retValue = False
        End If


        '⑦ 支社間取引の有無ラジオボタン(rdoBranchTransactionFlg)	
        If Me.rdoBranchTransactionFlg.SelectedIndex < 0 Then
            'いずれの選択肢も未チェックの場合は、メッセージ「支社間取引の有無を選択してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "001", Nothing) & "\n"
            retValue = False
        End If

        '⑧ 支援支社ドロップダウンリスト(ddlBranchList)	
        If Me.rdoBranchTransactionFlg.SelectedIndex > 0 And Me.ddlBranchList.SelectedIndex < 1 Then
            '支社間取引の有無の選択肢が「無」以外で、支援支社選択肢がブランクの場合は、メッセージ「支援/窓口支社を選択してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "002", Nothing) & "\n"
            retValue = False
        End If

        '⑨ 支社品質管理責任者テキストボックス(txtBranchQualityManager)	
        If String.IsNullOrEmpty(Me.txtBranchQualityManager.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「支社品質管理責任者を選択または入力してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "003", Nothing) & "\n"
            retValue = False
        End If

        '⑩ 部品質管理責任者テキストボックス(txtSectionQualityManager)	
        If String.IsNullOrEmpty(Me.txtSectionQualityManager.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「部品質管理責任者を選択または入力してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "004", Nothing) & "\n"
            retValue = False
        End If

        '⑪ グループ品質管理責任者テキストボックス(txtGroupQualityManager)	
        If String.IsNullOrEmpty(Me.txtGroupQualityManager.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「グループ品質管理責任者を選択または入力してください」を表示して処理を中断する
            strM += MessageComm.GetMessageContext("1", "005", Nothing) & "\n"
            retValue = False
        End If

        '⑫ プロジェクト品質管理責任者テキストボックス(txtProjectQualityManager)												
        If String.IsNullOrEmpty(Me.txtProjectQualityManager.Text.Trim) Then
            '値がNULL・ブランクの場合は、メッセージ「プロジェクト品質管理責任者を選択または入力してください」を表示して処理を中断する											
            strM += MessageComm.GetMessageContext("1", "006", Nothing) & "\n"
            retValue = False
        End If

        '⑬添付ファイル
        clsProject.fleProjectAttatch = Session(SESSION_PPROJECT_ATTATCH_ADD_NAME)
        clsProject.fleProjectAttatchFile = Session(SESSION_PPROJECT_ATTATCH_ADD)

        Dim tList As List(Of String) = New List(Of String)
        Dim i = 0
        For i = 0 To clsProject.fleProjectAttatch.Count - 1
            Dim tmpFileName = System.IO.Path.GetFileName(clsProject.fleProjectAttatch(i))
            If tmpFileName.Length > 50 Then

                tList = New List(Of String)
                tList.Add("ファイル名(" + tmpFileName + ")")
                tList.Add("50")

                strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

            End If
        Next

        '⑭営業プロセス(原価)のリスク管理表
        Dim sp1RiskManagementListFileName As String = Me.lnkSp1RiskManagementList.Text

        If sp1RiskManagementListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + sp1RiskManagementListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"
            retValue = False
        End If

        '⑮営業プロセス(原価)のチェックリスト
        Dim sp1RiskCheckFileName As String = Me.lnkSp1RiskCheckList.Text

            If sp1RiskCheckFileName.Length > 50 Then

                tList = New List(Of String)
                tList.Add("ファイル名(" + sp1RiskCheckFileName + ")")
                tList.Add("50")

                strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

            End If

        '⑯営業プロセス(見積)のリスク管理表
        Dim sp2RiskManagementListFileName As String = Me.lnkSp2RiskManagementList.Text

        If sp2RiskManagementListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + sp2RiskManagementListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

        End If

        '⑰営業プロセス(見積)のチェックリスト
        Dim sp2RiskCheckListFileName As String = Me.lnkSp2RiskCheckList.Text

        If sp2RiskCheckListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + sp2RiskCheckListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

        End If

        '⑱購買プロセスのリスク管理表
        Dim ppRiskManagementListFileName As String = Me.lnkPpRiskManagementList.Text

        If ppRiskManagementListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + ppRiskManagementListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

        End If

        '⑲購買プロセスのチェックリスト
        Dim ppRiskCheckListFileName As String = Me.lnkPpRiskCheckList.Text

        If ppRiskCheckListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + ppRiskCheckListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

        End If

        '⑳設計・開発プロセスのリスク管理表
        Dim dpRiskManagementListFileName As String = Me.lnkDpRiskManagementList.Text

        If dpRiskManagementListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + dpRiskManagementListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

        End If

        '21設計・開発プロセスのチェックリスト
        Dim dpRiskCheckListFileName As String = Me.lnkDpRiskCheckList.Text

        If dpRiskCheckListFileName.Length > 50 Then

            tList = New List(Of String)
            tList.Add("ファイル名(" + dpRiskCheckListFileName + ")")
            tList.Add("50")

            strM += MessageComm.GetMessageContext("1", "114", tList) & "\n"

        End If

        ' エラーがある場合、メッセージを表示し、処理を中断する
        If strM <> String.Empty Then
            strM = strM.Replace("'", "\'")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & strM & "');", True)

        End If

        Return retValue
    End Function

    ''' <summary>
    ''' 案件情報設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetProjectValue()

        clsProject.lblCreatedUserName = Me.lblCreatedUserName.Text.Trim
        clsProject.lblModifiedUserName = Me.lblModifiedUserName.Text.Trim
        clsProject.txtPjNo = Me.txtPjNo.Text.Trim
        clsProject.rdoProcess = Me.rdoProcess.SelectedValue
        clsProject.txtPjNameTemp = Me.txtPjNameTemp.Text.Trim
        clsProject.txtCustomerName = Me.txtCustomerName.Text.Trim
        clsProject.txtOrderCd = Me.txtOrderCd.Text.Trim
        clsProject.txtRelateOrderCd = Me.txtRelateOrderCd.Text.Trim
        clsProject.txtOrderNm = Me.txtOrderNm.Text.Trim
        clsProject.txtCompyNm = Me.txtCompyNm.Text.Trim
        clsProject.rdoCustomerType = Me.rdoCustomerType.SelectedValue
        clsProject.txtJyuchuCrr = Me.txtJyuchuCrr.Text.Trim
        clsProject.txtNokiYmd = Me.txtNokiYmd.Text.Trim
        clsProject.txtJyuchuSectNm = Me.txtJyuchuSectNm.Text.Trim
        clsProject.txtJyuchuUserNm = Me.txtJyuchuUserNm.Text.Trim
        clsProject.hdnProductSectId = Me.hdnProductSectId.Value
        clsProject.hdnProductSectCd = Me.hdnProductSectCd.Value
        clsProject.txtProductSectNm = Me.txtProductSectNm.Text.Trim

        clsProject.hdnProductUserId = Me.hdnProductUserId.Value
        clsProject.hdnProductUserCd = Me.hdnProductUserCd.Value
        clsProject.txtProductUser = Me.txtProductUser.Text.Trim
        clsProject.rdoBranchTransactionFlg = Me.rdoBranchTransactionFlg.SelectedValue

        If Me.ddlBranchList.Enabled = True Then
            clsProject.ddlBranchListId = Me.ddlBranchList.SelectedValue
            clsProject.ddlBranchListNm = Me.ddlBranchList.SelectedItem.Text
        Else
            clsProject.ddlBranchListId = " "
            clsProject.ddlBranchListNm = " "
        End If

        clsProject.txtBranchQualityManager = Me.txtBranchQualityManager.Text.Trim
        clsProject.txtSectionQualityManager = Me.txtSectionQualityManager.Text.Trim
        clsProject.txtGroupQualityManager = Me.txtGroupQualityManager.Text.Trim
        clsProject.txtProjectQualityManager = Me.txtProjectQualityManager.Text.Trim
        clsProject.txtRiskPreventionManager = Me.txtRiskPreventionManager.Text.Trim

        clsProject.chkRpm500MilFlg = Me.chkRpm500MilFlg.Checked
        If Me.chkRpmFirstProductFlg.Checked Then
            clsProject.chkRpmFirstProductFlg = "1"
        Else
            clsProject.chkRpmFirstProductFlg = "0"
        End If


        clsProject.ddlFirstProduct = Me.ddlFirstProduct.SelectedItem.Value
        clsProject.txtRpmFirstProductCause = Me.txtRpmFirstProductCause.Text.Trim

        If Me.chkRpmSpecialProductFlg.Checked Then
            clsProject.chkRpmSpecialProductFlg = "1"
        Else
            clsProject.chkRpmSpecialProductFlg = "0"
        End If

        clsProject.ddlSpecialProduct = Me.ddlSpecialProduct.Text.Trim
        clsProject.txtRpmSpecialProductCause = Me.txtRpmSpecialProductCause.Text.Trim

        If Me.rdoRpmType.SelectedValue = "0" Then
            clsProject.rdoRpmType = "A"
        ElseIf Me.rdoRpmType.SelectedValue = "1" Then
            clsProject.rdoRpmType = "B"
        End If

        '添付ファイル設定
        clsProject.fleProjectAttatch = Session(SESSION_PPROJECT_ATTATCH_ADD_NAME)
        clsProject.fleProjectAttatchFile = Session(SESSION_PPROJECT_ATTATCH_ADD)

        'If Me.lnkProjectAttatch1.Text <> String.Empty And Me.lnkProjectAttatch1.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch1.Text)

        '    If Session("projectAttatchFile1") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile1"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If
        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch1SeqNo.Value)

        'End If

        'If Me.lnkProjectAttatch2.Text <> String.Empty And Me.lnkProjectAttatch2.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch2.Text)

        '    If Session("projectAttatchFile2") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile2"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch2SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch3.Text <> String.Empty And Me.lnkProjectAttatch3.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch3.Text)

        '    If Session("projectAttatchFile3") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile3"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch3SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch4.Text <> String.Empty And Me.lnkProjectAttatch4.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch4.Text)

        '    If Session("projectAttatchFile4") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile4"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch4SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch5.Text <> String.Empty And Me.lnkProjectAttatch5.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch5.Text)

        '    If Session("projectAttatchFile5") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile5"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch5SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch6.Text <> String.Empty And Me.lnkProjectAttatch6.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch6.Text)

        '    If Session("projectAttatchFile6") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile6"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch6SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch7.Text <> String.Empty And Me.lnkProjectAttatch7.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch7.Text)

        '    If Session("projectAttatchFile7") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile7"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch7SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch8.Text <> String.Empty And Me.lnkProjectAttatch8.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch8.Text)

        '    If Session("projectAttatchFile8") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile8"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If
        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch8SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch9.Text <> String.Empty And Me.lnkProjectAttatch9.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch9.Text)

        '    If Session("projectAttatchFile9") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile9"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch9SeqNo.Value)
        'End If

        'If Me.lnkProjectAttatch10.Text <> String.Empty And Me.lnkProjectAttatch10.Visible Then
        '    clsProject.fleProjectAttatch.Add(Me.lnkProjectAttatch10.Text)
        '    If Session("projectAttatchFile10") IsNot Nothing Then
        '        clsProject.fleProjectAttatchFile.Add(Session("projectAttatchFile10"))
        '    Else
        '        clsProject.fleProjectAttatchFile.Add(Nothing)
        '    End If

        '    clsProject.fleProjectAttatchSeqNo.Add(Me.hdnProjectAttatch10SeqNo.Value)
        'End If

        clsProject.rdoProjectType = Me.rdoProjectType.SelectedValue
        clsProject.fleSp1RiskManagementList = Me.lnkSp1RiskManagementList.Text

        clsProject.fleSp1RiskManagementFileList = Session("sp1RiskManagementListFile")

        clsProject.fleSp1RiskCheckList = Me.lnkSp1RiskCheckList.Text
        clsProject.fleSp1RiskCheckFileList = Session("sp1RiskCheckFile")

        clsProject.chkSp1NoNeedFlg = Me.chkSp1NoNeedFlg.Checked
        clsProject.chkSp1CompleteFlg = Me.chkSp1CompleteFlg.Checked

        clsProject.fleSp2RiskManagementList = Me.lnkSp2RiskManagementList.Text
        clsProject.fleSp2RiskManagementFileList = Session("sp2RiskManagementListFile")

        clsProject.fleSp2RiskCheckList = Me.lnkSp2RiskCheckList.Text
        clsProject.fleSp2RiskCheckFileList = Session("sp2RiskCheckListFile")

        clsProject.chkSp2NoNeedFlg = Me.chkSp2NoNeedFlg.Checked
        clsProject.chkSp2CompleteFlg = Me.chkSp2CompleteFlg.Checked

        clsProject.flePpRiskManagementList = Me.lnkPpRiskManagementList.Text
        clsProject.flePpRiskManagementFileList = Session("ppRiskManagementListFile")

        clsProject.flePpRiskCheckList = Me.lnkPpRiskCheckList.Text
        clsProject.flePpRiskCheckFileList = Session("ppRiskCheckListFile")

        clsProject.chkPpNoNeedFlg = Me.chkPpNoNeedFlg.Checked
        clsProject.chkPpCompleteFlg = Me.chkPpCompleteFlg.Checked

        clsProject.fleDpRiskManagementList = Me.lnkDpRiskManagementList.Text
        clsProject.fleDpRiskManagemenFileList = Session("dpRiskManagementListFile")

        clsProject.fleDpRiskCheckList = Me.lnkDpRiskCheckList.Text
        clsProject.fleDpRiskCheckFileList = Session("dpRiskCheckListFile")

        clsProject.chkDpNoNeedFlg = Me.chkDpNoNeedFlg.Checked
        clsProject.chkDpCompleteFlg = Me.chkDpCompleteFlg.Checked

        clsProject.hdnModifiedOn = Me.hdnModifiedOn.Value

        '検討表ファイルフラグ設定
        clsProject.fleSp1RiskManagementListIsUpdate = Me.hdnSp1RiskManagementListIsUpdat.Value
        clsProject.fleSp1RiskManagementListIsDelete = Me.hdnSp1RiskManagementListIsDelete.Value
        clsProject.fleSp1RiskCheckListIsUpdate = Me.hdnSp1RiskCheckListIsUpdate.Value
        clsProject.fleSp1RiskCheckListIsDelete = Me.hdnSp1RiskCheckListIsDelete.Value

        clsProject.fleSp2RiskManagementListIsUpdate = Me.hdnSp2RiskManagementListIsUpdat.Value
        clsProject.fleSp2RiskManagementListIsDelete = Me.hdnSp2RiskManagementListIsDelete.Value
        clsProject.fleSp2RiskCheckListIsUpdate = Me.hdnSp2RiskCheckListIsUpdate.Value
        clsProject.fleSp2RiskCheckListIsDelete = Me.hdnSp2RiskCheckListIsDelete.Value

        clsProject.flePpRiskManagementListIsUpdate = Me.hdnPpRiskManagementListIsUpdat.Value
        clsProject.flePpRiskManagementListIsDelete = Me.hdnPpRiskManagementListIsDelete.Value
        clsProject.flePpRiskCheckListIsUpdate = Me.hdnPpRiskCheckListIsUpdate.Value
        clsProject.flePpRiskCheckListIsDelete = Me.hdnPpRiskCheckListIsDelete.Value

        clsProject.fleDpRiskManagementListIsUpdate = Me.hdnDpRiskManagementListIsUpdat.Value
        clsProject.fleDpRiskManagementListIsDelete = Me.hdnDpRiskManagementListIsDelete.Value
        clsProject.fleDpRiskCheckListIsUpdate = Me.hdnDpRiskCheckListIsUpdate.Value
        clsProject.fleDpRiskCheckListIsDelete = Me.hdnDpRiskCheckListIsDelete.Value


    End Sub

    ''' <summary>
    ''' 案件情報クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearProjectValue()
        clsProject.lblCreatedUserName = String.Empty
        clsProject.lblModifiedUserName = String.Empty
        clsProject.txtPjNo = String.Empty
        clsProject.rdoProcess = String.Empty
        clsProject.txtPjNameTemp = String.Empty
        clsProject.txtCustomerName = String.Empty
        clsProject.txtOrderCd = String.Empty
        clsProject.txtRelateOrderCd = String.Empty
        clsProject.txtOrderNm = String.Empty
        clsProject.txtCompyNm = String.Empty
        clsProject.rdoCustomerType = String.Empty
        clsProject.txtJyuchuCrr = String.Empty
        clsProject.txtNokiYmd = String.Empty
        clsProject.txtJyuchuSectNm = String.Empty
        clsProject.txtJyuchuUserNm = String.Empty
        clsProject.hdnProductSectId = String.Empty
        clsProject.hdnProductSectCd = String.Empty
        clsProject.txtProductSectNm = String.Empty

        clsProject.hdnProductUserId = String.Empty
        clsProject.hdnProductUserCd = String.Empty
        clsProject.txtProductUser = String.Empty
        clsProject.rdoBranchTransactionFlg = String.Empty
        clsProject.ddlBranchListId = String.Empty
        clsProject.ddlBranchListNm = String.Empty
        clsProject.txtBranchQualityManager = String.Empty
        clsProject.txtSectionQualityManager = String.Empty
        clsProject.txtGroupQualityManager = String.Empty
        clsProject.txtProjectQualityManager = String.Empty
        clsProject.txtRiskPreventionManager = String.Empty
        clsProject.chkRpm500MilFlg = String.Empty
        clsProject.chkRpmFirstProductFlg = String.Empty
        clsProject.ddlFirstProduct = String.Empty
        clsProject.txtRpmFirstProductCause = String.Empty
        clsProject.chkRpmSpecialProductFlg = String.Empty
        clsProject.ddlSpecialProduct = String.Empty
        clsProject.txtRpmSpecialProductCause = String.Empty
        clsProject.rdoRpmType = String.Empty
        clsProject.lnkProjectAttatch.Clear()
        clsProject.fleProjectAttatch.Clear()
        clsProject.fleProjectAttatchFile.Clear()
        clsProject.rdoProjectType = String.Empty
        clsProject.lnkSp1RiskManagementList = String.Empty
        clsProject.fleSp1RiskManagementList = String.Empty

        clsProject.fleSp1RiskManagementFileList = Nothing

        clsProject.lnkSp1RiskCheckList = String.Empty
        clsProject.fleSp1RiskCheckList = String.Empty
        clsProject.fleSp1RiskCheckFileList = Nothing
        clsProject.chkSp1NoNeedFlg = String.Empty
        clsProject.chkSp1CompleteFlg = String.Empty
        clsProject.lnkSp2RiskManagementList = String.Empty
        clsProject.fleSp2RiskManagementList = String.Empty
        clsProject.fleSp2RiskManagementFileList = Nothing
        clsProject.lnkSp2RiskCheckList = String.Empty
        clsProject.fleSp2RiskCheckList = String.Empty
        clsProject.fleSp2RiskCheckFileList = Nothing
        clsProject.chkSp2NoNeedFlg = String.Empty
        clsProject.chkSp2CompleteFlg = String.Empty
        clsProject.lnkPpRiskManagementList = String.Empty
        clsProject.flePpRiskManagementList = String.Empty
        clsProject.flePpRiskManagementFileList = Nothing
        clsProject.lnkPpRiskCheckList = String.Empty
        clsProject.flePpRiskCheckList = String.Empty
        clsProject.flePpRiskCheckFileList = Nothing
        clsProject.chkPpNoNeedFlg = String.Empty
        clsProject.chkPpCompleteFlg = String.Empty
        clsProject.lnkDpRiskManagementList = String.Empty
        clsProject.fleDpRiskManagementList = String.Empty
        clsProject.fleDpRiskManagemenFileList = Nothing
        clsProject.lnkDpRiskCheckList = String.Empty
        clsProject.fleDpRiskCheckList = String.Empty
        clsProject.fleDpRiskCheckFileList = Nothing
        clsProject.chkDpNoNeedFlg = String.Empty
        clsProject.chkDpCompleteFlg = String.Empty
        clsProject.lblOpenRound.Clear()
        clsProject.lblOpenDate.Clear()
        clsProject.lnkRiskPrevention.Clear()
        clsProject.hdnModifiedOn = String.Empty
        clsProject.createdUserId = String.Empty
        clsProject.a01m01SectCd = String.Empty

        '検討表ファイルフラグ設定
        clsProject.fleSp1RiskManagementListIsUpdate = ""
        clsProject.fleSp1RiskManagementListIsDelete = ""
        clsProject.fleSp1RiskCheckListIsUpdate = ""
        clsProject.fleSp1RiskCheckListIsDelete = ""

        clsProject.fleSp2RiskManagementListIsUpdate = ""
        clsProject.fleSp2RiskManagementListIsDelete = ""
        clsProject.fleSp2RiskCheckListIsUpdate = ""
        clsProject.fleSp2RiskCheckListIsDelete = ""

        clsProject.flePpRiskManagementListIsUpdate = ""
        clsProject.flePpRiskManagementListIsDelete = ""
        clsProject.flePpRiskCheckListIsUpdate = ""
        clsProject.flePpRiskCheckListIsDelete = ""

        clsProject.fleDpRiskManagementListIsUpdate = ""
        clsProject.fleDpRiskManagementListIsDelete = ""
        clsProject.fleDpRiskCheckListIsUpdate = ""
        clsProject.fleDpRiskCheckListIsDelete = ""

    End Sub

    ''' <summary>
    ''' リストの空行設定
    ''' </summary>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetRiskPreventionTableNull() As DataTable

        Dim dtRiskPre = New DataTable()

        dtRiskPre.Columns.Add("PROJECT_NO", System.Type.GetType("System.String"))
        dtRiskPre.Columns.Add("SEQ_NO", System.Type.GetType("System.String"))
        dtRiskPre.Columns.Add("REPORT_CATEGORY", System.Type.GetType("System.String"))
        dtRiskPre.Columns.Add("OPEN_DATE", System.Type.GetType("System.String"))
        dtRiskPre.Columns.Add("OPEN_ROUND", System.Type.GetType("System.String"))

        For i = 0 To 7
            Dim drNew As DataRow
            drNew = dtRiskPre.NewRow
            dtRiskPre.Rows.Add(drNew)

        Next

        Return dtRiskPre
    End Function



    ''' <summary>
    ''' ファイルダウンロード
    ''' </summary>
    ''' <param name="fileByte">ファイル</param>
    ''' <remarks></remarks>
    Private Sub DownLoadFile(ByVal fileByte As Byte(), ByVal fileNm As String)

        If fileByte Is Nothing Then
            Return
        End If

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileNm))
        HttpContext.Current.Response.AddHeader("Content-Length", fileByte.Length.ToString())
        HttpContext.Current.Response.ContentType = "application/application/octet-stream"
        HttpContext.Current.Response.BinaryWrite(fileByte)
        HttpContext.Current.Response.Flush()
        HttpContext.Current.Response.Close()

    End Sub
    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub fleSp1RiskManagementList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleSp1RiskManagementList.UploadedComplete

        Session("sp1RiskManagementListFile") = Me.fleSp1RiskManagementList.FileBytes
        Session("sp1RiskManagementListFileName") = Me.fleSp1RiskManagementList.FileName
        Session("clickControl") = "fleSp1RiskManagementList"


    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSp1RiskManagementListDel_Click(sender As Object, e As EventArgs) Handles btnSp1RiskManagementListDel.Click
        Me.fleSp1RiskManagementList.Visible = True
        Me.lnkSp1RiskManagementList.Text = String.Empty
        Me.btnSp1RiskManagementListDel.Visible = False

        Me.hdnSp1RiskManagementListIsDelete.Value = "1"
    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub fleSp1RiskCheckList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleSp1RiskCheckList.UploadedComplete
        Session("sp1RiskCheckFile") = Me.fleSp1RiskCheckList.FileBytes
        Session("sp1RiskCheckFileName") = Me.fleSp1RiskCheckList.FileName
        Session("clickControl") = "fleSp1RiskCheckList"
    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSp1RiskCheckListDel_Click(sender As Object, e As EventArgs) Handles btnSp1RiskCheckListDel.Click

        '表示設定
        Me.fleSp1RiskCheckList.Visible = True
        Me.lnkSp1RiskCheckList.Text = String.Empty
        Me.btnSp1RiskCheckListDel.Visible = False

        Me.hdnSp1RiskCheckListIsDelete.Value = "1"
    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub fleSp2RiskManagementList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleSp2RiskManagementList.UploadedComplete
        Session("sp2RiskManagementListFile") = Me.fleSp2RiskManagementList.FileBytes
        Session("sp2RiskManagementListFileName") = Me.fleSp2RiskManagementList.FileName
        Session("clickControl") = "fleSp2RiskManagementList"

    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSp2RiskManagementListDel_Click(sender As Object, e As EventArgs) Handles btnSp2RiskManagementListDel.Click
        Me.fleSp2RiskManagementList.Visible = True
        Me.lnkSp2RiskManagementList.Text = String.Empty
        Me.btnSp2RiskManagementListDel.Visible = False

        Me.hdnSp2RiskManagementListIsDelete.Value = "1"

    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub fleSp2RiskCheckList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleSp2RiskCheckList.UploadedComplete
        Session("sp2RiskCheckListFile") = Me.fleSp2RiskCheckList.FileBytes
        Session("sp2RiskCheckListFileName") = Me.fleSp2RiskCheckList.FileName
        Session("clickControl") = "fleSp2RiskCheckList"

    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSp2RiskCheckListDel_Click(sender As Object, e As EventArgs) Handles btnSp2RiskCheckListDel.Click
        Me.fleSp2RiskCheckList.Visible = True
        Me.lnkSp2RiskCheckList.Text = String.Empty
        Me.btnSp2RiskCheckListDel.Visible = False

        Me.hdnSp2RiskCheckListIsDelete.Value = "1"
    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub flePpRiskManagementList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles flePpRiskManagementList.UploadedComplete
        Session("ppRiskManagementListFile") = Me.flePpRiskManagementList.FileBytes
        Session("ppRiskManagementListFileName") = Me.flePpRiskManagementList.FileName
        Session("clickControl") = "flePpRiskManagementList"

    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnPpRiskManagementListDel_Click(sender As Object, e As EventArgs) Handles btnPpRiskManagementListDel.Click
        Me.flePpRiskManagementList.Visible = True
        Me.lnkPpRiskManagementList.Text = String.Empty
        Me.btnPpRiskManagementListDel.Visible = False

        Me.hdnPpRiskManagementListIsDelete.Value = "1"

    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub flePpRiskCheckList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles flePpRiskCheckList.UploadedComplete
        Session("ppRiskCheckListFile") = Me.flePpRiskCheckList.FileBytes
        Session("ppRiskCheckListFileName") = Me.flePpRiskCheckList.FileName
        Session("clickControl") = "flePpRiskCheckList"

    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnPpRiskCheckListDel_Click(sender As Object, e As EventArgs) Handles btnPpRiskCheckListDel.Click
        Me.flePpRiskCheckList.Visible = True
        Me.lnkPpRiskCheckList.Text = String.Empty
        Me.btnPpRiskCheckListDel.Visible = False

        Me.hdnPpRiskCheckListIsDelete.Value = "1"
    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub fleDpRiskManagementList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleDpRiskManagementList.UploadedComplete
        Session("dpRiskManagementListFile") = Me.fleDpRiskManagementList.FileBytes
        Session("dpRiskManagementListFileName") = Me.fleDpRiskManagementList.FileName
        Session("clickControl") = "fleDpRiskManagementList"

    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnDpRiskManagementListDel_Click(sender As Object, e As EventArgs) Handles btnDpRiskManagementListDel.Click
        Me.fleDpRiskManagementList.Visible = True
        Me.lnkDpRiskManagementList.Text = String.Empty
        Me.btnDpRiskManagementListDel.Visible = False

        Me.hdnDpRiskManagementListIsDelete.Value = "1"
    End Sub

    ''' <summary>
    ''' ファイルアップロード完了
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub fleDpRiskCheckList_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleDpRiskCheckList.UploadedComplete
        Session("dpRiskCheckListFile") = Me.fleDpRiskCheckList.FileBytes
        Session("dpRiskCheckListFileName") = Me.fleDpRiskCheckList.FileName
        Session("clickControl") = "fleDpRiskCheckList"

    End Sub

    ''' <summary>
    ''' ファイルの削除ボタンクリック
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnDpRiskCheckListDel_Click(sender As Object, e As EventArgs) Handles btnDpRiskCheckListDel.Click
        Me.fleDpRiskCheckList.Visible = True
        Me.lnkDpRiskCheckList.Text = String.Empty
        Me.btnDpRiskCheckListDel.Visible = False

        Me.hdnDpRiskCheckListIsDelete.Value = "1"

    End Sub

    ''' <summary>
    ''' (21)営業プロセス(原価)不要チェックボックス変更イベント(chkSp1NoNeedFlg_CheckedChanged) 
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub chkSp1NoNeedFlg_CheckedChanged(sender As Object, e As EventArgs) Handles chkSp1NoNeedFlg.CheckedChanged
        'チェックされた場合
        If Me.chkSp1NoNeedFlg.Checked Then
            'リスク予防管理活動の営業プロセス(原価)行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdSp1Pro.BgColor = GREY_COLOR
            Me.tdSp1Risk.BgColor = GREY_COLOR
            Me.tdSp1RiskFile.BgColor = GREY_COLOR
            Me.tdSp1Check.BgColor = GREY_COLOR
            Me.tdSp1CheckFile.BgColor = GREY_COLOR
            Me.tdSp1NoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の営業プロセス(原価)行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdSp1Pro.BgColor = ""
            Me.tdSp1Risk.BgColor = ""
            Me.tdSp1RiskFile.BgColor = ""
            Me.tdSp1Check.BgColor = ""
            Me.tdSp1CheckFile.BgColor = ""
            Me.tdSp1NoNeedFlg.BgColor = ""
        End If

    End Sub

    ''' <summary>
    ''' (22)営業プロセス(見積)不要チェックボックス変更イベント(chkSp2NoNeedFlg_CheckedChanged)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub chkSp2NoNeedFlg_CheckedChanged(sender As Object, e As EventArgs) Handles chkSp2NoNeedFlg.CheckedChanged
        'チェックされた場合
        If Me.chkSp2NoNeedFlg.Checked Then
            'リスク予防管理活動の営業プロセス(見積)行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdSp2Pro.BgColor = GREY_COLOR
            Me.tdSp2Risk.BgColor = GREY_COLOR
            Me.tdSp2RiskFile.BgColor = GREY_COLOR
            Me.tdSp2Check.BgColor = GREY_COLOR
            Me.tdSp2CheckFile.BgColor = GREY_COLOR
            Me.tdSp2NoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の営業プロセス(見積)行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdSp2Pro.BgColor = ""
            Me.tdSp2Risk.BgColor = ""
            Me.tdSp2RiskFile.BgColor = ""
            Me.tdSp2Check.BgColor = ""
            Me.tdSp2CheckFile.BgColor = ""
            Me.tdSp2NoNeedFlg.BgColor = ""
        End If
    End Sub

    ''' <summary>
    ''' (23)購買プロセス不要チェックボックス変更イベント(chkSp3NoNeedFlg_CheckedChanged)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub chkPpNoNeedFlg_CheckedChanged(sender As Object, e As EventArgs) Handles chkPpNoNeedFlg.CheckedChanged
        'チェックされた場合
        If Me.chkPpNoNeedFlg.Checked Then
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdPpPro.BgColor = GREY_COLOR
            Me.tdPpRisk.BgColor = GREY_COLOR
            Me.tdPpRiskFile.BgColor = GREY_COLOR
            Me.tdPpCheck.BgColor = GREY_COLOR
            Me.tdPpCheckFile.BgColor = GREY_COLOR
            Me.tdPpNoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdPpPro.BgColor = ""
            Me.tdPpRisk.BgColor = ""
            Me.tdPpRiskFile.BgColor = ""
            Me.tdPpCheck.BgColor = ""
            Me.tdPpCheckFile.BgColor = ""
            Me.tdPpNoNeedFlg.BgColor = ""
        End If
    End Sub

    ''' <summary>
    ''' (24)設計・開発プロセス不要チェックボックス変更イベント(chkSp4NoNeedFlg_CheckedChanged)
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub chkDpNoNeedFlg_CheckedChanged(sender As Object, e As EventArgs) Handles chkDpNoNeedFlg.CheckedChanged
        'チェックされた場合
        If Me.chkDpNoNeedFlg.Checked Then
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdDpPro.BgColor = GREY_COLOR
            Me.tdDpRisk.BgColor = GREY_COLOR
            Me.tdDpRiskFile.BgColor = GREY_COLOR
            Me.tdDpCheck.BgColor = GREY_COLOR
            Me.tdDpCheckFile.BgColor = GREY_COLOR
            Me.tdDpNoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdDpPro.BgColor = ""
            Me.tdDpRisk.BgColor = ""
            Me.tdDpRiskFile.BgColor = ""
            Me.tdDpCheck.BgColor = ""
            Me.tdDpCheckFile.BgColor = ""
            Me.tdDpNoNeedFlg.BgColor = ""
        End If
    End Sub

    ''' <summary>
    ''' (8.3) 初品チェックボックスクリックイベント(chkRpmFirstProductFlg_Click)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkRpmFirstProductFlg_CheckedChanged(sender As Object, e As EventArgs) Handles chkRpmFirstProductFlg.CheckedChanged
        'チェックされた場合
        If Me.chkRpmFirstProductFlg.Checked Then
            '初品選択肢ドロップダウンリスト(ddlFirstProduct)のEnabledをTrueにセットする
            Me.ddlFirstProduct.Enabled = True
            '初品理由テキストボックス(txtRpmFirstProductCause)のEnabledをTrueにセットする
            Me.txtRpmFirstProductCause.Enabled = True
        Else
            '初品選択肢ドロップダウンリスト(ddlFirstProduct)のEnabledをFalseにセットする
            Me.ddlFirstProduct.Enabled = False
            Me.ddlFirstProduct.SelectedIndex = 0
            '初品理由テキストボックス(txtRpmFirstProductCause)のEnabledをFalseにセットする
            Me.txtRpmFirstProductCause.Enabled = False
            Me.txtRpmFirstProductCause.Text = String.Empty

        End If
    End Sub

    ''' <summary>
    ''' (8.4) 特殊チェックボックスクリックイベント(chkRpmSpecialProductFlg_Click)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkRpmSpecialProductFlg_CheckedChanged(sender As Object, e As EventArgs) Handles chkRpmSpecialProductFlg.CheckedChanged
        'チェックされた場合
        If Me.chkRpmSpecialProductFlg.Checked Then
            '特殊選択肢ドロップダウンリスト(ddlSpecialProduct)のEnabledをTrueにセットする
            Me.ddlSpecialProduct.Enabled = True
            '特殊理由テキストボックス(txtRpmSpecialProductCause)のEnabledをTrueにセットする
            Me.txtRpmSpecialProductCause.Enabled = True
        Else
            '特殊選択肢ドロップダウンリスト(ddlSpecialProduct)のEnabledをFlaseにセットする
            Me.ddlSpecialProduct.Enabled = False
            Me.ddlSpecialProduct.SelectedIndex = 0
            '特殊理由テキストボックス(txtRpmSpecialProductCause)のEnabledをFlaseにセットする
            Me.txtRpmSpecialProductCause.Enabled = False
            Me.txtRpmSpecialProductCause.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' リスク不安要素検討表テーブルのカラー設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRiskManagementRowColor()
        '営業プロセス(原価)不要チェックボックスチェックされた場合
        If Me.chkSp1NoNeedFlg.Checked Then
            'リスク予防管理活動の営業プロセス(原価)行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdSp1Pro.BgColor = GREY_COLOR
            Me.tdSp1Risk.BgColor = GREY_COLOR
            Me.tdSp1RiskFile.BgColor = GREY_COLOR
            Me.tdSp1Check.BgColor = GREY_COLOR
            Me.tdSp1CheckFile.BgColor = GREY_COLOR
            Me.tdSp1NoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の営業プロセス(原価)行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdSp1Pro.BgColor = ""
            Me.tdSp1Risk.BgColor = ""
            Me.tdSp1RiskFile.BgColor = ""
            Me.tdSp1Check.BgColor = ""
            Me.tdSp1CheckFile.BgColor = ""
            Me.tdSp1NoNeedFlg.BgColor = ""
        End If

        '営業プロセス(見積)不要チェックボックスチェックされた場合
        If Me.chkSp2NoNeedFlg.Checked Then
            'リスク予防管理活動の営業プロセス(見積)行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdSp2Pro.BgColor = GREY_COLOR
            Me.tdSp2Risk.BgColor = GREY_COLOR
            Me.tdSp2RiskFile.BgColor = GREY_COLOR
            Me.tdSp2Check.BgColor = GREY_COLOR
            Me.tdSp2CheckFile.BgColor = GREY_COLOR
            Me.tdSp2NoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の営業プロセス(見積)行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdSp2Pro.BgColor = ""
            Me.tdSp2Risk.BgColor = ""
            Me.tdSp2RiskFile.BgColor = ""
            Me.tdSp2Check.BgColor = ""
            Me.tdSp2CheckFile.BgColor = ""
            Me.tdSp2NoNeedFlg.BgColor = ""
        End If

        '購買プロセス不要チェックボックスチェックされた場合
        If Me.chkPpNoNeedFlg.Checked Then
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdPpPro.BgColor = GREY_COLOR
            Me.tdPpRisk.BgColor = GREY_COLOR
            Me.tdPpRiskFile.BgColor = GREY_COLOR
            Me.tdPpCheck.BgColor = GREY_COLOR
            Me.tdPpCheckFile.BgColor = GREY_COLOR
            Me.tdPpNoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdPpPro.BgColor = ""
            Me.tdPpRisk.BgColor = ""
            Me.tdPpRiskFile.BgColor = ""
            Me.tdPpCheck.BgColor = ""
            Me.tdPpCheckFile.BgColor = ""
            Me.tdPpNoNeedFlg.BgColor = ""
        End If

        '設計・開発プロセス不要チェックボックスチェックされた場合
        If Me.chkDpNoNeedFlg.Checked Then
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色をグレーに変更する
            Me.tdDpPro.BgColor = GREY_COLOR
            Me.tdDpRisk.BgColor = GREY_COLOR
            Me.tdDpRiskFile.BgColor = GREY_COLOR
            Me.tdDpCheck.BgColor = GREY_COLOR
            Me.tdDpCheckFile.BgColor = GREY_COLOR
            Me.tdDpNoNeedFlg.BgColor = GREY_COLOR
        Else
            'チェックが外れた場合
            'リスク予防管理活動の購買プロセス行のプロセス・リスク・不運要素検討表・不要欄までの背景色を通常の背景色に変更する
            Me.tdDpPro.BgColor = ""
            Me.tdDpRisk.BgColor = ""
            Me.tdDpRiskFile.BgColor = ""
            Me.tdDpCheck.BgColor = ""
            Me.tdDpCheckFile.BgColor = ""
            Me.tdDpNoNeedFlg.BgColor = ""
        End If
    End Sub

    ''' <summary>
    ''' リスク予防管理対象のコントロール使用可設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetRmpControlEnabled()
        '初品チェックボックス(chkRpmFirstProductFlg)チェックされた場合
        If Me.chkRpmFirstProductFlg.Checked Then
            '初品選択肢ドロップダウンリスト(ddlFirstProduct)のEnabledをTrueにセットする
            Me.ddlFirstProduct.Enabled = True
            '初品理由テキストボックス(txtRpmFirstProductCause)のEnabledをTrueにセットする
            Me.txtRpmFirstProductCause.Enabled = True
        Else
            '初品選択肢ドロップダウンリスト(ddlFirstProduct)のEnabledをFalseにセットする
            Me.ddlFirstProduct.Enabled = False
            '初品理由テキストボックス(txtRpmFirstProductCause)のEnabledをFalseにセットする
            Me.txtRpmFirstProductCause.Enabled = False
        End If

        '特殊チェックボックスクチェックされた場合
        If Me.chkRpmSpecialProductFlg.Checked Then
            '特殊選択肢ドロップダウンリスト(ddlSpecialProduct)のEnabledをTrueにセットする
            Me.ddlSpecialProduct.Enabled = True
            '特殊理由テキストボックス(txtRpmSpecialProductCause)のEnabledをTrueにセットする
            Me.txtRpmSpecialProductCause.Enabled = True
        Else
            '特殊選択肢ドロップダウンリスト(ddlSpecialProduct)のEnabledをFlaseにセットする
            Me.ddlSpecialProduct.Enabled = False
            '特殊理由テキストボックス(txtRpmSpecialProductCause)のEnabledをFlaseにセットする
            Me.txtRpmSpecialProductCause.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' オーダ削除
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnDeleteOrderNo_Click(sender As Object, e As ImageClickEventArgs) Handles btnDeleteOrderNo.Click
        Me.txtOrderCd.Text = String.Empty

        '※オーダテキストボックスの値が削除された場合は以下の項目の値を削除し、行を非表示にする。初期表示行のを再表示する	
        '値の削除を行う項目			
        '① 工事名称(txtOrderNm)
        Me.txtOrderNm.Text = String.Empty
        '② 顧客(txtCompyNm)	
        Me.txtCompyNm.Text = String.Empty
        '③ 受注金額(txtJyuchuCrr)	
        Me.txtJyuchuCrr.Text = String.Empty
        '④ 納期日(txtNokiYmd)		
        Me.txtNokiYmd.Text = String.Empty
        '⑤ 受注部門(txtJyuchuSectNm)		
        Me.txtJyuchuSectNm.Text = String.Empty
        '⑥ 受注担当者(txtJyuchuUserNm)			
        Me.txtJyuchuUserNm.Text = String.Empty
        '非表示にする項目			
        '① 工事名称行(txtOrderNm)	
        Me.trOrderNm.Visible = False
        '② 顧客行(txtCompyNm)		
        Me.trCompyNm.Visible = False
        '③ 受注金額、納期日行(txtJyuchuCrr,txtNokiYmd)	
        Me.trJyuchuCrrNokiYmd.Visible = False
        '④ 受注部門、受注担当者行(txtJyuchuSectNm,txtJyuchuUserNm)			
        Me.trJyuchuSectNmJyuchuUserNm.Visible = False

        '初期表示の項目を再表示する			
        '① 工事名称（仮）行(txtPjNameTemp)			
        Me.trPjNameTemp.Visible = True
        '② 顧客行(txtCustomerName)
        Me.trCustomerName.Visible = True

    End Sub

    ''' <summary>
    ''' 関連オーダクリア
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRelateOrderDel_Click(sender As Object, e As ImageClickEventArgs) Handles btnRelateOrderDel.Click
        '関連オーダクリア
        Me.txtRelateOrderCd.Text = String.Empty
    End Sub

    ''' <summary>
    ''' ファイルアップロード完了で、画面を再更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lnkUpdate_Click(sender As Object, e As EventArgs) Handles lnkUpdate.Click

        Dim controlNm As String = String.Empty

        If Session("clickControl") IsNot Nothing Then
            controlNm = Session("clickControl")
        End If

        If controlNm = String.Empty Then
            Return
        End If

        Select Case controlNm
            Case "fleSp1RiskManagementList"
                Me.hdnSp1RiskManagementListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnSp1RiskManagementListIsUpdat.Value <> "1" Then
                    Me.hdnSp1RiskManagementListIsUpdat.Value = "0"
                End If

                Me.lnkSp1RiskManagementList.Text = Session("sp1RiskManagementListFileName")

                If Me.fleSp1RiskManagementList.Visible Then
                    Me.fleSp1RiskManagementList.Visible = False
                    Me.btnSp1RiskManagementListDel.Visible = True
                End If

            Case "fleSp1RiskCheckList"
                Me.hdnSp1RiskCheckListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnSp1RiskCheckListIsUpdate.Value <> "1" Then
                    Me.hdnSp1RiskCheckListIsUpdate.Value = "0"
                End If

                'リックボタンのテキスト再設定
                Me.lnkSp1RiskCheckList.Text = Session("sp1RiskCheckFileName")

                '非表示設定
                If Me.fleSp1RiskCheckList.Visible Then
                    Me.fleSp1RiskCheckList.Visible = False
                    Me.btnSp1RiskCheckListDel.Visible = True
                End If

            Case "fleSp2RiskManagementList"
                Me.hdnSp2RiskManagementListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnSp2RiskManagementListIsUpdat.Value <> "1" Then
                    Me.hdnSp2RiskManagementListIsUpdat.Value = "0"
                End If

                'リックボタンのテキスト再設定
                Me.lnkSp2RiskManagementList.Text = Session("sp2RiskManagementListFileName")

                '非表示設定
                If Me.fleSp2RiskManagementList.Visible Then
                    Me.fleSp2RiskManagementList.Visible = False
                    Me.btnSp2RiskManagementListDel.Visible = True
                End If

            Case "fleSp2RiskCheckList"
                'リックボタンのテキスト再設定
                Me.lnkSp2RiskCheckList.Text = Session("sp2RiskCheckListFileName")

                '非表示設定
                If Me.fleSp2RiskCheckList.Visible Then
                    Me.fleSp2RiskCheckList.Visible = False
                    Me.btnSp2RiskCheckListDel.Visible = True
                End If

                Me.hdnSp2RiskCheckListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnSp2RiskCheckListIsUpdate.Value <> "1" Then
                    Me.hdnSp2RiskCheckListIsUpdate.Value = 0
                End If
            Case "flePpRiskManagementList"
                Me.hdnPpRiskManagementListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnPpRiskManagementListIsUpdat.Value <> "1" Then
                    Me.hdnPpRiskManagementListIsUpdat.Value = 0
                End If

                'リックボタンのテキスト再設定
                Me.lnkPpRiskManagementList.Text = Session("ppRiskManagementListFileName")

                '非表示設定
                If Me.flePpRiskManagementList.Visible Then
                    Me.flePpRiskManagementList.Visible = False
                    Me.btnPpRiskManagementListDel.Visible = True
                End If

            Case "flePpRiskCheckList"
                Me.hdnPpRiskCheckListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnPpRiskCheckListIsUpdate.Value <> "1" Then
                    Me.hdnPpRiskCheckListIsUpdate.Value = 0
                End If

                'リックボタンのテキスト再設定
                Me.lnkPpRiskCheckList.Text = Session("ppRiskCheckListFileName")

                '非表示設定
                If Me.flePpRiskCheckList.Visible Then
                    Me.flePpRiskCheckList.Visible = False
                    Me.btnPpRiskCheckListDel.Visible = True
                End If

            Case "fleDpRiskManagementList"
                Me.hdnDpRiskManagementListIsDelete.Value = "0"

                '既存以外場合
                If Me.hdnDpRiskManagementListIsUpdat.Value <> "1" Then
                    Me.hdnDpRiskManagementListIsUpdat.Value = 0
                End If

                'リックボタンのテキスト再設定
                Me.lnkDpRiskManagementList.Text = Session("dpRiskManagementListFileName")

                '非表示設定
                If Me.fleDpRiskManagementList.Visible Then
                    Me.fleDpRiskManagementList.Visible = False
                    Me.btnDpRiskManagementListDel.Visible = True
                End If

            Case "fleDpRiskCheckList"
                Me.hdnDpRiskCheckListIsUpdate.Value = "0"

                '既存以外場合
                If Me.hdnDpRiskCheckListIsUpdate.Value <> "1" Then
                    Me.hdnDpRiskCheckListIsUpdate.Value = 0
                End If

                'リックボタンのテキスト再設定
                Me.lnkDpRiskCheckList.Text = Session("dpRiskCheckListFileName")

                '非表示設定
                If Me.fleDpRiskCheckList.Visible Then
                    Me.fleDpRiskCheckList.Visible = False
                    Me.btnDpRiskCheckListDel.Visible = True
                End If
        End Select

    End Sub

    ''' <summary>
    ''' ファイルアップロード完了で、画面を再更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lnkUpdateAttach_Click(sender As Object, e As EventArgs) Handles lnkUpdateAttach.Click
        ' 画面を再更新
        Dim dtAttatch As DataTable = GetAttatch()
        grdProjectAttatch.DataSource = dtAttatch
        grdProjectAttatch.DataBind()

        If (dtAttatch.Rows.Count >= 10) Then
            fleProjectAttatch.Visible = False
            rdoProjectType.Focus()
        Else
            fleProjectAttatch.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' SESSIONに格納したファイルを一覧を作成し、画面に設定する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetAttatch() As DataTable

        Dim dt As DataTable = Session(SESSION_PPROJECT_ATTATCH)
        dt = dt.Copy

        Dim fileNames As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD_NAME)
        Dim files As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD)

        For i As Integer = 0 To fileNames.Count - 1
            Dim newRow As DataRow = dt.NewRow()
            newRow.Item("FILE_SEQ_NO") = TMP_SEQ_BEGIN + i

            newRow.Item("ATTATCH_FILE_NAME") = fileNames(i)
            newRow.Item("isEditAble") = True
            dt.Rows.Add(newRow)
        Next

        Return dt

    End Function

    ''' <summary>
    ''' リストの空行設定
    ''' </summary>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Private Function GetRiskPreventionAttachNull() As DataTable

        Dim dtRiskPre = New DataTable()
        dtRiskPre.Columns.Add("FILE_SEQ_NO", System.Type.GetType("System.String"))
        dtRiskPre.Columns.Add("ATTATCH_FILE_NAME", System.Type.GetType("System.String"))

        Return dtRiskPre
    End Function

    ''' <summary>
    ''' ファイルアップロード完了処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub fleProjectAttatch_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles fleProjectAttatch.UploadedComplete

        ' ファイルがアップロード完了した場合、情報をSESSIONに格納
        If fleProjectAttatch.HasFile Then
            Dim fileNames As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD_NAME)
            Dim files As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD)

            fileNames.Add(fleProjectAttatch.FileName())
            files.Add(fleProjectAttatch.FileBytes)

            Session(SESSION_PPROJECT_ATTATCH_ADD_NAME) = fileNames
            Session(SESSION_PPROJECT_ATTATCH_ADD) = files

        End If

    End Sub

    ''' <summary>
    ''' 添付ファイル操作イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub grdProjectAttatch_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdProjectAttatch.RowCommand
        ' 添付ファイル削除ボタンクリックイベント
        If e.CommandName = "del" Then

            Dim seq = CInt(e.CommandArgument.ToString())

            If seq < TMP_SEQ_BEGIN Then
                ' ファイルはDBに保存している場合

                ' 選択したファイルをSESSIONに保存した添付ファイル一覧から削除
                Dim dt As DataTable = Session(SESSION_PPROJECT_ATTATCH)
                For Each row In dt.Rows
                    If row("FILE_SEQ_NO").ToString = e.CommandArgument.ToString() Then
                        dt.Rows.Remove(row)
                        Exit For
                    End If
                Next
                Session(SESSION_PPROJECT_ATTATCH) = dt

                ' 削除したファイルをSESSIONに記憶しておく
                Dim delList As ArrayList = Session(SESSION_PPROJECT_ATTATCH_DELETE)
                delList.Add(e.CommandArgument.ToString())
                Session(SESSION_PPROJECT_ATTATCH_DELETE) = delList
            Else
                ' ファイルは新規追加したファイル（SESSIONに格納）の場合

                Dim fileNames As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD_NAME)
                Dim files As ArrayList = Session(SESSION_PPROJECT_ATTATCH_ADD)

                fileNames.RemoveAt(seq - TMP_SEQ_BEGIN)
                files.RemoveAt(seq - TMP_SEQ_BEGIN)

            End If

            ' 画面を更新
            Dim dtAttatch As DataTable = GetAttatch()
            grdProjectAttatch.DataSource = dtAttatch
            grdProjectAttatch.DataBind()

            If (dtAttatch.Rows.Count >= 10) Then
                fleProjectAttatch.Visible = False
            Else
                fleProjectAttatch.Visible = True
            End If

        End If
    End Sub

End Class