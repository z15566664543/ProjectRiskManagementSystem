Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class OrderSearch
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
            MethodComm.SetDropDownListValue(ddlBranch, OrderSearchEntity.GetOrderSearchBranch, True)

            '空のオーダの情報
            gdvOrderList.DataSource = New DataTable
            gdvOrderList.DataBind()

        End If

    End Sub

    ''' <summary>
    ''' 本支社ドロップダウンリスト変更イベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged

        '本支社の選択値
        Dim branchText As String = ddlBranch.Text

        '部ドロップダウンリストデータジャック
        MethodComm.SetDropDownListValue(ddlDept, OrderSearchEntity.GetOrderSearchDept(branchText), True)

    End Sub

    ''' <summary>
    ''' 表示ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        'オーダ関連の情報を取得する
        BingData()

    End Sub

    ''' <summary>
    ''' ページング
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub gdvOrderList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvOrderList.PageIndexChanging

        'ページNo設定
        gdvOrderList.PageIndex = e.NewPageIndex

        'オーダ関連の情報を取得する
        BingData()

    End Sub

    ''' <summary>
    ''' オーダ関連の情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function BingData() As DataTable

        '本支社の選択値
        Dim branchCd As String = ddlBranch.Text

        '部門の選択値
        Dim deptCd As String = ddlDept.Text

        'オーダの値
        Dim orderCd As String = txtOrderCd.Text

        'オーダ名称の値
        Dim orderNm As String = txtOrderNm.Text

        'オーダデータ
        Dim gdData As DataTable

        'DBを連接する、データの値を取得する
        gdData = OrderSearchEntity.GetOrderSearch(branchCd, deptCd, orderCd, orderNm)

        'オーダ一覧のバインド
        gdvOrderList.DataSource = gdData
        gdvOrderList.DataBind()

        Return gdData

    End Function
End Class