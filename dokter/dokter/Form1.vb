Public Class Form1
    Sub tampil_dokter()
        Dim sql As String
        sql = "SELECT * FROM dokter"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "dokter")
        DataGridView1.DataSource = ds.Tables("dokter")
        DataGridView1.Refresh()
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
    Sub clear()

        TextBox2.Clear()
        TextBox3.Clear()
        DateTimePicker1.Value = Today
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()


    End Sub
    Sub noIdDokter()
        Dim sql As String
        sql = "select iddokter from dokter order by iddokter desc"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Label6.Text = "DR01"
        Else
            Label6.Text = "DR" + Format(Microsoft.VisualBasic.Right(dr.Item("iddokter"),
                                                                    2) + 1, "00")
        End If
        status = True
        TextBox2.Focus()
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        con.Close()
        con.Open()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from dokter where namadokter like '%" & TextBox1.Text & "%' ", con)
        ds = New DataSet
        da.Fill(ds, "dokter")
        DataGridView1.DataSource = ds.Tables("dokter")
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil_dokter()
        Call clear()
        Call noIdDokter()
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
            DateTimePicker1.Value = .Cells(3).Value
            TextBox4.Text = .Cells(4).Value
            TextBox5.Text = .Cells(5).Value
            TextBox6.Text = .Cells(6).Value
            TextBox7.Text = .Cells(7).Value

        End With
        Button2.Enabled = False
        Button4.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Close()
        con.Open()
        Dim sql As String
        If cek_formkosong() Then
            If status Then
                sql = "INSERT INTO dokter VALUES (' " & Label6.Text & "'," & _
                    "'" & TextBox2.Text & "','" & TextBox3.Text & "'," & _
                    "'" & Format(DateTimePicker1.Value, "yy-MM-dd") & "'," & _
                    "'" & TextBox4.Text & "'," & _
                    "'" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')"
            Else
                sql = "UPDATE dokter SET namadokter ='" & TextBox2.Text & "'," & _
                    "alamatDokter='" & TextBox3.Text & "'," & _
                    "tglLahir='" & Format(DateTimePicker1.Value, "yy-MM-dd") & "'," & _
                    "jenisKelamin ='" & TextBox4.Text & "'," & _
                    "spesialis='" & TextBox5.Text & "'," & _
                    "tarifDokter= '" & TextBox6.Text & "' ," & _
                    "TahunMasuk ='" & TextBox7.Text & "' WHERE iddokter='" & Label6.Text & "'"
            End If
            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Call tampil_dokter()
            Call clear()
            Call noIdDokter()
            Button2.Enabled = False
            Button3.Enabled = False
            

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Close()
        con.Open()
        cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM dokter WHERE iddokter = '" & Label6.Text & " ' ", con)
        cmd.ExecuteNonQuery()
        Call tampil_dokter()
        Call clear()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Button2.Enabled = True
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button2.Enabled = True
        status = False
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class
