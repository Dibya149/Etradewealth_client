Imports System.Web
Imports System.Data
Imports System.Data.SqlClient

Imports System.IO
Imports System.Net
Imports System.Globalization

Partial Class Profile
    Inherits System.Web.UI.Page

    Dim _dblAvailLeg As Double = 0
    Private dbGeneral As New clsGeneral
    Private dbConnect As cls_DataAccess

    Private cmd As New SqlCommand
    Private dRead As SqlDataReader

    Private strQuery, strCaptcha As String
    Dim tmpTable As New Data.DataTable
    Dim QryCls As New AccClass.MyAccClass.NewClass
    Dim minSpnsrNoLen, minScrtchLen As Integer

    Dim Upln, dblSpons, dblTehsil, dblDistrict, dblIdNo As Double
    Dim CurrDt As DateTime
    Dim montharray() As String = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
    Dim LastInsertID As Integer = 0
    Dim scrname As String
    Dim Obj As New DAL
    Dim IsoStart As String
    Dim IsoEnd As String
    Dim constr1 As String = ConfigurationManager.ConnectionStrings("constr1").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
           
            IsoStart = " Alter Database EtradeSelect Set Allow_SnapShot_isolation on;"
            IsoStart &= " Set Transaction Isolation level read uncommitted;Set nocount on;"
            IsoStart &= " Begin Tran  "

            IsoEnd = " Commit Tran "

            If Session("Status") <> "OK" Then
                Response.Redirect("Logout.aspx")
            End If

            If Not Page.IsPostBack Then
                FillState()
                
                FillDetail()
            End If
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try

    End Sub

    Private Sub FillState()
        Try
            Dim dt As DataTable = New DataTable()
            Dim Str As String = IsoStart & "Exec Sp_GetStateNAme" & IsoEnd
            dt = SqlHelper.ExecuteDataset(constr1, CommandType.Text, Str).Tables(0)
            If (dt.Rows.Count > 0) Then
                ddlState.DataSource = dt
                ddlState.DataValueField = "StateCode"
                ddlState.DataTextField = "StateName"
                ddlState.DataBind()
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub FillDetail()
        Try
            Dim idverified As String = ""

            Dim sql As String = "exec sp_MemDtl ' and mMst.Formno=''" & Session("Formno") & "'''  "
            Dim dt As New DataTable
            dt = Obj.GetData(sql)
            
            If (dt.Rows.Count > 0) Then

                If IsDBNull(dt.Rows(0)("RefIDNo")) Then
                    txtReferalId.Text = ""

                Else
                    txtReferalId.Text = dt.Rows(0)("RefIDNo")
                End If

                TxtReferalNm.Text = dt.Rows(0)("RefName")

                If IsDBNull(dt.Rows(0)("UpLnIDNo")) Then
                    TxtUplinerid.Text = ""
                Else
                    TxtUplinerid.Text = dt.Rows(0)("UpLnIDNo")
                End If
                TxtUplinerName.Text = dt.Rows(0)("UpLnName")
                hdnidno.Value = dt.Rows(0)("IDno")
                txtFrstNm.Text = dt.Rows(0)("MemName") & ""

                txtFrstNm.Text = dt.Rows(0)("MemName") & ""
                lblPosition.Text = IIf(dt.Rows(0)("LegNo") = 1, "Left", "Right")
                txtFNm.Text = dt.Rows(0)("MemFname") & ""
                TxtDobDate.Text = Format(dt.Rows(0)("MemDob"), "dd-MMM-yyyy")
                TxtDoj.Text = Format(dt.Rows(0)("Doj"), "dd-MMM-yyyy")
                txtPinCode.Text = dt.Rows(0)("Pincode")
                ddlTehsil.Text = dt.Rows(0)("City")
                ddlState.SelectedValue = dt.Rows(0)("StateCode")

                If dt.Rows(0)("ActiveStatus") = "Y" Then
                    TxtDoa.Text = Format(dt.Rows(0)("UpgradeDate"), "dd-MMM-yyyy")
                Else
                    TxtDoa.Text = ""
                End If
                txtPhNo.Text = dt.Rows(0)("PhN1") & ""
                txtMobileNo.Text = dt.Rows(0)("Mobl") & ""


                txtEMailId.Text = dt.Rows(0)("EMail") & ""



                txtNominee.Text = dt.Rows(0)("NomineeName") & ""
                txtRelation.Text = dt.Rows(0)("Relation") & ""
                'TxtMICR.Text = dRead.Item("MICRCode") & ""

                If dt.Rows(0)("ActiveStatus") = "N" Then
                    txtFrstNm.Enabled = True
                Else
                    txtFrstNm.Enabled = False
                End If




                If txtFNm.Text <> "" Then
                    txtFNm.Enabled = False
                Else
                    txtFNm.Enabled = True
                End If
                If Val(txtMobileNo.Text) = "0" Or dt.Rows(0)("ActiveStatus") = "N" Then
                    txtMobileNo.Enabled = True
                Else
                    txtMobileNo.Enabled = False
                End If
                If txtPhNo.Text.Length >= 10 Then
                    txtPhNo.Enabled = False
                Else
                    txtPhNo.Enabled = True
                End If
                If txtEMailId.Text <> "" Then
                    txtEMailId.Enabled = True
                Else
                    txtEMailId.Enabled = True
                End If



                If txtNominee.Text <> "" Then
                    txtNominee.Enabled = False
                Else
                    txtNominee.Enabled = True
                End If
                If txtRelation.Text <> "" Then
                    txtRelation.Enabled = False
                Else
                    txtRelation.Enabled = True
                End If




            End If
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Sub
    
    Private Function ConvertDateToString(ByVal Month As String) As String
        Try

        
            Select Case Month
                Case 1
                    Return "JAN"
                Case 2
                    Return "FEB"
                Case 3
                    Return "Mar"
                Case 4
                    Return "Apr"
                Case 5
                    Return "May"
                Case 6
                    Return "Jun"
                Case 7
                    Return "Jul"
                Case 8
                    Return "Aug"
                Case 9
                    Return "Sep"
                Case 10
                    Return "Oct"
                Case 11
                    Return "Nov"
                Case 12
                    Return "Dec"
            End Select
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Function
    

    Private Sub FindSession()
        Try

            
            cmd = New SqlCommand("Select Top 1 SessID,ToDate,FrmDate from M_SessnMaster order by SessID desc", dbConnect.cnnObject)
            dRead = cmd.ExecuteReader
            If dRead.Read = True Then
                Session("SessID") = dRead("SessID")
                
            Else
                '  errMsg.InnerText = "Session Not Exist. Please Enter New Session."
                Exit Sub
            End If
            dRead.Close()
            cmd.Cancel()

        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Sub

    Private Sub UpdateDb()
        Try

            If (hdnidno.Value.ToString().ToUpper() <> Session("IDNo").ToString().ToUpper()) Then
                scrname = "<SCRIPT language='javascript'>alert('Profile cannot be changed, Please try later.!!');" & "</SCRIPT>"
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Login Error", scrname, False)
                Exit Sub
            End If




            If txtMobileNo.Text <> "" Then
                Dim Dt1 As DataTable = New DataTable()
                Dim Dsmob As New DataSet
                Dim strSqlMob As String = IsoStart & " select Count(mobl) as mobileno from Etrade..M_Membermaster where Mobl='" & txtMobileNo.Text.Trim & "' And Formno <> '" & Session("Formno") & "' " & IsoEnd
                Dsmob = SqlHelper.ExecuteDataset(constr1, CommandType.Text, strSqlMob)
                Dt1 = Dsmob.Tables(0)

                If Dt1.Rows(0)("mobileno") > 1 Then
                    scrname = "<SCRIPT language='javascript'>alert('Already Registerd by this Mobile Number.');" & "</SCRIPT>"
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Login Error", scrname, False)
                    Exit Sub
                End If

            End If

            If txtEMailId.Text <> "" Then
                Dim Dt1 As DataTable = New DataTable()
                Dim Dsmob As New DataSet
                Dim strSqlMob As String = IsoStart & " select Count(EMail) as EmailID from Etrade..M_Membermaster where EMail='" & txtEMailId.Text.Trim & "' And Formno <> '" & Session("Formno") & "' " & IsoEnd
                Dsmob = SqlHelper.ExecuteDataset(constr1, CommandType.Text, strSqlMob)
                Dt1 = Dsmob.Tables(0)

                If Dt1.Rows(0)("EmailID") > 1 Then
                    scrname = "<SCRIPT language='javascript'>alert('Already Registerd by this Email ID.');" & "</SCRIPT>"
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "Login Error", scrname, False)
                    Exit Sub
                End If

            End If




            
            Dim strQry, strFld, strFldVal As String
            Dim strDOB As DateTime
            Dim Remark As String = ""
            Dim MembName As String = ""
            Dim Password As String = ""
            Dim TransactionPassword As String = ""
            Try
                Dim str As String = ""
                Dim Dt1 As DataTable
                Dt1 = New DataTable
               
                Try
                    strDOB = TxtDobDate.Text
                Catch ex As Exception
                    strDOB = Now
                End Try
                txtPhNo.Text = IIf(txtPhNo.Text = "", "0", txtPhNo.Text)
                
                Dim s As String = ""
               





                str = "select * from M_MemberMaster where Formno='" & Session("Formno") & "'"
                Dt1 = Obj.GetData(str)
                If Dt1.Rows.Count > 0 Then
                    MembName = Dt1.Rows(0)("MemFirstName") & " " & Dt1.Rows(0)("MemLastName")
                    Password = Dt1.Rows(0)("Passw")
                    TransactionPassword = Dt1.Rows(0)("EPassw")
                   
                    If ClearInject(Dt1.Rows(0)("MemfirstName")) <> ClearInject(txtFrstNm.Text) Then
                        Remark = Remark & " Member Name ,"
                    End If
                    

                    If (Dt1.Rows(0)("MemDob")) <> strDOB Then
                        Remark = Remark & "Dob ,"
                    End If
                   
                    If ClearInject(Dt1.Rows(0)("PhN1")) <> ClearInject(txtPhNo.Text) Then
                        Remark = Remark & " PhoneNo,"
                    End If
                    If ClearInject(Dt1.Rows(0)("Mobl")) <> ClearInject(txtMobileNo.Text) Then
                        Remark = Remark & " MobileNo,"
                    End If
                    If ClearInject(Dt1.Rows(0)("Email")) <> ClearInject(txtEMailId.Text) Then
                        Remark = Remark & " Email,"
                    End If
                    
                    If ClearInject(Dt1.Rows(0)("NomineeName")) <> ClearInject(txtNominee.Text) Then
                        Remark = Remark & " NomineeName,"
                    End If

                    If ClearInject(Dt1.Rows(0)("Relation")) <> ClearInject(txtRelation.Text) Then
                        Remark = Remark & " Relation,"
                    End If

                    If ClearInject(Dt1.Rows(0)("City")) <> ClearInject(ddlTehsil.Text) Then
                        Remark = Remark & " City,"
                    End If

                    If ClearInject(Dt1.Rows(0)("StateCode")) <> ClearInject(ddlState.SelectedValue) Then
                        Remark = Remark & " StateCode,"
                    End If


                    If ClearInject(Dt1.Rows(0)("Pincode")) <> ClearInject(txtPinCode.Text) Then
                        Remark = Remark & " Pincode,"
                    End If


                   


                    Remark = Remark & " Changed"

                End If

               
                strQry = " exec Sp_UpdateMemberProfile  '" & Session("FormNo") & "', "
                strQry &= " '" & ClearInject(txtFrstNm.Text.ToUpper) & "','" & ClearInject(txtFNm.Text.ToUpper) & "',"
                strQry &= " '" & Format(strDOB, "dd-MMM-yyyy") & "','" & ClearInject(txtPhNo.Text) & "',"
                strQry &= " '" & ClearInject(txtMobileNo.Text) & "','" & ClearInject(txtEMailId.Text) & "',"
                strQry &= " '" & ClearInject(txtNominee.Text.ToUpper) & "','" & ClearInject(txtRelation.Text.ToUpper) & "', "
                strQry &= " '" & ClearInject(ddlState.SelectedValue) & "','" & ClearInject(ddlTehsil.Text) & "',"
                strQry &= " '" & ClearInject(txtPinCode.Text) & "'"

               
                Dim Qry As String = "Insert Into TempMemberMaster Select MId,SessId,TransNo,IdNo,FormNo,KitId,UpLnFormNo,RefId,LegNo,RefLegNo,RefFormNo,CardNo,Prefix,MemFirstName,MemLastName,MemRelation,MemFName,MemDOB,MemGender,MarrgDate,MemOccupation,Address1,Address2,Post,Tehsil,City,CityCode,District,DistrictCode,StateCode,CountryId,CountryName,PinCode,STDCode,PhN1,PhN2,Fax,Mobl,ComMode,Passw,Doj,NomineeName,NomineeDOB,NomineeAge,Relation,PanNo,BankId,AcNo,IFSCode,ChDDNo,ChDDBankId,EMail,BV,Imported,UpGrdSessId,IsPanCard,E_MainPassw,EPassw,PlanId,Remarks,Fld1,Fld2,Fld3,Fld4,Fld5,ActiveStatus,RecTimeStamp,LastModified,UserCode,UserId,IsMarried,memPic,KitPlanId,IsTopUp,DSessId,UpgradeDate,BankName,BillNo,UpgrdDSessId,FSessId,RP,SP,CancelTopUp,MICRCode,BranchName,HostIp,ChDDDate,ChDDBank,ChDDBranch,Fld6,PID,Paymode,ProfilePic,IsBlock,BlockRemark,BlockDate,DeliveryAddress,DeliveryCenter,PV,AadharNo,AadharNo2,AadharNo3,MFormno,IsCompany,RegType,RegNo,PostalPin,PostalStateCode,PostalCityCode,PostalAreaCode,AreaCode,PostalDistrictCode,'Update Profile - " & Context.Request.UserHostAddress.ToString & "',GetDate(),'U',null From M_MemberMaster Where FormNo='" & Val(Session("FormNo")) & "'"
                Qry = Qry & " insert into UserHistory(UserId,UserName,PageName,Activity,ModifiedFlds,RecTimeStamp,MemberId)Values" & _
                "(0,'" & Session("MemName") & "','Profile','Profile Update','" & Remark & "',Getdate(),'" & Session("FormNo") & "')"
                Qry = Qry & strQry

                Dim i As Integer = Obj.SaveData(Qry)

                If i <> 0 Then
                    '   divBank.Visible = False
                    scrname = "<SCRIPT language='javascript'>alert('Profile Successfully Updated');" & "</SCRIPT>"
                Else
                    scrname = "<SCRIPT language='javascript'>alert('Try Again Later.');" & "</SCRIPT>"
                End If
                Me.RegisterStartupScript("MyAlert", scrname)
                FillDetail()
                Exit Sub
            Catch e As Exception
                scrname = "<SCRIPT language='javascript'>alert('" & e.Message & "');" & "</SCRIPT>"
                Me.RegisterStartupScript("MyAlert", scrname)
                dbGeneral.myMsgBx(e.Message)
                Return
            End Try
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Sub

    Private Sub sendSMS(ByVal Username As String, ByVal MobileNo As String, ByVal Password As String, ByVal TransPassword As String)

        Dim client As New WebClient
        Dim baseurl As String
        Dim data As Stream
        Dim datet As DateTime = Now
        Dim sms As String = "Dear " & Username & ", Your login details are ID-" & Session("Idno") & "/ Pwd-" & Password & "/Trans Code-" & TransPassword & ", pls visit " & Session("CompWeb") & " for more details."


        Try

            baseurl = " http://49.50.77.216/API/SMSHttp.aspx?UserId=" & Session("SmsId") & "&pwd=" & Session("SmsPass") & "&Message=" & sms & "&Contacts=" & MobileNo & "&SenderId=" & Session("ClientId") & ""

            data = client.OpenRead(baseurl)
            Dim reader As New StreamReader(data)
            Dim s As String
            s = reader.ReadToEnd()
            data.Close()
            reader.Close()
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
              

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        UpdateDb()
    End Sub

    Protected Sub CmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        Response.Redirect("index.aspx")
        'Response.Redirect("Epincpindex.aspx")
    End Sub

    Private Function ClearInject(ByVal StrObj As String) As String
        Try
            StrObj = Replace(Replace(Replace(StrObj, ";", ""), "'", ""), "=", "")
            Return Trim(StrObj)
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Function
    

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try


        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try

       
          
        Catch ex As Exception
            Dim path As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim text As String = path & ":  " & Format(Now, "dd-MMM-yyyy hh:mm:ss:fff " & Environment.NewLine)
            Obj.WriteToFile(text & ex.Message)
            Response.Write("Try later.")
        End Try
    End Sub
End Class
