<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStart))
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.mediaIN = New AxWMPLib.AxWindowsMediaPlayer()
        Me.mediaOUT = New AxWMPLib.AxWindowsMediaPlayer()
        Me.picBTNplay = New System.Windows.Forms.PictureBox()
        Me.picBTNexit = New System.Windows.Forms.PictureBox()
        Me.picBTNsettings = New System.Windows.Forms.PictureBox()
        Me.tmrFade = New System.Windows.Forms.Timer(Me.components)
        CType(Me.mediaIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mediaOUT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBTNplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBTNexit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBTNsettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrTick
        '
        Me.tmrTick.Enabled = True
        Me.tmrTick.Interval = 1
        '
        'mediaIN
        '
        Me.mediaIN.Enabled = True
        Me.mediaIN.Location = New System.Drawing.Point(12, 22)
        Me.mediaIN.Name = "mediaIN"
        Me.mediaIN.OcxState = CType(resources.GetObject("mediaIN.OcxState"), System.Windows.Forms.AxHost.State)
        Me.mediaIN.Size = New System.Drawing.Size(631, 416)
        Me.mediaIN.TabIndex = 0
        '
        'mediaOUT
        '
        Me.mediaOUT.Enabled = True
        Me.mediaOUT.Location = New System.Drawing.Point(12, 22)
        Me.mediaOUT.Name = "mediaOUT"
        Me.mediaOUT.OcxState = CType(resources.GetObject("mediaOUT.OcxState"), System.Windows.Forms.AxHost.State)
        Me.mediaOUT.Size = New System.Drawing.Size(631, 416)
        Me.mediaOUT.TabIndex = 5
        Me.mediaOUT.Visible = False
        '
        'picBTNplay
        '
        Me.picBTNplay.Image = CType(resources.GetObject("picBTNplay.Image"), System.Drawing.Image)
        Me.picBTNplay.Location = New System.Drawing.Point(258, 307)
        Me.picBTNplay.Name = "picBTNplay"
        Me.picBTNplay.Size = New System.Drawing.Size(133, 26)
        Me.picBTNplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBTNplay.TabIndex = 1
        Me.picBTNplay.TabStop = False
        Me.picBTNplay.Visible = False
        '
        'picBTNexit
        '
        Me.picBTNexit.Image = Global.WindowBound.My.Resources.Resources.BTN_exit
        Me.picBTNexit.Location = New System.Drawing.Point(258, 371)
        Me.picBTNexit.Name = "picBTNexit"
        Me.picBTNexit.Size = New System.Drawing.Size(133, 26)
        Me.picBTNexit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBTNexit.TabIndex = 3
        Me.picBTNexit.TabStop = False
        Me.picBTNexit.Visible = False
        '
        'picBTNsettings
        '
        Me.picBTNsettings.Image = Global.WindowBound.My.Resources.Resources.BTN_settings
        Me.picBTNsettings.Location = New System.Drawing.Point(258, 339)
        Me.picBTNsettings.Name = "picBTNsettings"
        Me.picBTNsettings.Size = New System.Drawing.Size(133, 26)
        Me.picBTNsettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBTNsettings.TabIndex = 2
        Me.picBTNsettings.TabStop = False
        Me.picBTNsettings.Visible = False
        '
        'tmrFade
        '
        Me.tmrFade.Interval = 22
        '
        'frmStart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(654, 450)
        Me.Controls.Add(Me.picBTNplay)
        Me.Controls.Add(Me.picBTNexit)
        Me.Controls.Add(Me.picBTNsettings)
        Me.Controls.Add(Me.mediaIN)
        Me.Controls.Add(Me.mediaOUT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStart"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = resources.GetString("$this.Text")
        CType(Me.mediaIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mediaOUT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBTNplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBTNexit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBTNsettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tmrTick As Timer
    Friend WithEvents mediaIN As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents picBTNplay As PictureBox
    Friend WithEvents picBTNsettings As PictureBox
    Friend WithEvents picBTNexit As PictureBox
    Friend WithEvents mediaOUT As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents tmrFade As Timer
End Class
