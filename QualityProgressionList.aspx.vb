Public Class QualityProgressionList
    Inherits System.Web.UI.Page

    '''<summary>
    ''' ページロードイベント。
    '''</summary>
    '''<param name="sender">イベントのソース</param>
    '''<param name="e">データを格納している System.EventArgs</param>
    '''<remarks>
    ''' ページをロードする時、品質推進会情報の初期検索を行う。
    '''</remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '品質推進会情報を全件検索
        searchData()
    End Sub

    '''<summary>
    ''' グリッドページングイベント。
    '''</summary>
    '''<param name="sender">イベントのソース</param>
    '''<param name="e">データを格納している System.EventArgs</param>
    '''<remarks>
    ''' グリッドをページングする時、品質推進会情報の再検索を行う。
    '''</remarks>
    Protected Sub gridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvQualityProgressionList.PageIndexChanging

        'ページNo設定
        gdvQualityProgressionList.PageIndex = e.NewPageIndex

        '品質推進会情報を取得する
        searchData()

    End Sub

    '''<summary>
    ''' 品質推進会情報検索。
    '''</summary>
    '''<remarks>
    ''' 品質推進会情報を取得する。
    '''</remarks>
    Function searchData() As DataTable

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

        'グリッドデータ
        Dim gdData As DataTable

        'DBを連接する、データの値を取得する
        gdData = QualityProgressionListEntity.GetSearchResult(parmBranch, parmDept, parmOpenYear, parmOpenQuarter, parmOrder)

        '品質推進会一覧のバインド
        gdvQualityProgressionList.DataSource = gdData
        gdvQualityProgressionList.DataBind()

        'ページをめくらない時ブランクをセットする
        If gdData.Rows.Count <= gdvQualityProgressionList.PageSize Then
            trHeader.Visible = True
        Else
            trHeader.Visible = False
        End If

        Return gdData

    End Function

End Class