Public Class Form1
    Function CheckInputs() As Integer
        Dim result As Integer = 0
        If (String.IsNullOrEmpty(TextBox1.Text)) Or Not My.Computer.FileSystem.DirectoryExists(TextBox1.Text) Then
            result += 1
        End If
        If (String.IsNullOrEmpty(TextBox2.Text)) Or Not My.Computer.FileSystem.DirectoryExists(TextBox2.Text) Then
            result += 1
        End If
        If (String.IsNullOrEmpty(TextBox3.Text)) Or Not My.Computer.FileSystem.FileExists(TextBox3.Text) Then
            result += 1
        End If
        Return result
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (FolderBrowserDialog1.ShowDialog() = DialogResult.OK) Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (FolderBrowserDialog2.ShowDialog() = DialogResult.OK) Then
            TextBox2.Text = FolderBrowserDialog2.SelectedPath
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (OpenFileDialog1.ShowDialog() = DialogResult.OK) Then
            TextBox3.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub StartProcess(ByVal info As ProcessStartInfo)
        Dim proc As New Process
        Try
            proc.StartInfo = info
            proc.Start()
            proc.WaitForExit()
            proc.Close()
        Catch ex As Exception
            MsgBox("Error while starting winlogbea: " & ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ProgressBar1.Value = 0
        lblStatus.Text = "Checking inputs..."
        lblStatus.Update()
        If CheckInputs() > 0 Then
            lblStatus.Text = "Input is wrong..."
            Return
        End If
        Dim Logs() As String = IO.Directory.GetFiles(TextBox2.Text)
        Dim i As Integer = If(Logs.Length > 0, 1000 / Logs.Length, 1000)
        lblStatus.Text = "Deleting registry file..."
        lblStatus.Update()
        My.Computer.FileSystem.DeleteFile(TextBox1.Text & "\data\evtx-registry.yml")
        For Each file In Logs
            lblStatus.Text = "Sending logfile " & file.Split("\").Last
            lblStatus.Update()
            ProgressBar1.Increment(i)
            Dim info As New ProcessStartInfo
            info.FileName = TextBox1.Text & "\winlogbeat.exe"
            info.Arguments = "-e -c " & TextBox3.Text & " -E EVTX_FILE=""" & file & """ "
            info.WindowStyle = ProcessWindowStyle.Normal
            info.UseShellExecute = True
            info.ErrorDialog = True
            StartProcess(info)
        Next
        lblStatus.Text = "Done uploading, everything went well..."
        lblStatus.Update()
        MsgBox("Import finished", MsgBoxStyle.Information)
    End Sub
End Class
