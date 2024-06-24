<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblAutoFireTitle = New System.Windows.Forms.Label()
        Me.lblAutoFireDesc = New System.Windows.Forms.Label()
        Me.lblClearDesc = New System.Windows.Forms.Label()
        Me.lblClearTitle = New System.Windows.Forms.Label()
        Me.picClear = New System.Windows.Forms.PictureBox()
        Me.picAutoFireTgl = New System.Windows.Forms.PictureBox()
        Me.picBackground = New System.Windows.Forms.PictureBox()
        Me.lblTransparentDesc = New System.Windows.Forms.Label()
        Me.picTransparentTgl = New System.Windows.Forms.PictureBox()
        Me.lblTransparentTitle = New System.Windows.Forms.Label()
        CType(Me.picClear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAutoFireTgl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBackground, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTransparentTgl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Presario", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(71, 18)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(228, 38)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Settings"
        '
        'lblAutoFireTitle
        '
        Me.lblAutoFireTitle.AutoSize = True
        Me.lblAutoFireTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblAutoFireTitle.Font = New System.Drawing.Font("Presario", 14.0!)
        Me.lblAutoFireTitle.ForeColor = System.Drawing.Color.White
        Me.lblAutoFireTitle.Location = New System.Drawing.Point(39, 98)
        Me.lblAutoFireTitle.Name = "lblAutoFireTitle"
        Me.lblAutoFireTitle.Size = New System.Drawing.Size(132, 21)
        Me.lblAutoFireTitle.TabIndex = 2
        Me.lblAutoFireTitle.Text = "Auto Fire"
        '
        'lblAutoFireDesc
        '
        Me.lblAutoFireDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblAutoFireDesc.Font = New System.Drawing.Font("Varela Round", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoFireDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.lblAutoFireDesc.Location = New System.Drawing.Point(41, 126)
        Me.lblAutoFireDesc.Name = "lblAutoFireDesc"
        Me.lblAutoFireDesc.Size = New System.Drawing.Size(191, 26)
        Me.lblAutoFireDesc.TabIndex = 4
        Me.lblAutoFireDesc.Text = "Continue firing shots when the left mouse button is held down."
        '
        'lblClearDesc
        '
        Me.lblClearDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblClearDesc.Font = New System.Drawing.Font("Varela Round", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClearDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.lblClearDesc.Location = New System.Drawing.Point(41, 282)
        Me.lblClearDesc.Name = "lblClearDesc"
        Me.lblClearDesc.Size = New System.Drawing.Size(191, 26)
        Me.lblClearDesc.TabIndex = 7
        Me.lblClearDesc.Text = "Clear all data including statistics and settings."
        '
        'lblClearTitle
        '
        Me.lblClearTitle.AutoSize = True
        Me.lblClearTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblClearTitle.Font = New System.Drawing.Font("Presario", 14.0!)
        Me.lblClearTitle.ForeColor = System.Drawing.Color.White
        Me.lblClearTitle.Location = New System.Drawing.Point(39, 254)
        Me.lblClearTitle.Name = "lblClearTitle"
        Me.lblClearTitle.Size = New System.Drawing.Size(164, 21)
        Me.lblClearTitle.TabIndex = 5
        Me.lblClearTitle.Text = "Clear  Data"
        '
        'picClear
        '
        Me.picClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.picClear.Image = Global.WindowBound.My.Resources.Resources.BTN_clear_C
        Me.picClear.Location = New System.Drawing.Point(282, 263)
        Me.picClear.Name = "picClear"
        Me.picClear.Size = New System.Drawing.Size(38, 41)
        Me.picClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picClear.TabIndex = 6
        Me.picClear.TabStop = False
        '
        'picAutoFireTgl
        '
        Me.picAutoFireTgl.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.picAutoFireTgl.Image = Global.WindowBound.My.Resources.Resources.TGL_true
        Me.picAutoFireTgl.Location = New System.Drawing.Point(282, 111)
        Me.picAutoFireTgl.Name = "picAutoFireTgl"
        Me.picAutoFireTgl.Size = New System.Drawing.Size(49, 27)
        Me.picAutoFireTgl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picAutoFireTgl.TabIndex = 3
        Me.picAutoFireTgl.TabStop = False
        '
        'picBackground
        '
        Me.picBackground.Image = Global.WindowBound.My.Resources.Resources.BGR_settings
        Me.picBackground.InitialImage = Global.WindowBound.My.Resources.Resources.BGR_settings
        Me.picBackground.Location = New System.Drawing.Point(23, 74)
        Me.picBackground.Name = "picBackground"
        Me.picBackground.Size = New System.Drawing.Size(330, 260)
        Me.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBackground.TabIndex = 8
        Me.picBackground.TabStop = False
        '
        'lblTransparentDesc
        '
        Me.lblTransparentDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblTransparentDesc.Font = New System.Drawing.Font("Varela Round", 8.249999!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransparentDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.lblTransparentDesc.Location = New System.Drawing.Point(41, 204)
        Me.lblTransparentDesc.Name = "lblTransparentDesc"
        Me.lblTransparentDesc.Size = New System.Drawing.Size(191, 26)
        Me.lblTransparentDesc.TabIndex = 11
        Me.lblTransparentDesc.Text = "Make the black backrgound of the game transparent."
        '
        'picTransparentTgl
        '
        Me.picTransparentTgl.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.picTransparentTgl.Image = Global.WindowBound.My.Resources.Resources.TGL_false
        Me.picTransparentTgl.Location = New System.Drawing.Point(282, 189)
        Me.picTransparentTgl.Name = "picTransparentTgl"
        Me.picTransparentTgl.Size = New System.Drawing.Size(49, 27)
        Me.picTransparentTgl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picTransparentTgl.TabIndex = 10
        Me.picTransparentTgl.TabStop = False
        '
        'lblTransparentTitle
        '
        Me.lblTransparentTitle.AutoSize = True
        Me.lblTransparentTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer), CType(CType(69, Byte), Integer))
        Me.lblTransparentTitle.Font = New System.Drawing.Font("Presario", 14.0!)
        Me.lblTransparentTitle.ForeColor = System.Drawing.Color.White
        Me.lblTransparentTitle.Location = New System.Drawing.Point(39, 176)
        Me.lblTransparentTitle.Name = "lblTransparentTitle"
        Me.lblTransparentTitle.Size = New System.Drawing.Size(200, 21)
        Me.lblTransparentTitle.TabIndex = 9
        Me.lblTransparentTitle.Text = "Transparency"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(376, 356)
        Me.Controls.Add(Me.lblTransparentDesc)
        Me.Controls.Add(Me.picTransparentTgl)
        Me.Controls.Add(Me.lblTransparentTitle)
        Me.Controls.Add(Me.lblClearDesc)
        Me.Controls.Add(Me.picClear)
        Me.Controls.Add(Me.lblClearTitle)
        Me.Controls.Add(Me.lblAutoFireDesc)
        Me.Controls.Add(Me.picAutoFireTgl)
        Me.Controls.Add(Me.lblAutoFireTitle)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.picBackground)
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSettings"
        CType(Me.picClear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAutoFireTgl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBackground, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTransparentTgl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblAutoFireTitle As Label
    Friend WithEvents picAutoFireTgl As PictureBox
    Friend WithEvents lblAutoFireDesc As Label
    Friend WithEvents lblClearDesc As Label
    Friend WithEvents picClear As PictureBox
    Friend WithEvents lblClearTitle As Label
    Friend WithEvents picBackground As PictureBox
    Friend WithEvents lblTransparentDesc As Label
    Friend WithEvents picTransparentTgl As PictureBox
    Friend WithEvents lblTransparentTitle As Label
End Class
