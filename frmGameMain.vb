Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Globalization
Imports System.Security.Authentication.ExtendedProtection


Public Class frmGameMain
    '---------------------------------------------------------------------------- VARIABLES ----------------------------------------------------------------------------
    Dim WithEvents formMainBackground As New frmGameBackground

    Public player As New Player
    'A class to store all of the player information.
    Public enemies() As Enemy
    'An array of the class enemy to store all the enemy information.
    Public XPs() As XP
    'An array of the class XP to store all of the XP dot information.
    Dim colors As New ColorPalette
    Dim toolTips As New ToolTips
    'A class to store the tooltips to show.


    Dim pressedKeys As New PressedKeys
    'A class that stores whether the arrow or mouse buttons are pressed.
    Dim storedExtend As New StoredExtend
    'A class that stores the value that each side of the window needs to be extended by.
    Dim tickCount As Integer = 0
    'Count the number of ticks.
    Public startTime As Date
    'The time the game was started (used to calculate stats.timeAlive)
    Dim shotStore As Boolean = False
    'Stores whether or not there is a shot available.
    Dim playerRadius As Integer = 80
    'How close the player has to be to the dropped XP to pick it up.
    Public piercing As Boolean = False
    'Whether or not the shots pass through the enemy.
    Public autoFire As Boolean = True
    'Whether auto fire is on.

    Dim gameBossForms() As frmGameBoss
    'The window to show to the boss in.


    Public playerSpeed As Integer = 2.9
    Dim objectSpeeds As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"shot", 14},
        {"extraShot", 23},
        {"circle", 2.4},
        {"square", 2},
        {"triangle", 2.75}
    }
    'Initialize a dictionary that stores {object type, speed}.
    Dim objectMaxHealth As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"player", 10},
        {"circle", 1},
        {"square", 2},
        {"triangle", 4}
    }
    'Initialize a dictionary that stores {object type, max health}.
    Dim objectSizes As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"player", 14},
        {"shot", 10},
        {"extraShot", 17},
        {"circle", 16},
        {"square", 18},
        {"triangle", 20}
    }
    'Initialize a dictionary with the size of each type of object.
    Dim objectXPs As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"circle", 2},
        {"square", 5},
        {"triangle", 8}
    }
    'Initialize a dictionary with the amount of XP that each enemy drops when killed.

    Public shots() As Tuple(Of Point, Point, Point, Integer)
    'An array of tuples that stores the [1] current location, [2] end destination, [3] the calculated movement and [4] the size of the shots.

    Protected Overrides Sub WndProc(ByRef m As Message)
        If (m.Msg = &H112) AndAlso (m.WParam.ToInt32() = &HF010) Then
            Return
        End If
        If (m.Msg = &HA1) AndAlso (m.WParam.ToInt32() = &H2) Then
            Return
        End If
        MyBase.WndProc(m)
    End Sub
    'Disable window dragging.

    '------------------------------------------------------------------------------- EVENTS -------------------------------------------------------------------------------
    Private Sub frmGameMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        formMainBackground.Show()
        frmGameBackground.Hide()
        'Switch the background forms.

        lblHealth.Font = New Font(frmStart.fonts.Families(2), 15.75, FontStyle.Bold)
        lblToolTip.Font = New Font(frmStart.fonts.Families(0), 9, FontStyle.Regular)
        lblToolTip.ForeColor = colors.tertiary
        lblXP.Font = New Font(frmStart.fonts.Families(2), 15.75, FontStyle.Bold)
        'Import fonts and colors.

        player.loc = {PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2)))}
        player.size = objectSizes("player")
        player.maxHealth = 10
        player.health = 10
        'Set default values for variables in the Player class.

        Randomize()
        'Initialize the random generator.

        tmrTick.Enabled = True
        'Only start the tick once all variables have been initialized to prevent null errors from occurring.
        tmrShot.Enabled = True
    End Sub
    'On form load.
    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
        If shots IsNot Nothing Then
            For i As Integer = 0 To (shots.Length - 1)
                Dim shot = shots(i)
                shot = New Tuple(Of Point, Point, Point, Integer)(New Point(shot.Item1.X + shot.Item3.X, shot.Item1.Y + shot.Item3.Y), shot.Item2, shot.Item3, shot.Item4)
                Using pen As New SolidBrush(colors.primary)
                    e.Graphics.FillEllipse(pen, New Rectangle(PointToClient(shot.Item1).X, PointToClient(shot.Item1).Y, shot.Item4, shot.Item4))
                End Using
                shots(i) = shot
            Next
            'Draw each shot and update its position.
        End If
        'Draw the shots.

        If gameBossForms IsNot Nothing Then
            For Each boss In gameBossForms
                If boss.shots IsNot Nothing Then
                    For i As Integer = 0 To (boss.shots.Length - 1)
                        Dim shot = boss.shots(i)
                        Using pen As New Pen(colors.red, 2)
                            e.Graphics.DrawEllipse(pen, New Rectangle(PointToClient(shot.Item1).X, PointToClient(shot.Item1).Y, shot.Item4, shot.Item4))
                        End Using
                    Next
                    'Draw each shot and update its position.
                End If
                'Draw the shots from the boss.

                Using pen As New Pen(colors.red, 20)
                    e.Graphics.DrawPolygon(pen, {PointToClient(boss.bossPoints(1)), PointToClient(boss.bossPoints(2)), PointToClient(boss.bossPoints(3)), PointToClient(boss.bossPoints(4)), PointToClient(boss.bossPoints(5)), PointToClient(boss.bossPoints(6)), PointToClient(boss.bossPoints(7)), PointToClient(boss.bossPoints(8))})
                End Using
                Using brush As New SolidBrush(colors.background)
                    e.Graphics.FillPolygon(brush, {PointToClient(boss.bossPoints(1)), PointToClient(boss.bossPoints(2)), PointToClient(boss.bossPoints(3)), PointToClient(boss.bossPoints(4)), PointToClient(boss.bossPoints(5)), PointToClient(boss.bossPoints(6)), PointToClient(boss.bossPoints(7)), PointToClient(boss.bossPoints(8))})
                End Using
                'Draw the octagon boss.
            Next
        End If
        'Draw the elements from the boss forms.

        If enemies IsNot Nothing Then
            For i As Integer = 0 To (enemies.Length - 1)
                Dim pen As Pen
                enemies(i).loc = New Point(enemies(i).loc.X + enemies(i).mov.X, enemies(i).loc.Y + enemies(i).mov.Y)
                'Update the position of the enemy

                Select Case enemies(i).type
                    Case "square"
                        pen = New Pen(If(enemies(i).white > 0, colors.secondary, colors.blue), 7)
                        e.Graphics.DrawRectangle(pen, New Rectangle(PointToClient(enemies(i).loc), New Size(enemies(i).size, enemies(i).size)))
                        'Draw a blue square.
                    Case "circle"
                        pen = New Pen(If(enemies(i).white > 0, colors.secondary, colors.green), 5)
                        e.Graphics.DrawEllipse(pen, New Rectangle(PointToClient(enemies(i).loc), New Size(enemies(i).size, enemies(i).size)))
                        'Draw a green circle.
                    Case "triangle"
                        pen = New Pen(If(enemies(i).white > 0, colors.secondary, colors.yellow), 7)
                        e.Graphics.DrawPolygon(pen, {New Point(PointToClient(enemies(i).loc).X + (enemies(i).size / 2), PointToClient(enemies(i).loc).Y), New Point(PointToClient(enemies(i).loc).X, PointToClient(enemies(i).loc).Y + enemies(i).size), New Point(PointToClient(enemies(i).loc).X + enemies(i).size, PointToClient(enemies(i).loc).Y + enemies(i).size)})
                End Select

                calcMoveObject(enemies(i).type, i)
            Next
            'Draw each moving enemy and update and recalculate its position.
        End If
        'Draw the moving enemies and update properties.

        Using pen As New SolidBrush(colors.purple)
            e.Graphics.FillEllipse(pen, New Rectangle(New Point(10, 13.5), New Size(12, 12)))
        End Using
        If XPs IsNot Nothing Then
            For i As Integer = 0 To (XPs.Length - 1)
                XPs(i).loc = New Point(XPs(i).loc.X + XPs(i).mov.X, XPs(i).loc.Y + XPs(i).mov.Y)
                Using pen As New SolidBrush(colors.purple)
                    e.Graphics.FillEllipse(pen, New Rectangle(PointToClient(XPs(i).loc), New Size(XPs(i).size, XPs(i).size)))
                End Using
                'Draw the dot.
                If XPs(i).mov <> New Point(0, 0) Then
                    If XPs(i).speed < 12 Then
                        XPs(i).speed += 0.8
                    End If
                    XPs(i).mov = calcMovePoint(XPs(i).loc, player.loc(9), XPs(i).speed)
                End If
                'Update its movement, slowly increasing the speed.
            Next
        End If
        'Draw the XP dots and update its position.

        If player.red > 0 Then
            Using pen As New Pen(colors.red, 14)
                e.Graphics.DrawEllipse(pen, CInt(PointToClient(player.loc(9)).X - player.size / 2), CInt(PointToClient(player.loc(9)).Y - (player.size / 2)), player.size, player.size)
            End Using
        Else
            Using pen As New Pen(colors.primary, 14)
                e.Graphics.DrawEllipse(pen, CInt(PointToClient(player.loc(9)).X - player.size / 2), CInt(PointToClient(player.loc(9)).Y - (player.size / 2)), player.size, player.size)
            End Using
        End If
        Using brush As New SolidBrush(colors.background)
            e.Graphics.FillRectangle(brush, New Rectangle(PointToClient(New Point(player.loc(9).X - (player.size / 2) + (player.size / 20), player.loc(9).Y - (player.size / 2) + (player.size / 20))), New Size(player.size - (player.size / 10), player.size - (player.size / 10))))
        End Using
        'Draw the player circle (circle + square).
    End Sub
    'Repaint all object on the canvas.
    Private Sub frmGameMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        toggleGame(True)
        frmGamePause.ShowDialog()
        e.Cancel = True
    End Sub
    'Do not allow the player to close the form but instead pause the game.
    Private Sub frmGameMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        lblToolTip.Left = (Me.Width / 2) - (lblToolTip.Width / 2)
    End Sub
    'Anchor lblToolTip to the bottom center of the window.

    '---- TIMERS ----
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        tickCount += 1
        Select Case tickCount
            Case 1
                startTime = DateAndTime.Now
                'Set the start time.
            Case 5
                storedExtend.top = 10
                storedExtend.bottom = 10
                storedExtend.left = 10
                storedExtend.right = 10
                tmrShrink.Enabled = True
                'Delay the initial window expand animation and start the shrinking of the window.
            Case 50
                addObject("circle")
                tmrCircleE.Enabled = True
                'Start generating circle enemies.
            Case 600
                Debug.WriteLine("500 tick")
                addObject("square")
                tmrSquareE.Enabled = True
                'Start generating square enemies.
            Case 2000
                Debug.WriteLine("1000 tick")
                addObject("triangle")
                tmrTriE.Enabled = True
                'Start generating triangle enemies.
            Case 10000
                Debug.WriteLine("2500 tick")
                addObject("boss")
                tmrBoss.Enabled = True
                'Start generating bosses in a new window.
            Case > 4000
                If tickCount Mod 200 = 0 Then
                    If tmrCircleE.Interval > 200 Then
                        tmrCircleE.Interval -= 100
                    End If
                    If tmrSquareE.Interval > 200 Then
                        tmrSquareE.Interval -= 100
                    End If
                    If tmrTriE.Interval > 200 Then
                        tmrTriE.Interval -= 100
                    End If
                End If
                'Increase the spawning rate of the enemies.
        End Select
        'Delay actions.
        If tickCount > 200 Then
            If toolTips.moveShow Then
                lblToolTip.Text = toolTips.moveText
                lblToolTip.Left = (Me.Width / 2) - (lblToolTip.Width / 2)
                lblToolTip.Visible = True
            ElseIf toolTips.moveDelay = 0 Then
                lblToolTip.Visible = False
                toolTips.moveDelay = tickCount
            End If
            If Not toolTips.moveShow And toolTips.shotShow And tickCount > (toolTips.moveDelay + 50) Then
                lblToolTip.Text = toolTips.shotText
                lblToolTip.Left = (Me.Width / 2) - (lblToolTip.Width / 2)
                lblToolTip.Visible = True
            ElseIf toolTips.shotDelay = 0 And Not toolTips.shotShow Then
                lblToolTip.Visible = False
                toolTips.shotDelay = tickCount
            End If
        End If
        'Show tooltips progressively if needed.

        If (tickCount Mod 1) = 0 Then
            picCanvas.Invalidate()
        End If
        'Redraw the canvas every tick.

        If player.red > 0 Then
            player.red -= 1
        End If
        If enemies IsNot Nothing Then
            For i As Integer = 0 To (enemies.Length - 1)
                If enemies(i).white > 0 Then
                    enemies(i).white -= 1
                End If
            Next
        End If
        'Lower the number of red/white frames for the player and enemies by 1.

        If pressedKeys.up = True And player.loc(9).Y > Me.Location.Y + 50 Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y - playerSpeed))
            toolTips.moveShow = False
        End If
        If pressedKeys.down = True And player.loc(9).Y < Me.Location.Y + Me.Height - 30 Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y + playerSpeed))
            toolTips.moveShow = False
        End If
        If pressedKeys.left = True And player.loc(9).X > Me.Location.X + 30 Then
            updatePlayerLoc(New Point(player.loc(9).X - playerSpeed, player.loc(9).Y))
            toolTips.moveShow = False
        End If
        If pressedKeys.right = True And player.loc(9).X < Me.Location.X + Me.Width - 32 Then
            updatePlayerLoc(New Point(player.loc(9).X + playerSpeed, player.loc(9).Y))
            toolTips.moveShow = False
        End If
        If Not pressedKeys.up And Not pressedKeys.down And Not pressedKeys.left And Not pressedKeys.right Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y))
        End If
        'Move the player if the arrow keys are currently being pressed, make sure the player does not leave the window and hide the tooltip.

        If shots IsNot Nothing Then
            checkWindowHit()
            If enemies IsNot Nothing Then
                checkEnemyHits()
            End If
        End If
        If enemies IsNot Nothing Then
            checkPlayerCollisions()
        End If
        If XPs IsNot Nothing Then
            checkXPProx()
            checkXPCollisions()
        End If
        'Check collisions between the window, shots, enemies, player and XP.

        checkPlayerBounds()
        'Ensure that the player doesn't leave the window.
        extendSides()
        'Resize the window accordingly.
    End Sub
    'Force redraw of all shapes and players and move the player accordingly.
    Private Sub tmrShot_Tick(sender As Object, e As EventArgs) Handles tmrShot.Tick
        If pressedKeys.mouseLeft Then
            addObject("shot")
            frmStats.stats.shotsFired += 1
        Else
            shotStore = True
        End If
        'Add a shot if the left mouse button is being pressed, else add it to the store.
    End Sub
    'Add a new shot.
    Private Sub tmrSquareE_Tick(sender As Object, e As EventArgs) Handles tmrSquareE.Tick
        If Rnd() > 0.9 Then
            addObject("square")
        End If
        'Add square enemies randomly
    End Sub
    'Add a new square enemy.
    Private Sub tmrCircleE_Tick(sender As Object, e As EventArgs) Handles tmrCircleE.Tick
        If Rnd() > 0.9 Then
            addObject("circle")
        End If
        'Add circle enemies randomly
    End Sub
    'Add a new circle enemy.
    Private Sub tmrTriE_Tick(sender As Object, e As EventArgs) Handles tmrTriE.Tick
        If Rnd() > 0.93 Then
            addObject("triangle")
        End If
        'Add triangle enemies randomly
    End Sub
    'Add a new triangle enemy.
    Private Sub tmrBoss_Tick(sender As Object, e As EventArgs) Handles tmrBoss.Tick
        If Rnd() > 0.3 And gameBossForms Is Nothing Then
            addObject("boss")
        End If
        'Add bosses randomly
    End Sub
    'Add a new boss.
    Private Sub tmrShrink_Tick(sender As Object, e As EventArgs) Handles tmrShrink.Tick
        If Me.Width > 200 Then
            Me.Width -= 2
            Me.Left += 1
        End If
        If Me.Height > 200 Then
            Me.Height -= 2
            Me.Top += 1
        End If
        If (player.loc(9).Y - player.size) < Me.Location.Y + 40 Then
            player.loc(9).Y += 1
        End If
        If player.loc(9).Y > Me.Location.Y + Me.Height - 30 Then
            player.loc(9).Y -= 1
        End If
        If player.loc(9).X < Me.Location.X + 24 Then
            player.loc(9).X += 1
        End If
        If player.loc(9).X > Me.Location.X + Me.Width - 33 Then
            player.loc(9).X -= 1
        End If
        'Push the player if it is at the edge of the window.
    End Sub
    'Shrink the window.

    '---- MOUSE & KEYS ----
    Private Sub frmGameMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown, formMainBackground.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.W
                pressedKeys.up = True
            Case Keys.Down, Keys.S
                pressedKeys.down = True
            Case Keys.Left, Keys.A
                pressedKeys.left = True
            Case Keys.Right, Keys.D
                pressedKeys.right = True
            Case Keys.Space 'Open shop.
                toggleGame(True)
                toolTips.XPShow = False
                frmGameShop.ShowDialog()
            Case Keys.Escape 'Pause the game
                toggleGame(True)
                frmGamePause.ShowDialog()
        End Select
    End Sub
    'Update the correct variables when a key is pressed.
    Private Sub frmGameMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp, formMainBackground.KeyUp
        Select Case e.KeyCode
            Case Keys.Up, Keys.W
                pressedKeys.up = False
            Case Keys.Down, Keys.S
                pressedKeys.down = False
            Case Keys.Left, Keys.A
                pressedKeys.left = False
            Case Keys.Right, Keys.D
                pressedKeys.right = False
        End Select
    End Sub
    'Update the correct variables when a key is released.
    Public Sub picCanvas_MouseDown(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseDown, formMainBackground.MouseDown
        If e.Button = MouseButtons.Left Then
            If autoFire Then
                pressedKeys.mouseLeft = True
            End If
            If shotStore Then
                    addObject("shot")
                    frmStats.stats.shotsFired += 1
                    tmrShot.Enabled = False
                    tmrShot.Enabled = True
                    'Reset the timer to ensure that the interval starts at 0 again.
                    shotStore = False
                End If
            ElseIf e.Button = MouseButtons.Right Then
                pressedKeys.mouseRight = True
        End If
    End Sub
    'Update the correct variables when a mouse button is pressed.
    Private Sub picCanvas_MouseUp(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseUp, formMainBackground.MouseUp
        If e.Button = MouseButtons.Left Then
            If autoFire Then
                pressedKeys.mouseLeft = False
            End If
        ElseIf e.Button = MouseButtons.Right Then
                pressedKeys.mouseRight = False
        End If
    End Sub
    'Update the correct variables when a mouse button is released.

    '----------------------------------------------------------------------------- FUNCTIONS -----------------------------------------------------------------------------
    ''' <summary>
    ''' This function calculates the movement in x and y coordinates that the object should move by each tick.
    ''' This is done by drawing a triangle between the <c>startPoint</c> and <c>endPoint</c> and calculating the angle between these points using trigonometry. 
    ''' This angle is then used to find the x and y movements which are assigned to <c>move.X</c> and <c>move.Y</c> respectively.
    ''' </summary>
    ''' <param name="type">is the type of object that the movement is being calculated of</param>
    ''' <param name="index">is the index of the object within its array</param>
    ''' <returns></returns>
    Public Function calcMoveObject(type As String, index As Integer) As Point
        Dim startPoint As Point
        Dim endPoint As Point
        Dim move As Point
        Dim shotSize As Integer
        Dim speed As Integer

        If type = "shot" Or type = "extraShot" Then
            startPoint = shots(index).Item1
            endPoint = shots(index).Item2
            shotSize = shots(index).Item4
            Try
                speed = objectSpeeds(type)
            Catch err As System.Collections.Generic.KeyNotFoundException
                Console.WriteLine($"Type not found as a key in objectSpeeds: '{type}', default automatically set...")
                speed = 10
            End Try
            'Catch errors when finding the speed of an object.
        Else
            speed = enemies(index).speed
            startPoint = enemies(index).loc
            endPoint = New Point(player.loc(0).X, player.loc(0).Y)
        End If



        Dim b As Integer = endPoint.Y - startPoint.Y
        Dim c As Integer = endPoint.X - startPoint.X

        Dim radians As Double = Math.Atan(Math.Abs(c / b))
        Dim degrees As Double = radians * (180 / Math.PI)
        'the degrees from the closest axis (rounded down/clockwise)

        Dim quadrant As Integer
        'Quadrant 1..<5
        If c <= 0 And b <= 0 Then
            quadrant = 1
            degrees += 270
        ElseIf c >= 0 And b <= 0 Then
            quadrant = 2
            degrees += 0
        ElseIf c >= 0 And b >= 0 Then
            quadrant = 3
            degrees += 90
        ElseIf c <= 0 And b >= 0 Then
            quadrant = 4
            degrees += 180
        End If
        'Calculate the quadrant that the endPoint lies in relative to the startPoint and add the corresponding degrees.
        'If b < 0 then the endPoint is above the startPoint and if c < 0 then the endPoint is to the left of the startPoint

        Dim v As Double = degrees / 360
        'Convert the degrees to a double from 0..<1 where 1 is a full revolution.

        Select Case v
            Case Is <= 0.25
                move.X = (4 * v) * speed
                move.Y = ((4 * v) - 1) * speed
            Case Is <= 0.5
                move.Y = (1 - 4 * (v - 0.25)) * speed
                move.X = (4 * (v - 0.25)) * speed
            Case Is <= 0.75
                move.X = (-4 * (v - 0.5)) * speed
                move.Y = (1 - 4 * (v - 0.5)) * speed
            Case Is <= 1
                move.Y = (-1 + 4 * (v - 0.75)) * speed
                move.X = (-4 * (v - 0.75)) * speed
        End Select
        'Calculate the movement per tick of the shot.

        If type = "shot" Or type = "extraShot" Then
            shots(index) = New Tuple(Of Point, Point, Point, Integer)(startPoint, endPoint, move, shotSize)
        Else
            enemies(index).mov = move
        End If

        Return move
    End Function
    'Calculate the movement of the object.

    ''' <summary>
    ''' This function calculates the movement in x and y coordinates between two points.
    ''' This is done by drawing a triangle between the <c>startPoint</c> and <c>endPoint</c> and calculating the angle between these points using trigonometry. 
    ''' This angle is then used to find the x and y movements is returned.
    ''' </summary>
    ''' <param name="startPoint">is the first point.</param>
    ''' <param name="endPoint">is the second point.</param>
    ''' <param name="speed">is the optional speed multiplier.</param>
    ''' <returns>The calculated movement as a point.</returns>
    Public Function calcMovePoint(startPoint As Point, endPoint As Point, Optional speed As Integer = 1) As Point
        Dim move As Point

        Dim b As Integer = endPoint.Y - startPoint.Y
        Dim c As Integer = endPoint.X - startPoint.X

        Dim radians As Double = Math.Atan(Math.Abs(c / b))
        Dim degrees As Double = radians * (180 / Math.PI)
        'the degrees from the closest axis (rounded down/clockwise)

        Dim quadrant As Integer
        'Quadrant 1..<5
        If c <= 0 And b <= 0 Then
            quadrant = 1
            degrees += 270
        ElseIf c >= 0 And b <= 0 Then
            quadrant = 2
            degrees += 0
        ElseIf c >= 0 And b >= 0 Then
            quadrant = 3
            degrees += 90
        ElseIf c <= 0 And b >= 0 Then
            quadrant = 4
            degrees += 180
        End If
        'Calculate the quadrant that the endPoint lies in relative to the startPoint and add the corresponding degrees.
        'If b < 0 then the endPoint is above the startPoint and if c < 0 then the endPoint is to the left of the startPoint

        Dim v As Double = degrees / 360
        'Convert the degrees to a double from 0..<1 where 1 is a full revolution.

        Select Case v
            Case Is <= 0.25
                move.X = (4 * v) * speed
                move.Y = ((4 * v) - 1) * speed
            Case Is <= 0.5
                move.Y = (1 - 4 * (v - 0.25)) * speed
                move.X = (4 * (v - 0.25)) * speed
            Case Is <= 0.75
                move.X = (-4 * (v - 0.5)) * speed
                move.Y = (1 - 4 * (v - 0.5)) * speed
            Case Is <= 1
                move.Y = (-1 + 4 * (v - 0.75)) * speed
                move.X = (-4 * (v - 0.75)) * speed
        End Select

        'Calculate the movement per tick of the shot.

        Return move
    End Function
    'Calculate the movement between two points.

    ''' <summary>
    ''' This function adds a new element to the corresponding array dictated by the <paramref name="type"/> and sets the correct values.
    ''' </summary>
    ''' <param name="type">is the type of the object that should be added.</param>
    ''' <returns></returns>
    Private Function addObject(type As String)
        Debug.WriteLine("add new object: " + type)
        If type = "shot" Or type = "extraShot" Then
            If shots Is Nothing Then
                shots = {New Tuple(Of Point, Point, Point, Integer)(New Point(player.loc(9).X, player.loc(9).Y), MousePosition, Nothing, If(type = "extraShot", 17, 10))}
            Else
                ReDim Preserve shots(shots.Length)
                shots(shots.Length - 1) = New Tuple(Of Point, Point, Point, Integer)(New Point(player.loc(9).X, player.loc(9).Y), MousePosition, Nothing, If(type = "extraShot", 17, 10))
            End If
            'Add start and end points to the new element.

            calcMoveObject(type, shots.Length - 1)
            'Calculate the movement of the shot.
            toolTips.shotShow = False
            'Do not show the move tooltip.

        ElseIf type = "boss" Then
            If gameBossForms Is Nothing Then
                gameBossForms = {New frmGameBoss}
                gameBossForms.Last.Location = New Point(Rnd() * (My.Computer.Screen.WorkingArea.Width - (2 * (gameBossForms.Last.Width + 50))), Rnd() * (My.Computer.Screen.WorkingArea.Height - (2 * (gameBossForms.Last.Height + 50))))
                gameBossForms.Last.Tag = (gameBossForms.Length - 1)
                gameBossForms.Last.Show()
            Else
                ReDim Preserve gameBossForms(gameBossForms.Length)
                gameBossForms(gameBossForms.Length - 1) = New frmGameBoss
                gameBossForms(gameBossForms.Length - 1).Location = New Point(Rnd() * (My.Computer.Screen.WorkingArea.Width - (2 * (gameBossForms.Last.Width + 50))), Rnd() * (My.Computer.Screen.WorkingArea.Height - (2 * (gameBossForms.Last.Height + 50))))
                gameBossForms(gameBossForms.Length - 1).Tag = (gameBossForms.Length - 1)

                gameBossForms(gameBossForms.Length - 1).Show()
            End If
            'Create a new boss form, set a random location, set its tag as its index and show the form.

        Else
            Dim locX As Integer
            Dim locY As Integer
            Select Case Math.Round(Rnd() * 4)
                Case 1 'Top
                    locX = Rnd() * Me.Width
                    locY = Me.Location.Y - 100
                Case 2 'Bottom
                    locX = Rnd() * Me.Width
                    locY = Me.Location.Y + Me.Height + 100
                Case 3 'Left
                    locX = Me.Location.Y - 100
                    locY = Rnd() * Me.Height
                Case 4 'Right
                    locX = Me.Location.Y + Me.Height + 100
                    locY = Rnd() * Me.Height
            End Select

            If enemies Is Nothing Then
                enemies = {New Enemy}
                enemies(0).type = type
                enemies(0).loc = PointToScreen(New Point(locX, locY))
                enemies(0).size = objectSizes(type)
                enemies(0).health = objectMaxHealth(type)
                enemies = {New Enemy With {
                     .type = type,
                     .loc = PointToScreen(New Point(locX, locY)),
                     .size = objectSizes(type),
                     .speed = objectSpeeds(type) + (Rnd() / 10),
                     .XP = objectXPs(type),
                     .health = objectMaxHealth(type)
                   }
                }
            Else
                ReDim Preserve enemies(enemies.Length)
                enemies(enemies.Length - 1) = New Enemy With {
                    .type = type,
                    .loc = PointToScreen(New Point(locX, locY)),
                    .size = objectSizes(type),
                    .speed = objectSpeeds(type) + (Rnd() / 5),
                    .XP = objectXPs(type),
                    .health = objectMaxHealth(type)
                }
            End If
            'Create a new enemy of the specified with default values 

            calcMoveObject(type, enemies.Length - 1)
            'Calculate the movement of the shot.
        End If
    End Function
    'Adds the specified object as a new element in its respective array.

    ''' <summary>
    ''' This function creates XP dots.
    ''' </summary>
    ''' <param name="loc">is the location that the enemy was killed.</param>
    ''' <param name="count">is the number of XP dots to create.</param>
    ''' <returns></returns>
    Public Function dropXP(loc As Point, count As Integer)
        For i As Integer = 1 To count
            If XPs Is Nothing Then
                XPs = {New XP}
                XPs(0).loc = New Point((loc.X - 50) + (Rnd() * 50), (loc.Y - 50) + (Rnd() * 50))
                XPs(0).size = 4 + (Rnd() * 7)
                XPs(0).mov = New Point(0, 0)
                XPs(0).speed = 1
            Else
                ReDim Preserve XPs(XPs.Length)
                XPs(XPs.Length - 1) = New XP With {
                    .loc = New Point((loc.X - 50) + (Rnd() * 50), (loc.Y - 50) + (Rnd() * 50)),
                    .size = 4 + (Rnd() * 7),
                    .mov = New Point(0, 0),
                    .speed = 1
                }
            End If
            'Create a new XP.
        Next
    End Function
    'Create XP dots.

    ''' <summary>
    ''' This function checks if any of the fired shots have pickedUp an enemy. If it has, the shot is removed and the enemy's health decreases.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkEnemyHits()
        For e As Integer = 0 To enemies.Length - 1
            Dim enemyHit As Boolean = False
            For s As Integer = 0 To shots.Length - 1
                Dim shot = shots(s)
                If enemies(e).loc.X <= (shot.Item1.X + shot.Item3.X + 10) And (shot.Item1.X - shot.Item3.X - 10) <= (enemies(e).loc.X + enemies(e).size) And (enemies(e).loc.Y - shot.Item3.Y - 10) <= shot.Item1.Y And shot.Item1.Y <= (enemies(e).loc.Y + enemies(e).size + shot.Item3.X + 10) Then
                    If Not piercing Then
                        removeElement("shots", s)
                    End If
                    If enemies(e).health <= 1 Then
                        dropXP(enemies(e).loc, enemies(e).XP)
                        removeElement("enemies", e)
                        frmStats.stats.enemiesKilled += 1
                    Else
                        enemies(e).health -= 1
                        enemies(e).white = 10
                    End If
                    'Remove the enemy If it has 0 health, otherwise, lower the health And make it white. 
                    enemyHit = True
                    'Exit the for loops.
                    Exit For
                End If
            Next
            If enemyHit Then
                Exit For
            End If
        Next
    End Function
    'Check if the shots have collided with an enemy.

    ''' <summary>
    ''' This function checks if any of the XP dots are within the <c>playerRadius</c>. If so, the XP dot starts moving towards the player.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkXPProx()
        For i As Integer = 0 To XPs.Length - 1
            If XPs(i).mov = New Point(0, 0) Then
                If XPs(i).loc.X <= (player.loc(9).X + playerRadius) And (player.loc(9).X - playerRadius) <= XPs(i).loc.X And XPs(i).loc.Y <= (player.loc(9).Y + playerRadius) And (player.loc(9).Y - playerRadius) <= XPs(i).loc.Y Then
                    XPs(i).mov = calcMovePoint(XPs(i).loc, player.loc(9), XPs(i).speed)
                    Exit For
                    'Exit the loop.
                End If
            End If
        Next
    End Function
    'Check the proximity of the XP dots to the player.

    ''' <summary>
    ''' This function checks if any of the XP dots have reached the player. If so, the XP dot is removed and the XP count increases.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkXPCollisions()
        For i As Integer = 0 To XPs.Length - 1
            If XPs(i).loc.X <= (player.loc(9).X + player.size + 5) And (player.loc(9).X - player.size - 5) <= XPs(i).loc.X And XPs(i).loc.Y <= (player.loc(9).Y + player.size + 5) And (player.loc(9).Y - player.size - 5) <= XPs(i).loc.Y Then
                removeElement("XP", i)
                frmStats.stats.xpCollected += 1
                player.XP += 1
                lblXP.Text = player.XP
                If Not toolTips.moveShow And Not toolTips.shotShow And tickCount > (toolTips.shotDelay + 50) And player.XP > 60 Then
                    lblToolTip.Text = toolTips.XPText
                    lblToolTip.Left = (Me.Width / 2) - (lblToolTip.Width / 2)
                    lblToolTip.Visible = True
                ElseIf toolTips.XPDelay = 0 And Not toolTips.XPShow Then
                    lblToolTip.Visible = False
                    toolTips.XPDelay = tickCount
                End If
                'Show tooltip if needed.
                Exit For
                'Exit the loop.
            End If
        Next
    End Function
    'Check if the XP has been picked up by the player.

    Private Function checkPlayerCollisions()
        For e As Integer = 0 To enemies.Length - 1
            Dim enemy = enemies(e)
            Dim hit = False
            For enemyX As Integer = enemies(e).loc.X To enemies(e).loc.X + enemies(e).size
                If Not hit Then
                    For enemyY As Integer = enemies(e).loc.Y To enemies(e).loc.Y + enemies(e).size
                        If ((player.loc(9).X - (player.size / 2)) < enemyX) And (enemyX < (player.loc(9).X + (player.size / 2))) And ((player.loc(9).Y - (player.size / 2)) < enemyY) And (enemyY < (player.loc(9).Y + (player.size / 2))) Then
                            Dim move As Point = calcMovePoint(enemies(e).loc, player.loc(0))
                            enemies(e).loc = New Point(enemies(e).loc.X - (move.X * 35), enemies(e).loc.Y - (move.Y * 35))
                            updatePlayerLoc(New Point(player.loc(9).X + (move.X * 35), player.loc(9).Y + (move.Y * 35)))
                            'Move the enemy and player.

                            player.red = 10
                            player.health -= 1
                            frmStats.stats.livesLost += 1
                            lblHealth.Text = ($"{player.health}/{player.maxHealth}")
                            'Update the player's health.
                            If player.health < 1 Then
                                endGame()
                            End If
                            'End the game if the player's health reaches 0

                            hit = True
                            Exit For
                            'Exit the loops.
                        End If
                    Next
                End If
            Next

        Next
    End Function
    'Check if the player has collided with an enemy.

    ''' <summary>
    ''' This function checks to see if the player has exited the bounds of the window.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkPlayerBounds()
        If player.loc(9).X < (Me.Location.X - 10) Then
            updatePlayerLoc(New Point(Me.Location.X + 20, player.loc(9).Y))
        ElseIf player.loc(9).X > (Me.Location.X + Me.Width + 10) Then
            updatePlayerLoc(New Point(Me.Location.X + Me.Width - 20, player.loc(9).Y))
        ElseIf player.loc(9).Y < (Me.Location.Y - 10) Then
            updatePlayerLoc(New Point(player.loc(9).X, Me.Location.Y + 20))
        ElseIf player.loc(9).Y > (Me.Location.Y + Me.Height + 10) Then
            updatePlayerLoc(New Point(player.loc(9).X, Me.Location.Y - Me.Height - 20))
        End If
    End Function
    'Check if the window has been hit by a shot.

    ''' <summary>
    ''' This function checks to see if a shot fired by the player has collided with the edge of the window on either of the 4 sides and increases the <c>storedExtend</c> appropriately.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkWindowHit()
        For i = 0 To shots.Length - 1
            Dim shot = shots(i)
            If shot.Item1.X < (Me.Location.X - 10) Then 'shot collided with left of the window.
                removeElement("shots", i)
                storedExtend.left = 9.5
                Exit For
            ElseIf shot.Item1.X > (Me.Location.X + Me.Width + 10) Then 'shot collided with right of the window.
                removeElement("shots", i)
                storedExtend.right = 9.5
                Exit For
            ElseIf shot.Item1.Y < (Me.Location.Y - 10) Then 'shot collided with top of the the window.
                removeElement("shots", i)
                storedExtend.top = 9.5
                Exit For
            ElseIf shot.Item1.Y > (Me.Location.Y + Me.Height + 10) Then 'shot collided with bottom of the window.
                removeElement("shots", i)
                storedExtend.bottom = 9.5
                Exit For
            End If
        Next
    End Function
    'Check if the window has been hit by a shot.

    ''' <summary>
    ''' This function removes the element at index <paramref name="index"/> in <paramref name="arrayName"/> and redimensions the array to one smaller.
    ''' </summary>
    ''' <param name="arrayName">is the array that is being edited</param>
    ''' <param name="index">is the index of element to be removed</param>
    ''' <returns></returns>
    Public Function removeElement(arrayName As String, index As Integer)
        Select Case arrayName
            Case "shots"
                Dim originalSize As Integer = shots.Length - 1
                shots(index) = shots(shots.Length - 1)
                ReDim Preserve shots(originalSize - 1)
            Case "enemies"
                Dim originalSize As Integer = enemies.Length - 1
                enemies(index) = enemies(enemies.Length - 1)
                ReDim Preserve enemies(originalSize - 1)
            Case "gameBossForms"
                If gameBossForms.Length <= 1 Then
                    gameBossForms(index).Close()
                    gameBossForms = {}
                Else
                    Dim originalSize As Integer = gameBossForms.Length - 1
                    gameBossForms(index) = gameBossForms(gameBossForms.Length - 1)
                    ReDim Preserve gameBossForms(originalSize - 1)
                End If
            Case "XP"
                Dim originalSize As Integer = XPs.Length - 1
                XPs(index) = XPs(XPs.Length - 1)
                ReDim Preserve XPs(originalSize - 1)
        End Select
    End Function
    'Remove an element from the specified array and index.

    ''' <summary>
    ''' This function updates <c>player.loc</c> by deleting the last value in the array, shifting the remaining down the index and adding <paramref name="newElement"/> to the start.
    ''' </summary>
    ''' <param name="newElement">is the new location</param>
    ''' <returns></returns>
    Public Function updatePlayerLoc(newElement As Point)
        For i As Integer = 0 To 9
            Select Case i
                Case 0 To 8
                    Dim element As Point = player.loc(i + 1)
                    player.loc(i) = element
                Case 9
                    player.loc(i) = newElement
            End Select
        Next
    End Function
    'Update the player's location.

    ''' <summary>
    ''' This function resizes the windows according to the store values found in the storedExtend variable.
    ''' </summary>
    ''' <returns></returns>
    Private Function extendSides()
        If storedExtend.top > 0 Then
            If Me.Top - storedExtend.top > 0 Then
                Me.Height += storedExtend.top
                Me.Top -= storedExtend.top
            End If
            storedExtend.top -= 1
        End If
        If storedExtend.bottom > 0 Then
            If Me.Bottom + storedExtend.bottom < frmStart.selectedScreen.WorkingArea.Height Then
                Me.Height += storedExtend.bottom
            End If
            storedExtend.bottom -= 1
        End If
        If storedExtend.left > 0 Then
            If Me.Left - storedExtend.left > 0 Then
                Me.Width += storedExtend.left
                Me.Left -= storedExtend.left
            End If
            storedExtend.left -= 1
        End If
        If storedExtend.right > 0 Then
            If Me.Right + storedExtend.right < frmStart.selectedScreen.WorkingArea.Width Then
                Me.Width += storedExtend.right
            End If
            storedExtend.right -= 1
        End If
    End Function
    'Resize the window.

    ''' <summary>
    ''' This function ends the game by stopping all the timers and showing the end/stats screen.
    ''' </summary>
    ''' <returns></returns>
    Public Function endGame()
        tmrTick.Enabled = False
        tmrShrink.Enabled = False
        tmrShot.Enabled = False
        tmrCircleE.Enabled = False
        tmrTriE.Enabled = False
        tmrSquareE.Enabled = False
        tmrBoss.Enabled = False
        frmStats.stats.timeAlive = $"{(DateAndTime.Now - startTime).Minutes}:{Format((DateAndTime.Now - startTime).Seconds, "00")}"
        If gameBossForms IsNot Nothing Then
            For Each boss In gameBossForms
                boss.tmrTick.Enabled = False
            Next
        End If


        frmStats.Show()
    End Function
    'End the game

    ''' <summary>
    ''' This function pauses or resumes the game by stopping all of the in-game timers.
    ''' </summary>
    ''' <param name="pause">whether or not the game is being paused or resumed.</param>
    ''' <returns></returns>
    Public Function toggleGame(pause As Boolean)
        If pause Then
            tmrTick.Enabled = False
            tmrShrink.Enabled = False
            tmrShot.Enabled = False
            tmrCircleE.Enabled = False
            tmrTriE.Enabled = False
            tmrSquareE.Enabled = False
            tmrBoss.Enabled = False
            If gameBossForms IsNot Nothing Then
                For Each bossForm In gameBossForms
                    bossForm.tmrShot.Enabled = False
                    bossForm.tmrTick.Enabled = False
                Next
            End If
        Else
                tmrTick.Enabled = True
            tmrShrink.Enabled = True
            tmrShot.Enabled = True
            tmrCircleE.Enabled = True
            tmrTriE.Enabled = True
            tmrSquareE.Enabled = True
            tmrBoss.Enabled = True
            If gameBossForms IsNot Nothing Then
                For Each bossForm In gameBossForms
                    bossForm.tmrShot.Enabled = True
                    bossForm.tmrTick.Enabled = True
                Next
            End If
        End If
    End Function
    'Pauses or resumes the game.

End Class


Public Class Player
    Public Property loc As Point() 'an array that stores the last 10 (0-9) locations of the player (center).
    Public Property size As Integer 'the size of the player.
    Public Property health As Integer 'the current health of the player.
    Public Property XP As Integer 'the current amount of XP the player has collected.
    Public Property red As Integer 'number of red frames to show.
    Public Property maxHealth As Integer 'the maximum health the player can have.
    'Public Property powerup As String
End Class
'Class to store the player information.
Public Class Enemy
    Public Property type As String 'the type of the enemy.
    Public Property loc As Point 'the current location of the enemy (top-left).
    Public Property mov As Point 'the calculated x and y axis movements of the enemy.
    Public Property size As Integer 'the size of the enemy.
    Public Property speed As Double 'the speed of the enemy.
    Public Property health As Integer 'the current health of the enemy.
    Public Property XP As Integer 'the amount of XP that the enemy drops when killed.
    Public Property white As Integer 'the number of white frames to show.
End Class
'Class to store enemy information.
Public Class Shot
    Public Property loc As Point 'the current location of the shot.
    Public Property endLoc As Point 'the end destination of shot (used to calculate the movement of the shot).
    Public Property mov As Point 'the calculated x and y axis movements of the shot.
    Public Property size As Integer 'the size of the shot.
End Class
'Class to store shot information.
Public Class XP
    Public Property loc As Point 'the location of the XP dot.
    Public Property size As Integer 'the size of the XP dot.
    Public Property speed As Integer 'the speed of the XP dot.
    Public Property mov As Point '(0, 0) if the XP hasn't been 'picked up'.
End Class
'Class to store the XP information
Public Class PressedKeys
    Public Property up As Boolean = False
    Public Property down As Boolean = False
    Public Property left As Boolean = False
    Public Property right As Boolean = False
    Public Property mouseLeft As Boolean = False
    Public Property mouseRight As Boolean = False
End Class
'Class to store the current state of buttons.
Public Class StoredExtend
    Public Property top As Integer
    Public Property bottom As Integer
    Public Property left As Integer
    Public Property right As Integer
End Class
'Class to store the resize values of each side.
Public Class Statistics
    Public Property timeAlive As String = "0:00"
    Public Property shotsFired As Integer = 0
    Public Property enemiesKilled As Integer = 0
    Public Property bossesKilled As Integer = 0
    Public Property livesLost As Integer = 0
    Public Property xpCollected As Integer = 0
End Class
'Class to store all of the current game statistics.
Public Class ColorPalette
    Public Property primary As Color = Color.FromArgb(255, 255, 255, 255)
    Public Property secondary As Color = Color.FromArgb(255, 217, 217, 217)
    Public Property tertiary As Color = Color.FromArgb(255, 151, 151, 151)
    Public Property background As Color = Color.FromArgb(255, 0, 0, 0)
    Public Property blue As Color = Color.FromArgb(255, 156, 243, 255)
    Public Property green As Color = Color.FromArgb(255, 158, 255, 156)
    Public Property yellow As Color = Color.FromArgb(255, 255, 213, 131)
    Public Property red As Color = Color.FromArgb(255, 255, 156, 156)
    Public Property purple As Color = Color.FromArgb(255, 241, 156, 255)
End Class
'Class to store all the custom colors.
Public Class ToolTips
    Public Property moveText As String = "use the arrows keys or WASD to move" 'the text to display on the tooltip.
    Public Property moveShow As Boolean = True 'whether or not the move tooltip should be shown.
    Public Property moveDelay As Integer = 0 'the tick count that the move tooltip was hidden to delay any consequent tooltips.
    Public Property shotText As String = "hold/click the left mouse button to shoot bullets" 'the text to display on the tooltip.
    Public Property shotShow As Boolean = True 'whether or not the move tooltip should be shown.
    Public Property shotDelay As Integer = 0 'the tick count that the shot tooltip was hidden to delay any consequent tooltips.
    Public Property XPText As String = "press the spacebar to open the shop"
    Public Property XPShow As Boolean = False
    Public Property XPDelay As Integer = 0
End Class
'Class to store all of the tooltip information.