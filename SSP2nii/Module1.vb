Imports System.IO

Module Module1
    Sub Main(args() As String)

        Dim SourceFilePath As String = Nothing
        If args.Length < 1 Then
            Console.WriteLine("usage hogehoge.exe Source")
            Environment.Exit(1)
        Else
            SourceFilePath = args(0)
        End If

        If File.Exists(Path.ChangeExtension(SourceFilePath, ".img")) = False OrElse File.Exists(Path.ChangeExtension(SourceFilePath, ".hdr")) = False Then
            Console.WriteLine(SourceFilePath & "が見つかりません。")
            Environment.Exit(2)
        End If

        Dim Source As New clsInterFile
        Source.Read(SourceFilePath)
        Source.FlipDimension(0)
        Source.FlipDimension(1)
        Source.FlipDimension(2)

        Dim Dest As New clsNIfTIFile
        Dest.MatrixX = Source.MatrixX
        Dest.MatrixY = Source.MatrixY
        Dest.MatrixZ = Source.MatrixZ
        Dest.SizeX = Source.SizeX
        Dest.SizeY = Source.SizeY
        Dest.SizeZ = Source.SizeZ
        Dest.SetPixels(Source.GetPixels)
        Dest.SrowX(0) = Source.SizeX
        Dest.SrowY(1) = Source.SizeY
        Dest.SrowZ(2) = Source.SizeZ
        Dest.SrowX(3) = Math.Floor(Source.MatrixX * Source.SizeX / 2) * -1
        Dest.SrowY(3) = Math.Floor(Source.MatrixY * Source.SizeY / 2) * -1
        Dest.SrowZ(3) = Math.Floor(Source.MatrixZ * Source.SizeZ / 2) * -1

        Dest.Write(Path.ChangeExtension(SourceFilePath, ".nii"), False)
    End Sub

End Module
