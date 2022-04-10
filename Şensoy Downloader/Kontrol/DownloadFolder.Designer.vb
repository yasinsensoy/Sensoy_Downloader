
Partial Class DownloadFolder
    ''' <summary> 
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary> 
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Component Designer generated code"

    ''' <summary> 
    ''' Required method for Designer support - do not modify 
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.btnSelAV = New System.Windows.Forms.Button()
        Me.txtSaveTo = New System.Windows.Forms.TextBox()
        Me.lblText = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnSelAV
        '
        Me.btnSelAV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelAV.ForeColor = System.Drawing.Color.Black
        Me.btnSelAV.Location = New System.Drawing.Point(444, 25)
        Me.btnSelAV.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSelAV.Name = "btnSelAV"
        Me.btnSelAV.Size = New System.Drawing.Size(29, 23)
        Me.btnSelAV.TabIndex = 2
        Me.btnSelAV.Text = "..."
        Me.btnSelAV.UseVisualStyleBackColor = True
        '
        'txtSaveTo
        '
        Me.txtSaveTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSaveTo.Location = New System.Drawing.Point(0, 25)
        Me.txtSaveTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSaveTo.Name = "txtSaveTo"
        Me.txtSaveTo.Size = New System.Drawing.Size(438, 23)
        Me.txtSaveTo.TabIndex = 1
        '
        'lblText
        '
        Me.lblText.AutoSize = True
        Me.lblText.Location = New System.Drawing.Point(0, 2)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(143, 17)
        Me.lblText.TabIndex = 0
        Me.lblText.Text = "Ýndirme yerini seçiniz:"
        '
        'DownloadFolder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnSelAV)
        Me.Controls.Add(Me.txtSaveTo)
        Me.Controls.Add(Me.lblText)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(162, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "DownloadFolder"
        Me.Size = New System.Drawing.Size(476, 55)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents btnSelAV As System.Windows.Forms.Button
    Private WithEvents txtSaveTo As System.Windows.Forms.TextBox
    Private WithEvents lblText As System.Windows.Forms.Label
End Class
