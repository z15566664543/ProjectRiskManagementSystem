Imports System.Net

Namespace App
    Public Class ExtTextBox
        Inherits TextBox

        Protected Overrides Sub Render(writer As HtmlTextWriter)

            If Me.TextMode = TextBoxMode.MultiLine And Me.MaxLength > 0 Then
                writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, Me.MaxLength.ToString())
            End If

            Dim _Enabled As Boolean = Me.Enabled
            Dim _ReadOnly As Boolean = Me.ReadOnly

            If Me.Enabled = False Then
                _Enabled = False
                MyBase.Enabled = True
                Me.ReadOnly = True
                writer.AddStyleAttribute("background-color", "#E5E5E5")
                writer.AddStyleAttribute("border", "1px solid #C2C3C2")
            End If

            MyBase.Render(writer)

            Me.Enabled = _Enabled
            Me.ReadOnly = _ReadOnly

        End Sub
    End Class
End Namespace