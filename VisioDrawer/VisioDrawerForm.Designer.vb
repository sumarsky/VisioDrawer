<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VisioDrawerForm
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
        Me.btnGenerateDefault = New System.Windows.Forms.Button()
        Me.btnOpenFile = New System.Windows.Forms.Button()
        Me.btnReadXml = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnGenerateDefault
        '
        Me.btnGenerateDefault.Location = New System.Drawing.Point(208, 64)
        Me.btnGenerateDefault.Name = "btnGenerateDefault"
        Me.btnGenerateDefault.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerateDefault.TabIndex = 0
        Me.btnGenerateDefault.Text = "Gen Default"
        Me.btnGenerateDefault.UseVisualStyleBackColor = True
        '
        'btnOpenFile
        '
        Me.btnOpenFile.Location = New System.Drawing.Point(208, 88)
        Me.btnOpenFile.Name = "btnOpenFile"
        Me.btnOpenFile.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenFile.TabIndex = 1
        Me.btnOpenFile.Text = "Open File"
        Me.btnOpenFile.UseVisualStyleBackColor = True
        '
        'btnReadXml
        '
        Me.btnReadXml.Location = New System.Drawing.Point(208, 112)
        Me.btnReadXml.Name = "btnReadXml"
        Me.btnReadXml.Size = New System.Drawing.Size(75, 23)
        Me.btnReadXml.TabIndex = 2
        Me.btnReadXml.Text = "Read Xml"
        Me.btnReadXml.UseVisualStyleBackColor = True
        '
        'VisioDrawerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 441)
        Me.Controls.Add(Me.btnReadXml)
        Me.Controls.Add(Me.btnOpenFile)
        Me.Controls.Add(Me.btnGenerateDefault)
        Me.Name = "VisioDrawerForm"
        Me.Text = "Visio Drawer"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerateDefault As System.Windows.Forms.Button
    Friend WithEvents btnOpenFile As System.Windows.Forms.Button
    Friend WithEvents btnReadXml As System.Windows.Forms.Button

End Class
