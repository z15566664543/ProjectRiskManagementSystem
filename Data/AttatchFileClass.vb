Public Class AttatchFileClass
    ''' <summary>
    ''' FILE_SEQ_NO
    ''' </summary>
    ''' <remarks></remarks>
    Private _file_seq_no As String
    Public Property file_seq_no() As String
        Get
            Return _file_seq_no
        End Get
        Set(ByVal value As String)
            _file_seq_no = value
        End Set
    End Property

    ''' <summary>
    ''' ATTATCH_FILE_NAME
    ''' </summary>
    ''' <remarks></remarks>
    Private _attatch_file_name As String
    Public Property attatch_file_name() As String
        Get
            Return _attatch_file_name
        End Get
        Set(ByVal value As String)
            _attatch_file_name = value
        End Set
    End Property

    ''' <summary>
    ''' ATTATCH_FILE
    ''' </summary>
    ''' <remarks></remarks>
    Private _attatch_file As New Byte()
    Public Property attatch_file() As String
        Get
            Return _attatch_file
        End Get
        Set(ByVal value As String)
            _attatch_file = value
        End Set
    End Property
End Class
