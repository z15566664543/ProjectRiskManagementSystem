Imports Oracle.DataAccess.Client

Public Class ProjectListEntity
    ''' <summary>
    ''' 案件検索結果情報を取得する
    ''' </summary>
    ''' <param name="searchBranch">支社</param>
    ''' <param name="searchDept">部門</param>
    ''' <param name="searchOrder">オーダ</param>
    ''' <param name="searchCustomer">顧客</param>
    ''' <param name="searchRpmTypeA">管理区分A</param>
    ''' <param name="searchRpmTypeB">管理区分B</param>
    ''' <param name="searchCompCategory">完了区分</param>
    ''' <returns>案件検索結果情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectList(searchBranch As String, searchDept As String, searchOrder As String, searchCustomer As String, searchRpmTypeA As String, searchRpmTypeB As String, searchCompCategory As String) As DataTable
        '品質推進会情報
        Dim mainData As DataTable

        'SQL定義
        Dim strSql As New StringBuilder

        '支社の上2桁
        Dim searchBranchHead As String = Left(searchBranch, 2)

        '部門の上4桁 
        Dim searchDeptHead As String = Left(searchDept, 4)

        'I. 案件番号～案件タイプまでの表示項目を検索するSQL
        'SQL作成
        With strSql
            .AppendLine("SELECT A.PROJECT_NO,")
            .AppendLine("       C.A01M002_SECT_NM,")
            .AppendLine("       A.PRODUCT_SECT_NM,")
            .AppendLine("       D.PROCESS_NAME,")
            .AppendLine("       A.ORDER_CD,")
            .AppendLine("       E.A01M009_ORDER_NM,")
            .AppendLine("       G.A01M015_COMPY_NM,")
            .AppendLine("       F.A01M014_JYUCHU_CRR,")
            .AppendLine("       TO_CHAR(TO_DATE(F.A01M014_NOKI_YMD, 'yyyymmdd'), 'yyyy/mm/dd') AS A01M014_NOKI_YMD,")
            .AppendLine("       H.CUSTOMER_TYPE_NAME,")

            .AppendLine("       (case")
            .AppendLine("         when A.BRANCH_TRANSACTION_FLG = '0' then")
            .AppendLine("          '無'")
            .AppendLine("         else")
            .AppendLine("          '有'")
            .AppendLine("       end) 　as BRANCH_TRANSACTION_FLG,")

            .AppendLine("       A.SUPPORT_BRANCH_NM,")

            .AppendLine("       (case")
            .AppendLine("         when A.RPM_500MIL_FLG = '1' then")
            .AppendLine("          '○'")
            .AppendLine("         else")
            .AppendLine("          ''")
            .AppendLine("       end) RPM_500MIL_FLG,")

            .AppendLine("       (case")
            .AppendLine("       when A.RPM_FIRST_PRODUCT_FLG = '1' then")
            .AppendLine("          '○'")
            .AppendLine("         else")
            .AppendLine("          ''")
            .AppendLine("       end)RPM_FIRST_PRODUCT_FLG,")

            .AppendLine("       (case")
            .AppendLine("         when A.RPM_SPECIAL_PRODUCT_FLG = '1' then")
            .AppendLine("          '○'")
            .AppendLine("         else")
            .AppendLine("          ''")
            .AppendLine("       end) RPM_SPECIAL_PRODUCT_FLG,")

            .AppendLine("       A.RPM_TYPE,")
            .AppendLine("       I.PROJECT_TYPE_NAME,")

            .AppendLine("       (case")
            .AppendLine("         when A.PROJECT_COMPLETE_FLG = '1' then")
            .AppendLine("          '完了'")
            .AppendLine("         else")
            .AppendLine("          ''")
            .AppendLine("       end) PROJECT_COMPLETE_FLG,")

            .AppendLine("       A.PROJECT_NAME_TEMP,")
            .AppendLine("       A.CUSTOMER_NAME")
            .AppendLine("  FROM PROJECT                  A,")
            .AppendLine("       A01ADMIN.A01M010_USER    B,")
            .AppendLine("       A01ADMIN.A01M002_SECTION C,")
            .AppendLine("       PROCESS_M                D,")
            .AppendLine("       A01ADMIN.A01M009_ORDER   E,")
            .AppendLine("       A01ADMIN.A01M014_ORDINFO F,")
            .AppendLine("       A01ADMIN.A01M015_COMPY   G,")
            .AppendLine("       CUSTOMER_TYPE_M          H,")
            .AppendLine("       PROJECT_TYPE_M           I")
            .AppendLine(" WHERE A.CREATED_USER_ID = B.A01M010_ID")
            .AppendLine("   AND CONCAT(SUBSTR(B.A01M010_SECT_CD, 1, 2), '000000') =")
            .AppendLine("       C.A01M002_SECT_CD")
            .AppendLine("   AND A.ORDER_CD = E.A01M009_ORDER_CD(+)")
            .AppendLine("   AND A.ORDER_CD = F.A01M014_ORDER_CD(+)")
            .AppendLine("   AND F.A01M014_CUSTM_ID = G.A01M015_ID(+)")
            .AppendLine("   AND A.CUSTOMER_TYPE_NO = H.CUSTOMER_TYPE_NO(+)")
            .AppendLine("   AND A.PROJECT_TYPE_NO = I.PROJECT_TYPE_NO(+)")
            .AppendLine("   AND C.A01M002_APLEND_YMD = 21001231")
            .AppendLine("   AND A.PROCESS_NO = D.PROCESS_NO")
        End With

        '絞り込み条件：支社、部門
        If Not String.IsNullOrEmpty(searchBranch) Then
            If String.IsNullOrEmpty(searchDept) Then
                strSql.AppendLine("AND (SUBSTR(B.A01M010_SECT_CD, 1, 2) = :Branch_V OR")
                strSql.AppendLine("     SUBSTR(A.SUPPORT_BRANCH_ID, 1, 2) = :Branch_ID_V) ")
            Else
                strSql.AppendLine("AND SUBSTR(B.A01M010_SECT_CD, 1, 4) = :Dept_V ")
            End If
        End If

        '絞り込み条件：オーダ
        If Not String.IsNullOrEmpty(searchOrder) Then
            strSql.AppendLine("AND A.ORDER_CD LIKE :Order_V")
        End If

        '絞り込み条件：顧客
        If Not String.IsNullOrEmpty(searchCustomer) Then
            strSql.AppendLine("AND G.A01M015_COMPY_NM LIKE  :Customer_V")
        End If

        '絞り込み条件：管理区分As
        If searchRpmTypeA = "1" Then
            strSql.AppendLine("AND A.RPM_TYPE = 'A'")
        End If

        '絞り込み条件：管理区分B
        If searchRpmTypeB = "1" Then
            strSql.AppendLine("AND A.RPM_TYPE = 'B'")
        End If

        ' 絞り込み条件：完了区分
        If Not searchCompCategory = "1" Then
            strSql.AppendLine("AND A.PROJECT_COMPLETE_FLG = '0'")
        End If

        '並び順
        strSql.AppendLine(" ORDER BY A.PROJECT_NO DESC")

        'SQL変数
        Dim dbcmd As OracleCommand = New OracleCommand(strSql.ToString)

        '絞り込み条件：支社、部門
        If Not String.IsNullOrEmpty(searchBranch) Then
            If String.IsNullOrEmpty(searchDept) Then
                dbcmd.Parameters.Add(New OracleParameter("Branch_V", searchBranchHead))
                dbcmd.Parameters.Add(New OracleParameter("Branch_ID_V", searchBranchHead))
            Else
                dbcmd.Parameters.Add(New OracleParameter("Dept_V", searchDeptHead))
            End If
        End If

        '絞り込み条件：オーダ
        If Not String.IsNullOrEmpty(searchOrder) Then
            dbcmd.Parameters.Add(New OracleParameter("Order_V", "%" + searchOrder + "%"))
        End If

        '絞り込み条件：顧客
        If Not String.IsNullOrEmpty(searchCustomer) Then
            dbcmd.Parameters.Add(New OracleParameter("Customer_V", "%" + searchCustomer + "%"))
        End If

        '検索処理を行う
        mainData = DatabaseComm.DbSearchAdapter(dbcmd)

        '営業プロセス(原価)情報を設定する
        mainData.Columns.Add("RP_P1_COUNT")

        '営業プロセス(見積)を設定する
        mainData.Columns.Add("RP_P2_COUNT")

        '購買プロセス情報を設定する
        mainData.Columns.Add("RP_P3_COUNT")

        '設計・開発プロセス情報を設定する
        mainData.Columns.Add("RP_P4_COUNT")

        '営業プロセス(原価)情報
        Dim salesCostStatusData As DataTable

        '営業プロセス(見積)情報
        Dim salesQuoteStatusData As DataTable

        '購買プロセス情報
        Dim purchaseStatusData As DataTable

        '設計・開発プロセス情報
        Dim developStatusData As DataTable

        'Ⅰの検索結果よりループする
        For i As Integer = 0 To mainData.Rows.Count - 1

            If String.IsNullOrEmpty(mainData.Rows(i).Item("ORDER_CD").ToString) Then
                mainData.Rows(i).Item("A01M009_ORDER_NM") = mainData.Rows(i).Item("PROJECT_NAME_TEMP")
            End If

            If String.IsNullOrEmpty(mainData.Rows(i).Item("ORDER_CD").ToString) Then
                mainData.Rows(i).Item("A01M015_COMPY_NM") = mainData.Rows(i).Item("CUSTOMER_NAME")
            End If

            '営業プロセス(原価)情報
            '営業プロセス(原価)情報SQL作成
            strSql = New StringBuilder
            With strSql
                .AppendLine("SELECT A.RP_P1_COUNT  AS RP_P1_COUNT,")
                .AppendLine("       B.FLG_CATEGORY AS P1_FLG_CATEGORY_N,")
                .AppendLine("       B.FLAG         AS P1_FLAG_N,")
                .AppendLine("       C.FLG_CATEGORY AS P1_FLG_CATEGORY_C,")
                .AppendLine("       C.FLAG         AS P1_FLAG_C")
                .AppendLine("  FROM (SELECT (CASE")
                .AppendLine("         WHEN COUNT(PROCESS_NO) = '0' THEN")
                .AppendLine("          '-'")
                .AppendLine("         else")
                .AppendLine("          TO_CHAR(COUNT(PROCESS_NO))")
                .AppendLine("       END) AS RP_P1_COUNT")
                .AppendLine("          FROM RISK_PREVENTION")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 0) A,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 0")
                .AppendLine("           AND FLG_CATEGORY = '0') B,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 0")
                .AppendLine("           AND FLG_CATEGORY = '1') C")
            End With

            '営業プロセス(原価)SQL変数
            dbcmd = New OracleCommand(strSql.ToString)
            dbcmd.Parameters.Add(New OracleParameter("PROJECT_NO_V", mainData.Rows(i)("PROJECT_NO").ToString))

            '営業プロセス(原価)検索処理を行う
            salesCostStatusData = DatabaseComm.DbSearchAdapter(dbcmd)
            If salesCostStatusData.Rows.Count > 0 Then

                If salesCostStatusData.Rows(0).Item("P1_FLG_CATEGORY_N").ToString = "0" And salesCostStatusData.Rows(0).Item("P1_FLAG_N").ToString = "1" Then

                    mainData.Rows(i)("RP_P1_COUNT") = salesCostStatusData.Rows(0).Item("RP_P1_COUNT").ToString + "(不要)"

                ElseIf salesCostStatusData.Rows(0).Item("P1_FLG_CATEGORY_C").ToString = "1" And salesCostStatusData.Rows(0).Item("P1_FLAG_C").ToString = "0" Then

                    mainData.Rows(i)("RP_P1_COUNT") = salesCostStatusData.Rows(0).Item("RP_P1_COUNT").ToString + "(未完了)"

                ElseIf salesCostStatusData.Rows(0).Item("P1_FLG_CATEGORY_C").ToString = "1" And salesCostStatusData.Rows(0).Item("P1_FLAG_C").ToString = "1" Then

                    mainData.Rows(i)("RP_P1_COUNT") = salesCostStatusData.Rows(0).Item("RP_P1_COUNT").ToString + "(完了)"

                End If

            End If


            '営業プロセス(見積)情報
            '営業プロセス(見積)情報SQL作成
            strSql = New StringBuilder
            With strSql
                .AppendLine("SELECT A.RP_P2_COUNT  AS RP_P2_COUNT,")
                .AppendLine("       B.FLG_CATEGORY AS P2_FLG_CATEGORY_N,")
                .AppendLine("       B.FLAG         AS P2_FLAG_N,")
                .AppendLine("       C.FLG_CATEGORY AS P2_FLG_CATEGORY_C,")
                .AppendLine("       C.FLAG         AS P2_FLAG_C")
                .AppendLine("  FROM (SELECT (CASE")
                .AppendLine("         WHEN COUNT(PROCESS_NO) = '0' THEN")
                .AppendLine("          '-'")
                .AppendLine("         else")
                .AppendLine("          TO_CHAR(COUNT(PROCESS_NO))")
                .AppendLine("       END) AS RP_P2_COUNT")
                .AppendLine("          FROM RISK_PREVENTION")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 1) A,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 1")
                .AppendLine("           AND FLG_CATEGORY = '0') B,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 1")
                .AppendLine("           AND FLG_CATEGORY = '1') C")
            End With

            '営業プロセス(見積)情報SQL変数
            dbcmd = New OracleCommand(strSql.ToString)
            dbcmd.Parameters.Add(New OracleParameter("PROJECT_NO_V", mainData.Rows(i)("PROJECT_NO").ToString))

            '営業プロセス(見積)情報検索処理を行う
            salesQuoteStatusData = DatabaseComm.DbSearchAdapter(dbcmd)
            If salesQuoteStatusData.Rows.Count > 0 Then
                If salesQuoteStatusData.Rows(0).Item("P2_FLG_CATEGORY_N").ToString = "0" And salesQuoteStatusData.Rows(0).Item("P2_FLAG_N").ToString = "1" Then

                    mainData.Rows(i)("RP_P2_COUNT") = salesQuoteStatusData.Rows(0).Item("RP_P2_COUNT").ToString + "(不要)"

                ElseIf salesQuoteStatusData.Rows(0).Item("P2_FLG_CATEGORY_C").ToString = "1" And salesQuoteStatusData.Rows(0).Item("P2_FLAG_C").ToString = "0" Then

                    mainData.Rows(i)("RP_P2_COUNT") = salesQuoteStatusData.Rows(0).Item("RP_P2_COUNT").ToString + "(未完了)"

                ElseIf salesQuoteStatusData.Rows(0).Item("P2_FLAG_C").ToString = "1" And salesQuoteStatusData.Rows(0).Item("P2_FLAG_C").ToString = "1" Then

                    mainData.Rows(i)("RP_P2_COUNT") = salesQuoteStatusData.Rows(0).Item("RP_P2_COUNT").ToString + "(完了)"

                End If

            End If



            '購買プロセス情報
            '購買プロセス情報SQL作成
            strSql = New StringBuilder
            With strSql
                .AppendLine("SELECT A.RP_P3_COUNT  AS RP_P3_COUNT,")
                .AppendLine("       B.FLG_CATEGORY AS P3_FLG_CATEGORY_N,")
                .AppendLine("       B.FLAG         AS P3_FLAG_N,")
                .AppendLine("       C.FLG_CATEGORY AS P3_FLG_CATEGORY_C,")
                .AppendLine("       C.FLAG         AS P3_FLAG_C")
                .AppendLine("  FROM (SELECT (CASE")
                .AppendLine("         WHEN COUNT(PROCESS_NO) = '0' THEN")
                .AppendLine("          '-'")
                .AppendLine("         else")
                .AppendLine("          TO_CHAR(COUNT(PROCESS_NO))")
                .AppendLine("       END) AS RP_P3_COUNT")
                .AppendLine("          FROM RISK_PREVENTION")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 2) A,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 2")
                .AppendLine("           AND FLG_CATEGORY = '0') B,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 2")
                .AppendLine("           AND FLG_CATEGORY = '1') C")
            End With

            '購買プロセス情報SQL変数
            dbcmd = New OracleCommand(strSql.ToString)
            dbcmd.Parameters.Add(New OracleParameter("PROJECT_NO_V", mainData.Rows(i)("PROJECT_NO").ToString))

            '購買プロセス検索処理を行う
            purchaseStatusData = DatabaseComm.DbSearchAdapter(dbcmd)
            If purchaseStatusData.Rows.Count > 0 Then
                If purchaseStatusData.Rows(0).Item("P3_FLG_CATEGORY_N").ToString = "0" And purchaseStatusData.Rows(0).Item("P3_FLAG_N").ToString = "1" Then

                    mainData.Rows(i)("RP_P3_COUNT") = purchaseStatusData.Rows(0).Item("RP_P3_COUNT").ToString + "(不要)"

                ElseIf purchaseStatusData.Rows(0).Item("P3_FLG_CATEGORY_C").ToString = "1" And purchaseStatusData.Rows(0).Item("P3_FLAG_C").ToString = "0" Then

                    mainData.Rows(i)("RP_P3_COUNT") = purchaseStatusData.Rows(0).Item("RP_P3_COUNT").ToString + "(未完了)"

                ElseIf purchaseStatusData.Rows(0).Item("P3_FLAG_C").ToString = "1" And purchaseStatusData.Rows(0).Item("P3_FLAG_C").ToString = "1" Then

                    mainData.Rows(i)("RP_P3_COUNT") = purchaseStatusData.Rows(0).Item("RP_P3_COUNT").ToString + "(完了)"

                End If

            End If

            '設計・開発プロセス
            '設計・開発プロセスSQL作成
            strSql = New StringBuilder
            With strSql
                .AppendLine("SELECT A.RP_P4_COUNT  AS RP_P4_COUNT,")
                .AppendLine("       B.FLG_CATEGORY AS P4_FLG_CATEGORY_N,")
                .AppendLine("       B.FLAG         AS P4_FLAG_N,")
                .AppendLine("       C.FLG_CATEGORY AS P4_FLG_CATEGORY_C,")
                .AppendLine("       C.FLAG         AS P4_FLAG_C")
                .AppendLine("  FROM (SELECT (CASE")
                .AppendLine("         WHEN COUNT(PROCESS_NO) = '0' THEN")
                .AppendLine("          '-'")
                .AppendLine("         else")
                .AppendLine("          TO_CHAR(COUNT(PROCESS_NO))")
                .AppendLine("       END) AS RP_P4_COUNT")
                .AppendLine("          FROM RISK_PREVENTION")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 3) A,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 3")
                .AppendLine("           AND FLG_CATEGORY = '0') B,")
                .AppendLine("       (SELECT FLG_CATEGORY, FLAG")
                .AppendLine("          FROM PROJECT_PROCESS_FLG")
                .AppendLine("         WHERE PROJECT_NO = :PROJECT_NO_V")
                .AppendLine("           AND PROCESS_NO = 3")
                .AppendLine("           AND FLG_CATEGORY = '1') C")
            End With

            '設計・開発プロセスSQL変数
            dbcmd = New OracleCommand(strSql.ToString)
            dbcmd.Parameters.Add(New OracleParameter("PROJECT_NO_V", mainData.Rows(i)("PROJECT_NO").ToString))

            '設計・開発プロセス検索処理を行う
            developStatusData = DatabaseComm.DbSearchAdapter(dbcmd)
            If developStatusData.Rows.Count > 0 Then
                If developStatusData.Rows(0).Item("P4_FLG_CATEGORY_N").ToString = "0" And developStatusData.Rows(0).Item("P4_FLAG_N").ToString = "1" Then

                    mainData.Rows(i)("RP_P4_COUNT") = developStatusData.Rows(0).Item("RP_P4_COUNT").ToString + "(不要)"

                ElseIf developStatusData.Rows(0).Item("P4_FLG_CATEGORY_C").ToString = "1" And developStatusData.Rows(0).Item("P4_FLAG_C").ToString = "0" Then

                    mainData.Rows(i)("RP_P4_COUNT") = developStatusData.Rows(0).Item("RP_P4_COUNT").ToString + "(未完了)"

                ElseIf developStatusData.Rows(0).Item("P4_FLAG_C").ToString = "1" And developStatusData.Rows(0).Item("P4_FLAG_C").ToString = "1" Then

                    mainData.Rows(i)("RP_P4_COUNT") = developStatusData.Rows(0).Item("RP_P4_COUNT").ToString + "(完了)"

                End If

            End If
        Next

        Return mainData
    End Function

End Class
