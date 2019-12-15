''' <summary>
''' 管理責任者クラス
''' </summary>
''' <remarks></remarks>
Public Class UserManagerClass
    '部門名
    Private _AllsectNm As String
    ''' <summary>
    ''' 部門名
    ''' </summary>
    ''' <value>A01M002_ALLSECT_NM</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllsectNm() As String
        Get
            Return _AllsectNm
        End Get
        Set(ByVal value As String)
            _AllsectNm = value
        End Set
    End Property


    '役職名
    Private _PostclsNm As String
    ''' <summary>
    ''' 役職名
    ''' </summary>
    ''' <value>A01M003_POSTCLS_NM</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostclsNm() As String
        Get
            Return _PostclsNm
        End Get
        Set(ByVal value As String)
            _PostclsNm = value
        End Set
    End Property

    '氏名
    Private _Fullname As String
    ''' <summary>
    ''' 氏名
    ''' </summary>
    ''' <value>A01M010_FULLNAME</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Fullname() As String
        Get
            Return _Fullname
        End Get
        Set(ByVal value As String)
            _Fullname = value
        End Set
    End Property

End Class
