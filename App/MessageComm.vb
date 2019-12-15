''' <summary>
''' メッセージ共通部品
''' </summary>
''' <remarks></remarks>
Public Class MessageComm
    ''' <summary>
    ''' XMLファイルから、メッセージ情報読込
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetMessageInfoXML() As DataSet
        'XMLパス
        Dim strPath As String
        'メッセージ情報データセット
        Dim ds As DataSet = New DataSet
        'XMLパス情報読込
        strPath = HttpContext.Current.Server.MapPath("Ext\MessageContext_JP.xml")
        'XMLファイルから、メッセージ情報読込
        ds.ReadXml(strPath)

        Return ds
    End Function

    ''' <summary>
    ''' 指定メッセージ取得
    ''' </summary>
    ''' <param name="messageType">メッセージタイプ 0:異常 1:チェックメッセージ 2:正常メッセージ</param>
    ''' <param name="messageId">メッセージID</param>
    ''' <param name="messageParamList">メッセージパラメータ</param>
    ''' <returns>メッセージ内容</returns>
    ''' <remarks>異常の場合、String.Emptyを戻る</remarks>
    Public Shared Function GetMessageContext(ByVal messageType As String, ByVal messageId As String, ByVal messageParamList As List(Of String)) As String
        'メッセージ内容
        Dim strMessage As String = String.Empty
        'テーブル名
        Dim strTableNm As String = String.Empty
        '項目名
        Dim strColumnNm As String = String.Empty
        'メッセージデータセット
        Dim dsMessage As DataSet = New DataSet

        'メッセージタイプより、テーブル名、項目名を設定
        Select Case messageType
            Case 0
                strTableNm = "ExceptionMessage"
                strColumnNm = "EXCEPTION" + messageId
            Case 1
                strTableNm = "CheckMessage"
                strColumnNm = "CHECK" + messageId
            Case 2
                strTableNm = "InfoMessage"
                strColumnNm = "INFO" + messageId
        End Select

        'メッセージ情報を取得する
        dsMessage = GetMessageInfoXML()

        Try
            Dim dr As DataRow
            Dim strMessTemp As String

            dr = dsMessage.Tables(strTableNm).Rows(0)
            strMessTemp = dr(strColumnNm).ToString
            If messageParamList Is Nothing Then
                'メッセージ内容を戻る
                Return strMessTemp
            End If

            'メッセージパラメータを置き換える
            For i = 0 To messageParamList.Count - 1
                strMessTemp = strMessTemp.Replace("｛" + i.ToString + "｝", messageParamList(i))
            Next
            strMessage = strMessTemp

        Catch ex As Exception
            '異常の場合、String.Emptyを戻る
            Return String.Empty

        End Try
        'メッセージ内容を戻る
        Return strMessage

    End Function

End Class
