'Devin Kenney
'Text based adventure game with classes and lists
'4/9/2019

Imports System
Imports System.Data.SQLite
Imports System.Text
Imports TextBasedAdventure.Database_SQLite

Module Module1
    'setup and initialize varibles used
    Dim counter As Integer = 0
    Dim name_char As String = ""
    Dim name As String = ""
    Dim damage As Integer = 0
    Dim player As New Avatar
    Dim creature As New Monster
    'decides what set of armor and weapons player has
    Dim index As Integer = 0
    'set up players only once
    Dim setup As Boolean

#Region "Main sub"

    'main sub
    Sub Main()

        Dim answer As String = ""
        'sets up players and monster

        If setup = False Then

            setPlayerAndMonsterUp(index)

        End If

        While True

            'reset players health for a cost, this is nessary once you die.
            If player.HP <= 0 Then

                Console.Write(vbNewLine + "Your health is reastored every time you die, this costs 75XP points" + vbNewLine)
                player.XP -= 75
                Console.Write(vbNewLine + "You now have: " + player.XP.ToString + " xp" + vbNewLine)
                player.HP = setHealthPoints(index)
                Console.Write("set health to: " + setHealthPoints(index).ToString)

            End If

            'continue with the loop asking options
            Console.Write(vbNewLine)
            Console.Write("do you want to adventure(a), upgrade(u), buy new items(b), save your game(s), recall as saved game(r) or exit(e): ")
            answer = Console.ReadLine().ToString.Trim

            Select Case answer
                Case "a"
                    adventure(index)
                Case "u"
                    upgradeItems(index)
                Case "b"
                    buyNewItems(index)
                Case "s"
                    saveGame(index)
                Case "r"
                    recallSaves(index)
                Case "e"
                    End
            End Select

        End While

    End Sub
#End Region

#Region "Options for Main() sub"

    'sub for farming monsters
    Sub adventure(index)

        'set health of player
        setHealthPoints(index)

        Console.Write(vbNewLine)
        Console.Write("Fighting " + creature.name.ToString)
        'while the players health is still greater then or = to 0 do this loop
        Do While player.HP >= 0

            Console.Write(vbNewLine)
            Console.Write(vbNewLine + player.Name.ToString + " attacks " + creature.name.ToString + " and does " + player.weapons.Item(index).swordDamage.ToString + " Damage")
            Console.Write(vbNewLine + creature.name.ToString + " attacks " + player.Name.ToString + " and does " + creature.damage.ToString + " Damage")
            Console.Write(vbNewLine + player.Name.ToString + " health is: " + player.HP.ToString)
            Console.Write(vbNewLine + creature.name.ToString + " health: " + creature.health.ToString)

            'set players hp after attack
            player.HP -= creature.damage
            creature.health -= player.weapons.Item(index).swordDamage

            'give xp if creature killed
            If creature.health <= 0 Then

                Console.Write(vbNewLine + "You Won!" + vbNewLine)
                creature.health = 5
                player.XP += creature.givenXP
                Console.Write(vbNewLine + "Your xp is now: " + player.XP.ToString)

            End If
            'exit if player has died
            If player.HP <= 0 Then

                Console.Write(vbNewLine + "You Died" + vbNewLine)
                Exit Do
                Main()

            End If

            'to speed things along if your health is very high the game grants you instant loot
            If player.HP > 30 Then

                'time is in mils
                'sleep so xp and stuff is per sec
                Sleep(0)
                Console.Write(vbNewLine + "You have instant loot" + vbNewLine)
                Console.Write(vbNewLine + "You gained: " & ((player.HP / creature.health) * creature.givenXP).ToString + vbNewLine)

                player.XP += (player.HP / creature.health) * creature.givenXP
                player.HP = 0
                Exit Do
                Main()

            Else

                Sleep(500)

            End If


        Loop

    End Sub
    'upgrade the items you alread have.
    Sub upgradeItems(index)

        Dim answer As String = ""
        Console.Write(vbNewLine + "Upgrading costs 10% of XP per damage point and 30% XP for health points" + vbNewLine)
        Console.Write(vbNewLine + "Upgrade by entering the name of the item you want to upgrade: " + player.weapons.Item(index).swordName.ToString + vbNewLine)
        Console.Write(vbNewLine + "Upgrade " + player.weapons.Item(index).swordName.ToString + vbNewLine)
        Console.Write(vbNewLine + "Upgrade " + player.weapons.Item(index).ringName.ToString + vbNewLine)
        Console.Write(vbNewLine + "Upgrade " + player.armor.Item(index).helmetName.ToString + vbNewLine)
        Console.Write(vbNewLine + "Upgrade " + player.armor.Item(index).breastPlateName.ToString + vbNewLine)
        Console.Write(vbNewLine + "Upgrade " + player.armor.Item(index).legName.ToString + vbNewLine)
        Console.Write(vbNewLine + "Upgrade " + player.armor.Item(index).feetName.ToString + vbNewLine)

        answer = Console.ReadLine()
        'upgrade sword

        If answer.ToLower.Trim = player.weapons.Item(index).swordName.ToString.ToLower Then

            upgradeSword()

        End If

        'upgrade ring
        If answer.ToLower.Trim = player.weapons.Item(index).ringName.ToString.ToLower Then

            upgradeRing()

        End If

        'upgrade helmet
        If answer.ToLower.Trim = player.armor.Item(index).helmetName.ToString.ToLower Then

            upgradeHelmet()

        End If

        'upgrade breast plate
        If answer.ToLower.Trim = player.armor.Item(index).breastPlateName.ToString.ToLower Then

            upgradeBreastPlate()

        End If

        'upgrade Legs
        If answer.ToLower.Trim = player.armor.Item(index).legName.ToString.ToLower Then

            upgradeLegs()

        End If

        'upgrade feet
        If answer.ToLower.Trim = player.armor.Item(index).feetName.ToString.ToLower Then

            upgradeFeet()

        End If

    End Sub

    'upgrade sword options
    Sub upgradeSword()

        Dim answer As String = ""

        Console.Write(vbNewLine + "To upgrade damage enter the number of damage you want to buy(like 1,5,10)" + vbNewLine)
        answer = Console.ReadLine()

        If (answer * 0.1) < player.XP Then

            player.XP -= player.XP * 0.1
            Console.Write(vbNewLine + (player.XP * 0.1).ToString + " XP deducted. You now have" + player.XP.ToString + vbNewLine)
            player.weapons.Item(index).swordDamage += answer
            Console.Write(vbNewLine + "Upgraded " + player.weapons.Item(index).swordName.ToString + " Damage to: " + answer.ToString + vbNewLine)

        Else

            Console.Write(vbNewLine + "Unable to upgrade " + player.weapons.Item(index).swordName.ToString + " damage" + vbNewLine)

        End If

    End Sub

    'upgrade ring options
    Sub upgradeRing()

        Dim answer As String = ""

        Console.Write(vbNewLine + "To upgrade damage enter the number of damage you want to buy(like 1,5,10)" + vbNewLine)
        answer = Console.ReadLine()

        If (answer * 0.1) < player.XP Then

            player.XP -= player.XP * 0.1
            Console.Write(vbNewLine + (player.XP * 0.1).ToString + " XP deducted. You now have" + player.XP.ToString + vbNewLine)
            player.weapons.Item(index).ringDamage += answer
            Console.Write(vbNewLine + "Upgraded " + player.weapons.Item(index).ringName.ToString + " Damage to: " + answer.ToString + vbNewLine)

        Else

            Console.Write(vbNewLine + "Unable to upgrade " + player.weapons.Item(index).swordName.ToString + " damage" + vbNewLine)

        End If

    End Sub

    'upgrade helmet options
    Sub upgradeHelmet()

        Dim answer As String = ""

        Console.Write(vbNewLine + "To upgrade health enter the number of health you want to buy(like 1,5,10)" + vbNewLine)
        answer = Console.ReadLine()

        If (answer * 0.3) < player.XP Then

            player.XP -= player.XP * 0.1
            Console.Write(vbNewLine + (player.XP * 0.1).ToString + " XP deducted. You now have" + player.XP.ToString + vbNewLine)
            player.armor.Item(index).helmentHP += answer
            player.HP = setHealthPoints(index)
            Console.Write(vbNewLine + "Upgraded " + player.armor.Item(index).helmetName.ToString + " health to: " + answer.ToString + vbNewLine)

        Else

            Console.Write(vbNewLine + "Unable to upgrade " + player.armor.Item(index).helmetName.ToString + " health " + vbNewLine)

        End If

    End Sub

    'upgrade breastplate options
    Sub upgradeBreastPlate()

        Dim answer As String = ""

        Console.Write(vbNewLine + "To upgrade health enter the number of health you want to buy(like 1,5,10)" + vbNewLine)
        answer = Console.ReadLine()

        If (answer * 0.3) < player.XP Then

            player.XP -= player.XP * 0.1
            Console.Write(vbNewLine + (player.XP * 0.1).ToString + " XP deducted. You now have" + player.XP.ToString + vbNewLine)
            player.armor.Item(index).breastPlateHP += answer
            player.HP = setHealthPoints(index)
            Console.Write(vbNewLine + "Upgraded " + player.armor.Item(index).breastPlateName.ToString + " health to: " + answer.ToString + vbNewLine)

        Else

            Console.Write(vbNewLine + "Unable to upgrade " + player.armor.Item(index).breastPlateName.ToString + " health " + vbNewLine)

        End If

    End Sub

    'upgrade leg options
    Sub upgradeLegs()

        Dim answer As String = ""

        Console.Write(vbNewLine + "To upgrade health enter the number of health you want to buy(like 1,5,10)" + vbNewLine)
        answer = Console.ReadLine()

        If (answer * 0.3) < player.XP Then

            player.XP -= player.XP * 0.1
            player.armor.Item(index).legHP += answer
            player.HP = setHealthPoints(index)
            Console.Write(vbNewLine + "Upgraded " + player.armor.Item(index).legName.ToString + " health to: " + answer.ToString + vbNewLine)

        Else

            Console.Write(vbNewLine + "Unable to upgrade " + player.armor.Item(index).legName.ToString + " health " + vbNewLine)

        End If

    End Sub

    'upgrade feet options
    Sub upgradeFeet()

        Dim answer As String = ""

        Console.Write(vbNewLine + "To upgrade health enter the number of health you want to buy(like 1,5,10)" + vbNewLine)
        answer = Console.ReadLine()

        If (answer * 0.3) < player.XP Then

            player.XP -= player.XP * 0.1
            Console.Write(vbNewLine + (player.XP * 0.1).ToString + " XP deducted. You now have" + player.XP.ToString + vbNewLine)
            player.armor.Item(index).feetHP += answer
            player.HP = setHealthPoints(index)
            Console.Write(vbNewLine + "Upgraded " + player.armor.Item(index).feetName.ToString + " health to: " + answer.ToString + vbNewLine)

        Else

            Console.Write(vbNewLine + "Unable to upgrade " + player.armor.Item(index).feetName.ToString + " health " + vbNewLine)

        End If

    End Sub

    'store to buy pre made items
    Sub buyNewItems(index)

        Dim answer As String = ""
        Console.Write(vbNewLine + "Upgrade to legenary weapons and armor here:" + vbNewLine)
        Console.Write(vbNewLine + "Enter 1 for 'Legenary Sword with 100,000 Damage' " + vbNewLine)
        Console.Write(vbNewLine + "It Costs 1,000,000 XP" + vbNewLine)
        Console.Write(vbNewLine + "Enter 2 for 'Legenary Ring with 50,000 Damage' " + vbNewLine)
        Console.Write(vbNewLine + "It Costs 700,000 XP" + vbNewLine)
        Console.Write(vbNewLine + "Enter 3 for 'Legenary Helmet with 15,000 Health' " + vbNewLine)
        Console.Write(vbNewLine + "It Costs 1,000,000 XP" + vbNewLine)
        Console.Write(vbNewLine + "Enter 4 for 'Legenary Breastplate with 20,000 Health' " + vbNewLine)
        Console.Write(vbNewLine + "It Costs 1,500,000 XP" + vbNewLine)
        Console.Write(vbNewLine + "Enter 5 for 'Legenary Leggings with 10,000 Health' " + vbNewLine)
        Console.Write(vbNewLine + "It Costs 500,000 XP" + vbNewLine)
        Console.Write(vbNewLine + "Enter 6 for 'Legenary shooes with 5,000 Health' " + vbNewLine)
        Console.Write(vbNewLine + "It Costs 250,000 XP" + vbNewLine)

        answer = Console.ReadLine

        If answer = "1" And player.XP > 1000000 Then

            player.XP -= 1000000
            player.addSword("Legenary Reaper", 1000000, index)
            player.printSwordName(index)
            player.printSwordDamage(index)

        ElseIf answer = "2" And player.XP > 700000 Then

            player.XP -= 700000
            player.addRing("Legenary Ring", 700000, index)
            player.printRingName(index)
            player.printRingDamage(index
                                   )
        ElseIf answer = "3" And player.XP > 1000000 Then

            player.XP -= 1000000
            player.addHelmet("Legenary Helemt", 15000, index)
            player.printHelmetName(index)
            player.printHelmetHp(index)
        ElseIf answer = "4" And player.XP > 1500000 Then

            player.XP -= 1500000
            player.addBreastPlate("Legenary Breastplate", 20000, index)
            player.printBreastplateName(index)
            player.printBreastplatetHp(index)

        ElseIf answer = "5" And player.XP > 500000 Then

            player.XP -= 500000
            player.addLegs("", 10000, index)
            player.printLegName(index)
            player.printLegHp(index)

        ElseIf answer = "6" And player.XP > 250000 Then

            player.XP -= 250000
            player.addFeet("Legenary Shoes", 5000, index)

        Else
            Console.Write(vbNewLine + "Not a valid entry" + vbNewLine)
        End If


    End Sub
#End Region

#Region "Save and Recall Saved data"
    'returns save id as 1-index of items or in this deafult case 1-0
    Function generateSaveID()

        Dim saveid As String = ""
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder

        For i As Integer = 1 To 8

            Dim idx As Integer = r.Next(0, 35)

            sb.Append(s.Substring(idx, 1))

        Next

        saveid = sb.ToString

        Console.Write(vbNewLine + "Remeber this SaveID: " + saveid + vbNewLine)

        Return saveid

    End Function

    'save game with database
    Sub saveGame(index)

        Dim sc As SQLiteConnection = Database_SQLite.CreateConnection()

        InsertData(sc, index)

    End Sub
    'insert save data to Saves
    Sub InsertData(conn As SQLiteConnection, index As Integer)

        Dim Name As String = ""
        Dim XP As Integer = 0
        Dim saveID As String = ""
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
        Dim answer As String = ""
        Dim i As String = ""
        Dim savedID As String = ""
        Dim s As String = ""
        Dim q As String = "select saveID from SavedGame"
        Dim sqlr As SQLiteDataReader
        Dim sqlite_cmd As SQLiteCommand

        sqlite_cmd = conn.CreateCommand()
        sqlite_cmd.CommandText = q
        sqlr = sqlite_cmd.ExecuteReader()

        While sqlr.Read()

            savedID = sqlr("saveID")

        End While

        'set name, xp, saveID
        Name = player.Name
        XP = player.XP
        saveID = generateSaveID()

        'set sword name and damage
        swordName = player.weapons.Item(index).swordName.ToString
        swordDamage = player.weapons.Item(index).swordDamage.ToString

        'set ring name and damage
        ringName = player.weapons.Item(index).ringName.ToString
        ringDamage = player.weapons.Item(index).ringDamage.ToString

        'set helmet name and HP
        helmetName = player.armor.Item(index).helmetName.ToString
        helmetHealthPoints = player.armor.Item(index).helmentHP.ToString

        'set breastplate name and HP
        breastplateName = player.armor.Item(index).breastPlateName.ToString
        breastplateHealthPoints = player.armor.Item(index).breastPlateHP.ToString

        'set leg name and HP
        legName = player.armor.Item(index).legName.ToString
        legHealthPoints = player.armor.Item(index).legHP.ToString

        'set feet name and HP
        feetName = player.armor.Item(index).feetName.ToString
        feetHealthPoints = player.armor.Item(index).feetHP.ToString

        If savedID.ToString.Trim.ToLower = saveID.ToString.Trim.ToLower Then

            Database_SQLite.UpdateData_Movie(Name, XP, saveID, swordName, swordDamage, ringName, ringDamage, helmetName, helmetHealthPoints, breastplateName, breastplateHealthPoints, legName, legHealthPoints, feetName, feetHealthPoints, conn)
        Else
            Console.Write(Database_SQLite.InsertData(Name, XP, saveID, swordName, swordDamage, ringName, ringDamage, helmetName, helmetHealthPoints, breastplateName, breastplateHealthPoints, legName, legHealthPoints, feetName, feetHealthPoints, conn))
        End If
    End Sub

    Sub recallSaves(index)
        Dim sc As SQLiteConnection = Database_SQLite.CreateConnection()
        recallSavedGame(sc, index)

    End Sub

    Sub recallSavedGame(ByVal conn As SQLiteConnection, index As Integer)
        Dim s As String = ""
        Console.Write(vbNewLine + "Enter Save ID" + vbNewLine)
        Dim answer As String = Console.ReadLine().ToString
        Dim q As String
        q = String.Format("select Name, XP, saveID, swordName, swordDamage, ringName, ringDamage, helmetName, helmetHealthPoints, breastplateName, breastplateHealthPoints, legName, legHealthPoints, feetName, feetHealthPoints from SavedGame where saveID = '{0}'", answer)
        Dim Name As String = ""
        Dim XP As Integer = 0
        Dim saveID As String = ""
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

            'check if answer is save id if not exit to main

            If answer.ToString.Trim.ToLower = saveID.ToLower.Trim Then

                player.addSword(swordName.ToString, swordDamage, index)
                player.addRing(ringName.ToString, ringDamage, index)
                player.addHelmet(helmetName.ToString, helmetHealthPoints, index)
                player.addBreastPlate(breastplateName.ToString, breastplateHealthPoints, index)
                player.addLegs(legName.ToString, legHealthPoints, index)
                player.addFeet(feetName.ToString, feetHealthPoints, index)
                player.Name = Name.ToString
                player.HP = setHealthPoints(index)
                player.XP = XP

            Else

                Main()

            End If

        End While

    End Sub

#End Region

#Region "Set up Player and Monster"
    Sub setPlayerAndMonsterUp(index)

        'add player weapons and armor

        player.addSword("ReaperSythe", 1, index)
        player.addRing("RaixRing", 10, 0)
        player.addHelmet("Dracon Helmet", 5, 0)
        player.addBreastPlate("Dracon Breatplate", 10, 0)
        player.addLegs("Dracon Leggings", 5, 0)
        player.addFeet("Dracon Shoes", 1, 0)
        Console.Write(vbNewLine + "Enter a Name for your Avatar: " + vbNewLine)
        player.Name = Console.ReadLine().ToString.Trim
        player.HP = setHealthPoints(index)
        player.XP = 0
        'set monster up
        creature.setDamage(1)
        creature.setHealth(5)
        creature.setName("Draxx")
        creature.setXPGiven(100)
        setup = True

    End Sub

    Function setHealthPoints(index)

        Dim health As Integer

        health = player.armor.Item(index).helmentHP + player.armor.Item(index).breastPlateHP + player.armor.Item(index).legHP + player.armor.Item(index).feetHP

        Return health

    End Function
#End Region

#Region "Classes for Player and Monster"
    'Charchter Class also adds sword ring and armor
    Class Avatar

        Public Property Name As String
        Public Property HP As Integer
        Public Property XP As Integer
        Public Property weapons As List(Of Weapon) = New List(Of Weapon)
        Public Property armor As List(Of ArmorInputs) = New List(Of ArmorInputs)


        Sub addSword(name, damage, index)

            weapons.Add(New Weapon)
            weapons.Item(index).swordName = name
            weapons.Item(index).swordDamage = damage

        End Sub

        Sub addRing(name, damage, index)

            weapons.Add(New Weapon)
            weapons.Item(index).ringName = name
            weapons.Item(index).ringDamage = damage

        End Sub

        Sub addHelmet(name, hp, index)

            armor.Add(New ArmorInputs)
            armor.Item(index).helmetName = name
            armor.Item(index).helmentHP = hp

        End Sub

        Sub addBreastPlate(name, hp, index)

            armor.Add(New ArmorInputs)
            armor.Item(index).breastPlateName = name
            armor.Item(index).breastPlateHP = hp

        End Sub

        Sub addLegs(name, hp, index)

            armor.Add(New ArmorInputs)
            armor.Item(index).legName = name
            armor.Item(index).legHP = hp

        End Sub

        Sub addFeet(name, hp, index)

            armor.Add(New ArmorInputs)
            armor.Item(index).feetName = name
            armor.Item(index).feetHP = hp

        End Sub

        'print subs for ease of use and simplicty
        'print name and HP and XP of player
        Sub printPlayerName(index)

            Console.Write(vbNewLine + "your name is: " + player.Name.ToString + vbNewLine)

        End Sub

        Sub printPlayerHP(index)

            Console.Write(vbNewLine + "your health is: " + player.HP.ToString + vbNewLine)

        End Sub

        Sub printPlayerXP(index)

            Console.Write(vbNewLine + "your XP is: " + player.XP.ToString + vbNewLine)

        End Sub

        'print sword information
        Sub printSwordName(index)

            Console.Write(vbNewLine + player.Name.ToString + " weapon name is: " + player.weapons.Item(index).swordName.ToString + vbNewLine)

        End Sub

        Sub printSwordDamage(index)

            Console.Write(vbNewLine + player.Name.ToString + " weapon damage is: " + player.weapons.Item(index).swordDamage.ToString + vbNewLine)

        End Sub

        'print ring informartion
        Sub printRingName(index)

            Console.Write(vbNewLine + player.Name.ToString + " ring name is: " + player.weapons.Item(index).ringName.ToString + vbNewLine)

        End Sub

        Sub printRingDamage(index)

            Console.Write(vbNewLine + player.Name.ToString + " ring damage is: " + player.weapons.Item(index).ringDamage.ToString + vbNewLine)

        End Sub

        'print helmet information
        Sub printHelmetName(index)

            Console.Write(vbNewLine + player.Name.ToString + " helmet name is: " + player.armor.Item(index).helmetName.ToString + vbNewLine)

        End Sub

        Sub printHelmetHp(index)

            Console.Write(vbNewLine + player.Name.ToString + " helmet HP is: " + player.armor.Item(index).helmentHP.ToString + vbNewLine)

        End Sub

        'print breastplate information
        Sub printBreastplateName(index)

            Console.Write(vbNewLine + player.Name.ToString + " breastplate name is: " + player.armor.Item(index).breastPlateName.ToString + vbNewLine)

        End Sub

        Sub printBreastplatetHp(index)

            Console.Write(vbNewLine + player.Name.ToString + " breastplate HP is: " + player.armor.Item(index).breastPlateHP.ToString + vbNewLine)

        End Sub

        'print leg information
        Sub printLegName(index)

            Console.Write(vbNewLine + player.Name.ToString + " leg armor name is: " + player.armor.Item(index).legName.ToString + vbNewLine)

        End Sub

        Sub printLegHp(index)

            Console.Write(vbNewLine + player.Name.ToString + " leg armor HP is: " + player.armor.Item(index).legHP.ToString + vbNewLine)

        End Sub

        'print feet information
        Sub prinFeetName(index)

            Console.Write(vbNewLine + player.Name.ToString + " shoes name is: " + player.armor.Item(index).feetName.ToString + vbNewLine)

        End Sub

        Sub printFeetHp(index)

            Console.Write(vbNewLine + player.Name.ToString + " shoes HP is: " + player.armor.Item(index).feetHP.ToString + vbNewLine)

        End Sub

        'print loadout of all information for testing
        Sub printPlayerLoadout(index)

            player.printPlayerName(index)

            player.printPlayerHP(index)

            player.printPlayerXP(index)

            player.printSwordName(index)

            player.printSwordDamage(index)

            player.printRingName(index)

            player.printRingDamage(index)

            player.printHelmetName(index)

            player.printHelmetHp(index)

            player.printBreastplateName(index)

            player.printBreastplatetHp(index)

            player.printLegName(index)

            player.printLegHp(index)

            player.prinFeetName(index)

            player.printFeetHp(index)

        End Sub

    End Class

    'Weapon clas for List
    Public Class Weapon

        Public Property swordName As String
        Public Property swordDamage As Integer
        Public Property ringName As String
        Public Property ringDamage As Integer

    End Class

    'Armor class for List
    Public Class ArmorInputs

        Public Property helmetName As String
        Public Property helmentHP As Integer
        Public Property breastPlateName As String
        Public Property breastPlateHP As Integer
        Public Property legName As String
        Public Property legHP As Integer
        Public Property feetName As String
        Public Property feetHP As Integer

    End Class
    'monster class
    Class Monster

        Public Property name As String
        Public Property health As Integer
        Public Property damage As Integer
        Public Property givenXP As Integer

        Sub setDamage(damage)

            creature.damage = damage

        End Sub

        Sub setHealth(health)

            creature.health = health

        End Sub

        Sub setName(name)

            creature.name = name

        End Sub

        Sub setXPGiven(xp)

            creature.givenXP = xp

        End Sub

        Sub printMonsterName()

            Console.Write(vbNewLine + "Monsters name is: " + creature.name.ToString + vbNewLine)

        End Sub

        Sub printMonsterHealth()

            Console.Write(vbNewLine + "Monsters health is: " + creature.health.ToString + vbNewLine)

        End Sub

        Sub printMonsterDamage()

            Console.Write(vbNewLine + "Monsters attack is: " + creature.damage.ToString + vbNewLine)

        End Sub

        Sub printMonsterXP()

            Console.Write(vbNewLine + "XP when you beat monster: " + creature.givenXP.ToString + vbNewLine)

        End Sub

        Sub printMonsterLoadout()

            Console.Write(vbNewLine + "Monsters name is: " + creature.name.ToString + vbNewLine)
            Console.Write(vbNewLine + "Monsters health is: " + creature.health.ToString + vbNewLine)
            Console.Write(vbNewLine + "Monsters attack is: " + creature.damage.ToString + vbNewLine)
            Console.Write(vbNewLine + "XP when you beat monster: " + creature.givenXP.ToString + vbNewLine)

        End Sub

    End Class
#End Region

#Region "Sleep Function"
    'function for waiting 
    Sub Sleep(seconds As Integer)

        System.Threading.Thread.Sleep(seconds)

    End Sub

#End Region

End Module
