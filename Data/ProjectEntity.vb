Imports Oracle.DataAccess.Client
Imports log4net.Repository.Hierarchy

Public Class ProjectEntity
    ''' <summary>
    ''' 支社品質管理責任者情報取得
    ''' </summary>
    ''' <param name="SectCD">所属コード</param>
    ''' <returns>責任者情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetBranchQualityManagerInfo(ByVal SectCD As String) As UserManagerClass

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        Dim userInfo As UserManagerClass = New UserManagerClass
        'SQLを発行する
        With strSql
            .Append(" SELECT B.A01M002_ALLSECT_NM, ")
            .Append("        C.A01M003_POSTCLS_NM, ")
            .Append("        A.A01M010_FULLNAME ")
            .Append("   FROM A01ADMIN.A01M010_USER    A, ")
            .Append("        A01ADMIN.A01M002_SECTION B, ")
            .Append("        A01ADMIN.A01M003_POSTCLS C ")
            .Append("  WHERE (A.A01M010_POSTCLS_CD = '100' OR A.A01M010_POSTCLS_CD = '090' OR A.A01M010_POSTCLS_CD = '010')  ")
            .Append("    AND A.A01M010_APLEND_YMD = 21001231  ")
            .Append("    AND A.A01M010_EMP_END = 0  ")
            .Append("    AND A.A01M010_SECT_CD = B.A01M002_SECT_CD  ")
            .Append("    AND A.A01M010_POSTCLS_CD = C.A01M003_POSTCLS_CD  ")
            .Append("    AND A.A01M010_SECT_CD LIKE :SECT_CD ")

        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        'パラメタ：所属コード上2桁
        dbcmd.Parameters.Add("SECT_CD", SectCD + "%")

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then

            userInfo.AllsectNm = dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString
            userInfo.PostclsNm = dt.Rows(0).Item("A01M003_POSTCLS_NM").ToString
            userInfo.Fullname = dt.Rows(0).Item("A01M010_FULLNAME").ToString
        End If

        Return userInfo

    End Function

    ''' <summary>
    ''' 部品質管理責任者情報取得
    ''' </summary>
    ''' <param name="SectCD">所属コード</param>
    ''' <returns>責任者情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetSectionQualityManagerInfo(ByVal SectCD As String) As UserManagerClass

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        Dim userInfo As UserManagerClass = New UserManagerClass

        'SQLを発行する
        With strSql
            .Append(" SELECT B.A01M002_ALLSECT_NM, ")
            .Append("        C.A01M003_POSTCLS_NM, ")
            .Append("        A.A01M010_FULLNAME ")
            .Append("   FROM A01ADMIN.A01M010_USER    A, ")
            .Append("        A01ADMIN.A01M002_SECTION B, ")
            .Append("        A01ADMIN.A01M003_POSTCLS C ")
            .Append("  WHERE (A.A01M010_POSTCLS_CD = '140'  ")
            .Append("     OR A.A01M010_AD1POSTCLS_CD = '140' ")
            .Append("     OR A.A01M010_AD2POSTCLS_CD = '140'  ")
            .Append("     OR A.A01M010_AD3POSTCLS_CD = '140'  ")
            .Append("     OR A.A01M010_AD4POSTCLS_CD = '140'  ")
            .Append("     OR A.A01M010_AD5POSTCLS_CD = '140'")
            .Append("     OR A.A01M010_POSTCLS_CD IN ('013', '020', '040') ")
            .Append("        )  ")
            .Append("    AND A.A01M010_APLEND_YMD = 21001231  ")
            .Append("    AND A.A01M010_EMP_END = 0  ")
            .Append("    AND A.A01M010_SECT_CD = B.A01M002_SECT_CD  ")
            .Append("    AND A.A01M010_POSTCLS_CD = C.A01M003_POSTCLS_CD  ")
            .Append("    AND A.A01M010_SECT_CD = :SECT_CD ")

        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        'パラメタ：所属コード上4桁0000
        dbcmd.Parameters.Add("SECT_CD", SectCD + "0000")

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then

            userInfo.AllsectNm = dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString
            userInfo.PostclsNm = dt.Rows(0).Item("A01M003_POSTCLS_NM").ToString
            userInfo.Fullname = dt.Rows(0).Item("A01M010_FULLNAME").ToString
        End If

        Return userInfo

    End Function

    ''' <summary>
    ''' グループ品質管理責任者情報取得
    ''' </summary>
    ''' <param name="SectCD">所属コード</param>
    ''' <returns>責任者情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetGroupQualityManagerInfo(ByVal SectCD As String) As UserManagerClass

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        Dim userInfo As UserManagerClass = New UserManagerClass

        'SQLを発行する
        With strSql
            .Append(" SELECT B.A01M002_ALLSECT_NM, ")
            .Append("        C.A01M003_POSTCLS_NM, ")
            .Append("        A.A01M010_FULLNAME ")
            .Append("   FROM A01ADMIN.A01M010_USER    A, ")
            .Append("        A01ADMIN.A01M002_SECTION B, ")
            .Append("        A01ADMIN.A01M003_POSTCLS C ")
            .Append("  WHERE (A.A01M010_POSTCLS_CD = '300'  ")
            .Append("     OR A.A01M010_AD1POSTCLS_CD = '300' ")
            .Append("     OR A.A01M010_AD2POSTCLS_CD = '300' ")
            .Append("     OR A.A01M010_AD3POSTCLS_CD = '300' ")
            .Append("     OR A.A01M010_AD4POSTCLS_CD = '300' ")
            .Append("     OR A.A01M010_AD5POSTCLS_CD = '300')  ")
            .Append("    AND A.A01M010_APLEND_YMD = 21001231  ")
            .Append("    AND A.A01M010_EMP_END = 0  ")
            .Append("    AND A.A01M010_SECT_CD = B.A01M002_SECT_CD  ")
            .Append("    AND B.A01M002_APLEND_YMD = 21001231  ")
            .Append("    AND A.A01M010_POSTCLS_CD = C.A01M003_POSTCLS_CD  ")
            .Append("    AND A.A01M010_SECT_CD = :SECT_CD  ")

        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        'パラメタ：所属コード上6桁00
        dbcmd.Parameters.Add("SECT_CD", SectCD + "00")

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then
            userInfo.AllsectNm = dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString
            userInfo.PostclsNm = dt.Rows(0).Item("A01M003_POSTCLS_NM").ToString
            userInfo.Fullname = dt.Rows(0).Item("A01M010_FULLNAME").ToString
        End If

        Return userInfo

    End Function

    ''' <summary>
    ''' 新規案件
    ''' </summary>
    ''' <param name="clsProject">案件情報</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function InsertProjectInfo(ByVal clsProject As ProjectClass, ByVal sectCd As String _
                                             , ByVal UserCd As String, ByVal UserId As String, ByVal UserName As String) As String
        Dim strSql As New StringBuilder
        '新規案件番号を採番する
        Dim strProNo As String
        strProNo = GetProjectNo(sectCd)
        Dim db As DatabaseComm = New DatabaseComm
        Dim logger As log4net.ILog
        logger = log4net.LogManager.GetLogger("Global_asax")
        '①～⑦の合計7テーブルへの一連のDELETE処理はTRANSACTIONとし、いずれかのSQLでエラーが発生した場合はすべての処理をロールバックする
        Try
            db.open()
            '① 案件テーブル(PROJECT)へのINSERT
            '※①～④の合計4テーブルへの一連のINSERT処理はTRANSACTIONとし、いずれかのSQLでエラーが発生した場合はすべての処理をロールバックする
            '
            'INSERTに必要な情報を取得しSQLを発行する
            With strSql
                .Append(" INSERT INTO PROJECT(  ")
                .Append("     PROJECT_NO ")
                .Append("   , PROCESS_NO ")
                .Append("   , PROJECT_NAME_TEMP ")
                .Append("   , CUSTOMER_NAME ")
                .Append("   , ORDER_CD ")
                .Append("   , RELATE_ORDER_CD ")
                .Append("   , CUSTOMER_TYPE_NO ")
                .Append("   , PRODUCT_SECT_ID ")
                .Append("   , PRODUCT_SECT_CD ")
                .Append("   , PRODUCT_SECT_NM ")
                .Append("   , PRODUCT_USER_ID ")
                .Append("   , PRODUCT_USER_CD ")
                .Append("   , PRODUCT_USER_FULLNAME ")
                .Append("   , BRANCH_TRANSACTION_FLG ")
                .Append("   , SUPPORT_BRANCH_ID ")
                .Append("   , SUPPORT_BRANCH_NM ")
                .Append("   , BRANCH_QUALITY_MANAGER ")
                .Append("   , SECTION_QUALITY_MANAGER ")
                .Append("   , GROUP_QUALITY_MANAGER ")
                .Append("   , PROJECT_QUALITY_MANAGER ")
                .Append("   , RISK_PREVENTION_MANAGER ")
                .Append("   , RPM_500MIL_FLG ")
                .Append("   , RPM_FIRST_PRODUCT_FLG ")
                .Append("   , FIRST_PRODUCT_NO ")
                .Append("   , RPM_FIRST_PRODUCT_CAUSE ")
                .Append("   , RPM_SPECIAL_PRODUCT_FLG ")
                .Append("   , SPECIAL_PRODUCT_NO ")
                .Append("   , RPM_SPECIAL_PRODUCT_CAUSE ")
                .Append("   , RPM_TYPE ")
                .Append("   , SM_CHECKSHEET_FILE_NAME ")
                .Append("   , SM_CHECKSHEET_FILE ")
                .Append("   , PROJECT_TYPE_NO ")
                .Append("   , PROJECT_COMPLETE_FLG ")
                .Append("   , CREATED_ON ")
                .Append("   , CREATED_USER_CD ")
                .Append("   , CREATED_USER_ID ")
                .Append("   , CREATED_USER_NAME ")
                .Append("   , MODIFIED_ON ")
                .Append("   , MODIFIED_USER_CD ")
                .Append("   , MODIFIED_USER_ID ")
                .Append("   , MODIFIED_USER_NAME ")
                .Append("     )  ")
                .Append(" VALUES (  ")
                .Append("     :PROJECT_NO ")
                .Append("   , :PROCESS_NO ")
                .Append("   , :PROJECT_NAME_TEMP ")
                .Append("   , :CUSTOMER_NAME ")
                .Append("   , :ORDER_CD ")
                .Append("   , :RELATE_ORDER_CD ")
                .Append("   , :CUSTOMER_TYPE_NO ")
                .Append("   , :PRODUCT_SECT_ID ")
                .Append("   , :PRODUCT_SECT_CD ")
                .Append("   , :PRODUCT_SECT_NM ")
                .Append("   , :PRODUCT_USER_ID ")
                .Append("   , :PRODUCT_USER_CD ")
                .Append("   , :PRODUCT_USER_FULLNAME ")
                .Append("   , :BRANCH_TRANSACTION_FLG ")
                .Append("   , :SUPPORT_BRANCH_ID ")
                .Append("   , :SUPPORT_BRANCH_NM ")
                .Append("   , :BRANCH_QUALITY_MANAGER ")
                .Append("   , :SECTION_QUALITY_MANAGER ")
                .Append("   , :GROUP_QUALITY_MANAGER ")
                .Append("   , :PROJECT_QUALITY_MANAGER ")
                .Append("   , :RISK_PREVENTION_MANAGER ")
                .Append("   , :RPM_500MIL_FLG ")
                .Append("   , :RPM_FIRST_PRODUCT_FLG ")
                .Append("   , :FIRST_PRODUCT_NO ")
                .Append("   , :RPM_FIRST_PRODUCT_CAUSE ")
                .Append("   , :RPM_SPECIAL_PRODUCT_FLG ")
                .Append("   , :SPECIAL_PRODUCT_NO ")
                .Append("   , :RPM_SPECIAL_PRODUCT_CAUSE ")
                .Append("   , :RPM_TYPE ")
                .Append("   , :SM_CHECKSHEET_FILE_NAME ")
                .Append("   , :SM_CHECKSHEET_FILE ")
                .Append("   , :PROJECT_TYPE_NO ")
                .Append("   , :PROJECT_COMPLETE_FLG ")
                .Append("   , sysdate ")
                .Append("   , :CREATED_USER_CD ")
                .Append("   , :CREATED_USER_ID ")
                .Append("   , :CREATED_USER_NAME ")
                .Append("   , sysdate ")
                .Append("   , :MODIFIED_USER_CD ")
                .Append("   , :MODIFIED_USER_ID ")
                .Append("   , :MODIFIED_USER_NAME ")
                .Append(" )  ")

            End With
            Dim dbcmd As OracleCommand = New OracleCommand
            dbcmd.CommandText = strSql.ToString

            'NULLの場合、半角スペースを設定 
            If clsProject.txtPjNameTemp = String.Empty Then
                clsProject.txtPjNameTemp = " "
            End If

            'NULLの場合、半角スペースを設定 
            If clsProject.txtCustomerName = String.Empty Then
                clsProject.txtCustomerName = " "
            End If

            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbcmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            'プロセスラジオボタン(rdoProcess)の値をセットする
            dbcmd.Parameters.Add("PROCESS_NO", clsProject.rdoProcess)
            '③ @PROJECT_NAME_TEMP
            '工事名称（仮）(txtPjNameTemp)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NAME_TEMP", clsProject.txtPjNameTemp)
            '④ @CUSTOMER_NAME
            '顧客テキストボックス(txtCustomerName)の値をセットする
            dbcmd.Parameters.Add("CUSTOMER_NAME", clsProject.txtCustomerName)
            '⑤ @ORDER_CD
            'オーダテキストボックス(txtOrderCd)の値をセットする
            dbcmd.Parameters.Add("ORDER_CD", clsProject.txtOrderCd)
            '⑥ @RELATE_ORDER_CD
            '関連オーダテキストボックス(txtRelateOrderCd)の値をセットする
            dbcmd.Parameters.Add("RELATE_ORDER_CD", clsProject.txtRelateOrderCd)
            '⑦ @CUSTOMER_TYPE_NO
            '顧客区分ラジオボタン(rdoCustomerType)の値をセットする
            dbcmd.Parameters.Add("CUSTOMER_TYPE_NO", clsProject.rdoCustomerType)
            '⑧ @PRODUCT_SECT_ID
            '製造部門テキストボックス(txtProductSectNm)にセットした部門ID(A01ADMIN.A01M002_SECTION.A01M002_ID)をセットする 
            dbcmd.Parameters.Add("PRODUCT_SECT_ID", clsProject.hdnProductSectId)
            '⑨ @PRODUCT_SECT_CD
            '製造部門テキストボックス(txtProductSectNm)にセットした部門コード(A01ADMIN.A01M002_SECTION.A01M002_SECT_CD)をセットする
            dbcmd.Parameters.Add("PRODUCT_SECT_CD", clsProject.hdnProductSectCd)
            '⑩ @PRODUCT_SECT_NM
            '製造部門テキストボックス(txtProductSectNm)の値をセットする
            dbcmd.Parameters.Add("PRODUCT_SECT_NM", clsProject.txtProductSectNm)
            '⑪ @PRODUCT_USER_ID
            '製造部門担当者テキストボックス(txtProductUser)にセットしたユーザID(A01ADMIN.A01M010_USER.A01M010_ID)をセットする
            dbcmd.Parameters.Add("PRODUCT_USER_ID", clsProject.hdnProductUserId)
            '⑫ @PRODUCT_USER_CD
            '製造部門担当者テキストボックス(txtProductUser)にセットしたユーザコード(A01ADMIN.A01M010_USER.A01M010_USER_CD)をセットする
            dbcmd.Parameters.Add("PRODUCT_USER_CD", clsProject.hdnProductUserCd)
            '⑬ @PRODUCT_USER_FULLNAME
            '製造部門担当者テキストボックス(txtProductUser)の値をセットする
            dbcmd.Parameters.Add("PRODUCT_USER_FULLNAME", clsProject.txtProductUser)
            '⑭ @BRANCH_TRANSACTION_FLG
            '支社間取引の有無ラジオボタン(rdoBranchTransactionFlg)の値をセットする
            dbcmd.Parameters.Add("BRANCH_TRANSACTION_FLG", clsProject.rdoBranchTransactionFlg)
            '⑮ @SUPPORT_BRANCH_ID
            '支援支社ドロップダウンリスト(ddlBranchList)の値(Value)をセットする
            dbcmd.Parameters.Add("SUPPORT_BRANCH_ID", clsProject.ddlBranchListId)
            '⑯ @SUPPORT_BRANCH_NM
            '支援支社ドロップダウンリスト(ddlBranchList)のテキスト(Text)をセットする
            dbcmd.Parameters.Add("SUPPORT_BRANCH_NM", clsProject.ddlBranchListNm)
            '⑰ @BRANCH_QUALITY_MANAGER
            '支社品質管理責任者テキストボックス(txtBranchQualityManager)の値をセットする
            dbcmd.Parameters.Add("BRANCH_QUALITY_MANAGER", clsProject.txtBranchQualityManager)
            '⑱ @SECTION_QUALITY_MANAGER
            '部品質管理責任者テキストボックス(txtSectionQualityManager)の値をセットする
            dbcmd.Parameters.Add("SECTION_QUALITY_MANAGER", clsProject.txtSectionQualityManager)
            '⑲ @GROUP_QUALITY_MANAGER
            'グループ品質管理責任者テキストボックス(txtGroupQualityManager)の値をセットする
            dbcmd.Parameters.Add("GROUP_QUALITY_MANAGER", clsProject.txtGroupQualityManager)
            '⑳ @PROJECT_QUALITY_MANAGER
            'プロジェクト品質管理責任者テキストボックス(txtProjectQualityManager)の値をセットする
            dbcmd.Parameters.Add("PROJECT_QUALITY_MANAGER", clsProject.txtProjectQualityManager)
            '㉑ @RISK_PREVENTION_MANAGER
            'リスク予防管理責任者テキストボックス(txtRiskPreventionManager)の値をセットする
            dbcmd.Parameters.Add("RISK_PREVENTION_MANAGER", clsProject.txtRiskPreventionManager)
            '㉒ @RPM_500MIL_FLG
            'リスク予防管理対象 500万円以上チェックボックス(chkRpm500MilFlg)がチェック状態の場合は1、未チェック状態の場合は0をセットする
            If clsProject.chkRpm500MilFlg Then
                dbcmd.Parameters.Add("RPM_500MIL_FLG", "1")
            Else
                dbcmd.Parameters.Add("RPM_500MIL_FLG", "0")
            End If

            '㉓ @RPM_FIRST_PRODUCT_FLG
            'リスク予防管理対象 初品チェックボックス(chkRpmFirstProductFlg)がチェック状態の場合は1、未チェック状態の場合は0をセットする
            If clsProject.chkRpmFirstProductFlg Then
                dbcmd.Parameters.Add("RPM_FIRST_PRODUCT_FLG", "1")
            Else
                dbcmd.Parameters.Add("RPM_FIRST_PRODUCT_FLG", "0")
            End If

            '㉔ @FIRST_PRODUCT_NO
            'リスク予防管理対象 初品ドロップダウンリスト(ddlFirstProduct)の値をセットする
            dbcmd.Parameters.Add("FIRST_PRODUCT_NO", clsProject.ddlFirstProduct)

            '㉕ @RPM_FIRST_PRODUCT_CAUSE
            'リスク予防管理対象 初品理由ドロップダウンリスト(ddlRpmFirstProductCause)の値をセットする
            dbcmd.Parameters.Add("RPM_FIRST_PRODUCT_CAUSE", clsProject.txtRpmFirstProductCause)
            '㉖ @RPM_SPECIAL_PRODUCT_FLG
            'リスク予防管理対象 特殊チェックボックス(chkRpmSpecialProductFlg)がチェック状態の場合は1、未チェック状態の場合は0をセットする
            If clsProject.chkRpmSpecialProductFlg = "1" Then
                dbcmd.Parameters.Add("RPM_SPECIAL_PRODUCT_FLG", "1")
            Else
                dbcmd.Parameters.Add("RPM_SPECIAL_PRODUCT_FLG", "0")
            End If

            '㉗ @SPECIAL_PRODUCT_NO
            'リスク予防管理対象 特殊ドロップダウンリスト(ddlSpecialProduct)の値をセットする
            dbcmd.Parameters.Add("SPECIAL_PRODUCT_NO", clsProject.ddlSpecialProduct)
            '㉘ @RPM_SPECIAL_PRODUCT_CAUSE
            'リスク予防管理対象 特殊理由ドロップダウンリスト(ddlRpmSpecialProductCause)の値をセットする
            dbcmd.Parameters.Add("RPM_SPECIAL_PRODUCT_CAUSE", clsProject.txtRpmSpecialProductCause)
            '㉙ @RPM_TYPE
            'リスク予防管理区分ラジオボタン(rdoRpmType)の値をセットする
            dbcmd.Parameters.Add("RPM_TYPE", clsProject.rdoRpmType)
            '㉚ @SM_CHECKSHEET_FILE_NAME,@SM_CHECKSHEET_FILE
            'いずれもNULLをセット
            dbcmd.Parameters.Add("SM_CHECKSHEET_FILE_NAME", String.Empty)
            dbcmd.Parameters.Add("SM_CHECKSHEET_FILE", String.Empty)
            '㉛ @PROJECT_TYPE_NO
            '案件タイプラジオボタン(rdoProjectType)の値をセットする
            dbcmd.Parameters.Add("PROJECT_TYPE_NO", clsProject.rdoProjectType)
            '㉜ @PROJECT_COMPLETE_FLG
            '以下の条件を満たしている場合は、1をセットする。そうでない場合は、0をセットする
            '営業プロセス(原価)の不要チェックボックス(chkSp1NoNeedFlg)・完了チェックボックス(chkSp1Complete)のいずれか以上がチェックされている
            'かつ
            '営業プロセス(見積)の不要チェックボックス(chkSp2NoNeedFlg)・完了チェックボックス(chkSp2Complete)のいずれか以上がチェックされている
            'かつ
            '購買プロセスの不要チェックボックス(chkSp3NoNeedFlg)・完了チェックボックス(chkSp3Complete)のいずれか以上がチェックされている
            'かつ
            '設計・開発プロセスの不要チェックボックス(chkSp4NoNeedFlg)・完了チェックボックス(chkSp4Complete)のいずれか以上がチェックされている
            'TODO
            If (clsProject.chkSp1NoNeedFlg Or clsProject.chkSp1CompleteFlg) _
                And (clsProject.chkSp2NoNeedFlg Or clsProject.chkSp2CompleteFlg) _
                And (clsProject.chkPpNoNeedFlg Or clsProject.chkPpCompleteFlg) _
                And (clsProject.chkDpNoNeedFlg Or clsProject.chkDpCompleteFlg) Then
                dbcmd.Parameters.Add("PROJECT_COMPLETE_FLG", "1")
            Else
                dbcmd.Parameters.Add("PROJECT_COMPLETE_FLG", "0")
            End If

            'Session変数UserCdの値をセットする
            dbcmd.Parameters.Add("CREATED_USER_CD", UserCd)
            '㉟ @CREATED_USER_ID, 
            'Session変数UserIdの値をセットする
            dbcmd.Parameters.Add("CREATED_USER_ID", UserId)
            '㊱ @CREATED_USER_NAME
            'Session変数UserNameの値をセットする
            dbcmd.Parameters.Add("CREATED_USER_NAME", UserName)

            'Session変数UserCdの値をセットする
            dbcmd.Parameters.Add("MODIFIED_USER_CD", UserCd)
            '㉟ @MODIFIED_USER_ID
            'Session変数UserIdの値をセットする
            dbcmd.Parameters.Add("MODIFIED_USER_ID", UserId)
            '㊱ @MODIFIED_USER_NAME
            'Session変数UserNameの値をセットする
            dbcmd.Parameters.Add("MODIFIED_USER_NAME", UserName)
            '
            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbcmd.CommandText)

            Dim strParme As String = String.Empty
            For i = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(i).ParameterName & " = " & dbcmd.Parameters.Item(i).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbcmd)

            '② 案件添付ファイルテーブル(PROJECT_ATTATCH)へのINSERT
            '※添付ファイル参照ボタン(fleProjectAttatch(N))に1件も添付ファイルが選択されていない場合は、INSERT処理を実行しない
            If clsProject.fleProjectAttatch.Count > 0 Then
                '添付ファイル参照ボタン(fleProjectAttatch(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                For i = 1 To clsProject.fleProjectAttatch.Count
                    Dim strFileSql As New StringBuilder
                    'INSERTに必要な情報を取得しSQLを発行する
                    With strFileSql
                        .Append(" INSERT INTO PROJECT_ATTATCH(    ")
                        .Append("        PROJECT_NO   ")
                        .Append("      , FILE_SEQ_NO   ")
                        .Append("      , ATTATCH_FILE_NAME   ")
                        .Append("      , ATTATCH_FILE   ")
                        .Append("        )   ")
                        .Append(" VALUES (   ")
                        .Append("       :PROJECT_NO  ")
                        .Append("     , :FILE_SEQ_NO  ")
                        .Append("     , :ATTATCH_FILE_NAME  ")
                        .Append("     , :ATTATCH_FILE  ")
                        .Append("       )   ")
                    End With

                    Dim dbFileCmd As OracleCommand = New OracleCommand
                    dbFileCmd.CommandText = strFileSql.ToString

                    '① @PROJECT_NO
                    '採番した新規案件番号をセットする
                    dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                    '② @FILE_SEQ_NO
                    '添付ファイル参照ボタン(fleProjectAttatch(N))の引数をセットする　※Nが1であれば1をセット
                    dbFileCmd.Parameters.Add("FILE_SEQ_NO", i.ToString)

                    '③ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", System.IO.Path.GetFileName(clsProject.fleProjectAttatch(i - 1).ToString))

                    '④ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleProjectAttatchFile(i - 1))
                    'INSERT発行前にログを出力する
                    logger.Info(UserCd & " " & dbFileCmd.CommandText)

                    strParme = String.Empty
                    For iFile = 0 To dbFileCmd.Parameters.Count - 1
                        strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                    & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                    Next
                    logger.Info(UserCd & " " & strParme)

                    'INSERT SQL文を発行する
                    db.excute(dbFileCmd)

                Next
            End If

            '③ 案件リスク管理票テーブル(RISK_MANAGEMENT_LIST)へのINSERT
            '※各プロセスのリスク管理表(fleSp1-4RiskManagementList)・チェックリスト(fleSp1-4RiskCheckList)に添付ファイルが選択されていない場合は、INSERT処理を実行しない
            Dim strRiskFileSql As New StringBuilder
            'INSERTに必要な情報を取得し各プロセスの添付ファイル1件ごとにSQLを発行する
            '
            With strRiskFileSql
                .Append(" INSERT INTO RISK_MANAGEMENT_LIST(  ")
                .Append("        PROJECT_NO ")
                .Append("      , PROCESS_NO ")
                .Append("      , MANAGE_CATEGORY ")
                .Append("      , ATTATCH_FILE_NAME ")
                .Append("      , ATTATCH_FILE ")
                .Append("        )  ")
                .Append(" VALUES (  ")
                .Append("        :PROJECT_NO ")
                .Append("      , :PROCESS_NO ")
                .Append("      , :MANAGE_CATEGORY ")
                .Append("      , :ATTATCH_FILE_NAME ")
                .Append("      , :ATTATCH_FILE ")
                .Append("        )   ")
            End With

            '営業プロセス(原価) リスク管理表の場合

            If clsProject.fleSp1RiskManagementList <> String.Empty Then
                '添付ファイル参照ボタン(fleSp1RiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '0をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "0")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0、チェックリストの場合は1をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")
                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp1RiskManagementList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp1RiskManagementFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)

            End If

            '営業プロセス(原価) チェックリストの場合
            If clsProject.fleSp1RiskCheckList <> String.Empty Then
                '添付ファイル参照ボタン(fleSp1RiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '0をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "0")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")
                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp1RiskCheckList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp1RiskCheckFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)
            End If

            '営業プロセス(見積) リスク管理表の場合
            If clsProject.fleSp2RiskManagementList <> String.Empty Then
                '添付ファイル参照ボタン(fleSp2RiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '1をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "1")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp2RiskManagementList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp2RiskManagementFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)

            End If

            '営業プロセス(見積) チェックリストの場合
            If clsProject.fleSp2RiskCheckList <> String.Empty Then
                '添付ファイル参照ボタン(fleSp2RiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '1をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "1")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")
                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp2RiskCheckList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp2RiskCheckFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)
            End If

            '購買プロセス リスク管理表の場合
            If clsProject.flePpRiskManagementList <> String.Empty Then
                '添付ファイル参照ボタン(flePpRiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する

                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '2をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "2")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.flePpRiskManagementList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.flePpRiskManagementFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)

            End If

            '購買プロセス チェックリストの場合
            If clsProject.flePpRiskCheckList <> String.Empty Then
                '添付ファイル参照ボタン(fleSp2RiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '2をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "2")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")
                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.flePpRiskCheckList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.flePpRiskCheckFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)

            End If

            '設計・開発プロセス リスク管理表・チェックリストの場合
            If clsProject.fleDpRiskManagementList <> String.Empty Then
                '添付ファイル参照ボタン(fleDpRiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '3をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "3")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleDpRiskManagementList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleDpRiskManagemenFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)
            End If

            If clsProject.fleDpRiskCheckList <> String.Empty Then
                '添付ファイル参照ボタン(fleDpRiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
                Dim dbFileCmd As OracleCommand = New OracleCommand
                dbFileCmd.CommandText = strRiskFileSql.ToString

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", strProNo)

                '② @PROCESS_NO
                '3をセットする
                dbFileCmd.Parameters.Add("PROJECT_NO", "3")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")
                '④ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleDpRiskCheckList)

                '⑤ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleDpRiskCheckFileList)

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbFileCmd.Parameters.Count - 1
                    strParme = strParme & dbFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbFileCmd)
            End If

            '④ 案件プロセスフラグテーブル(PROJECT_PROCESS_FLG)へのINSERT
            'INSERTに必要な情報を取得し各プロセスごとに不要レコード・完了レコードINSERTのSQLを発行する　※必ず8回（プロセス4*不要・完了2=8）のINSERTを発行する
            Dim strProcessSql As New StringBuilder
            With strProcessSql
                .Append(" INSERT INTO PROJECT_PROCESS_FLG (  ")
                .Append("        PROJECT_NO ")
                .Append("      , PROCESS_NO ")
                .Append("      , FLG_CATEGORY ")
                .Append("      , FLAG)  ")
                .Append(" VALUES ( ")
                .Append("        :PROJECT_NO ")
                .Append("      , :PROCESS_NO ")
                .Append("      , :FLG_CATEGORY ")
                .Append("      , :FLAG) ")
            End With

            Dim dbProcessCmd As OracleCommand = New OracleCommand
            dbProcessCmd.CommandText = strProcessSql.ToString

            '営業プロセス(原価)不要レコードの場合
            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '0をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "0")
            '③ @FLG_CATEGORY
            '不要レコードの場合は0をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "0")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp1NoNeedFlg Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '営業プロセス(原価)完了レコードの場合
            dbProcessCmd.Parameters.Clear()

            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '0をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "0")
            '③ @FLG_CATEGORY
            '完了レコードの場合は1をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "1")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp1CompleteFlg Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '営業プロセス(見積)不要レコードの場合
            dbProcessCmd.Parameters.Clear()
            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '1をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "1")
            '③ @FLG_CATEGORY
            '不要レコードの場合は0をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "0")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp2NoNeedFlg Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '営業プロセス(見積)完了レコードの場合
            dbProcessCmd.Parameters.Clear()

            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '1をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "1")
            '③ @FLG_CATEGORY
            '完了レコードの場合は1をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "1")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp2CompleteFlg = "1" Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '購買プロセス不要レコードの場合
            dbProcessCmd.Parameters.Clear()
            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '2をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "2")
            '③ @FLG_CATEGORY
            '不要レコードの場合は0をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "0")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkPpNoNeedFlg = "1" Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '購買プロセス完了レコードの場合
            dbProcessCmd.Parameters.Clear()

            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '2をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "2")
            '③ @FLG_CATEGORY
            '完了レコードの場合は1をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "1")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkPpCompleteFlg Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '設計・開発プロセス不要レコードの場合
            dbProcessCmd.Parameters.Clear()
            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '3をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "3")
            '③ @FLG_CATEGORY
            '不要レコードの場合は0をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "0")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkDpNoNeedFlg Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            '設計・開発プロセス完了レコードの場合
            dbProcessCmd.Parameters.Clear()

            '① @PROJECT_NO
            '採番した新規案件番号をセットする
            dbProcessCmd.Parameters.Add("PROJECT_NO", strProNo)
            '② @PROCESS_NO
            '3をセットする
            dbProcessCmd.Parameters.Add("PROCESS_NO", "3")
            '③ @FLG_CATEGORY
            '完了レコードの場合は1をセットする
            dbProcessCmd.Parameters.Add("FLG_CATEGORY", "1")
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkDpCompleteFlg Then
                dbProcessCmd.Parameters.Add("FLAG", "1")
            Else
                dbProcessCmd.Parameters.Add("FLAG", "0")
            End If

            'INSERT発行前にログを出力する
            logger.Info(UserCd & " " & dbProcessCmd.CommandText)
            strParme = String.Empty
            For iFile = 0 To dbProcessCmd.Parameters.Count - 1
                strParme = strParme & dbProcessCmd.Parameters.Item(iFile).ParameterName _
                            & " = " & dbProcessCmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'INSERT SQL文を発行する
            db.excute(dbProcessCmd)

            'トランザクションのコミットを発行する
            db.close()

            'INSERT SQL文の正常発行ログを出力する
            logger.Info(UserCd & " " & "案件INSERT正常完了")

            Return strProNo
        Catch ex As Exception
            'INSERT処理でエラーが発生した場合はINSERT発行をロールバックし、画面上にメッセージ「案件の登録でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            db.close()
            'エラーログを出力する
            logger.Info(ex.ToString)
            Return String.Empty

        End Try

    End Function

    ''' <summary>
    ''' 修正登録案件
    ''' </summary>
    ''' <param name="clsProject">案件情報</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateProjectInfo(ByVal clsProject As ProjectClass, ByVal sectCd As String _
                                             , ByVal UserCd As String, ByVal UserId As String, ByVal UserName As String, arrDelete As ArrayList) As Boolean

        Dim db As DatabaseComm = New DatabaseComm
        Dim strSql As New StringBuilder

        Dim logger As log4net.ILog
        logger = log4net.LogManager.GetLogger("Global_asax")
        '①～⑦の合計7テーブルへの一連のDELETE処理はTRANSACTIONとし、いずれかのSQLでエラーが発生した場合はすべての処理をロールバックする
        Try
            db.open()
            'DELETE文を発行する
            Dim dbcmd As OracleCommand = New OracleCommand

            '① 案件テーブル(PROJECT)へのUPDATE
            '※①～④の合計4テーブルへの一連の更新処理はTRANSACTIONとし、いずれかのSQLでエラーが発生した場合はすべての処理をロールバックする
            'UPDATEに必要な情報を取得しSQLを発行する
            With strSql
                .Append(" 	UPDATE PROJECT SET ")
                .Append(" 	  PROCESS_NO                  = :PROCESS_NO      ")
                .Append(" 	  , PROJECT_NAME_TEMP         = :PROJECT_NAME_TEMP      ")
                .Append(" 	  , CUSTOMER_NAME             = :CUSTOMER_NAME      ")
                .Append(" 	  , ORDER_CD                  = :ORDER_CD      ")
                .Append(" 	  , RELATE_ORDER_CD           = :RELATE_ORDER_CD      ")
                .Append(" 	  , CUSTOMER_TYPE_NO          = :CUSTOMER_TYPE_NO      ")
                .Append(" 	  , PRODUCT_SECT_ID           = :PRODUCT_SECT_ID      ")
                .Append(" 	  , PRODUCT_SECT_CD           = :PRODUCT_SECT_CD      ")
                .Append(" 	  , PRODUCT_SECT_NM           = :PRODUCT_SECT_NM      ")
                .Append(" 	  , PRODUCT_USER_ID           = :PRODUCT_USER_ID      ")
                .Append(" 	  , PRODUCT_USER_CD           = :PRODUCT_USER_CD      ")
                .Append(" 	  , PRODUCT_USER_FULLNAME     = :PRODUCT_USER_FULLNAME      ")
                .Append(" 	  , BRANCH_TRANSACTION_FLG    = :BRANCH_TRANSACTION_FLG      ")
                .Append(" 	  , SUPPORT_BRANCH_ID         = :SUPPORT_BRANCH_ID      ")
                .Append(" 	  , SUPPORT_BRANCH_NM         = :SUPPORT_BRANCH_NM      ")
                .Append(" 	  , BRANCH_QUALITY_MANAGER    = :BRANCH_QUALITY_MANAGER      ")
                .Append(" 	  , SECTION_QUALITY_MANAGER   = :SECTION_QUALITY_MANAGER      ")
                .Append(" 	  , GROUP_QUALITY_MANAGER     = :GROUP_QUALITY_MANAGER      ")
                .Append(" 	  , PROJECT_QUALITY_MANAGER   = :PROJECT_QUALITY_MANAGER      ")
                .Append(" 	  , RISK_PREVENTION_MANAGER   = :RISK_PREVENTION_MANAGER      ")
                .Append(" 	  , RPM_500MIL_FLG            = :RPM_500MIL_FLG      ")
                .Append(" 	  , RPM_FIRST_PRODUCT_FLG     = :RPM_FIRST_PRODUCT_FLG      ")
                .Append(" 	  , FIRST_PRODUCT_NO          = :FIRST_PRODUCT_NO      ")
                .Append(" 	  , RPM_FIRST_PRODUCT_CAUSE   = :RPM_FIRST_PRODUCT_CAUSE      ")
                .Append(" 	  , RPM_SPECIAL_PRODUCT_FLG   = :RPM_SPECIAL_PRODUCT_FLG      ")
                .Append(" 	  , SPECIAL_PRODUCT_NO        = :SPECIAL_PRODUCT_NO      ")
                .Append(" 	  , RPM_SPECIAL_PRODUCT_CAUSE = :RPM_SPECIAL_PRODUCT_CAUSE      ")
                .Append(" 	  , RPM_TYPE                  = :RPM_TYPE      ")
                .Append(" 	  , SM_CHECKSHEET_FILE_NAME   = :SM_CHECKSHEET_FILE_NAME      ")
                .Append(" 	  , SM_CHECKSHEET_FILE        = :SM_CHECKSHEET_FILE      ")
                .Append(" 	  , PROJECT_TYPE_NO           = :PROJECT_TYPE_NO      ")
                .Append(" 	  , PROJECT_COMPLETE_FLG      = :PROJECT_COMPLETE_FLG      ")
                .Append(" 	  , MODIFIED_ON               = sysdate      ")
                .Append(" 	  , MODIFIED_USER_CD          = :MODIFIED_USER_CD      ")
                .Append(" 	  , MODIFIED_USER_ID          = :MODIFIED_USER_ID      ")
                .Append(" 	  , MODIFIED_USER_NAME        = :MODIFIED_USER_NAME       ")
                .Append(" 	WHERE PROJECT_NO              = :PROJECT_NO      ")
            End With
            dbcmd.CommandText = strSql.ToString

            'NULLの場合、半角スペースを設定 
            If clsProject.txtPjNameTemp = String.Empty Then
                clsProject.txtPjNameTemp = " "
            End If

            'NULLの場合、半角スペースを設定 
            If clsProject.txtCustomerName = String.Empty Then
                clsProject.txtCustomerName = " "
            End If

            '② @PROCESS_NO
            'プロセスラジオボタン(rdoProcess)の値をセットする
            dbcmd.Parameters.Add("PROCESS_NO", clsProject.rdoProcess)
            '③ @PROJECT_NAME_TEMP
            '工事名称（仮）(txtPjNameTemp)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NAME_TEMP", clsProject.txtPjNameTemp)

            '④ @CUSTOMER_NAME
            '顧客テキストボックス(txtCustomerName)の値をセットする
            dbcmd.Parameters.Add("CUSTOMER_NAME", clsProject.txtCustomerName)

            '⑤ @ORDER_CD
            'オーダテキストボックス(txtOrderCd)の値をセットする
            dbcmd.Parameters.Add("ORDER_CD", clsProject.txtOrderCd)
            '⑥ @RELATE_ORDER_CD
            '関連オーダテキストボックス(txtRelateOrderCd)の値をセットする
            dbcmd.Parameters.Add("RELATE_ORDER_CD", clsProject.txtRelateOrderCd)
            '⑦ @CUSTOMER_TYPE_NO
            '顧客区分ラジオボタン(rdoCustomerType)の値をセットする
            dbcmd.Parameters.Add("CUSTOMER_TYPE_NO", clsProject.rdoCustomerType)
            '⑧ @PRODUCT_SECT_ID
            '製造部門テキストボックス(txtProductSectNm)にセットした部門ID(A01ADMIN.A01M002_SECTION.A01M002_ID)をセットする
            dbcmd.Parameters.Add("PRODUCT_SECT_ID", clsProject.hdnProductSectId)
            '⑨ @PRODUCT_SECT_CD
            '製造部門テキストボックス(txtProductSectNm)にセットした部門コード(A01ADMIN.A01M002_SECTION.A01M002_SECT_CD)をセットする
            dbcmd.Parameters.Add("PRODUCT_SECT_CD", clsProject.hdnProductSectCd)
            '⑩ @PRODUCT_SECT_NM
            '製造部門テキストボックス(txtProductSectNm)の値をセットする
            dbcmd.Parameters.Add("PRODUCT_SECT_NM", clsProject.txtProductSectNm)
            '⑪ @PRODUCT_USER_ID
            '製造部門担当者テキストボックス(txtProductUser)にセットしたユーザID(A01ADMIN.A01M010_USER.A01M010_ID)をセットする
            dbcmd.Parameters.Add("PRODUCT_USER_ID", clsProject.hdnProductUserId)
            '⑫ @PRODUCT_USER_CD
            '製造部門担当者テキストボックス(txtProductUser)にセットしたユーザコード(A01ADMIN.A01M010_USER.A01M010_USER_CD)をセットする
            dbcmd.Parameters.Add("PRODUCT_USER_CD", clsProject.hdnProductUserCd)
            '⑬ @PRODUCT_USER_FULLNAME
            '製造部門担当者テキストボックス(txtProductUser)の値をセットする
            dbcmd.Parameters.Add("PRODUCT_USER_FULLNAME", clsProject.txtProductUser)
            '⑭ @BRANCH_TRANSACTION_FLG
            '支社間取引の有無ラジオボタン(rdoBranchTransactionFlg)の値をセットする
            dbcmd.Parameters.Add("BRANCH_TRANSACTION_FLG", clsProject.rdoBranchTransactionFlg)
            '⑮ @SUPPORT_BRANCH_ID
            '支援支社ドロップダウンリスト(ddlBranchList)の値(Value)をセットする
            dbcmd.Parameters.Add("SUPPORT_BRANCH_ID", clsProject.ddlBranchListId)
            '⑯ @SUPPORT_BRANCH_NM
            '支援支社ドロップダウンリスト(ddlBranchList)のテキスト(Text)をセットする
            dbcmd.Parameters.Add("SUPPORT_BRANCH_NM", clsProject.ddlBranchListNm)
            '⑰ @BRANCH_QUALITY_MANAGER
            '支社品質管理責任者テキストボックス(txtBranchQualityManager)の値をセットする
            dbcmd.Parameters.Add("BRANCH_QUALITY_MANAGER", clsProject.txtBranchQualityManager)
            '⑱ @SECTION_QUALITY_MANAGER
            '部品質管理責任者テキストボックス(txtSectionQualityManager)の値をセットする
            dbcmd.Parameters.Add("SECTION_QUALITY_MANAGER", clsProject.txtSectionQualityManager)
            '⑲ @GROUP_QUALITY_MANAGER
            'グループ品質管理責任者テキストボックス(txtGroupQualityManager)の値をセットする
            dbcmd.Parameters.Add("GROUP_QUALITY_MANAGER", clsProject.txtGroupQualityManager)
            '⑳ @PROJECT_QUALITY_MANAGER
            'プロジェクト品質管理責任者テキストボックス(txtProjectQualityManager)の値をセットする
            dbcmd.Parameters.Add("PROJECT_QUALITY_MANAGER", clsProject.txtProjectQualityManager)
            '㉑ @RISK_PREVENTION_MANAGER
            'リスク予防管理責任者テキストボックス(txtRiskPreventionManager)の値をセットする
            dbcmd.Parameters.Add("RISK_PREVENTION_MANAGER", clsProject.txtRiskPreventionManager)
            '㉒ @RPM_500MIL_FLG
            'リスク予防管理対象 500万円以上チェックボックス(chkRpm500MilFlg)がチェック状態の場合は1、未チェック状態の場合は0をセットする
            If clsProject.chkRpm500MilFlg Then
                dbcmd.Parameters.Add("RPM_500MIL_FLG", "1")
            Else
                dbcmd.Parameters.Add("RPM_500MIL_FLG", "0")
            End If

            '㉓ @RPM_FIRST_PRODUCT_FLG
            'リスク予防管理対象 初品チェックボックス(chkRpmFirstProductFlg)がチェック状態の場合は1、未チェック状態の場合は0をセットする
            If clsProject.chkRpmFirstProductFlg Then
                dbcmd.Parameters.Add("RPM_FIRST_PRODUCT_FLG", "1")
            Else
                dbcmd.Parameters.Add("RPM_FIRST_PRODUCT_FLG", "0")
            End If

            '㉔ @FIRST_PRODUCT_NO
            'リスク予防管理対象 初品ドロップダウンリスト(ddlFirstProduct)の値をセットする
            dbcmd.Parameters.Add("FIRST_PRODUCT_NO", clsProject.ddlFirstProduct)

            '㉕ @RPM_FIRST_PRODUCT_CAUSE
            'リスク予防管理対象 初品理由ドロップダウンリスト(ddlRpmFirstProductCause)の値をセットする
            dbcmd.Parameters.Add("RPM_FIRST_PRODUCT_CAUSE", clsProject.txtRpmFirstProductCause)

            '㉖ @RPM_SPECIAL_PRODUCT_FLG
            'リスク予防管理対象 特殊チェックボックス(chkRpmSpecialProductFlg)がチェック状態の場合は1、未チェック状態の場合は0をセットする
            If clsProject.chkRpmSpecialProductFlg Then
                dbcmd.Parameters.Add("RPM_SPECIAL_PRODUCT_FLG", "1")
            Else
                dbcmd.Parameters.Add("RPM_SPECIAL_PRODUCT_FLG", "0")
            End If

            '㉗ @SPECIAL_PRODUCT_NO
            'リスク予防管理対象 特殊ドロップダウンリスト(ddlSpecialProduct)の値をセットする
            dbcmd.Parameters.Add("SPECIAL_PRODUCT_NO", clsProject.ddlSpecialProduct)

            '㉘ @RPM_SPECIAL_PRODUCT_CAUSE
            'リスク予防管理対象 特殊理由ドロップダウンリスト(ddlRpmSpecialProductCause)の値をセットする
            dbcmd.Parameters.Add("RPM_SPECIAL_PRODUCT_CAUSE", clsProject.txtRpmSpecialProductCause)
            '㉙ @RPM_TYPE
            'リスク予防管理区分ラジオボタン(rdoRpmType)の値をセットする
            dbcmd.Parameters.Add("RPM_TYPE", clsProject.rdoRpmType)
            '㉚ @SM_CHECKSHEET_FILE_NAME,@SM_CHECKSHEET_FILE
            'いずれもNULLをセット
            dbcmd.Parameters.Add("SM_CHECKSHEET_FILE_NAME", String.Empty)
            dbcmd.Parameters.Add("SM_CHECKSHEET_FILE", String.Empty)
            '㉛ @PROJECT_TYPE_NO
            '案件タイプラジオボタン(rdoProjectType)の値をセットする
            dbcmd.Parameters.Add("PROJECT_TYPE_NO", clsProject.rdoProjectType)

            '㉜ @PROJECT_COMPLETE_FLG
            '以下の条件を満たしている場合は、1をセットする。そうでない場合は、0をセットする
            '営業プロセス(原価)の不要チェックボックス(chkSp1NoNeedFlg)・完了チェックボックス(chkSp1Complete)のいずれか以上がチェックされている
            'かつ
            '営業プロセス(見積)の不要チェックボックス(chkSp2NoNeedFlg)・完了チェックボックス(chkSp2Complete)のいずれか以上がチェックされている
            'かつ
            '購買プロセスの不要チェックボックス(chkSp3NoNeedFlg)・完了チェックボックス(chkSp3Complete)のいずれか以上がチェックされている
            'かつ
            '設計・開発プロセスの不要チェックボックス(chkSp4NoNeedFlg)・完了チェックボックス(chkSp4Complete)のいずれか以上がチェックされている
            If (clsProject.chkSp1NoNeedFlg Or clsProject.chkSp1CompleteFlg) _
                And (clsProject.chkSp2NoNeedFlg Or clsProject.chkSp2CompleteFlg) _
                And (clsProject.chkPpNoNeedFlg Or clsProject.chkPpCompleteFlg) _
                And (clsProject.chkDpNoNeedFlg Or clsProject.chkDpCompleteFlg) Then
                dbcmd.Parameters.Add("PROJECT_COMPLETE_FLG", "1")
            Else
                dbcmd.Parameters.Add("PROJECT_COMPLETE_FLG", "0")
            End If


            '㉞ @MODIFIED_USER_CD
            'Session変数UserCdの値をセットする
            dbcmd.Parameters.Add("MODIFIED_USER_CD", UserCd)
            '
            '㉟ @MODIFIED_USER_ID
            'Session変数UserIdの値をセットする
            dbcmd.Parameters.Add("MODIFIED_USER_ID", UserId)
            '
            '㊱ @MODIFIED_USER_NAME
            'Session変数UserNameの値をセットする
            dbcmd.Parameters.Add("MODIFIED_USER_NAME", UserName)

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strSql.ToString)

            Dim strParme As String = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            '② 案件添付ファイルテーブル(PROJECT_ATTATCH)への更新(INSERT,UPDATE,DELETE)
            '添付ファイル(N)の画面上の変更状況に応じて、添付ファイル1件ごとに以下の処理を実行する

            ' ===================================================
            ' ファイル削除：削除されたファイルをDBから削除
            ' ===================================================
            For Each strFileSeq As String In arrDelete
                Dim strAttachDelete = New StringBuilder
                With strAttachDelete
                    .Append("      DELETE PROJECT_ATTATCH  ")
                    .Append("       WHERE PROJECT_NO  = :PROJECT_NO    ")
                    .Append("         AND FILE_SEQ_NO = :FILE_SEQ_NO    ")
                End With

                dbcmd.Parameters.Clear()
                dbcmd.CommandText = strAttachDelete.ToString

                '案件番号テキストボックス(txtPjNo)の値をセットする
                dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                'FILE_SEQ_NO
                dbcmd.Parameters.Add("FILE_SEQ_NO", strFileSeq)

                'DELETE発行前にログを出力する
                logger.Info(UserCd & " " & strSql.ToString)

                strParme = String.Empty
                For iFile = 0 To dbcmd.Parameters.Count - 1
                    strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'DELETE SQL文を発行する
                db.excute(dbcmd)

            Next

            ' ===================================================
            ' ファイル採番：既存データから最大番号を取得
            ' ===================================================
            strSql = New StringBuilder
            With strSql
                .Append("SELECT NVL(MAX(FILE_SEQ_NO),0) AS FILE_SEQ_NO FROM PROJECT_ATTATCH WHERE PROJECT_NO = :PROJECT_NO")
            End With

            dbcmd.Parameters.Clear()
            dbcmd.CommandText = strSql.ToString
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            Dim dr As OracleDataReader = dbcmd.ExecuteReader()
            dr.Read()
            Dim fileSeqNo = Int32.Parse(dr.Item("FILE_SEQ_NO").ToString)



            ' ===================================================
            ' 品質推進会議添付ファイルテーブル(QUALITY_PROGRESSION_ATTATCH)への更新へのINSERT
            ' ===================================================
            Dim i = 0
            For i = 0 To clsProject.fleProjectAttatch.Count - 1
                'ファイル追加
                Dim strAttachInsert As New StringBuilder
                With strAttachInsert
                    .Append(" INSERT INTO PROJECT_ATTATCH(    ")
                    .Append("        PROJECT_NO   ")
                    .Append("      , FILE_SEQ_NO   ")
                    .Append("      , ATTATCH_FILE_NAME   ")
                    .Append("      , ATTATCH_FILE   ")
                    .Append("        )   ")
                    .Append(" VALUES (   ")
                    .Append("       :PROJECT_NO  ")
                    .Append("     , :FILE_SEQ_NO  ")
                    .Append("     , :ATTATCH_FILE_NAME  ")
                    .Append("     , :ATTATCH_FILE  ")
                    .Append("       )   ")
                End With

                dbcmd.Parameters.Clear()
                dbcmd.CommandText = strAttachInsert.ToString
                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @FILE_SEQ_NO
                '添付ファイル参照ボタン(fleProjectAttatch(N))の引数をセットする　※Nが1であれば1をセット
                dbcmd.Parameters.Add("FILE_SEQ_NO", fileSeqNo + i + 1)

                '③ @ATTATCH_FILE_NAME
                '添付ファイル名をセットする
                dbcmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleProjectAttatch(i).ToString)

                '④ @ATTATCH_FILE
                '添付ファイルのバイナリをセットする
                dbcmd.Parameters.Add("ATTATCH_FILE", clsProject.fleProjectAttatchFile(i))

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbcmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbcmd.Parameters.Count - 1
                    strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbcmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbcmd)

            Next

            '③ 案件リスク管理票テーブル(RISK_MANAGEMENT_LIST)への更新(INSERT,UPDATE,DELETE)
            '各プロセスのリスク管理表・チェックリストの画面上の変更状況に応じて、添付ファイル1件ごとに以下の処理を実行する
            Dim strRiskFileDeleteSql As New StringBuilder
            With strRiskFileDeleteSql
                .Append("  DELETE RISK_MANAGEMENT_LIST       ")
                .Append("   WHERE PROJECT_NO = :PROJECT_NO   ")
                .Append("     AND PROCESS_NO = :PROCESS_NO   ")
                .Append("     AND MANAGE_CATEGORY = :MANAGE_CATEGORY  ")

            End With

            Dim strRiskFileInsertSql As New StringBuilder
            'INSERTに必要な情報を取得し各プロセスの添付ファイル1件ごとにSQLを発行する
            With strRiskFileInsertSql
                .Append(" INSERT INTO RISK_MANAGEMENT_LIST(  ")
                .Append("        ATTATCH_FILE_NAME ")
                .Append("      , ATTATCH_FILE ")
                .Append("      , PROJECT_NO ")
                .Append("      , PROCESS_NO ")
                .Append("      , MANAGE_CATEGORY ")
                .Append("        )  ")
                .Append(" VALUES (  ")
                .Append("        :ATTATCH_FILE_NAME ")
                .Append("      , :ATTATCH_FILE ")
                .Append("      , :PROJECT_NO ")
                .Append("      , :PROCESS_NO ")
                .Append("      , :MANAGE_CATEGORY ")
                .Append("        )   ")
            End With

            Dim strRiskFileUpdateSql As New StringBuilder
            With strRiskFileUpdateSql
                .Append(" UPDATE RISK_MANAGEMENT_LIST SET ")
                .Append("        ATTATCH_FILE_NAME = :ATTATCH_FILE_NAME")
                .Append("      , ATTATCH_FILE = :ATTATCH_FILE ")
                .Append("  WHERE PROJECT_NO = :PROJECT_NO")
                .Append("    AND PROCESS_NO = :PROCESS_NO")
                .Append("    AND MANAGE_CATEGORY = :MANAGE_CATEGORY ")
            End With

            '営業プロセス(原価) リスク管理表の場合
            '添付ファイル参照ボタン(fleSp1RiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            Dim dbRmFileCmd As OracleCommand = New OracleCommand

            If clsProject.fleSp1RiskManagementListIsUpdate = "1" And _
                clsProject.fleSp1RiskManagementListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.fleSp1RiskManagementListIsUpdate = "1" And _
                clsProject.fleSp1RiskManagementListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.fleSp1RiskManagementListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString
            End If
            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.fleSp1RiskManagementListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp1RiskManagementList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp1RiskManagementFileList)
                End If

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '0をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "0")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0、チェックリストの場合は1をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '営業プロセス(原価) チェックリストの場合
            '添付ファイル参照ボタン(fleSp1RiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.fleSp1RiskCheckListIsUpdate = "1" And _
             clsProject.fleSp1RiskCheckListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.fleSp1RiskCheckListIsUpdate = "1" And _
                clsProject.fleSp1RiskCheckListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.fleSp1RiskCheckListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString
            End If

            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()

                '削除以外場合
                If clsProject.fleSp1RiskCheckListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp1RiskCheckList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp1RiskCheckFileList)
                End If
                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '0をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "0")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '営業プロセス(見積) リスク管理表の場合
            '添付ファイル参照ボタン(fleSp2RiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.fleSp2RiskManagementListIsUpdate = "1" And _
                clsProject.fleSp2RiskManagementListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.fleSp2RiskManagementListIsUpdate = "1" And _
                clsProject.fleSp2RiskManagementListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.fleSp2RiskManagementListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString

            End If

            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.fleSp2RiskManagementListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp2RiskManagementList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp2RiskManagementFileList)
                End If
                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '1をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "1")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '営業プロセス(見積) チェックリストの場合
            '添付ファイル参照ボタン(fleSp2RiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.fleSp2RiskCheckListIsUpdate = "1" And _
            clsProject.fleSp2RiskCheckListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.fleSp2RiskCheckListIsUpdate = "1" And _
                clsProject.fleSp2RiskCheckListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.fleSp2RiskCheckListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString

            End If
            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.fleSp2RiskCheckListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleSp2RiskCheckList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleSp2RiskCheckFileList)

                End If

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '1をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "1")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '購買プロセス リスク管理表の場合
            '添付ファイル参照ボタン(flePpRiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.flePpRiskManagementListIsUpdate = "1" And _
               clsProject.flePpRiskManagementListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.flePpRiskManagementListIsUpdate = "1" And _
                clsProject.flePpRiskManagementListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.flePpRiskManagementListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString

            End If

            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.flePpRiskManagementListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.flePpRiskManagementList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.flePpRiskManagementFileList)

                End If

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '2をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "2")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '購買プロセス チェックリストの場合
            '添付ファイル参照ボタン(fleSp2RiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.flePpRiskCheckListIsUpdate = "1" And _
               clsProject.flePpRiskCheckListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.flePpRiskCheckListIsUpdate = "1" And _
                   clsProject.flePpRiskCheckListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.flePpRiskCheckListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString

            End If

            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.flePpRiskCheckListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.flePpRiskCheckList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.flePpRiskCheckFileList)

                End If

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '2をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "2")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '設計・開発プロセス リスク管理表・チェックリストの場合
            '添付ファイル参照ボタン(fleDpRiskManagementList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.fleDpRiskManagementListIsUpdate = "1" And _
           clsProject.fleDpRiskManagementListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.fleDpRiskManagementListIsUpdate = "1" And _
                clsProject.fleDpRiskManagementListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.fleDpRiskManagementListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString

            End If

            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.fleDpRiskManagementListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleDpRiskManagementList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleDpRiskManagemenFileList)

                End If

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '3をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "3")

                '③ @MANAGE_CATEGORY
                'リスク管理表の場合は0をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "0")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '添付ファイル参照ボタン(fleDpRiskCheckList(N))に添付されているファイル分、1件ずつINSERT処理を実行する
            dbRmFileCmd.CommandText = String.Empty
            If clsProject.fleDpRiskCheckListIsUpdate = "1" And _
              clsProject.fleDpRiskCheckListIsDelete = "1" Then
                '削除SQL
                dbRmFileCmd.CommandText = strRiskFileDeleteSql.ToString

            ElseIf clsProject.fleDpRiskCheckListIsUpdate = "1" And _
                   clsProject.fleDpRiskCheckListIsDelete = "0" Then
                '更新SQL
                dbRmFileCmd.CommandText = strRiskFileUpdateSql.ToString

            ElseIf clsProject.fleDpRiskCheckListIsUpdate = "0" Then
                '登録SQL
                dbRmFileCmd.CommandText = strRiskFileInsertSql.ToString

            End If
            'SQL文設定場合
            If dbRmFileCmd.CommandText <> String.Empty Then

                dbRmFileCmd.Parameters.Clear()
                '削除以外場合
                If clsProject.fleDpRiskCheckListIsDelete <> "1" Then
                    '④ @ATTATCH_FILE_NAME
                    '添付ファイル名をセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE_NAME", clsProject.fleDpRiskCheckList)

                    '⑤ @ATTATCH_FILE
                    '添付ファイルのバイナリをセットする
                    dbRmFileCmd.Parameters.Add("ATTATCH_FILE", clsProject.fleDpRiskCheckFileList)

                End If

                '① @PROJECT_NO
                '採番した新規案件番号をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

                '② @PROCESS_NO
                '3をセットする
                dbRmFileCmd.Parameters.Add("PROJECT_NO", "3")

                '③ @MANAGE_CATEGORY
                'チェックリストの場合は1をセットする
                dbRmFileCmd.Parameters.Add("MANAGE_CATEGORY", "1")

                'INSERT発行前にログを出力する
                logger.Info(UserCd & " " & dbRmFileCmd.CommandText)
                strParme = String.Empty
                For iFile = 0 To dbRmFileCmd.Parameters.Count - 1
                    strParme = strParme & dbRmFileCmd.Parameters.Item(iFile).ParameterName _
                                & " = " & dbRmFileCmd.Parameters.Item(iFile).Value.ToString & " "
                Next
                logger.Info(UserCd & " " & strParme)

                'INSERT SQL文を発行する
                db.excute(dbRmFileCmd)
            End If

            '④ 案件プロセスフラグテーブル(PROJECT_PROCESS_FLG)へのUPDATE
            'UPDATEに必要な情報を取得し各プロセスごとに不要レコード・完了レコードUPDATEのSQLを発行する　※必ず8回（プロセス4*不要・完了2=8）のUPDATEを発行する
            Dim strProcessFlg As New StringBuilder
            With strProcessFlg
                .Append("      UPDATE PROJECT_PROCESS_FLG SET   ")
                .Append("             FLAG = :FLAG    ")
                .Append("       WHERE PROJECT_NO = :PROJECT_NO    ")
                .Append("         AND PROCESS_NO = :PROCESS_NO    ")
                .Append("         AND FLG_CATEGORY = :FLG_CATEGORY   ")
            End With

            dbcmd.CommandText = strProcessFlg.ToString
            dbcmd.Parameters.Clear()

            '営業プロセス(原価)の場合
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp1NoNeedFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '0をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "0")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "0")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            dbcmd.Parameters.Clear()

            '営業プロセス(原価)の場合
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp1CompleteFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '0をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "0")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "1")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            dbcmd.Parameters.Clear()
            '営業プロセス(見積)不要レコードの場合
            '④ @FLAG
            '不要/完了チェックボックス(chkSp1 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp2NoNeedFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '1をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "1")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "0")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            dbcmd.Parameters.Clear()

            '営業プロセス(見積)完了レコードの場合
            '④ @FLAG
            '不要/完了チェックボックス(chkSp2 NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkSp2CompleteFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '1をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "1")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "1")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            '購買プロセス不要レコードの場合
            dbcmd.Parameters.Clear()

            '④ @FLAG
            '不要/完了チェックボックス(chkPp NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkPpNoNeedFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '2をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "2")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "0")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            dbcmd.Parameters.Clear()

            '購買プロセス完了レコードの場合
            '④ @FLAG
            '不要/完了チェックボックス(chkPp NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkPpCompleteFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '2をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "2")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "1")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            '設計・開発プロセスの場合
            dbcmd.Parameters.Clear()

            '④ @FLAG
            '不要/完了チェックボックス(chkDp NoNeed/Complete Flg)がチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkDpNoNeedFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '3をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "3")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "0")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            dbcmd.Parameters.Clear()

            '購買プロセス完了レコードの場合
            '④ @FLAG
            '不要/完了チェックボックスがチェックされている場合は1、未チェックの場合は0をセットする
            If clsProject.chkDpCompleteFlg Then
                dbcmd.Parameters.Add("FLAG", "1")
            Else
                dbcmd.Parameters.Add("FLAG", "0")
            End If

            '① @PROJECT_NO
            '案件番号テキストボックス(txtPjNo)の値をセットする
            dbcmd.Parameters.Add("PROJECT_NO", clsProject.txtPjNo)

            '② @PROCESS_NO
            '3をセットする
            dbcmd.Parameters.Add("PROCESS_NO", "3")

            '③ @FLG_CATEGORY
            '不要レコードの場合は0、完了レコードの場合は1をセットする
            dbcmd.Parameters.Add("FLG_CATEGORY", "1")

            'UPDATE発行前にログを出力する
            logger.Info(UserCd & " " & strProcessFlg.ToString)

            strParme = String.Empty
            For iFile = 0 To dbcmd.Parameters.Count - 1
                strParme = strParme & dbcmd.Parameters.Item(iFile).ParameterName & " = " & dbcmd.Parameters.Item(iFile).Value & " "
            Next
            logger.Info(UserCd & " " & strParme)

            'UPDATE SQL文を発行する
            db.excute(dbcmd)

            'トランザクションのコミットを発行する
            db.close()

            '案件修正登録SQL文の正常発行ログを出力する
            logger.Info(UserCd & " " & "案件UPDATE正常完了")

            Return True
        Catch ex As Exception
            'UPDATE処理でエラーが発生した場合はUPDATE発行をロールバックして処理を中断する
            db.close()
            'エラーログを出力する
            logger.Info(ex.ToString)
            Return False

        End Try

        Return True
    End Function

    ''' <summary>
    ''' 案件削除
    ''' </summary>
    ''' <param name="PjNo">案件番号</param>
    ''' <param name="UserCd">Session変数UserCd</param>
    ''' <returns>案件削除SQL文の正常発行</returns>
    ''' <remarks>この案件に関連したリスク予防・管理検討会情報も削除</remarks>
    Public Shared Function DeleteProjectInfo(ByVal PjNo As String, ByVal UserCd As String) As Boolean
        Dim db As DatabaseComm = New DatabaseComm
        Dim strSql As String = String.Empty
        Dim logger As log4net.ILog
        Logger = log4net.LogManager.GetLogger("Global_asax")
        '①～⑦の合計7テーブルへの一連のDELETE処理はTRANSACTIONとし、いずれかのSQLでエラーが発生した場合はすべての処理をロールバックする
        Try
            db.open()
            '① リスク予防検討会添付ファイルテーブル(RISK_PREVENTION_ATTATCH)へのDELETE
            strSql = "DELETE FROM RISK_PREVENTION_ATTATCH WHERE PROJECT_NO = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            Dim dbcmd As OracleCommand = New OracleCommand
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            '② リスク予防検討会テーブル(RISK_PREVENTION)へのDELETE
            strSql = "DELETE FROM RISK_PREVENTION WHERE PROJECT_NO = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            '③ 品質推進テーブル(QUALITY_PROGRESSION_RELATE)へのDELETE
            strSql = "DELETE FROM QUALITY_PROGRESSION_RELATE WHERE PROJECT_NO = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            '④ 案件プロセスフラグテーブル(PROJECT_PROCESS_FLG)へのDELETE
            strSql = "DELETE FROM PROJECT_PROCESS_FLG WHERE PROJECT_NO = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            '⑤ 案件リスク管理表テーブル(RISK_MANAGEMENT_LIST)へのDELETE
            strSql = "DELETE FROM RISK_MANAGEMENT_LIST WHERE PROJECT_NO = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            '⑥ 案件添付ファイルテーブル(PROJECT_ATTATCH)へのDELETE
            strSql = "DELETE FROM PROJECT_ATTATCH WHERE PROJECT_NO = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            '⑦ 案件テーブル(PROJECT)へのDELETE
            strSql = "DELETE FROM PROJECT WHERE PROJECT_NO  = " & PjNo
            'DELETE発行前にログを出力する
            logger.Info(UserCd & " " & strSql)

            'DELETE文を発行する
            dbcmd.CommandText = strSql.ToString

            db.excute(dbcmd)

            'トランザクションのコミットを発行する
            db.close()

        Catch ex As Exception
            'DELETE発行処理でエラーが発生した場合はSQL発行をロールバックし、画面上にメッセージ「案件の削除でエラーが発生しました。システム管理者へお問合せください」を表示して処理を中断する
            db.close()
            'エラーログを出力する
            logger.Info(ex.ToString)
            Return False

        End Try

        '案件削除SQL文の正常発行ログを出力する
        logger.Info(UserCd & " " & "案件DELETE正常完了")
        Return True

    End Function

    ''' <summary>
    ''' 新規案件番号を採番する
    ''' </summary>
    ''' <param name="sectCd">支社コード</param>
    ''' <returns>新規案件番号</returns>
    ''' <remarks></remarks>
    Private Shared Function GetProjectNo(ByVal sectCd As String) As String
        Dim proNo As String = String.Empty
        Dim nowYear As String
        Dim sectCdTemp As String
        Dim dt As DataTable
        Dim strSql As New StringBuilder

        '新規案件番号を採番する
        'システム日付より西暦を取得する
        nowYear = Now.Year.ToString

        'Session変数UserSectCdの上2桁目を取得する（支社コード：1～6のいずれかとなる）
        sectCdTemp = sectCd.Substring(1, 1)

        '以下のSQLを発行し、当年・当該支社の最大案件番号を取得する
        With strSql
            .Append(" SELECT MAX(A.PROJECT_NO) AS MAX_PROJECT_NO  ")
            .Append("  FROM PROJECT A  ")
            .Append(" WHERE SUBSTR(TO_CHAR(A.PROJECT_NO), 1, 5) =:SECT_CD ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        'パラメタ：西暦+支社コード
        dbcmd.Parameters.Add("SECT_CD", nowYear & sectCdTemp)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        '取得レコードが存在する場合
        If dt.Rows.Count > 0 Then
            Dim maxProNo As String
            Dim proNoTemp As Integer
            'MAX_PROJECT_NOの下5桁を+1した番号を新規案件番号とする
            maxProNo = dt.Rows(0).Item("MAX_PROJECT_NO").ToString
            If Not String.IsNullOrEmpty(maxProNo) Then
                proNoTemp = Convert.ToInt16(maxProNo.Substring(maxProNo.Length - 5)) + 1
                proNo = maxProNo.Substring(0, maxProNo.Length - 5) & proNoTemp.ToString("00000")
                Return proNo
            Else
                '取得レコードが存在しない場合
                '西暦+支社コード+00001 を新規案件番号とする　例．2016200001
                proNo = nowYear & sectCdTemp & "00001"
                Return proNo
            End If
        End If
        Return proNo
    End Function

    ''' <summary>
    ''' 案件存在チェック
    ''' </summary>
    ''' <param name="ProjectNo" >案件番号</param>
    ''' <returns>責任者情報</returns>
    ''' <remarks></remarks>
    Public Shared Function CheckProjectIsExits(ByVal ProjectNo As String, ByRef clsProject As ProjectClass) As Boolean


        Dim dt As DataTable
        Dim strSql As New StringBuilder
        Dim userInfo As UserManagerClass = New UserManagerClass
        'SQLを発行する
        With strSql
            .Append("   SELECT *  ")
            .Append("     FROM PROJECT, A01ADMIN.A01M010_USER A")
            .Append("    WHERE PROJECT_NO = :PROJECT_NO ")
            .Append("      AND PROJECT.CREATED_USER_ID = A.A01M010_ID")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count = 0 Then
            Return False
        End If

        '案件情報設定
        clsProject.txtPjNo = dt.Rows(0).Item("PROJECT_NO").ToString
        clsProject.rdoProcess = dt.Rows(0).Item("PROCESS_NO").ToString
        clsProject.txtPjNameTemp = dt.Rows(0).Item("PROJECT_NAME_TEMP").ToString
        clsProject.txtCustomerName = dt.Rows(0).Item("CUSTOMER_NAME").ToString
        clsProject.txtOrderCd = dt.Rows(0).Item("ORDER_CD").ToString
        clsProject.txtRelateOrderCd = dt.Rows(0).Item("RELATE_ORDER_CD").ToString
        clsProject.rdoCustomerType = dt.Rows(0).Item("CUSTOMER_TYPE_NO").ToString
        clsProject.txtProductSectNm = dt.Rows(0).Item("PRODUCT_SECT_NM").ToString
        clsProject.a01m01SectCd = dt.Rows(0).Item("A01M010_SECT_CD").ToString

        clsProject.txtProductUser = dt.Rows(0).Item("PRODUCT_USER_FULLNAME").ToString
        clsProject.rdoBranchTransactionFlg = dt.Rows(0).Item("BRANCH_TRANSACTION_FLG").ToString
        clsProject.ddlBranchListId = dt.Rows(0).Item("SUPPORT_BRANCH_ID").ToString
        clsProject.txtBranchQualityManager = dt.Rows(0).Item("BRANCH_QUALITY_MANAGER").ToString
        clsProject.txtSectionQualityManager = dt.Rows(0).Item("SECTION_QUALITY_MANAGER").ToString
        clsProject.txtGroupQualityManager = dt.Rows(0).Item("GROUP_QUALITY_MANAGER").ToString
        clsProject.txtProjectQualityManager = dt.Rows(0).Item("PROJECT_QUALITY_MANAGER").ToString
        clsProject.txtRiskPreventionManager = dt.Rows(0).Item("RISK_PREVENTION_MANAGER").ToString
        clsProject.chkRpm500MilFlg = dt.Rows(0).Item("RPM_500MIL_FLG").ToString
        clsProject.ddlFirstProduct = dt.Rows(0).Item("FIRST_PRODUCT_NO").ToString
        clsProject.chkRpmFirstProductFlg = dt.Rows(0).Item("RPM_FIRST_PRODUCT_FLG").ToString
        clsProject.txtRpmFirstProductCause = dt.Rows(0).Item("RPM_FIRST_PRODUCT_CAUSE").ToString
        clsProject.chkRpmSpecialProductFlg = dt.Rows(0).Item("RPM_SPECIAL_PRODUCT_FLG").ToString
        clsProject.ddlSpecialProduct = dt.Rows(0).Item("SPECIAL_PRODUCT_NO").ToString
        clsProject.txtRpmSpecialProductCause = dt.Rows(0).Item("RPM_SPECIAL_PRODUCT_CAUSE").ToString
        clsProject.rdoRpmType = dt.Rows(0).Item("RPM_TYPE").ToString
        clsProject.rdoProjectType = dt.Rows(0).Item("PROJECT_TYPE_NO").ToString
        clsProject.createdUserId = dt.Rows(0).Item("CREATED_USER_ID").ToString
        clsProject.lblCreatedUserName = dt.Rows(0).Item("CREATED_USER_NAME").ToString
        clsProject.hdnModifiedOn = dt.Rows(0).Item("MODIFIED_ON").ToString
        clsProject.lblModifiedUserName = dt.Rows(0).Item("MODIFIED_USER_NAME").ToString

        clsProject.hdnProductSectId = dt.Rows(0).Item("PRODUCT_SECT_ID").ToString
        clsProject.hdnProductSectCd = dt.Rows(0).Item("PRODUCT_SECT_CD").ToString

        clsProject.hdnProductUserId = dt.Rows(0).Item("PRODUCT_USER_ID").ToString
        clsProject.hdnProductUserCd = dt.Rows(0).Item("PRODUCT_USER_CD").ToString

        Return True
    End Function

    ''' <summary>
    ''' オーダ関連の情報取得
    ''' </summary>
    ''' <returns>オーダ関連の情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRelateOrderInfo(ByVal orderCd As String) As RelateOrderClass

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        Dim reOrderInfo As New RelateOrderClass
        'SQLを発行する
        With strSql
            .Append("   SELECT A.A01M009_ORDER_NM    AS A01M009_ORDER_NM")
            .Append("        , E.A01M015_COMPY_NM    AS A01M015_COMPY_NM")
            .Append("        , B.A01M014_JYUCHU_CRR  AS A01M014_JYUCHU_CRR")
            .Append("        , B.A01M014_NOKI_YMD    AS A01M014_NOKI_YMD")
            .Append("        , C.A01M002_ALLSECT_NM  AS A01M002_ALLSECT_NM")
            .Append("        , D.A01M010_FULLNAME    AS A01M010_FULLNAME")
            .Append("     FROM A01ADMIN.A01M009_ORDER A")
            .Append("        , A01ADMIN.A01M014_ORDINFO B")
            .Append("        , A01ADMIN.A01M002_SECTION C")
            .Append("        , A01ADMIN.A01M010_USER D")
            .Append("        , A01ADMIN.A01M015_COMPY E ")
            .Append("    WHERE A.A01M009_ORDER_CD = :ORDER_CD ")
            .Append("      AND A.A01M009_ORDER_CD = B.A01M014_ORDER_CD ")
            .Append("      AND B.A01M014_JYUCHU_SECT_ID = C.A01M002_ID ")
            .Append("      AND B.A01M014_JYUCHU_USER_ID = D.A01M010_ID ")
            .Append("      AND B.A01M014_CUSTM_ID = E.A01M015_ID ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("ORDER_CD", orderCd)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then

            reOrderInfo.a01m009OrderNm = dt.Rows(0).Item("A01M009_ORDER_NM").ToString
            reOrderInfo.a01m015CompyNm = dt.Rows(0).Item("A01M015_COMPY_NM").ToString
            reOrderInfo.a01m014JyuchuCrr = dt.Rows(0).Item("A01M014_JYUCHU_CRR").ToString
            reOrderInfo.a01m014NokiYmd = dt.Rows(0).Item("A01M014_NOKI_YMD").ToString
            reOrderInfo.a01m002AllsectNm = dt.Rows(0).Item("A01M002_ALLSECT_NM").ToString
            reOrderInfo.a01m010Fullname = dt.Rows(0).Item("A01M010_FULLNAME").ToString
        End If

        Return reOrderInfo

    End Function

    ''' <summary>
    ''' リスク不安要素検討表の情報取得
    ''' </summary>
    ''' <returns>オーダ関連の情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRiskManagementListInfo(ByVal ProjectNo As String, _
                                                     ByVal ProcessNo As String, _
                                                     ByVal ManageCategory As String, _
                                                     ByRef clsProject As ProjectClass) As Boolean

        Dim dt As DataTable
        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append("  SELECT ATTATCH_FILE_NAME  ")
            .Append("       , ATTATCH_FILE ")
            .Append("    FROM RISK_MANAGEMENT_LIST ")
            .Append("   WHERE PROJECT_NO = :PROJECT_NO    ")
            .Append("     AND PROCESS_NO = :PROCESS_NO     ")
            .Append("     AND MANAGE_CATEGORY = :MANAGE_CATEGORY  ")
            .Append("   ORDER BY PROJECT_NO, PROCESS_NO, MANAGE_CATEGORY     ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)
        dbcmd.Parameters.Add("PROCESS_NO", ProcessNo)
        dbcmd.Parameters.Add("MANAGE_CATEGORY", ManageCategory)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then

            Select Case ProcessNo
                Case 0
                    '営業プロセス(原価) 
                    If ManageCategory = "0" Then
                        clsProject.fleSp1RiskManagementList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    Else
                        clsProject.fleSp1RiskCheckList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    End If
                Case 1
                    If ManageCategory = "0" Then
                        clsProject.fleSp2RiskManagementList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    Else
                        clsProject.fleSp2RiskCheckList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    End If
                Case 2
                    If ManageCategory = "0" Then
                        clsProject.flePpRiskManagementList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    Else
                        clsProject.flePpRiskCheckList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    End If
                Case 3
                    If ManageCategory = "0" Then
                        clsProject.fleDpRiskManagementList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    Else
                        clsProject.fleDpRiskCheckList = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
                    End If
            End Select
        End If
        Return True

    End Function

    ''' <summary>
    ''' 不要・完了の情報取得
    ''' </summary>
    ''' <returns>不要・完了の情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetNoNeedCompleteInfo(ByVal ProjectNo As String, _
                                                     ByVal ProcessNo As String, _
                                                     ByVal FlgCategory As String, _
                                                     ByRef clsProject As ProjectClass) As Boolean

        Dim dt As DataTable
        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append("  SELECT FLAG  ")
            .Append("    FROM PROJECT_PROCESS_FLG ")
            .Append("   WHERE PROJECT_NO = :PROJECT_NO    ")
            .Append("     AND PROCESS_NO = :PROCESS_NO     ")
            .Append("     AND FLG_CATEGORY = :FLG_CATEGORY  ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)
        dbcmd.Parameters.Add("PROCESS_NO", ProcessNo)
        dbcmd.Parameters.Add("FLG_CATEGORY", FlgCategory)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then

            Select Case ProcessNo
                Case 0
                    '営業プロセス(原価) 
                    If FlgCategory = "0" Then
                        clsProject.chkSp1NoNeedFlg = dt.Rows(0).Item("FLAG").ToString
                    Else
                        clsProject.chkSp1CompleteFlg = dt.Rows(0).Item("FLAG").ToString
                    End If
                Case 1
                    If FlgCategory = "0" Then
                        clsProject.chkSp2NoNeedFlg = dt.Rows(0).Item("FLAG").ToString
                    Else
                        clsProject.chkSp2CompleteFlg = dt.Rows(0).Item("FLAG").ToString
                    End If
                Case 2
                    If FlgCategory = "0" Then
                        clsProject.chkPpNoNeedFlg = dt.Rows(0).Item("FLAG").ToString
                    Else
                        clsProject.chkPpCompleteFlg = dt.Rows(0).Item("FLAG").ToString
                    End If
                Case 3
                    If FlgCategory = "0" Then
                        clsProject.chkDpNoNeedFlg = dt.Rows(0).Item("FLAG").ToString
                    Else
                        clsProject.chkDpCompleteFlg = dt.Rows(0).Item("FLAG").ToString
                    End If

            End Select
        End If

        Return True
    End Function

    ''' <summary>
    ''' リスク予防・管理検討会情報取得
    ''' </summary>
    ''' <param name="ProjectNo">案件NO</param>
    ''' <returns>スク予防・管理検討会情報</returns>
    ''' <remarks></remarks>
    Public Shared Function GetRiskPreventionList(ByVal ProjectNo As String) As DataTable
        Dim dtRisk As New DataTable

        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append(" SELECT PROJECT_NO  ")
            .Append("      , SEQ_NO  ")
            .Append("      , REPORT_CATEGORY  ")
            .Append("      , OPEN_DATE  ")
            .Append("      , OPEN_ROUND   ")
            .Append("   FROM RISK_PREVENTION   ")
            .Append("  WHERE PROJECT_NO = :PROJECT_NO   ")
            .Append("  ORDER BY SEQ_NO   ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)

        dtRisk = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dtRisk
    End Function

    ''' <summary>
    ''' 案件存在チェック
    ''' </summary>
    ''' <param name="ProjectNo">案件NO</param>
    ''' <returns>件数</returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectCount(ByVal ProjectNo As String) As Integer
        Dim dtInfo As New DataTable
        Dim iniCount As Integer = 0
        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append(" SELECT COUNT(PROJECT_NO) AS PROJECT_CNT  ")
            .Append("   FROM PROJECT A   ")
            .Append("  WHERE A.PROJECT_NO = :PROJECT_NO  ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)

        dtInfo = DatabaseComm.DbSearchAdapter(dbcmd)
        If dtInfo.Rows.Count > 0 Then
            If dtInfo.Rows(0).Item("PROJECT_CNT").ToString = String.Empty Then
                iniCount = 0
            Else
                iniCount = Convert.ToInt32(dtInfo.Rows(0).Item("PROJECT_CNT").ToString)
            End If
        End If

        Return iniCount
    End Function

    ''' <summary>
    ''' 案件の最終更新日時取得
    ''' </summary>
    ''' <param name="ProjectNo">案件NO</param>
    ''' <returns>最終更新日</returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectUpdateDate(ByVal ProjectNo As String) As String
        Dim dtInfo As New DataTable
        Dim strUpdate As String = String.Empty

        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append(" SELECT MODIFIED_ON  ")
            .Append("   FROM PROJECT A   ")
            .Append("  WHERE A.PROJECT_NO = :PROJECT_NO  ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)

        dtInfo = DatabaseComm.DbSearchAdapter(dbcmd)
        If dtInfo.Rows.Count > 0 Then
            strUpdate = dtInfo.Rows(0).Item("MODIFIED_ON").ToString
        End If

        Return strUpdate
    End Function

    ''' <summary>
    ''' 案件の添付ファイル取得
    ''' </summary>
    ''' <param name="ProjectNo">案件NO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectAttatch(ByVal ProjectNo As String, ByRef clsProject As ProjectClass) As Boolean
        Dim dtInfo As New DataTable
        Dim strUpdate As String = String.Empty

        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append(" SELECT FILE_SEQ_NO ")
            .Append("      , ATTATCH_FILE_NAME ")
            .Append("      , ATTATCH_FILE ")
            .Append("   FROM PROJECT_ATTATCH    ")
            .Append("  WHERE PROJECT_NO =:PROJECT_NO  ")
            .Append("  ORDER BY FILE_SEQ_NO  ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)

        dtInfo = DatabaseComm.DbSearchAdapter(dbcmd)

        clsProject.fleProjectAttatchSeqNo.Clear()
        clsProject.fleProjectAttatch.Clear()

        For Each row In dtInfo.Rows
            clsProject.fleProjectAttatchSeqNo.Add(row("FILE_SEQ_NO").ToString)
            clsProject.fleProjectAttatch.Add(row("ATTATCH_FILE_NAME").ToString)
        Next
        Return True
    End Function

    ''' <summary>
    ''' リスク不安要素検討表のファイル取得
    ''' </summary>
    ''' <param name="ProjectNo">案件番号</param>
    ''' <param name="ProcessNo">プロセス</param>
    ''' <param name="ManageCategory">小分類</param>
    ''' <param name="FileName">ファイル名</param>
    ''' <param name="File">ファイル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRiskManagementListFile(ByVal ProjectNo As String, _
                                                     ByVal ProcessNo As String, _
                                                     ByVal ManageCategory As String, _
                                                     ByRef FileName As String, _
                                                     ByRef File As Byte()) As Boolean

        Dim dt As DataTable
        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append("  SELECT ATTATCH_FILE_NAME  ")
            .Append("       , ATTATCH_FILE ")
            .Append("    FROM RISK_MANAGEMENT_LIST ")
            .Append("   WHERE PROJECT_NO = :PROJECT_NO    ")
            .Append("     AND PROCESS_NO = :PROCESS_NO     ")
            .Append("     AND MANAGE_CATEGORY = :MANAGE_CATEGORY  ")
            .Append("   ORDER BY PROJECT_NO, PROCESS_NO, MANAGE_CATEGORY     ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)
        dbcmd.Parameters.Add("PROCESS_NO", ProcessNo)
        dbcmd.Parameters.Add("MANAGE_CATEGORY", ManageCategory)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)
        If dt.Rows.Count > 0 Then
            FileName = dt.Rows(0).Item("ATTATCH_FILE_NAME").ToString
            File = dt.Rows(0).Item("ATTATCH_FILE")
        End If
        Return True

    End Function


    ''' <summary>
    ''' 案件の添付ファイル取得
    ''' </summary>
    ''' <param name="ProjectNo">案件NO</param>
    ''' <param name="FileSeqNo">SEQ</param>
    ''' <param name="FileName">ファイル名</param>
    ''' <param name="File">ファイル</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetProjectAttatchFile(ByVal ProjectNo As String, _
                                                 ByVal FileSeqNo As String, _
                                                 ByRef FileName As String, _
                                                 ByRef File As Byte()) As Boolean
        Dim dtInfo As New DataTable
        Dim strUpdate As String = String.Empty

        Dim strSql As New StringBuilder

        'SQLを発行する
        With strSql
            .Append(" SELECT FILE_SEQ_NO ")
            .Append("      , ATTATCH_FILE_NAME ")
            .Append("      , ATTATCH_FILE ")
            .Append("   FROM PROJECT_ATTATCH    ")
            .Append("  WHERE PROJECT_NO =:PROJECT_NO  ")
            .Append("    AND FILE_SEQ_NO =:FILE_SEQ_NO  ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString

        'パラメタ
        dbcmd.Parameters.Add("PROJECT_NO", ProjectNo)
        dbcmd.Parameters.Add("FILE_SEQ_NO", FileSeqNo)

        dtInfo = DatabaseComm.DbSearchAdapter(dbcmd)
        If dtInfo.Rows.Count > 0 Then
            FileName = dtInfo.Rows(0).Item("ATTATCH_FILE_NAME").ToString
            File = dtInfo.Rows(0).Item("ATTATCH_FILE")
        End If

        Return True
    End Function

    ''' <summary>
    ''' 添付ファイルの情報を取得
    ''' </summary>
    ''' <param name="proNo">案件NO</param>
    ''' <returns></returns>
    ''' <remarks>添付ファイルの情報を取得</remarks>
    Public Shared Function GetProjectAttatchInfo(proNo As String) As DataTable
        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT FILE_SEQ_NO, ")
            .Append("        ATTATCH_FILE_NAME ")
            .Append("   FROM PROJECT_ATTATCH    ")
            .Append("  WHERE PROJECT_NO =:PROJECT_NO  ")
            .Append("  ORDER BY FILE_SEQ_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", proNo)

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function

    ''' <summary>
    ''' 添付ファイルの情報を取得
    ''' </summary>
    ''' <param name="qpNo"></param>
    ''' <param name="fileSeqNo"></param>
    ''' <returns></returns>
    Public Shared Function GetProjectAttatch(qpNo As String,
                                                Optional fileSeqNo As String = "") As DataTable

        Dim dt As DataTable
        Dim strSql As New StringBuilder
        With strSql
            .Append(" SELECT FILE_SEQ_NO, ")
            .Append("        ATTATCH_FILE_NAME, ")
            .Append("        ATTATCH_FILE ")
            .Append("   FROM PROJECT_ATTATCH ")
            .Append("  WHERE PROJECT_NO =:PROJECT_NO  ")
            If Not fileSeqNo = "" Then
                .Append("    AND FILE_SEQ_NO = :FILE_SEQ_NO ")
            End If

            .Append("  ORDER BY FILE_SEQ_NO ")
        End With

        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql.ToString
        dbcmd.Parameters.Add("PROJECT_NO", qpNo)
        If Not fileSeqNo = "" Then
            dbcmd.Parameters.Add("FILE_SEQ_NO", fileSeqNo)
        End If

        dt = DatabaseComm.DbSearchAdapter(dbcmd)

        Return dt

    End Function
End Class
