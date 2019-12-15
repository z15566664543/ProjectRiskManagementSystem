Public Class RiskPrevention
    Inherits System.Web.UI.Page

    Private ReadOnly SESSION_PREVENTION_ATTATCH As String = "SESSION_RISK_PREVENTION_ATTATCH"
    Private ReadOnly SESSION_PREVENTION_ATTATCH_DELETE As String = "SESSION_RISK_PREVENTION_ATTATCH_DELETE"
    Private ReadOnly SESSION_PREVENTION_ATTATCH_ADD As String = "SESSION_RISK_PREVENTION_ATTATCH_ADD"
    Private ReadOnly SESSION_PREVENTION_ATTATCH_ADD_NAME As String = "SESSION_RISK_PREVENTION_ATTATCH_ADD_NAME"
    Private ReadOnly TMP_SEQ_BEGIN As Integer = 10000

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' txtSenderUserFullNameはEnable=Falseのため、Hidden変数から値を再設定する
        txtSenderUserFullName.Text = hdnSenderUserFullName.Value

        If Me.IsPostBack Then
            Return
        End If

        ' 画面を構築
        ' ⑤ プロセスラジオボタン(rdoProcess)
        Dim dt As DataTable
        dt = RiskPreventionEntity.GetProcess()
        rdoProcess.DataSource = dt
        rdoProcess.DataTextField = "PROCESS_NAME"
        rdoProcess.DataValueField = "PROCESS_NO"
        rdoProcess.DataBind()

        ' ⑤ 検討段階ラジオボタン(rdoDiscussPhase)
        dt = RiskPreventionEntity.GetDiscussPhase()
        rdoDiscussPhase.DataSource = dt
        rdoDiscussPhase.DataTextField = "DISCUSS_PHASE_NAME"
        rdoDiscussPhase.DataValueField = "DISCUSS_PHASE_NO"
        rdoDiscussPhase.DataBind()

        ' 画面タイトル(lblPageTitle)のdefault値を設定
        lblPageTitle.Text = "◆◆◆ リスク予防管理検討会 開催通知 兼 レビュー依頼書 ◆◆◆"

        ' ===================================================
        ' 業務処理開始
        ' ===================================================
        ' (10) 添付ファイルリンククリックイベント(lnkRiskPreventionAttatch(N)_Click)(URLの例:～/RiskPrevention.aspx?fid=5)
        If Not Request.QueryString("fid") Is Nothing Then

            Dim seq = CInt(Request.QueryString("fid").ToString())
            Dim hasError = False

            If seq < TMP_SEQ_BEGIN Then
                ' ファイルはDBに存在している場合
                Dim attathcDt As DataTable
                attathcDt = RiskPreventionEntity.GetRiskPreventionAttatch(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                                   Session(SessionComm.SESSION_SEQ_NO).ToString,
                                                                   Request.QueryString("fid").ToString())
                If attathcDt.Rows.Count > 0 Then
                    DownLoadFile(attathcDt.Rows(0).Item("ATTATCH_FILE_NAME").ToString, attathcDt.Rows(0).Item("ATTATCH_FILE"))
                Else
                    hasError = True
                End If

            Else

                ' ファイルは新規登録（SESSIONに格納）した場合
                Dim fileNames As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD_NAME)
                Dim files As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD)
                DownLoadFile(fileNames(seq - TMP_SEQ_BEGIN), files(seq - TMP_SEQ_BEGIN))

            End If

            If hasError Then
                ' ファイルが存在しません。削除された可能性があります
                Dim msg As String
                msg = MessageComm.GetMessageContext("1", "111", Nothing)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)

            End If
            Return
        End If

        ' ===================================================
        ' パラメータ値の取得し、SessionとHidden変数に設定
        ' ===================================================
        ' 隠し変数
        If Not Request.QueryString(SessionComm.PARA_RPPJ_NO) Is Nothing Then
            hdnRpPjNo.Value = Request.QueryString(SessionComm.PARA_RPPJ_NO).ToString()
            Session(SessionComm.SESSION_RP_PJ_NO) = Request.QueryString(SessionComm.PARA_RPPJ_NO).ToString()
        Else
            hdnRpPjNo.Value = Session(SessionComm.SESSION_RP_PJ_NO).ToString
        End If

        If Not Request.QueryString(SessionComm.PARA_SEQ_NO) Is Nothing Then
            hdnRpSeqNo.Value = Request.QueryString(SessionComm.PARA_SEQ_NO).ToString()
            Session(SessionComm.SESSION_SEQ_NO) = Request.QueryString(SessionComm.PARA_SEQ_NO).ToString()
        Else
            Session(SessionComm.SESSION_SEQ_NO) = ""
            hdnRpSeqNo.Value = Session(SessionComm.SESSION_SEQ_NO).ToString
        End If

        ' ===================================================
        ' 画面値の設定
        ' ===================================================
        If Session(SessionComm.SESSION_SEQ_NO) = "" Then
            Page_Load_New(sender, e)
        Else
            Page_Load_Update(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' ページロードイベント(Page_Load)　
    ''' ※新規登録（案件画面のリスク予防検討会登録ボタンから表示された）の場合
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load_New(sender As Object, e As EventArgs)

        Dim dt As DataTable

        ' 一時ファイルをクリア
        Session(SESSION_PREVENTION_ATTATCH_DELETE) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD_NAME) = New ArrayList()

        ' 起票者ラベル
        lblCreatedUserName.Text = Session(SessionComm.SESSION_USER_NAME)
        ' 最終更新者ラベル
        lblModifiedUserName.Text = ""

        ' ===================================================
        ' 0. 報告区分・リスクの有無
        ' ===================================================
        ' ① 報告区分ラジオボタン(rdoReportCategory)
        rdoReportCategory.ClearSelection()
        setRdoReportCategory()

        ' ② 確認日テキストボックス(txtCheckDate)
        txtCheckDate.Text = ""

        ' ===================================================
        ' 1. リスク予防管理検討会 対象工事
        ' ===================================================
        ' オーダ関連の情報を取得
        ' ① 工事名称テキストボックス(txtOrderNm)
        ' ② 顧客テキストボックス(txtCompyNm)
        txtOrderNm.Text = ""
        txtCompyNm.Text = ""

        If Session(SessionComm.SESSION_ORDER_CD) Is Nothing _
            OrElse String.IsNullOrEmpty(Session(SessionComm.SESSION_ORDER_CD).ToString) Then
            ' オーダがNullの案件
            dt = RiskPreventionEntity.GetProjectInfo(Session(SessionComm.SESSION_RP_PJ_NO).ToString)
            If dt.Rows.Count > 0 Then
                txtOrderNm.Text = dt.Rows(0).Item("PROJECT_NAME_TEMP").ToString
                txtCompyNm.Text = dt.Rows(0).Item("CUSTOMER_NAME").ToString
            End If
        Else
            ' オーダが存在の場合
            dt = RiskPreventionEntity.GetOrderInfo(Session(SessionComm.SESSION_ORDER_CD).ToString)
            If dt.Rows.Count > 0 Then
                txtOrderNm.Text = dt.Rows(0).Item("A01M009_ORDER_NM").ToString
                txtCompyNm.Text = dt.Rows(0).Item("A01M015_COMPY_NM").ToString
            End If
        End If

        ' ③ 担当部門テキストボックス(txtProductSectNm)
        txtProductSectNm.Text = Session(SessionComm.SESSION_RP_PRODUCT_SECT_NM).ToString

        ' ④ 顧客区分テキストボックス(txtCustomerTypeName)
        txtCustomerTypeName.Text = Session(SessionComm.SESSION_RP_CUSTOMER_TYPE).ToString

        ' ⑤ プロセスラジオボタン(rdoProcess)
        rdoProcess.SelectedValue = Session(SessionComm.SESSION_RP_PROCESS_NO).ToString
        hdnProcessName.Value = rdoProcess.SelectedItem.Text

        ' ⑥ 検討段階ラジオボタン(rdoDiscussPhase)
        rdoDiscussPhase.ClearSelection()
        hdnDiscussPhaseName.Value = ""

        ' ===================================================
        ' 2. リスク予防管理検討会 実施内容
        ' ===================================================
        ' ① 発信者テキストボックス(txtSenderUserFullName)
        hdnSenderUserFullName.Value = ""
        txtSenderUserFullName.Text = hdnSenderUserFullName.Value
        hdnUserCD.Value = ""
        hdnUserID.Value = ""

        If Not Session(SessionComm.SESSION_USER_CD) Is Nothing Then
            dt = RiskPreventionEntity.GetUserInfo(Session(SessionComm.SESSION_USER_CD).ToString)
            If dt.Rows.Count > 0 Then

                hdnSenderUserFullName.Value =
                        dt.Rows(0).Item("A01M010_FULLNAME").ToString + " " _
                    + dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString + " " _
                    + dt.Rows(0).Item("A01M003_POSTCLS_NM").ToString

                txtSenderUserFullName.Text = hdnSenderUserFullName.Value
                hdnUserCD.Value = dt.Rows(0).Item("A01M010_USER_CD").ToString
                hdnUserID.Value = dt.Rows(0).Item("A01M010_ID").ToString

            End If
        End If

        ' ② 開催日時テキストボックス(txtOpenDate)
        txtOpenDate.Text = ""

        ' ③ 開催時間テキストボックス(txtOpenTime)
        txtOpenTime.Text = ""

        ' ④ 開催回テキストボックス(txtOpenRound)
        txtOpenRound.Text = "1"

        ' ⑤ 開催場所テキストボックス(txtOpenPlace)
        txtOpenPlace.Text = ""

        ' ⑥ レビューポイントテキストボックス(txtReviewPoint)
        txtReviewPoint.Text = ""

        ' ⑦ レビュアー（出席予定者）テキストボックス(txtReviewerPlan)
        txtReviewerPlan.Text = ""

        ' 添付ファイル ファイル参照ボタン
        ' dt = RiskPreventionEntity.GetRiskPreventionAttatchInfo("", "")
        dt = GetRiskPreventionAttachNull()

        Dim column As DataColumn = New DataColumn("isEditAble")
        dt.Columns.Add(column)
        For Each t In dt.Rows
            t("isEditAble") = True
        Next

        grdRiskPreventionAttatch.DataSource = dt
        grdRiskPreventionAttatch.DataBind()
        Session(SESSION_PREVENTION_ATTATCH) = dt
        lnkRiskPreventionAttatch.Enabled = True
        If (dt.Rows.Count >= 10) Then
            lnkRiskPreventionAttatch.Visible = False
        Else
            lnkRiskPreventionAttatch.Visible = True
        End If

        ' ⑪ リスク・不安要素検討表ダウンロードボタン(btnRiskManagementListDl)
        '@PROJECT_NOにはSession変数RpPjNoの値をセットする
        '@PROCESS_NOにはSession変数RpProcessNoの値をセットする
        '@MANAGE_CATEGORYには0をセットする
        dt = RiskPreventionEntity.GetRiskManagement(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                    Session(SessionComm.SESSION_RP_PROCESS_NO).ToString,
                                                    "0")
        ' リスク・不安要素検討表ダウンロードボタン
        If dt.Rows.Count > 0 Then
            btnRiskManagementListDl.Enabled = True
        Else
            btnRiskManagementListDl.Enabled = False
        End If

        ' ⑫ 開催案内ボタン(btnOpenGuidanceCreate)
        btnOpenGuidanceCreate.Enabled = False

        ' ===================================================
        ' 3. 議事録
        ' ===================================================
        ' レビュアー（出席者）テキストボックス
        txtReviewer.Text = ""

        ' レビューコメントテキストボックス
        txtReviewRemark.Text = ""

        ' その他特記事項テキストボックス
        txtRemark.Text = ""

        ' ===================================================
        ' フッタ
        ' ===================================================
        ' 最終更新日時
        hdnModifiedOn.Value = ""

    End Sub


    ''' <summary>
    ''' ページロードイベント(Page_Load)　
    ''' ※登録済み案件の参照・修正（検索結果画面またはリスク予防検討会画面から表示された）の場合
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load_Update(sender As Object, e As EventArgs)

        Dim dt As DataTable
        Dim rowRiskPrevention As Object
        Dim isEditAble = False

        ' 一時ファイルをクリア
        Session(SESSION_PREVENTION_ATTATCH_DELETE) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD_NAME) = New ArrayList()

        ' ===================================================
        ' ヘッダ
        ' ===================================================
        ' ① 起票者ラベル(lblCreatedUserName)
        lblCreatedUserName.Text = ""

        ' ② 最終更新者ラベル(lblModifiedUserName)
        lblModifiedUserName.Text = ""

        dt = RiskPreventionEntity.GetRiskPrevention(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                            Session(SessionComm.SESSION_SEQ_NO).ToString)
        If dt.Rows.Count > 0 Then
            rowRiskPrevention = dt.Rows(0)
            isEditAble = editAble(rowRiskPrevention)
        Else
            Dim msg As String
            Dim tList As List(Of String) = New List(Of String)
            tList.Add(Session(SessionComm.SESSION_RP_PJ_NO).ToString)
            tList.Add(Session(SessionComm.SESSION_SEQ_NO).ToString)
            msg = MessageComm.GetMessageContext("1", "112", tList)

            ' 取得レコードが0件の場合は、画面上にメッセージ「案件番号：xxxxxxxxxx　枝番：xx のリスク予防・管理検討会情報を表示できません。削除された可能性があります」を表示し、
            ' ブランクのリスク予防検討会の画面を表示後、1つ前（呼出し元）の画面に戻る
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)
            Return

        End If

        Session(SessionComm.SESSION_ORDER_CD) = rowRiskPrevention("ORDER_CD").ToString

        ' ===================================================
        ' ヘッダ
        ' ===================================================
        ' ① 起票者ラベル(lblCreatedUserName)
        lblCreatedUserName.Text = rowRiskPrevention("CREATED_USER_NAME").ToString

        ' ② 最終更新者ラベル(lblModifiedUserName)
        lblModifiedUserName.Text = rowRiskPrevention("MODIFIED_USER_NAME").ToString

        ' ===================================================
        ' 0. 報告区分・リスクの有無
        ' ===================================================
        ' ① 報告区分ラジオボタン(rdoReportCategory)
        rdoReportCategory.SelectedValue = rowRiskPrevention("REPORT_CATEGORY").ToString
        setRdoReportCategory()
        rdoReportCategory.Enabled = isEditAble

        ' ② 確認日テキストボックス(txtCheckDate)
        txtCheckDate.Text = rowRiskPrevention("CHECK_DATE").ToString
        txtCheckDate.Enabled = isEditAble

        ' ===================================================
        ' 1. リスク予防管理検討会 対象工事
        ' ===================================================

        ' ③ 担当部門テキストボックス(txtProductSectNm)
        txtProductSectNm.Text = rowRiskPrevention("PRODUCT_SECT_NM").ToString

        ' ④ 顧客区分テキストボックス(txtCustomerTypeName)
        txtCustomerTypeName.Text = rowRiskPrevention("CUSTOMER_TYPE_NAME").ToString

        ' ⑤ プロセスラジオボタン(rdoProcess)
        rdoProcess.SelectedValue = rowRiskPrevention("PROCESS_NO").ToString
        hdnProcessName.Value = rdoProcess.SelectedItem.Text
        rdoProcess.Enabled = isEditAble

        ' ⑥ 検討段階ラジオボタン(rdoDiscussPhase)
        rdoDiscussPhase.SelectedValue = rowRiskPrevention("DISCUSS_PHASE_NO").ToString
        hdnDiscussPhaseName.Value = rdoDiscussPhase.SelectedItem.Text
        rdoDiscussPhase.Enabled = isEditAble

        '① 工事名称テキストボックス(txtOrderNm)	
        '② 顧客テキストボックス(txtCompyNm)
        If IsDBNull(rowRiskPrevention("ORDER_CD")) _
            OrElse String.IsNullOrEmpty(rowRiskPrevention("ORDER_CD").ToString) Then

            ' オーダがNullの案件
            txtOrderNm.Text = rowRiskPrevention("PROJECT_NAME_TEMP").ToString
            txtCompyNm.Text = rowRiskPrevention("CUSTOMER_NAME").ToString
        Else

            ' オーダが存在の場合
            dt = RiskPreventionEntity.GetOrderInfo(rowRiskPrevention("ORDER_CD").ToString)
            If dt.Rows.Count > 0 Then
                txtOrderNm.Text = dt.Rows(0).Item("A01M009_ORDER_NM").ToString
                txtCompyNm.Text = dt.Rows(0).Item("A01M015_COMPY_NM").ToString
            End If
        End If

        ' ===================================================
        ' 2. リスク予防管理検討会 実施内容
        ' ===================================================
        ' ① 発信者テキストボックス(txtSenderUserFullName)
        hdnSenderUserFullName.Value = rowRiskPrevention("SENDER_USER_FULLNAME").ToString
        txtSenderUserFullName.Text = hdnSenderUserFullName.Value
        txtSenderUserFullName.Enabled = False
        hdnUserCD.Value = rowRiskPrevention("SENDER_USER_CD").ToString
        hdnUserID.Value = rowRiskPrevention("SENDER_USER_ID").ToString

        ' 発信選択アイコン(imgSenderUserSearch)
        imgSenderUserSearch.Enabled = isEditAble
        imgSenderUserClear.Enabled = isEditAble

        ' ② 開催日テキストボックス(txtOpenDate)
        If String.IsNullOrEmpty(rowRiskPrevention("OPEN_DATE").ToString) Then
            txtOpenDate.Text = ""
        Else
            Dim dtOpenDate As Date = Today
            If Date.TryParse(rowRiskPrevention("OPEN_DATE").ToString, dtOpenDate) Then
                requestedDeliveryDateCalendar.SelectedDate = dtOpenDate
                txtOpenDate.Text = dtOpenDate.ToLongDateString
            End If
        End If

        txtOpenDate.Enabled = isEditAble
        ' 開催日選択カレンダー(imgOpenDateCalendar)
        imgOpenDateCalendar.Enabled = isEditAble

        ' ③ 開催時間テキストボックス(txtOpenTime)
        txtOpenTime.Text = rowRiskPrevention("OPEN_TIME").ToString
        txtOpenTime.Enabled = isEditAble

        ' ④ 開催回テキストボックス(txtOpenRound)
        txtOpenRound.Text = rowRiskPrevention("OPEN_ROUND").ToString
        txtOpenRound.Enabled = isEditAble

        ' ⑤ 開催場所テキストボックス(txtOpenPlace)
        txtOpenPlace.Text = rowRiskPrevention("OPEN_PLACE").ToString
        txtOpenPlace.Enabled = isEditAble

        ' ⑥ レビューポイントテキストボックス(txtReviewPoint)
        txtReviewPoint.Text = rowRiskPrevention("REVIEW_POINT").ToString
        txtReviewPoint.Enabled = isEditAble

        ' ⑦ レビュアー（出席予定者）テキストボックス(txtReviewerPlan)
        txtReviewerPlan.Text = rowRiskPrevention("REVIEWER_PLAN").ToString
        txtReviewerPlan.Enabled = isEditAble


        ' ⑧ 添付ファイル(lnkRiskPreventionAttatch(N),btnRiskPreventionAttatchDel(N),btnRiskPreventionAttatchAdd)
        dt = RiskPreventionEntity.GetRiskPreventionAttatchInfo(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                           Session(SessionComm.SESSION_SEQ_NO).ToString)

        Dim column As DataColumn = New DataColumn("isEditAble")
        dt.Columns.Add(column)
        For Each t In dt.Rows
            t("isEditAble") = isEditAble.ToString
        Next

        grdRiskPreventionAttatch.DataSource = dt
        grdRiskPreventionAttatch.DataBind()
        Session(SESSION_PREVENTION_ATTATCH) = dt
        lnkRiskPreventionAttatch.Enabled = isEditAble
        If (dt.Rows.Count >= 10) Then
            lnkRiskPreventionAttatch.Visible = False
        Else
            lnkRiskPreventionAttatch.Visible = True
        End If

        ' ⑨ リスク・不安要素検討表ダウンロードボタン(btnRiskManagementListDl)
        '@PROJECT_NOにはSession変数RpPjNoの値をセットする
        '@PROCESS_NOにはRISK_PREVENTION.PROCESS_NOの値をセットする
        '@MANAGE_CATEGORYには0をセットする
        dt = RiskPreventionEntity.GetRiskManagement(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                    rowRiskPrevention("PROCESS_NO").ToString,
                                                    "0")
        ' リスク・不安要素検討表ダウンロードボタン
        If dt.Rows.Count > 0 And isEditAble Then
            btnRiskManagementListDl.Enabled = True
        Else
            btnRiskManagementListDl.Enabled = False
        End If

        ' ⑩ 開催案内ボタン(btnOpenGuidanceCreate)
        btnOpenGuidanceCreate.Enabled = isEditAble

        ' ===================================================
        ' 3. 議事録
        ' ===================================================
        ' レビュアー（出席者）テキストボックス(txtReviewer)
        txtReviewer.Text = rowRiskPrevention("REVIEWER").ToString
        txtReviewer.Enabled = isEditAble

        ' レビューコメントテキストボックス(txtReviewRemark)
        txtReviewRemark.Text = rowRiskPrevention("REVIEW_REMARK").ToString
        txtReviewRemark.Enabled = isEditAble

        'その他特記事項テキストボックス(txtRemark)
        txtRemark.Text = rowRiskPrevention("REMARK").ToString
        txtRemark.Enabled = isEditAble

        ' ===================================================
        ' フッタ
        ' ===================================================
        ' ① リスク予防検討会登録ボタン(btnRiskPreventionInput)	
        ' 	アクセス権が参照の場合は、btnRiskPreventionInputのEnabledプロパティをFalseにセットする
        btnRiskPreventionInput.Enabled = isEditAble

        ' ② リスク予防検討会削除ボタン(btnRiskPreventionDel)	
        ' アクセス権が参照の場合は、btnRiskPreventionDelのEnabledプロパティをFalseにセットする
        btnRiskPreventionDel.Enabled = isEditAble

        ' ③ 最終更新日時隠し項目(hdnModifiedOn)	
        hdnModifiedOn.Value = rowRiskPrevention("MODIFIED_ON").ToString

        ' 排他チェックはデフォールトは行わない
        hdnIfOverwrite.Value = "0"

    End Sub

    ''' <summary>
    ''' 報告区分ラジオボタン選択イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rdoReportCategory_CheckedChanged(sender As Object, e As EventArgs) Handles rdoReportCategory.SelectedIndexChanged
        setRdoReportCategory()
    End Sub

    ''' <summary>
    ''' プロセスラジオボタン選択イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rdoProcess_CheckedChanged(sender As Object, e As EventArgs) Handles rdoProcess.SelectedIndexChanged
        ' リスク・不安要素検討表ダウンロードボタン
        '@PROJECT_NOにはSession変数RpPjNoの値をセットする
        '@PROCESS_NOにはSession変数RpProcessNoの値をセットする
        '@MANAGE_CATEGORYには0をセットする
        Dim dt As DataTable = RiskPreventionEntity.GetRiskManagement(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                                     rdoProcess.SelectedValue,
                                                                     "0")
        '取得レコードが0件の場合
        'リスク・不安要素検討表ダウンロードボタン(btnRiskManagementListDl)のEnabledプロパティをFalseに設定する
        '取得レコードが1件の場合
        'リスク・不安要素検討表ダウンロードボタン(btnRiskManagementListDl)のEnabledプロパティをTrueに設定する
        If dt.Rows.Count > 0 Then
            btnRiskManagementListDl.Enabled = True
        Else
            btnRiskManagementListDl.Enabled = False
        End If

        hdnProcessName.Value = rdoProcess.SelectedItem.Text

    End Sub


    ''' <summary>
    ''' 検討段階オボタン選択イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub rdoDiscussPhase_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rdoDiscussPhase.SelectedIndexChanged
        hdnDiscussPhaseName.Value = rdoDiscussPhase.SelectedItem.Text
    End Sub


    ''' <summary>
    ''' リスク・不安要素検討表ダウンロードボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRiskManagementListDl_Click(sender As Object, e As EventArgs) Handles btnRiskManagementListDl.Click

        Dim dt As DataTable = RiskPreventionEntity.GetRiskManagement(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                                    rdoProcess.SelectedValue,
                                                                    "0")

        If dt.Rows.Count > 0 Then

            DownLoadFile(dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString, dt.Rows(0).Item("ATTATCH_FILE"))
        Else

            ' ファイルが存在しません。削除された可能性があります
            Dim msg As String
            msg = MessageComm.GetMessageContext("1", "111", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)
            btnRiskManagementListDl.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' リスク予防検討会登録ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRiskPreventionInput_Click(sender As Object, e As EventArgs) Handles btnRiskPreventionInput.Click

        Dim dtOpenDate As Date
        Dim msg As String = ""
        Dim strSeqNo As String

        ' txtSenderUserFullNameはEnable=Falseのため、Hidden変数から値を再設定する
        txtSenderUserFullName.Text = hdnSenderUserFullName.Value

        Dim hasError = False
        ' 報告区分ラジオボタン(rdoReportCategory)
        ' いずれの選択肢も未チェックの場合は、メッセージ「報告区分を選択してください」を表示して処理を中断する
        If rdoReportCategory.SelectedIndex < 0 Then
            msg = MessageComm.GetMessageContext("1", "101", Nothing)
            hasError = True
        End If


        ' プロセスラジオボタン(rdoProcess)	
        ' いずれの選択肢も未チェックの場合は、メッセージ「プロセスを選択してください」を表示して処理を中断する
        If rdoProcess.SelectedIndex < 0 Then
            If hasError Then
                msg += "\n"
            End If
            msg += MessageComm.GetMessageContext("1", "102", Nothing)
            hasError = True

        End If

        ' 検討段階ラジオボタン(rdoDiscussPhase)	
        ' いずれの選択肢も未チェックの場合は、メッセージ「検討段階を選択してください」を表示して処理を中断する
        If rdoDiscussPhase.SelectedIndex < 0 Then
            If hasError Then
                msg += "\n"
            End If
            msg += MessageComm.GetMessageContext("1", "103", Nothing)
            hasError = True

        End If

        ' 開催日テキストボックス(txtOpenDate)	
        ' フォーマットは不正の場合、メッセージを表示し、処理を中断する
        If Not String.IsNullOrEmpty(txtOpenDate.Text) Then
            If Not Date.TryParse(txtOpenDate.Text, dtOpenDate) Then
                If hasError Then
                    msg += "\n"
                End If
                msg += MessageComm.GetMessageContext("1", "104", Nothing)
                hasError = True
            End If
        End If

        ' 開催回テキストボックス(txtOpenRound)	
        ' 数値ではない場合、メッセージを表示し、処理を中断する
        If Not String.IsNullOrEmpty(txtOpenRound.Text) Then
            If Not IsNumeric(txtOpenRound.Text) Then
                If hasError Then
                    msg += "\n"
                End If
                msg += MessageComm.GetMessageContext("1", "105", Nothing)
                hasError = True

            ElseIf InStr(txtOpenRound.Text, ".") Then
                If hasError Then
                    msg += "\n"
                End If
                msg += MessageComm.GetMessageContext("1", "105", Nothing)
                hasError = True

            ElseIf InStr(txtOpenRound.Text, "-") Then
                If hasError Then
                    msg += "\n"
                End If
                msg += MessageComm.GetMessageContext("1", "105", Nothing)
                hasError = True

            End If
        End If

        Dim tList As List(Of String) = New List(Of String)
        ' レビューポイントテキストボックス(txtReviewPoint)	
        If Not String.IsNullOrEmpty(txtReviewPoint.Text) Then

            If txtReviewPoint.Text.Length > txtReviewPoint.MaxLength Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("レビューポイント")
                tList.Add(txtReviewPoint.MaxLength.ToString)

                msg += MessageComm.GetMessageContext("1", "114", tList)
                hasError = True
            End If
        End If

        ' レビュアー（出席予定者）(txtReviewerPlan)	
        If Not String.IsNullOrEmpty(txtReviewerPlan.Text) Then

            If txtReviewerPlan.Text.Length > txtReviewerPlan.MaxLength Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("レビュアー（出席予定者）")
                tList.Add(txtReviewerPlan.MaxLength.ToString)

                msg += MessageComm.GetMessageContext("1", "114", tList)
                hasError = True
            End If
        End If

        ' レビュアー（出席者）(txtReviewer)	
        If Not String.IsNullOrEmpty(txtReviewer.Text) Then

            If txtReviewer.Text.Length > txtReviewer.MaxLength Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("レビュアー（出席者）")
                tList.Add(txtReviewer.MaxLength.ToString)

                msg += MessageComm.GetMessageContext("1", "114", tList)
                hasError = True
            End If
        End If

        ' レビューコメント(txtReviewRemark)	
        If Not String.IsNullOrEmpty(txtReviewRemark.Text) Then

            If txtReviewRemark.Text.Length > txtReviewRemark.MaxLength Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("レビューコメント")
                tList.Add(txtReviewRemark.MaxLength.ToString)

                msg += MessageComm.GetMessageContext("1", "114", tList)
                hasError = True
            End If
        End If

        ' その他特記事項(txtRemark)	
        If Not String.IsNullOrEmpty(txtRemark.Text) Then

            If txtRemark.Text.Length > txtRemark.MaxLength Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("その他特記事項")
                tList.Add(txtRemark.MaxLength.ToString)

                msg += MessageComm.GetMessageContext("1", "114", tList)
                hasError = True
            End If
        End If

        ' 新規追加したファイルの取得
        Dim fileNames As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD_NAME)
        Dim files As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD)

        Dim i = 0
        For i = 0 To fileNames.Count - 1
            Dim tmpFileName = System.IO.Path.GetFileName(fileNames(i))
            If tmpFileName.Length > 50 Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("添付ファイル名(" + tmpFileName + ")")
                tList.Add("50")

                msg += MessageComm.GetMessageContext("1", "114", tList)
                hasError = True
            End If
        Next

        ' エラーがある場合、メッセージを表示し、処理を中断する
        If hasError Then
            msg = msg.Replace("'", "\'")
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)
            Return
        End If

        Try
            ' リスク予防検討会登録ボタンクリックイベント(btnRiskPreventionInput_Click)　※新規登録の場合
            ' ※案件のINSERTかUPDATEかの判定は、Session変数RpSeqNoに値がセットされているか(->UPDATE)、いないか(->INSERT)で判定する
            If Session(SessionComm.SESSION_SEQ_NO) = "" Then

                strSeqNo = RiskPreventionEntity.InsertRistPrevention(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                          rdoReportCategory.SelectedValue,
                                                          txtCheckDate.Text,
                                                          rdoProcess.SelectedValue,
                                                          rdoDiscussPhase.SelectedValue,
                                                          hdnUserID.Value,
                                                          hdnUserCD.Value,
                                                          txtSenderUserFullName.Text,
                                                          txtOpenDate.Text,
                                                          txtOpenTime.Text,
                                                          txtOpenRound.Text,
                                                          txtOpenPlace.Text,
                                                          txtReviewPoint.Text,
                                                          txtReviewerPlan.Text,
                                                          txtReviewer.Text,
                                                          txtReviewRemark.Text,
                                                          txtRemark.Text,
                                                          Session(SessionComm.SESSION_USER_CD).ToString,
                                                          Session(SessionComm.SESSION_USER_ID).ToString,
                                                          Session(SessionComm.SESSION_USER_NAME).ToString,
                                                          fileNames,
                                                          files,
                                                          Session(SessionComm.SESSION_USER_CD).ToString)

            Else

                ' リスク予防検討会登録ボタンクリックイベント(btnRiskPreventionInput_Click)　※修正登録(UPDATE)の場合
                ' ※案件のINSERTかUPDATEかの判定は、Session変数RpSeqNoに値がセットされているか(->UPDATE)、いないか(->INSERT)で判定する
                Dim dt As DataTable

                ' DB内の案件の最終更新日時を取得する
                dt = RiskPreventionEntity.GetRiskPrevention(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                            Session(SessionComm.SESSION_SEQ_NO).ToString)

                ' SQLで取得したMODIFIED_ONの値と最終更新日時隠し項目(hdnModifiedOn)の値を比較する
                If dt.Rows.Count > 0 Then
                    ' 排他エラー「当該リスク予防検討会の情報はすでに他のユーザによって更新されています。このまま登録処理を続けますか？」
                    ' の確認画面で、OKが選択された場合は排他チェックを外す
                    If hdnIfOverwrite.Value <> "1" And hdnModifiedOn.Value <> dt.Rows(0).Item("MODIFIED_ON").ToString Then
                        '比較した値が異なる場合は以下の確認メッセージを表示する
                        '「当該リスク予防検討会の情報はすでに他のユーザによって更新されています。このまま登録処理を続けますか？」
                        msg = MessageComm.GetMessageContext("1", "110", Nothing)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "overWriteConfirm('" & msg & "');", True)
                        Return
                    End If
                Else
                    ' リスク予防・管理検討会情報はすでに他のユーザによって削除されています。案件検索画面に戻ります
                    msg = MessageComm.GetMessageContext("1", "113", Nothing)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "updateError('" & msg & "');", True)
                    Return
                End If

                ' リスク予防検討会のUPDATE
                strSeqNo = RiskPreventionEntity.UpdateRistPrevention(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                          Session(SessionComm.SESSION_SEQ_NO).ToString,
                                                          rdoReportCategory.SelectedValue,
                                                          txtCheckDate.Text,
                                                          rdoProcess.SelectedValue,
                                                          rdoDiscussPhase.SelectedValue,
                                                          hdnUserID.Value,
                                                          hdnUserCD.Value,
                                                          txtSenderUserFullName.Text,
                                                          txtOpenDate.Text,
                                                          txtOpenTime.Text,
                                                          txtOpenRound.Text,
                                                          txtOpenPlace.Text,
                                                          txtReviewPoint.Text,
                                                          txtReviewerPlan.Text,
                                                          txtReviewer.Text,
                                                          txtReviewRemark.Text,
                                                          txtRemark.Text,
                                                          Session(SessionComm.SESSION_USER_CD).ToString,
                                                          Session(SessionComm.SESSION_USER_ID).ToString,
                                                          Session(SessionComm.SESSION_USER_NAME).ToString,
                                                          Session(SESSION_PREVENTION_ATTATCH_DELETE),
                                                          fileNames,
                                                          files,
                                                          Session(SessionComm.SESSION_USER_CD).ToString)
            End If
        Catch ex As Exception

            ' UPDATE処理でエラーが発生した場合、エラーログを出力する
            GetLogger().Error(ex.ToString)

            ' UPDATE処理でエラーが発生した場合はUPDATE発行をロールバックし、
            ' 画面上にメッセージ「リスク予防・管理検討会の登録でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            msg = MessageComm.GetMessageContext("1", "108", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)
            Return
        End Try

        ' 画面上にメッセージ「リスク予防・管理検討会の登録が完了しました。案件番号：xxxxxxxxxx　枝番：xx」を表示する。
        tList = New List(Of String)
        tList.Add(Session(SessionComm.SESSION_RP_PJ_NO).ToString)
        tList.Add(strSeqNo)
        msg = MessageComm.GetMessageContext("1", "106", tList)
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)

        ' 画面を更新する
        hdnRpSeqNo.Value = strSeqNo
        Session(SessionComm.SESSION_SEQ_NO) = strSeqNo

        ' 一時ファイルをクリア
        Session(SESSION_PREVENTION_ATTATCH_DELETE) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD_NAME) = New ArrayList()

        Page_Load_Update(sender, e)

    End Sub


    ''' <summary>
    ''' リスク予防検討会削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRiskPreventionDel_Click(sender As Object, e As EventArgs) Handles btnRiskPreventionDel.Click

        Try
            ' Session変数RpSeqNoの値がNULL・ブランクの場合
            ' "(1) ページロードイベント(Page_Load)　※新規登録（案件画面のリスク予防検討会登録ボタンから表示された）の場合"を実行する
            If Session(SessionComm.SESSION_SEQ_NO) = "" Then
                Page_Load_New(sender, e)
            Else
                RiskPreventionEntity.DeleteRistPrevention(Session(SessionComm.SESSION_RP_PJ_NO).ToString,
                                                          Session(SessionComm.SESSION_SEQ_NO).ToString,
                                                          Session(SessionComm.SESSION_USER_CD).ToString)

                ' 画面上にメッセージ「リスク予防・管理検討会の削除が完了しました。案件番号：xxxxxxxxxx　枝番：xx」を表示する。
                Dim msg As String
                Dim tList As List(Of String) = New List(Of String)
                tList.Add(Session(SessionComm.SESSION_RP_PJ_NO).ToString)
                tList.Add(Session(SessionComm.SESSION_SEQ_NO).ToString)
                msg = MessageComm.GetMessageContext("1", "107", tList)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "deleteEnd('" & msg & "');", True)
            End If

        Catch ex As Exception
            'DELETE発行処理でエラーが発生した場合、エラーログを出力する
            GetLogger().Error(ex.ToString)

            ' DELETE発行処理でエラーが発生した場合はSQL発行をロールバックし、
            ' 画面上にメッセージ「リスク予防・管理検討会の削除でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            Dim msg = MessageComm.GetMessageContext("1", "109", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)
        End Try

    End Sub

    ''' <summary>
    ''' 添付ファイル操作イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub grdRiskPreventionAttatch_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdRiskPreventionAttatch.RowCommand

        ' 添付ファイル削除ボタンクリックイベント
        If e.CommandName = "del" Then

            Dim seq = CInt(e.CommandArgument.ToString())

            If seq < TMP_SEQ_BEGIN Then
                ' ファイルはDBに保存している場合

                ' 選択したファイルをSESSIONに保存した添付ファイル一覧から削除
                Dim dt As DataTable = Session(SESSION_PREVENTION_ATTATCH)
                For Each row In dt.Rows
                    If row("FILE_SEQ_NO").ToString = e.CommandArgument.ToString() Then
                        dt.Rows.Remove(row)
                        Exit For
                    End If
                Next
                Session(SESSION_PREVENTION_ATTATCH) = dt

                ' 削除したファイルをSESSIONに記憶しておく
                Dim delList As ArrayList = Session(SESSION_PREVENTION_ATTATCH_DELETE)
                delList.Add(e.CommandArgument.ToString())
                Session(SESSION_PREVENTION_ATTATCH_DELETE) = delList
            Else
                ' ファイルは新規追加したファイル（SESSIONに格納）の場合

                Dim fileNames As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD_NAME)
                Dim files As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD)

                fileNames.RemoveAt(seq - TMP_SEQ_BEGIN)
                files.RemoveAt(seq - TMP_SEQ_BEGIN)

            End If

            ' 画面を更新
            Dim dtAttatch As DataTable = GetAttatch()
            grdRiskPreventionAttatch.DataSource = dtAttatch
            grdRiskPreventionAttatch.DataBind()

            If (dtAttatch.Rows.Count >= 10) Then
                lnkRiskPreventionAttatch.Visible = False
            Else
                lnkRiskPreventionAttatch.Visible = True
            End If

        End If
    End Sub

    ''' <summary>
    ''' 開催日時カレンダーアイコンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub imgOpenDateCalendar_Click(sender As Object, e As ImageClickEventArgs) Handles imgOpenDateCalendar.Click
        calendar.Visible = Not calendar.Visible
        Dim dtOpenDate As Date = Today
        If Not Date.TryParse(txtOpenDate.Text, dtOpenDate) Then
        Else
            requestedDeliveryDateCalendar.SelectedDate = dtOpenDate
        End If
    End Sub

    ''' <summary>
    ''' 開催日時カレンダーにて年月日が選択した場合イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub requestedDeliveryDateCalendar_SelectionChanged(sender As Object, e As EventArgs) Handles requestedDeliveryDateCalendar.SelectionChanged

        txtOpenDate.Text = requestedDeliveryDateCalendar.SelectedDate.ToLongDateString()
        calendar.Visible = False
        txtOpenDate.Focus()
    End Sub


    ''' <summary>
    ''' 報告区分ラジオボタン選択された場合、画面タイトル等の変更処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setRdoReportCategory()

        Select Case rdoReportCategory.SelectedValue
            Case "開催通知" ' 選択肢「開催通知 兼 レビュー依頼書」が選択された場合

                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ リスク予防管理検討会 開催通知 兼 レビュー依頼書 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ リスク予防管理検討会 開催通知 兼 レビュー依頼書 ◆◆◆"

                ' 2. リスク予防管理検討会 実施内容のすべての行を表示する
                tab2.Visible = True

                ' 3. 議事録の(1) 出席者 レビュアー行・(2) レビューコメント行を表示する
                trReviewerHeader.Visible = True
                trReviewerDetail.Visible = True
                trReviewRemarkHeader.Visible = True
                trReviewRemarkDetail.Visible = True

            Case "議事録" ' 選択肢「議事録」が選択された場合

                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ リスク予防管理検討会 議事録 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ リスク予防管理検討会 議事録 ◆◆◆"

                ' 2. リスク予防管理検討会 実施内容のすべての行を表示する
                tab2.Visible = True

                ' 3. 議事録の(1) 出席者 レビュアー行・(2) レビューコメント行を表示する
                trReviewerHeader.Visible = True
                trReviewerDetail.Visible = True
                trReviewRemarkHeader.Visible = True
                trReviewRemarkDetail.Visible = True

            Case "リスクなし" ' 選択肢「リスクなしと判断」が選択された場合

                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ リスク予防管理結果報告 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ リスク予防管理結果報告 ◆◆◆"

                ' 2. リスク予防管理検討会 実施内容のすべての行を非表示にする
                tab2.Visible = False

                ' 3. 議事録の(1) 出席者 レビュアー行・(2) レビューコメント行を非表示にする（(3) その他特記事項行は非表示にしない）
                trReviewerHeader.Visible = False
                trReviewerDetail.Visible = False
                trReviewRemarkHeader.Visible = False
                trReviewRemarkDetail.Visible = False

            Case Else
                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ リスク予防管理検討会 開催通知 兼 レビュー依頼書 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ リスク予防管理検討会 開催通知 兼 レビュー依頼書 ◆◆◆"

                ' 2. リスク予防管理検討会 実施内容のすべての行を表示する
                tab2.Visible = True

                ' 3. 議事録の(1) 出席者 レビュアー行・(2) レビューコメント行を表示する
                trReviewerHeader.Visible = True
                trReviewerDetail.Visible = True
                trReviewRemarkHeader.Visible = True
                trReviewRemarkDetail.Visible = True
        End Select

    End Sub

    ''' <summary>
    ''' アクセス権を判断する
    ''' </summary>
    ''' <param name="rowRiskPrevention"></param>
    ''' <returns>アクセス権：True:編集,False:参照</returns>
    ''' <remarks></remarks>
    Private Function editAble(rowRiskPrevention As Object) As Boolean

        Dim a01m01SectCd = rowRiskPrevention("A01M010_SECT_CD").ToString
        Dim createdUserId = rowRiskPrevention("CREATED_USER_ID").ToString
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

    Protected Sub lnkRiskPreventionAttatch_UploadedComplete(sender As Object, e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles lnkRiskPreventionAttatch.UploadedComplete
        ' ファイルがアップロード完了した場合、情報をSESSIONに格納
        If lnkRiskPreventionAttatch.HasFile Then
            Dim fileNames As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD_NAME)
            Dim files As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD)

            fileNames.Add(lnkRiskPreventionAttatch.FileName())
            files.Add(lnkRiskPreventionAttatch.FileBytes)

            Session(SESSION_PREVENTION_ATTATCH_ADD_NAME) = fileNames
            Session(SESSION_PREVENTION_ATTATCH_ADD) = files
        End If

    End Sub

    ''' <summary>
    ''' ファイルアップロード完了で、画面を再更新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lnkUpdate_Click(sender As Object, e As EventArgs) Handles lnkUpdate.Click

        ' 画面を再更新
        Dim dtAttatch As DataTable = GetAttatch()
        grdRiskPreventionAttatch.DataSource = dtAttatch
        grdRiskPreventionAttatch.DataBind()

        If (dtAttatch.Rows.Count >= 10) Then
            lnkRiskPreventionAttatch.Visible = False
            txtReviewer.Focus()
        Else
            lnkRiskPreventionAttatch.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' SESSIONに格納したファイルを一覧を作成し、画面に設定する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function GetAttatch() As DataTable

        Dim dt As DataTable = Session(SESSION_PREVENTION_ATTATCH)
        dt = dt.Copy

        Dim fileNames As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD_NAME)
        Dim files As ArrayList = Session(SESSION_PREVENTION_ATTATCH_ADD)

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
    ''' ファイルダウンロード
    ''' </summary>
    ''' <param name="fileByte">ファイル</param>
    ''' <remarks></remarks>
    Private Sub DownLoadFile(ByRef fileNm As String, ByRef fileByte As Byte())

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
        HttpContext.Current.Response.Clear()
        HttpContext.Current.ApplicationInstance.CompleteRequest()

    End Sub

    ''' <summary>
    ''' Log対象のインスタンスを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLogger() As log4net.ILog

        Return log4net.LogManager.GetLogger("RiskPrevention")

    End Function

End Class