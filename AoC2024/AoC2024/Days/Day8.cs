namespace AoC2024;

public class Day8
{
    private readonly InputFiles _inputFiles = new InputFiles();
    
    private class GridPosData
    {
        public char Frequency { get; set; }
        public bool IsAntinode { get; set; }

        public GridPosData(char frequency, bool isAntinode)
        {
            Frequency = frequency;
            IsAntinode = isAntinode;
        }
    }

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(8, false);
        var inputList = _inputFiles.SplitString(input);

        var grid = new List<List<GridPosData>>();

        foreach (var inputRow in inputList)
        {
            var inputRowParts = inputRow.ToCharArray();
            var tempGridRow = new List<GridPosData>();
            foreach (var inputRowPart in inputRowParts)
            {
                tempGridRow.Add(new GridPosData(inputRowPart, false));
            }
            grid.Add(tempGridRow);
        }

        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[0].Count; j++)
            {
                for (var k = i; k < grid.Count; k++)
                {
                    for (var l = 0; l < grid[0].Count; l++)
                    {
                        if (i == k && j == l) continue;

                        if (grid[i][j].Frequency != '.' && grid[i][j].Frequency == grid[k][l].Frequency)
                        {
                            var heightDiff = k - i;
                            var widthDiff = l - j;

                            try
                            {
                                grid[i - heightDiff][j - widthDiff].IsAntinode = true;
                            }
                            catch
                            {
                                //Do nothing
                            }

                            try
                            {
                                grid[k + heightDiff][l + widthDiff].IsAntinode = true;
                            }
                            catch
                            {
                                //Do nothing
                            }
                        }
                    }
                }
            }
        }
        
        grid.ForEach(row => Console.WriteLine(string.Join(" ", row.Select(cell => cell.IsAntinode ? "X" : "."))));

        int antinodeCount = grid.Sum(row => row.Count(item => item.IsAntinode));

        Console.WriteLine($"Step one result: {antinodeCount}");
    }
    
    

    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(8, false);
        var inputList = _inputFiles.SplitString(input);

        var grid = new List<List<GridPosData>>();

        foreach (var inputRow in inputList)
        {
            var inputRowParts = inputRow.ToCharArray();
            var tempGridRow = new List<GridPosData>();
            foreach (var inputRowPart in inputRowParts)
            {
                tempGridRow.Add(new GridPosData(inputRowPart, false));
            }
            grid.Add(tempGridRow);
        }

        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[0].Count; j++)
            {
                for (var k = i; k < grid.Count; k++)
                {
                    for (var l = 0; l < grid[0].Count; l++)
                    {
                        if (i == k && j == l) continue;

                        if (grid[i][j].Frequency != '.' && grid[i][j].Frequency == grid[k][l].Frequency)
                        {
                            var heightDiff = k - i;
                            var widthDiff = l - j;

                            grid[i][j].IsAntinode = true;
                            grid[k][l].IsAntinode = true;

                            try
                            {
                                var cycleHeight = 0;
                                var cycleWidth = 0;
                                while (true)
                                {
                                    grid[i - heightDiff - cycleHeight][j - widthDiff - cycleWidth].IsAntinode = true;
                                    cycleHeight += heightDiff;
                                    cycleWidth += widthDiff;
                                }
                            }
                            catch
                            {
                                //Do nothing
                            }

                            try
                            {
                                var cycleHeight = 0;
                                var cycleWidth = 0;
                                while (true)
                                {
                                    grid[k + heightDiff + cycleHeight][l + widthDiff + cycleWidth].IsAntinode = true;
                                    cycleHeight += heightDiff;
                                    cycleWidth += widthDiff;
                                }
                            }
                            catch
                            {
                                //Do nothing
                            }
                        }
                    }
                }
            }
        }
        
        grid.ForEach(row => Console.WriteLine(string.Join(" ", row.Select(cell => cell.IsAntinode ? "X" : "."))));

        int antinodeCount = grid.Sum(row => row.Count(item => item.IsAntinode));

        Console.WriteLine($"Step two result: {antinodeCount}");
    }
}