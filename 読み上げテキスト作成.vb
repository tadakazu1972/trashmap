Sub 作成実行()
    
    On Error GoTo ErrTrap
    
    Dim dpCsvRead As String
    Dim dpCsvWrite As String
    dpCsvRead = ThisWorkbook.Path & "\csv(utf-8)"
    dpCsvWrite = ThisWorkbook.Path & "\text\ward"
    
    Dim FSO As Scripting.FileSystemObject
    Set FSO = CreateObject("Scripting.FileSystemObject")
    
    Dim htmlTemplate As String
    Dim htmlResult As String
    htmlTemplate = readTextFile(ThisWorkbook.Path & "\" & "template.html")
    
    Dim s As String
    Dim f As Scripting.File
    Dim csv As String
    Dim csvRows As Variant
    Dim csvRow As String
    Dim csvRowIndex As Long
    Dim csvCells As Variant
    Dim csvCellsOld As Variant
    For Each f In FSO.GetFolder(dpCsvRead).Files
        
        If FSO.GetExtensionName(f.Path) = "csv" Then
            
            '初期化
            s = ""
            csv = ""
            csvRows = Empty
            csvRow = ""
            csvRowIndex = 0
            csvCells = Empty
            csvCellsOld = Empty
            
            'csv読み込み
            csv = readTextFile(f.Path)
            csv = Replace(csv, vbCr, "")
            csvRows = Split(csv, vbLf)
            For csvRowIndex = 0 To UBound(csvRows)
                
                csvRow = CStr(csvRows(csvRowIndex))
                If csvRow <> "" Then
                    
                    If Not IsEmpty(csvCells) Then csvCellsOld = csvCells
                    csvCells = Split(csvRow, ",")
                    If Not IsEmpty(csvCellsOld) Then
                        
                        '行政区
                        Dim isChangedCagedory1 As Boolean
                        isChangedCagedory1 = False
                        If csvCells(0) <> csvCellsOld(0) Then
                            s = s & "<h1>" & csvCells(0) & "</h1>" & vbCrLf
                            isChangedCagedory1 = True
                        End If
                        
                        '町名
                        Dim isChangedCagedory2 As Boolean
                        isChangedCagedory2 = False
                        If isChangedCagedory1 Or csvCells(1) <> csvCellsOld(1) Then
                            If csvCells(1) <> "" Then s = s & "<h2>" & csvCells(1) & "</h2>" & vbCrLf
                            isChangedCagedory2 = True
                        End If
                        
                        '丁目
                        Dim isChangedCagedory3 As Boolean
                        isChangedCagedory3 = False
                        If isChangedCagedory2 Or csvCells(2) <> csvCellsOld(2) Then
                            If csvCells(2) <> "" Then s = s & "<h3>" & csvCells(2) & "</h3>" & vbCrLf
                            isChangedCagedory3 = True
                        End If
                        
                        '番地
                        Dim isChangedCagedory4 As Boolean
                        isChangedCagedory4 = False
                        If isChangedCagedory3 Or csvCells(3) <> csvCellsOld(3) Then
                            If csvCells(3) <> "" Then s = s & "<h4>" & csvCells(3) & "</h4>" & vbCrLf
                            isChangedCagedory4 = True
                        End If
                        
                        '備考
                        Dim isChangedCagedory5 As Boolean
                        isChangedCagedory5 = False
                        If isChangedCagedory4 Or csvCells(4) <> csvCellsOld(4) Then
                            If csvCells(4) <> "" Then s = s & "<h5>" & csvCells(4) & "</h5>" & vbCrLf
                            isChangedCagedory5 = True
                        End If
                        
                        '普通ごみ
                        Dim gomi1 As String
                        gomi1 = ""
                        If (csvCells(5) & csvCells(6) & csvCells(7)) <> "" Then
                            If csvCells(5) <> "" Then
                                If gomi1 <> "" Then gomi1 = gomi1 & "、"
                                gomi1 = gomi1 & csvCells(5)
                            End If
                            If csvCells(6) <> "" Then
                                If gomi1 <> "" Then gomi1 = gomi1 & "、"
                                gomi1 = gomi1 & csvCells(6) & "（午前）"
                            End If
                            If csvCells(7) <> "" Then
                                If gomi1 <> "" Then gomi1 = gomi1 & "、"
                                gomi1 = gomi1 & csvCells(7) & "（午後）"
                            End If
                            gomi1 = "<li>普通ごみ：" & gomi1 & "</li>"
                        End If
                        
                        '資源ごみ
                        Dim gomi2 As String
                        gomi2 = ""
                        If (csvCells(8) & csvCells(9) & csvCells(10)) <> "" Then
                            If csvCells(8) <> "" Then
                                If gomi2 <> "" Then gomi2 = gomi2 & "、"
                                gomi2 = gomi2 & csvCells(8)
                            End If
                            If csvCells(9) <> "" Then
                                If gomi2 <> "" Then gomi2 = gomi2 & "、"
                                gomi2 = gomi2 & csvCells(9) & "（午前）"
                            End If
                            If csvCells(10) <> "" Then
                                If gomi2 <> "" Then gomi2 = gomi2 & "、"
                                gomi2 = gomi2 & csvCells(10) & "（午後）"
                            End If
                            gomi2 = "<li>資源ごみ：" & gomi2 & "</li>"
                        End If
                        
                        '容器包装プラスチック
                        Dim gomi3 As String
                        gomi3 = ""
                        If (csvCells(11) & csvCells(12) & csvCells(13)) <> "" Then
                            If csvCells(11) <> "" Then
                                If gomi3 <> "" Then gomi3 = gomi3 & "、"
                                gomi3 = gomi3 & csvCells(11)
                            End If
                            If csvCells(12) <> "" Then
                                If gomi3 <> "" Then gomi3 = gomi3 & "、"
                                gomi3 = gomi3 & csvCells(12) & "（午前）"
                            End If
                            If csvCells(13) <> "" Then
                                If gomi3 <> "" Then gomi3 = gomi3 & "、"
                                gomi3 = gomi3 & csvCells(13) & "（午後）"
                            End If
                            gomi3 = "<li>容器包装プラスチック：" & gomi3 & "</li>"
                        End If
                        
                        '古紙衣類
                        Dim gomi4 As String
                        gomi4 = ""
                        If (csvCells(14) & csvCells(15) & csvCells(16)) <> "" Then
                            If csvCells(14) <> "" Then
                                If gomi4 <> "" Then gomi4 = gomi4 & "、"
                                gomi4 = gomi4 & csvCells(14)
                            End If
                            If csvCells(15) <> "" Then
                                If gomi4 <> "" Then gomi4 = gomi4 & "、"
                                gomi4 = gomi4 & csvCells(15) & "（午前）"
                            End If
                            If csvCells(16) <> "" Then
                                If gomi4 <> "" Then gomi4 = gomi4 & "、"
                                gomi4 = gomi4 & csvCells(16) & "（午後）"
                            End If
                            gomi4 = "<li>古紙衣類：" & gomi4 & "</li>"
                        ElseIf csvCells(17) <> "" Then 'コミュニティ改修の場合、上記データは入ってこない
                            gomi4 = "<li>古紙衣類（コミュニティ回収）：" & csvCells(17) & "</li>"
                        End If
                        
                        'ペットボトル
                        Dim gomi5 As String
                        gomi5 = ""
                        If csvCells(18) <> "" Then
                            gomi5 = "<li>ペットボトル（コミュニティ回収）：" & csvCells(18) & "</li>"
                        End If
                        
                        'ごみ統合
                        Dim gomi As String
                        gomi = gomi1 & gomi2 & gomi3 & gomi4 & gomi5
                        If gomi <> "" Then
                            s = s & "<ul>" & gomi & "</ul>"
                        End If
                        
                    End If
                    
                End If
                
            Next
            
            'html作成
            htmlResult = Replace(htmlTemplate, "{{content}}", s)
            writeTextFile dpCsvWrite & "\" & FSO.GetBaseName(f.Name) & ".html", htmlResult
            
        End If
        
    Next
    
ErrTrap:
    
    If Err.Number > 0 Then
        MsgBox Err.Description & vbCrLf & "CSVファイルを全て閉じてから実行してください。", vbInformation, Err.Source
    Else
        MsgBox "処理終了"
    End If
    
    Set f = Nothing
    Set FSO = Nothing
    
End Sub

Private Function readTextFile(ByVal fp As String) As String
    
    With CreateObject("ADODB.Stream")
        .Charset = "UTF-8"
        .Open
        .LoadFromFile fp
        readTextFile = .ReadText
        .Close
    End With
    
End Function

Private Sub writeTextFile(ByVal fp As String, ByVal s As String)
    
    With CreateObject("ADODB.Stream")
        .Charset = "UTF-8"
        .Open
        .WriteText s
        .SaveToFile fp, 2
        .Close
    End With
    
End Sub
