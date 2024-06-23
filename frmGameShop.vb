Public Class frmGameShop
    Dim colors As New ColorPalette
    Private Sub frmGameShop_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblTitle.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle.ForeColor = colors.primary
        lblTip1.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblTip1.ForeColor = colors.secondary
        'Import fonts and colors.
    End Sub
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        If frmGameMain.player.XP >= 50 Then
            picUpgrade1.Image = My.Resources.UPG__5_lives_C
        Else
            picUpgrade1.Image = My.Resources.UPG__5_lives_B
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
End Class