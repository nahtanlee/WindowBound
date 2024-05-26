Imports System.Drawing.Imaging
Imports System.Drawing.Text
Imports System.Globalization
Public Class frmGameMain
    '---------------------------------------------------------------------------- VARIABLES ----------------------------------------------------------------------------
    Dim player As New Player
    'A class to store all of the player information.
    Dim enemies() As Enemy
    'An array of the class enemy to store all the enemy information.
    Dim colors As New ColorPalette
    Dim fonts As New PrivateFontCollection()

    Dim pressedKeys As New PressedKeys
    'A class that stores whether the arrow buttons are pressed.
    Dim storedExtend As New StoredExtend
    'A class that stores the value that each side of the window needs to be extended by.
    Dim tickCount As Integer = 0
    'Count the number of ticks

    Dim playerSpeed As Integer = 2.3
    Dim objectSpeeds As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"shot", 16},
        {"extraShot", 23},
        {"square", 2}
    }
    'Initialize a dictionary that stores {object type, speeed}.
    Dim objectMaxHealth As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"player", 10},
        {"square", 2}
    }
    'Initialize a dictionary that stores {object type, max health}.
    Dim objectSizes As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"player", 14},
        {"square", 18}
    }

    Dim shots() As Tuple(Of Point, Point, Point, Integer)
    'An array of tuples that stores the [1] current location, [2] end destination, [3] the calculated movement and [4] the size of the shots.


    '------------------------------------------------------------------------------- EVENTS -------------------------------------------------------------------------------
    Private Sub frmGameMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        fonts.AddFontFile("Fonts/Pressario.ttf") 'Title
        fonts.AddFontFile("Fonts/BoldenaBold.ttf") 'Numbers
        fonts.AddFontFile("Fonts/VarelaRound.ttf") 'Text
        lblHealth.Font = New Font(fonts.Families(1), 16, FontStyle.Bold)
        'Import fonts.

        player.loc = {PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2)))}
        player.size = objectSizes("player")
        player.maxHealth = 10
        player.health = 10
        'Set default values for variables in the Player class.

        pressedKeys.up = False
        pressedKeys.down = False
        pressedKeys.left = False
        pressedKeys.right = False
        'Set default values for variables.

        Randomize()
        'Initialize the random generator.

        tmrTick.Enabled = True
        'Only start the tick once all variables have been initialized to prevent null errors from occurring.
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

        If enemies IsNot Nothing Then
            For i As Integer = 0 To (enemies.Length - 1)
                Dim pen As New Pen(If(enemies(i).white > 0, colors.secondary, colors.green), 7)
                'Make the pen secondary if it has just been hit.
                enemies(i).loc = New Point(enemies(i).loc.X + enemies(i).mov.X, enemies(i).loc.Y + enemies(i).mov.Y)
                'Update the position of the enemy
                e.Graphics.DrawRectangle(pen, New Rectangle(PointToClient(enemies(i).loc), New Size(enemies(i).size, enemies(i).size)))
                calcMove(enemies(i).type, i)
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
    Private Sub tmrtick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        tickCount += 1
        Select Case tickCount
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
        End Select


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

        If pressedKeys.up = True And player.loc(9).Y > Me.Location.Y + 40 Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y - playerSpeed))
        End If
        If pressedKeys.down = True And player.loc(9).Y < Me.Location.Y + Me.Height - 30 Then
            updatePlayerLoc(New Point(player.loc(9).X, player.loc(9).Y + playerSpeed))
        End If
        If pressedKeys.left = True And player.loc(9).X > Me.Location.X + 17 Then
            updatePlayerLoc(New Point(player.loc(9).X - playerSpeed, player.loc(9).Y))
        End If
        If pressedKeys.right = True And player.loc(9).X < Me.Location.X + Me.Width - 33 Then
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
        addObject("shot")
    End Sub
    'Add a new shot.
    Private Sub tmrsquareE_Tick(sender As Object, e As EventArgs) Handles tmrSquareE.Tick
        If Rnd() > 0.9 Then
            addObject("square")
        End If
    End Sub
    'Add a new square enemy.
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
    Private Sub frmGameMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
    Private Sub frmGameMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
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
    Private Sub picCanvas_MouseDown(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseDown
        If e.Button = MouseButtons.Left Then
            tmrShot.Enabled = True
            addObject("shot")
            'Add normal sized shots
        ElseIf e.Button = MouseButtons.Right Then
            addObject("extraShot")
            'Add a large shot.
        End If
    End Sub
    'Add the correct shot based on which mouse is pressed.
    Private Sub picCanvas_MouseUp(sender As Object, e As MouseEventArgs) Handles picCanvas.MouseUp
        If e.Button = MouseButtons.Left Then
            tmrShot.Enabled = False
        End If
    End Sub
    'Stop adding shots.



    '----------------------------------------------------------------------------- FUNCTIONS -----------------------------------------------------------------------------
    ''' <summary>
    ''' This function calculates the movement in x and y coordinates that the object should move by each tick.
    ''' This is done by drawing a triangle between the <c>startPoint</c> and <c>endPoint</c> and calculating the angle between these points using trigonometry. 
    ''' This angle is then used to find the x and y movements whihc are assigned to <c>move.X</c> and <c>move.Y</c> repectively.
    ''' </summary>
    ''' <param name="type">is the type of object that the movement is being calculated of</param>
    ''' <param name="index">is the index of the object within its array</param>
    ''' <returns></returns>
    Private Function calcMove(type As String, index As Integer) As Point
        Dim startPoint As Point
        Dim endPoint As Point
        Dim move As Point
        Dim shotSize As Integer
        Dim speed As Integer

        If type = "shot" Or type = "extraShot" Then
            startPoint = shots(index).Item1
            endPoint = shots(index).Item2
            shotSize = shots(index).Item4
        ElseIf type = "square" Then
            startPoint = enemies(index).loc
            endPoint = New Point(player.loc(0).X, player.loc(0).Y)
        End If

        Try
            speed = objectSpeeds(type)
        Catch err As System.Collections.Generic.KeyNotFoundException
            Console.WriteLine($"Type not found as a key in objectSpeeds: '{type}', default speed automatically set...")
            speed = 10
        End Try
        'Stop errors when finding the speed of an object

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
        ElseIf type = "square" Then
            enemies(index).mov = move
        End If

        Return move
    End Function
    'Calculate the movement of the object.

    ''' <summary>
    ''' This function adds a new element to the corresponding array dictated by the <paramref name="type"/> and sets the correct values.
    ''' </summary>
    ''' <param name="type">is the type of the object that should be added</param>
    ''' <returns></returns>
    Public Function addObject(type As String)
        If type = "shot" Or type = "extraShot" Then
            If shots Is Nothing Then
                shots = {New Tuple(Of Point, Point, Point, Integer)(New Point(player.loc(9).X, player.loc(9).Y), MousePosition, Nothing, If(type = "extraShot", 17, 10))}
            Else
                ReDim Preserve shots(shots.Length)
                shots(shots.Length - 1) = New Tuple(Of Point, Point, Point, Integer)(New Point(player.loc(9).X, player.loc(9).Y), MousePosition, Nothing, If(type = "extraShot", 17, 10))
            End If
            'Add start and end points to the new element.
            Debug.WriteLine($"Number of shots: {shots.Length}")

            calcMove(type, shots.Length - 1)
            'Calculate the movement of the shot.
        ElseIf type = "square" Then
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

            calcMove(type, enemies.Length - 1)
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
                If enemies(e).loc.X <= shot.Item1.X And shot.Item1.X <= (enemies(e).loc.X + enemies(e).size) And enemies(e).loc.Y <= shot.Item1.Y And shot.Item1.Y <= (enemies(e).loc.Y + enemies(e).size) Then
                    removeElement("shots", s)
                    If enemies(e).health <= 1 Then
                        removeElement("enemies", e)
                    Else
                        enemies(e).health -= 1
                        enemies(e).white = 7
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
                        If ((player.loc(0).X - (player.size / 2)) < enemyX) And (enemyX < (player.loc(0).X + (player.size / 2))) And ((player.loc(0).Y - (player.size / 2)) < enemyY) And (enemyY < (player.loc(0).Y + (player.size / 2))) Then
                            enemies(e).loc = New Point(enemies(e).loc.X - (enemies(e).mov.X * 15), enemies(e).loc.Y - (enemies(e).mov.Y * 15))
                            updatePlayerLoc(New Point(player.loc(0).X + (enemies(e).mov.X * 15), player.loc(0).Y + (enemies(e).mov.Y * 15)))
                            'Move the enemy and player.

                            player.red = 10
                            player.health -= 1
                            lblHealth.Text = ($"{player.health}/{player.maxHealth}")
                            'update the player's health

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
                storedExtend.left = 7
                Exit For
            ElseIf shot.Item1.X > (Me.Location.X + Me.Width + 10) Then 'shot collided with right of the window.
                Debug.WriteLine("right")
                removeElement("shots", i)
                storedExtend.right = 7
                Exit For
            ElseIf shot.Item1.Y < (Me.Location.Y - 10) Then 'shot collided with top of the the window.
                Debug.WriteLine("top")
                removeElement("shots", i)
                storedExtend.top = 7
                Exit For
            ElseIf shot.Item1.Y > (Me.Location.Y + Me.Height + 10) Then 'shot collided with bottom of the window.
                Debug.WriteLine("bottom")
                removeElement("shots", i)
                storedExtend.bottom = 7
                Exit For
            End If
        Next
    End Function
    'Extend the window if it has been hit by a shot.

    ''' <summary>
    ''' This function removes the element at index <paramref name="index"/> in <paramref name="arrayName"/> and redimensions the array to one smaller.
    ''' </summary>
    ''' <param name="arrayName">is the array that is being edited</param>
    ''' <param name="index">is the index of element to be removed</param>
    ''' <returns></returns>
    Private Function removeElement(arrayName As String, index As Integer)
        Dim OGSize = If(arrayName = "shots", shots, enemies).Length
        If arrayName = "shots" Then
            Dim originalSize As Integer = shots.Length - 1
            shots(index) = shots(shots.Length - 1)
            ReDim Preserve shots(originalSize - 1)
        ElseIf arrayName = "enemies" Then
            Dim originalSize As Integer = enemies.Length - 1
            enemies(index) = enemies(enemies.Length - 1)
            ReDim Preserve enemies(originalSize - 1)
        End If
    End Function
    'Remove an element from the specified array and index.

    ''' <summary>
    ''' This function updates <c>player.loc</c> by deleting the last value in the array, shifting the remaining down the index and adding <paramref name="newElement"/> to the start.
    ''' </summary>
    ''' <param name="newElement">is the new location</param>
    ''' <returns></returns>
    Private Function updatePlayerLoc(newElement As Point)
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
            Me.Height += storedExtend.top
            Me.Top -= storedExtend.top
            storedExtend.top -= 1
        End If
        If storedExtend.bottom > 0 Then
            Me.Height += storedExtend.bottom
            storedExtend.bottom -= 1
        End If
        If storedExtend.left > 0 Then
            Me.Width += storedExtend.left
            Me.Left -= storedExtend.left
            storedExtend.left -= 1
        End If
        If storedExtend.right > 0 Then
            Me.Width += storedExtend.right
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
Public Class PressedKeys
    Public Property up As Boolean
    Public Property down As Boolean
    Public Property left As Boolean
    Public Property right As Boolean
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
    Public Property shotsFired As Integer
    Public Property enemiesKilled As Integer
    Public Property bossesKilled As Integer
    Public Property xpCollected As Integer
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
End Class
'Class to store all the custom colors.