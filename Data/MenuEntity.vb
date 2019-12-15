Public Class MenuEntity

    ''' <summary>
    ''' 支社ドロップダウンリストデータを取得する共通方法
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuBranch() As String
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
    ''' 部門ドロップダウンリストデータを取得する共通方法
    ''' </summary>
    ''' <param name="BranchText">支社の値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuDept(BranchText As String) As String
        'SQL定義
        Dim strSql As New StringBuilder

        ''SQL作成
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
    ''' プロセスドロップダウンリストデータを取得する方法
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuProcess() As String
        'SQL定義
        Dim strSql As New StringBuilder

        ''SQL作成
        With strSql
            .AppendLine("SELECT")
            .AppendLine(" PROCESS_NO,")
            .AppendLine(" PROCESS_NAME")
            .AppendLine(" FROM PROCESS_M")
            .AppendLine(" ORDER BY PROCESS_NO")
        End With
        Return strSql.ToString
    End Function

    ''' <summary>
    ''' 開催年ドロップダウンリストを取得する方法
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuRpiOpenYear() As String
        'SQL定義
        Dim strSql As New StringBuilder

        ''SQL作成
        With strSql
            .AppendLine("SELECT")
            .AppendLine(" DISTINCT TO_CHAR(OPEN_DATE, 'yyyy')")
            .AppendLine("  AS OPEN_YEAR")
            .AppendLine("  FROM RISK_PREVENTION")
            .AppendLine(" WHERE TO_CHAR(OPEN_DATE, 'yyyy') IS NOT NULL")
            .AppendLine(" ORDER BY OPEN_YEAR DESC")
        End With
        Return strSql.ToString
    End Function

    ''' <summary>
    ''' 開催年度ドロップダウンリストを取得する方法
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetMenuQpOpenFiscalYear() As String
        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT OPEN_FISCAL_YEAR")
            .AppendLine("  FROM QUALITY_PROGRESSION")
            .AppendLine(" WHERE TO_CHAR(OPEN_DATE, 'yyyy') IS NOT NULL")
            .AppendLine(" GROUP BY OPEN_FISCAL_YEAR")
            .AppendLine(" ORDER BY OPEN_FISCAL_YEAR DESC")
        End With
        Return strSql.ToString
    End Function

    ''' <summary>
    ''' システム管理者テーブルを取得する方法
    ''' </summary>
    ''' <param name="userID">ドメインアカウント</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAdminSession(userID As String) As DataTable
        'データ定義
        Dim data As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT COUNT(*) AS ADMIN_CNT")
            .AppendLine("  FROM SYSTEM_ADMIN")
            .AppendLine(" WHERE (ADMIN_FLG = '1')")
            .AppendLine("   AND (LOGIN_ID = '" + userID + "')")
        End With

        'DBを連接する、データの値を取得する
        data = DatabaseComm.DbSearchAdapter(strSql.ToString)
        For i As Integer = 0 To data.Rows.Count - 1
            If String.IsNullOrEmpty(data.Rows(i).Item(0).ToString) Then

            End If
        Next
        Return data
    End Function

    ''' <summary>
    ''' 社員マスタを取得する方法
    ''' </summary>
    ''' <param name="userID">ドメインアカウント</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserSession(userID As String) As DataTable
        'データ定義
        Dim data As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        'SQL作成
        With strSql
            .AppendLine("SELECT A01M010_ID,")
            .AppendLine("       A01M010_USER_CD,")
            .AppendLine("       A01M010_SECT_CD,")
            .AppendLine("       A01M010_AD5POSTCLS_CD,")
            .AppendLine("       A01M010_FULLNAME")
            .AppendLine("  FROM A01ADMIN.A01M010_USER")
            .AppendLine(" WHERE (A01M010_LOGIN_ID = '" + userID + "')")
            .AppendLine("   AND (A01M010_APLEND_YMD = 21001231)")
        End With

        'DBを連接する、データの値を取得する
        data = DatabaseComm.DbSearchAdapter(strSql.ToString)

        Return data
    End Function
End Class
