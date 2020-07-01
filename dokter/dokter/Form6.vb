Public Class Form6
    Sub tampil_pasien()
        Dim sql As String
        sql = "SELECT * FROM pasien"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "pasien")
        DataGridView1.DataSource = ds.Tables("pasien")
        DataGridView1.Refresh()
    End Sub
    Sub clear()

        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""
        DateTimePicker1.Value = Today
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        Label6.Text = ""
        PictureBox2.ImageLocation = ""

    End Sub
    Function cek_formkosong() As Boolean
        If TextBox2.Text = "" Or
            TextBox3.Text = "" Or
            TextBox4.Text = "" Or
            TextBox5.Text = "" Or
            TextBox6.Text = "" Or
            TextBox7.Text = "" Then
            MessageBox.Show("data masih ada yang kosong", "PERHATIAN",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        Else
            Return True
        End If
    End Function
    Sub noIdpasien()
        Dim sql As String
        sql = "select idPasien from pasien order by idPasien desc"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Label1.Text = "NRM001"
        Else
            Label1.Text = "NRM" + Format(Microsoft.VisualBasic.Right(dr.Item("idPasien"),
                                                                    2) + 1, "00")
        End If
        status = True
        TextBox2.Focus()
    End Sub
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil_pasien()
        Call noIdpasien()
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        PictureBox2.ImageLocation = OpenFileDialog1.FileName
        Label6.Text = OpenFileDialog1.FileName
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        Label6.Text = OpenFileDialog1.FileName.Replace("\", "\\")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Close()
        con.Open()
        Dim sql As String
        If cek_formkosong() Then
            If status Then
                sql = "INSERT INTO pasien VALUES('" & Label1.Text & "' , '" & TextBox2.Text & "' , '" & TextBox3.Text & "' , '" & ComboBox1.Text & "' , '" & Format(DateTimePicker1.Value, "yy-MM-dd") & "' ,'" & TextBox4.Text & "' , '" & TextBox5.Text & "' , '" & TextBox6.Text & "' , '" & TextBox7.Text & "' , '" & Label6.Text & "')"
            Else
                sql = "UPDATE pasien SET namaPasien='" & TextBox2.Text & "' , alamatPasien='" & TextBox3.Text & "' , jeniskelamin='" & ComboBox1.Text & "' , tglLahir='" & Format(DateTimePicker1.Value, "yy-MM-dd") & "' , agama='" & TextBox4.Text & "' , statusPerkawinan='" & TextBox5.Text & "' , tinggiBadan='" & TextBox6.Text & "' , beratBadan='" & TextBox7.Text & "' , foto='" & Label6.Text & "' WHERE idPasien='" & Label1.Text & "'"
            End If

            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Call tampil_pasien()
            Call noIdpasien()
            Call clear()
            Button2.Enabled = True
            Button3.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button2.Enabled = True
        status = False
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
            TextBox4.Text = .Cells(5).Value
            TextBox5.Text = .Cells(6).Value
            TextBox6.Text = .Cells(7).Value
            TextBox7.Text = .Cells(8).Value
            PictureBox2.ImageLocation = .Cells(9).Value

        End With
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        Button2.Enabled = False
        Button4.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        con.Close()
        con.Open()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from pasien where namaPasien like '%" & TextBox1.Text & "%' ", con)
        ds = New DataSet
        da.Fill(ds, "pasien")
        DataGridView1.DataSource = ds.Tables("pasien")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Close()
        con.Open()
        cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM pasien WHERE idPasien = '" & Label1.Text & " ' ", con)
        cmd.ExecuteNonQuery()
        Call tampil_pasien()
        Call clear()
    End Sub
End Class