Public Class frmStart
    Dim currentMedia As String
    Dim counter As Integer = 0
    Private Sub frmStart_Load(sender As Object, e As EventArgs) Handles Me.Load
        frmGameMain.Location = New Point(Me.Location.X + 148.5, Me.Location.Y + 64)

        currentMedia = "IN"
        mediaIN.uiMode = "none"
        mediaIN.URL = "TitleScreen_IN.mp4"
        mediaOUT.uiMode = "none"
        mediaOUT.URL = "TitleScreen_OUT.mp4"
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
        Select Case counter
            Case 2
                picBTNplay.Visible = True
            Case 4
                picBTNsettings.Visible = True
            Case 6
                picBTNexit.Visible = True
        End Select
        'offset the change visibilty of the buttons to avoid flickering.
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