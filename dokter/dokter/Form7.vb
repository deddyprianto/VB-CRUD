Public Class Form7
    Sub tampil_petugas()
        Dim sql As String
        sql = "SELECT * FROM petugas"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "petugas")
        DataGridView1.DataSource = ds.Tables("petugas")
        DataGridView1.Refresh()
    End Sub
    Sub clear()

        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        DateTimePicker1.Value = Today
        TextBox5.Clear()
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        Label6.Text = ""
        PictureBox2.ImageLocation = ""

    End Sub
    Function cek_formkosong() As Boolean
        If TextBox2.Text = "" Or
            TextBox3.Text = "" Or
            ComboBox1.Text = "" Or
            ComboBox2.Text = "" Or
            ComboBox3.Text = "" Then
            MessageBox.Show("data masih ada yang kosong", "PERHATIAN",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        Else
            Return True
        End If
    End Function
    Sub noIdpetugas()
        Dim sql As String
        sql = "select idpetugas from petugas order by idpetugas desc"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Label1.Text = "PTG001"
        Else
            Label1.Text = "PTG" + Format(Microsoft.VisualBasic.Right(dr.Item("idpetugas"),
                                                                    2) + 1, "00")
        End If
        status = True
        TextBox2.Focus()
    End Sub
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil_petugas()
        Call noIdpetugas()
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        PictureBox2.ImageLocation = OpenFileDialog1.FileName
        Label6.Text = OpenFileDialog1.FileName
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        Label6.Text = OpenFileDialog1.FileName.Replace("\", "\\")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Close()
        con.Open()
        Dim sql As String
        If cek_formkosong() Then
            If status Then
                sql = "INSERT INTO petugas VALUES('" & Label1.Text & "' , '" & TextBox2.Text & "' , '" & TextBox3.Text & "' , '" & ComboBox1.Text & "' , '" & Format(DateTimePicker1.Value, "yy-MM-dd") & "' ,'" & TextBox5.Text & "' , '" & ComboBox2.Text & "' , '" & ComboBox3.Text & "' , '" & Label6.Text & "')"
            Else
                sql = "UPDATE petugas SET namapetugas='" & TextBox2.Text & "' , alamatPetugas='" & TextBox3.Text & "' , jenisKelamin='" & ComboBox1.Text & "' , TanggalLahir='" & Format(DateTimePicker1.Value, "yy-MM-dd") & "' , NoHp='" & TextBox5.Text & "' , PendidikanTerakhir	='" & ComboBox2.Text & "' , divisi='" & ComboBox3.Text & "' , Foto='" & Label6.Text & "' WHERE idpetugas='" & Label1.Text & "'"
            End If
            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Call tampil_petugas()
            Call noIdpetugas()
            Call clear()
            Button2.Enabled = False
            Button3.Enabled = False

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button2.Enabled = True
        status = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Close()
        con.Open()
        cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM petugas WHERE idpetugas = '" & Label1.Text & " ' ", con)
        cmd.ExecuteNonQuery()
        Call tampil_petugas()
        Call clear()
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        con.Close()
        con.Open()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from petugas where namapetugas like '%" & TextBox1.Text & "%' ", con)
        ds = New DataSet
        da.Fill(ds, "petugas")
        DataGridView1.DataSource = ds.Tables("petugas")
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Label1.Text = .Cells(0).Value
            TextBox2.Text = .Cells(1).Value
            TextBox3.Text = .Cells(2).Value
            ComboBox1.Text = .Cells(3).Value
            DateTimePicker1.Value = .Cells(4).Value
            TextBox5.Text = .Cells(5).Value
            ComboBox2.Text = .Cells(6).Value
            ComboBox3.Text = .Cells(7).Value
            PictureBox2.ImageLocation = .Cells(8).Value

        End With
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        Button2.Enabled = False
        Button4.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

    End Sub
End Class