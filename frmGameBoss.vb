Public Class frmGameBoss
    Dim bossLoc As Point
    Dim bossRadius As Integer = 50
    Dim bossPoints(8) As Point

    Dim colors As New ColorPalette

    Private Sub frmGameBoss_Load(sender As Object, e As EventArgs) Handles Me.Load
        bossLoc = New Point(Me.Width / 2, Me.Height / 2)
        For i As Integer = 0 To 7
            bossPoints(i) = New Point(CInt(bossLoc.X + bossRadius * Math.Cos(i * ((2 * Math.PI / 8) + (15 * Math.PI / 180)))), CInt(bossLoc.X + bossRadius * Math.Sin(i * ((2 * Math.PI / 8) + (15 * Math.PI / 180)))))
        Next
        bossPoints(8) = bossPoints(0)

    End Sub
    Private Sub tmrTick_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        picCanvas.Invalidate()
    End Sub

    Private Sub picCanvas_Paint(sender As Object, e As PaintEventArgs) Handles picCanvas.Paint
        Using pen As New Pen(colors.red, 10)
            e.Graphics.DrawPolygon(pen, bossPoints)
        End Using
    End Sub


End Class