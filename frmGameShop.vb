Public Class frmGameShop
    Dim colors As New ColorPalette
    Private Sub frmGameShop_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblTitle.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle.ForeColor = colors.primary
        lblTip1.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblTip1.ForeColor = colors.secondary
        lblXP.Font = New Font(frmStart.fonts.Families(2), 15.75, FontStyle.Bold)
        'Import fonts and colors.
    End Sub
    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
        Using pen As New SolidBrush(colors.purple)
            e.Graphics.FillEllipse(pen, New Rectangle(New Point(10, 13.5), New Size(12, 12)))
        End Using
    End Sub
    'Draw the XP dot.
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        lblXP.Text = frmGameMain.player.XP
        'Update the XP number.
        If frmGameMain.player.XP >= 50 Then
            picUpgrade1.Image = My.Resources.UPG__5_lives_C
        Else
            picUpgrade1.Image = My.Resources.UPG__5_lives_B
        End If
        If frmGameMain.player.XP >= 125 Then
            picUpgrade1.Image = My.Resources.UPG_full_lives_C
        Else
            picUpgrade1.Image = My.Resources.UPG_full_lives_B
        End If
    End Sub

    Private Sub frmGameShop_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        frmGameMain.toggleGame(False)
        Me.Hide()
        frmGameMain.Activate()
    End Sub

    Private Sub picUpgrade1_Click(sender As Object, e As EventArgs) Handles picUpgrade1.Click
        If frmGameMain.player.XP >= 50 Then
            frmGameMain.player.XP -= 50
            frmGameMain.player.maxHealth += 5
            frmGameMain.player.health += 5
            frmGameMain.lblHealth.Text = ($"{frmGameMain.player.health}/{frmGameMain.player.maxHealth}")
        End If
    End Sub
    'Add 5 lives.
    Private Sub picUpgrade2_Click(sender As Object, e As EventArgs) Handles picUpgrade2.Click
        If frmGameMain.player.XP >= 125 Then
            frmGameMain.player.XP -= 125
            frmGameMain.player.health = frmGameMain.player.maxHealth
            frmGameMain.lblHealth.Text = ($"{frmGameMain.player.health}/{frmGameMain.player.maxHealth}")
        End If
    End Sub
    'Restore all lives to full.


End Class