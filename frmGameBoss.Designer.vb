<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGameBoss
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGameBoss))
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.tmrShot = New System.Windows.Forms.Timer(Me.components)
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrTick
        '
        Me.tmrTick.Interval = 1
        '
        'tmrShot
        '
        Me.tmrShot.Interval = 4000
        '
        'picCanvas
        '
        Me.picCanvas.Location = New System.Drawing.Point(0, 0)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(657, 518)
        Me.picCanvas.TabIndex = 0
        Me.picCanvas.TabStop = False
        '
        'frmGameBoss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(225, 213)
        Me.Controls.Add(Me.picCanvas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGameBoss"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Boss"
        Me.TopMost = True
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tmrTick As Timer
    Friend WithEvents picCanvas As PictureBox
    Friend WithEvents tmrShot As Timer
End Class
