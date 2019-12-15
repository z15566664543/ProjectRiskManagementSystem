Public Class MethodComm
    ''' <summary>
    ''' ラジオボタンの値は設定
    ''' </summary>
    ''' <param name="rdoList">ラジオボタン</param>
    ''' <param name="strSql">検索SQL</param>
    ''' <remarks></remarks>
    Public Shared Sub SetRadioListValue(ByRef rdoList As RadioButtonList, ByVal strSql As String)
        Dim dtValue As DataTable = New DataTable()

        dtValue = DatabaseComm.DbSearchAdapter(strSql)
        rdoList.DataSource = dtValue
        rdoList.DataMember = "ds"
        rdoList.DataBind()

    End Sub

    ''' <summary>
    ''' ドロップダウンリストの値は設定
    ''' </summary>
    ''' <param name="ddList">ドロップダウンリスト</param>
    ''' <param name="strSql">検索SQL</param>
    ''' <param name="bolAdd">静的項目を追加するフラグ</param>
    ''' <remarks></remarks>
    Public Shared Sub SetDropDownListValue(ByRef ddList As DropDownList, ByVal strSql As String, ByVal bolAdd As Boolean)
        Dim dtValue As DataTable = New DataTable()
        'データを取得
        dtValue = DatabaseComm.DbSearchAdapter(strSql)

        '静的項目を追加する
        If bolAdd Then
            '一時データテーブル作成
            Dim copyTable As DataTable = dtValue.Clone
            '一時データテーブルのデータをクリア
            copyTable.Clear()
            Dim drNew = copyTable.NewRow
            copyTable.Rows.Add(drNew)

            For i = 0 To dtValue.Rows.Count - 1
                copyTable.Rows.Add(dtValue.Rows(i).ItemArray)
            Next
            'データ設定
            ddList.DataSource = copyTable
        Else
            'データ設定
            ddList.DataSource = dtValue

        End If

        ddList.DataMember = "ds"
        ddList.DataBind()

    End Sub
End Class
