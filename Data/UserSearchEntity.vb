Imports Oracle.DataAccess.Client

Public Class UserSearchEntity

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
    ''' ユーザー関連の情報を取得するSQL作成
    ''' </summary>
    ''' <param name="BranchCd">本支社の選択値</param>
    ''' <param name="DeptCd">部の選択値</param>
    ''' <param name="UserCd">ユーザーNoの値</param>
    ''' <param name="FullNm">ユーザー名称の値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSearchResult(BranchCd As String, DeptCd As String, UserCd As String, FullNm As String) As DataTable

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
            .AppendLine("SELECT A.A01M010_USER_CD, ")
            .AppendLine("       A.A01M010_FULLNAME, ")
            .AppendLine("       D.A01M002_SECT_NM AS BRANCH_NAME, ")
            .AppendLine("       B.A01M002_SECT_CD, ")
            .AppendLine("       B.A01M002_SECT_NM, ")
            .AppendLine("       C.A01M003_POSTCLS_NM, ")
            .AppendLine("       A.A01M010_ID, ")
            .AppendLine("       B.A01M002_ALLSECT_NM, ")
            .AppendLine("       B.A01M002_ID ")
            .AppendLine("  FROM A01ADMIN.A01M010_USER    A, ")
            .AppendLine("       A01ADMIN.A01M002_SECTION B, ")
            .AppendLine("       A01ADMIN.A01M003_POSTCLS C, ")
            .AppendLine("       A01ADMIN.A01M002_SECTION D ")
            .AppendLine(" WHERE A.A01M010_APLEND_YMD = 21001231 ")
            .AppendLine("   AND A.A01M010_EMP_END = 0 ")
            .AppendLine("   AND A.A01M010_SECT_CD = B.A01M002_SECT_CD ")
            .AppendLine("   AND B.A01M002_APLEND_YMD = 21001231 ")
            .AppendLine("   AND D.A01M002_APLEND_YMD = 21001231 ")
            .AppendLine("   AND A.A01M010_POSTCLS_CD = C.A01M003_POSTCLS_CD(+) ")
            .AppendLine("   AND B.A01M002_DIV_CD = D.A01M002_DIV_CD ")
            .AppendLine("   AND ((D.A01M002_ORGLYR_CD = '01') OR (D.A01M002_ORGLYR_CD = '02')) ")
        End With

        '本支社と部の選択値を判断する
        If Not String.IsNullOrEmpty(BranchCd) Then
            If String.IsNullOrEmpty(DeptCd) Then
                With strSql
                    .AppendLine("AND SUBSTR(B.A01M002_SECT_CD, 1, 2) = :BranchCd_V ")
                End With
            Else
                With strSql
                    .AppendLine("AND SUBSTR(B.A01M002_SECT_CD, 1, 4) = :DeptCd_V ")
                End With
            End If
        End If

        'ユーザーNoの値を判断する
        If Not String.IsNullOrEmpty(UserCd) Then
            With strSql
                .AppendLine("AND A.A01M010_USER_CD LIKE :UserCd_V ")
            End With
        End If

        'ユーザー名称の値を判断する
        If Not String.IsNullOrEmpty(FullNm) Then
            With strSql
                .AppendLine("AND A.A01M010_FULLNAME LIKE :FullNm_V ")
            End With
        End If

        With strSql
            .AppendLine("ORDER BY A.A01M010_USER_CD ")
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

        If Not String.IsNullOrEmpty(UserCd) Then
            'ユーザーNoの変数
            dbcmd.Parameters.Add(New OracleParameter("UserCd_V", "%" + UserCd + "%"))
        End If

        If Not String.IsNullOrEmpty(FullNm) Then
            'ユーザー名称の変数
            dbcmd.Parameters.Add(New OracleParameter("FullNm_V", "%" + FullNm + "%"))
        End If

        data = DatabaseComm.DbSearchAdapter(dbcmd)

        Return data

    End Function

End Class