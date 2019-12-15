Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

Public Class ProjectList
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' ページロードイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        searchData()
    End Sub

    ''' <summary>
    ''' ページング
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub gdvPjList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvPjList.PageIndexChanging
        'ページNo設定
        gdvPjList.PageIndex = e.NewPageIndex

        '案件検索結果を取得する。
        searchData()
    End Sub

    ''' <summary>
    ''' 案件検索結果
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 案件検索結果を取得する。
    ''' </remarks>
    Function searchData() As DataTable

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

        '案件検索結果一覧のバインド
        gdvPjList.DataSource = gdData
        gdvPjList.DataBind()
        'gdvPjList.HeaderRow.Style.Add("word-break", "keep-all")

        'ページをめくらない時ブランクをセットする
        If gdData.Rows.Count <= gdvPjList.PageSize Then
            trHeader.Visible = True
        Else
            trHeader.Visible = False
        End If

        Return gdData

    End Function


    ''' <summary>
    ''' ヘッダー制作
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks>
    ''' 制作複数ヘッダー
    ''' </remarks>
    Protected Sub gdvPjList_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdvPjList.RowCreated
        Select Case (e.Row.RowType)
            Case DataControlRowType.Header

                'ヘッダー定義
                Dim Header As TableCellCollection = e.Row.Cells
                Header.Clear()

                'ヘッダー作成
                Header.Add(New TableHeaderCell())
                Header(0).Attributes.Add("rowspan", "2")
                Header(0).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(0).Text = "案件番号"

                Header.Add(New TableHeaderCell())
                Header(1).Attributes.Add("rowspan", "2")
                Header(1).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(1).Text = "支社<br/>(起票支社)"

                Header.Add(New TableHeaderCell())
                Header(2).Attributes.Add("rowspan", "2")
                Header(2).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(2).Text = "製造部門"

                Header.Add(New TableHeaderCell())
                Header(3).Attributes.Add("rowspan", "2")
                Header(3).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(3).Text = "プロセス"

                Header.Add(New TableHeaderCell())
                Header(4).Attributes.Add("rowspan", "2")
                Header(4).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(4).Text = "オーダ"

                Header.Add(New TableHeaderCell())
                Header(5).Attributes.Add("rowspan", "2")
                Header(5).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(5).Text = "工事名称"

                Header.Add(New TableHeaderCell())
                Header(6).Attributes.Add("rowspan", "2")
                Header(6).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(6).Text = "顧客"

                Header.Add(New TableHeaderCell())
                Header(7).Attributes.Add("rowspan", "2")
                Header(7).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(7).Text = "受注金額"

                Header.Add(New TableHeaderCell())
                Header(8).Attributes.Add("rowspan", "2")
                Header(8).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(8).Text = "納期日"

                Header.Add(New TableHeaderCell())
                Header(9).Attributes.Add("rowspan", "2")
                Header(9).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(9).Text = "顧客区分"

                Header.Add(New TableHeaderCell())
                Header(10).Attributes.Add("colspan", "2")
                Header(10).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(10).Text = "支社間取引"

                Header.Add(New TableHeaderCell())
                Header(11).Attributes.Add("colspan", "3")
                Header(11).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(11).Text = "リスク予防管理対象"

                Header.Add(New TableHeaderCell())
                Header(12).Attributes.Add("rowspan", "2")
                Header(12).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(12).Text = "管理区分"

                Header.Add(New TableHeaderCell())
                Header(13).Attributes.Add("rowspan", "2")
                Header(13).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(13).Text = "案件タイプ"

                Header.Add(New TableHeaderCell())
                Header(14).Attributes.Add("colspan", "4")
                Header(14).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(14).Text = "リスク予防管理検討会(回数)"

                Header.Add(New TableHeaderCell())
                Header(15).Attributes.Add("rowspan", "2")
                Header(15).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(15).Text = "完了</th></tr><tr>"

                Header.Add(New TableHeaderCell())
                Header(16).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(16).Text = "有無"

                Header.Add(New TableHeaderCell())
                Header(17).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(17).Text = "取引先支社"

                Header.Add(New TableHeaderCell())
                Header(18).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(18).Text = "500万"

                Header.Add(New TableHeaderCell())
                Header(19).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(19).Text = "初品"

                Header.Add(New TableHeaderCell())
                Header(20).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(20).Text = "特殊"

                Header.Add(New TableHeaderCell())
                Header(21).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(21).Text = "営業プロセス<br/>(原価)"

                Header.Add(New TableHeaderCell())
                Header(22).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(22).Text = "営業プロセス<br/>(見積)"

                Header.Add(New TableHeaderCell())
                Header(23).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(23).Text = "購買<br/>プロセス"

                Header.Add(New TableHeaderCell())
                Header(24).Attributes.Add("class", "List_GridViewPjHeaderStyle")
                Header(24).Text = "設計・開発<br/>プロセス"
        End Select

    End Sub
End Class