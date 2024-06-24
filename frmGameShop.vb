Imports System.ComponentModel

Public Class frmGameShop
    Dim XP As Integer
    'The amount of XP the player has.
    Dim colors As New ColorPalette
    Dim prices As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"+5 lives", 50},
        {"full health", 125},
        {"speed", 30},
        {"piercing", 60},
        {"shrink", 45},
        {"shot interval", 75}
    }
    Dim available As Dictionary(Of String, Boolean) = New Dictionary(Of String, Boolean) From {
        {"+5 lives", True},
        {"full health", True},
        {"speed", True},
        {"piercing", True},
        {"shrink", True},
        {"shot interval", True}
    }
    'The prices of all the upgrades.
    Private Sub frmGameShop_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblTitle.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle.ForeColor = colors.primary
        lblTip1.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblTip1.ForeColor = colors.secondary
        lblXP.Font = New Font(frmStart.fonts.Families(2), 15.75, FontStyle.Bold)
        'Import fonts and colors.
        tmrTick.Enabled = True
        XP = frmGameMain.player.XP
    End Sub
    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
        Using pen As New SolidBrush(colors.purple)
            e.Graphics.FillEllipse(pen, New Rectangle(New Point(10, 13.5), New Size(12, 12)))
        End Using
    End Sub
    'Draw the XP dot.
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        XP = frmGameMain.player.XP
        lblXP.Text = frmGameMain.player.XP
        'Update the XP number.
        If XP >= prices("+5 lives") And available("+5 lives") Then
            picUpgrade1.Image = My.Resources.UPG__5_lives_C
        Else
            picUpgrade1.Image = My.Resources.UPG__5_lives_B
        End If
        If XP >= prices("full health") And available("full health") Then
            picUpgrade2.Image = My.Resources.UPG_full_lives_C
        Else
            picUpgrade2.Image = My.Resources.UPG_full_lives_B
        End If
        If XP >= prices("speed") And available("full health") Then
            picUpgrade3.Image = My.Resources.UPG_speed_1_C
        Else
            picUpgrade3.Image = My.Resources.UPG_speed_1_B
        End If
        If XP >= prices("piercing") And available("piercing") Then
            picUpgrade4.Image = My.Resources.UPG_piercing_C
        Else
            picUpgrade4.Image = My.Resources.UPG_piercing_B
        End If
        If XP >= prices("shot interval") And available("shot interval") Then
            picUpgrade5.Image = My.Resources.UPG_shot_interval_C
        Else
            picUpgrade5.Image = My.Resources.UPG_shot_interval_B
        End If
        If XP >= prices("shrink") And available("shrink") Then
            picUpgrade6.Image = My.Resources.UPG_shrink_C
        Else
            picUpgrade6.Image = My.Resources.UPG_shrink_B
        End If
        'Show whether the upgrade is available or not.
    End Sub

    Private Sub frmGameShop_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            frmGameMain.lblXP.Text = frmGameMain.player.XP
            frmGameMain.toggleGame(False)
            Me.Hide()
            frmGameMain.Activate()
        End If
    End Sub
    Private Sub frmGameShop_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        frmGameMain.lblXP.Text = frmGameMain.player.XP

        frmGameMain.toggleGame(False)
        Me.Hide()
        frmGameMain.Activate()
    End Sub
    'Resume the game and close the shop.

    Private Sub picUpgrade1_Click(sender As Object, e As EventArgs) Handles picUpgrade1.Click
        If XP >= prices("+5 lives") And available("+5 lives") Then
            frmGameMain.player.XP -= prices("+5 lives")
            frmGameMain.player.maxHealth += 5
            frmGameMain.player.health += 5
            frmGameMain.lblHealth.Text = ($"{frmGameMain.player.health}/{frmGameMain.player.maxHealth}")
        End If
    End Sub
    'Add 5 lives.
    Private Sub picUpgrade2_Click(sender As Object, e As EventArgs) Handles picUpgrade2.Click
        If XP >= prices("full health") And available("full health") Then
            frmGameMain.player.XP -= prices("full health")
            frmGameMain.player.health = frmGameMain.player.maxHealth
            frmGameMain.lblHealth.Text = ($"{frmGameMain.player.health}/{frmGameMain.player.maxHealth}")
        End If
    End Sub
    'Restore all lives to full.
    Private Sub picUpgrade3_Click(sender As Object, e As EventArgs) Handles picUpgrade3.Click
        If XP >= prices("speed") And available("speed") Then
            frmGameMain.player.XP -= prices("speed")
            frmGameMain.playerSpeed += 0.2
        End If
    End Sub
    'Increase the players speed.
    Private Sub picUpgrade4_Click(sender As Object, e As EventArgs) Handles picUpgrade4.Click
        If XP >= prices("piercing") And available("piercing") Then
            frmGameMain.player.XP -= prices("piercing")
            available("piercing") = False
            frmGameMain.piercing = True
        End If
    End Sub
    'Allow the shots to go through the enemies.
    Private Sub picUpgrade5_Click(sender As Object, e As EventArgs) Handles picUpgrade5.Click
        If XP >= prices("shot interval") And available("shot interval") Then
            frmGameMain.tmrShot.Interval -= 250
            frmGameMain.player.XP -= prices("shot interval")
            If frmGameMain.tmrShot.Interval <= 200 Then
                available("shot interval") = False
            End If
            'Stop the timer interval from being less than 200.
        End If
    End Sub
    'Decrease the shot interval.
    Private Sub picUpgrade6_Click(sender As Object, e As EventArgs) Handles picUpgrade6.Click
        If XP >= prices("shrink") And available("shrink") Then
            frmGameMain.player.XP -= prices("shrink")
            frmGameMain.player.size -= 3
            If frmGameMain.player.size <= 8 Then
                available("shrink") = False
            End If
        End If
    End Sub
    'Shrink the player.
End Class