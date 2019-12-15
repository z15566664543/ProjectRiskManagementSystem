Imports Oracle.DataAccess.Client

Public Class SectionSearchEntity

    ''' <summary>
    ''' 本支社検索SQL作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDorpDownListBranch() As String

        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT A01M002_SECT_NM, A01M002_SECT_CD ")
            .AppendLine("  FROM A01ADMIN.A01M002_SECTION ")
            .AppendLine(" WHERE (A01M002_APLEND_YMD = 21001231) ")
            .AppendLine("   AND ((A01M002_ORGLYR_CD = '01') OR (A01M002_ORGLYR_CD = '02')) ")
            .AppendLine(" ORDER BY A01M002_SECT_CD")
        End With

        Return strSql.ToString

    End Function

    ''' <summary>
    ''' 部検索SQL作成
    ''' </summary>
    ''' <param name="branchCd">本支社の選択値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDorpDownListDept(branchCd As String) As String

        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT A01M002_SECT_NM, A01M002_SECT_CD ")
            .AppendLine("  FROM A01ADMIN.A01M002_SECTION ")
            .AppendLine(" WHERE (A01M002_APLEND_YMD = 21001231) ")
            .AppendLine("   AND (A01M002_ORGLYR_CD = '03') ")
            .AppendLine("   AND SUBSTR(A01M002_SECT_CD, 1, 2) = SUBSTR( '" + branchCd + "', 1, 2) ")
            .AppendLine(" ORDER BY A01M002_SECT_CD")
        End With

        Return strSql.ToString

    End Function

    ''' <summary>
    ''' 所属関連の情報を取得するSQL作成
    ''' </summary>
    ''' <param name="BranchCd">本支社の選択値</param>
    ''' <param name="DeptCd">部の選択値</param>
    ''' <param name="SectCd">所属Noの値</param>
    ''' <param name="SectNm">所属名称の値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSearchResult(BranchCd As String, DeptCd As String, SectCd As String, SectNm As String) As DataTable

        'データ定義
        Dim data As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        '本支社ドロップダウンリストの選択値の上2桁
        Dim branchCd2 As String = Left(BranchCd, 2)

        '部ドロップダウンリストの選択値の上4桁
        Dim deptCd4 As String = Left(DeptCd, 4)

        'SQL作成
        With strSql
            .AppendLine("SELECT A.A01M002_SECT_CD, ")
            .AppendLine("       B.A01M002_SECT_NM AS BRANCH_NM, ")
            .AppendLine("       A.A01M002_SECT_NM, ")
            .AppendLine("       A.A01M002_SECT_ALIAS, ")
            .AppendLine("       A.A01M002_ID, ")
            .AppendLine("       A.A01M002_ALLSECT_NM ")
            .AppendLine("  FROM A01ADMIN.A01M002_SECTION A, A01ADMIN.A01M002_SECTION B")
            .AppendLine(" WHERE A.A01M002_APLEND_YMD = 21001231 ")
            .AppendLine("   AND B.A01M002_APLEND_YMD = 21001231 ")
            .AppendLine("   AND A.A01M002_DIV_CD = B.A01M002_DIV_CD ")
            .AppendLine("   AND ((B.A01M002_ORGLYR_CD = '01') OR (B.A01M002_ORGLYR_CD = '02')) ")
        End With

        '本支社と部の選択値を判断する
        If Not String.IsNullOrEmpty(BranchCd) Then
            If String.IsNullOrEmpty(DeptCd) Then
                With strSql
                    .AppendLine("AND SUBSTR(A.A01M002_SECT_CD, 1, 2) = :BranchCd_V ")
                End With
            Else
                With strSql
                    .AppendLine("AND SUBSTR(A.A01M002_SECT_CD, 1, 4) = :DeptCd_V ")
                End With
            End If
        End If

        '所属コードの値を判断する
        If Not String.IsNullOrEmpty(SectCd) Then
            With strSql
                .AppendLine("AND A.A01M002_SECT_CD LIKE :SectCd_V ")
            End With
        End If

        '所属名称の値を判断する
        If Not String.IsNullOrEmpty(SectNm) Then
            With strSql
                .AppendLine("AND A.A01M002_SECT_NM LIKE :SectNm_V ")
            End With
        End If

        With strSql
            .AppendLine("ORDER BY A01M002_SECT_CD ")
        End With

        'SQL変数
        Dim dbcmd As OracleCommand = New OracleCommand(strSql.ToString)

        If Not String.IsNullOrEmpty(BranchCd) Then
            If String.IsNullOrEmpty(DeptCd) Then
                '本支社の変数
                dbcmd.Parameters.Add(New OracleParameter("BranchCd_V", branchCd2))
            Else
                '部の変数
                dbcmd.Parameters.Add(New OracleParameter("DeptCd_V", deptCd4))
            End If
        End If

        If Not String.IsNullOrEmpty(SectCd) Then
            '所属コードの変数
            dbcmd.Parameters.Add(New OracleParameter("SectCd_V", "%" + SectCd + "%"))
        End If

        If Not String.IsNullOrEmpty(SectNm) Then
            '所属名称の変数
            dbcmd.Parameters.Add(New OracleParameter("SectNm_V", "%" + SectNm + "%"))
        End If

        data = DatabaseComm.DbSearchAdapter(dbcmd)

        Return data

    End Function

End Class