namespace AoC2024;

public class Day4
{
    private readonly InputFiles _inputFiles = new InputFiles();

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(4, false);
        var inputList = _inputFiles.SplitString(input);

        var grid = new List<List<char>>();

        var posX = 0;
        
        foreach (var inputRow in inputList)
        {
            var inputRowChars = inputRow.ToCharArray();
            var posY = 0;
            grid.Add(new List<char>());
            foreach (var inputRowChar in inputRowChars)
            {
                grid[posX].Add(inputRowChar);
                posY++;
            }

            posX++;
        }

        var foundXmasCount = 0;
        
        for (posX = 0; posX < grid.Count; posX++)
        {
            for (var posY = 0; posY < grid[0].Count; posY++)
            {
                if (grid[posX][posY] == 'X')
                {
                    //Do Upwards Check
                    if (posX >= 3)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX-1][posY] + grid[posX-2][posY] + grid[posX-3][posY];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Up Right
                    if (posX >= 3 && posY <= grid[0].Count - 4)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX-1][posY+1] + grid[posX-2][posY+2] + grid[posX-3][posY+3];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Right
                    if (posY <= grid[0].Count - 4)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX][posY+1] + grid[posX][posY+2] + grid[posX][posY+3];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Down Right
                    if (posX <= grid.Count - 4 && posY <= grid[0].Count - 4)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX+1][posY+1] + grid[posX+2][posY+2] + grid[posX+3][posY+3];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Down
                    if (posX <= grid.Count - 4)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX+1][posY] + grid[posX+2][posY] + grid[posX+3][posY];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Down Left
                    if (posX <= grid.Count - 4 && posY >= 3)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX+1][posY-1] + grid[posX+2][posY-2] + grid[posX+3][posY-3];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Left
                    if (posY >= 3)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX][posY-1] + grid[posX][posY-2] + grid[posX][posY-3];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                    
                    //DO Up Left
                    if (posX >= 3 && posY >= 3)
                    {
                        var testWord = grid[posX][posY].ToString() + grid[posX-1][posY-1] + grid[posX-2][posY-2] + grid[posX-3][posY-3];
                        if (testWord == "XMAS") foundXmasCount++;
                    }
                }
            }
        }
        
        
        Console.WriteLine($"Step one result: {foundXmasCount}");
    }
    
    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(4, false);
        var inputList = _inputFiles.SplitString(input);
        
                var grid = new List<List<char>>();

        var posX = 0;
        
        foreach (var inputRow in inputList)
        {
            var inputRowChars = inputRow.ToCharArray();
            var posY = 0;
            grid.Add(new List<char>());
            foreach (var inputRowChar in inputRowChars)
            {
                grid[posX].Add(inputRowChar);
                posY++;
            }

            posX++;
        }

        var foundXmasCount = 0;
        
        for (posX = 1; posX < grid.Count-1; posX++)
        {
            for (var posY = 1; posY < grid[0].Count-1; posY++)
            {
                if (grid[posX][posY] == 'A')
                {
                    //Do Upwards Check
                    if (grid[posX - 1][posY - 1] == 'M' && grid[posX - 1][posY + 1] == 'M' && grid[posX + 1][posY - 1] == 'S' && grid[posX + 1][posY + 1] == 'S')
                    {
                        foundXmasCount++;
                    }
                    
                    //Do Right Check
                    if (grid[posX - 1][posY - 1] == 'S' && grid[posX - 1][posY + 1] == 'M' && grid[posX + 1][posY - 1] == 'S' && grid[posX + 1][posY + 1] == 'M')
                    {
                        foundXmasCount++;
                    }
                    
                    //Do Downwards Check
                    if (grid[posX - 1][posY - 1] == 'S' && grid[posX - 1][posY + 1] == 'S' && grid[posX + 1][posY - 1] == 'M' && grid[posX + 1][posY + 1] == 'M')
                    {
                        foundXmasCount++;
                    }
                    
                    //Do Left Check
                    if (grid[posX - 1][posY - 1] == 'M' && grid[posX - 1][posY + 1] == 'S' && grid[posX + 1][posY - 1] == 'M' && grid[posX + 1][posY + 1] == 'S')
                    {
                        foundXmasCount++;
                    }
                }
            }
        }
        
        
        Console.WriteLine($"Step two result: {foundXmasCount}");
    }
}