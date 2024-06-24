Imports System.IO

Public Class frmSettings
    Dim colors As New ColorPalette
    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblTitle.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle.ForeColor = colors.primary
        lblAutoFireTitle.Font = New Font(frmStart.fonts.Families(1), 14)
        lblAutoFireTitle.ForeColor = colors.primary
        lblAutoFireDesc.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblAutoFireDesc.ForeColor = colors.secondary
        lblTransparentTitle.Font = New Font(frmStart.fonts.Families(1), 14)
        lblTransparentTitle.ForeColor = colors.primary
        lblTransparentDesc.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblTransparentDesc.ForeColor = colors.secondary
        lblClearTitle.Font = New Font(frmStart.fonts.Families(1), 14)
        lblClearTitle.ForeColor = colors.primary
        lblClearDesc.Font = New Font(frmStart.fonts.Families(0), 8.25)
        lblClearDesc.ForeColor = colors.secondary
        'Import the colors and fonts.

        If frmGameMain.autoFire Then
            picAutoFireTgl.Image = My.Resources.TGL_true
        Else
            picAutoFireTgl.Image = My.Resources.TGL_false
        End If
        If frmGameMain.transparentBgr Then
            picTransparentTgl.Image = My.Resources.TGL_true
        Else
            picTransparentTgl.Image = My.Resources.TGL_false
        End If
        picClear.Image = My.Resources.BTN_clear_C
    End Sub
    Private Sub picAutoFireTgl_Click(sender As Object, e As EventArgs) Handles picAutoFireTgl.Click
        If frmGameMain.autoFire Then
            picAutoFireTgl.Image = My.Resources.TGL_false
            frmGameMain.autoFire = False
        Else
            picAutoFireTgl.Image = My.Resources.TGL_true
            frmGameMain.autoFire = True
        End If
    End Sub
    'Toggle auto fire on or off.
    Private Sub picTransparentTgl_Click(sender As Object, e As EventArgs) Handles picTransparentTgl.Click
        If frmGameMain.transparentBgr Then
            picTransparentTgl.Image = My.Resources.TGL_false
            frmGameMain.transparentBgr = False
        Else
            picTransparentTgl.Image = My.Resources.TGL_true
            frmGameMain.transparentBgr = True
        End If
    End Sub
    'Toggle the transparent background
    Private Sub picClear_Click(sender As Object, e As EventArgs) Handles picClear.Click
        picClear.Image = My.Resources.BTN_clear_B
        Dim bestStatsFileWrite As StreamWriter = File.CreateText("Data\bestStats.txt")
        'Open the text file to write.
        bestStatsFileWrite.Write("timeAlive: 0:00
shotsFired: 0
enemiesKilled: 0
bossesKilled: 0
livesLost: 0
xpCollected: 0
")
        bestStatsFileWrite.Close()
        'Update the text file.
    End Sub
    'Clear all the saved data.

End Class