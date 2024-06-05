﻿Public Class frmGameBoss
    Dim bossLoc As Point
    Dim bossRadius As Integer = 50
    Dim bossPoints(8) As Point

    Dim speed As Integer = 3
    Dim colors As New ColorPalette

    Public shots() As Tuple(Of Point, Point, Point, Integer)
    'An array of tuples that stores the [1] current location, [2] end destination, [3] the calculated movement and [4] the size of the shots.
    Dim shotCount As Integer = 0

    Private Sub frmGameBoss_Load(sender As Object, e As EventArgs) Handles Me.Load
        bossLoc = New Point(Me.Width / 2 - 15, Me.Height / 2 - 15)
        For i As Integer = 0 To 7
            bossPoints(i) = New Point(CInt(bossLoc.X + bossRadius * Math.Cos(i * ((2 * Math.PI / 12) + (15 * Math.PI / 180)))), CInt(bossLoc.Y + bossRadius * Math.Sin(i * ((2 * Math.PI / 12) + (15 * Math.PI / 180)))))
        Next
        bossPoints(8) = bossPoints(0)
        'Generate the points of the octagon.
        tmrTick.Enabled = True
        tmrShot.Enabled = True
        'Start the timers.
    End Sub
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        picCanvas.Invalidate()
    End Sub

    Private Sub tmrShot_Tick(sender As Object, e As EventArgs) Handles tmrShot.Tick
        If shotCount < 8 Then
            shotCount += 1
        Else
            shotCount = 1
        End If
        For i As Integer = 1 To 8
            'Dim angle As Double = ((45 * i)) / 360
            'Debug.WriteLine($"{i}: {angle} - {Math.Floor(angle)} = {angle - Math.Floor(angle)}")
            Dim cycle As Integer = i
            For v As Integer = 1 To shotCount
                Select Case cycle
                    Case 1 To 7
                        cycle += 1
                    Case 8
                        cycle = 1
                End Select
            Next

            ' angle -= Math.Floor(angle)
            Dim move As Point
            'Calculate the angle of the shot where 0 is 0 degrees and 1 is 360 degrees.
            Select Case cycle
                Case 1
                    move.X = 0
                    move.Y = -(1 * speed)
                Case 2
                    move.X = 0.5 * speed
                    move.Y = -(0.5 * speed)
                Case 3
                    move.X = 1 * speed
                    move.Y = 0
                Case 4
                    move.X = 0.5 * speed
                    move.Y = 0.5 * speed
                Case 5
                    move.X = 0
                    move.Y = 1 * speed
                Case 6
                    move.X = -(0.5 * speed)
                    move.Y = 0.5 * speed
                Case 7
                    move.X = -(1 * speed)
                    move.Y = 0
                Case 8
                    move.X = -(0.5 * speed)
                    move.Y = -(0.5 * speed)
            End Select
            'Calculate the movement per tick of the shot.

            'Select Case angle
            '    Case Is <= 0.25
            '        move.X = (4 * angle) * speed
            '        move.Y = ((4 * angle) - 1) * speed
            '    Case Is <= 0.5
            '        move.Y = (1 - 4 * (angle - 0.25)) * speed
            '        move.X = (4 * (angle - 0.25)) * speed
            '    Case Is <= 0.75
            '        move.X = (-4 * (angle - 0.5)) * speed
            '        move.Y = (1 - 4 * (angle - 0.5)) * speed
            '    Case Is <= 1
            '        move.Y = (-1 + 4 * (angle - 0.75)) * speed
            '        move.X = (-4 * (angle - 0.75)) * speed
            'End Select
            ''Calculate the movement per tick of the shot.

            Debug.WriteLine($"shotCount: {shotCount}, i: {i}, cycle: {cycle}, move: {move}")

            If shots Is Nothing Then
                    shots = {New Tuple(Of Point, Point, Point, Integer)(PointToScreen(New Point(bossLoc.X - 5, bossLoc.Y - 5)), Nothing, move, 8)}
                Else
                    ReDim Preserve shots(shots.Length)
                    shots(shots.Length - 1) = New Tuple(Of Point, Point, Point, Integer)(PointToScreen(New Point(bossLoc.X - 5, bossLoc.Y - 5)), Nothing, move, 8)
                End If
                'Add a new shot
            Next
        'Add 8 shots firing outwards.

    End Sub
    'Add 8 new shots

    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
        Using pen As New Pen(colors.red, 10)
            e.Graphics.DrawPolygon(pen, bossPoints)
        End Using

        If frmGameMain.shots IsNot Nothing Then
            For i As Integer = 0 To (frmGameMain.shots.Length - 1)
                Dim shot = frmGameMain.shots(i)
                shot = New Tuple(Of Point, Point, Point, Integer)(New Point(shot.Item1.X + shot.Item3.X, shot.Item1.Y + shot.Item3.Y), shot.Item2, shot.Item3, shot.Item4)
                Using pen As New SolidBrush(colors.primary)
                    e.Graphics.FillEllipse(pen, New Rectangle(PointToClient(shot.Item1).X, PointToClient(shot.Item1).Y, shot.Item4, shot.Item4))
                End Using
                frmGameMain.shots(i) = shot
            Next
            'Draw each shot and update its position.
        End If
        'Draw the shots from the player.

        If shots IsNot Nothing Then
            For i As Integer = 0 To (shots.Length - 1)
                Dim shot = shots(i)
                shot = New Tuple(Of Point, Point, Point, Integer)(New Point(shot.Item1.X + shot.Item3.X, shot.Item1.Y + shot.Item3.Y), shot.Item2, shot.Item3, shot.Item4)
                Using pen As New Pen(colors.red, 2)
                    e.Graphics.DrawEllipse(pen, New Rectangle(PointToClient(shot.Item1).X, PointToClient(shot.Item1).Y, shot.Item4, shot.Item4))
                End Using
                shots(i) = shot
            Next
            'Draw each shot and update its position.
        End If
        'Draw the shots from the boss.

        If frmGameMain.enemies IsNot Nothing Then
            For i As Integer = 0 To (frmGameMain.enemies.Length - 1)
                Dim pen As Pen
                frmGameMain.enemies(i).loc = New Point(frmGameMain.enemies(i).loc.X + frmGameMain.enemies(i).mov.X, frmGameMain.enemies(i).loc.Y + frmGameMain.enemies(i).mov.Y)
                'Update the position of the enemy

                Select Case frmGameMain.enemies(i).type
                    Case "square"
                        pen = New Pen(If(frmGameMain.enemies(i).white > 0, colors.secondary, colors.blue), 7)
                        e.Graphics.DrawRectangle(pen, New Rectangle(PointToClient(frmGameMain.enemies(i).loc), New Size(frmGameMain.enemies(i).size, frmGameMain.enemies(i).size)))
                        'Draw a blue square.
                    Case "circle"
                        pen = New Pen(If(frmGameMain.enemies(i).white > 0, colors.secondary, colors.green), 5)
                        e.Graphics.DrawEllipse(pen, New Rectangle(PointToClient(frmGameMain.enemies(i).loc), New Size(frmGameMain.enemies(i).size, frmGameMain.enemies(i).size)))
                        'Draw a green circle.
                    Case "triangle"
                        pen = New Pen(If(frmGameMain.enemies(i).white > 0, colors.secondary, colors.yellow), 7)
                        e.Graphics.DrawPolygon(pen, {New Point(PointToClient(frmGameMain.enemies(i).loc).X + (frmGameMain.enemies(i).size / 2), PointToClient(frmGameMain.enemies(i).loc).Y), New Point(PointToClient(frmGameMain.enemies(i).loc).X, PointToClient(frmGameMain.enemies(i).loc).Y + frmGameMain.enemies(i).size), New Point(PointToClient(frmGameMain.enemies(i).loc).X + frmGameMain.enemies(i).size, PointToClient(frmGameMain.enemies(i).loc).Y + frmGameMain.enemies(i).size)})
                End Select

                frmGameMain.calcMove(frmGameMain.enemies(i).type, i)
            Next
            'Draw each moving enemy and update and recalculate its position.
        End If
        'Draw the moving enemies and update properties.

        If frmGameMain.player.red > 0 Then
            Using pen As New Pen(colors.red, 14)
                e.Graphics.DrawEllipse(pen, CInt(PointToClient(frmGameMain.player.loc(9)).X - frmGameMain.player.size / 2), CInt(PointToClient(frmGameMain.player.loc(9)).Y - (frmGameMain.player.size / 2)), frmGameMain.player.size, frmGameMain.player.size)
            End Using
        Else
            Using pen As New Pen(colors.primary, 14)
                e.Graphics.DrawEllipse(pen, CInt(PointToClient(frmGameMain.player.loc(9)).X - frmGameMain.player.size / 2), CInt(PointToClient(frmGameMain.player.loc(9)).Y - (frmGameMain.player.size / 2)), frmGameMain.player.size, frmGameMain.player.size)
            End Using
        End If
        Using brush As New SolidBrush(colors.background)
            e.Graphics.FillRectangle(brush, New Rectangle(PointToClient(New Point(frmGameMain.player.loc(9).X - (frmGameMain.player.size / 2) + (frmGameMain.player.size / 20), frmGameMain.player.loc(9).Y - (frmGameMain.player.size / 2) + (frmGameMain.player.size / 20))), New Size(frmGameMain.player.size - (frmGameMain.player.size / 10), frmGameMain.player.size - (frmGameMain.player.size / 10))))
        End Using
        'Draw the player circle (circle + square).
    End Sub
End Class