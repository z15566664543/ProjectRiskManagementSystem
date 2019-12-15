Imports Oracle.DataAccess.Client

Public Class RiskPreventionListEntity

    ''' <summary>
    ''' リスク予防・管理検討会検索SQL作成
    ''' </summary>
    ''' <param name="sessSearchType">検索結果タイプ</param>
    ''' <param name="sessSearchBranch">本支社の選択値</param>
    ''' <param name="sessSearchDept">部門の選択値</param>
    ''' <param name="sessSearchProcess">プロセスの選択値</param>
    ''' <param name="sessSearchOpenYear">開催年の選択値</param>
    ''' <param name="sessSearchOpenMonth">開催月の選択値</param>
    ''' <param name="sessSearchOrder">オーダの入力値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSearchResult(sessSearchType As String, sessSearchBranch As String, sessSearchDept As String, sessSearchProcess As String, sessSearchOpenYear As String, sessSearchOpenMonth As String, sessSearchOrder As String) As DataTable

        'データ定義
        Dim data As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        '本支社ドロップダウンリストの選択値の上2桁
        Dim sessSearchBranch_V As String = Left(sessSearchBranch, 2)

        '部ドロップダウンリストの選択値の上4桁
        Dim sessSearchDept_V As String = Left(sessSearchDept, 4)

        'SQL作成
        With strSql
            .AppendLine("SELECT A.PROJECT_NO, ")
            .AppendLine("       C.A01M002_SECT_NM, ")
            .AppendLine("       D.PRODUCT_SECT_NM, ")
            .AppendLine("       E.PROCESS_NAME, ")
            .AppendLine("       F.DISCUSS_PHASE_NAME, ")
            .AppendLine("       D.ORDER_CD, ")
            .AppendLine("       G.A01M009_ORDER_NM, ")
            .AppendLine("       D.PROJECT_NAME_TEMP, ")
            .AppendLine("       I.A01M015_COMPY_NM, ")
            .AppendLine("       D.CUSTOMER_NAME, ")
            .AppendLine("       A.SENDER_USER_FULLNAME, ")
            .AppendLine("       A.OPEN_DATE, ")
            .AppendLine("       A.OPEN_ROUND, ")
            .AppendLine("       A.OPEN_PLACE, ")
            .AppendLine("       A.REVIEWER, ")
            .AppendLine("       A.REVIEW_REMARK, ")
            .AppendLine("       A.SEQ_NO ")
            .AppendLine("  FROM RISK_PREVENTION          A, ")
            .AppendLine("       A01ADMIN.A01M010_USER    B, ")
            .AppendLine("       A01ADMIN.A01M002_SECTION C, ")
            .AppendLine("       PROJECT                  D, ")
            .AppendLine("       PROCESS_M                E, ")
            .AppendLine("       DISCUSS_PHASE_M          F, ")
            .AppendLine("       A01ADMIN.A01M009_ORDER   G, ")
            .AppendLine("       A01ADMIN.A01M014_ORDINFO H, ")
            .AppendLine("       A01ADMIN.A01M015_COMPY   I ")
            .AppendLine(" WHERE A.CREATED_USER_ID = B.A01M010_ID ")
            .AppendLine("   AND CONCAT(SUBSTR(B.A01M010_SECT_CD, 1, 2), '000000') = ")
            .AppendLine("       C.A01M002_SECT_CD ")
            .AppendLine("   AND A.PROJECT_NO = D.PROJECT_NO ")
            .AppendLine("   AND A.PROCESS_NO = E.PROCESS_NO ")
            .AppendLine("   AND A.DISCUSS_PHASE_NO = F.DISCUSS_PHASE_NO ")
            .AppendLine("   AND D.ORDER_CD = G.A01M009_ORDER_CD(+) ")
            .AppendLine("   AND D.ORDER_CD = H.A01M014_ORDER_CD(+) ")
            .AppendLine("   AND H.A01M014_CUSTM_ID = I.A01M015_ID(+) ")
            .AppendLine("   AND C.A01M002_APLEND_YMD = 21001231 ")
        End With

        'タイプ判断
        If sessSearchType = "2" Then

            '本支社と部の選択値を判断する
            If Not String.IsNullOrEmpty(sessSearchBranch) Then

                If String.IsNullOrEmpty(sessSearchDept) Then

                    With strSql
                        .AppendLine("AND SUBSTR(B.A01M010_SECT_CD, 1, 2) = :BranchCd_V ")
                    End With

                Else

                    With strSql
                        .AppendLine("AND SUBSTR(B.A01M010_SECT_CD, 1, 4) = :DeptCd_V ")
                    End With

                End If

            End If

            'プロセスの値を判断する
            If Not String.IsNullOrEmpty(sessSearchProcess) Then

                With strSql
                    .AppendLine("AND A.PROCESS_NO = :Process_V ")
                End With

            End If

            '開催年の値を判断する
            If Not String.IsNullOrEmpty(sessSearchOpenYear) Then

                With strSql
                    .AppendLine("AND EXTRACT(year from A.OPEN_DATE) = :OpenYear_V ")
                End With

            End If

            '開催月の値を判断する
            If Not String.IsNullOrEmpty(sessSearchOpenMonth) Then

                With strSql
                    .AppendLine("AND EXTRACT(month from A.OPEN_DATE) = :OpenMonth_V ")
                End With

            End If

            'オーダの値を判断する
            If Not String.IsNullOrEmpty(sessSearchOrder) Then

                With strSql
                    .AppendLine("AND D.ORDER_CD LIKE :Order_V ")
                End With

            End If

        End If

        With strSql
            .AppendLine(" ORDER BY A.PROJECT_NO DESC, A.SEQ_NO DESC ")
        End With

        'SQL変数
        Dim dbcmd As OracleCommand = New OracleCommand(strSql.ToString)

        If Not String.IsNullOrEmpty(sessSearchBranch) Then

            If String.IsNullOrEmpty(sessSearchDept) Then

                '本支社の変数
                dbcmd.Parameters.Add(New OracleParameter("BranchCd_V", sessSearchBranch_V))

            Else

                '部の変数
                dbcmd.Parameters.Add(New OracleParameter("DeptCd_V", sessSearchDept_V))

            End If

        End If

        If Not String.IsNullOrEmpty(sessSearchProcess) Then

            'プロセスの変数
            dbcmd.Parameters.Add(New OracleParameter("Process_V", sessSearchProcess))

        End If

        If Not String.IsNullOrEmpty(sessSearchOpenYear) Then

            '開催年の変数
            dbcmd.Parameters.Add(New OracleParameter("OpenYear_V", sessSearchOpenYear))

        End If

        If Not String.IsNullOrEmpty(sessSearchOpenMonth) Then

            '開催月の変数
            dbcmd.Parameters.Add(New OracleParameter("OpenMonth_V", sessSearchOpenMonth))

        End If

        If Not String.IsNullOrEmpty(sessSearchOrder) Then

            'オーダの変数
            dbcmd.Parameters.Add(New OracleParameter("Order_V", "%" + sessSearchOrder + "%"))

        End If

        data = DatabaseComm.DbSearchAdapter(dbcmd)

        'データが存在する
        If data.Rows.Count > 0 Then

            'ループ
            For i As Integer = 0 To data.Rows.Count - 1

                If i = data.Rows.Count Then
                    Exit For
                End If

                '"A01M009_ORDER_NM"の値設定
                If String.IsNullOrEmpty(data.Rows(i).Item("ORDER_CD").ToString) Then
                    data.Rows(i).Item("A01M009_ORDER_NM") = data.Rows(i).Item("PROJECT_NAME_TEMP")
                End If

                '"A01M015_COMPY_NM"の値設定
                If String.IsNullOrEmpty(data.Rows(i).Item("ORDER_CD").ToString) Then
                    data.Rows(i).Item("A01M015_COMPY_NM") = data.Rows(i).Item("CUSTOMER_NAME")
                End If

                '"REVIEWER"の値設定
                If Not String.IsNullOrEmpty(data.Rows(i).Item("REVIEWER").ToString) And Not String.IsNullOrEmpty(data.Rows(i).Item("REVIEW_REMARK").ToString) Then

                    data.Rows(i).Item("REVIEWER") = "○"

                Else

                    data.Rows(i).Item("REVIEWER") = ""

                End If

                '"OPEN_ROUND"の値設定
                If Not String.IsNullOrEmpty(data.Rows(i).Item("OPEN_ROUND").ToString) Then

                    data.Rows(i).Item("OPEN_ROUND") = "第" + data.Rows(i).Item("OPEN_ROUND") + "回目"

                End If

            Next

        End If

        Return data

    End Function

End Class
