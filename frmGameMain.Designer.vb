﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.tmrShot = New System.Windows.Forms.Timer(Me.components)
        Me.tmrSquareE = New System.Windows.Forms.Timer(Me.components)
        Me.tmrShrink = New System.Windows.Forms.Timer(Me.components)
        Me.lblHealth = New System.Windows.Forms.Label()
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picCanvas
        '
        Me.picCanvas.BackColor = System.Drawing.Color.Transparent
        Me.picCanvas.Location = New System.Drawing.Point(1, 0)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(1350, 781)
        Me.picCanvas.TabIndex = 0
        Me.picCanvas.TabStop = False
        '
        'tmrTick
        '
        Me.tmrTick.Interval = 1
        '
        'tmrShot
        '
        Me.tmrShot.Interval = 1000
        '
        'tmrSquareE
        '
        Me.tmrSquareE.Interval = 500
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
        Me.lblHealth.Location = New System.Drawing.Point(293, 9)
        Me.lblHealth.Name = "lblHealth"
        Me.lblHealth.Size = New System.Drawing.Size(52, 24)
        Me.lblHealth.TabIndex = 1
        Me.lblHealth.Text = "5/10"
        '
        'frmGameMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(357, 322)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblHealth)
        Me.Controls.Add(Me.picCanvas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmGameMain"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WindowBound"
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
End Class
