namespace AoC2024;

public class Day6
{
    private readonly InputFiles _inputFiles = new InputFiles();

    private struct GuardData
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Direction { get; set; }

        public GuardData(int x, int y, char direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(6, false);
        var inputList = _inputFiles.SplitString(input);

        var gridObstacles = new List<List<bool>>();
        var gridPath = new List<List<bool>>();
        var guardPos = new GuardData();

        for (var i = 0; i < inputList.Length; i++)
        {
            var inputRowParts = inputList[i].ToCharArray();
            var gridObstaclesRow = new List<bool>();
            for (var j = 0; j < inputRowParts.Length; j++)
            {
                gridObstaclesRow.Add(inputRowParts[j] == '#');
                if (inputRowParts[j] == '^')
                {
                    guardPos = new GuardData(i, j, 'U');
                }
            }
            gridObstacles.Add(gridObstaclesRow);
        }

        
        // Probably slow, but sexy.
        var gridSize = new Tuple<int, int>(gridObstacles.Count, gridObstacles[0].Count);
        gridPath = Enumerable
            .Range(0, gridSize.Item1)
            .Select(_ => Enumerable.Repeat(false, gridSize.Item2).ToList())
            .ToList();

        gridPath[guardPos.X][guardPos.Y] = true;
        
        try
        {
            while (true)
            {
                var obstacleInFront = true;
                switch (guardPos.Direction)
                {
                    case 'U': obstacleInFront = gridObstacles[guardPos.X-1][guardPos.Y];
                        break;
                    case 'D': obstacleInFront = gridObstacles[guardPos.X+1][guardPos.Y];
                        break;
                    case 'L': obstacleInFront = gridObstacles[guardPos.X][guardPos.Y-1];
                        break;
                    case 'R': obstacleInFront = gridObstacles[guardPos.X][guardPos.Y+1];
                        break;
                }

                if (obstacleInFront)
                {
                    switch (guardPos.Direction)
                    {
                        case 'U': guardPos.Direction = 'R';
                            break;
                        case 'D': guardPos.Direction = 'L';
                            break;
                        case 'L': guardPos.Direction = 'U';
                            break;
                        case 'R': guardPos.Direction = 'D';
                            break;
                    }
                }
                else
                {
                    switch (guardPos.Direction)
                    {
                        case 'U': guardPos.X--;
                            break;
                        case 'D': guardPos.X++;
                            break;
                        case 'L': guardPos.Y--;
                            break;
                        case 'R': guardPos.Y++;
                            break;
                    }

                    gridPath[guardPos.X][guardPos.Y] = true;
                }
            }
        }
        catch (Exception e)
        {
            // We moved outside of the grid...
        }
        
        var trueCount = gridPath.Sum(row => row.Count(cell => cell));
        
        Console.WriteLine($"Step one result: {trueCount}");
    }

    
    
    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(6, false);
        var inputList = _inputFiles.SplitString(input);

        var gridObstacles = new List<List<bool>>();
        var gridPath = new List<List<bool>>();
        var guardPos = new GuardData();
        var guardStartPos = new GuardData();

        for (var i = 0; i < inputList.Length; i++)
        {
            var inputRowParts = inputList[i].ToCharArray();
            var gridObstaclesRow = new List<bool>();
            for (var j = 0; j < inputRowParts.Length; j++)
            {
                gridObstaclesRow.Add(inputRowParts[j] == '#');
                if (inputRowParts[j] == '^')
                {
                    guardStartPos = new GuardData(i, j, 'U');
                }
            }
            gridObstacles.Add(gridObstaclesRow);
        }

        
        // Probably slow, but sexy.
        var gridSize = (gridObstacles.Count, gridObstacles[0].Count);
        gridPath = Enumerable
            .Range(0, gridSize.Item1)
            .Select(_ => Enumerable.Repeat(false, gridSize.Item2).ToList())
            .ToList();

        gridPath[guardPos.X][guardPos.Y] = true;

        var goodPlacesCount = 0;

        for (var i = 0; i < gridSize.Item1; i++)
        {
            for (var j = 0; j < gridSize.Item2; j++)
            {
                if (guardStartPos.X == i && guardStartPos.Y == j) continue;
                guardPos = new GuardData(guardStartPos.X, guardStartPos.Y, guardStartPos.Direction);

                List<List<bool>> tempGridObstacles = gridObstacles
                    .Select(innerList => new List<bool>(innerList))
                    .ToList();

                tempGridObstacles[i][j] = true;
                
                
                
                try
                {
                    var totalAmountOfAllowedMoves = 100000;
                    while (totalAmountOfAllowedMoves > 0)
                    {
                        totalAmountOfAllowedMoves--;
                        var obstacleInFront = true;
                        switch (guardPos.Direction)
                        {
                            case 'U': obstacleInFront = tempGridObstacles[guardPos.X-1][guardPos.Y];
                                break;
                            case 'D': obstacleInFront = tempGridObstacles[guardPos.X+1][guardPos.Y];
                                break;
                            case 'L': obstacleInFront = tempGridObstacles[guardPos.X][guardPos.Y-1];
                                break;
                            case 'R': obstacleInFront = tempGridObstacles[guardPos.X][guardPos.Y+1];
                                break;
                        }

                        if (obstacleInFront)
                        {
                            switch (guardPos.Direction)
                            {
                                case 'U': guardPos.Direction = 'R';
                                    break;
                                case 'D': guardPos.Direction = 'L';
                                    break;
                                case 'L': guardPos.Direction = 'U';
                                    break;
                                case 'R': guardPos.Direction = 'D';
                                    break;
                            }
                        }
                        else
                        {
                            switch (guardPos.Direction)
                            {
                                case 'U': guardPos.X--;
                                    break;
                                case 'D': guardPos.X++;
                                    break;
                                case 'L': guardPos.Y--;
                                    break;
                                case 'R': guardPos.Y++;
                                    break;
                            }

                            gridPath[guardPos.X][guardPos.Y] = true;
                        }
                    }

                    // Console.WriteLine();
                    // Console.WriteLine();
                    // Console.WriteLine();
                    // tempGridObstacles.ForEach(row => Console.WriteLine(string.Join(" ", row.Select(cell => cell ? "X" : "."))));
                    // Console.WriteLine("Was a good place");
                    goodPlacesCount++;
                }
                catch (Exception e)
                {
                    // We moved outside of the grid...
                }
            }
        }
        
        Console.WriteLine($"Step two result: {goodPlacesCount}");
    }

}