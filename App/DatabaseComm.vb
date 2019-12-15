Imports Oracle.DataAccess.Client

Public Class DatabaseComm

    Dim dbConnection As OracleConnection
    Dim dbTrans As OracleTransaction
    Dim hasError As Boolean

    ''' <summary>
    ''' DB検索
    ''' </summary>
    ''' <param name="strSql">検索SQL</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Shared Function DbSearchAdapter(ByVal strSql As String) As DataTable
        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql
        Return DbSearchAdapter(dbcmd)
    End Function

    ''' <summary>
    ''' DB検索
    ''' </summary>
    ''' <param name="dbcmd">検索SQL</param>
    ''' <returns>データテーブル</returns>
    ''' <remarks></remarks>
    Public Shared Function DbSearchAdapter(ByVal dbcmd As OracleCommand) As DataTable
        Dim strConn = setConnectString()
        Dim dt = New DataTable("ds")
        Dim dbConnection As OracleConnection = New OracleConnection(strConn)

        Try
            dbConnection.Open()
            dbcmd.Connection = dbConnection
            Dim da As OracleDataAdapter = New OracleDataAdapter(dbcmd)
            da.Fill(dt)

        Catch ex As Exception
            Throw ex
        Finally
            If Not dbConnection Is Nothing Then
                dbConnection.Close()
            End If
        End Try
        Return dt
        Exit Function
    End Function

    ''' <summary>
    ''' DB検索(複数)
    ''' </summary>
    ''' <param name="strSqlList">検索SQLリスト</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Public Shared Function DbSearchAdapter(ByVal strSqlList As String()) As DataSet
        Dim strConn = setConnectString()
        Dim dbcmd As OracleCommand
        Dim dt = New DataTable
        Dim ds = New DataSet

        Dim dbConnection As OracleConnection = New OracleConnection(strConn)

        Try
            dbConnection.Open()
            dbcmd = dbConnection.CreateCommand
            Dim indexTable As Integer = 0
            For Each strSql In strSqlList
                dt = New DataTable("Table" + indexTable.ToString)

                dbcmd.CommandText = strSql
                Dim da As OracleDataAdapter = New OracleDataAdapter(dbcmd)
                da.Fill(dt)
                ds.Tables.Add(dt)
                indexTable = indexTable + 1
            Next
            dbConnection.Close()
        Catch ex As Exception
            Throw ex
        Finally
            If Not dbConnection Is Nothing Then
                dbConnection.Close()
            End If
        End Try

        Return ds

    End Function


    ''' <summary>
    ''' 更新DB
    ''' </summary>
    ''' <param name="dbcmd">更新用SQLコマンド</param>
    ''' <remarks></remarks>
    Public Function excute(dbcmd As OracleCommand) As Boolean
        '
        Dim count = 0
        Try
            ' Assign transaction object for a pending local transaction
            dbcmd.Connection = dbConnection
            dbcmd.Transaction = dbTrans
            count = dbcmd.ExecuteNonQuery()
        Catch ex As Exception
            hasError = True
            If Not dbConnection Is Nothing Then
                close()
            End If
            Throw ex
        Finally

        End Try

        Return hasError
    End Function

    ''' <summary>
    ''' 更新DB
    ''' </summary>
    ''' <param name="strSql">更新用SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function excute(ByVal strSql As String) As Boolean
        Dim dbcmd As OracleCommand = New OracleCommand
        dbcmd.CommandText = strSql
        Return excute(dbcmd)
    End Function

    ''' <summary>
    ''' トランザクションを管理するため、DBに接続
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function open() As OracleConnection
        Dim strConn = setConnectString()
        dbConnection = New OracleConnection(strConn)
        hasError = False

        Try
            dbConnection.Open()
            dbTrans = dbConnection.BeginTransaction(IsolationLevel.ReadCommitted)
        Catch ex As Exception
            hasError = True
            close()
            Throw ex
        End Try
        Return dbConnection

    End Function

    ''' <summary>
    ''' トランザクションを管理し、DBをクローズ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub close()

        If Not dbTrans Is Nothing Then
            If hasError Then
                dbTrans.Rollback()
            Else
                dbTrans.Commit()
            End If
            dbTrans = Nothing

        End If

        If Not dbConnection Is Nothing Then
            dbConnection.Close()
            dbConnection = Nothing
        End If
    End Sub

    ''' <summary>
    ''' DB connect 文字列
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function setConnectString() As String

        Dim server As String = ConfigurationManager.AppSettings("DB_Server")
        Dim serverNm As String = ConfigurationManager.AppSettings("DB_Service_Name")
        Dim user As String = ConfigurationManager.AppSettings("DB_User")
        Dim pwd As String = ConfigurationManager.AppSettings("DB_Password")
        Dim port As String = ConfigurationManager.AppSettings("DB_Port")
        Dim strConn As String = String.Empty

        strConn = "Data Source=" + server + "/" + serverNm + ";User Id=" + user + ";Password=" + pwd

        Return strConn
    End Function

End Class
