Imports System.ComponentModel
Imports System.Drawing.Drawing2D

Partial Public Class BlockedProgressBar
    Inherits Control
    Private _blockList As List(Of Block)

    Public Sub New()
        InitializeComponent()
        _blockList = New List(Of Block)()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.DoubleBuffer, True)
    End Sub

    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property BlockList() As List(Of Block)
        Get
            Return _blockList
        End Get
        Set(value As List(Of Block))
            _blockList = value
            Me.Refresh()
        End Set
    End Property

    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        Dim Color1 As Color = ControlPaint.Dark(Me.ForeColor)
        Dim Color2 As Color = ControlPaint.Light(Me.ForeColor)
        Dim top As Long = ClientRectangle.Top + ClientRectangle.Height / 2 - 1
        Dim height As Long = ClientRectangle.Height - top
        DrawRectangleH(pe, top, height, Color2, Color1)
        top = ClientRectangle.Top
        height = ClientRectangle.Height / 2
        DrawRectangleH(pe, top, height, Color1, Color2)
        MyBase.OnPaint(pe)
    End Sub

    Private Sub DrawRectangleH(pe As PaintEventArgs, top As Integer, height As Integer, fromColor As Color, toColor As Color)
        Dim rect As New Rectangle(ClientRectangle.Left, top, ClientRectangle.Width, height)
        Dim brush As New LinearGradientBrush(rect, fromColor, toColor, LinearGradientMode.Vertical)
        If _blockList.Count > 0 Then
            Dim rects As RectangleF() = GetRectanglesH(top, height)
            If rects.Length > 0 Then
                pe.Graphics.FillRectangles(brush, rects)
            End If
        End If
    End Sub

    Private Function GetRectanglesH(top As Integer, height As Integer) As RectangleF()
        Dim rects As New List(Of RectangleF)()
        Dim pf As Single = ClientRectangle.Width / _blockList.Count
        For Each block As Block In _blockList
            Dim x As Single = _blockList.IndexOf(block) * pf
            If block.Ilerleme > 0 And block.Boyut > 0 Then
                Dim w As Single = (block.Ilerleme * pf) / block.Boyut
                rects.Add(New RectangleF(x, top, w, height))
            End If
        Next
        Return rects.ToArray()
    End Function
End Class

Public Class Block
    Public Sub New(boyut As Single, ilerleme As Single)
        Me.Boyut = boyut
        Me.Ilerleme = ilerleme
    End Sub

    Public Property Boyut() As Single
    Public Property Ilerleme() As Single
End Class
