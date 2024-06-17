Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmStats
    Dim colors As New ColorPalette
    Dim bestStats As New Statistics
    'Class to store the best game statistics.

    Public stats As New Statistics
    'Class to store all of the current game statistics.
    Private Sub frmStats_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Restart()
        'Return to the home screen.
    End Sub

    Private Sub frmStats_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            Me.Close()
        End If
    End Sub

    Private Sub frmStats_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim updatedTxtFile As String = ""
        Dim bestStatsFileRead As StreamReader = File.OpenText("Data\bestStats.txt")
        'Open the text file to read.
        Do While bestStatsFileRead.Peek <> -1
            Dim line As String = bestStatsFileRead.ReadLine
            If line IsNot Nothing Then
                Dim name As String = Regex.Match(line, "([a-zA-Z]+)\:\s[0-9\:]+").Groups(1).Value
                Dim bestVal = Regex.Match(line, "[a-zA-Z]+\:\s([0-9\:]+)").Groups(1).Value
                Dim currentVal = CallByName(stats, name, CallType.Get)

                Debug.WriteLine($"name: {name}, bestVal: {bestVal}, currentVal: {currentVal}")
                Debug.WriteLine($"{CInt(Replace(bestVal, ":", ""))} > {CInt(Replace(currentVal, ":", ""))}")

                If CInt(Replace(bestVal, ":", "")) < CInt(Replace(currentVal, ":", "")) Then
                    CallByName(Me.bestStats, name, CallType.Set, currentVal.ToString)
                    updatedTxtFile = $"{updatedTxtFile}{name}: {currentVal}{vbNewLine}"
                Else
                    CallByName(Me.bestStats, name, CallType.Set, bestVal.ToString)
                    updatedTxtFile = $"{updatedTxtFile}{name}: {bestVal}{vbNewLine}"
                End If
            End If
        Loop
        'Set the best scores

        bestStatsFileRead.Close()
        Dim bestStatsFileWrite As StreamWriter = File.CreateText("Data\bestStats.txt")
        'Open the text file to write.
        bestStatsFileWrite.Write(updatedTxtFile)
        bestStatsFileWrite.Close()
        'Update the text file.

        lblTitle.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle.ForeColor = colors.primary
        lblTip1.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblTip1.ForeColor = colors.secondary

        Dim titleFont As New Font(frmStart.fonts.Families(1), 9.75)
        Dim valFont As New Font(frmStart.fonts.Families(2), 11, FontStyle.Bold)
        Dim bestFont As New Font(frmStart.fonts.Families(2), 8, FontStyle.Bold)

        statTitle1.Font = titleFont
        statTitle2.Font = titleFont
        statTitle3.Font = titleFont
        statTitle4.Font = titleFont
        statTitle5.Font = titleFont
        statTitle6.Font = titleFont
        statTitle1.ForeColor = colors.primary
        statTitle2.ForeColor = colors.primary
        statTitle3.ForeColor = colors.primary
        statTitle4.ForeColor = colors.primary
        statTitle5.ForeColor = colors.primary
        statTitle6.ForeColor = colors.primary
        statVal1.Font = valFont
        statVal2.Font = valFont
        statVal3.Font = valFont
        statVal4.Font = valFont
        statVal5.Font = valFont
        statVal6.Font = valFont
        statVal1.ForeColor = colors.primary
        statVal2.ForeColor = colors.primary
        statVal3.ForeColor = colors.primary
        statVal4.ForeColor = colors.primary
        statVal5.ForeColor = colors.primary
        statVal6.ForeColor = colors.primary
        statBest1.Font = bestFont
        statBest2.Font = bestFont
        statBest3.Font = bestFont
        statBest4.Font = bestFont
        statBest5.Font = bestFont
        statBest6.Font = bestFont
        statBest1.ForeColor = colors.tertiary
        statBest2.ForeColor = colors.tertiary
        statBest3.ForeColor = colors.tertiary
        statBest4.ForeColor = colors.tertiary
        statBest5.ForeColor = colors.tertiary
        statBest6.ForeColor = colors.tertiary
        'Set all of the correct fonts and colors.

        statVal1.Text = stats.timeAlive
        statVal2.Text = stats.shotsFired
        statVal3.Text = stats.enemiesKilled
        statVal4.Text = stats.bossesKilled
        statVal5.Text = stats.livesLost
        statVal6.Text = stats.xpCollected
        statBest1.Text = bestStats.timeAlive
        statBest2.Text = bestStats.shotsFired
        statBest3.Text = bestStats.enemiesKilled
        statBest4.Text = bestStats.bossesKilled
        statBest5.Text = bestStats.livesLost
        statBest6.Text = bestStats.xpCollected
        'Set all the correct values.


    End Sub
End Class