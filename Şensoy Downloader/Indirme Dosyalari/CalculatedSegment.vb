
<Serializable> Public Structure CalculatedSegment
    Private m_startPosition As Long
    Private m_endPosition As Long

    Public ReadOnly Property StartPosition() As Long
        Get
            Return m_startPosition
        End Get
    End Property

    Public ReadOnly Property EndPosition() As Long
        Get
            Return m_endPosition
        End Get
    End Property

    Public Sub New(startPos As Long, endPos As Long)
        Me.m_endPosition = endPos
        Me.m_startPosition = startPos
    End Sub
End Structure
