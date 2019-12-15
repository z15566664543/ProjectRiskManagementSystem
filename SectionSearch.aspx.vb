Public Class SectionSearch

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
            MethodComm.SetDropDownListValue(ddlBranch, SectionSearchEntity.GetDorpDownListBranch, True)

            '空の所属情報
            gdvSectList.DataSource = New DataTable
            gdvSectList.DataBind()

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
        MethodComm.SetDropDownListValue(ddlDept, SectionSearchEntity.GetDorpDownListDept(ddlBranch.Text), True)

    End Sub

    ''' <summary>
    ''' 表示ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        '所属関連の情報を取得する
        BingData()

    End Sub

    ''' <summary>
    ''' ページング
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Protected Sub gridView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvSectList.PageIndexChanging

        'ページNo設定
        gdvSectList.PageIndex = e.NewPageIndex

        '所属関連の情報を取得する
        BingData()

    End Sub

    ''' <summary>
    ''' 所属関連の情報を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function BingData() As DataTable

        '所属データ
        Dim gdData As DataTable

        '本支社の選択値
        Dim branchCd As String = ddlBranch.Text

        '部門の選択値
        Dim deptCd As String = ddlDept.Text

        'コードの値
        Dim sectCd As String = txtSectCd.Text

        '所属名称の値
        Dim sectNm As String = txtSectNm.Text

        'DBを連接する、データの値を取得する
        gdData = SectionSearchEntity.GetSearchResult(branchCd, deptCd, sectCd, sectNm)

        '所属一覧のバインド
        gdvSectList.DataSource = gdData
        gdvSectList.DataBind()

        Return gdData

    End Function

End Class