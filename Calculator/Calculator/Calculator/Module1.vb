Module Module1

    Function add(num1 As Integer, num2 As Integer)
        Return num1 + num2
    End Function

    Function minus(num1 As Integer, num2 As Integer)
        Return num1 - num2
    End Function
    Function multiply(num1 As Integer, num2 As Integer)
        Return num1 * num2
    End Function

    Function divide(num1 As Integer, num2 As Integer)
        Return num1 / num2
    End Function


    Sub Main()

        'open file and read the content 
        Dim file As String
        Console.WriteLine("Type the file name to be opened : ")
        file = Console.ReadLine()
        FileOpen(1, file & ".txt", OpenMode.Input)
        Dim Acount As New List(Of String)
        Dim line As String
        Dim total As Integer

        'while the file is not end yet 
        While Not EOF(1)

            'get the line in the .txt file and store into line which is a string type
            'And add the string to the Acount (String type list)
            line = LineInput(1)
            Acount.Add(line)

        End While

        'close the file for reading content and open file to append the content
        FileClose(1)
        FileOpen(2, file & ".txt", OpenMode.Append)

        'counter is to control the Acount
        Dim counter As Integer = 0

        'loop thorugh the list and change it to char array
        While counter < Acount.Count


            'change the string type list to char array
            Dim Barray As String = Acount(counter)
            Dim seperators() As Char = {"+", "-", "*", "/", "(", ")"}
            Dim Num_seperators() As Char = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}

            Dim array() As String = Barray.Split(seperators)
            Dim Opt_array() As String = Barray.Split(Num_seperators)

            'array(1)-->opt_array(1)--->array(2)
            'Console.WriteLine(array(1))
            'Console.WriteLine(Opt_array(1))

            Dim Anum As Integer = Len(Barray) 'Anum = 7


            'i is to control the array
            Dim i As Integer = 0

            'loop thorugh the char array and do the calculation
            While i < Anum


                Dim j As Integer

                'if there is a bracket in the equation
                If (Barray.Contains("(")) Then
                    total = 0

                    For j = 1 To Opt_array.Length - 2
                        Select Case Opt_array(j)
                            Case "*"
                                total = multiply(array(j), array(j + 1))
                            Case "/"
                                total = divide(array(j), array(j + 1))
                            Case "+"
                                total = add(array(j), array(j + 1))
                            Case "-"
                                total = minus(array(j), array(j + 1))
                        End Select
                        If (Opt_array(j).Contains(")*")) Then
                            Exit For
                        End If

                    Next
                    For j = 1 To Opt_array.Length - 2
                        Select Case Opt_array(j)
                            Case ")+"
                                total = add(total, array(j + 2))
                            Case ")-"
                                total = minus(total, array(j + 2))

                            Case ")*"
                                total = multiply(total, array(j + 2))
                            Case ")/"
                                total = divide(total, array(j + 2))

                        End Select

                    Next

                ElseIf (Anum = 3) Then

                    total = 0
                    Dim num1 As Integer = array(0)
                    Dim num2 As Integer = array(1)

                    If (Opt_array(1) = "+") Then
                        total = add(num1, num2)

                    ElseIf (Opt_array(1) = "-") Then
                        total = minus(num1, num2)

                    ElseIf (Opt_array(1) = "*") Then
                        total = multiply(num1, num2)

                    ElseIf (Opt_array(1) = "/") Then
                        total = divide(num1, num2)
                    End If

                    'without bracket + -  before * / 
                ElseIf (Opt_array(1) = "+" Or Opt_array(1) = "-") Then
                    total = 0
                    For j = 1 To Opt_array.Length - 2
                        Select Case Opt_array(j)
                            Case "*"
                                total = multiply(array(j - 1), array(j))

                            Case "/"
                                total = divide(array(j - 1), array(j))

                        End Select
                    Next

                    For j = 1 To Opt_array.Length - 2

                        Select Case Opt_array(j)
                            Case "+"
                                If (Opt_array(j - 1) = "*" Or Opt_array(j - 1) = "/") Then
                                    total = add(total, array(j - 1))

                                Else
                                    total = add(array(j - 1), total)

                                End If
                            Case "-"
                                If (Opt_array(j - 1) = "*" Or Opt_array(j - 1) = "/") Then
                                    total = minus(total, array(j - 1))

                                Else
                                    total = minus(array(j - 1), total)

                                End If
                            Case "*"
                                total = total
                            Case "/"
                                total = total
                        End Select

                    Next

                Else
                    total = 0
                    For j = 1 To Opt_array.Length - 2
                        Select Case Opt_array(j)
                            Case "*"
                                total = multiply(array(j - 1), array(j))

                            Case "/"
                                total = divide(array(j - 1), array(j))

                        End Select
                    Next

                    For j = 1 To Opt_array.Length - 2

                        Select Case Opt_array(j)
                            Case "+"
                                If (Opt_array(j - 1) = "*" Or Opt_array(j - 1) = "/") Then
                                    total = add(total, array(j))

                                Else
                                    total = add(array(j), total)

                                End If
                            Case "-"
                                If (Opt_array(j - 1) = "*" Or Opt_array(j - 1) = "/") Then
                                    total = minus(total, array(j))

                                Else
                                    total = minus(array(j), total)

                                End If
                            Case "*"
                                total = total
                            Case "/"
                                total = total
                        End Select

                    Next

                End If

                'write answer in to the .txt file
                PrintLine(2, vbCrLf)
                PrintLine(2, Acount(counter) & " = " & total)
                Exit While
            End While

            counter += 1

        End While
        FileClose(2)
        Console.ReadLine()

    End Sub

End Module
