Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Partial Class Assignment
    Inherits System.Web.UI.Page
    Dim IsoStart As String
    Dim IsoEnd As String
    Dim constr1 As String = ConfigurationManager.ConnectionStrings("constr1").ConnectionString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            IsoStart = " Alter Database EtradeSelect Set Allow_SnapShot_isolation on;"
            IsoStart &= " Set Transaction Isolation level read uncommitted;Set nocount on;"
            IsoStart &= " Begin Tran  "

            IsoEnd = " Commit Tran "
            If Session("Status") = "OK" Then

                If (Session("ActiveStatus") = "N") Then
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Key", "alert('you have not enrolled,please subscribe for the course.!');location.replace('Index.aspx');", True)
                    Exit Sub
                End If

                If Not Page.IsPostBack Then
                    'completeAssignment()
                    LoadAssignment()

                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadAssignment()
        Try

            Dim dt As DataTable = New DataTable()
            Dim str As String = IsoStart & " Exec Sp_GetAssignment 'Get'," & Session("FormNo") & " " & IsoEnd
            dt = SqlHelper.ExecuteDataset(constr1, CommandType.Text, str).Tables(0)
            If (dt.Rows.Count > 0) Then

                RepAssign.DataSource = dt
                RepAssign.DataBind()
                completeAssignment()
            End If

           
            

        Catch ex As Exception

        End Try
    End Sub
    Private Sub completeAssignment()
        Dim dt As DataTable = New DataTable()

        Dim str1 As String = IsoStart & " Exec Sp_GetAssignment 'Disable'," & Session("FormNo") & " " & IsoEnd
        dt = SqlHelper.ExecuteDataset(constr1, CommandType.Text, str1).Tables(0)
        If (dt.Rows.Count > 0) Then
            Repdesable.DataSource = dt
            Repdesable.DataBind()
            'RepAssign.DataSource = dt
            'RepAssign.DataBind()

        End If
    End Sub
End Class
