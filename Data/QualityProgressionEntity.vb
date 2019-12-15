Imports Oracle.DataAccess.Client
Imports log4net.Repository.Hierarchy
Imports System.IO

Public Class QualityProgressionEntity

    ''' <summary>
    ''' Log対象のインスタンスを取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetLogger() As log4net.ILog

        Return log4net.LogManager.GetLogger("QualityProgressionEntity")

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
            .Append("        B.A01M002_SECT_CD, ")
            .Append("        B.A01M002_ID, ")
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
    ''' 起票者の所属する支社と製造部門の所属支社が一致し、完了していない案件を取得する
    ''' </summary>
    ''' <param name="userSectCd2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetIncompletePj(userSectCd2 As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT A.PROJECT_NO, ")
            .Append("        NVL2(B.A01M009_ORDER_NM, B.A01M009_ORDER_NM, A.PROJECT_NAME_TEMP) AS ORDER_NM, ")
            .Append("        A.PRODUCT_SECT_NM, ")
            .Append("        A.PRODUCT_USER_FULLNAME, ")
            .Append("        D.PROCESS_NAME, ")
            .Append("        D.PROCESS_NO, ")
            .Append("        C.ATTATCH_FILE_NAME, ")
            .Append("        '''?fid2=' || A.PROJECT_NO || '&process=' || D.PROCESS_NO || '''' AS URL")
            .Append("   FROM PROJECT                A, ")
            .Append("        A01ADMIN.A01M009_ORDER B, ")
            .Append("        RISK_MANAGEMENT_LIST   C, ")
            .Append("        PROCESS_M              D ")
            .Append("  WHERE A.ORDER_CD = B.A01M009_ORDER_CD(+) ")
            .Append("    AND A.PROJECT_NO = C.PROJECT_NO(+) ")
            .Append("    AND A.PROCESS_NO = C.PROCESS_NO(+) ")
            .Append("    AND 0 = C.MANAGE_CATEGORY(+) ")
            .Append("    AND A.PROJECT_COMPLETE_FLG != 1 ")
            .Append("    AND A.PROCESS_NO = D.PROCESS_NO ")
            .Append("    AND SUBSTR(A.PRODUCT_SECT_CD, 1, 2) = :SECT_CD2 ")
            .Append("  ORDER BY A.PROJECT_NO ")

        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("SECT_CD2", userSectCd2)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 当該品質推進会議の新規登録時の未完了案件を取得する
    ''' </summary>
    ''' <param name="qpNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    Public Shared Function GetProgressionRelate(qpNo As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT A.PROJECT_NO, ")
            .Append("        NVL2(B.A01M009_ORDER_NM, ")
            .Append("        B.A01M009_ORDER_NM, ")
            .Append("        A.PROJECT_NAME_TEMP) AS ORDER_NM, ")
            .Append("        A.PRODUCT_SECT_NM, ")
            .Append("        A.PRODUCT_USER_FULLNAME, ")
            .Append("        D.PROCESS_NAME, ")
            .Append("        D.PROCESS_NO, ")
            .Append("        C.ATTATCH_FILE_NAME, ")
            .Append("        '''?fid2=' || A.PROJECT_NO || '&process=' || D.PROCESS_NO || '''' AS URL")
            .Append("   FROM PROJECT                    A, ")
            .Append("        A01ADMIN.A01M009_ORDER     B, ")
            .Append("        RISK_MANAGEMENT_LIST       C, ")
            .Append("        PROCESS_M                  D, ")
            .Append("        QUALITY_PROGRESSION_RELATE E ")
            .Append("  WHERE A.ORDER_CD = B.A01M009_ORDER_CD(+) ")
            .Append("    AND A.PROJECT_NO = C.PROJECT_NO(+) ")
            .Append("    AND A.PROCESS_NO = C.PROCESS_NO(+) ")
            .Append("    AND 0 = C.MANAGE_CATEGORY(+)")
            .Append("    AND A.PROCESS_NO = D.PROCESS_NO ")
            .Append("    AND E.PROGRESSION_NO = :PROGRESSION_NO ")
            .Append("    AND E.PROJECT_NO = A.PROJECT_NO ")
            .Append("  ORDER BY A.PROJECT_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 品質推進会議の取得
    ''' </summary>
    ''' <param name="qpNo"></param>
    ''' <returns></returns>
    ''' <remarks>対象案件がDB内に存在するか確認するため</remarks>
    Public Shared Function GetProgression(qpNo As String) As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT * ")
            .Append("   FROM QUALITY_PROGRESSION A, ")
            .Append("        A01ADMIN.A01M010_USER B ")
            .Append("  WHERE A.PROGRESSION_NO = :PROGRESSION_NO ")
            .Append("    AND A.CREATED_USER_ID = B.A01M010_ID(+) ")

        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

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
    ''' <param name="qpNo"></param>
    ''' <returns></returns>
    ''' <remarks>添付ファイルの情報を取得</remarks>
    Public Shared Function GetPreventionAttatchInfo(qpNo As String) As DataTable
        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT FILE_SEQ_NO, ")
            .Append("        ATTATCH_FILE_NAME ")
            .Append("   FROM QUALITY_PROGRESSION_ATTATCH ")
            .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            .Append("  ORDER BY FILE_SEQ_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 添付ファイルの情報を取得
    ''' </summary>
    ''' <param name="qpNo"></param>
    ''' <param name="fileSeqNo"></param>
    ''' <returns></returns>
    ''' <remarks>添付ファイルの情報を取得</remarks>
    Public Shared Function GetPreventionAttatch(qpNo As String,
                                                Optional fileSeqNo As String = "") As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT FILE_SEQ_NO, ")
            .Append("        ATTATCH_FILE_NAME, ")
            .Append("        ATTATCH_FILE ")
            .Append("   FROM QUALITY_PROGRESSION_ATTATCH ")
            .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            If Not fileSeqNo = "" Then
                .Append("    AND FILE_SEQ_NO = :FILE_SEQ_NO ")
            End If

            .Append("  ORDER BY FILE_SEQ_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)
        If Not fileSeqNo = "" Then
            dbcmd.Parameters.Add("FILE_SEQ_NO", fileSeqNo)
        End If

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 品質推進会議登録 ※修正登録(UPDATE)の場合
    ''' </summary>
    ''' <param name="qpNo"></param>
    ''' <param name="incompletePjs"></param>
    ''' <param name="reportCategory"></param>
    ''' <param name="branchName"></param>
    ''' <param name="targetSectId"></param>
    ''' <param name="targetSectName"></param>
    ''' <param name="openerUserId"></param>
    ''' <param name="openerUserCd"></param>
    ''' <param name="openerUserName"></param>
    ''' <param name="conferenceName"></param>
    ''' <param name="senderUserId"></param>
    ''' <param name="senderUserCd"></param>
    ''' <param name="senderUserName"></param>
    ''' <param name="openDate"></param>
    ''' <param name="openFiscalYear"></param>
    ''' <param name="openQuarter"></param>
    ''' <param name="openTime"></param>
    ''' <param name="openRound"></param>
    ''' <param name="openPlace"></param>
    ''' <param name="reviewPoint"></param>
    ''' <param name="reviewPlan"></param>
    ''' <param name="reviewer"></param>
    ''' <param name="conferenceContent"></param>
    ''' <param name="modifiedUserCd"></param>
    ''' <param name="modifiedUserId"></param>
    ''' <param name="modifiedUserName"></param>
    ''' <param name="arrDelete"></param>
    ''' <param name="uploadFiles"></param>
    ''' <param name="userCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function UpdateProgression(qpNo As String,
                                            incompletePjs As DataTable,
                                            reportCategory As String,
                                            branchName As String,
                                            targetSectId As String,
                                            targetSectName As String,
                                            openerUserId As String,
                                            openerUserCd As String,
                                            openerUserName As String,
                                            conferenceName As String,
                                            senderUserId As String,
                                            senderUserCd As String,
                                            senderUserName As String,
                                            openDate As String,
                                            openFiscalYear As String,
                                            openQuarter As String,
                                            openTime As String,
                                            openRound As String,
                                            openPlace As String,
                                            reviewPoint As String,
                                            reviewPlan As String,
                                            reviewer As String,
                                            conferenceContent As String,
                                            modifiedUserCd As String,
                                            modifiedUserId As String,
                                            modifiedUserName As String,
                                            arrDelete As ArrayList,
                                            uploadFileNames As ArrayList,
                                            uploadFiles As ArrayList,
                                            userCd As String) As String

        Dim db As DatabaseComm = New DatabaseComm
        Dim strParamLog As StringBuilder

        Try
            Dim dbConnection = db.open()

            ' ===================================================
            ' 品質推進会議テーブル(QUALITY_PROGRESSION)へのUPDATE
            ' ===================================================
            Dim strSql As New StringBuilder
            With strSql
                .Append(" UPDATE QUALITY_PROGRESSION ")
                .Append("    SET REPORT_CATEGORY    = :REPORT_CATEGORY, ")
                .Append("        BRANCH_NAME        = :BRANCH_NAME, ")
                .Append("        TARGET_SECT_ID     = :TARGET_SECT_ID, ")
                .Append("        TARGET_SECT_NAME   = :TARGET_SECT_NAME, ")
                .Append("        OPENER_USER_ID     = :OPENER_USER_ID, ")
                .Append("        OPENER_USER_CD     = :OPENER_USER_CD, ")
                .Append("        OPENER_USER_NAME   = :OPENER_USER_NAME, ")
                .Append("        CONFERENCE_NAME    = :CONFERENCE_NAME, ")
                .Append("        SENDER_USER_ID     = :SENDER_USER_ID, ")
                .Append("        SENDER_USER_CD     = :SENDER_USER_CD, ")
                .Append("        SENDER_USER_NAME   = :SENDER_USER_NAME, ")
                .Append("        OPEN_DATE          = :OPEN_DATE, ")
                .Append("        OPEN_FISCAL_YEAR   = :OPEN_FISCAL_YEAR, ")
                .Append("        OPEN_QUARTER       = :OPEN_QUARTER, ")
                .Append("        OPEN_TIME          = :OPEN_TIME, ")
                .Append("        OPEN_ROUND         = :OPEN_ROUND, ")
                .Append("        OPEN_PLACE         = :OPEN_PLACE, ")
                .Append("        REVIEW_POINT       = :REVIEW_POINT, ")
                .Append("        REVIEWER_PLAN      = :REVIEWER_PLAN, ")
                .Append("        REVIEWER           = :REVIEWER, ")
                .Append("        CONFERENCE_CONTENT = :CONFERENCE_CONTENT, ")
                .Append("        MODIFIED_ON        = sysdate, ")
                .Append("        MODIFIED_USER_CD   = :MODIFIED_USER_CD, ")
                .Append("        MODIFIED_USER_ID   = :MODIFIED_USER_ID, ")
                .Append("        MODIFIED_USER_NAME = :MODIFIED_USER_NAME ")
                .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            End With

            Dim dbcmd As OracleCommand = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Parameters.Add("REPORT_CATEGORY", reportCategory)
            dbcmd.Parameters.Add("BRANCH_NAME", branchName)
            dbcmd.Parameters.Add("TARGET_SECT_ID", targetSectId)
            dbcmd.Parameters.Add("TARGET_SECT_NAME", targetSectName)
            dbcmd.Parameters.Add("OPENER_USER_ID", openerUserId)
            dbcmd.Parameters.Add("OPENER_USER_CD", openerUserCd)
            dbcmd.Parameters.Add("OPENER_USER_NAME", openerUserName)
            dbcmd.Parameters.Add("CONFERENCE_NAME", conferenceName)
            dbcmd.Parameters.Add("SENDER_USER_ID", senderUserId)
            dbcmd.Parameters.Add("SENDER_USER_CD", senderUserCd)
            dbcmd.Parameters.Add("SENDER_USER_NAME", senderUserName)

            If Not String.IsNullOrEmpty(openDate) Then
                Dim dtOpenDate As Date
                Date.TryParse(openDate, dtOpenDate)
                dbcmd.Parameters.Add("OPEN_DATE", dtOpenDate)
            Else
                dbcmd.Parameters.Add("OPEN_DATE", openDate)
            End If

            dbcmd.Parameters.Add("OPEN_FISCAL_YEAR", openFiscalYear)
            dbcmd.Parameters.Add("OPEN_QUARTER", openQuarter)
            dbcmd.Parameters.Add("OPEN_TIME", openTime)
            dbcmd.Parameters.Add("OPEN_ROUND", openRound)
            dbcmd.Parameters.Add("OPEN_PLACE", openPlace)
            dbcmd.Parameters.Add("REVIEW_POINT", reviewPoint)
            dbcmd.Parameters.Add("REVIEWER_PLAN", reviewPlan)
            dbcmd.Parameters.Add("REVIEWER", reviewer)
            dbcmd.Parameters.Add("CONFERENCE_CONTENT", conferenceContent)
            dbcmd.Parameters.Add("MODIFIED_USER_CD", modifiedUserCd)
            dbcmd.Parameters.Add("MODIFIED_USER_ID", modifiedUserId)
            dbcmd.Parameters.Add("MODIFIED_USER_NAME", modifiedUserName)
            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROGRESSION_NO=" & qpNo)
                .Append(" REPORT_CATEGORY=" & reportCategory)
                .Append(" BRANCH_NAME=" & branchName)
                .Append(" TARGET_SECT_ID=" & targetSectId)
                .Append(" TARGET_SECT_NAME=" & targetSectName)
                .Append(" OPENER_USER_ID=" & openerUserId)
                .Append(" OPENER_USER_CD=" & openerUserCd)
                .Append(" OPENER_USER_NAME=" & openerUserName)
                .Append(" CONFERENCE_NAME=" & conferenceName)
                .Append(" SENDER_USER_ID=" & senderUserId)
                .Append(" SENDER_USER_CD=" & senderUserCd)
                .Append(" SENDER_USER_NAME=" & senderUserName)
                .Append(" OPEN_DATE=" & openDate)
                .Append(" OPEN_FISCAL_YEAR=" & openFiscalYear)
                .Append(" OPEN_QUARTER=" & openQuarter)
                .Append(" OPEN_TIME=" & openTime)
                .Append(" OPEN_ROUND=" & openRound)
                .Append(" OPEN_PLACE=" & openPlace)
                .Append(" REVIEW_POINT=" & reviewPoint)
                .Append(" REVIEWER_PLAN=" & reviewPlan)
                .Append(" REVIEWER=" & reviewer)
                .Append(" CONFERENCE_CONTENT=" & conferenceContent)
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
                    .Append(" DELETE FROM QUALITY_PROGRESSION_ATTATCH T ")
                    .Append("  WHERE T.PROGRESSION_NO = :PROGRESSION_NO ")
                    .Append("    AND T.FILE_SEQ_NO = :FILE_SEQ_NO ")
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)
                dbcmd.Parameters.Add("FILE_SEQ_NO", strFileSeq)

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROGRESSION_NO=" & qpNo)
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
                .Append("SELECT NVL(MAX(FILE_SEQ_NO),0) AS FILE_SEQ_NO FROM QUALITY_PROGRESSION_ATTATCH WHERE PROGRESSION_NO = :PROGRESSION_NO")
            End With

            dbcmd = New OracleCommand
            dbcmd.Connection = dbConnection
            dbcmd.CommandText = strSql.ToString
            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

            Dim dr As OracleDataReader = dbcmd.ExecuteReader()
            dr.Read()
            Dim fileSeqNo = Int32.Parse(dr.Item("FILE_SEQ_NO").ToString)

            ' ===================================================
            ' 品質推進会議添付ファイルテーブル(QUALITY_PROGRESSION_ATTATCH)への更新へのINSERT
            ' ===================================================
            Dim i = 0
            For i = 0 To uploadFiles.Count - 1
                strSql = New StringBuilder
                With strSql
                    With strSql
                        .Append(" INSERT INTO QUALITY_PROGRESSION_ATTATCH ")
                        .Append("   (PROGRESSION_NO, ")
                        .Append("    FILE_SEQ_NO, ")
                        .Append("    ATTATCH_FILE_NAME, ")
                        .Append("    ATTATCH_FILE) ")
                        .Append(" VALUES ")
                        .Append("   (:PROGRESSION_NO, ")
                        .Append("    :FILE_SEQ_NO, ")
                        .Append("    :ATTATCH_FILE_NAME, ")
                        .Append("    :ATTATCH_FILE) ")
                    End With
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)
                dbcmd.Parameters.Add("FILE_SEQ_NO", fileSeqNo + i + 1)
                dbcmd.Parameters.Add("ATTATCH_FILE_NAME", System.IO.Path.GetFileName(uploadFileNames(i)))
                dbcmd.Parameters.Add("ATTATCH_FILE", uploadFiles(i))

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROGRESSION_NO=" & qpNo)
                    .Append(" FILE_SEQ_NO=" & i + 1)
                    .Append(" ATTATCH_FILE_NAME=" & System.IO.Path.GetFileName(uploadFileNames(i)))
                End With
                GetLogger().Info(userCd & " " & dbcmd.CommandText)
                GetLogger().Info(userCd & " " & strParamLog.ToString)

                db.excute(dbcmd)

            Next

            ' ===================================================
            ' 品質推進テーブル(QUALITY_PROGRESSION_RELATE)へのDELETE
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append(" DELETE FROM QUALITY_PROGRESSION_RELATE ")
                .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            End With

            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection

            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROGRESSION_NO=" & qpNo)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

            ' ===================================================
            ' 品質推進テーブル(QUALITY_PROGRESSION_RELATE)へのINSERT
            ' ===================================================
            For i = 0 To incompletePjs.Rows.Count - 1

                strSql = New StringBuilder
                With strSql
                    With strSql
                        .Append(" INSERT INTO QUALITY_PROGRESSION_RELATE ")
                        .Append("   (PROJECT_NO, PROGRESSION_NO) ")
                        .Append(" VALUES ")
                        .Append("   (:PROJECT_NO, :PROGRESSION_NO) ")

                    End With
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROJECT_NO", incompletePjs.Rows(i).Item("PROJECT_NO").ToString)
                dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROJECT_NO=" & incompletePjs.Rows(i).Item("PROJECT_NO").ToString)
                    .Append(" PROGRESSION_NO=" & qpNo)
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

        GetLogger().Info(userCd & " " & "品質推進会議UPDATE正常完了")

        Return qpNo

    End Function

    ''' <summary>
    ''' 品質推進会議登録 ※(新規の場合)
    ''' </summary>
    ''' <param name="incompletePjs"></param>
    ''' <param name="reportCategory"></param>
    ''' <param name="branchName"></param>
    ''' <param name="targetSectId"></param>
    ''' <param name="targetSectName"></param>
    ''' <param name="openerUserId"></param>
    ''' <param name="openerUserCd"></param>
    ''' <param name="openerUserName"></param>
    ''' <param name="conferenceName"></param>
    ''' <param name="senderUserId"></param>
    ''' <param name="senderUserCd"></param>
    ''' <param name="senderUserName"></param>
    ''' <param name="openDate"></param>
    ''' <param name="openFiscalYear"></param>
    ''' <param name="openQuarter"></param>
    ''' <param name="openTime"></param>
    ''' <param name="openRound"></param>
    ''' <param name="openPlace"></param>
    ''' <param name="reviewPoint"></param>
    ''' <param name="reviewPlan"></param>
    ''' <param name="reviewer"></param>
    ''' <param name="conferenceContent"></param>
    ''' <param name="modifiedUserCd"></param>
    ''' <param name="modifiedUserId"></param>
    ''' <param name="modifiedUserName"></param>
    ''' <param name="uploadFiles"></param>
    ''' <param name="userCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Shared Function InsertProgression(incompletePjs As DataTable,
                                             reportCategory As String,
                                            branchName As String,
                                            targetSectId As String,
                                            targetSectName As String,
                                            openerUserId As String,
                                            openerUserCd As String,
                                            openerUserName As String,
                                            conferenceName As String,
                                            senderUserId As String,
                                            senderUserCd As String,
                                            senderUserName As String,
                                            openDate As String,
                                            openFiscalYear As String,
                                            openQuarter As String,
                                            openTime As String,
                                            openRound As String,
                                            openPlace As String,
                                            reviewPoint As String,
                                            reviewPlan As String,
                                            reviewer As String,
                                            conferenceContent As String,
                                            modifiedUserCd As String,
                                            modifiedUserId As String,
                                            modifiedUserName As String,
                                            uploadFileNames As ArrayList,
                                            uploadFiles As ArrayList,
                                            userCd As String) As String


        Dim db As DatabaseComm = New DatabaseComm
        Dim dbConnection As OracleConnection
        Dim dbcmd As OracleCommand

        Dim qpNo As String
        Dim strSql As StringBuilder
        Dim strParamLog As StringBuilder

        Try
            dbConnection = db.open()
            ' ===================================================
            ' 新規品質推進会議番号を採番する
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append("SELECT NVL(MAX(PROGRESSION_NO),0) + 1 AS PROGRESSION_NO FROM QUALITY_PROGRESSION")
            End With

            dbcmd = New OracleCommand
            dbcmd.Connection = dbConnection
            dbcmd.CommandText = strSql.ToString

            ' Log処理(SQL文)
            GetLogger().Info(userCd & " " & dbcmd.CommandText)

            Dim dr As OracleDataReader = dbcmd.ExecuteReader()
            dr.Read()
            qpNo = dr.Item("PROGRESSION_NO")

            ' ===================================================
            ' 品質推進会議テーブル(QUALITY_PROGRESSION)へのINSERT
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append(" INSERT INTO QUALITY_PROGRESSION ")
                .Append("   (PROGRESSION_NO, ")
                .Append("    REPORT_CATEGORY, ")
                .Append("    BRANCH_NAME, ")
                .Append("    TARGET_SECT_ID, ")
                .Append("    TARGET_SECT_NAME, ")
                .Append("    OPENER_USER_ID, ")
                .Append("    OPENER_USER_CD, ")
                .Append("    OPENER_USER_NAME, ")
                .Append("    CONFERENCE_NAME, ")
                .Append("    SENDER_USER_ID, ")
                .Append("    SENDER_USER_CD, ")
                .Append("    SENDER_USER_NAME, ")
                .Append("    OPEN_DATE, ")
                .Append("    OPEN_FISCAL_YEAR, ")
                .Append("    OPEN_QUARTER, ")
                .Append("    OPEN_TIME, ")
                .Append("    OPEN_ROUND, ")
                .Append("    OPEN_PLACE, ")
                .Append("    REVIEW_POINT, ")
                .Append("    REVIEWER_PLAN, ")
                .Append("    REVIEWER, ")
                .Append("    CONFERENCE_CONTENT, ")
                .Append("    CREATED_ON, ")
                .Append("    CREATED_USER_CD, ")
                .Append("    CREATED_USER_ID, ")
                .Append("    CREATED_USER_NAME, ")
                .Append("    MODIFIED_ON, ")
                .Append("    MODIFIED_USER_CD, ")
                .Append("    MODIFIED_USER_ID, ")
                .Append("    MODIFIED_USER_NAME) ")
                .Append(" VALUES ")
                .Append("   (:PROGRESSION_NO, ")
                .Append("    :REPORT_CATEGORY, ")
                .Append("    :BRANCH_NAME, ")
                .Append("    :TARGET_SECT_ID, ")
                .Append("    :TARGET_SECT_NAME, ")
                .Append("    :OPENER_USER_ID, ")
                .Append("    :OPENER_USER_CD, ")
                .Append("    :OPENER_USER_NAME, ")
                .Append("    :CONFERENCE_NAME, ")
                .Append("    :SENDER_USER_ID, ")
                .Append("    :SENDER_USER_CD, ")
                .Append("    :SENDER_USER_NAME, ")
                .Append("    :OPEN_DATE, ")
                .Append("    :OPEN_FISCAL_YEAR, ")
                .Append("    :OPEN_QUARTER, ")
                .Append("    :OPEN_TIME, ")
                .Append("    :OPEN_ROUND, ")
                .Append("    :OPEN_PLACE, ")
                .Append("    :REVIEW_POINT, ")
                .Append("    :REVIEWER_PLAN, ")
                .Append("    :REVIEWER, ")
                .Append("    :CONFERENCE_CONTENT, ")
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

            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)
            dbcmd.Parameters.Add("REPORT_CATEGORY", reportCategory)
            dbcmd.Parameters.Add("BRANCH_NAME", branchName)
            dbcmd.Parameters.Add("TARGET_SECT_ID", targetSectId)
            dbcmd.Parameters.Add("TARGET_SECT_NAME", targetSectName)
            dbcmd.Parameters.Add("OPENER_USER_ID", openerUserId)
            dbcmd.Parameters.Add("OPENER_USER_CD", openerUserCd)
            dbcmd.Parameters.Add("OPENER_USER_NAME", openerUserName)
            dbcmd.Parameters.Add("CONFERENCE_NAME", conferenceName)
            dbcmd.Parameters.Add("SENDER_USER_ID", senderUserId)
            dbcmd.Parameters.Add("SENDER_USER_CD", senderUserCd)
            dbcmd.Parameters.Add("SENDER_USER_NAME", senderUserName)

            If Not String.IsNullOrEmpty(openDate) Then
                Dim dtOpenDate As Date
                Date.TryParse(openDate, dtOpenDate)
                dbcmd.Parameters.Add("OPEN_DATE", dtOpenDate)
            Else
                dbcmd.Parameters.Add("OPEN_DATE", openDate)
            End If

            dbcmd.Parameters.Add("OPEN_FISCAL_YEAR", openFiscalYear)
            dbcmd.Parameters.Add("OPEN_QUARTER", openQuarter)
            dbcmd.Parameters.Add("OPEN_TIME", openTime)
            dbcmd.Parameters.Add("OPEN_ROUND", openRound)
            dbcmd.Parameters.Add("OPEN_PLACE", openPlace)
            dbcmd.Parameters.Add("REVIEW_POINT", reviewPoint)
            dbcmd.Parameters.Add("REVIEWER_PLAN", reviewPlan)
            dbcmd.Parameters.Add("REVIEWER", reviewer)
            dbcmd.Parameters.Add("CONFERENCE_CONTENT", conferenceContent)
            dbcmd.Parameters.Add("CREATED_USER_CD", modifiedUserCd)
            dbcmd.Parameters.Add("CREATED_USER_ID", modifiedUserId)
            dbcmd.Parameters.Add("CREATED_USER_NAME", modifiedUserName)
            dbcmd.Parameters.Add("MODIFIED_USER_CD", modifiedUserCd)
            dbcmd.Parameters.Add("MODIFIED_USER_ID", modifiedUserId)
            dbcmd.Parameters.Add("MODIFIED_USER_NAME", modifiedUserName)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROGRESSION_NO=" & qpNo)
                .Append(" REPORT_CATEGORY=" & reportCategory)
                .Append(" BRANCH_NAME=" & branchName)
                .Append(" TARGET_SECT_ID=" & targetSectId)
                .Append(" TARGET_SECT_NAME=" & targetSectName)
                .Append(" OPENER_USER_ID=" & openerUserId)
                .Append(" OPENER_USER_CD=" & openerUserCd)
                .Append(" OPENER_USER_NAME=" & openerUserName)
                .Append(" CONFERENCE_NAME=" & conferenceName)
                .Append(" SENDER_USER_ID=" & senderUserId)
                .Append(" SENDER_USER_CD=" & senderUserCd)
                .Append(" SENDER_USER_NAME=" & senderUserName)
                .Append(" OPEN_DATE=" & openDate)
                .Append(" OPEN_FISCAL_YEAR=" & openFiscalYear)
                .Append(" OPEN_QUARTER=" & openQuarter)
                .Append(" OPEN_TIME=" & openTime)
                .Append(" OPEN_ROUND=" & openRound)
                .Append(" OPEN_PLACE=" & openPlace)
                .Append(" REVIEW_POINT=" & reviewPoint)
                .Append(" REVIEWER_PLAN=" & reviewPlan)
                .Append(" REVIEWER=" & reviewer)
                .Append(" CONFERENCE_CONTENT=" & conferenceContent)
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
            '品質推進会議添付ファイルテーブル(QUALITY_PROGRESSION_ATTATCH)へのINSERT
            ' ===================================================
            Dim i = 0
            For i = 0 To uploadFiles.Count - 1
                strSql = New StringBuilder
                With strSql
                    With strSql
                        .Append(" INSERT INTO QUALITY_PROGRESSION_ATTATCH ")
                        .Append("   (PROGRESSION_NO, ")
                        .Append("    FILE_SEQ_NO, ")
                        .Append("    ATTATCH_FILE_NAME, ")
                        .Append("    ATTATCH_FILE) ")
                        .Append(" VALUES ")
                        .Append("   (:PROGRESSION_NO, ")
                        .Append("    :FILE_SEQ_NO, ")
                        .Append("    :ATTATCH_FILE_NAME, ")
                        .Append("    :ATTATCH_FILE) ")
                    End With
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)
                dbcmd.Parameters.Add("FILE_SEQ_NO", i + 1)
                dbcmd.Parameters.Add("ATTATCH_FILE_NAME", System.IO.Path.GetFileName(uploadFileNames(i)))
                dbcmd.Parameters.Add("ATTATCH_FILE", uploadFiles(i))

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROGRESSION_NO=" & qpNo)
                    .Append(" FILE_SEQ_NO=" & i + 1)
                    .Append(" ATTATCH_FILE_NAME=" & System.IO.Path.GetFileName(uploadFileNames(i)))
                End With
                GetLogger().Info(userCd & " " & dbcmd.CommandText)
                GetLogger().Info(userCd & " " & strParamLog.ToString)

                db.excute(dbcmd)

            Next

            ' ===================================================
            ' 品質推進テーブル(QUALITY_PROGRESSION_RELATE)へのINSERT
            ' ===================================================
            For i = 0 To incompletePjs.Rows.Count - 1

                strSql = New StringBuilder
                With strSql
                    With strSql
                        .Append(" INSERT INTO QUALITY_PROGRESSION_RELATE ")
                        .Append("   (PROJECT_NO, PROGRESSION_NO) ")
                        .Append(" VALUES ")
                        .Append("   (:PROJECT_NO, :PROGRESSION_NO) ")

                    End With
                End With

                dbcmd = New OracleCommand
                dbcmd.CommandText = strSql.ToString
                dbcmd.Parameters.Add("PROJECT_NO", incompletePjs.Rows(i).Item("PROJECT_NO").ToString)
                dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

                ' Log処理(SQL文)
                strParamLog = New StringBuilder
                With strParamLog
                    .Append(" PROJECT_NO=" & incompletePjs.Rows(i).Item("PROJECT_NO").ToString)
                    .Append(" PROGRESSION_NO=" & qpNo)
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

        GetLogger().Info(userCd & " " & "品質推進会議INSERT正常完了")

        Return qpNo
    End Function

    ''' <summary>
    ''' 品質推進会議削除
    ''' </summary>
    ''' <param name="qpNo"></param>
    ''' <param name="userCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteProgression(qpNo As String, ByVal userCd As String) As String


        Dim db As DatabaseComm = New DatabaseComm
        Dim dbcmd As OracleCommand
        Dim strSql As StringBuilder
        Dim strParamLog As StringBuilder

        Dim dbConnection As OracleConnection

        Try
            dbConnection = db.open()

            ' ① 品質推進会議添付ファイルテーブル(QUALITY_PROGRESSION_ATTATCH)へのDELETE
            strSql = New StringBuilder
            With strSql
                .Append(" DELETE FROM QUALITY_PROGRESSION_ATTATCH ")
                .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            End With

            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection
            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROJECT_NO=" & qpNo)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

            ' ② 品質推進テーブル(QUALITY_PROGRESSION_RELATE)へのDELETE
            strSql = New StringBuilder
            With strSql
                .Append(" DELETE FROM QUALITY_PROGRESSION_RELATE ")
                .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            End With

            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection

            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROGRESSION_NO=" & qpNo)
            End With
            GetLogger().Info(userCd & " " & dbcmd.CommandText)
            GetLogger().Info(userCd & " " & strParamLog.ToString)

            db.excute(dbcmd)

            ' ③ 品質推進会議テーブル(QUALITY_PROGRESSION)へのDELETE
            strSql = New StringBuilder
            With strSql
                .Append(" DELETE FROM QUALITY_PROGRESSION ")
                .Append("  WHERE PROGRESSION_NO = :PROGRESSION_NO ")
            End With

            dbcmd = New OracleCommand
            dbcmd.CommandText = strSql.ToString
            dbcmd.Connection = dbConnection

            dbcmd.Parameters.Add("PROGRESSION_NO", qpNo)

            ' Log処理(SQL文)
            strParamLog = New StringBuilder
            With strParamLog
                .Append(" PROGRESSION_NO=" & qpNo)
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

        GetLogger().Info(userCd & " " & "品質推進会議DELETE正常完了")

        Return qpNo

    End Function

End Class
