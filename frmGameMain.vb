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
    Public stats As New Statistics

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

    Dim gameBossForms() As frmGameBoss
    'The window to show to the boss in.


    Dim playerSpeed As Integer = 3
    Dim objectSpeeds As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"shot", 14},
        {"extraShot", 23},
        {"circle", 2.4},
        {"square", 2},
        {"triangle", 2.9}
    }
    'Initialize a dictionary that stores {object type, speeed}.
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

    Public shots() As Tuple(Of Point, Point, Point, Integer)
    'An array of tuples that stores the [1] current location, [2] end destination, [3] the calculated movement and [4] the size of the shots.



    '------------------------------------------------------------------------------- EVENTS -------------------------------------------------------------------------------
    Private Sub frmGameMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        formMainBackground.Show()
        frmGameBackground.Hide()
        'Switch the background forms.

        lblHealth.Font = New Font(frmStart.fonts.Families(1), 15.75, FontStyle.Bold)
        'Import fonts.

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
                        shot = New Tuple(Of Point, Point, Point, Integer)(New Point(shot.Item1.X + shot.Item3.X, shot.Item1.Y + shot.Item3.Y), shot.Item2, shot.Item3, shot.Item4)
                        Using pen As New Pen(colors.red, 2)
                            e.Graphics.DrawEllipse(pen, New Rectangle(PointToClient(shot.Item1).X, PointToClient(shot.Item1).Y, shot.Item4, shot.Item4))
                        End Using
                        boss.shots(i) = shot
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
            Case 10
                tmrSquareE.Enabled = True
                'Start generating square enemies.
            Case 200
                tmrCircleE.Enabled = True
                'Start generating circle enemies.
            Case 1000
                tmrTriE.Enabled = True
                'Start generating triangle enemies.
            Case 30
                addObject("boss")
                'Generate a boss in a new window.
        End Select
        'Delay actions.

        If (tickCount Mod 2) = 0 Then
            picCanvas.Invalidate()
        End If
        'Redraw all the shots and player every second tick.

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
        End If
        If pressedKeys.down = True And player.loc(9).Y < Me.Location.Y + Me.Height - 30 Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y + playerSpeed))
        End If
        If pressedKeys.left = True And player.loc(9).X > Me.Location.X + 30 Then
            updatePlayerLoc(New Point(player.loc(9).X - playerSpeed, player.loc(9).Y))
        End If
        If pressedKeys.right = True And player.loc(9).X < Me.Location.X + Me.Width - 32 Then
            updatePlayerLoc(New Point(player.loc(9).X + playerSpeed, player.loc(9).Y))
        End If
        If Not pressedKeys.up And Not pressedKeys.down And Not pressedKeys.left And Not pressedKeys.right Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y))
        End If
        'Move the player if the arrow keys are currently being pressed and make sure the player does not leave the window.

        If shots IsNot Nothing Then
            checkWindowHit()
            If enemies IsNot Nothing Then
                checkEnemyHits()
            End If
        End If
        If enemies IsNot Nothing Then
            checkPlayerCollisions()
        End If
        'Check collisions between the window, shots, enemies and player.

        extendSides()
        'Resize the window accordingly.
    End Sub
    'Force redraw of all shapes and players and move the player accordingly.
    Private Sub tmrShot_Tick(sender As Object, e As EventArgs) Handles tmrShot.Tick
        If pressedKeys.mouseLeft Then
            addObject("shot")
            stats.shotsFired += 1
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
        If Rnd() > 0.91 Then
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
    Private Sub tmrShrink_Tick(sender As Object, e As EventArgs) Handles tmrShrink.Tick
        If Me.Width > 300 Then
            Me.Width -= 2
            Me.Left += 1
        End If
        If Me.Height > 300 Then
            Me.Height -= 2
            Me.Top += 1
        End If
        If player.loc(9).Y < Me.Location.Y + 40 Then
            player.loc(9).Y += 1
        End If
        If player.loc(9).Y > Me.Location.Y + Me.Height - 30 Then
            player.loc(9).Y -= 1
        End If
        If player.loc(9).X < Me.Location.X + 17 Then
            player.loc(9).X += 1
        End If
        If player.loc(9).X > Me.Location.X + Me.Width - 33 Then
            player.loc(9).X -= 1
        End If
    End Sub
    'Shrink the window.

    '---- MOUSE & KEYS----
    Private Sub frmGameMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown, formMainBackground.KeyDown
        Select Case e.KeyCode
            Case Keys.Up
                pressedKeys.up = True
            Case Keys.Down
                pressedKeys.down = True
            Case Keys.Left
                pressedKeys.left = True
            Case Keys.Right
                pressedKeys.right = True
        End Select
    End Sub
    'Update the correct variables when a key is pressed.
    Private Sub frmGameMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp, formMainBackground.KeyUp
        Select Case e.KeyCode
            Case Keys.Up
                pressedKeys.up = False
            Case Keys.Down
                pressedKeys.down = False
            Case Keys.Left
                pressedKeys.left = False
            Case Keys.Right
                pressedKeys.right = False
        End Select
    End Sub
    'Update the correct variables when a key is released.
    Public Sub picCanvas_MouseDown(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseDown, formMainBackground.MouseDown
        If e.Button = MouseButtons.Left Then
            pressedKeys.mouseLeft = True
            If shotStore Then
                addObject("shot")
                stats.shotsFired += 1
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
            pressedKeys.mouseLeft = False
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
        Else
            startPoint = enemies(index).loc
            endPoint = New Point(player.loc(0).X, player.loc(0).Y)
        End If

        Try
            speed = objectSpeeds(type)
        Catch err As System.Collections.Generic.KeyNotFoundException
            Console.WriteLine($"Type not found as a key in objectSpeeds: '{type}', defau automatically set...")
            speed = 10
        End Try
        'Stop errors when finding t of an object

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
    ''' <returns>The calculated movement as a point.</returns>
    Public Function calcMovePoint(startPoint As Point, endPoint As Point) As Point
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
                move.X = (4 * v)
                move.Y = ((4 * v) - 1)
            Case Is <= 0.5
                move.Y = (1 - 4 * (v - 0.25))
                move.X = (4 * (v - 0.25))
            Case Is <= 0.75
                move.X = (-4 * (v - 0.5))
                move.Y = (1 - 4 * (v - 0.5))
            Case Is <= 1
                move.Y = (-1 + 4 * (v - 0.75))
                move.X = (-4 * (v - 0.75))
        End Select
        'Calculate the movement per tick of the shot.

        Return move
    End Function
    'Calculate the movement between two points.

    ''' <summary>
    ''' This function adds a new element to the corresponding array dictated by the <paramref name="type"/> and sets the correct values.
    ''' </summary>
    ''' <param name="type">is the type of the object that should be added</param>
    ''' <returns></returns>
    Private Function addObject(type As String)
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
        ElseIf type = "boss" Then
            If gameBossForms Is Nothing Then
                gameBossForms = {New frmGameBoss}
                gameBossForms.Last.Location = New Point(Rnd() * (My.Computer.Screen.WorkingArea.Width - (2 * (gameBossForms.Last.Width + 50))), Rnd() * (My.Computer.Screen.WorkingArea.Height - (2 * (gameBossForms.Last.Height + 50))))
                gameBossForms.Last.Tag = (gameBossForms.Length - 1)
                gameBossForms.Last.Show()
            Else
                ReDim Preserve gameBossForms(gameBossForms.Length)
                gameBossForms.Last.Location = New Point(Rnd() * (My.Computer.Screen.WorkingArea.Width - (2 * (gameBossForms.Last.Width + 50))), Rnd() * (My.Computer.Screen.WorkingArea.Height - (2 * (gameBossForms.Last.Height + 50))))
                gameBossForms.Last.Tag = (gameBossForms.Length - 1)
                gameBossForms.Last.Show()
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
                     .health = objectMaxHealth(type)
                   }
                }
            Else
                ReDim Preserve enemies(enemies.Length)
                enemies(enemies.Length - 1) = New Enemy With {
                    .type = type,
                    .loc = PointToScreen(New Point(locX, locY)),
                    .size = objectSizes(type),
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
    ''' This function checks if any of the fired shots have hit an enemy. If it has, the shot is removed and the enemy's health decreases.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkEnemyHits()
        For e As Integer = 0 To enemies.Length - 1
            Dim enemyHit As Boolean = False
            For s As Integer = 0 To shots.Length - 1
                Dim shot = shots(s)
                If enemies(e).loc.X <= (shot.Item1.X + shot.Item3.X + 10) And (shot.Item1.X - shot.Item3.X - 10) <= (enemies(e).loc.X + enemies(e).size) And (enemies(e).loc.Y - shot.Item3.Y - 10) <= shot.Item1.Y And shot.Item1.Y <= (enemies(e).loc.Y + enemies(e).size + shot.Item3.X + 10) Then
                    Debug.WriteLine("Hit")
                    removeElement("shots", s)
                    If enemies(e).health <= 1 Then
                        removeElement("enemies", e)
                        stats.enemiesKilled += 1
                    Else
                        enemies(e).health -= 1
                        enemies(e).white = 10
                    End If
                    'Remove the enemy if it has 0 health, otherwise, lower the health and make it white. 
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
    ''' This function checks if the player has collided with any of the enemies. If they have, the players health decreases.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkPlayerCollisions()
        For e As Integer = 0 To enemies.Length - 1
            Dim enemy = enemies(e)
            Dim hit = False
            For enemyX As Integer = enemies(e).loc.X To enemies(e).loc.X + enemies(e).size
                If Not hit Then
                    For enemyY As Integer = enemies(e).loc.Y To enemies(e).loc.Y + enemies(e).size
                        If ((player.loc(9).X - (player.size / 2)) < enemyX) And (enemyX < (player.loc(9).X + (player.size / 2))) And ((player.loc(9).Y - (player.size / 2)) < enemyY) And (enemyY < (player.loc(9).Y + (player.size / 2))) Then
                            Dim move As Point = calcMovePoint(enemies(e).loc, player.loc(9))
                            enemies(e).loc = New Point(enemies(e).loc.X - (move.X * 35), enemies(e).loc.Y - (move.Y * 35))
                            updatePlayerLoc(New Point(player.loc(0).X + (move.X * 35), player.loc(0).Y + (move.Y * 35)))
                            'Move the enemy and player.

                            player.red = 10
                            player.health -= 1
                            stats.livesLost += 1
                            lblHealth.Text = ($"{player.health}/{player.maxHealth}")
                            'Update the player's health.
                            If player.health < 1 Then
                                tmrTick.Enabled = False
                                tmrShrink.Enabled = False
                                tmrShot.Enabled = False
                                stats.timeAlive = DateAndTime.Now - startTime
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
    ''' This function checks to see if a shot fired by the player has collided with the edge of the window on either of the 4 sides and increases the <c>storedExtend</c> appropriately.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkWindowHit()
        For i = 0 To shots.Length - 1
            Dim shot = shots(i)
            If shot.Item1.X < (Me.Location.X - 10) Then 'shot collided with left of the window.
                Debug.WriteLine("left")
                removeElement("shots", i)
                storedExtend.left = 9.5
                Exit For
            ElseIf shot.Item1.X > (Me.Location.X + Me.Width + 10) Then 'shot collided with right of the window.
                Debug.WriteLine("right")
                removeElement("shots", i)
                storedExtend.right = 9.5
                Exit For
            ElseIf shot.Item1.Y < (Me.Location.Y - 10) Then 'shot collided with top of the the window.
                Debug.WriteLine("top")
                removeElement("shots", i)
                storedExtend.top = 9.5
                Exit For
            ElseIf shot.Item1.Y > (Me.Location.Y + Me.Height + 10) Then 'shot collided with bottom of the window.
                Debug.WriteLine("bottom")
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
        If arrayName = "shots" Then
            Dim originalSize As Integer = shots.Length - 1
            shots(index) = shots(shots.Length - 1)
            ReDim Preserve shots(originalSize - 1)
        ElseIf arrayName = "enemies" Then
            Dim originalSize As Integer = enemies.Length - 1
            enemies(index) = enemies(enemies.Length - 1)
            ReDim Preserve enemies(originalSize - 1)
        ElseIf arrayName = "gameBossForms" Then
            If gameBossForms.Length <= 1 Then
                gameBossForms = {}
            Else
                Dim originalSize As Integer = gameBossForms.Length - 1
                gameBossForms(index) = gameBossForms(gameBossForms.Length - 1)
                ReDim Preserve gameBossForms(originalSize - 1)
            End If
        End If
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
    End Function    '
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

End Class


Public Class Player
    Public Property loc As Point() 'an array that stores the last 10 (0-9) locations of the player (center).
    Public Property size As Integer 'the size of the player.
    Public Property health As Integer 'the current health of the player.
    Public Property red As Integer 'number of red frames to show.
    Public Property maxHealth As Integer 'the maximum health the player can have.
    'Public Property powerup As String
End Class
'Class to store the player information.
Public Class Enemy
    Public Property type As String 'the type of the enemy (square).
    Public Property loc As Point 'the current location of the enemy (top-left).
    Public Property mov As Point 'the calculated x and y axis movements of the enemy.
    Public Property size As Integer 'the size of the enemy.
    Public Property health As Integer 'the current health of the enemy.
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
    Public Property mov As Point '(0, 0) if the XP hasn't been 'picked up'.
End Class
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
    Public Property timeAlive As TimeSpan
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
    Public Property tertiary As Color = Color.FromArgb(255, 176, 176, 176)
    Public Property background As Color = Color.FromArgb(255, 0, 0, 0)
    Public Property blue As Color = Color.FromArgb(255, 156, 243, 255)
    Public Property green As Color = Color.FromArgb(255, 158, 255, 156)
    Public Property yellow As Color = Color.FromArgb(255, 255, 213, 131)
    Public Property red As Color = Color.FromArgb(255, 255, 156, 156)
    Public Property purple As Color = Color.FromArgb(255, 241, 156, 255)
End Class
'Class to store all the custom colors.