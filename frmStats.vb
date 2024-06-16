Public Class frmStats
    Private Sub frmStats_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        frmStart.Show()
    End Sub

    Private Sub frmStats_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            Me.Close()
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles statTitle1.Click

    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles statVal1.Click

    End Sub
End Class