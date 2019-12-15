''' <summary>
''' 案件クラス
''' </summary>
''' <remarks></remarks>
Public Class ProjectClass

    'Session変数UserName
    Private _lblCreatedUserName As String = String.Empty
    Public Property lblCreatedUserName() As String
        Get
            Return _lblCreatedUserName
        End Get
        Set(ByVal value As String)
            _lblCreatedUserName = value
        End Set
    End Property

    ''' <summary>
    ''' 更新担当者 
    ''' </summary>
    ''' <remarks></remarks>
    Private _lblModifiedUserName As String = String.Empty
    Public Property lblModifiedUserName() As String
        Get
            Return _lblModifiedUserName
        End Get
        Set(ByVal value As String)
            _lblModifiedUserName = value
        End Set
    End Property

    ''' <summary>
    ''' 案件番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtPjNo As String = String.Empty
    Public Property txtPjNo() As String
        Get
            Return _txtPjNo
        End Get
        Set(ByVal value As String)
            _txtPjNo = value
        End Set
    End Property

    ''' <summary>
    ''' プロセス
    ''' </summary>
    ''' <remarks></remarks>
    Private _rdoProcess As String = String.Empty
    Public Property rdoProcess() As String
        Get
            Return _rdoProcess
        End Get
        Set(ByVal value As String)
            _rdoProcess = value
        End Set
    End Property

    ''' <summary>
    ''' 工事名称(仮)
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtPjNameTemp As String = String.Empty
    Public Property txtPjNameTemp() As String
        Get
            Return _txtPjNameTemp
        End Get
        Set(ByVal value As String)
            _txtPjNameTemp = value
        End Set
    End Property

    ''' <summary>
    ''' 顧客
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtCustomerName As String = String.Empty
    Public Property txtCustomerName() As String
        Get
            Return _txtCustomerName
        End Get
        Set(ByVal value As String)
            _txtCustomerName = value
        End Set
    End Property

    ''' <summary>
    ''' オーダ
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtOrderCd As String = String.Empty
    Public Property txtOrderCd() As String
        Get
            Return _txtOrderCd
        End Get
        Set(ByVal value As String)
            _txtOrderCd = value
        End Set
    End Property

    ''' <summary>
    ''' 関連オーダ
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtRelateOrderCd As String = String.Empty
    Public Property txtRelateOrderCd() As String
        Get
            Return _txtRelateOrderCd
        End Get
        Set(ByVal value As String)
            _txtRelateOrderCd = value
        End Set
    End Property

    ''' <summary>
    ''' 工事名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtOrderNm As String = String.Empty
    Public Property txtOrderNm() As String
        Get
            Return _txtOrderNm
        End Get
        Set(ByVal value As String)
            _txtOrderNm = value
        End Set
    End Property

    ''' <summary>
    ''' 顧客
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtCompyNm As String = String.Empty
    Public Property txtCompyNm() As String
        Get
            Return _txtCompyNm
        End Get
        Set(ByVal value As String)
            _txtCompyNm = value
        End Set
    End Property

    ''' <summary>
    ''' 顧客区分
    ''' </summary>
    ''' <remarks></remarks>
    Private _rdoCustomerType As String = String.Empty
    Public Property rdoCustomerType() As String
        Get
            Return _rdoCustomerType
        End Get
        Set(ByVal value As String)
            _rdoCustomerType = value
        End Set
    End Property

    ''' <summary>
    ''' 受注金額
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtJyuchuCrr As String = String.Empty
    Public Property txtJyuchuCrr() As String
        Get
            Return _txtJyuchuCrr
        End Get
        Set(ByVal value As String)
            _txtJyuchuCrr = value
        End Set
    End Property

    ''' <summary>
    ''' 納期日
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtNokiYmd As String = String.Empty
    Public Property txtNokiYmd() As String
        Get
            Return _txtNokiYmd
        End Get
        Set(ByVal value As String)
            _txtNokiYmd = value
        End Set
    End Property

    ''' <summary>
    ''' 受注部門
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtJyuchuSectNm As String = String.Empty
    Public Property txtJyuchuSectNm() As String
        Get
            Return _txtJyuchuSectNm
        End Get
        Set(ByVal value As String)
            _txtJyuchuSectNm = value
        End Set
    End Property

    ''' <summary>
    ''' 受注担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtJyuchuUserNm As String = String.Empty
    Public Property txtJyuchuUserNm() As String
        Get
            Return _txtJyuchuUserNm
        End Get
        Set(ByVal value As String)
            _txtJyuchuUserNm = value
        End Set
    End Property

    ''' <summary>
    ''' 製造部門ID
    ''' </summary>
    ''' <remarks></remarks>
    Private _hdnProductSectId As String = String.Empty
    Public Property hdnProductSectId() As String
        Get
            Return _hdnProductSectId
        End Get
        Set(ByVal value As String)
            _hdnProductSectId = value
        End Set
    End Property

    ''' <summary>
    ''' 製造部門CD
    ''' </summary>
    ''' <remarks></remarks>
    Private _hdnProductSectCd As String = String.Empty
    Public Property hdnProductSectCd() As String
        Get
            Return _hdnProductSectCd
        End Get
        Set(ByVal value As String)
            _hdnProductSectCd = value
        End Set
    End Property

    ''' <summary>
    ''' 製造部門
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtProductSectNm As String = String.Empty
    Public Property txtProductSectNm() As String
        Get
            Return _txtProductSectNm
        End Get
        Set(ByVal value As String)
            _txtProductSectNm = value
        End Set
    End Property

    ''' <summary>
    ''' 製造部門担当者ID
    ''' </summary>
    ''' <remarks></remarks>
    Private _hdnProductUserId As String = String.Empty
    Public Property hdnProductUserId() As String
        Get
            Return _hdnProductUserId
        End Get
        Set(ByVal value As String)
            _hdnProductUserId = value
        End Set
    End Property

    ''' <summary>
    ''' 製造部門担当者CD
    ''' </summary>
    ''' <remarks></remarks>
    Private _hdnProductUserCd As String = String.Empty
    Public Property hdnProductUserCd() As String
        Get
            Return _hdnProductUserCd
        End Get
        Set(ByVal value As String)
            _hdnProductUserCd = value
        End Set
    End Property

    ''' <summary>
    ''' 製造部門担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtProductUser As String = String.Empty
    Public Property txtProductUser() As String
        Get
            Return _txtProductUser
        End Get
        Set(ByVal value As String)
            _txtProductUser = value
        End Set
    End Property

    ''' <summary>
    ''' 支社間取引の有無
    ''' </summary>
    ''' <remarks></remarks>
    Private _rdoBranchTransactionFlg As String = String.Empty
    Public Property rdoBranchTransactionFlg() As String
        Get
            Return _rdoBranchTransactionFlg
        End Get
        Set(ByVal value As String)
            _rdoBranchTransactionFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 支援支社ID
    ''' </summary>
    ''' <remarks></remarks>
    Private _ddlBranchListId As String = String.Empty
    Public Property ddlBranchListId() As String
        Get
            Return _ddlBranchListId
        End Get
        Set(ByVal value As String)
            _ddlBranchListId = value
        End Set
    End Property

    ''' <summary>
    ''' 支援支社
    ''' </summary>
    ''' <remarks></remarks>
    Private _ddlBranchListNm As String = String.Empty
    Public Property ddlBranchListNm() As String
        Get
            Return _ddlBranchListNm
        End Get
        Set(ByVal value As String)
            _ddlBranchListNm = value
        End Set
    End Property

    ''' <summary>
    ''' 支社品質管理責任者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtBranchQualityManager As String = String.Empty
    Public Property txtBranchQualityManager() As String
        Get
            Return _txtBranchQualityManager
        End Get
        Set(ByVal value As String)
            _txtBranchQualityManager = value
        End Set
    End Property

    ''' <summary>
    ''' 部品質管理責任者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtSectionQualityManager As String = String.Empty
    Public Property txtSectionQualityManager() As String
        Get
            Return _txtSectionQualityManager
        End Get
        Set(ByVal value As String)
            _txtSectionQualityManager = value
        End Set
    End Property

    ''' <summary>
    ''' グループ品質管理責任者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtGroupQualityManager As String = String.Empty
    Public Property txtGroupQualityManager() As String
        Get
            Return _txtGroupQualityManager
        End Get
        Set(ByVal value As String)
            _txtGroupQualityManager = value
        End Set
    End Property

    ''' <summary>
    ''' プロジェクト品質管理責任者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtProjectQualityManager As String = String.Empty
    Public Property txtProjectQualityManager() As String
        Get
            Return _txtProjectQualityManager
        End Get
        Set(ByVal value As String)
            _txtProjectQualityManager = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理責任者
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtRiskPreventionManager As String = String.Empty
    Public Property txtRiskPreventionManager() As String
        Get
            Return _txtRiskPreventionManager
        End Get
        Set(ByVal value As String)
            _txtRiskPreventionManager = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象ー500万以上
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkRpm500MilFlg As String
    Public Property chkRpm500MilFlg() As String
        Get
            Return _chkRpm500MilFlg
        End Get
        Set(ByVal value As String)
            _chkRpm500MilFlg = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象ー初品フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkRpmFirstProductFlg As String
    Public Property chkRpmFirstProductFlg() As String
        Get
            Return _chkRpmFirstProductFlg
        End Get
        Set(ByVal value As String)
            _chkRpmFirstProductFlg = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象ー初品
    ''' </summary>
    ''' <remarks></remarks>
    Private _ddlFirstProduct As String = String.Empty
    Public Property ddlFirstProduct() As String
        Get
            Return _ddlFirstProduct
        End Get
        Set(ByVal value As String)
            _ddlFirstProduct = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象ー初品理由
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtRpmFirstProductCause As String = String.Empty
    Public Property txtRpmFirstProductCause() As String
        Get
            Return _txtRpmFirstProductCause
        End Get
        Set(ByVal value As String)
            _txtRpmFirstProductCause = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象―特殊フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkRpmSpecialProductFlg As String
    Public Property chkRpmSpecialProductFlg() As String
        Get
            Return _chkRpmSpecialProductFlg
        End Get
        Set(ByVal value As String)
            _chkRpmSpecialProductFlg = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象ー特殊
    ''' </summary>
    ''' <remarks></remarks>
    Private _ddlSpecialProduct As String = String.Empty
    Public Property ddlSpecialProduct() As String
        Get
            Return _ddlSpecialProduct
        End Get
        Set(ByVal value As String)
            _ddlSpecialProduct = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理対象ー特殊理由
    ''' </summary>
    ''' <remarks></remarks>
    Private _txtRpmSpecialProductCause As String = String.Empty
    Public Property txtRpmSpecialProductCause() As String
        Get
            Return _txtRpmSpecialProductCause
        End Get
        Set(ByVal value As String)
            _txtRpmSpecialProductCause = value
        End Set
    End Property

    ''' <summary>
    ''' リスク予防管理区分
    ''' </summary>
    ''' <remarks></remarks>
    Private _rdoRpmType As String = String.Empty
    Public Property rdoRpmType() As String
        Get
            Return _rdoRpmType
        End Get
        Set(ByVal value As String)
            _rdoRpmType = value
        End Set
    End Property

    ''' <summary>
    ''' 添付ファイルリック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkProjectAttatch As ArrayList = New ArrayList()
    Public Property lnkProjectAttatch() As ArrayList
        Get
            Return _lnkProjectAttatch
        End Get
        Set(ByVal value As ArrayList)
            _lnkProjectAttatch = value
        End Set
    End Property

    ''' <summary>
    ''' 添付ファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleProjectAttatch As ArrayList = New ArrayList()
    Public Property fleProjectAttatch() As ArrayList
        Get
            Return _fleProjectAttatch
        End Get
        Set(ByVal value As ArrayList)
            _fleProjectAttatch = value
        End Set
    End Property

    ''' <summary>
    ''' 添付ファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleProjectAttatchFile As ArrayList = New ArrayList()
    Public Property fleProjectAttatchFile() As ArrayList
        Get
            Return _fleProjectAttatchFile
        End Get
        Set(ByVal value As ArrayList)
            _fleProjectAttatchFile = value
        End Set
    End Property

    ''' <summary>
    ''' 添付ファイル更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleProjectAttatchIsUpdate As ArrayList = New ArrayList()
    Public Property fleProjectAttatchIsUpdate() As ArrayList
        Get
            Return _fleProjectAttatchIsUpdate
        End Get
        Set(ByVal value As ArrayList)
            _fleProjectAttatchIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 添付ファイル削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleProjectAttatchIsDetele As ArrayList = New ArrayList()
    Public Property fleProjectAttatchIsDetele() As ArrayList
        Get
            Return _fleProjectAttatchIsDetele
        End Get
        Set(ByVal value As ArrayList)
            _fleProjectAttatchIsDetele = value
        End Set
    End Property

    ''' <summary>
    ''' 添付ファイルSEQ
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleProjectAttatchSeqNo As ArrayList = New ArrayList()
    Public Property fleProjectAttatchSeqNo() As ArrayList
        Get
            Return _fleProjectAttatchSeqNo
        End Get
        Set(ByVal value As ArrayList)
            _fleProjectAttatchSeqNo = value
        End Set
    End Property

    ''' <summary>
    ''' 案件タイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private _rdoProjectType As String = String.Empty
    Public Property rdoProjectType() As String
        Get
            Return _rdoProjectType
        End Get
        Set(ByVal value As String)
            _rdoProjectType = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)リスク管理表リック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkSp1RiskManagementList As String = String.Empty
    Public Property lnkSp1RiskManagementList() As String
        Get
            Return _lnkSp1RiskManagementList
        End Get
        Set(ByVal value As String)
            _lnkSp1RiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)リスト管理表
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp1RiskManagementList As String = String.Empty
    Public Property fleSp1RiskManagementList() As String
        Get
            Return _fleSp1RiskManagementList
        End Get
        Set(ByVal value As String)
            _fleSp1RiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)リスク管理表ファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp1RiskManagementFileList As Byte()
    Public Property fleSp1RiskManagementFileList() As Byte()
        Get
            Return _fleSp1RiskManagementFileList
        End Get
        Set(ByVal value As Byte())
            _fleSp1RiskManagementFileList = value
        End Set
    End Property

    ''' <summary>
    '''  購買プロセス(原価)リスク管理表更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleSp1RiskManagementListIsUpdate As String = String.Empty
    Public Property fleSp1RiskManagementListIsUpdate() As String
        Get
            Return _fleSp1RiskManagementListIsUpdate
        End Get
        Set(ByVal value As String)
            _fleSp1RiskManagementListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスリスク管理表削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleSp1RiskManagementListIsDelete As String = String.Empty
    Public Property fleSp1RiskManagementListIsDelete() As String
        Get
            Return _fleSp1RiskManagementListIsDelete
        End Get
        Set(ByVal value As String)
            _fleSp1RiskManagementListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)チェックリストリック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkSp1RiskCheckList As String = String.Empty
    Public Property lnkSp1RiskCheckList() As String
        Get
            Return _lnkSp1RiskCheckList
        End Get
        Set(ByVal value As String)
            _lnkSp1RiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)チェックリスト
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp1RiskCheckList As String = String.Empty
    Public Property fleSp1RiskCheckList() As String
        Get
            Return _fleSp1RiskCheckList
        End Get
        Set(ByVal value As String)
            _fleSp1RiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)チェックリストファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp1RiskCheckFileList As Byte()
    Public Property fleSp1RiskCheckFileList() As Byte()
        Get
            Return _fleSp1RiskCheckFileList
        End Get
        Set(ByVal value As Byte())
            _fleSp1RiskCheckFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)チェックリスト更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleSp1RiskCheckListIsUpdate As String = String.Empty
    Public Property fleSp1RiskCheckListIsUpdate() As String
        Get
            Return _fleSp1RiskCheckListIsUpdate
        End Get
        Set(ByVal value As String)
            _fleSp1RiskCheckListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)チェックリスト削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleSp1RiskCheckListIsDelete As String = String.Empty
    Public Property fleSp1RiskCheckListIsDelete() As String
        Get
            Return _fleSp1RiskCheckListIsDelete
        End Get
        Set(ByVal value As String)
            _fleSp1RiskCheckListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)不要
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkSp1NoNeedFlg As Boolean
    Public Property chkSp1NoNeedFlg() As Boolean
        Get
            Return _chkSp1NoNeedFlg
        End Get
        Set(ByVal value As Boolean)
            _chkSp1NoNeedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(原価)完了
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkSp1CompleteFlg As Boolean
    Public Property chkSp1CompleteFlg() As Boolean
        Get
            Return _chkSp1CompleteFlg
        End Get
        Set(ByVal value As Boolean)
            _chkSp1CompleteFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)リスク管理表リック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkSp2RiskManagementList As String = String.Empty
    Public Property lnkSp2RiskManagementList() As String
        Get
            Return _lnkSp2RiskManagementList
        End Get
        Set(ByVal value As String)
            _lnkSp2RiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)リスク管理表
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp2RiskManagementList As String = String.Empty
    Public Property fleSp2RiskManagementList() As String
        Get
            Return _fleSp2RiskManagementList
        End Get
        Set(ByVal value As String)
            _fleSp2RiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)リスク管理表ファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp2RiskManagementFileList As Byte()
    Public Property fleSp2RiskManagementFileList() As Byte()
        Get
            Return _fleSp2RiskManagementFileList
        End Get
        Set(ByVal value As Byte())
            _fleSp2RiskManagementFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)リスク管理表更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleSp2RiskManagementListIsUpdate As String = String.Empty
    Public Property fleSp2RiskManagementListIsUpdate() As String
        Get
            Return _fleSp2RiskManagementListIsUpdate
        End Get
        Set(ByVal value As String)
            _fleSp2RiskManagementListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)リスク管理表削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleSp2RiskManagementListIsDelete As String = String.Empty
    Public Property fleSp2RiskManagementListIsDelete() As String
        Get
            Return _fleSp2RiskManagementListIsDelete
        End Get
        Set(ByVal value As String)
            _fleSp2RiskManagementListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)チェックリストリック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkSp2RiskCheckList As String = String.Empty
    Public Property lnkSp2RiskCheckList() As String
        Get
            Return _lnkSp2RiskCheckList
        End Get
        Set(ByVal value As String)
            _lnkSp2RiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)チェックリスト
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp2RiskCheckList As String = String.Empty
    Public Property fleSp2RiskCheckList() As String
        Get
            Return _fleSp2RiskCheckList
        End Get
        Set(ByVal value As String)
            _fleSp2RiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)チェックリストファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleSp2RiskCheckFileList As Byte()
    Public Property fleSp2RiskCheckFileList() As Byte()
        Get
            Return _fleSp2RiskCheckFileList
        End Get
        Set(ByVal value As Byte())
            _fleSp2RiskCheckFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)チェックリスト更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleSp2RiskCheckListIsUpdate As String = String.Empty
    Public Property fleSp2RiskCheckListIsUpdate() As String
        Get
            Return _fleSp2RiskCheckListIsUpdate
        End Get
        Set(ByVal value As String)
            _fleSp2RiskCheckListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)チェックリスト削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleSp2RiskCheckListIsDelete As String = String.Empty
    Public Property fleSp2RiskCheckListIsDelete() As String
        Get
            Return _fleSp2RiskCheckListIsDelete
        End Get
        Set(ByVal value As String)
            _fleSp2RiskCheckListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)不要
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkSp2NoNeedFlg As Boolean
    Public Property chkSp2NoNeedFlg() As Boolean
        Get
            Return _chkSp2NoNeedFlg
        End Get
        Set(ByVal value As Boolean)
            _chkSp2NoNeedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス(見積)完了
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkSp2CompleteFlg As Boolean
    Public Property chkSp2CompleteFlg() As Boolean
        Get
            Return _chkSp2CompleteFlg
        End Get
        Set(ByVal value As Boolean)
            _chkSp2CompleteFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスリスク管理表リック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkPpRiskManagementList As String = String.Empty
    Public Property lnkPpRiskManagementList() As String
        Get
            Return _lnkPpRiskManagementList
        End Get
        Set(ByVal value As String)
            _lnkPpRiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスリスク管理表
    ''' </summary>
    ''' <remarks></remarks>
    Private _flePpRiskManagementList As String = String.Empty
    Public Property flePpRiskManagementList() As String
        Get
            Return _flePpRiskManagementList
        End Get
        Set(ByVal value As String)
            _flePpRiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスリスク管理表ファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _flePpRiskManagementFileList As Byte()
    Public Property flePpRiskManagementFileList() As Byte()
        Get
            Return _flePpRiskManagementFileList
        End Get
        Set(ByVal value As Byte())
            _flePpRiskManagementFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスリスク管理表更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _flePpRiskManagementListIsUpdate As String = String.Empty
    Public Property flePpRiskManagementListIsUpdate() As String
        Get
            Return _flePpRiskManagementListIsUpdate
        End Get
        Set(ByVal value As String)
            _flePpRiskManagementListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスリスク管理表削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _flePpRiskManagementListIsDelete As String = String.Empty
    Public Property flePpRiskManagementListIsDelete() As String
        Get
            Return _flePpRiskManagementListIsDelete
        End Get
        Set(ByVal value As String)
            _flePpRiskManagementListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスチェックリストリック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkPpRiskCheckList As String = String.Empty
    Public Property lnkPpRiskCheckList() As String
        Get
            Return _lnkPpRiskCheckList
        End Get
        Set(ByVal value As String)
            _lnkPpRiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスチェックリスト
    ''' </summary>
    ''' <remarks></remarks>
    Private _flePpRiskCheckList As String = String.Empty
    Public Property flePpRiskCheckList() As String
        Get
            Return _flePpRiskCheckList
        End Get
        Set(ByVal value As String)
            _flePpRiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスチェックリストファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _flePpRiskCheckFileList As Byte()
    Public Property flePpRiskCheckFileList() As Byte()
        Get
            Return _flePpRiskCheckFileList
        End Get
        Set(ByVal value As Byte())
            _flePpRiskCheckFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスチェックリスト更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _flePpRiskCheckListIsUpdate As String = String.Empty
    Public Property flePpRiskCheckListIsUpdate() As String
        Get
            Return _flePpRiskCheckListIsUpdate
        End Get
        Set(ByVal value As String)
            _flePpRiskCheckListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセスチェックリスト削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _flePpRiskCheckListIsDelete As String = String.Empty
    Public Property flePpRiskCheckListIsDelete() As String
        Get
            Return _flePpRiskCheckListIsDelete
        End Get
        Set(ByVal value As String)
            _flePpRiskCheckListIsDelete = value
        End Set
    End Property


    ''' <summary>
    ''' 購買プロセス不要
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkPpNoNeedFlg As Boolean
    Public Property chkPpNoNeedFlg() As Boolean
        Get
            Return _chkPpNoNeedFlg
        End Get
        Set(ByVal value As Boolean)
            _chkPpNoNeedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 購買プロセス完了
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkPpCompleteFlg As Boolean
    Public Property chkPpCompleteFlg() As Boolean
        Get
            Return _chkPpCompleteFlg
        End Get
        Set(ByVal value As Boolean)
            _chkPpCompleteFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスリスク管理リック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkDpRiskManagementList As String = String.Empty
    Public Property lnkDpRiskManagementList() As String
        Get
            Return _lnkDpRiskManagementList
        End Get
        Set(ByVal value As String)
            _lnkDpRiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスリスク管理
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleDpRiskManagementList As String = String.Empty
    Public Property fleDpRiskManagementList() As String
        Get
            Return _fleDpRiskManagementList
        End Get
        Set(ByVal value As String)
            _fleDpRiskManagementList = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスリスク管理ファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleDpRiskManagemenFileList As Byte()
    Public Property fleDpRiskManagemenFileList() As Byte()
        Get
            Return _fleDpRiskManagemenFileList
        End Get
        Set(ByVal value As Byte())
            _fleDpRiskManagemenFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスリスク管理更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleDpRiskManagementListIsUpdate As String = String.Empty
    Public Property fleDpRiskManagementListIsUpdate() As String
        Get
            Return _fleDpRiskManagementListIsUpdate
        End Get
        Set(ByVal value As String)
            _fleDpRiskManagementListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスリスク管理削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleDpRiskManagementListIsDelete As String = String.Empty
    Public Property fleDpRiskManagementListIsDelete() As String
        Get
            Return _fleDpRiskManagementListIsDelete
        End Get
        Set(ByVal value As String)
            _fleDpRiskManagementListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスチェックリストリック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkDpRiskCheckList As String = String.Empty
    Public Property lnkDpRiskCheckList() As String
        Get
            Return _lnkDpRiskCheckList
        End Get
        Set(ByVal value As String)
            _lnkDpRiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスチェックリスト
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleDpRiskCheckList As String = String.Empty
    Public Property fleDpRiskCheckList() As String
        Get
            Return _fleDpRiskCheckList
        End Get
        Set(ByVal value As String)
            _fleDpRiskCheckList = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスチェックリストファイル
    ''' </summary>
    ''' <remarks></remarks>
    Private _fleDpRiskCheckFileList As Byte()
    Public Property fleDpRiskCheckFileList() As Byte()
        Get
            Return _fleDpRiskCheckFileList
        End Get
        Set(ByVal value As Byte())
            _fleDpRiskCheckFileList = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスチェックリスト更新フラグ
    ''' </summary>
    ''' <remarks>0:新規登録 1:更新</remarks>
    Private _fleDpRiskCheckListIsUpdate As String = String.Empty
    Public Property fleDpRiskCheckListIsUpdate() As String
        Get
            Return _fleDpRiskCheckListIsUpdate
        End Get
        Set(ByVal value As String)
            _fleDpRiskCheckListIsUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセスチェックリスト削除フラグ
    ''' </summary>
    ''' <remarks>0:更新Or新規 1:削除</remarks>
    Private _fleDpRiskCheckListIsDelete As String = String.Empty
    Public Property fleDpRiskCheckListIsDelete() As String
        Get
            Return _fleDpRiskCheckListIsDelete
        End Get
        Set(ByVal value As String)
            _fleDpRiskCheckListIsDelete = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセス不要
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkDpNoNeedFlg As Boolean
    Public Property chkDpNoNeedFlg() As Boolean
        Get
            Return _chkDpNoNeedFlg
        End Get
        Set(ByVal value As Boolean)
            _chkDpNoNeedFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 設計開発プロセス完了
    ''' </summary>
    ''' <remarks></remarks>
    Private _chkDpCompleteFlg As Boolean
    Public Property chkDpCompleteFlg() As Boolean
        Get
            Return _chkDpCompleteFlg
        End Get
        Set(ByVal value As Boolean)
            _chkDpCompleteFlg = value
        End Set
    End Property

    ''' <summary>
    ''' 回数
    ''' </summary>
    ''' <remarks></remarks>
    Private _lblOpenRound As ArrayList = New ArrayList()
    Public Property lblOpenRound() As ArrayList
        Get
            Return _lblOpenRound
        End Get
        Set(ByVal value As ArrayList)
            _lblOpenRound = value
        End Set
    End Property

    ''' <summary>
    ''' 開催日
    ''' </summary>
    ''' <remarks></remarks>
    Private _lblOpenDate As ArrayList
    Public Property lblOpenDate() As ArrayList
        Get
            Return _lblOpenDate
        End Get
        Set(ByVal value As ArrayList)
            _lblOpenDate = value
        End Set
    End Property

    ''' <summary>
    ''' 議事録リック
    ''' </summary>
    ''' <remarks></remarks>
    Private _lnkRiskPrevention As ArrayList
    Public Property lnkRiskPrevention() As ArrayList
        Get
            Return _lnkRiskPrevention
        End Get
        Set(ByVal value As ArrayList)
            _lnkRiskPrevention = value
        End Set
    End Property

    ''' <summary>
    ''' 支社ラベル
    ''' </summary>
    ''' <remarks></remarks>
    Private _lblSupportBranch As String = String.Empty
    Public Property lblSupportBranch() As String
        Get
            Return _lblSupportBranch
        End Get
        Set(ByVal value As String)
            _lblSupportBranch = value
        End Set
    End Property

    ''' <summary>
    ''' 更新日
    ''' </summary>
    ''' <remarks></remarks>
    Private _hdnModifiedOn As String = String.Empty
    Public Property hdnModifiedOn() As String
        Get
            Return _hdnModifiedOn
        End Get
        Set(ByVal value As String)
            _hdnModifiedOn = value
        End Set
    End Property

    ''' <summary>
    ''' 登録担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private _createdUserId As String = String.Empty
    Public Property createdUserId() As String
        Get
            Return _createdUserId
        End Get
        Set(ByVal value As String)
            _createdUserId = value
        End Set
    End Property

    ''' <summary>
    ''' 部門コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _a01m01SectCd As String = String.Empty
    Public Property a01m01SectCd() As String
        Get
            Return _a01m01SectCd
        End Get
        Set(ByVal value As String)
            _a01m01SectCd = value
        End Set
    End Property

End Class
