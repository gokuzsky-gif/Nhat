Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Xpo

Namespace BusinessObjects
    <MapInheritance(MapInheritanceType.ParentTable)>
    <DefaultProperty(NameOf(PermissionPolicyUser.UserName))>
    Public Class ApplicationUser
        Inherits PermissionPolicyUser
        Implements ISecurityUserWithLoginInfo

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Browsable(False)>
        <NonCloneable>
        <Aggregated, Association("User-LoginInfo")>
        Public ReadOnly Property LoginInfo As XPCollection(Of ApplicationUserLoginInfo)
            Get
                Return GetCollection(Of ApplicationUserLoginInfo)(NameOf(LoginInfo))
            End Get
        End Property

        Private ReadOnly Property UserLogins As IEnumerable(Of ISecurityUserLoginInfo) Implements ISecurityUserWithLoginInfo.UserLogins
            Get
                Return LoginInfo.OfType(Of ISecurityUserLoginInfo)()
            End Get
        End Property

        Private Function CreateUserLoginInfo(loginProviderName As String, providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
            Dim result As ApplicationUserLoginInfo = New ApplicationUserLoginInfo(Session)
            result.LoginProviderName = loginProviderName
            result.ProviderUserKey = providerUserKey
            result.User = Me
            Return result
        End Function
    End Class
End Namespace
