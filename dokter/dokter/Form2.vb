Public Class Form2

    Sub tampil_perawat()
        Dim sql As String
        sql = "SELECT * FROM perawat"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "perawat")
        DataGridView1.DataSource = ds.Tables("perawat")
        DataGridView1.Refresh()
    End Sub

    Function cek_formkosong() As Boolean
        If TextBox2.Text = "" Or
            TextBox3.Text = "" Or
            TextBox4.Text = "" Or
            TextBox5.Text = "" Then
            MessageBox.Show("data masih ada yang kosong", "PERHATIAN",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        Else
            Return True
        End If
    End Function
    Sub clear()

        TextBox2.Clear()
        TextBox3.Clear()
        DateTimePicker1.Value = Today
        TextBox4.Clear()
        TextBox5.Clear()


    End Sub
    Sub noIdPerawat()
        Dim sql As String
        sql = "select idperawat from perawat order by idperawat desc"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Label6.Text = "PR01"
        Else
            Label6.Text = "PR" + Format(Microsoft.VisualBasic.Right(dr.Item("idperawat"),
                                                                    2) + 1, "00")
        End If
        status = True
        TextBox2.Focus()
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil_perawat()
        Call clear()
        Call noIdPerawat()
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Label6.Text = .Cells(0).Value
            TextBox2.Text = .Cells(1).Value
            TextBox3.Text = .Cells(2).Value
            TextBox4.Text = .Cells(3).Value
            DateTimePicker1.Value = .Cells(4).Value
            TextBox5.Text = .Cells(5).Value

        End With
        Button2.Enabled = False
        Button4.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Close()
        con.Open()
        cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM perawat WHERE idperawat = '" & Label6.Text & " ' ", con)
        cmd.ExecuteNonQuery()
        Call tampil_perawat()
        Call clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button2.Enabled = True
        status = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        con.Close()
        con.Open()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from perawat where namaperawat like '%" & TextBox1.Text & "%' ", con)
        ds = New DataSet
        da.Fill(ds, "perawat")
        DataGridView1.DataSource = ds.Tables("perawat")

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Button2.Enabled = True
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        con.Close()
        con.Open()
        Dim sql As String
        If cek_formkosong() Then
            If status Then
                sql = "INSERT INTO perawat VALUES (' " & Label6.Text & "'," & _
                    "'" & TextBox2.Text & "','" & TextBox3.Text & "'," & _
                    "'" & TextBox4.Text & "'," & _
                    "'" & Format(DateTimePicker1.Value, "yy-MM-dd") & "'," & _
                    "'" & TextBox5.Text & "')"
            Else
                sql = "UPDATE perawat SET namaperawat='" & TextBox2.Text & "'," & _
                    "AlamatPerawat='" & TextBox3.Text & "'," & _
                    "JenisKelamin ='" & TextBox4.Text & "'," & _
                    "TanggalLahir ='" & Format(DateTimePicker1.Value, "yy-MM-dd") & "'," & _
                    "TahunMasuk='" & TextBox5.Text & "' WHERE idperawat='" & Label6.Text & "'"
            End If
            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Call tampil_perawat()
            Call clear()
            Call noIdPerawat()
            Button2.Enabled = False
            Button3.Enabled = False

        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub
End Class