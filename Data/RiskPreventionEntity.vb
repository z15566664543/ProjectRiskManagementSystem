Imports Oracle.DataAccess.Client
Imports log4net.Repository.Hierarchy
Imports System.IO

Public Class RiskPreventionEntity

    ''' <summary>
    ''' Log対象のインスタンスを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLogger() As log4net.ILog

        Return log4net.LogManager.GetLogger("RiskPreventionEntity")

    End Function

    ''' <summary>
    ''' プロセスを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProcess() As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append("SELECT PROCESS_NO, PROCESS_NAME FROM PROCESS_M ORDER BY PROCESS_NO ")
        End With

        dt = DatabaseComm.DbSearchAdapter(strSql.ToString)

        Return dt

    End Function

    ''' <summary>
    ''' 検討段階を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetDiscussPhase() As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append("SELECT DISCUSS_PHASE_NO, DISCUSS_PHASE_NAME FROM DISCUSS_PHASE_M ORDER BY DISCUSS_PHASE_NO")
        End With

        dt = DatabaseComm.DbSearchAdapter(strSql.ToString)

        Return dt

    End Function

    ''' <summary>
    ''' オーダ関連の情報を取得
    ''' </summary>
    ''' <param name="orderCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetOrderInfo(orderCd As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT A.A01M009_ORDER_NM, ")
            .Append("        E.A01M015_COMPY_NM, ")
            .Append("        B.A01M014_JYUCHU_CRR, ")
            .Append("        B.A01M014_NOKI_YMD, ")
            .Append("        C.A01M002_ALLSECT_NM, ")
            .Append("        D.A01M010_FULLNAME ")
            .Append("   FROM A01ADMIN.A01M009_ORDER   A, ")
            .Append("        A01ADMIN.A01M014_ORDINFO B, ")
            .Append("        A01ADMIN.A01M002_SECTION C, ")
            .Append("        A01ADMIN.A01M010_USER    D, ")
            .Append("        A01ADMIN.A01M015_COMPY   E ")
            .Append("  WHERE A.A01M009_ORDER_CD = :ORDER_CD ")
            .Append("    AND A.A01M009_ORDER_CD = B.A01M014_ORDER_CD ")
            .Append("    AND B.A01M014_JYUCHU_SECT_ID = C.A01M002_ID ")
            .Append("    AND B.A01M014_JYUCHU_USER_ID = D.A01M010_ID ")
            .Append("    AND B.A01M014_CUSTM_ID = E.A01M015_ID ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("ORDER_CD", orderCd)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 案件関連の情報を取得
    ''' </summary>
    ''' <param name="pjNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectInfo(pjNo As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT T.* FROM PROJECT T WHERE T.PROJECT_NO = :PROJECT_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", pjNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' リスク予防検討会登録ボタンクリックイベント
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="seqNo"></param>
    ''' <returns></returns>
    ''' <remarks>対象案件がDB内に存在するか確認するため</remarks>
    Public Shared Function GetRiskPrevention(projectNo As String,
                                        seqNo As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT B.PROJECT_NO, ")
            .Append("        B.SEQ_NO, ")
            .Append("        B.REPORT_CATEGORY, ")
            .Append("        B.CHECK_DATE, ")
            .Append("        B.PROCESS_NO, ")
            .Append("        B.DISCUSS_PHASE_NO, ")
            .Append("        B.SENDER_USER_ID, ")
            .Append("        B.SENDER_USER_CD, ")
            .Append("        B.SENDER_USER_FULLNAME, ")
            .Append("        B.OPEN_DATE, ")
            '.Append("        TO_CHAR(B.OPEN_DATE,'YYYY" + Chr(34) + "年" + Chr(34) + "MM" + Chr(34) + "月" + Chr(34) + "DD" + Chr(34) + "日" + Chr(34) + "') AS OPEN_DATE, ")
            .Append("        B.OPEN_TIME, ")
            .Append("        B.OPEN_ROUND, ")
            .Append("        B.OPEN_PLACE, ")
            .Append("        B.REVIEW_POINT, ")
            .Append("        B.REVIEWER_PLAN, ")
            .Append("        B.REVIEWER, ")
            .Append("        B.REVIEW_REMARK, ")
            .Append("        B.REMARK, ")
            .Append("        B.CREATED_ON, ")
            .Append("        B.CREATED_USER_CD, ")
            .Append("        B.CREATED_USER_ID, ")
            .Append("        B.CREATED_USER_NAME, ")
            .Append("        B.MODIFIED_ON, ")
            .Append("        B.MODIFIED_USER_CD, ")
            .Append("        B.MODIFIED_USER_ID, ")
            .Append("        B.MODIFIED_USER_NAME, ")
            .Append("        A.ORDER_CD, ")
            .Append("        A.PROJECT_NAME_TEMP, ")
            .Append("        A.CUSTOMER_NAME, ")
            .Append("        A.PRODUCT_SECT_NM, ")
            .Append("        C.A01M010_SECT_CD, ")
            .Append("        D.CUSTOMER_TYPE_NAME ")
            .Append("   FROM PROJECT               A, ")
            .Append("        RISK_PREVENTION       B, ")
            .Append("        A01ADMIN.A01M010_USER C, ")
            .Append("        CUSTOMER_TYPE_M       D ")
            .Append("  WHERE B.PROJECT_NO = :PROJECT_NO ")
            .Append("    AND B.SEQ_NO = :SEQ_NO ")
            .Append("    AND B.CREATED_USER_ID = C.A01M010_ID ")
            .Append("    AND A.PROJECT_NO = B.PROJECT_NO ")
            .Append("    AND A.CUSTOMER_TYPE_NO = D.CUSTOMER_TYPE_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", projectNo)
        dbcmd.Parameters.Add("SEQ_NO", seqNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' ユーザ情報検索
    ''' </summary>
    ''' <param name="userCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserInfo(userCd As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT B.A01M002_ALLSECT_NM, ")
            .Append("        C.A01M003_POSTCLS_NM, ")
            .Append("        A.A01M010_ID, ")
            .Append("        A.A01M010_USER_CD, ")
            .Append("        A.A01M010_FULLNAME ")
            .Append("   FROM A01ADMIN.A01M010_USER    A, ")
            .Append("        A01ADMIN.A01M002_SECTION B, ")
            .Append("        A01ADMIN.A01M003_POSTCLS C ")
            .Append("  WHERE A.A01M010_APLEND_YMD = 21001231 ")
            .Append("    AND A.A01M010_EMP_END = 0 ")
            .Append("    AND A.A01M010_SECT_CD = B.A01M002_SECT_CD ")
            .Append("    AND B.A01M002_APLEND_YMD = 21001231 ")
            .Append("    AND A.A01M010_POSTCLS_CD = C.A01M003_POSTCLS_CD(+) ")
            .Append("    AND A.A01M010_USER_CD = :USER_CD ")

        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("USER_CD", userCd)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' リスク・不安要素検討表の検索
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="processNo"></param>
    ''' <param name="manageCategory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRiskManagement(projectNo As String,
                                             processNo As String,
                                             manageCategory As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT ATTATCH_FILE_NAME, ")
            .Append("        ATTATCH_FILE ")
            .Append("   FROM RISK_MANAGEMENT_LIST ")
            .Append("  WHERE PROJECT_NO = :PROJECT_NO ")
            .Append("    AND PROCESS_NO = :PROCESS_NO ")
            .Append("    AND MANAGE_CATEGORY = :MANAGE_CATEGORY ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", projectNo)
        dbcmd.Parameters.Add("PROCESS_NO", processNo)
        dbcmd.Parameters.Add("MANAGE_CATEGORY", manageCategory)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 添付ファイルの情報を取得
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="seqNo"></param>
    ''' <returns></returns>
    ''' <remarks>添付ファイルの情報を取得</remarks>
    Public Shared Function GetRiskPreventionAttatchInfo(projectNo As String,
                                                        seqNo As String) As DataTable
        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT FILE_SEQ_NO, ")
            .Append("        ATTATCH_FILE_NAME ")
            .Append("   FROM RISK_PREVENTION_ATTATCH ")
            .Append("  WHERE PROJECT_NO = :PROJECT_NO ")
            .Append("    AND SEQ_NO = :SEQ_NO ")
            .Append("  ORDER BY FILE_SEQ_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", projectNo)
        dbcmd.Parameters.Add("SEQ_NO", seqNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 添付ファイルの情報を取得
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="seqNo"></param>
    ''' <param name="fileSeqNo"></param>
    ''' <returns></returns>
    ''' <remarks>添付ファイルの情報を取得</remarks>
    Public Shared Function GetRiskPreventionAttatch(projectNo As String,
                                                    seqNo As String,
                                                    Optional fileSeqNo As String = "") As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT FILE_SEQ_NO, ")
            .Append("        ATTATCH_FILE_NAME, ")
            .Append("        ATTATCH_FILE ")
            .Append("   FROM RISK_PREVENTION_ATTATCH ")
            .Append("  WHERE PROJECT_NO = :PROJECT_NO ")
            .Append("    AND SEQ_NO = :SEQ_NO ")
            If Not fileSeqNo = "" Then
                .Append("    AND FILE_SEQ_NO = :FILE_SEQ_NO ")
            End If

            .Append("  ORDER BY FILE_SEQ_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", projectNo)
        dbcmd.Parameters.Add("SEQ_NO", seqNo)
        If Not fileSeqNo = "" Then
            dbcmd.Parameters.Add("FILE_SEQ_NO", fileSeqNo)
        End If

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' リスク予防検討会登録(更新の場合)
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="seqNo"></param>
    ''' <param name="reportCategory"></param>
    ''' <param name="checkDate"></param>
    ''' <param name="processNo"></param>
    ''' <param name="discussPhaseNo"></param>
    ''' <param name="senderUserId"></param>
    ''' <param name="senderUserCd"></param>
    ''' <param name="senderUserFullname"></param>
    ''' <param name="openDate"></param>
    ''' <param name="openTime"></param>
    ''' <param name="openRound"></param>
    ''' <param name="openPlace"></param>
    ''' <param name="reviewPoint"></param>
    ''' <param name="reviewerPlan"></param>
    ''' <param name="reviewer"></param>
    ''' <param name="reviewRemark"></param>
    ''' <param name="remark"></param>
    ''' <param name="modifiedUserCd"></param>
    ''' <param name="modifiedUserId"></param>
    ''' <param name="modifiedUserName"></param>
    ''' <param name="arrDelete"></param>
    ''' <param name="fileNames"></param>
    ''' <param name="files"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateRistPrevention(projectNo As String,
                                                seqNo As String,
                                                reportCategory As String,
                                                checkDate As String,
                                                processNo As String,
                                                discussPhaseNo As String,
                                                senderUserId As String,
                                                senderUserCd As String,
                                                senderUserFullname As String,
                                                openDate As String,
                                                openTime As String,
                                                openRound As String,
                                                openPlace As String,
                                                reviewPoint As String,
                                                reviewerPlan As String,
                                                reviewer As String,
                                                reviewRemark As String,
                                                remark As String,
                                                modifiedUserCd As String,
                                                modifiedUserId As String,
                                                modifiedUserName As String,
                                                arrDelete As ArrayList,
                                                fileNames As ArrayList,
                                                files As ArrayList,
                                                userCd As String) As String

        Dim db As DatabaseComm = New DatabaseComm
        Dim strParamLog As StringBuilder

        Try
            Dim dbConnection = db.open()

            ' ===================================================
            ' リスク予防検討会テーブル(RISK_PREVENTION)へのUPDATE
            ' ===================================================
            Dim strSql As New StringBuilder
            With strSql
                .Append(" UPDATE RISK_PREVENTION ")
                .Append("    SET REPORT_CATEGORY      = :REPORT_CATEGORY, ")
                .Append("        CHECK_DATE           = :CHECK_DATE, ")
                .Append("        PROCESS_NO           = :PROCESS_NO, ")
                .Append("        DISCUSS_PHASE_NO     = :DISCUSS_PHASE_NO, ")
                .Append("        SENDER_USER_ID       = :SENDER_USER_ID, ")
                .Append("        SENDER_USER_CD       = :SENDER_USER_CD, ")
                .Append("        SENDER_USER_FULLNAME = :SENDER_USER_FULLNAME, ")
                .Append("        OPEN_DATE            = :OPEN_DATE, ")
                .Append("        OPEN_TIME            = :OPEN_TIME, ")
                .Append("        OPEN_ROUND           = :OPEN_ROUND, ")
                .Append("        OPEN_PLACE           = :OPEN_PLACE, ")
                .Append("        REVIEW_POINT         = :REVIEW_POINT, ")
                .Append("        REVIEWER_PLAN        = :REVIEWER_PLAN, ")
                .Append("        REVIEWER             = :REVIEWER, ")
                .Append("        REVIEW_REMARK        = :REVIEW_REMARK, ")
                .Append("        REMARK               = :REMARK, ")
                .Append("        MODIFIED_ON          = sysdate, ")
                .Append("        MODIFIED_USER_CD     = :MODIFIED_USER_CD, ")
                .Append("        MODIFIED_USER_ID     = :MODIFIED_USER_ID, ")
                .Append("        MODIFIED_USER_NAME   = :MODIFIED_USER_NAME ")
                .Append("  WHERE PROJECT_NO = :PROJECT_NO ")
                .Append("    AND SEQ_NO = :SEQ_NO ")
            End With

            Dim dbcmd As OracleCommand = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Parameters.Add("REPORT_CATEGORY", reportCategory)
            dbcmd.Parameters.Add("CHECK_DATE", checkDate)
            dbcmd.Parameters.Add("PROCESS_NO", processNo)
            dbcmd.Parameters.Add("DISCUSS_PHASE_NO", discussPhaseNo)
            dbcmd.Parameters.Add("SENDER_USER_ID", senderUserId)
            dbcmd.Parameters.Add("SENDER_USER_CD", senderUserCd)
            dbcmd.Parameters.Add("SENDER_USER_FULLNAME", senderUserFullname)
            If Not String.IsNullOrEmpty(openDate) Then
                Dim dtOpenDate As Date
                Date.TryParse(openDate, dtOpenDate)
                dbcmd.Parameters.Add("OPEN_DATE", dtOpenDate)
            Else
                dbcmd.Parameters.Add("OPEN_DATE", openDate)
            End If
            dbcmd.Parameters.Add("OPEN_TIME", openTime)
            dbcmd.Parameters.Add("OPEN_ROUND", openRound)
            dbcmd.Parameters.Add("OPEN_PLACE", openPlace)
            dbcmd.Parameters.Add("REVIEW_POINT", reviewPoint)
            dbcmd.Parameters.Add("REVIEWER_PLAN", reviewerPlan)
            dbcmd.Parameters.Add("REVIEWER", reviewer)
            dbcmd.Parameters.Add("REVIEW_REMARK", reviewRemark)
            dbcmd.Parameters.Add("REMARK", remark)
            dbcmd.Parameters.Add("MODIFIED_USER_CD", modifiedUserCd)
            dbcmd.Parameters.Add("MODIFIED_USER_ID", modifiedUserId)
            dbcmd.Parameters.Add("MODIFIED_USER_NAME", modifiedUserName)
            dbcmd.Parameters.Add("PROJECT_NO", projectNo)
            dbcmd.Parameters.Add("SEQ_NO", seqNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROJECT_NO=" & projectNo)
                .Append(" SEQ_NO=" & seqNo)
                .Append(" REPORT_CATEGORY=" & reportCategory)
                .Append(" CHECK_DATE=" & checkDate)
                .Append(" PROCESS_NO=" & processNo)
                .Append(" DISCUSS_PHASE_NO=" & discussPhaseNo)
                .Append(" SENDER_USER_ID=" & senderUserId)
                .Append(" SENDER_USER_CD=" & senderUserCd)
                .Append(" SENDER_USER_FULLNAME=" & senderUserFullname)
                .Append(" OPEN_DATE=" & openDate)
                .Append(" OPEN_TIME=" & openTime)
                .Append(" OPEN_ROUND=" & openRound)
                .Append(" OPEN_PLACE=" & openPlace)
                .Append(" REVIEW_POINT=" & reviewPoint)
                .Append(" REVIEWER_PLAN=" & reviewerPlan)
                .Append(" REVIEWER=" & reviewer)
                .Append(" REVIEW_REMARK=" & reviewRemark)
                .Append(" REMARK=" & remark)
                .Append(" CREATED_USER_CD=" & modifiedUserCd)
                .Append(" CREATED_USER_ID=" & modifiedUserId)
                .Append(" CREATED_USER_NAME=" & modifiedUserName)
                .Append(" MODIFIED_USER_CD=" & modifiedUserCd)
                .Append(" MODIFIED_USER_ID=" & modifiedUserId)
                .Append(" MODIFIED_USER_NAME=" & modifiedUserName)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

            ' ===================================================
            ' ファイル削除：削除されたファイルをDBから削除
            ' ===================================================
            For Each strFileSeq As String In arrDelete
                strSql = New StringBuilder
                With strSql
                    .Append(" DELETE FROM RISK_PREVENTION_ATTATCH T ")
                    .Append("  WHERE T.PROJECT_NO = :PROJECT_NO ")
                    .Append("    AND T.SEQ_NO = :SEQ_NO ")
                    .Append("    AND T.FILE_SEQ_NO = :FILE_SEQ_NO ")
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROJECT_NO", projectNo)
                dbcmd.Parameters.Add("SEQ_NO", seqNo)
                dbcmd.Parameters.Add("FILE_SEQ_NO", strFileSeq)

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROJECT_NO=" & projectNo)
                    .Append(" SEQ_NO=" & seqNo)
                    .Append(" FILE_SEQ_NO=" & strFileSeq)
                End With
                GetLogger().Info(userCd & " " & dbcmd.CommandText)
                GetLogger().Info(userCd & " " & strParamLog.ToString)

                db.excute(dbcmd)

            Next

            ' ===================================================
            ' ファイル採番：既存データから最大番号を取得
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append("SELECT NVL(MAX(FILE_SEQ_NO),0) AS FILE_SEQ_NO FROM RISK_PREVENTION_ATTATCH WHERE PROJECT_NO = :PROJECT_NO AND SEQ_NO = :SEQ_NO")
            End With

            dbcmd = New OracleCommand
            dbcmd.Connection = dbConnection
            dbcmd.CommandText = strSql.ToString
            dbcmd.Parameters.Add("PROJECT_NO", projectNo)
            dbcmd.Parameters.Add("SEQ_NO", seqNo)

            Dim dr As OracleDataReader = dbcmd.ExecuteReader()
            dr.Read()
            Dim fileSeqNo = Int32.Parse(dr.Item("FILE_SEQ_NO").ToString)

            ' ===================================================
            ' リスク予防検討会添付ファイルテーブル(RISK_PREVENTION_ATTATCH)へのINSERT
            ' ===================================================
            Dim i = 0
            For i = 0 To fileNames.Count - 1

                strSql = New StringBuilder
                With strSql
                    With strSql
                        .Append(" INSERT INTO RISK_PREVENTION_ATTATCH ")
                        .Append("   (PROJECT_NO, ")
                        .Append("    SEQ_NO, ")
                        .Append("    FILE_SEQ_NO, ")
                        .Append("    ATTATCH_FILE_NAME, ")
                        .Append("    ATTATCH_FILE) ")
                        .Append(" VALUES ")
                        .Append("   (:PROJECT_NO, ")
                        .Append("    :SEQ_NO, ")
                        .Append("    :FILE_SEQ_NO, ")
                        .Append("    :ATTATCH_FILE_NAME, ")
                        .Append("    :ATTATCH_FILE) ")
                    End With
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROJECT_NO", projectNo)
                dbcmd.Parameters.Add("SEQ_NO", seqNo)
                dbcmd.Parameters.Add("FILE_SEQ_NO", fileSeqNo + i + 1)
                dbcmd.Parameters.Add("ATTATCH_FILE_NAME", System.IO.Path.GetFileName(fileNames(i)))
                dbcmd.Parameters.Add("ATTATCH_FILE", files(i))

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROJECT_NO=" & projectNo)
                    .Append(" SEQ_NO=" & seqNo)
                    .Append(" FILE_SEQ_NO=" & i + 1)
                    .Append(" ATTATCH_FILE_NAME=" & System.IO.Path.GetFileName(fileNames(i)))
                End With
                GetLogger().Info(userCd & " " & dbcmd.CommandText)
                GetLogger().Info(userCd & " " & strParamLog.ToString)

                db.excute(dbcmd)

            Next

        Catch ex As Exception
            Throw ex

        Finally
            ' トランザクションのコミットまたはロールバックを発行する
            ' 処理でエラーが発生した場合はSQL発行をロールバックし、処理を中断する
            db.close()
        End Try

        GetLogger().Info(userCd & " " & "リスク予防・管理検討会UPDATE正常完了")

        Return seqNo

    End Function

    ''' <summary>
    ''' リスク予防検討会登録(新規の場合)
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="reportCategory"></param>
    ''' <param name="checkDate"></param>
    ''' <param name="processNo"></param>
    ''' <param name="discussPhaseNo"></param>
    ''' <param name="senderUserId"></param>
    ''' <param name="senderUserCd"></param>
    ''' <param name="senderUserFullname"></param>
    ''' <param name="openDate"></param>
    ''' <param name="openTime"></param>
    ''' <param name="openRound"></param>
    ''' <param name="openPlace"></param>
    ''' <param name="reviewPoint"></param>
    ''' <param name="reviewerPlan"></param>
    ''' <param name="reviewer"></param>
    ''' <param name="reviewRemark"></param>
    ''' <param name="remark"></param>
    ''' <param name="modifiedUserCd"></param>
    ''' <param name="modifiedUserId"></param>
    ''' <param name="modifiedUserName"></param>
    ''' <param name="fileNames"></param>
    ''' <param name="files"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertRistPrevention(projectNo As String,
                                                reportCategory As String,
                                                checkDate As String,
                                                processNo As String,
                                                discussPhaseNo As String,
                                                senderUserId As String,
                                                senderUserCd As String,
                                                senderUserFullname As String,
                                                openDate As String,
                                                openTime As String,
                                                openRound As String,
                                                openPlace As String,
                                                reviewPoint As String,
                                                reviewerPlan As String,
                                                reviewer As String,
                                                reviewRemark As String,
                                                remark As String,
                                                modifiedUserCd As String,
                                                modifiedUserId As String,
                                                modifiedUserName As String,
                                                fileNames As ArrayList,
                                                files As ArrayList,
                                                userCd As String) As String


        Dim db As DatabaseComm = New DatabaseComm
        Dim dbConnection As OracleConnection
        Dim dbcmd As OracleCommand

        Dim seqNo As String
        Dim strSql As StringBuilder
        Dim strParamLog As StringBuilder

        Try
            dbConnection = db.open()
            ' ===================================================
            ' 案件番号新規枝番を採番する
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append("SELECT NVL(MAX(SEQ_NO),0) + 1 AS SEQ_NO FROM RISK_PREVENTION WHERE PROJECT_NO = :PROJECT_NO")
            End With

            dbcmd = New OracleCommand
            dbcmd.Connection = dbConnection
            dbcmd.CommandText = strSql.ToString
            dbcmd.Parameters.Add("PROJECT_NO", projectNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROJECT_NO=" & projectNo)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            Dim dr As OracleDataReader = dbcmd.ExecuteReader()
            dr.Read()
            seqNo = dr.Item("SEQ_NO")

            ' ===================================================
            ' リスク予防検討会テーブル(RISK_PREVENTION)へのINSERT
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append(" INSERT INTO RISK_PREVENTION ")
                .Append("   (PROJECT_NO, ")
                .Append("    SEQ_NO, ")
                .Append("    REPORT_CATEGORY, ")
                .Append("    CHECK_DATE, ")
                .Append("    PROCESS_NO, ")
                .Append("    DISCUSS_PHASE_NO, ")
                .Append("    SENDER_USER_ID, ")
                .Append("    SENDER_USER_CD, ")
                .Append("    SENDER_USER_FULLNAME, ")
                .Append("    OPEN_DATE, ")
                .Append("    OPEN_TIME, ")
                .Append("    OPEN_ROUND, ")
                .Append("    OPEN_PLACE, ")
                .Append("    REVIEW_POINT, ")
                .Append("    REVIEWER_PLAN, ")
                .Append("    REVIEWER, ")
                .Append("    REVIEW_REMARK, ")
                .Append("    REMARK, ")
                .Append("    CREATED_ON, ")
                .Append("    CREATED_USER_CD, ")
                .Append("    CREATED_USER_ID, ")
                .Append("    CREATED_USER_NAME, ")
                .Append("    MODIFIED_ON, ")
                .Append("    MODIFIED_USER_CD, ")
                .Append("    MODIFIED_USER_ID, ")
                .Append("    MODIFIED_USER_NAME) ")
                .Append(" VALUES ")
                .Append("   (:PROJECT_NO, ")
                .Append("    :SEQ_NO, ")
                .Append("    :REPORT_CATEGORY, ")
                .Append("    :CHECK_DATE, ")
                .Append("    :PROCESS_NO, ")
                .Append("    :DISCUSS_PHASE_NO, ")
                .Append("    :SENDER_USER_ID, ")
                .Append("    :SENDER_USER_CD, ")
                .Append("    :SENDER_USER_FULLNAME, ")
                .Append("    :OPEN_DATE, ")
                .Append("    :OPEN_TIME, ")
                .Append("    :OPEN_ROUND, ")
                .Append("    :OPEN_PLACE, ")
                .Append("    :REVIEW_POINT, ")
                .Append("    :REVIEWER_PLAN, ")
                .Append("    :REVIEWER, ")
                .Append("    :REVIEW_REMARK, ")
                .Append("    :REMARK, ")
                .Append("    sysdate, ")
                .Append("    :CREATED_USER_CD, ")
                .Append("    :CREATED_USER_ID, ")
                .Append("    :CREATED_USER_NAME, ")
                .Append("    sysdate, ")
                .Append("    :MODIFIED_USER_CD, ")
                .Append("    :MODIFIED_USER_ID, ")
                .Append("    :MODIFIED_USER_NAME) ")
            End With

            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection

            dbcmd.Parameters.Add("PROJECT_NO", projectNo)
            dbcmd.Parameters.Add("SEQ_NO", seqNo)
            dbcmd.Parameters.Add("REPORT_CATEGORY", reportCategory)
            dbcmd.Parameters.Add("CHECK_DATE", checkDate)
            dbcmd.Parameters.Add("PROCESS_NO", processNo)
            dbcmd.Parameters.Add("DISCUSS_PHASE_NO", discussPhaseNo)
            dbcmd.Parameters.Add("SENDER_USER_ID", senderUserId)
            dbcmd.Parameters.Add("SENDER_USER_CD", senderUserCd)
            dbcmd.Parameters.Add("SENDER_USER_FULLNAME", senderUserFullname)
            If Not String.IsNullOrEmpty(openDate) Then
                Dim dtOpenDate As Date
                Date.TryParse(openDate, dtOpenDate)
                dbcmd.Parameters.Add("OPEN_DATE", dtOpenDate)
            Else
                dbcmd.Parameters.Add("OPEN_DATE", openDate)
            End If
            dbcmd.Parameters.Add("OPEN_TIME", openTime)
            dbcmd.Parameters.Add("OPEN_ROUND", openRound)
            dbcmd.Parameters.Add("OPEN_PLACE", openPlace)
            dbcmd.Parameters.Add("REVIEW_POINT", reviewPoint)
            dbcmd.Parameters.Add("REVIEWER_PLAN", reviewerPlan)
            dbcmd.Parameters.Add("REVIEWER", reviewer)
            dbcmd.Parameters.Add("REVIEW_REMARK", reviewRemark)
            dbcmd.Parameters.Add("REMARK", remark)
            dbcmd.Parameters.Add("CREATED_USER_CD", modifiedUserCd)
            dbcmd.Parameters.Add("CREATED_USER_ID", modifiedUserId)
            dbcmd.Parameters.Add("CREATED_USER_NAME", modifiedUserName)
            dbcmd.Parameters.Add("MODIFIED_USER_CD", modifiedUserCd)
            dbcmd.Parameters.Add("MODIFIED_USER_ID", modifiedUserId)
            dbcmd.Parameters.Add("MODIFIED_USER_NAME", modifiedUserName)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROJECT_NO=" & projectNo)
                .Append(" SEQ_NO=" & seqNo)
                .Append(" REPORT_CATEGORY=" & reportCategory)
                .Append(" CHECK_DATE=" & checkDate)
                .Append(" PROCESS_NO=" & processNo)
                .Append(" DISCUSS_PHASE_NO=" & discussPhaseNo)
                .Append(" SENDER_USER_ID=" & senderUserId)
                .Append(" SENDER_USER_CD=" & senderUserCd)
                .Append(" SENDER_USER_FULLNAME=" & senderUserFullname)
                .Append(" OPEN_DATE=" & openDate)
                .Append(" OPEN_TIME=" & openTime)
                .Append(" OPEN_ROUND=" & openRound)
                .Append(" OPEN_PLACE=" & openPlace)
                .Append(" REVIEW_POINT=" & reviewPoint)
                .Append(" REVIEWER_PLAN=" & reviewerPlan)
                .Append(" REVIEWER=" & reviewer)
                .Append(" REVIEW_REMARK=" & reviewRemark)
                .Append(" REMARK=" & remark)
                .Append(" CREATED_USER_CD=" & modifiedUserCd)
                .Append(" CREATED_USER_ID=" & modifiedUserId)
                .Append(" CREATED_USER_NAME=" & modifiedUserName)
                .Append(" MODIFIED_USER_CD=" & modifiedUserCd)
                .Append(" MODIFIED_USER_ID=" & modifiedUserId)
                .Append(" MODIFIED_USER_NAME=" & modifiedUserName)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

            ' ===================================================
            ' リスク予防検討会添付ファイルテーブル(RISK_PREVENTION_ATTATCH)へのINSERT
            ' ===================================================
            Dim i = 0
            For i = 0 To files.Count - 1
                strSql = New StringBuilder
                With strSql
                    With strSql
                        .Append(" INSERT INTO RISK_PREVENTION_ATTATCH ")
                        .Append("   (PROJECT_NO, ")
                        .Append("    SEQ_NO, ")
                        .Append("    FILE_SEQ_NO, ")
                        .Append("    ATTATCH_FILE_NAME, ")
                        .Append("    ATTATCH_FILE) ")
                        .Append(" VALUES ")
                        .Append("   (:PROJECT_NO, ")
                        .Append("    :SEQ_NO, ")
                        .Append("    :FILE_SEQ_NO, ")
                        .Append("    :ATTATCH_FILE_NAME, ")
                        .Append("    :ATTATCH_FILE) ")
                    End With
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROJECT_NO", projectNo)
                dbcmd.Parameters.Add("SEQ_NO", seqNo)
                dbcmd.Parameters.Add("FILE_SEQ_NO", i + 1)
                dbcmd.Parameters.Add("ATTATCH_FILE_NAME", System.IO.Path.GetFileName(fileNames(i)))
                dbcmd.Parameters.Add("ATTATCH_FILE", files(i))

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROJECT_NO=" & projectNo)
                    .Append(" SEQ_NO=" & seqNo)
                    .Append(" FILE_SEQ_NO=" & i + 1)
                    .Append(" ATTATCH_FILE_NAME=" & System.IO.Path.GetFileName(fileNames(i)))
                End With
                GetLogger().Info(userCd & " " & dbcmd.CommandText)
                GetLogger().Info(userCd & " " & strParamLog.ToString)

                db.excute(dbcmd)

            Next

        Catch ex As Exception
            Throw ex

        Finally
            ' トランザクションのコミットまたはロールバックを発行する
            ' 処理でエラーが発生した場合はSQL発行をロールバックし、処理を中断する
            db.close()
        End Try

        GetLogger().Info(userCd & " " & "リスク予防・管理検討会INSERT正常完了")

        Return seqNo
    End Function

    ''' <summary>
    ''' リスク予防検討会削除
    ''' </summary>
    ''' <param name="projectNo"></param>
    ''' <param name="seqNo"></param>
    ''' <param name="userCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteRistPrevention(projectNo As String, seqNo As String, ByVal userCd As String) As String


        Dim db As DatabaseComm = New DatabaseComm
        Dim dbcmd As OracleCommand
        Dim strSql As StringBuilder
        Dim strParamLog As StringBuilder

        Dim dbConnection As OracleConnection

        Try
            dbConnection = db.open()

            ' ① リスク予防検討会添付ファイルテーブル(RISK_PREVENTION_ATTATCH)へのDELETE
            strSql = New StringBuilder
            With strSql
                .Append(" DELETE FROM RISK_PREVENTION_ATTATCH ")
                .Append("  WHERE PROJECT_NO = :PROJECT_NO ")
                .Append("    AND SEQ_NO = :SEQ_NO ")

            End With
            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection
            dbcmd.Parameters.Add("PROJECT_NO", projectNo)
            dbcmd.Parameters.Add("SEQ_NO", seqNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROJECT_NO=" & projectNo)
                .Append(" SEQ_NO=" & seqNo)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

            ' ② リスク予防検討会テーブル(RISK_PREVENTION)へのDELETE
            strSql = New StringBuilder
            With strSql
                .Append(" DELETE FROM RISK_PREVENTION ")
                .Append("  WHERE PROJECT_NO = :PROJECT_NO ")
                .Append("    AND SEQ_NO = :SEQ_NO ")

            End With
            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection

            dbcmd.Parameters.Add("PROJECT_NO", projectNo)
            dbcmd.Parameters.Add("SEQ_NO", seqNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROJECT_NO=" & projectNo)
                .Append(" SEQ_NO=" & seqNo)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

        Catch ex As Exception
            Throw ex

        Finally
            ' トランザクションのコミットまたはロールバックを発行する
            ' DELETE発行処理でエラーが発生した場合はSQL発行をロールバックし、処理を中断する
            db.close()
        End Try

        GetLogger().Info(userCd & " " & "リスク予防・管理検討会DELETE正常完了")

        Return seqNo

    End Function

End Class
