Imports System.Threading
Imports System.IO

Public Class QualityProgression
    Inherits System.Web.UI.Page

    Private ReadOnly SESSION_PREVENTION_ATTATCH As String = "SESSION_PREVENTION_ATTATCH"
    Private ReadOnly SESSION_PREVENTION_ATTATCH_ADD As String = "SESSION_PREVENTION_ATTATCH_ADD"
    Private ReadOnly SESSION_PREVENTION_ATTATCH_ADD_NAME As String = "SESSION_PREVENTION_ATTATCH_ADD_NAME"
    Private ReadOnly SESSION_PREVENTION_ATTATCH_DELETE As String = "SESSION_PREVENTION_ATTATCH_DELETE"
    Private ReadOnly SESSION_PREVENTION_INCOMPLETE_PJ As String = "SESSION_PREVENTION_INCOMPLETE_PJ"
    Private ReadOnly INTENT As String = Chr(10) & " 　　　　　　　　　　　　　　　　　"
    Private ReadOnly TMP_SEQ_BEGIN As Integer = 10000

    ''' <summary>
    ''' ページロード
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' txtSenderUserNameはEnable=Falseのため、Hidden変数から値を再設定する
        txtSenderUserName.Text = hdnSenderUserName.Value

        If Me.IsPostBack Then
            Return
        End If

        ' 画面タイトル(lblPageTitle)のdefault値を設定
        lblPageTitle.Text = "◆◆◆ 品質推進会 開催通知 ◆◆◆"

        ' ===================================================
        ' 業務処理開始
        ' ===================================================
        ' (10) 添付ファイルリンククリックイベント(lnkRiskPreventionAttatch(N)_Click)(URLの例:～/QualityProgression.aspx?fid=5)
        If Not Request.QueryString("fid") Is Nothing Then

            Dim seq = CInt(Request.QueryString("fid").ToString())

            Dim hasError = False

            If seq < TMP_SEQ_BEGIN Then

                ' ファイルはDBに存在している場合
                Dim attathcDt As DataTable
                attathcDt = QualityProgressionEntity.GetPreventionAttatch(Session(SessionComm.SESSION_QPNO).ToString,
                                                      Request.QueryString("fid").ToString())
                If attathcDt.Rows.Count > 0 Then

                    DownLoadFile(attathcDt.Rows(0).Item("ATTATCH_FILE_NAME").ToString, attathcDt.Rows(0).Item("ATTATCH_FILE"))
                Else
                    hasError = True
                End If

            Else

                ' ファイルは新規登録した（SESSIONに格納）場合
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

        ' (5) リスク・不安要素検討表ダウンロードボタンクリックイベント(btnRiskManagementListDl_Click)(URLの例:～/QualityProgression.aspx?fid2=5&process=1)
        If Not Request.QueryString("fid2") Is Nothing Then
            Dim attathcDt As DataTable
            attathcDt = QualityProgressionEntity.GetRiskManagement(Request.QueryString("fid2").ToString(),
                                                                   Request.QueryString("process").ToString(),
                                                                   "0")

            If attathcDt.Rows.Count > 0 Then

                DownLoadFile(attathcDt.Rows(0).Item("ATTATCH_FILE_NAME").ToString, attathcDt.Rows(0).Item("ATTATCH_FILE"))

            Else
                ' ファイルが存在しません。削除された可能性があります
                Dim msg As String
                msg = MessageComm.GetMessageContext("1", "111", Nothing)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)

            End If

            Return
        End If

        ' ===================================================
        ' パラメータ値の取得し、SessionとHidden変数に設定
        ' ===================================================
        ' 隠し変数
        If Not Request.QueryString(SessionComm.PARA_QPNO) Is Nothing Then
            Session(SessionComm.SESSION_QPNO) = Request.QueryString(SessionComm.PARA_QPNO).ToString()
            hdnQpNo.Value = Session(SessionComm.SESSION_QPNO).ToString
        Else
            Session(SessionComm.SESSION_QPNO) = ""
            hdnQpNo.Value = Session(SessionComm.SESSION_QPNO).ToString
        End If

        ' ===================================================
        ' 画面値の設定
        ' ===================================================
        If Session(SessionComm.SESSION_QPNO) = "" Then
            Page_Load_New(sender, e)
        Else
            Page_Load_Update(sender, e)
        End If

    End Sub

    ''' <summary>
    ''' ページロードイベント(Page_Load)　
    ''' ※新規登録（メニュー画面の品質推進会登録ボタンから表示された）の場合
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

        ' ===================================================
        ' ヘッダ
        ' =================================================== 
        ' 起票者ラベル
        lblCreatedUserName.Text = Session(SessionComm.SESSION_USER_NAME)

        ' 最終更新者ラベル
        lblModifiedUserName.Text = ""

        ' ===================================================
        ' 0. 報告区分
        ' ===================================================
        ' ① 報告区分ラジオボタン(rdoReportCategory)
        rdoReportCategory.ClearSelection()
        setRdoReportCategory()

        ' ===================================================
        ' 1. 品質推進会 実施部門
        ' ===================================================

        ' 初期値を設定
        ' ① 支社名テキストボックス(txtBranchName)
        txtBranchName.Text = ""

        ' ② 対象部門テキストボックス(txtTargetSectName)
        txtTargetSectName.Text = ""
        hdnTargetSectCD.Value = ""

        ' ③ 開催担当者テキストボックス(txtOpenerUserName)
        txtOpenerUserName.Text = ""
        hdnOpenerUserID.Value = ""
        hdnOpenerUserCD.Value = ""

        ' ④ 顧客区分テキストボックス(txtCustomerTypeName)
        txtConferenceName.Text = ""

        ' ===================================================
        ' 2. リスク予防管理検討会 実施内容
        ' ===================================================
        ' ① 発信担当者テキストボックス(txtSenderUserName)
        txtSenderUserName.Text = ""
        hdnSenderUserName.Value = ""
        hdnSenderUserCD.Value = ""
        hdnSenderUserID.Value = ""

        ' ログインユーザの氏名・所属・役職を取得
        If Not Session(SessionComm.SESSION_USER_CD) Is Nothing Then
            dt = QualityProgressionEntity.GetUserInfo(Session(SessionComm.SESSION_USER_CD).ToString)
            If dt.Rows.Count > 0 Then

                ' 全角スペース区切りで組織名が上位から並んでいる　例．西日本支社　長崎システム開発二部　開発二グループ
                Dim strAllSectNm = dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString()
                Dim arrAllSectNm As String()
                arrAllSectNm = strAllSectNm.Split("　")

                ' ① 支社名テキストボックス(txtBranchName)
                ' B.A01M002_ALLSECT_NM（所属全名称）の1番目の要素をセットする
                If arrAllSectNm.Length > 0 Then
                    txtBranchName.Text = arrAllSectNm(0)
                End If

                ' ② 対象部門テキストボックス(txtTargetSectName)
                ' B.A01M002_ALLSECT_NM（所属全名称）の2番目の要素をセットする
                If arrAllSectNm.Length > 1 Then
                    txtTargetSectName.Text = arrAllSectNm(1)
                End If

                hdnTargetSectCD.Value = dt.Rows(0).Item("A01M002_ID").ToString()

                ' ③ 開催担当者テキストボックス(txtOpenerUserName)
                txtOpenerUserName.Text =
                      dt.Rows(0).Item("A01M010_FULLNAME").ToString + " " _
                    + dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString + " " _
                    + dt.Rows(0).Item("A01M003_POSTCLS_NM").ToString
                hdnOpenerUserID.Value = dt.Rows(0).Item("A01M010_ID").ToString
                hdnOpenerUserCD.Value = dt.Rows(0).Item("A01M010_USER_CD").ToString

                ' ① 発信担当者テキストボックス(txtSenderUserName)
                hdnSenderUserName.Value =
                      dt.Rows(0).Item("A01M010_FULLNAME").ToString + " " _
                    + dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString + " " _
                    + dt.Rows(0).Item("A01M003_POSTCLS_NM").ToString
                txtSenderUserName.Text = hdnSenderUserName.Value
                hdnSenderUserID.Value = dt.Rows(0).Item("A01M010_ID").ToString
                hdnSenderUserCD.Value = dt.Rows(0).Item("A01M010_USER_CD").ToString

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

        ' ※⑥～⑪まで、取得したレコードをリスクフォロー対象案件(gdvIncompletePjList)内に1行ずつすべて表示する
        dt = QualityProgressionEntity.GetIncompletePj(Left(Session(SessionComm.SESSION_USER_SECT_CD).ToString, 2))

        Dim strPjs = New StringBuilder
        Dim i = 0
        Dim column As DataColumn = New DataColumn("canDownLoad")
        dt.Columns.Add(column)

        column = New DataColumn("isEditAble")
        dt.Columns.Add(column)

        column = New DataColumn("ORDER_NM_DISP")
        dt.Columns.Add(column)

        Dim canDownLoad As Boolean = False
        Dim isEditAble As Boolean = True

        For Each row In dt.Rows
            If IsDBNull(row("ATTATCH_FILE_NAME")) Then
                canDownLoad = False
            Else
                canDownLoad = True
            End If

            row("canDownLoad") = canDownLoad.ToString
            row("isEditAble") = isEditAble.ToString
            Dim strOrderName As String = row("ORDER_NM").ToString
            row("ORDER_NM_DISP") = strOrderName.Replace("'", "\'")

            ' @工事名称（担当：@担当者、@プロセス）
            If i > 0 Then
                strPjs.Append(INTENT)
            End If
            strPjs.Append(row("ORDER_NM").ToString & "（担当：" & row("PRODUCT_USER_FULLNAME").ToString & "、" & row("PROCESS_NAME").ToString & "）")
            i = i + 1

        Next
        gdvIncompletePjList.DataSource = dt
        gdvIncompletePjList.DataBind()
        Session(SESSION_PREVENTION_INCOMPLETE_PJ) = dt

        ' 開催案内ボタンクリックイベント用の「フォロー対象案件」
        hdnIncompletePjList.Value = strPjs.ToString

        ' ⑫ レビューポイントテキストボックス(txtReviewPoint)
        txtReviewPoint.Text = ""

        ' ⑬ レビュアー（出席予定者）テキストボックス(txtReviewerPlan)
        txtReviewerPlan.Text = ""

        ' ⑭ 添付ファイル
        ' NULL DataTableを作成
        ' dt = QualityProgressionEntity.GetPreventionAttatchInfo("")
        dt = GetRiskPreventionAttachNull()

        column = New DataColumn("isEditAble")
        dt.Columns.Add(column)
        For Each row In dt.Rows
            row("isEditAble") = isEditAble.ToString
        Next

        grdQualityProgressionAttatch.DataSource = dt
        grdQualityProgressionAttatch.DataBind()
        Session(SESSION_PREVENTION_ATTATCH) = dt
        If (dt.Rows.Count >= 10) Then
            lnkRiskPreventionAttatch.Visible = False
        Else
            lnkRiskPreventionAttatch.Visible = True
        End If

        lnkRiskPreventionAttatch.Enabled = True

        ' ⑰ 開催案内ボタン(btnOpenGuidanceCreate)
        btnOpenGuidanceCreate.Enabled = False

        ' ===================================================
        ' 3. 議事録
        ' ===================================================
        ' レビュアー（出席者）テキストボックス
        txtReviewer.Text = ""

        ' レビューコメントテキストボックス
        txtReviewRemark.Text = ""

        ' ===================================================
        ' フッタ
        ' ===================================================
        ' 最終更新日時
        hdnModifiedOn.Value = ""

    End Sub


    ''' <summary>
    ''' ページロードイベント(Page_Load)　
    ''' ※登録済み品質推進会議の参照・修正（検索結果画面から表示された）の場合
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load_Update(sender As Object, e As EventArgs)

        Dim dt As DataTable
        Dim rowPrevention As Object
        Dim isEditAble = False

        ' 一時ファイルをクリア
        Session(SESSION_PREVENTION_ATTATCH_DELETE) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD) = New ArrayList()
        Session(SESSION_PREVENTION_ATTATCH_ADD_NAME) = New ArrayList()

        ' 品質推進会議を取得
        dt = QualityProgressionEntity.GetProgression(Session(SessionComm.SESSION_QPNO).ToString)
        If dt.Rows.Count > 0 Then
            rowPrevention = dt.Rows(0)
            isEditAble = editAble(rowPrevention)
        Else
            Dim msg As String
            Dim tList As List(Of String) = New List(Of String)
            tList.Add(Session(SessionComm.SESSION_QPNO).ToString)
            msg = MessageComm.GetMessageContext("1", "201", tList)

            ' 取得レコードが0件の場合は、画面上にメッセージ「品質推進会議番号：xxxxxxxxxx の案件を表示できません。削除された可能性があります」を表示し、
            ' プランクの案件画面を表示後、1つ前（呼出し元）の画面に戻る
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)
            Return

        End If

        ' ===================================================
        ' ヘッダ
        ' ===================================================
        ' ① 起票者ラベル(lblCreatedUserName)
        lblCreatedUserName.Text = rowPrevention("CREATED_USER_NAME").ToString

        ' ② 最終更新者ラベル(lblModifiedUserName)
        lblModifiedUserName.Text = rowPrevention("MODIFIED_USER_NAME").ToString

        ' ===================================================
        ' 0. 報告区分
        ' ===================================================
        ' ① 報告区分ラジオボタン(rdoReportCategory)
        rdoReportCategory.SelectedValue = rowPrevention("REPORT_CATEGORY").ToString
        setRdoReportCategory()
        rdoReportCategory.Enabled = isEditAble

        ' ===================================================
        ' 1. 品質推進会 実施部門
        ' ===================================================

        ' ① 支社名テキストボックス(txtBranchName)
        txtBranchName.Text = rowPrevention("BRANCH_NAME").ToString

        ' ② 対象部門テキストボックス(txtTargetSectName)
        txtTargetSectName.Text = rowPrevention("TARGET_SECT_NAME").ToString
        hdnTargetSectCD.Value = rowPrevention("TARGET_SECT_ID").ToString

        ' ③ 開催担当者テキストボックス(txtOpenerUserName)
        txtOpenerUserName.Text = rowPrevention("OPENER_USER_NAME").ToString
        hdnOpenerUserID.Value = rowPrevention("OPENER_USER_ID").ToString
        hdnOpenerUserCD.Value = rowPrevention("OPENER_USER_CD").ToString

        ' ④ 会議名テキストボックス(txtConferenceName)
        txtConferenceName.Text = rowPrevention("CONFERENCE_NAME").ToString
        txtConferenceName.Enabled = isEditAble

        ' ===================================================
        ' 2. 品質推進会 実施内容
        ' ===================================================

        ' ① 発信担当者テキストボックス(txtSenderUserName)
        hdnSenderUserName.Value = rowPrevention("SENDER_USER_NAME").ToString
        txtSenderUserName.Text = hdnSenderUserName.Value
        hdnSenderUserID.Value = rowPrevention("SENDER_USER_ID").ToString
        hdnSenderUserCD.Value = rowPrevention("SENDER_USER_CD").ToString
        txtSenderUserName.Enabled = False

        ' 発信選択アイコン(imgSenderUserSearch)
        imgSenderUserSearch.Enabled = isEditAble
        imgSenderUserClear.Enabled = isEditAble

        ' ② 開催日テキストボックス(txtOpenDate)
        If String.IsNullOrEmpty(rowPrevention("OPEN_DATE").ToString) Then
            txtOpenDate.Text = ""
        Else
            Dim dtOpenDate As Date = Today
            If Date.TryParse(rowPrevention("OPEN_DATE").ToString, dtOpenDate) Then
                requestedDeliveryDateCalendar.SelectedDate = dtOpenDate
                txtOpenDate.Text = dtOpenDate.ToLongDateString
            End If
        End If

        txtOpenDate.Enabled = isEditAble

        ' 開催日選択カレンダー(imgOpenDateCalendar)
        imgOpenDateCalendar.Enabled = isEditAble

        ' ③ 開催時間テキストボックス(txtOpenTime)
        txtOpenTime.Text = rowPrevention("OPEN_TIME").ToString
        txtOpenTime.Enabled = isEditAble

        ' ④ 開催回テキストボックス(txtOpenRound)
        txtOpenRound.Text = rowPrevention("OPEN_ROUND").ToString
        txtOpenRound.Enabled = isEditAble

        ' ⑤ 開催場所テキストボックス(txtOpenPlace)
        txtOpenPlace.Text = rowPrevention("OPEN_PLACE").ToString
        txtOpenPlace.Enabled = isEditAble

        ' ※⑥～⑪まで、取得したレコードをリスクフォロー対象案件(gdvIncompletePjList)内に1行ずつすべて表示する
        dt = QualityProgressionEntity.GetProgressionRelate(Session(SessionComm.SESSION_QPNO))

        Dim strPjs = New StringBuilder
        Dim i = 0
        Dim column As DataColumn = New DataColumn("canDownLoad")
        dt.Columns.Add(column)

        column = New DataColumn("isEditAble")
        dt.Columns.Add(column)

        column = New DataColumn("ORDER_NM_DISP")
        dt.Columns.Add(column)

        Dim canDownLoad As Boolean = False
        For Each row In dt.Rows
            If IsDBNull(row("ATTATCH_FILE_NAME")) Then
                canDownLoad = False
            Else
                canDownLoad = True
            End If

            row("canDownLoad") = canDownLoad.ToString
            row("isEditAble") = isEditAble.ToString

            Dim strOrderName As String = row("ORDER_NM").ToString
            row("ORDER_NM_DISP") = strOrderName.Replace("'", "\'")

            ' @工事名称（担当：@担当者、@プロセス）
            If i > 0 Then
                strPjs.Append(INTENT)
            End If
            strPjs.Append(row("ORDER_NM").ToString & "（担当：" & row("PRODUCT_USER_FULLNAME").ToString & "、" & row("PROCESS_NAME").ToString & "）")
            i = i + 1
        Next
        gdvIncompletePjList.DataSource = dt
        gdvIncompletePjList.DataBind()
        Session(SESSION_PREVENTION_INCOMPLETE_PJ) = dt

        ' 開催案内ボタンクリックイベント用の「フォロー対象案件」
        hdnIncompletePjList.Value = strPjs.ToString

        ' ⑫ レビューポイントテキストボックス(txtReviewPoint)
        txtReviewPoint.Text = rowPrevention("REVIEW_POINT").ToString
        txtReviewPoint.Enabled = isEditAble

        ' ⑬ レビュアー（出席予定者）テキストボックス(txtReviewerPlan)
        txtReviewerPlan.Text = rowPrevention("REVIEWER_PLAN").ToString
        txtReviewerPlan.Enabled = isEditAble


        ' ⑭ 添付ファイル
        dt = QualityProgressionEntity.GetPreventionAttatchInfo(Session(SessionComm.SESSION_QPNO).ToString)

        column = New DataColumn("isEditAble")
        dt.Columns.Add(column)
        For Each row In dt.Rows
            row("isEditAble") = isEditAble.ToString
        Next

        grdQualityProgressionAttatch.DataSource = dt
        grdQualityProgressionAttatch.DataBind()
        Session(SESSION_PREVENTION_ATTATCH) = dt
        lnkRiskPreventionAttatch.Enabled = isEditAble
        If (dt.Rows.Count >= 10) Then
            lnkRiskPreventionAttatch.Visible = False
        Else
            lnkRiskPreventionAttatch.Visible = True
        End If

        ' ⑮ 開催案内ボタン(btnOpenGuidanceCreate)
        btnOpenGuidanceCreate.Enabled = isEditAble

        ' ===================================================
        ' 3. 議事録
        ' ===================================================
        ' レビュアー（出席者）テキストボックス(txtReviewer)
        txtReviewer.Text = rowPrevention("REVIEWER").ToString
        txtReviewer.Enabled = isEditAble

        ' レビューコメントテキストボックス(txtReviewRemark)
        txtReviewRemark.Text = rowPrevention("CONFERENCE_CONTENT").ToString
        txtReviewRemark.Enabled = isEditAble

        ' ===================================================
        ' フッタ
        ' ===================================================
        ' ① 品質推進会議登録ボタン(btnQualityProgressionInput)
        ' 	アクセス権が参照の場合は、btnQualityProgressionInputのEnabledプロパティをFalseにセットする
        btnQualityProgressionInput.Enabled = isEditAble

        ' ② 品質推進会議削除ボタン(btnQualityProgressionDel)	
        ' アクセス権が参照の場合は、btnQualityProgressionDelのEnabledプロパティをFalseにセットする
        btnQualityProgressionDel.Enabled = isEditAble

        ' ③ 最終更新日時隠し項目(hdnModifiedOn)
        hdnModifiedOn.Value = rowPrevention("MODIFIED_ON").ToString

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
    ''' リスク予防検討会登録ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRiskPreventionInput_Click(sender As Object, e As EventArgs) Handles btnQualityProgressionInput.Click

        Dim dtOpenDate As Date
        Dim msg As String = ""
        Dim strSeqNo As String = String.Empty
        Dim strNendo As String = String.Empty
        Dim strHanki As String = String.Empty

        txtSenderUserName.Text = hdnSenderUserName.Value

        Dim hasError = False
        ' 報告区分ラジオボタン(rdoReportCategory)
        ' いずれの選択肢も未チェックの場合は、メッセージ「報告区分を選択してください」を表示して処理を中断する
        If rdoReportCategory.SelectedIndex < 0 Then
            msg = MessageComm.GetMessageContext("1", "101", Nothing)
            hasError = True
        End If

        ' 会議名テキストボックス(txtConferenceName)	
        ' 未入力の場合、メッセージを表示し、処理を中断する
        If String.IsNullOrEmpty(txtConferenceName.Text.Trim) Then
            If hasError Then
                msg += "\n"
            End If
            msg += MessageComm.GetMessageContext("1", "208", Nothing)
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
            Else
                strNendo = GetJpNendo(dtOpenDate)
                strHanki = GetJpHanki(dtOpenDate)
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

        ' 議事内容(txtReviewRemark)	
        If Not String.IsNullOrEmpty(txtReviewRemark.Text) Then

            If txtReviewRemark.Text.Length > txtReviewRemark.MaxLength Then
                If hasError Then
                    msg += "\n"
                End If

                tList = New List(Of String)
                tList.Add("議事内容")
                tList.Add(txtReviewRemark.MaxLength.ToString)

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
            If Session(SessionComm.SESSION_QPNO) = "" Then

                strSeqNo = QualityProgressionEntity.InsertProgression(Session(SESSION_PREVENTION_INCOMPLETE_PJ),
                                                                      rdoReportCategory.SelectedValue,
                                                                      txtBranchName.Text,
                                                                      hdnTargetSectCD.Value,
                                                                      txtTargetSectName.Text,
                                                                      hdnOpenerUserID.Value,
                                                                      hdnOpenerUserCD.Value,
                                                                      txtOpenerUserName.Text,
                                                                      txtConferenceName.Text.Trim,
                                                                      hdnSenderUserID.Value,
                                                                      hdnSenderUserCD.Value,
                                                                      txtSenderUserName.Text,
                                                                      txtOpenDate.Text,
                                                                      strNendo,
                                                                      strHanki,
                                                                      txtOpenTime.Text,
                                                                      txtOpenRound.Text,
                                                                      txtOpenPlace.Text,
                                                                      txtReviewPoint.Text,
                                                                      txtReviewerPlan.Text,
                                                                      txtReviewer.Text,
                                                                      txtReviewRemark.Text,
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
                dt = QualityProgressionEntity.GetProgression(Session(SessionComm.SESSION_QPNO).ToString)

                ' SQLで取得したMODIFIED_ONの値と最終更新日時隠し項目(hdnModifiedOn)の値を比較する
                If dt.Rows.Count > 0 Then
                    ' 排他エラー「当該リスク予防検討会の情報はすでに他のユーザによって更新されています。このまま登録処理を続けますか？」
                    ' の確認画面で、OKが選択された場合は排他チェックを外す
                    If hdnIfOverwrite.Value <> "1" And hdnModifiedOn.Value <> dt.Rows(0).Item("MODIFIED_ON").ToString Then
                        '比較した値が異なる場合は以下の確認メッセージを表示する
                        '「当該品質推進会の情報はすでに他のユーザによって更新されています。このまま登録処理を続けますか？」
                        msg = MessageComm.GetMessageContext("1", "202", Nothing)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "overWriteConfirm('" & msg & "');", True)
                        Return
                    End If
                Else
                    ' 当該品質推進会情報はすでに他のユーザによって削除されています。品質推進会検索画面に戻ります
                    msg = MessageComm.GetMessageContext("1", "203", Nothing)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "updateError('" & msg & "');", True)
                    Return
                End If

                ' リスク予防検討会のUPDATE
                strSeqNo = QualityProgressionEntity.UpdateProgression(Session(SessionComm.SESSION_QPNO).ToString,
                                                                      Session(SESSION_PREVENTION_INCOMPLETE_PJ),
                                                                      rdoReportCategory.SelectedValue,
                                                                      txtBranchName.Text,
                                                                      hdnTargetSectCD.Value,
                                                                      txtTargetSectName.Text,
                                                                      hdnOpenerUserID.Value,
                                                                      hdnOpenerUserCD.Value,
                                                                      txtOpenerUserName.Text,
                                                                      txtConferenceName.Text.Trim,
                                                                      hdnSenderUserID.Value,
                                                                      hdnSenderUserCD.Value,
                                                                      txtSenderUserName.Text,
                                                                      txtOpenDate.Text,
                                                                      strNendo,
                                                                      strHanki,
                                                                      txtOpenTime.Text,
                                                                      txtOpenRound.Text,
                                                                      txtOpenPlace.Text,
                                                                      txtReviewPoint.Text,
                                                                      txtReviewerPlan.Text,
                                                                      txtReviewer.Text,
                                                                      txtReviewRemark.Text,
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
            ' 画面上にメッセージ「品質推進会議の登録でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            msg = MessageComm.GetMessageContext("1", "204", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)
            Return
        End Try

        ' 画面上にメッセージ「品質推進会議の登録が完了しました。会議名：xxxxxxxxxx」を表示する。
        tList = New List(Of String)
        tList.Add(txtConferenceName.Text)
        msg = MessageComm.GetMessageContext("1", "205", tList)
        msg = msg.Replace("'", "\'")
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)

        ' 画面を更新する
        hdnQpNo.Value = strSeqNo
        Session(SessionComm.SESSION_QPNO) = strSeqNo

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
    Protected Sub btnRiskPreventionDel_Click(sender As Object, e As EventArgs) Handles btnQualityProgressionDel.Click

        Try
            ' Session変数QpNoの値がNULL・ブランクの場合
            ' "(1) ページロードイベント(Page_Load)　※新規登録（メニュー画面の品質推進会登録ボタンから表示された）の場合"を実行する
            If Session(SessionComm.SESSION_QPNO) = "" Then
                Page_Load_New(sender, e)
            Else
                QualityProgressionEntity.DeleteProgression(Session(SessionComm.SESSION_QPNO).ToString,
                                                          Session(SessionComm.SESSION_USER_CD).ToString)

                ' 画面上にメッセージ「品質推進会議の削除が完了しました。会議名：xxxxxxxxxx」を表示する。
                Dim msg As String
                Dim tList As List(Of String) = New List(Of String)
                tList.Add(txtConferenceName.Text)
                msg = MessageComm.GetMessageContext("1", "206", tList)
                msg = msg.Replace("'", "\'")
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "deleteEnd('" & msg & "');", True)
            End If

        Catch ex As Exception
            'DELETE発行処理でエラーが発生した場合、エラーログを出力する
            GetLogger().Error(ex.ToString)

            ' DELETE発行処理でエラーが発生した場合はSQL発行をロールバックし、
            ' 画面上にメッセージ「品質推進会議の削除でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            Dim msg = MessageComm.GetMessageContext("1", "207", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "alert('" & msg & "');", True)
        End Try

    End Sub

    ''' <summary>
    ''' 添付ファイル操作イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub grdQualityProgressionAttatch_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdQualityProgressionAttatch.RowCommand
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
            grdQualityProgressionAttatch.DataSource = dtAttatch
            grdQualityProgressionAttatch.DataBind()

            If (dtAttatch.Rows.Count >= 10) Then
                lnkRiskPreventionAttatch.Visible = False
            Else
                lnkRiskPreventionAttatch.Visible = True
            End If

        End If
    End Sub

    ''' <summary>
    ''' リスクフォロー対象案件イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub gdvIncompletePjList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdvIncompletePjList.RowCommand

        ' 削除ボタンクリックイベント
        If e.CommandName = "del" Then

            ' 選択したファイルをSESSIONに保存した添付ファイル一覧から削除
            Dim dt As DataTable = Session(SESSION_PREVENTION_INCOMPLETE_PJ)
            For Each row In dt.Rows
                If row("PROJECT_NO").ToString = e.CommandArgument.ToString() Then
                    dt.Rows.Remove(row)
                    Exit For
                End If
            Next

            ' 開催案内文を再生成する
            Dim i = 0
            Dim strPjs = New StringBuilder
            For Each row In dt.Rows
                ' @工事名称（担当：@担当者、@プロセス）
                If i > 0 Then
                    strPjs.Append(INTENT)
                End If
                strPjs.Append(row("ORDER_NM").ToString & "（担当：" & row("PRODUCT_USER_FULLNAME").ToString & "、" & row("PROCESS_NAME").ToString & "）")
                i = i + 1
            Next
            ' 開催案内ボタンクリックイベント用の「フォロー対象案件」
            hdnIncompletePjList.Value = strPjs.ToString

            Session(SESSION_PREVENTION_INCOMPLETE_PJ) = dt

            ' 画面を更新
            gdvIncompletePjList.DataSource = dt
            gdvIncompletePjList.DataBind()

        End If

    End Sub

    ''' <summary>
    ''' ファイルアップロード完了処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
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
        grdQualityProgressionAttatch.DataSource = dtAttatch
        grdQualityProgressionAttatch.DataBind()

        If (dtAttatch.Rows.Count >= 10) Then
            lnkRiskPreventionAttatch.Visible = False
            txtReviewer.Focus()
        Else
            lnkRiskPreventionAttatch.Visible = True
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
            Case "開催通知" ' 選択肢「開催通知」が選択された場合
                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ 品質推進会 開催通知 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ 品質推進会 開催通知 ◆◆◆"

            Case "議事録" ' 選択肢「議事録」が選択された場合
                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ 品質推進会 議事録 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ 品質推進会 議事録 ◆◆◆"

            Case Else
                ' 画面タイトル(lblPageTitle)の値に「◆◆◆ 品質推進会 開催通知 ◆◆◆」をセットする
                lblPageTitle.Text = "◆◆◆ 品質推進会 開催通知 ◆◆◆"

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
    ''' 日本財政年度取得
    ''' </summary>
    ''' <param name="dtValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJpNendo(ByVal dtValue As Date) As String
        Dim strNendo As String = ""

        Try
            strNendo = dtValue.ToString().Substring(0, 4)
            Dim dtTemp As Date = CDate(strNendo & "/03/31")
            If Date.Compare(dtValue, dtTemp) < 0 OrElse Date.Compare(dtValue, dtTemp) = 0 Then
                strNendo = CStr(CInt(strNendo) - 1)
            End If
        Catch ex As Exception
        End Try

        Return strNendo
    End Function

    ''' <summary>
    ''' 日本四半期取得
    ''' </summary>
    ''' <param name="dtValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetJpHanki(ByVal dtValue As Date) As String

        Dim hanki As Integer = System.Math.Truncate(dtValue.Month / 4)
        If hanki = 0 Then
            hanki = 4
        End If

        Return hanki.ToString
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

        Return log4net.LogManager.GetLogger("QualityProgression")

    End Function

End Class