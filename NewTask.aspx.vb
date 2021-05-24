Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Partial Class NewTask
    Inherits System.Web.UI.Page
    Dim IsoStart As String
    Dim IsoEnd As String
    Dim Obj As New DAL
    Dim constr1 As String = ConfigurationManager.ConnectionStrings("constr1").ConnectionString
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            IsoStart = " Alter Database EtradeSelect Set Allow_SnapShot_isolation on;"
            IsoStart &= " Set Transaction Isolation level read uncommitted;Set nocount on;"
            IsoStart &= " Begin Tran  "

            IsoEnd = " Commit Tran "
            If Session("Status") = "OK" Then

                'If (Session("ActiveStatus") = "N") Then
                '    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Key", "alert('If person has not done activation, then You have not subscribed to our course, Please Enroll Now!');location.replace('Index.aspx');", True)
                '    Exit Sub
                'End If

                If Not Page.IsPostBack Then
                    If (Request.QueryString("id") <> Nothing) Then
                        LoadAssignment(Request.QueryString("id"))
                    Else
                        Response.Redirect("Assignment.aspx", False)
                    End If

                End If
            Else
                Response.Redirect("Default.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadAssignment(ByVal ID As String)
        Try
            Dim dt As DataTable = New DataTable()
            Dim str As String = IsoStart & " Exec Sp_GetAssignment 'TaskStatus'," & Session("FormNo") & "," & Crypto.Decrypt(ID) & " " & IsoEnd
            dt = SqlHelper.ExecuteDataset(constr1, CommandType.Text, str).Tables(0)
            If (dt.Rows.Count > 0) Then
                RptDirects.DataSource = dt
                RptDirects.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub RptDirects_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles RptDirects.RowCommand
        Try
            Dim scrname As String = ""
            Dim FlNm As String = ""
            Dim UImage As String = ""
            Dim IsImage As String = "N"
            Dim Code As String = "N"
            Dim id As String = e.CommandArgument.ToString()
            Dim row As GridViewRow = CType(((CType(e.CommandSource, Control)).NamingContainer), GridViewRow)
            Dim fp As FileUpload = TryCast(row.FindControl("Fup1"), FileUpload)
            Dim txtCode As TextBox = TryCast(row.FindControl("txtCode"), TextBox)

            Dim txtAccount As TextBox = TryCast(row.FindControl("txtAccount"), TextBox)
            Dim txtMobile As TextBox = TryCast(row.FindControl("txtMobile"), TextBox)
            Dim txtEmail As TextBox = TryCast(row.FindControl("txtEmail"), TextBox)
            Dim txtCCode As TextBox = TryCast(row.FindControl("txtCCode"), TextBox)
            Dim txtScore As TextBox = TryCast(row.FindControl("TxtScore"), TextBox)

            Dim txtOther As TextBox = TryCast(row.FindControl("txtOther"), TextBox)
            Dim txtOther2 As TextBox = TryCast(row.FindControl("txtOther2"), TextBox)
            'Dim txtlink As TextBox = TryCast(row.FindControl("txtlink"), TextBox)
            'Dim txtreason As TextBox = TryCast(row.FindControl("Txtreason"), TextBox)

            Dim HdnTypeid As HiddenField = TryCast(row.FindControl("HdnTypeId"), HiddenField)
            Dim HdnTypename As HiddenField = TryCast(row.FindControl("HdnTypeName"), HiddenField)

            If e.CommandName = "UploadCOLFile" Then

                If txtAccount.Visible = True Then
                    If (txtAccount.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Account No. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    Else
                    End If

                End If

                If txtMobile.Visible = True Then

                    If (txtMobile.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Mobile No. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    Else
                        If (Val(txtMobile.Text.Trim().Length) < 10) Then
                            scrname = "<SCRIPT language='javascript'>alert('Invaild Mobile No.!! ');" & "</SCRIPT>"
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                            Exit Sub
                        End If
                    End If
                End If
                If txtEmail.Visible = True Then


                    If (txtEmail.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Email ID. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    End If
                End If
                If txtCCode.Visible = True Then


                    If (txtCCode.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Enter Enter Client Code.!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    Else
                    End If

                End If
                If txtCode.Visible = True Then

                    If (txtCode.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Code/Enter Mobile No. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    End If
                End If


                If txtScore.Visible = True Then

                    If (txtScore.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Score. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    End If
                End If


                If txtOther.Visible = True Then

                    If (txtOther.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Other. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    End If
                End If

                'If txtlink.Visible = True Then

                '    If (txtlink.Text.Trim() = "") Then
                '        scrname = "<SCRIPT language='javascript'>alert('Please Enter Link. .!! ');" & "</SCRIPT>"
                '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                '        Exit Sub
                '    End If
                'End If

                If txtOther2.Visible = True Then

                    If (txtOther2.Text.Trim() = "") Then
                        scrname = "<SCRIPT language='javascript'>alert('Please Enter Other2. .!! ');" & "</SCRIPT>"
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                        Exit Sub
                    End If
                End If

                'If txtreason.Visible = True Then

                '    If (txtreason.Text.Trim() = "") Then
                '        scrname = "<SCRIPT language='javascript'>alert('Please Enter Reason for not completed task. .!! ');" & "</SCRIPT>"
                '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Close", scrname, False)
                '        Exit Sub
                '    End If
                'End If

                If fp.HasFile Then
                    Dim strextension As String = ""
                    strextension = System.IO.Path.GetExtension(fp.FileName)
                    If (strextension.ToUpper() = ".JPG") Or (strextension.ToUpper() = ".JPEG") Or (strextension.ToUpper() = ".PNG") Then
                        If (Not System.IO.Directory.Exists("C:\inetpub\etradewelath_learn\images\Task\" & Format(Now, "yyyyMMdd"))) Then
                            System.IO.Directory.CreateDirectory("C:\inetpub\etradewelath_learn\images\Task\" & Format(Now, "yyyyMMdd"))
                        End If

                        FlNm = Format(Now, "yyMMddhhmmssfff") & Path.GetExtension(fp.PostedFile.FileName)
                        Dim FileName As String = Server.MapPath("images/Task/" & Format(Now, "yyyyMMdd") & "/" & FlNm)
                        ' Resize Image Before Uploading to DataBase
                        Dim imageToBeResized As System.Drawing.Image = System.Drawing.Image.FromStream(fp.PostedFile.InputStream)
                        Dim imageHeight As Integer = imageToBeResized.Height
                        Dim imageWidth As Integer = imageToBeResized.Width
                        Dim maxHeight As Integer = 1800
                        Dim maxWidth As Integer = 600
                        imageHeight = (imageHeight * maxWidth) / imageWidth
                        imageWidth = maxWidth
                        If imageHeight > maxHeight Then
                            imageWidth = (imageWidth * maxHeight) / imageHeight
                            imageHeight = maxHeight
                        End If
                        Dim bitmap As New Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight)
                        Dim stream As System.IO.MemoryStream = New MemoryStream()
                        If strextension.ToUpper() = ".JPG" Or strextension.ToUpper() = ".JPEG" Then bitmap.Save(FileName, System.Drawing.Imaging.ImageFormat.Jpeg)
                        If strextension.ToUpper() = ".PNG" Then bitmap.Save(FileName, System.Drawing.Imaging.ImageFormat.Png)
                        If strextension.ToUpper() = ".JPEG" Then bitmap.Save(FileName, System.Drawing.Imaging.ImageFormat.Jpeg)

                        FileName = "http://" & HttpContext.Current.Request.Url.Host & "/images/Task/" & Format(Now, "yyyyMMdd") & "/" & FlNm
                        UImage = FileName
                        IsImage = "Y"
                    End If

                Else
                    UImage = ""
                    IsImage = "N"
                End If


                If (txtCode.Text <> "") Then
                    Code = txtCode.Text
                End If

                Dim dt1 As DataTable = New DataTable()
                Dim str As String = " Exec Sp_InsertTrntask_New1 '" & Convert.ToInt32(e.CommandArgument) & "','" & Session("FormNo") & "', "
                str &= "1,'" & HttpContext.Current.Request.UserHostAddress & "',"
                str &= "'" & Code & "','" & IsImage & "','" & UImage & "','" & Session("RankId") & "',"
                str &= "'" & txtAccount.Text.Trim() & "','" & txtMobile.Text.Trim() & "','" & txtEmail.Text.Trim() & "','" & txtCCode.Text.Trim() & "','" & txtScore.Text.Trim & "','" & txtOther.Text.Trim & "','" & txtOther2.Text.Trim & "'"
                dt1 = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("constr").ConnectionString, CommandType.Text, str).Tables(0)
                scrname = "<SCRIPT language='javascript'>alert('" & dt1.Rows(0)("Result") & "');" & "</SCRIPT>"
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Login Error", scrname, False)

                LoadAssignment(Request.QueryString("id"))

            End If

        Catch ex As Exception

        End Try
    End Sub




    Protected Sub RptDirects_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RptDirects.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim HdnTypeid As HiddenField = TryCast(e.Row.FindControl("HdnTypeId"), HiddenField)
            Dim HdnTypename As HiddenField = TryCast(e.Row.FindControl("HdnTypeName"), HiddenField)
            Dim str1() As String = HdnTypeid.Value.Split(",")
            For i = 0 To str1.Length - 1
                If str1(i) = 1 Then
                    TryCast(e.Row.FindControl("txtAccount"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("lblAccount"), Label).Visible = True
                End If
                If str1(i) = 2 Then
                    TryCast(e.Row.FindControl("txtCCode"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("lblccode"), Label).Visible = True

                End If
                If str1(i) = 3 Then
                    TryCast(e.Row.FindControl("txtMobile"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("lblMobile"), Label).Visible = True

                End If

                If str1(i) = 4 Then
                    TryCast(e.Row.FindControl("txtEmail"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("LblEmail"), Label).Visible = True

                End If

                If str1(i) = 5 Then
                    TryCast(e.Row.FindControl("TxtScore"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("LblScore"), Label).Visible = True

                End If

                If str1(i) = 6 Then
                    TryCast(e.Row.FindControl("txtOther"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("Lblother"), Label).Visible = True

                End If

                If str1(i) = 7 Then
                    TryCast(e.Row.FindControl("txtOther2"), TextBox).Visible = True
                    TryCast(e.Row.FindControl("lblother2"), Label).Visible = True

                End If


            Next

        End If
    End Sub
End Class
