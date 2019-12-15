Public Class RiskPreventionList

    Inherits System.Web.UI.Page

    ''' <summary>
    ''' ページロードイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '検索結果関連の情報を取得する
        setData()

    End Sub

    ''' <summary>
    ''' ページング
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub gridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvRiskPreventionList.PageIndexChanging

        'ページNo設定
        gdvRiskPreventionList.PageIndex = e.NewPageIndex

        '検索結果関連の情報を取得する
        setData()

    End Sub

    ''' <summary>
    ''' 検索結果関連の情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function setData() As DataTable

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

        '検索結果一覧のバインド
        gdvRiskPreventionList.DataSource = gdData
        gdvRiskPreventionList.DataBind()

        'ページをめくらない時ブランクをセットする
        If gdData.Rows.Count <= gdvRiskPreventionList.PageSize Then
            trHeader.Visible = True
        Else
            trHeader.Visible = False
        End If

        Return gdData

    End Function

End Class