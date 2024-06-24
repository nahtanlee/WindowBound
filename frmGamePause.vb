Imports System.ComponentModel

Public Class frmGamePause
    Dim colors As New ColorPalette
    Private Sub frmGamePause_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblTitle1.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle1.ForeColor = colors.primary
        lblTitle2.Font = New Font(frmStart.fonts.Families(1), 26.25)
        lblTitle2.ForeColor = colors.primary
        'Import fonts and colors.
    End Sub

    Private Sub frmGamePause_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        frmGameMain.Activate()
        frmGameMain.toggleGame(False)
    End Sub
    'Resume the game.
    Private Sub picBTNresume_Click(sender As Object, e As EventArgs) Handles picBTNresume.Click
        frmGameMain.toggleGame(False)
        Me.Hide()
        frmGameMain.Activate()
    End Sub
    'Hide the form and resume the game.

    Private Sub picBTNhome_Click(sender As Object, e As EventArgs) Handles picBTNhome.Click
        Application.Restart()
    End Sub
    'Go back home and end the game.
    Private Sub frmGamePause_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            frmGameMain.toggleGame(False)
            Me.Hide()
            frmGameMain.Activate()
        End If
    End Sub

End Class