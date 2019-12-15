Public Class UserSearch

    Inherits System.Web.UI.Page

    ''' <summary>
    ''' ページロードイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            '本支社ドロップダウンリストデータジャック
            MethodComm.SetDropDownListValue(ddlBranch, UserSearchEntity.GetDorpDownListBranch, True)

            '空のユーザー情報
            gdvUserList.DataSource = New DataTable
            gdvUserList.DataBind()

        End If

    End Sub

    ''' <summary>
    ''' 本支社ドロップダウンリスト変更イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged

        '部ドロップダウンリストデータジャック
        MethodComm.SetDropDownListValue(ddlDept, UserSearchEntity.GetDorpDownListDept(ddlBranch.Text), True)

    End Sub

    ''' <summary>
    ''' 表示ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        'ユーザー関連の情報を取得する
        BingData()

    End Sub

    ''' <summary>
    ''' ページング
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub gridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvUserList.PageIndexChanging

        'ページNo設定
        gdvUserList.PageIndex = e.NewPageIndex

        'ユーザー関連の情報を取得する
        BingData()

    End Sub

    ''' <summary>
    ''' ユーザー関連の情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function BingData() As DataTable

        '本支社の選択値
        Dim branchCd As String = ddlBranch.Text

        '部門の選択値
        Dim deptCd As String = ddlDept.Text

        'ユーザーの値
        Dim userCd As String = txtUserCd.Text

        'ユーザー名称の値
        Dim fullNm As String = txtFullName.Text

        'ユーザーデータ
        Dim gdData As DataTable

        'DBを連接する、データの値を取得する
        gdData = UserSearchEntity.GetSearchResult(branchCd, deptCd, userCd, fullNm)

        'ユーザー一覧のバインド
        gdvUserList.DataSource = gdData
        gdvUserList.DataBind()

        Return gdData

    End Function

End Class