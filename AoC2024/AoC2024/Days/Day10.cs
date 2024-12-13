namespace AoC2024;

public class Day10
{
    private readonly InputFiles _inputFiles = new InputFiles();
    private List<(int, int)> _trailStarts = new List<(int, int)>();
    private List<(int, int)> _trailEnds = new List<(int, int)>();

    private void TryToWalk(int x, int y, int fromLevel, List<List<int>> grid, bool multiPathEnable = false)
    {
        if (x < 0 || y < 0 || x >= grid.Count || y >= grid[0].Count) return;
        
        if (grid[x][y] != fromLevel+1) return;
        
        if (grid[x][y] == 9)
        {
            if (!multiPathEnable && _trailEnds.Contains((x, y))) return;
            _trailEnds.Add((x, y));
            return;
        }
        
        TryToWalk(x-1, y, grid[x][y], grid, multiPathEnable);
        TryToWalk(x+1, y, grid[x][y], grid, multiPathEnable);
        TryToWalk(x, y-1, grid[x][y], grid, multiPathEnable);
        TryToWalk(x, y+1, grid[x][y], grid, multiPathEnable);
    }

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(10, false);
        var inputList = _inputFiles.SplitString(input);
        
        var grid = new List<List<int>>();

        foreach (var inputListRow in inputList)
        {
            var tempGridRow = new List<int>();
            var rowChars = inputListRow.ToCharArray();

            foreach (var rowChar in rowChars)
            {
                tempGridRow.Add(int.Parse(rowChar.ToString()));
            }
            grid.Add(tempGridRow);
        }

        for (var i = 0; i < grid.Count; i++)
        {
            for (var j = 0; j < grid[0].Count; j++)
            {
                if (grid[i][j] == 0)
                {
                    _trailStarts.Add((i, j));
                }
            }
        }
        
        var trailEnds = 0;
        
        foreach (var trailStart in _trailStarts)
        {
            _trailEnds = new List<(int, int)>();
            TryToWalk(trailStart.Item1, trailStart.Item2, -1, grid);
            trailEnds += _trailEnds.Count;
        }



        Console.WriteLine($"Step one result: {trailEnds}");
    }
    
    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(10, false);
        var inputList = _inputFiles.SplitString(input);
        
        var grid = new List<List<int>>();

        foreach (var inputListRow in inputList)
        {
            var tempGridRow = new List<int>();
            var rowChars = inputListRow.ToCharArray();

            foreach (var rowChar in rowChars)
            {
                tempGridRow.Add(int.Parse(rowChar.ToString()));
            }
            grid.Add(tempGridRow);
        }
        
        var trailEnds = 0;
        
        foreach (var trailStart in _trailStarts)
        {
            _trailEnds = new List<(int, int)>();
            TryToWalk(trailStart.Item1, trailStart.Item2, -1, grid, true);
            trailEnds += _trailEnds.Count;
        }



        Console.WriteLine($"Step two result: {trailEnds}");
    }
}