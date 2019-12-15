Public Class CheckComm
    ''' <summary>
    ''' ユーザー取得
    ''' </summary>
    ''' <param name="loginpage">呼出ページ</param>
    ''' <returns>ユーザー名</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLocalUser(ByVal loginpage As Page) As String
        Dim domainAndName As String

        domainAndName = loginpage.User.Identity.Name
        Return domainAndName

    End Function

    
End Class
