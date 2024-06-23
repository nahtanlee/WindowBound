<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGameShop
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.picUpgrade1 = New System.Windows.Forms.PictureBox()
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.lblTip1 = New System.Windows.Forms.Label()
        CType(Me.picUpgrade1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Presario", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(262, 19)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(133, 38)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "SHOP"
        '
        'picUpgrade1
        '
        Me.picUpgrade1.Image = Global.WindowBound.My.Resources.Resources.UPG__5_lives_B
        Me.picUpgrade1.InitialImage = Global.WindowBound.My.Resources.Resources.UPG__5_lives_B
        Me.picUpgrade1.Location = New System.Drawing.Point(26, 84)
        Me.picUpgrade1.Name = "picUpgrade1"
        Me.picUpgrade1.Size = New System.Drawing.Size(223, 275)
        Me.picUpgrade1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUpgrade1.TabIndex = 2
        Me.picUpgrade1.TabStop = False
        '
        'tmrTick
        '
        Me.tmrTick.Interval = 1
        '
        'lblTip1
        '
        Me.lblTip1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblTip1.AutoSize = True
        Me.lblTip1.Font = New System.Drawing.Font("Varela Round", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTip1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.lblTip1.Location = New System.Drawing.Point(211, 368)
        Me.lblTip1.Name = "lblTip1"
        Me.lblTip1.Size = New System.Drawing.Size(211, 13)
        Me.lblTip1.TabIndex = 3
        Me.lblTip1.Text = "press the spacebar to resume the game"
        '
        'frmGameShop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(645, 390)
        Me.Controls.Add(Me.lblTip1)
        Me.Controls.Add(Me.picUpgrade1)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGameShop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Shop"
        Me.TopMost = True
        CType(Me.picUpgrade1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents picUpgrade1 As PictureBox
    Friend WithEvents tmrTick As Timer
    Friend WithEvents lblTip1 As Label
End Class
