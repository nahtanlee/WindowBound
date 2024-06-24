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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGameShop))
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.picUpgrade1 = New System.Windows.Forms.PictureBox()
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.lblTip1 = New System.Windows.Forms.Label()
        Me.picUpgrade2 = New System.Windows.Forms.PictureBox()
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        Me.lblXP = New System.Windows.Forms.Label()
        CType(Me.picUpgrade1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picUpgrade2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Presario", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(140, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(133, 38)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "SHOP"
        '
        'picUpgrade1
        '
        Me.picUpgrade1.Image = Global.WindowBound.My.Resources.Resources.UPG__5_lives_B
        Me.picUpgrade1.InitialImage = Global.WindowBound.My.Resources.Resources.UPG__5_lives_B
        Me.picUpgrade1.Location = New System.Drawing.Point(25, 64)
        Me.picUpgrade1.Name = "picUpgrade1"
        Me.picUpgrade1.Size = New System.Drawing.Size(167, 206)
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
        Me.lblTip1.Location = New System.Drawing.Point(98, 287)
        Me.lblTip1.Name = "lblTip1"
        Me.lblTip1.Size = New System.Drawing.Size(211, 13)
        Me.lblTip1.TabIndex = 3
        Me.lblTip1.Text = "press the spacebar to resume the game"
        '
        'picUpgrade2
        '
        Me.picUpgrade2.Image = Global.WindowBound.My.Resources.Resources.UPG_full_lives_B
        Me.picUpgrade2.InitialImage = Global.WindowBound.My.Resources.Resources.UPG__5_lives_B
        Me.picUpgrade2.Location = New System.Drawing.Point(212, 64)
        Me.picUpgrade2.Name = "picUpgrade2"
        Me.picUpgrade2.Size = New System.Drawing.Size(167, 206)
        Me.picUpgrade2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picUpgrade2.TabIndex = 4
        Me.picUpgrade2.TabStop = False
        '
        'picCanvas
        '
        Me.picCanvas.Location = New System.Drawing.Point(0, -3)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(100, 50)
        Me.picCanvas.TabIndex = 5
        Me.picCanvas.TabStop = False
        '
        'lblXP
        '
        Me.lblXP.AutoSize = True
        Me.lblXP.Font = New System.Drawing.Font("Boldena", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXP.ForeColor = System.Drawing.Color.White
        Me.lblXP.Location = New System.Drawing.Point(28, 9)
        Me.lblXP.Name = "lblXP"
        Me.lblXP.Size = New System.Drawing.Size(23, 24)
        Me.lblXP.TabIndex = 6
        Me.lblXP.Text = "0"
        Me.lblXP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmGameShop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(406, 309)
        Me.Controls.Add(Me.lblXP)
        Me.Controls.Add(Me.picCanvas)
        Me.Controls.Add(Me.picUpgrade2)
        Me.Controls.Add(Me.lblTip1)
        Me.Controls.Add(Me.picUpgrade1)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGameShop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Shop"
        Me.TopMost = True
        CType(Me.picUpgrade1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picUpgrade2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents picUpgrade1 As PictureBox
    Friend WithEvents tmrTick As Timer
    Friend WithEvents lblTip1 As Label
    Friend WithEvents picUpgrade2 As PictureBox
    Friend WithEvents picCanvas As PictureBox
    Friend WithEvents lblXP As Label
End Class
