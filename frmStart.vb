Imports System.Drawing.Text

Public Class frmStart
    Public fonts As New PrivateFontCollection()
    'Collection to store local fonts.

    Public selectedScreen As Screen
    'The main screen to show the game on.

    Dim currentMedia As String
    Dim counter As Integer = 0
    Private Sub frmStart_Load(sender As Object, e As EventArgs) Handles Me.Load
        selectedScreen = Screen.AllScreens(0)
        'Set the screen as the primary screen.

        Me.Location = New Point(selectedScreen.WorkingArea.Width / 2 - 335, selectedScreen.WorkingArea.Height / 2 - 244.5)
        frmGameMain.Location = New Point(Me.Location.X + 148.5, Me.Location.Y + 64)
        'Set the locations of the forms

        currentMedia = "IN"
        mediaIN.uiMode = "none"
        mediaIN.URL = "Animations/TitleScreen_IN.mp4"
        mediaOUT.uiMode = "none"
        mediaOUT.URL = "Animations/TitleScreen_OUT.mp4"
        'Set default values for media players.

        fonts.AddFontFile("Fonts/Pressario.ttf") 'Title [1]
        fonts.AddFontFile("Fonts/BoldenaBold.ttf") 'Numbers [2]
        fonts.AddFontFile("Fonts/VarelaRound.ttf") 'Text [0]
        'Import local fonts.
    End Sub
    'show the startup animation.

    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        If currentMedia = "IN" Then
            mediaOUT.Ctlcontrols.pause()
        End If
        If mediaIN.Ctlcontrols.currentPosition > 1.8 And currentMedia = "IN" Then
            mediaOUT.Ctlcontrols.pause()
            mediaIN.Ctlcontrols.pause()
            counter = If(counter < 8, counter + 1, 0)
            counter += 1
        End If
        If mediaOUT.Ctlcontrols.currentPosition > 5.3 And currentMedia = "OUT" Then
            mediaOUT.Ctlcontrols.pause()
            frmGameMain.Show()
            Me.Hide()
        End If
        If mediaOUT.Ctlcontrols.currentPosition > 2.7 And currentMedia = "OUT" And tmrFade.Enabled = False Then
            tmrFade.Enabled = True
            Me.TopMost = True
            frmGameBackground.Opacity = 0
            frmGameBackground.Show()
        End If
        Select Case counter
            Case 2
                picBTNplay.Visible = True
            Case 4
                picBTNsettings.Visible = True
            Case 6
                picBTNexit.Visible = True
        End Select
        'Offset the changing the visibility of the buttons to avoid flickering.
    End Sub

    Private Sub tmrFade_Tick(sender As Object, e As EventArgs) Handles tmrFade.Tick
        If frmGameBackground.Opacity < 0.73 Then
            frmGameBackground.Opacity += 0.01
        End If
    End Sub

    Private Sub picBTNplay_Click(sender As Object, e As EventArgs) Handles picBTNplay.Click
        currentMedia = "OUT"
        picBTNplay.Visible = False
        picBTNsettings.Visible = False
        picBTNexit.Visible = False
        mediaOUT.Visible = True
        mediaIN.Visible = False
        mediaOUT.Ctlcontrols.play()
        'play the out animation
    End Sub
    'Start a new game

    Private Sub picBTNexit_Click(sender As Object, e As EventArgs) Handles picBTNexit.Click
        End
    End Sub
    'Close/end the application.

End Class