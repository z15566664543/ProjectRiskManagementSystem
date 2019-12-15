Imports Oracle.DataAccess.Client

Public Class QualityProgressionListEntity

    ''' <summary>
    ''' 品質推進会情報を取得する
    ''' </summary>
    ''' <param name="parmBranch">支社</param>
    ''' <param name="parmDept">部門</param>
    ''' <param name="parmOpenYear">開催年度</param>
    ''' <param name="parmOpenQuarter">開催四半期</param>
    ''' <param name="parmOrder">オーダ</param>
    ''' <returns>品質推進会情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetSearchResult(parmBranch As String, parmDept As String, parmOpenYear As String, parmOpenQuarter As String, parmOrder As String) As DataTable

        '品質推進会情報
        Dim mainData As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        '支社の上2桁
        Dim parmBranchHead As String = Left(parmBranch, 2)

        '部門の上4桁 
        Dim parmDeptHead As String = Left(parmDept, 4)

        'I. 対象案件オーダ以外の表示項目を検索するSQL
        'SQL作成
        With strSql
            .AppendLine("SELECT A.PROGRESSION_NO, ")
            .AppendLine("       A.CONFERENCE_NAME, ")
            .AppendLine("       A.BRANCH_NAME, ")
            .AppendLine("       A.TARGET_SECT_NAME, ")
            .AppendLine("       '' AS ORDER_CD, ")
            .AppendLine("       A.SENDER_USER_NAME, ")
            .AppendLine("       A.OPEN_DATE, ")
            .AppendLine("       A.OPEN_PLACE, ")
            .AppendLine("       NVL2(A.REVIEWER, NVL2(A.CONFERENCE_CONTENT, '○', ''), '')　AS CREATED ")
            .AppendLine("  FROM QUALITY_PROGRESSION A, ")
            .AppendLine("       A01ADMIN.A01M002_SECTION B ")
            .AppendLine(" WHERE A.TARGET_SECT_ID = B.A01M002_ID ")
        End With

        '絞り込み条件：支社、部門
        If Not String.IsNullOrEmpty(parmBranch) Then
            If String.IsNullOrEmpty(parmDept) Then
                With strSql
                    .AppendLine("AND SUBSTR(B.A01M002_SECT_CD, 1, 2) = :Branch_V ")
                End With
            Else
                With strSql
                    .AppendLine("AND SUBSTR(B.A01M002_SECT_CD, 1, 4) = :Dept_V ")
                End With
            End If
        End If

        '絞り込み条件：開催年度
        If Not String.IsNullOrEmpty(parmOpenYear) Then
            With strSql
                .AppendLine("AND A.OPEN_FISCAL_YEAR = :OpenYear_V ")
            End With
        End If

        '絞り込み条件：開催四半期
        If Not String.IsNullOrEmpty(parmOpenQuarter) Then
            With strSql
                .AppendLine("AND A.OPEN_QUARTER = :OpenQuarter_V ")
            End With
        End If

        '並び順
        With strSql
            .AppendLine("ORDER BY A.OPEN_DATE DESC")
        End With

        'SQL変数
        Dim dbcmd As OracleCommand = New OracleCommand(strSql.ToString)

        'SQL変数：支社、部門
        If Not String.IsNullOrEmpty(parmBranch) Then
            If String.IsNullOrEmpty(parmDept) Then
                dbcmd.Parameters.Add(New OracleParameter("Branch_V", parmBranchHead))
            Else
                dbcmd.Parameters.Add(New OracleParameter("Dept_V", parmDeptHead))
            End If
        End If

        'SQL変数：開催年度
        If Not String.IsNullOrEmpty(parmOpenYear) Then
            dbcmd.Parameters.Add(New OracleParameter("OpenYear_V", parmOpenYear))
        End If

        'SQL変数：開催四半期
        If Not String.IsNullOrEmpty(parmOpenQuarter) Then
            dbcmd.Parameters.Add(New OracleParameter("OpenQuarter_V", parmOpenQuarter))
        End If

        '検索処理を行う
        mainData = DatabaseComm.DbSearchAdapter(dbcmd)

        'II.対象案件オーダの表示項目を検索するSQL
        If mainData.Rows.Count > 0 Then

            'オーダ情報
            Dim orderData As DataTable

            'Ⅰの検索結果よりループする
            For i As Integer = 0 To mainData.Rows.Count - 1

                If i = mainData.Rows.Count Then
                    Exit For
                End If

                'SQL作成
                strSql = New StringBuilder
                With strSql
                    .AppendLine("SELECT LISTAGG (C.ORDER_CD, ' ') WITHIN GROUP (ORDER BY C.ORDER_CD) CONCAT_ORDER_CD ")
                    .AppendLine("  FROM QUALITY_PROGRESSION A, ")
                    .AppendLine("       QUALITY_PROGRESSION_RELATE B, ")
                    .AppendLine("       PROJECT C ")
                    .AppendLine(" WHERE A.PROGRESSION_NO = :ProgessionNo_V ")
                    .AppendLine("   AND A.PROGRESSION_NO = B.PROGRESSION_NO ")
                    .AppendLine("   AND B.PROJECT_NO = C.PROJECT_NO ")
                    .AppendLine("   AND C.PROJECT_COMPLETE_FLG = '0' ")
                End With

                'SQL変数
                dbcmd = New OracleCommand(strSql.ToString)
                dbcmd.Parameters.Add(New OracleParameter("ProgessionNo_V", mainData.Rows(i)("PROGRESSION_NO").ToString))

                '検索処理を行う
                orderData = DatabaseComm.DbSearchAdapter(dbcmd)

                'オーダ情報を設定する
                mainData.Rows(i)("ORDER_CD") = orderData.Rows(0)("CONCAT_ORDER_CD").ToString


                '絞り込み条件：オーダ
                If Not String.IsNullOrEmpty(parmOrder) Then
                    If orderData.Rows(0)("CONCAT_ORDER_CD").ToString.IndexOf(parmOrder) < 0 Then
                        mainData.Rows.Remove(mainData.Rows(i))
                        i = i - 1
                    End If
                End If

            Next

        End If

        Return mainData

    End Function

End Class