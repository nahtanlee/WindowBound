Imports System.Globalization
Public Class frmGameMain
    '---------------------------------------------------------------------------- VARIABLES ----------------------------------------------------------------------------
    Dim player As New Player
    'A class to store all of the player information.
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
        {"square", 1}
    }
    'Initialize a dictionary that stores {object type, speeed}.
    Dim objectMaxHealth As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer) From {
        {"player", 10},
        {"square", 2}
    }
    'Initialize a dictionary that stores {object type, max health}.

    Dim shots() As Tuple(Of Point, Point, Point, Integer)
    'An array of tuples that stores the [1] current location, [2] end destination, [3] the calculated movement and [4] the size of the shots.
    Dim movingEnemies() As Tuple(Of Point, Point, Integer, String, Integer, Integer)
    'An array of tuples that stores [1] the current location, [2] calculated movement, [3] size, [4] type (square, circle, square) of the enemies (moving), [5] current health and [6] how many white frames to show (number of frames after it has been hit).



    '------------------------------------------------------------------------------- EVENTS -------------------------------------------------------------------------------
    Private Sub frmGameMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        player.loc = {PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2))), PointToScreen(New Point((Me.Width / 2), (Me.Height / 2)))}
        player.size = 14
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
        'Only start the tick once all variables have been initialized to prevent null errors from occuring.
    End Sub
    'On form load.
    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
        If shots IsNot Nothing Then
            For i As Integer = 0 To (shots.Length - 1)
                Dim shot = shots(i)
                shot = New Tuple(Of Point, Point, Point, Integer)(New Point(shot.Item1.X + shot.Item3.X, shot.Item1.Y + shot.Item3.Y), shot.Item2, shot.Item3, shot.Item4)
                Using pen As New SolidBrush(Color.LawnGreen)
                    e.Graphics.FillEllipse(pen, New Rectangle(PointToClient(shot.Item1).X - shot.Item3.X, PointToClient(shot.Item1).Y - shot.Item3.Y, shot.Item4, shot.Item4))
                    e.Graphics.FillEllipse(pen, New Rectangle(PointToClient(shot.Item1).X, PointToClient(shot.Item1).Y, shot.Item4, shot.Item4))
                End Using
                shots(i) = shot
            Next
            'Draw each shot with a trail and update its position.
        End If

        If movingEnemies IsNot Nothing Then
            For i As Integer = 0 To (movingEnemies.Length - 1)
                Dim movingEnemy = movingEnemies(i)
                Dim pen As New Pen(Color.Cyan, 7)
                If movingEnemies(i).Item6 > 0 Then
                    pen = New Pen(Color.White, 7)
                    movingEnemy = New Tuple(Of Point, Point, Integer, String, Integer, Integer)(New Point(movingEnemy.Item1.X + movingEnemy.Item2.X, movingEnemy.Item1.Y + movingEnemy.Item2.Y), movingEnemy.Item2, movingEnemy.Item3, movingEnemy.Item4, movingEnemy.Item5, movingEnemy.Item6 - 1)
                Else
                    movingEnemy = New Tuple(Of Point, Point, Integer, String, Integer, Integer)(New Point(movingEnemy.Item1.X + movingEnemy.Item2.X, movingEnemy.Item1.Y + movingEnemy.Item2.Y), movingEnemy.Item2, movingEnemy.Item3, movingEnemy.Item4, movingEnemy.Item5, 0)
                End If
                'Make the pen white if it has just been hit.
                e.Graphics.DrawRectangle(pen, New Rectangle(PointToClient(movingEnemy.Item1), New Size(movingEnemy.Item3, movingEnemy.Item3)))
                movingEnemies(i) = movingEnemy
                calcMove("square", i)
            Next
            'Draw each moving enemy and update and recalculate its position.
        End If
        Using pen As New Pen(Color.White, 14)
            e.Graphics.DrawEllipse(pen, CInt(PointToClient(player.loc(9)).X - player.size / 2), CInt(PointToClient(player.loc(9)).Y - (player.size / 2)), player.size, player.size)
        End Using
        Using brush As New SolidBrush(Me.BackColor)
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
            Case 50
                tmrSquareE.Enabled = True
                'Start generating square enemies
        End Select

        picCanvas.Invalidate()
        'Redraw all the shots and player.

        If pressedKeys.up = True And player.loc(9).Y > Me.Location.Y + 40 Then
            updateplayerLoc(New Point(player.loc(9).X, player.loc(9).Y - playerSpeed))
        End If
        If pressedKeys.down = True And player.loc(9).Y < Me.Location.Y + Me.Height - 30 Then
            updateplayerLoc(New Point(player.loc(9).X, player.loc(9).Y + playerSpeed))
        End If
        If pressedKeys.left = True And player.loc(9).X > Me.Location.X + 17 Then
            updateplayerLoc(New Point(player.loc(9).X - playerSpeed, player.loc(9).Y))
        End If
        If pressedKeys.right = True And player.loc(9).X < Me.Location.X + Me.Width - 33 Then
            updateplayerLoc(New Point(player.loc(9).X + playerSpeed, player.loc(9).Y))
        End If
        'Move the player if the arrow keys are currently being pressed and make sure the player does not leave the window.

        If shots IsNot Nothing Then
            checkWindowHit()
            If movingEnemies IsNot Nothing Then
                checkEnemyHits()
            End If
        End If
        'Check collisions between the shots and enemies.

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
            startPoint = movingEnemies(index).Item1
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
            movingEnemies(index) = New Tuple(Of Point, Point, Integer, String, Integer, Integer)(startPoint, move, movingEnemies(index).Item3, type, movingEnemies(index).Item5, movingEnemies(index).Item6)
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

            If movingEnemies Is Nothing Then
                movingEnemies = {New Tuple(Of Point, Point, Integer, String, Integer, Integer)(PointToScreen(New Point(locX, locY)), Nothing, 18, "square", objectSpeeds("square"), False)}
            Else
                ReDim Preserve movingEnemies(movingEnemies.Length)
                movingEnemies(movingEnemies.Length - 1) = New Tuple(Of Point, Point, Integer, String, Integer, Integer)(PointToScreen(New Point(locX, locY)), Nothing, 18, "square", objectSpeeds("square"), False)
            End If
            'Add start and end points to the new element.

            calcMove(type, movingEnemies.Length - 1)
            'Calculate the movement of the shot.
        End If
    End Function
    'Adds the specified object as a new element in its respective array.

    ''' <summary>
    ''' This function checks if any of the fired shots have hit an enemy. If it has, the shot is removed and the enemy's health decreases.
    ''' </summary>
    ''' <returns></returns>
    Private Function checkEnemyHits()
        For e As Integer = 0 To movingEnemies.Length - 1
            Dim enemyHit As Boolean = False
            Dim enemy = movingEnemies(e)
            For s As Integer = 0 To shots.Length - 1
                Dim shot = shots(s)
                If enemy.Item1.X <= shot.Item1.X And shot.Item1.X <= (enemy.Item1.X + enemy.Item3) And enemy.Item1.Y <= shot.Item1.Y And shot.Item1.Y <= (enemy.Item1.Y + enemy.Item3) Then
                    removeElement("shots", s)
                    If enemy.Item5 <= 1 Then
                        removeElement("movingEnemies", e)
                    Else
                        movingEnemies(e) = New Tuple(Of Point, Point, Integer, String, Integer, Integer)(movingEnemies(e).Item1, movingEnemies(e).Item2, movingEnemies(e).Item3, movingEnemies(e).Item4, movingEnemies(e).Item5 - 1, 7)
                    End If
                    enemyHit = True
                    Exit For
                End If
            Next
            If enemyHit Then
                Exit For
            End If
        Next
    End Function
    'Check if the shots have collided with an enemy.

    Private Function checkPlayerCollisions()
        For e As Integer = 0 To movingEnemies.Length - 1
            Dim enemy = movingEnemies(e)

            'For playerX As Integer = playe

        Next
    End Function

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
        Dim OGSize = If(arrayName = "shots", shots, movingEnemies).Length
        If arrayName = "shots" Then
            Dim originalSize As Integer = shots.Length - 1
            shots(index) = shots(shots.Length - 1)
            ReDim Preserve shots(originalSize - 1)
        ElseIf arrayName = "movingEnemies" Then
            Dim originalSize As Integer = movingEnemies.Length - 1
            movingEnemies(index) = movingEnemies(movingEnemies.Length - 1)
            ReDim Preserve movingEnemies(originalSize - 1)
        End If
    End Function
    'Remove an element from the specified array and index.

    ''' <summary>
    ''' This function updates <c>player.loc</c> by deleting the last value in the array, shifting the remaining down the index and adding <paramref name="newElement"/> to the start.
    ''' </summary>
    ''' <param name="newElement">is the new location</param>
    ''' <returns></returns>
    Private Function updateplayerLoc(newElement As Point)
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
    Public Property maxHealth As Integer 'the maximum health the player can have.
    'Public Property powerup As String
End Class
'Class to store the player information.
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
'Class to store the resize values of each side.