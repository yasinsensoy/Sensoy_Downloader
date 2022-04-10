
Partial Class Proxy
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
        Me.chkUseProxy = New System.Windows.Forms.CheckBox()
        Me.txtProxtAddress = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.numProxyPort = New System.Windows.Forms.NumericUpDown()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtDomain = New System.Windows.Forms.TextBox()
        Me.chkBypass = New System.Windows.Forms.CheckBox()
        DirectCast(Me.numProxyPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' chkUseProxy
        ' 
        Me.chkUseProxy.AutoSize = True
        Me.chkUseProxy.Location = New System.Drawing.Point(0, 3)
        Me.chkUseProxy.Name = "chkUseProxy"
        Me.chkUseProxy.Size = New System.Drawing.Size(73, 17)
        Me.chkUseProxy.TabIndex = 0
        Me.chkUseProxy.Text = "Use proxy"
        Me.chkUseProxy.UseVisualStyleBackColor = True
        ' 
        ' txtProxtAddress
        ' 
        Me.txtProxtAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProxtAddress.Location = New System.Drawing.Point(1, 43)
        Me.txtProxtAddress.Name = "txtProxtAddress"
        Me.txtProxtAddress.Size = New System.Drawing.Size(306, 20)
        Me.txtProxtAddress.TabIndex = 2
        ' 
        ' label1
        ' 
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(0, 27)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(77, 13)
        Me.label1.TabIndex = 1
        Me.label1.Text = "Proxy Address:"
        ' 
        ' label2
        ' 
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(0, 73)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(58, 13)
        Me.label2.TabIndex = 3
        Me.label2.Text = "Proxy Port:"
        ' 
        ' numProxyPort
        ' 
        Me.numProxyPort.Location = New System.Drawing.Point(3, 90)
        Me.numProxyPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
        Me.numProxyPort.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numProxyPort.Name = "numProxyPort"
        Me.numProxyPort.Size = New System.Drawing.Size(88, 20)
        Me.numProxyPort.TabIndex = 4
        Me.numProxyPort.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' label3
        ' 
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(1, 169)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(63, 13)
        Me.label3.TabIndex = 6
        Me.label3.Text = "User Name:"
        ' 
        ' txtUsername
        ' 
        Me.txtUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUsername.Location = New System.Drawing.Point(2, 185)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(306, 20)
        Me.txtUsername.TabIndex = 7
        ' 
        ' label4
        ' 
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(0, 211)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(56, 13)
        Me.label4.TabIndex = 8
        Me.label4.Text = "Password:"
        ' 
        ' txtPassword
        ' 
        Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Location = New System.Drawing.Point(2, 227)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(306, 20)
        Me.txtPassword.TabIndex = 9
        ' 
        ' label5
        ' 
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(0, 258)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(46, 13)
        Me.label5.TabIndex = 10
        Me.label5.Text = "Domain:"
        ' 
        ' txtDomain
        ' 
        Me.txtDomain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDomain.Location = New System.Drawing.Point(1, 274)
        Me.txtDomain.Name = "txtDomain"
        Me.txtDomain.Size = New System.Drawing.Size(306, 20)
        Me.txtDomain.TabIndex = 11
        ' 
        ' chkBypass
        ' 
        Me.chkBypass.AutoSize = True
        Me.chkBypass.Location = New System.Drawing.Point(1, 131)
        Me.chkBypass.Name = "chkBypass"
        Me.chkBypass.Size = New System.Drawing.Size(100, 17)
        Me.chkBypass.TabIndex = 5
        Me.chkBypass.Text = "Bypass on local"
        Me.chkBypass.UseVisualStyleBackColor = True
        ' 
        ' Proxy
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtDomain)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.Controls.Add(Me.txtProxtAddress)
        Me.Controls.Add(Me.chkBypass)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.numProxyPort)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.chkUseProxy)
        Me.Name = "Proxy"
        Me.Size = New System.Drawing.Size(308, 308)
        DirectCast(Me.numProxyPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private WithEvents chkUseProxy As System.Windows.Forms.CheckBox
    Private WithEvents txtProxtAddress As System.Windows.Forms.TextBox
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents numProxyPort As System.Windows.Forms.NumericUpDown
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txtUsername As System.Windows.Forms.TextBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents txtDomain As System.Windows.Forms.TextBox
    Private WithEvents chkBypass As System.Windows.Forms.CheckBox
End Class
