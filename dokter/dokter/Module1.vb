Imports MySql.Data.MySqlClient
Module Module1
    Public con As MySqlConnection
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public dr As MySqlDataReader
    Public ds As DataSet
    Public status As Boolean

    Public Sub koneksi()
        con = New MySqlConnection("server = localhost; " & _
                                  "user id = root; " & _
                                  "password = ''; " & _
                                  "database = rumah-sakit;")
        con.Open()

    End Sub
End Module
