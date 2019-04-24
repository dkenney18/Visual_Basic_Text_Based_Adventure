Imports Microsoft.VisualBasic
Imports System
Imports System.Data.SQLite

Class Database_SQLite
    Public Shared Function CreateConnection() As SQLiteConnection
        Dim sc As SQLiteConnection
        Dim cs As String = System.IO.Directory.GetCurrentDirectory()
        cs = "\TextBasedAdventure\SaveGame.db"
        cs = "Data Source = " + cs

        sc = New SQLiteConnection(cs)

        Try
            sc.Open()
        Catch ex As Exception
            Console.Write("Database_SQLite.CreateConnection() Error, WRONG STRING: " & ex.Message)
        End Try

        Return sc
    End Function

    Public Shared Function InsertData(ByVal Name As String, ByVal XP As Integer, ByVal saveID As String, ByVal swordName As String, ByVal swordDamage As Integer, ByVal ringName As String, ByVal ringDamage As Integer, ByVal helmetName As String, ByVal helmetHealthPoints As Integer, ByVal breastplateName As String, ByVal breastplateHealthPoints As Integer, ByVal legName As String, ByVal legHealthPoints As Integer, ByVal feetName As String, ByVal feetHealthPoints As Integer, ByVal conn As SQLiteConnection) As String
        Dim i As String = ""
        i += "insert into SavedGame"
        i += "(Name, XP, saveID, swordName, swordDamage, ringName, ringDamage, helmetName, helmetHealthPoints, breastplateName, breastplateHealthPoints, legName, legHealthPoints, feetName, feetHealthPoints)"
        i += " values "
        i += "('" & Name & "', '" & XP & "', '" & saveID & "', '" & swordName & "', '" & swordDamage & "', '" & ringName & "', '" & ringDamage & "', '" & helmetName & "', '" & helmetHealthPoints & "', '" & breastplateName & "', '" & breastplateHealthPoints & "', '" & legName & "', '" & legHealthPoints & "', '" & feetName & "', '" & feetHealthPoints & "')"

        Try
            Dim sqlite_cmd As SQLiteCommand
            sqlite_cmd = conn.CreateCommand()
            sqlite_cmd.CommandText = i
            sqlite_cmd.ExecuteNonQuery()
            i = vbCrLf & "Insert Completed Successfully." & vbCrLf
        Catch ex As Exception
            i = "Database_SQLite.InsertData() Error:" & ex.Message
        End Try

        Return i
    End Function

    Public Shared Function UpdateData_Movie(ByVal Name As String, ByVal XP As Integer, ByVal saveID As String, ByVal swordName As String, ByVal swordDamage As Integer, ByVal ringName As String, ByVal ringDamage As Integer, ByVal helmetName As String, ByVal helmetHealthPoints As Integer, ByVal breastplateName As String, ByVal breastplateHealthPoints As Integer, ByVal legName As String, ByVal legHealthPoints As Integer, ByVal feetName As String, ByVal feetHealthPoints As Integer, ByVal conn As SQLiteConnection) As String
        Dim i As String = ""

        If saveID.ToString().Trim().Length > 0 Then
            i += "  update SavedGame "
            i += "  set "

            If Name.ToString.Trim().Length > 0 Then
                i += "  Name = " & """"c & Name & """"c & ","
            End If

            If XP.ToString.Trim().Length > 0 Then
                i += "  XP = " & """"c & XP & """"c & ","
            End If

            If saveID.ToString().Trim().Length > 0 Then
                i += "  SaveID = " & """"c & saveID & """"c & ","
            End If

            If swordName.ToString().Trim().Length > 0 Then
                i += "  swordName = " & """"c & swordName & """"c & ","
            End If

            If swordDamage.ToString().Trim().Length > 0 Then
                i += "  swordDamage = " & """"c & swordDamage & """"c & ","
            End If

            If ringName.ToString().Trim().Length > 0 Then
                i += "  ringName = " & """"c & ringName & """"c & ","
            End If

            If ringDamage.ToString().Trim().Length > 0 Then
                i += "  ringDamage = " & """"c & ringDamage & """"c & ","
            End If

            If helmetName.ToString().Trim().Length > 0 Then
                i += "  helmetName = " & """"c & helmetName & """"c & ","
            End If

            If helmetHealthPoints.ToString().Trim().Length > 0 Then
                i += "  helmetHealthPoints = " & """"c & helmetHealthPoints & """"c & ","
            End If

            If breastplateName.ToString().Trim().Length > 0 Then
                i += "   breastplateName = " & """"c & breastplateName & """"c & ","
            End If

            If breastplateHealthPoints.ToString().Trim().Length > 0 Then
                i += "   breastplateHealthPoints = " & """"c & breastplateHealthPoints & """"c & ","
            End If

            If legName.ToString().Trim().Length > 0 Then
                i += "  legsName = " & """"c & legName & """"c & ","
            End If

            If legHealthPoints.ToString().Trim().Length > 0 Then
                i += "  legHealthPoints = " & """"c & legHealthPoints & """"c & ","
            End If

            If feetName.ToString().Trim().Length > 0 Then
                i += "  feetName = " & """"c & feetName & """"c & ","
            End If

            If feetHealthPoints.ToString().Trim().Length > 0 Then
                i += "  feetHealthPoints = " & """"c & feetHealthPoints & """"c & ","
            End If

            i += " where SaveID = " & saveID

            Try
                Dim sqlite_cmd As SQLiteCommand
                sqlite_cmd = conn.CreateCommand()
                sqlite_cmd.CommandText = i
                sqlite_cmd.ExecuteNonQuery()
                i = vbCrLf & "Update Completed Successfully. Enter 0 to continue" & vbCrLf
            Catch e As Exception
                i = "Database_SQLite.UpdateData_Movie() Error: " & e.Message
            End Try
        End If

        Return i
    End Function

    Public Shared Function DeleteData_Movie(ByVal saveID As String, ByVal conn As SQLiteConnection) As String
        Dim i As String = ""
        i += "Delete from SavedGame "
        i += "Where saveID = " & saveID

        Try

            If saveID.ToString().Length > 0 Then
                Dim sqlite_cmd As SQLiteCommand
                sqlite_cmd = conn.CreateCommand()
                sqlite_cmd.CommandText = i
                sqlite_cmd.ExecuteNonQuery()
                i = vbCrLf & "Delete completed successfully." & vbCrLf
            End If

        Catch ex As Exception
            i = "Database_SQLite.DeleteData_Movie() Error:" & ex.Message
        End Try

        Return i
    End Function

    Public Shared Function Select_MovieData(ByVal conn As SQLiteConnection) As String
        Dim s As String = ""
        Dim q As String = "select Name, XP, saveID, swordName, swordDamage, ringName, ringDamage, helmetName, helmetHealthPoints, breastplateName, breastplateHealthPoints, legName, legHealthPoints, feetName, feetHealthPoints from SavedGame"
        Dim Name As String = ""
        Dim XP As Integer = 0
        Dim saveID As Integer = 0
        Dim swordName As String = ""
        Dim swordDamage As Integer = 0
        Dim ringName As String = ""
        Dim ringDamage As Integer = 0
        Dim helmetName As String = ""
        Dim helmetHealthPoints As Integer = 0
        Dim breastplateName As String = ""
        Dim breastplateHealthPoints As Integer = 0
        Dim legName As String = ""
        Dim legHealthPoints As Integer = 0
        Dim feetName As String = ""
        Dim feetHealthPoints As Integer = 0
        Dim sqlr As SQLiteDataReader
        Dim sqlite_cmd As SQLiteCommand
        sqlite_cmd = conn.CreateCommand()
        sqlite_cmd.CommandText = q
        sqlr = sqlite_cmd.ExecuteReader()

        While sqlr.Read()

            Try

                Name = sqlr("Name").ToString()
                XP = sqlr("XP").ToString()
                saveID = sqlr("saveID").ToString()
                swordName = sqlr("swordName").ToString()
                swordDamage = sqlr("swordDamage").ToString
                ringName = sqlr("ringName").ToString()
                ringDamage = sqlr("ringDamage").ToString()
                helmetName = sqlr("helmetName").ToString()
                helmetHealthPoints = sqlr("helmetHealthPoints").ToString()
                breastplateName = sqlr("breastplateName").ToString()
                breastplateHealthPoints = sqlr("breastplateHealthPoints").ToString()
                legName = sqlr("legName").ToString()
                legHealthPoints = sqlr("legHealthPoints").ToString()
                feetName = sqlr("feetName").ToString()
                feetHealthPoints = sqlr("feetHealthPoints").ToString()



                If Name.ToString.Trim().Length > 0 Then
                    s += "  Name: " & """"c & Name & """"c
                End If

                If XP.ToString.Trim().Length > 0 Then
                    s += "  XP: " & """"c & XP & """"c
                End If

                If swordName.ToString.Trim().Length > 0 Then
                    s += "  Sword name: " & """"c & swordName & """"c
                End If

                If swordDamage.ToString.Trim().Length > 0 Then
                    s += "  Sword damage: " & """"c & swordDamage & """"c
                End If

                If ringName.ToString.Trim().Length > 0 Then
                    s += "  Ring name: " & """"c & ringName & """"c
                End If

                If ringDamage.ToString.Trim().Length > 0 Then
                    s += "  Ring damage: " & """"c & ringDamage & """"c
                End If

                If helmetName.ToString.Trim().Length > 0 Then
                    s += "  Helmet name: " & """"c & helmetName & """"c
                End If

                If helmetHealthPoints.ToString.Trim().Length > 0 Then
                    s += "  Helmet health points: " & """"c & helmetHealthPoints & """"c
                End If

                If breastplateName.ToString.Trim().Length > 0 Then
                    s += "  Breastplate name: " & """"c & breastplateName & """"c
                End If

                If breastplateHealthPoints.ToString.Trim().Length > 0 Then
                    s += "  Breastplate health points: " & """"c & breastplateHealthPoints & """"c
                End If

                If legName.ToString.Trim().Length > 0 Then
                    s += "  Leg name: " & """"c & legName & """"c
                End If

                If legHealthPoints.ToString.Trim().Length > 0 Then
                    s += "  Leg health points: " & """"c & legHealthPoints & """"c
                End If

                If feetName.ToString.Trim().Length > 0 Then
                    s += "  Feet name: " & """"c & feetName & """"c
                End If

                If feetHealthPoints.ToString.Trim().Length > 0 Then
                    s += "  Feet health points: " & """"c & feetHealthPoints & """"c
                End If

                s += vbCrLf
            Catch ex As Exception
                s = "Select_MovieData() Error: " & ex.Message
            End Try
        End While

        Return s
    End Function

    Public Shared Function Apost(ByVal s As String) As String
        s = s.Replace("'", "''").Trim()
        Return s
    End Function
End Class
