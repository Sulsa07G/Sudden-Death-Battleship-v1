Module Module1
    'Sullivan Sherwood
    '11/21/22
    'Sudden Death Battleship


    'This will be used to represent the state of each cell
    Public Enum cellstate
        empty
        miss
        hit
        cmpship
    End Enum

    Public rows As Integer = 4
    Public cols As Integer = 5
    'This 2d array will be used to represent the board
    Public board(rows - 1, cols - 1) As cellstate

    Sub Main()
        Dim gameOver As Boolean = False
        Dim shotCount As Integer = 0
        resetboard()
        PlaceComputerShip()
        Do
            shotCount += 1
            PrintBoard()
            Dim row As Integer = GetUserInput("Please enter the row to fire on -> ", rows - 1)
            Dim col As Integer = GetUserInput("Please enter the col to fire on -> ", cols - 1)
            If board(row, col) = cellstate.cmpship Then
                board(row, col) = cellstate.hit
                gameOver = True
            Else
                board(row, col) = cellstate.miss
            End If
        Loop While Not gameOver
        PrintBoard()
        Console.WriteLine("You hit it in {0} shots!", shotCount.ToString)
    End Sub

    ''' <summary>
    ''' Sets all cells in the board to cellstate.empty
    ''' </summary>
    Sub resetboard()
        'Use nested for loops to poulate
        'For i = 0 to max row num
        'For j = 0 to max col num
        'Set val to cellstate.empty
        For i As Integer = 0 To board.GetUpperBound(0) 'rows - 1
            For j As Integer = 0 To board.GetUpperBound(1) 'cols - 1
                board(i, j) = cellstate.empty
            Next
        Next
    End Sub

    ''' <summary>
    ''' Selects a random int from 0 to rows -1 and a random int from cols-1 to
    ''' place a computer ship
    ''' </summary>
    Sub PlaceComputerShip()
        Dim rand As New Random
        Dim row As Integer = rand.Next(0, rows)
        Dim col As Integer = rand.Next(0, cols)
        board(row, col) = cellstate.cmpship
    End Sub

    ''' <summary>
    ''' prints board
    ''' </summary>
    Sub PrintBoard()

        Console.Write("  ")
        For colNum As Integer = 0 To cols - 1
            Console.Write(colNum & " ")
        Next
        Console.Write(vbNewLine)

        For row As Integer = 0 To board.GetUpperBound(0)
            Console.Write(row & " ")
            For col As Integer = 0 To board.GetUpperBound(1)
                Select Case board(row, col)
                    Case cellstate.empty, cellstate.cmpship
                        Console.Write("- ")
                    Case cellstate.miss
                        Console.ForegroundColor = ConsoleColor.Red
                        Console.Write("x ")
                        Console.ResetColor()
                    Case cellstate.hit
                        Console.ForegroundColor = ConsoleColor.Green
                        Console.Write("H ")
                        Console.ResetColor()
                End Select
            Next
            Console.Write(vbNewLine)
        Next
    End Sub

    ''' <summary>
    ''' repeats prompt to the user until a number between 0 and max (inclusive) is given
    ''' return that number
    ''' </summary>
    ''' <param name="prompt"></param>
    ''' <param name="max"></param>
    ''' <returns>an int between 0 and max</returns>
    Function GetUserInput(prompt As String, max As Integer)
        Dim valid As Boolean = False
        Dim userInput As Integer
        Dim inputStr As String
        Do
            Console.WriteLine(prompt)
            inputStr = Console.ReadLine
            valid = Integer.TryParse(inputStr, userInput)
            If Not (valid AndAlso userInput >= 0 AndAlso userInput <= max) Then
                valid = False
            End If
        Loop While Not valid
        Return userInput
    End Function

End Module
