<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGamePause
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
        Me.picBTNresume = New System.Windows.Forms.PictureBox()
        Me.picBTNhome = New System.Windows.Forms.PictureBox()
        Me.lblTitle1 = New System.Windows.Forms.Label()
        Me.lblTitle2 = New System.Windows.Forms.Label()
        CType(Me.picBTNresume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBTNhome, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picBTNresume
        '
        Me.picBTNresume.Image = Global.WindowBound.My.Resources.Resources.BTN_resume
        Me.picBTNresume.Location = New System.Drawing.Point(110, 145)
        Me.picBTNresume.Name = "picBTNresume"
        Me.picBTNresume.Size = New System.Drawing.Size(133, 26)
        Me.picBTNresume.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBTNresume.TabIndex = 2
        Me.picBTNresume.TabStop = False
        '
        'picBTNhome
        '
        Me.picBTNhome.Image = Global.WindowBound.My.Resources.Resources.BTN_home
        Me.picBTNhome.Location = New System.Drawing.Point(110, 177)
        Me.picBTNhome.Name = "picBTNhome"
        Me.picBTNhome.Size = New System.Drawing.Size(133, 26)
        Me.picBTNhome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBTNhome.TabIndex = 3
        Me.picBTNhome.TabStop = False
        '
        'lblTitle1
        '
        Me.lblTitle1.AutoSize = True
        Me.lblTitle1.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle1.Font = New System.Drawing.Font("Presario", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle1.ForeColor = System.Drawing.Color.White
        Me.lblTitle1.Location = New System.Drawing.Point(105, 21)
        Me.lblTitle1.Name = "lblTitle1"
        Me.lblTitle1.Size = New System.Drawing.Size(140, 38)
        Me.lblTitle1.TabIndex = 4
        Me.lblTitle1.Text = "GAME"
        '
        'lblTitle2
        '
        Me.lblTitle2.AutoSize = True
        Me.lblTitle2.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle2.Font = New System.Drawing.Font("Presario", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle2.ForeColor = System.Drawing.Color.White
        Me.lblTitle2.Location = New System.Drawing.Point(79, 71)
        Me.lblTitle2.Name = "lblTitle2"
        Me.lblTitle2.Size = New System.Drawing.Size(191, 38)
        Me.lblTitle2.TabIndex = 5
        Me.lblTitle2.Text = "PAUSED"
        '
        'frmGamePause
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(359, 226)
        Me.Controls.Add(Me.lblTitle2)
        Me.Controls.Add(Me.lblTitle1)
        Me.Controls.Add(Me.picBTNhome)
        Me.Controls.Add(Me.picBTNresume)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGamePause"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game Paused"
        Me.TopMost = True
        CType(Me.picBTNresume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBTNhome, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents picBTNresume As PictureBox
    Friend WithEvents picBTNhome As PictureBox
    Friend WithEvents lblTitle1 As Label
    Friend WithEvents lblTitle2 As Label
End Class
