Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Dim logger As log4net.ILog

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        logger = log4net.LogManager.GetLogger("Global_asax")
        logger.Info("アプリケーション開始Application_Start!")

    End Sub

    ''' <summary>
    ''' Session開始
    ''' </summary>
    ''' <param name="sender">イベントのソース</param>
    ''' <param name="e">データを格納している System.EventArgs</param>
    ''' <remarks></remarks>
    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)

        'アクセスユーザのドメインアカウントを取得する
        Dim userID As String = Environment.GetEnvironmentVariable("USERNAME")

        '管理者システム管理者テーブルのデータ
        Dim dataAdmin As DataTable

        '社員マスタのデータ
        Dim dataUser As DataTable

        Dim Msg As String = MessageComm.GetMessageContext("1", "302", Nothing)

        If String.IsNullOrEmpty(userID) Then
            '取得したドメインアカウントがNullまたはブランクの場合は以下のメッセージを表示してすべての画面を閉じる。
            Response.Write("<script language='javascript'>alert(""" & Msg & """)</script>")
            Response.Write("<script language='javascript'>window.pener=null;window.open('','_self');window.close();</script>")

        Else

            '取得したドメインアカウントを引数としてシステム管理者テーブルへSelect Countを実行する
            dataAdmin = MenuEntity.GetAdminSession(userID)

            If dataAdmin.Rows(0).Item("ADMIN_CNT") = "0" Then

                '結果が0の場合は、Session変数AdminFlgに"0"を格納する
                Session(SessionComm.SESSION_ADMIN_FLG) = "0"
            ElseIf dataAdmin.Rows(0)("ADMIN_CNT") >= "1" Then

                '結果が1以上の場合は、Session変数AdminFlgに"1"を格納する
                Session(SessionComm.SESSION_ADMIN_FLG) = "1"
            End If

            '取得したドメインアカウントを引数として社員マスタへSelectを実行する
            dataUser = MenuEntity.GetUserSession(userID)
            If Not dataUser.Rows.Count = 0 Then

                '結果よりA01M010_SECT_CDの値をSession変数UserSectCdに格納する
                Session(SessionComm.SESSION_USER_SECT_CD) = dataUser.Rows(0).Item("A01M010_SECT_CD")

                '結果よりA01M010_AD5POSTCLS_CDの値をSession変数UserPostClsCdに格納する
                Session(SessionComm.SESSION_USER_POST_CLS_CD) = dataUser.Rows(0).Item("A01M010_AD5POSTCLS_CD")

                '結果よりA01M010_IDの値をSession変数UserIdに格納する
                Session(SessionComm.SESSION_USER_ID) = dataUser.Rows(0).Item("A01M010_ID")

                '結果よりA01M010_USER_CDの値をSession変数UserCdに格納する
                Session(SessionComm.SESSION_USER_CD) = dataUser.Rows(0).Item("A01M010_USER_CD")

                '結果よりA01M010_FULLNAMEの値をSession変数UserNameに格納する
                Session(SessionComm.SESSION_USER_NAME) = dataUser.Rows(0).Item("A01M010_FULLNAME")

                'Session変数SearchTypeに1をセットする
                Session(SessionComm.SESSION_SEARCH_TYPE) = "1"

                'ProjectList.aspxを表示する
                Response.Redirect("ProjectList.aspx", True)
            Else
                Response.Write("<script language='javascript'>alert(""" & Msg & """)</script>")
                Response.Write("<script language='javascript'>window.opener=null;window.open('','_self');window.close();</script>")
            End If
        End If
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
        logger.Info("Application_End!")
    End Sub

End Class