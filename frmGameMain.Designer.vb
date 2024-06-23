<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGameMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGameMain))
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.tmrShot = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSquareE = New System.Windows.Forms.Timer(Me.components)
        Me.tmrShrink = New System.Windows.Forms.Timer(Me.components)
        Me.lblHealth = New System.Windows.Forms.Label()
        Me.tmrCircleE = New System.Windows.Forms.Timer(Me.components)
        Me.tmrTriE = New System.Windows.Forms.Timer(Me.components)
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        Me.tmrBoss = New System.Windows.Forms.Timer(Me.components)
        Me.lblToolTip = New System.Windows.Forms.Label()
        Me.lblXP = New System.Windows.Forms.Label()
        Me.lblTick = New System.Windows.Forms.Label()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrTick
        '
        Me.tmrTick.Interval = 1
        '
        'tmrShot
        '
        Me.tmrShot.Enabled = True
        Me.tmrShot.Interval = 700
        '
        'tmrSquareE
        '
        Me.tmrSquareE.Interval = 1800
        '
        'tmrShrink
        '
        '
        'lblHealth
        '
        Me.lblHealth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHealth.AutoSize = True
        Me.lblHealth.Font = New System.Drawing.Font("Boldena", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHealth.ForeColor = System.Drawing.Color.White
        Me.lblHealth.Location = New System.Drawing.Point(303, 9)
        Me.lblHealth.Name = "lblHealth"
        Me.lblHealth.Size = New System.Drawing.Size(62, 24)
        Me.lblHealth.TabIndex = 1
        Me.lblHealth.Text = "10/10"
        Me.lblHealth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmrCircleE
        '
        Me.tmrCircleE.Interval = 600
        '
        'tmrTriE
        '
        Me.tmrTriE.Interval = 3000
        '
        'picCanvas
        '
        Me.picCanvas.BackColor = System.Drawing.Color.Transparent
        Me.picCanvas.Location = New System.Drawing.Point(1, 0)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(43690, 43690)
        Me.picCanvas.TabIndex = 0
        Me.picCanvas.TabStop = False
        '
        'tmrBoss
        '
        Me.tmrBoss.Interval = 4000
        '
        'lblToolTip
        '
        Me.lblToolTip.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblToolTip.AutoSize = True
        Me.lblToolTip.Font = New System.Drawing.Font("Varela Round", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToolTip.ForeColor = System.Drawing.Color.White
        Me.lblToolTip.Location = New System.Drawing.Point(91, 354)
        Me.lblToolTip.Name = "lblToolTip"
        Me.lblToolTip.Size = New System.Drawing.Size(180, 13)
        Me.lblToolTip.TabIndex = 2
        Me.lblToolTip.Text = "use the arrows or WASD to move"
        Me.lblToolTip.Visible = False
        '
        'lblXP
        '
        Me.lblXP.AutoSize = True
        Me.lblXP.Font = New System.Drawing.Font("Boldena", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblXP.ForeColor = System.Drawing.Color.White
        Me.lblXP.Location = New System.Drawing.Point(28, 9)
        Me.lblXP.Name = "lblXP"
        Me.lblXP.Size = New System.Drawing.Size(23, 24)
        Me.lblXP.TabIndex = 3
        Me.lblXP.Text = "0"
        Me.lblXP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTick
        '
        Me.lblTick.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTick.AutoSize = True
        Me.lblTick.Font = New System.Drawing.Font("Boldena", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTick.ForeColor = System.Drawing.Color.White
        Me.lblTick.Location = New System.Drawing.Point(12, 345)
        Me.lblTick.Name = "lblTick"
        Me.lblTick.Size = New System.Drawing.Size(23, 24)
        Me.lblTick.TabIndex = 4
        Me.lblTick.Text = "0"
        Me.lblTick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmGameMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(374, 376)
        Me.Controls.Add(Me.lblTick)
        Me.Controls.Add(Me.lblXP)
        Me.Controls.Add(Me.lblToolTip)
        Me.Controls.Add(Me.lblHealth)
        Me.Controls.Add(Me.picCanvas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGameMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WindowBound"
        Me.TopMost = True
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents picCanvas As PictureBox
    Friend WithEvents tmrTick As Timer
    Friend WithEvents tmrShot As Timer
    Friend WithEvents tmrSquareE As Timer
    Friend WithEvents tmrShrink As Timer
    Friend WithEvents lblHealth As Label
    Friend WithEvents tmrCircleE As Timer
    Friend WithEvents tmrTriE As Timer
    Friend WithEvents tmrBoss As Timer
    Friend WithEvents lblToolTip As Label
    Friend WithEvents lblXP As Label
    Friend WithEvents lblTick As Label
End Class
