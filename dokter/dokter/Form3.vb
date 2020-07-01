Public Class Form3
    Sub tampil_obat()
        Dim sql As String
        sql = "SELECT * FROM obat"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "obat")
        DataGridView1.DataSource = ds.Tables("obat")
        DataGridView1.Refresh()
    End Sub
    Function cek_formkosong() As Boolean
        If TextBox2.Text = "" Or
            TextBox3.Text = "" Then
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
       
    End Sub
    Sub noIdobat()
        Dim sql As String
        sql = "select idObat from obat order by idObat desc"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Label6.Text = "OB001"
        Else
            Label6.Text = "OB" + Format(Microsoft.VisualBasic.Right(dr.Item("idObat"),
                                                                    2) + 1, "00")
        End If
        status = True
        TextBox2.Focus()
    End Sub
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil_obat()
        Call clear()
        Call noIdobat()
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Label6.Text = .Cells(0).Value
            TextBox2.Text = .Cells(1).Value
            TextBox3.Text = .Cells(2).Value
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
        cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM obat WHERE idObat = '" & Label6.Text & " ' ", con)
        cmd.ExecuteNonQuery()
        Call tampil_obat()
        Call clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Close()
        con.Open()
        Dim sql As String
        If cek_formkosong() Then
            If status Then
                sql = "INSERT INTO obat VALUES (' " & Label6.Text & "'," & _
                    "'" & TextBox2.Text & "','" & TextBox3.Text & "')"
            Else
                sql = "UPDATE obat SET namaObat='" & TextBox2.Text & "'," & _
                    "hargaObat='" & TextBox3.Text & "' WHERE idObat='" & Label6.Text & "'"
            End If
            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Call tampil_obat()
            Call clear()
            Call noIdobat()
            Button2.Enabled = False
            Button3.Enabled = False

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button2.Enabled = True
        status = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        con.Close()
        con.Open()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from obat where namaObat like '%" & TextBox1.Text & "%' ", con)
        ds = New DataSet
        da.Fill(ds, "obat")
        DataGridView1.DataSource = ds.Tables("obat")
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Button2.Enabled = True
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()

    End Sub
End Class