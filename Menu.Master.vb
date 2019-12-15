Imports System.IO

Public Class Menu
    Inherits System.Web.UI.MasterPage

    ''' <summary>
    ''' ページロードイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            '対象案件一覧の支社ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlPjBranch, MenuEntity.GetMenuBranch(), True)

            '対象案件一覧の部門ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlPjDept, MenuEntity.GetMenuDept(ddlPjBranch.Text), True)

            'リスク・予防管理検討会の支社ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlRpiBranch, MenuEntity.GetMenuBranch(), True)

            'リスク・予防管理検討会の部門ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlRpiDept, MenuEntity.GetMenuDept(ddlRpiBranch.Text), True)

            '品質推進会の支社ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlQpBranch, MenuEntity.GetMenuBranch(), True)

            '品質推進会の部門ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlQpDept, MenuEntity.GetMenuDept(ddlQpBranch.Text), True)

            'リスク・予防管理検討会のプロセスドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlProcess, MenuEntity.GetMenuProcess(), True)

            'リスク・予防管理検討会の開催年ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlRpiOpenYear, MenuEntity.GetMenuRpiOpenYear(), True)

            '品質推進会の開催年度ドロップダウンリストのデータジャック
            MethodComm.SetDropDownListValue(ddlQpOpenFiscalYear, MenuEntity.GetMenuQpOpenFiscalYear, True)

            '入力値を取得する
            If Session(SessionComm.SESSION_SEARCH_TYPE) = "1" Then
                PjInputValueSave()
            ElseIf Session(SessionComm.SESSION_SEARCH_TYPE) = "2" Then
                RpiInputValueSave()
                chkRpmTypeAll.Checked = True
            ElseIf Session(SessionComm.SESSION_SEARCH_TYPE) = "3" Then
                QpInputValueSave()
                chkRpmTypeAll.Checked = True
            Else
                chkRpmTypeAll.Checked = True
            End If

        End If

    End Sub

    ''' <summary>
    ''' 対象案件一覧 支社ドロップダウンリスト変更イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub ddlPjBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPjBranch.SelectedIndexChanged

        '対象案件一覧の部門ドロップダウンリストのデータジャック
        MethodComm.SetDropDownListValue(ddlPjDept, MenuEntity.GetMenuDept(ddlPjBranch.Text), True)

    End Sub


    ''' <summary>
    ''' リスク・予防管理検討会 支社ドロップダウンリスト変更イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub ddlRpiBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRpiBranch.SelectedIndexChanged

        'リスク・予防管理検討会の部門ドロップダウンリストのデータジャック
        MethodComm.SetDropDownListValue(ddlRpiDept, MenuEntity.GetMenuDept(ddlRpiBranch.Text), True)

    End Sub


    ''' <summary>
    ''' 品質推進会 支社ドロップダウンリスト変更イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub ddlQpBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlQpBranch.SelectedIndexChanged

        '品質推進会の部門ドロップダウンリストのデータジャック
        MethodComm.SetDropDownListValue(ddlQpDept, MenuEntity.GetMenuDept(ddlQpBranch.Text), True)

    End Sub


    ''' <summary>
    ''' 対象案件一覧 検索ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnPjSearch_Click(sender As Object, e As EventArgs) Handles btnPjSearch.Click

        'Session変数SearchTypeに1をセットする
        Session(SessionComm.SESSION_SEARCH_TYPE) = "1"

        '対象案件一覧支社ドロップダウンリストの値
        Session(SessionComm.SESSION_SEARCH_BRANCH) = ddlPjBranch.Text

        '対象案件一覧部門ドロップダウンリストの値
        Session(SessionComm.SESSION_SEARCH_DEPT) = ddlPjDept.Text

        '対象案件一覧オーダテキストボックスの値
        Session(SessionComm.SESSION_SEARCH_ORDER) = txtPjOrder.Text

        '対象案件一覧顧客テキストボックスの値
        Session(SessionComm.SESSION_SEARCH_CUSTOMER) = txtCustomer.Text

        '管理区分ラジオボタンチェック判断
        If chkRpmTypeA.Checked = True Then

            Session(SessionComm.SESSION_SEARCH_RPM_TYPE_A) = "1"
            Session(SessionComm.SESSION_SEARCH_RPM_TYPE_B) = "0"

        ElseIf chkRpmTypeB.Checked = True Then

            Session(SessionComm.SESSION_SEARCH_RPM_TYPE_B) = "1"
            Session(SessionComm.SESSION_SEARCH_RPM_TYPE_A) = "0"

        ElseIf chkRpmTypeAll.Checked = True Then

            Session(SessionComm.SESSION_SEARCH_RPM_TYPE_A) = "0"
            Session(SessionComm.SESSION_SEARCH_RPM_TYPE_B) = "0"

        End If

        '完了区分チェックボックスチェック判断
        If chkCompCategory.Checked = True Then

            Session(SessionComm.SESSION_SEARCH_COMP_CATEGORY) = "1"

        Else

            Session(SessionComm.SESSION_SEARCH_COMP_CATEGORY) = "0"

        End If

        Response.Write("<script language=javascript>window.location.href='ProjectList.aspx'</script>")

    End Sub


    ''' <summary>
    ''' リスク・予防管理検討会 検索ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnRpiOrder_Click(sender As Object, e As EventArgs) Handles btnRpiSearch.Click

        'Session変数SearchTypeに2をセットする
        Session(SessionComm.SESSION_SEARCH_TYPE) = "2"

        '支社ドロップダウンリストの値
        Session(SessionComm.SESSION_SEARCH_BRANCH) = ddlRpiBranch.Text

        '部門ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_DEPT) = ddlRpiDept.Text

        'プロセスドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_PROCESS) = ddlProcess.Text

        '開催年ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_OPEN_YEAR) = ddlRpiOpenYear.Text

        '開催月ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_OPEN_MONTH) = ddlRpiOpenMonth.Text

        '対象オーダ
        Session(SessionComm.SESSION_SEARCH_ORDER) = txtRpiOrder.Text

        Response.Write("<script language=javascript>window.location.href='RiskPreventionList.aspx'</script>")
    End Sub

    ''' <summary>
    ''' 品質推進会 検索ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnQpSearch_Click(sender As Object, e As EventArgs) Handles btnQpSearch.Click

        'Session変数SearchTypeに3をセットする
        Session(SessionComm.SESSION_SEARCH_TYPE) = "3"

        '支社ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_BRANCH) = ddlQpBranch.Text

        '部門ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_DEPT) = ddlQpDept.Text

        '開催年度ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_OPEN_YEAR) = ddlQpOpenFiscalYear.Text

        '開催四半期ドロップダウンリスト
        Session(SessionComm.SESSION_SEARCH_OPEN_QUARTER) = ddlQpOpenQuarter.Text

        '対象オーダ
        Session(SessionComm.SESSION_SEARCH_ORDER) = txtQpOrder.Text

        Response.Write("<script language=javascript>window.location.href='QualityProgressionList.aspx'</script>")
    End Sub

    ''' <summary>
    ''' 検索結果のCSV出力ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnCsvOutput_Click(sender As Object, e As EventArgs) Handles btnCsvOutput.Click

        'データ座標変数
        Dim intRow, intCol As Integer

        '出力項目
        Dim strRow As String = ""

        'タイプ変換変数
        Dim encText As System.Text.Encoding = System.Text.Encoding.GetEncoding(932)

        'タイプ変換変数
        Dim btText() As Byte

        'データ
        Dim data As DataTable = New DataTable

        'Session変数SearchTypeをを判断して、データを取得する
        If Session(SessionComm.SESSION_SEARCH_TYPE) = "1" Then
            data = PjExportValue(PjSearchData())

        ElseIf Session(SessionComm.SESSION_SEARCH_TYPE) = "2" Then
            data = RplExportValue(RplSearchData())

        ElseIf Session(SessionComm.SESSION_SEARCH_TYPE) = "3" Then
            data = QpExportValue(QpSearchData())
        Else
            Dim msg As String = MessageComm.GetMessageContext("1", "303", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)
        End If

        '検索結果のCSV出力
        If data.Rows.Count > 0 Then
            '検索結果は1レコードを1行とし、1カラムごとにダブルクォーテーション(")でくくる
            For intRow = 0 To data.Rows.Count - 1
                strRow += """"
                For intCol = 0 To data.Columns.Count - 1
                    If Not data.Rows(intRow).Item(intCol).ToString = "" Then
                        strRow += CStr(data.Rows(intRow).Item(intCol))
                    Else
                        strRow += ""
                    End If
                    If intCol < data.Columns.Count - 1 Then
                        strRow += ""","""
                    End If
                Next
                strRow += """" + System.Environment.NewLine
            Next

            'Responseを介してクライアントのIEへSearchResult.csvをダウンロードさせる
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=SearchRsult.csv")

            'タイプ変換
            btText = encText.GetBytes(strRow)

            '検索結果のCSV出力
            HttpContext.Current.Response.BinaryWrite(btText)
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.Close()
            HttpContext.Current.Response.Clear()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Else
            Dim msg As String = MessageComm.GetMessageContext("1", "301", Nothing)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "", "errorBack('" & msg & "');", True)
        End If

    End Sub

    ''' <summary>
    ''' 案件検索結果エクスポートデータを作成する
    ''' </summary>
    ''' <param name="PJSearchData">案件検索情報データ</param>
    ''' <returns>案件検索結果エクスポートデータ</returns>
    ''' <remarks></remarks>
    Function PjExportValue(PJSearchData As DataTable) As DataTable

        '案件検索結果エクスポートデータを定義して、データ列を作成する
        Dim ExportValue As DataTable = New DataTable
        ExportValue.Columns.Add("PROJECT_NO")
        ExportValue.Columns.Add("A01M002_SECT_NM")
        ExportValue.Columns.Add("PRODUCT_SECT_NM")
        ExportValue.Columns.Add("PROCESS_NAME")
        ExportValue.Columns.Add("ORDER_CD")
        ExportValue.Columns.Add("A01M009_ORDER_NM")
        ExportValue.Columns.Add("A01M015_COMPY_NM")
        ExportValue.Columns.Add("A01M014_JYUCHU_CRR")
        ExportValue.Columns.Add("A01M014_NOKI_YMD")
        ExportValue.Columns.Add("CUSTOMER_TYPE_NAME")
        ExportValue.Columns.Add("BRANCH_TRANSACTION_FLG")
        ExportValue.Columns.Add("SUPPORT_BRANCH_NM")
        ExportValue.Columns.Add("RPM_500MIL_FLG")
        ExportValue.Columns.Add("RPM_FIRST_PRODUCT_FLG")
        ExportValue.Columns.Add("RPM_SPECIAL_PRODUCT_FLG")
        ExportValue.Columns.Add("RPM_TYPE")
        ExportValue.Columns.Add("PROJECT_TYPE_NAME")
        ExportValue.Columns.Add("RP_P1_COUNT")
        ExportValue.Columns.Add("RP_P2_COUNT")
        ExportValue.Columns.Add("RP_P3_COUNT")
        ExportValue.Columns.Add("RP_P4_COUNT")
        ExportValue.Columns.Add("PROJECT_COMPLETE_FLG")

        If PJSearchData.Rows.Count > 0 Then

            For i As Integer = 0 To PJSearchData.Rows.Count - 1

                'データ行を定義する
                Dim dataRow As DataRow = ExportValue.NewRow()

                'データ行を作成する
                dataRow("PROJECT_NO") = PJSearchData.Rows(i).Item("PROJECT_NO").ToString
                dataRow("A01M002_SECT_NM") = PJSearchData.Rows(i).Item("A01M002_SECT_NM").ToString
                dataRow("PRODUCT_SECT_NM") = PJSearchData.Rows(i).Item("PRODUCT_SECT_NM").ToString
                dataRow("PROCESS_NAME") = PJSearchData.Rows(i).Item("PROCESS_NAME").ToString
                dataRow("ORDER_CD") = PJSearchData.Rows(i).Item("ORDER_CD").ToString
                dataRow("A01M009_ORDER_NM") = PJSearchData.Rows(i).Item("A01M009_ORDER_NM").ToString
                dataRow("A01M015_COMPY_NM") = PJSearchData.Rows(i).Item("A01M015_COMPY_NM").ToString
                dataRow("A01M014_JYUCHU_CRR") = PJSearchData.Rows(i).Item("A01M014_JYUCHU_CRR").ToString
                dataRow("A01M014_NOKI_YMD") = PJSearchData.Rows(i).Item("A01M014_NOKI_YMD").ToString
                dataRow("CUSTOMER_TYPE_NAME") = PJSearchData.Rows(i).Item("CUSTOMER_TYPE_NAME").ToString
                dataRow("BRANCH_TRANSACTION_FLG") = PJSearchData.Rows(i).Item("BRANCH_TRANSACTION_FLG").ToString
                dataRow("SUPPORT_BRANCH_NM") = PJSearchData.Rows(i).Item("SUPPORT_BRANCH_NM").ToString
                dataRow("RPM_500MIL_FLG") = PJSearchData.Rows(i).Item("RPM_500MIL_FLG").ToString
                dataRow("RPM_FIRST_PRODUCT_FLG") = PJSearchData.Rows(i).Item("RPM_FIRST_PRODUCT_FLG").ToString
                dataRow("RPM_SPECIAL_PRODUCT_FLG") = PJSearchData.Rows(i).Item("RPM_SPECIAL_PRODUCT_FLG").ToString
                dataRow("RPM_TYPE") = PJSearchData.Rows(i).Item("RPM_TYPE").ToString
                dataRow("PROJECT_TYPE_NAME") = PJSearchData.Rows(i).Item("PROJECT_TYPE_NAME").ToString
                dataRow("RP_P1_COUNT") = PJSearchData.Rows(i).Item("RP_P1_COUNT").ToString
                dataRow("RP_P2_COUNT") = PJSearchData.Rows(i).Item("RP_P2_COUNT").ToString
                dataRow("RP_P3_COUNT") = PJSearchData.Rows(i).Item("RP_P3_COUNT").ToString
                dataRow("RP_P4_COUNT") = PJSearchData.Rows(i).Item("RP_P4_COUNT").ToString
                dataRow("PROJECT_COMPLETE_FLG") = PJSearchData.Rows(i).Item("PROJECT_COMPLETE_FLG").ToString

                ExportValue.Rows.Add(dataRow)
            Next

        End If

        Return ExportValue

    End Function


    ''' <summary>
    '''  リスク予防・管理検討会検索結果エクスポートデータを作成する
    ''' </summary>
    ''' <param name="RplSearchData">
    ''' リスク予防・管理検討会検索結果情報
    ''' </param>
    ''' <returns>
    ''' リスク予防・管理検討会検索結果エクスポートデータ
    ''' </returns>
    ''' <remarks></remarks>
    Function RplExportValue(RplSearchData As DataTable) As DataTable

        'リスク予防・管理検討会検索結果エクスポートデータを定義して、データ列を作成する
        Dim ExportValue As DataTable = New DataTable
        ExportValue.Columns.Add("PROJECT_NO")
        ExportValue.Columns.Add("A01M002_SECT_NM")
        ExportValue.Columns.Add("PRODUCT_SECT_NM")
        ExportValue.Columns.Add("PROCESS_NAME")
        ExportValue.Columns.Add("DISCUSS_PHASE_NAME")
        ExportValue.Columns.Add("ORDER_CD")
        ExportValue.Columns.Add("A01M009_ORDER_NM")
        ExportValue.Columns.Add("A01M015_COMPY_NM")
        ExportValue.Columns.Add("SENDER_USER_FULLNAME")
        ExportValue.Columns.Add("OPEN_DATE")
        ExportValue.Columns.Add("OPEN_ROUND")
        ExportValue.Columns.Add("OPEN_PLACE")
        ExportValue.Columns.Add("REVIEWER")

        If RplSearchData.Rows.Count > 0 Then

            For i As Integer = 0 To RplSearchData.Rows.Count - 1

                'データ行を定義する
                Dim dataRow As DataRow = ExportValue.NewRow()

                'データ行を作成する
                dataRow("PROJECT_NO") = RplSearchData.Rows(i).Item("PROJECT_NO").ToString
                dataRow("A01M002_SECT_NM") = RplSearchData.Rows(i).Item("A01M002_SECT_NM").ToString
                dataRow("PRODUCT_SECT_NM") = RplSearchData.Rows(i).Item("PRODUCT_SECT_NM").ToString
                dataRow("PROCESS_NAME") = RplSearchData.Rows(i).Item("PROCESS_NAME").ToString
                dataRow("DISCUSS_PHASE_NAME") = RplSearchData.Rows(i).Item("DISCUSS_PHASE_NAME").ToString
                dataRow("ORDER_CD") = RplSearchData.Rows(i).Item("ORDER_CD").ToString
                dataRow("A01M009_ORDER_NM") = RplSearchData.Rows(i).Item("A01M009_ORDER_NM").ToString
                dataRow("A01M015_COMPY_NM") = RplSearchData.Rows(i).Item("A01M015_COMPY_NM").ToString
                dataRow("SENDER_USER_FULLNAME") = RplSearchData.Rows(i).Item("SENDER_USER_FULLNAME").ToString
                dataRow("OPEN_DATE") = Left(RplSearchData.Rows(i).Item("OPEN_DATE").ToString, 10)
                dataRow("OPEN_ROUND") = RplSearchData.Rows(i).Item("OPEN_ROUND").ToString
                dataRow("OPEN_PLACE") = RplSearchData.Rows(i).Item("OPEN_PLACE").ToString
                dataRow("REVIEWER") = RplSearchData.Rows(i).Item("REVIEWER").ToString

                ExportValue.Rows.Add(dataRow)
            Next

        End If

        Return ExportValue

    End Function


    ''' <summary>
    ''' 品質推進会議検索結果エクスポートデータを作成する
    ''' </summary>
    ''' <param name="QpSearchData">
    ''' 品質推進会議検索情報
    ''' </param>
    ''' <returns>
    ''' 品質推進会議検索結果エクスポートデータ
    ''' </returns>
    ''' <remarks></remarks>
    Function QpExportValue(QpSearchData As DataTable) As DataTable

        '品質推進会議検索結果エクスポートデータを定義して、データ列を作成する
        Dim ExportValue As DataTable = New DataTable
        ExportValue.Columns.Add("CONFERENCE_NAME")
        ExportValue.Columns.Add("BRANCH_NAME")
        ExportValue.Columns.Add("TARGET_SECT_NAME")
        ExportValue.Columns.Add("ORDER_CD")
        ExportValue.Columns.Add("SENDER_USER_NAME")
        ExportValue.Columns.Add("OPEN_DATE")
        ExportValue.Columns.Add("OPEN_PLACE")
        ExportValue.Columns.Add("CREATED")

        If QpSearchData.Rows.Count > 0 Then

            For i As Integer = 0 To QpSearchData.Rows.Count - 1

                'データ行を定義する
                Dim dataRow As DataRow = ExportValue.NewRow()

                'データ行を作成する
                dataRow("CONFERENCE_NAME") = QpSearchData.Rows(i).Item("CONFERENCE_NAME").ToString
                dataRow("BRANCH_NAME") = QpSearchData.Rows(i).Item("BRANCH_NAME").ToString
                dataRow("TARGET_SECT_NAME") = QpSearchData.Rows(i).Item("TARGET_SECT_NAME").ToString
                dataRow("ORDER_CD") = QpSearchData.Rows(i).Item("ORDER_CD").ToString
                dataRow("SENDER_USER_NAME") = QpSearchData.Rows(i).Item("SENDER_USER_NAME").ToString

                If Not String.IsNullOrEmpty(QpSearchData.Rows(i).Item("OPEN_DATE").ToString) Then
                    dataRow("OPEN_DATE") = Format(QpSearchData.Rows(i).Item("OPEN_DATE"), "yyyy/MM/dd")
                Else
                    dataRow("OPEN_DATE") = ""
                End If

                dataRow("OPEN_PLACE") = QpSearchData.Rows(i).Item("OPEN_PLACE").ToString
                dataRow("CREATED") = QpSearchData.Rows(i).Item("CREATED").ToString

                ExportValue.Rows.Add(dataRow)

            Next

        End If

        Return ExportValue

    End Function



    ''' <summary>
    ''' 案件検索情報検索。
    ''' </summary>
    ''' <returns>
    ''' 案件検索情報
    ''' </returns>
    ''' <remarks>
    ''' 案件検索情報を取得する。
    ''' </remarks>
    Function PjSearchData() As DataTable

        'Sessionから、前画面に入力する検索条件を取得する
        '支社の値
        Dim searchBranch As String = Session(SessionComm.SESSION_SEARCH_BRANCH)

        '部門の値
        Dim searchDept As String = Session(SessionComm.SESSION_SEARCH_DEPT)

        '顧客の値
        Dim searchCustomer As String = Session(SessionComm.SESSION_SEARCH_CUSTOMER)

        '管理区分Aの値
        Dim searchRpmTypeA As String = Session(SessionComm.SESSION_SEARCH_RPM_TYPE_A)

        '管理区分Bの値
        Dim searchRpmTypeB As String = Session(SessionComm.SESSION_SEARCH_RPM_TYPE_B)

        '完了区分の値
        Dim searchCompCategory As String = Session(SessionComm.SESSION_SEARCH_COMP_CATEGORY)

        'オーダの値
        Dim searchOrder As String = Session(SessionComm.SESSION_SEARCH_ORDER)

        'グリッドデータ
        Dim gdData As DataTable

        'DBを連接する、データの値を取得する
        gdData = ProjectListEntity.GetProjectList(searchBranch, searchDept, searchOrder, searchCustomer, searchRpmTypeA, searchRpmTypeB, searchCompCategory)

        Return gdData

    End Function

    ''' <summary>
    ''' リスク予防管理検討会情報検索。
    ''' </summary>
    ''' <returns>
    ''' リスク予防管理検討会情報
    ''' </returns>
    ''' <remarks>
    ''' リスク予防管理検討会情報を取得する。
    ''' </remarks>
    Function RplSearchData() As DataTable

        'Sessionから、前画面に入力する検索条件を取得する
        '検索結果データ
        Dim gdData As DataTable

        '検索結果タイプ
        Dim sessSearchType As String = Session(SessionComm.SESSION_SEARCH_TYPE)

        '本支社の選択値
        Dim sessSearchBranch As String = Session(SessionComm.SESSION_SEARCH_BRANCH)

        '部門の選択値
        Dim sessSearchDept As String = Session(SessionComm.SESSION_SEARCH_DEPT)

        'プロセスの選択値
        Dim sessSearchProcess As String = Session(SessionComm.SESSION_SEARCH_PROCESS)

        '開催年の選択値
        Dim sessSearchOpenYear As String = Session(SessionComm.SESSION_SEARCH_OPEN_YEAR)

        '開催月の選択値
        Dim sessSearchOpenMonth As String = Session(SessionComm.SESSION_SEARCH_OPEN_MONTH)

        'オーダの入力値
        Dim sessSearchOrder As String = Session(SessionComm.SESSION_SEARCH_ORDER)

        'DBを連接する、データの値を取得する
        gdData = RiskPreventionListEntity.GetSearchResult(sessSearchType, sessSearchBranch, sessSearchDept, sessSearchProcess, sessSearchOpenYear, sessSearchOpenMonth, sessSearchOrder)

        Return gdData

    End Function

    '''<summary>
    ''' 品質推進会情報検索。
    '''</summary>
    ''' <returns>
    ''' 品質推進会情報
    ''' </returns>
    '''<remarks>
    ''' 品質推進会情報を取得する。
    '''</remarks>
    Function QpSearchData() As DataTable

        'Sessionから、前画面に入力する検索条件を取得する
        '支社の値
        Dim parmBranch As String = Session(SessionComm.SESSION_SEARCH_BRANCH)

        '部門の値
        Dim parmDept As String = Session(SessionComm.SESSION_SEARCH_DEPT)

        '開催年度の値
        Dim parmOpenYear As String = Session(SessionComm.SESSION_SEARCH_OPEN_YEAR)

        '開催四半期の値
        Dim parmOpenQuarter As String = Session(SessionComm.SESSION_SEARCH_OPEN_QUARTER)

        'オーダの値
        Dim parmOrder As String = Session(SessionComm.SESSION_SEARCH_ORDER)

        'DBを連接する、データの値を取得する
        Dim gdData As DataTable = QualityProgressionListEntity.GetSearchResult(parmBranch, parmDept, parmOpenYear, parmOpenQuarter, parmOrder)

        Return gdData

    End Function

    ''' <summary>
    ''' 対象案件一覧入力値保存
    ''' </summary>
    ''' <remarks></remarks>
    Sub PjInputValueSave()
        '対象案件一覧
        '対象案件一覧支社ドロップダウンリストの値
        ddlPjBranch.Text = Session(SessionComm.SESSION_SEARCH_BRANCH)

        '対象案件一覧部門ドロップダウンリストの値
        ddlPjDept.Text = Session(SessionComm.SESSION_SEARCH_DEPT)

        '対象案件一覧の部門ドロップダウンリストのデータジャック
        MethodComm.SetDropDownListValue(ddlPjDept, MenuEntity.GetMenuDept(ddlPjBranch.Text), True)

        '対象案件一覧オーダテキストボックスの値
        txtPjOrder.Text = Session(SessionComm.SESSION_SEARCH_ORDER)

        '対象案件一覧顧客テキストボックスの値
        txtCustomer.Text = Session(SessionComm.SESSION_SEARCH_CUSTOMER)

        '管理区分ラジオボタンチェック判断
        If Session(SessionComm.SESSION_SEARCH_RPM_TYPE_A) = "1" And Session(SessionComm.SESSION_SEARCH_RPM_TYPE_B) = "0" Then
            chkRpmTypeA.Checked = True
        ElseIf Session(SessionComm.SESSION_SEARCH_RPM_TYPE_A) = "0" And Session(SessionComm.SESSION_SEARCH_RPM_TYPE_B) = "1" Then
            chkRpmTypeB.Checked = True
        Else
            chkRpmTypeAll.Checked = True
        End If

        '完了区分チェックボックスチェック判断
        If Session(SessionComm.SESSION_SEARCH_COMP_CATEGORY) = "1" Then
            chkCompCategory.Checked = True
        Else
            chkCompCategory.Checked = False
        End If

    End Sub

    ''' <summary>
    ''' リスク・予防管理検討会入力値保存
    ''' </summary>
    ''' <remarks></remarks>
    Sub RpiInputValueSave()

        'リスク・予防管理検討会
        '支社ドロップダウンリストの値
        ddlRpiBranch.Text = Session(SessionComm.SESSION_SEARCH_BRANCH)

        '部門ドロップダウンリスト
        ddlRpiDept.Text = Session(SessionComm.SESSION_SEARCH_DEPT)

        'リスク・予防管理検討会の部門ドロップダウンリストのデータジャック
        MethodComm.SetDropDownListValue(ddlRpiDept, MenuEntity.GetMenuDept(ddlRpiBranch.Text), True)

        'プロセスドロップダウンリスト
        ddlProcess.Text = Session(SessionComm.SESSION_SEARCH_PROCESS)

        '開催年ドロップダウンリスト
        ddlRpiOpenYear.Text = Session(SessionComm.SESSION_SEARCH_OPEN_YEAR)

        '開催月ドロップダウンリスト
        ddlRpiOpenMonth.Text = Session(SessionComm.SESSION_SEARCH_OPEN_MONTH)

        '対象オーダ
        txtRpiOrder.Text = Session(SessionComm.SESSION_SEARCH_ORDER)

    End Sub

    ''' <summary>
    ''' 品質推進会入力値保存
    ''' </summary>
    ''' <remarks></remarks>
    Sub QpInputValueSave()

        '品質推進会
        '支社ドロップダウンリスト
        ddlQpBranch.Text = Session(SessionComm.SESSION_SEARCH_BRANCH)

        '品質推進会の部門ドロップダウンリストのデータジャック
        MethodComm.SetDropDownListValue(ddlQpDept, MenuEntity.GetMenuDept(ddlQpBranch.Text), True)

        '部門ドロップダウンリスト
        ddlQpDept.Text = Session(SessionComm.SESSION_SEARCH_DEPT)

        '開催年度ドロップダウンリスト
        ddlQpOpenFiscalYear.Text = Session(SessionComm.SESSION_SEARCH_OPEN_YEAR)

        '開催四半期ドロップダウンリスト
        ddlQpOpenQuarter.Text = Session(SessionComm.SESSION_SEARCH_OPEN_QUARTER)

        '対象オーダ
        txtQpOrder.Text = Session(SessionComm.SESSION_SEARCH_ORDER)

    End Sub

    Protected Sub btnPjInput_Click(sender As Object, e As EventArgs) Handles btnPjInput.Click
        Response.Write("<script language=javascript>window.location.href='Project.aspx'</script>")
    End Sub

    Protected Sub btnQpInput_Click(sender As Object, e As EventArgs) Handles btnQpInput.Click
        Response.Write("<script language=javascript>window.location.href='QualityProgression.aspx'</script>")
    End Sub

End Class