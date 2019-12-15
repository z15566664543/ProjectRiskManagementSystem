Imports Oracle.DataAccess.Client


Public Class OrderSearchEntity

    ''' <summary>
    ''' 本支社検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderSearchBranch() As String

        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT A01M002_SECT_NM, A01M002_SECT_CD")
            .AppendLine("  FROM A01ADMIN.A01M002_SECTION")
            .AppendLine(" WHERE (A01M002_APLEND_YMD = 21001231)")
            .AppendLine("   AND ((A01M002_ORGLYR_CD = '01')")
            .AppendLine("    OR (A01M002_ORGLYR_CD = '02'))")
            .AppendLine(" ORDER BY A01M002_SECT_CD")
        End With

        Return strSql.ToString

    End Function

    ''' <summary>
    ''' 部検索SQL作成
    ''' </summary>
    ''' <param name="BranchText">本支社の選択値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderSearchDept(BranchText As String) As String

        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT A01M002_SECT_NM, A01M002_SECT_CD")
            .AppendLine("  FROM A01ADMIN.A01M002_SECTION")
            .AppendLine(" WHERE (A01M002_APLEND_YMD = 21001231)")
            .AppendLine("   AND (A01M002_ORGLYR_CD = '03')")
            .AppendLine("   AND SUBSTR(A01M002_SECT_CD, 1, 2) = SUBSTR('" + BranchText + "', 1, 2)")
            .AppendLine(" ORDER BY A01M002_SECT_CD")
        End With

        Return strSql.ToString

    End Function

    ''' <summary>
    ''' オーダ関連の情報を取得するSQL作成
    ''' </summary>
    ''' <param name="branchCd">本支社の選択値</param>
    ''' <param name="deptCd">部の選択値</param>
    ''' <param name="orderCd">オーダNoの値</param>
    ''' <param name="orderNm">オーダ名称の値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderSearch(branchCd As String, deptCd As String, orderCd As String, orderNm As String) As DataTable

        'データ定義
        Dim data As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        '本支社ドロップダウンリストの選択値の上2桁
        Dim branchCd2 As String = Left(branchCd, 2)

        '部ドロップダウンリストの選択値の上4桁
        Dim deotCd4 As String = Left(deptCd, 4)

        'SQL作成
        With strSql
            .AppendLine("SELECT ")
            .AppendLine("       A.A01M009_ORDER_CD,")
            .AppendLine("       A.A01M009_ORDER_NM,")
            .AppendLine("       E.A01M015_COMPY_NM,")
            .AppendLine("       B.A01M014_JYUCHU_CRR,")
            .AppendLine("       B.A01M014_NOKI_YMD,")
            .AppendLine("       C.A01M002_ALLSECT_NM,")
            .AppendLine("       D.A01M010_FULLNAME,")
            .AppendLine("       (CASE")
            .AppendLine("         WHEN B.A01M014_SIMEKIRI_STAT = '0' THEN")
            .AppendLine("          '仕掛'")
            .AppendLine("         WHEN B.A01M014_SIMEKIRI_STAT = '9' THEN")
            .AppendLine("          '締切'")
            .AppendLine("         WHEN B.A01M014_SIMEKIRI_STAT = '2' THEN")
            .AppendLine("          '仕掛(進行基準)'")
            .AppendLine("       END)　AS A01M014_SIMEKIRI_STAT")
            .AppendLine(" FROM  A01ADMIN.A01M009_ORDER   A,")
            .AppendLine("       A01ADMIN.A01M014_ORDINFO B,")
            .AppendLine("       A01ADMIN.A01M002_SECTION C,")
            .AppendLine("       A01ADMIN.A01M010_USER    D,")
            .AppendLine("       A01ADMIN.A01M015_COMPY   E")
            .AppendLine(" WHERE A.A01M009_ORDER_CD = B.A01M014_ORDER_CD")
            .AppendLine("   AND B.A01M014_JYUCHU_SECT_ID = C.A01M002_ID")
            .AppendLine("   AND B.A01M014_JYUCHU_USER_ID = D.A01M010_ID")
            .AppendLine("   AND B.A01M014_CUSTM_ID = E.A01M015_ID")
        End With

        '本支社と部の選択値を判断する
        If Not String.IsNullOrEmpty(branchCd) Then
            If String.IsNullOrEmpty(deptCd) Then
                With strSql
                    .AppendLine("AND SUBSTR(C.A01M002_SECT_CD, 1, 2) = :BranchCd ")
                End With
            Else
                With strSql
                    .AppendLine("AND SUBSTR(C.A01M002_SECT_CD, 1, 4) =:DeptCd ")
                End With
            End If
        End If

        'オーダNoの値を判断する
        If Not String.IsNullOrEmpty(orderCd) Then
            With strSql
                .AppendLine("AND A.A01M009_ORDER_CD LIKE :OrderCd ")
            End With
        End If

        'オーダ名称の値を判断する
        If Not String.IsNullOrEmpty(orderNm) Then
            With strSql
                .AppendLine("AND A.A01M009_ORDER_NM LIKE :OrderNm ")
            End With
        End If

        With strSql
            .AppendLine("ORDER BY A.A01M009_ORDER_CD")
        End With

        'SQL変数
        Dim dbcmd As OracleCommand = New OracleCommand(strSql.ToString)

        If Not String.IsNullOrEmpty(branchCd) Then
            If String.IsNullOrEmpty(deptCd) Then
                '本支社の変数
                dbcmd.Parameters.Add(New OracleParameter("BranchCd", branchCd2))
            Else
                '部の変数
                dbcmd.Parameters.Add(New OracleParameter("DeptCd", deotCd4))
            End If
        End If

        If Not String.IsNullOrEmpty(orderCd) Then
            'オーダNoの変数
            dbcmd.Parameters.Add(New OracleParameter("OrderCd", "%" + orderCd + "%"))
        End If

        If Not String.IsNullOrEmpty(orderNm) Then
            'オーダ名称の変数
            dbcmd.Parameters.Add(New OracleParameter("OrderNm", "%" + orderNm + "%"))
        End If

        Dim a As String = dbcmd.ToString

        data = DatabaseComm.DbSearchAdapter(dbcmd)

        Return data

    End Function

End Class
