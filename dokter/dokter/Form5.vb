Public Class Form5

    Sub Viewpendaftaran()
        Dim sql As String
        sql = "SELECT * FROM tampilpendaftaran"
        da = New MySql.Data.MySqlClient.MySqlDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "tampilpendaftaran")
        DataGridView1.DataSource = ds.Tables("tampilpendaftaran")
        DataGridView1.Refresh()
    End Sub

    Sub tampil_idpendaftaran()
        Dim sql As String
        sql = "select nopendaftaran from pendaftaran order by nopendaftaran desc"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Label6.Text = "DFTR001"
        Else
            Label6.Text = "DFTR" + Format(Microsoft.VisualBasic.Right(dr.Item("nopendaftaran"),
                                                                    2) + 1, "00")
        End If
        status = True
    End Sub
    Function cek_formkosong() As Boolean
        If RichTextBox1.Text = "" Then
            MessageBox.Show("data masih ada yang kosong", "PERHATIAN",
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        Else
            Return True
        End If
    End Function
    Sub bersih()

        Label1.Text = ""
        ComboBox1.Text = ""
        Label14.Text = ""
        Label15.Text = ""
        ComboBox2.Text = ""
        Label17.Text = ""
        Label18.Text = ""
        RichTextBox1.Text = ""
        ComboBox3.Text = ""
        Label24.Text = ""
        Label25.Text = ""

    End Sub
    Sub pasien()
        con.Close()
        con.Open()

        Dim sql As String
        sql = "SELECT * FROM pasien"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader

        If dr.HasRows Then
            Do While dr.Read
                ComboBox1.Items.Add(dr(0))
            Loop
        End If

    End Sub
   
    Sub Dokter()
        con.Close()
        con.Open()
        Dim sql As String
        sql = "SELECT * FROM dokter"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            Do While dr.Read
                ComboBox2.Items.Add(dr(0))
            Loop
        End If
    End Sub
    Sub Petugas()
        con.Close()
        con.Open()
        Dim sql As String
        sql = "SELECT * FROM petugas"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        If dr.HasRows Then
            Do While dr.Read
                ComboBox3.Items.Add(dr(0))
            Loop

        End If

    End Sub
   

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call koneksi()
        Call Viewpendaftaran()
        Call tampil_idpendaftaran()
        Call pasien()
        Call Dokter()
        Call Petugas()

        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Label21.Text = Date.Today
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        con.Close()
        con.Open()
        Dim sql As String
        sql = "SELECT * FROM pasien WHERE idPasien= '" & ComboBox1.Text & "'"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Label14.Text = dr.Item("namaPasien")
            Label15.Text = dr.Item("jeniskelamin")
            PictureBox2.ImageLocation = dr.Item("foto")
        End If
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        con.Close()
        con.Open()
        Dim sql As String
        sql = "SELECT * FROM dokter WHERE iddokter= '" & ComboBox2.Text & "' "
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Label17.Text = dr.Item("namadokter")
            Label18.Text = dr.Item("spesialis")
        End If
    End Sub
   
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        con.Close()
        con.Open()
        Dim sql As String
        sql = "SELECT * FROM petugas WHERE idpetugas='" & ComboBox3.Text & "'"
        cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Label24.Text = dr.Item("namapetugas")
            Label25.Text = dr.Item("divisi")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Close()
        con.Open()
        Dim sql As String
        If cek_formkosong() Then
            If status Then
                sql = "INSERT INTO pendaftaran VALUES('" & Label6.Text & "' , '" & ComboBox1.Text & "' , '" & ComboBox3.Text & "' , '" & ComboBox2.Text & "' , '" & Format(DateTimePicker1.Value, "yy-MM-dd") & "' , '" & RichTextBox1.Text & "')"
            Else
                sql = "UPDATE pendaftaran SET idPasien='" & ComboBox1.Text & "' , idpetugas='" & ComboBox3.Text & "' , iddokter='" & ComboBox2.Text & "'  , tanggaldaftar='" & Format(DateTimePicker1.Value, "yy-MM-dd") & "' , keterangan='" & RichTextBox1.Text & "' WHERE nopendaftaran='" & Label6.Text & "' "
            End If

            cmd = New MySql.Data.MySqlClient.MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()
            Call Viewpendaftaran()
            Call tampil_idpendaftaran()
            Call bersih()
            Button2.Enabled = False
            Button3.Enabled = False
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Close()
        con.Open()
        cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM pendaftaran WHERE nopendaftaran = '" & Label6.Text & " ' ", con)
        cmd.ExecuteNonQuery()
        Call Viewpendaftaran()
        Call bersih()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button2.Enabled = True
        status = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer

        i = DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            Label6.Text = .Cells(0).Value
            DateTimePicker1.Value = .Cells(1).Value
            ComboBox1.Text = .Cells(2).Value
            Label14.Text = .Cells(3).Value
            Label15.Text = .Cells(4).Value
            ComboBox2.Text = .Cells(5).Value
            Label17.Text = .Cells(6).Value
            Label18.Text = .Cells(7).Value
            ComboBox3.Text = .Cells(8).Value
            Label24.Text = .Cells(9).Value
            Label25.Text = .Cells(10).Value
            RichTextBox1.Text = .Cells(11).Value
        End With
        Button2.Enabled = False
        Button4.Enabled = True
        Button3.Enabled = True
    End Sub

 
End Class