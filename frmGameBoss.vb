﻿Public Class frmGameBoss
    Dim bossLoc As Point
    Dim bossRadius As Integer = 50
    Dim bossPoints(8) As Point

    Dim colors As New ColorPalette

    Private Sub frmGameBoss_Load(sender As Object, e As EventArgs) Handles Me.Load
        bossLoc = New Point(75, 80)
        For i As Integer = 0 To 7
            bossPoints(i) = New Point(CInt(bossLoc.X + bossRadius * Math.Cos(i * ((2 * Math.PI / 8) + (15 * Math.PI / 180)))), CInt(bossLoc.X + bossRadius * Math.Sin(i * ((2 * Math.PI / 8) + (15 * Math.PI / 180)))))
        Next
        bossPoints(8) = bossPoints(0)
        'Generate the points of the octagon.
        tmrTick.Enabled = True
    End Sub
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        picCanvas.Invalidate()
    End Sub

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
        'Draw the shots.

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