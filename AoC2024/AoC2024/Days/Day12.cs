namespace AoC2024;

public class Day12
{
    private readonly InputFiles _inputFiles = new InputFiles();

    private List<List<GridPoint>> grid = new List<List<GridPoint>>();

    private class GridPoint
    {
        public char RegionCharacter { get; set; }
        public bool IsInitialPoint { get; set; } = false;
        public bool HasBeenProcessed { get; set; } = false;
        public int? RegionArea { get; set; }
        public int? RegionPerimiter { get; set; }

        public GridPoint(char regionCharacter)
        {
            RegionCharacter = regionCharacter;
        }
    }

    // Area / perimiter
    private (int, int) CheckGridPoint(char regionCharacter, int x, int y)
    {
        if (x < 0 || y < 0 || x >= grid.Count || y >= grid[x].Count) return (-1, -1);
        if (grid[x][y].RegionCharacter != regionCharacter) return (-1, -1);
        if (grid[x][y].HasBeenProcessed) return (0, 0);

        grid[x][y].HasBeenProcessed = true;

        var returnResult = (1, 0);
        var result = (0, 0);
        result = CheckGridPoint(regionCharacter, x-1, y);
        if (result.Item1 == -1)
        {
            returnResult.Item2++;
        } 
        else
        {
            returnResult = (returnResult.Item1 + result.Item1, returnResult.Item2 + result.Item2);
        }
        result = CheckGridPoint(regionCharacter, x+1, y);
        if (result.Item1 == -1)
        {
            returnResult.Item2++;
        } 
        else
        {
            returnResult = (returnResult.Item1 + result.Item1, returnResult.Item2 + result.Item2);
        }
        result = CheckGridPoint(regionCharacter, x, y-1);
        if (result.Item1 == -1)
        {
            returnResult.Item2++;
        } 
        else
        {
            returnResult = (returnResult.Item1 + result.Item1, returnResult.Item2 + result.Item2);
        }
        result = CheckGridPoint(regionCharacter, x, y+1);
        if (result.Item1 == -1)
        {
            returnResult.Item2++;
        } 
        else
        {
            returnResult = (returnResult.Item1 + result.Item1, returnResult.Item2 + result.Item2);
        }
        return returnResult;
    }

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(12, false);
        var inputList = _inputFiles.SplitString(input);

        foreach (var InputListRow in inputList)
        {
            var tempGridRow = new List<GridPoint>();
            foreach (var InputListRowItem in InputListRow.ToCharArray())
            {
                tempGridRow.Add(new GridPoint(InputListRowItem));
            }
            grid.Add(tempGridRow);
        }

        var totalCost = 0;
        
        for (var x = 0; x < grid.Count; x++)
        {
            for (var y = 0; y < grid[x].Count; y++)
            {
                if (grid[x][y].HasBeenProcessed) continue;
                var result = CheckGridPoint(grid[x][y].RegionCharacter, x, y);
                totalCost += result.Item1 * result.Item2;
                //grid[x][y].IsInitialPoint = true;
                //grid[x][y].RegionArea
            }
        }
        
        
        Console.WriteLine($"Step one result: {totalCost}");
    }


    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(12, false);
        var inputList = _inputFiles.SplitString(input);

        grid = new List<List<GridPoint>>();
        foreach (var InputListRow in inputList)
        {
            var tempGridRow = new List<GridPoint>();
            foreach (var InputListRowItem in InputListRow.ToCharArray())
            {
                tempGridRow.Add(new GridPoint(InputListRowItem));
            }
            grid.Add(tempGridRow);
        }
        
        var totalCost = 0;
        
        for (var x = 0; x < grid.Count; x++)
        {
            for (var y = 0; y < grid[x].Count; y++)
            {
                if (grid[x][y].HasBeenProcessed) continue;
                var result = CheckGridPoint(grid[x][y].RegionCharacter, x, y);
                //totalCost += result.Item1 * result.Item2;
                //grid[x][y].IsInitialPoint = true;
                //grid[x][y].RegionArea

                var perimiterScannerDirection = 'R';
                var perimiterScannerX = x;
                var perimiterScannerY = y;
                var perimiterScannerRegionCharacter = grid[x][y].RegionCharacter;
                var perimiterScannerSides = 1;

                Console.WriteLine($"Scanning area starting at X={x} and y={y}");
                
                do
                {
                    switch (perimiterScannerDirection)
                    {
                        case 'U':
                            if (perimiterScannerY != 0 && grid[perimiterScannerX][perimiterScannerY - 1].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'L';
                                perimiterScannerY--;
                                break;
                            }

                            if (perimiterScannerX != 0 && grid[perimiterScannerX - 1][perimiterScannerY].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerX--;
                                break;
                            }

                            if (perimiterScannerY < grid[perimiterScannerX].Count - 1 && grid[perimiterScannerX][perimiterScannerY + 1].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'R';
                                perimiterScannerY++;
                                break;
                            }

                            perimiterScannerSides += 2;
                            perimiterScannerDirection = 'D';
                            perimiterScannerX++;
                            break;
                        case 'D':
                            if (perimiterScannerY < grid[perimiterScannerX].Count - 1 && grid[perimiterScannerX][perimiterScannerY + 1].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'R';
                                perimiterScannerY++;
                                break;
                            }

                            if (perimiterScannerX < grid.Count - 1 && grid[perimiterScannerX + 1][perimiterScannerY].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerX++;
                                break;
                            }

                            if (perimiterScannerY != 0 && grid[perimiterScannerX][perimiterScannerY - 1].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'L';
                                perimiterScannerY--;
                                break;
                            }

                            perimiterScannerSides += 2;
                            perimiterScannerDirection = 'U';
                            perimiterScannerX--;
                            break;
                        case 'R':
                            // Singlespace-fix
                            if ((perimiterScannerX == 0 || grid[perimiterScannerX - 1][perimiterScannerY].RegionCharacter != perimiterScannerRegionCharacter)
                                && (perimiterScannerX == grid.Count - 1 || grid[perimiterScannerX + 1][perimiterScannerY].RegionCharacter != perimiterScannerRegionCharacter)
                                && (perimiterScannerY == 0 || grid[perimiterScannerX][perimiterScannerY - 1].RegionCharacter != perimiterScannerRegionCharacter)
                                && (perimiterScannerY == grid[perimiterScannerX].Count - 1 || grid[perimiterScannerX][perimiterScannerY + 1].RegionCharacter != perimiterScannerRegionCharacter))
                            {
                                break;
                            }
                            
                            if (perimiterScannerX != 0 && grid[perimiterScannerX - 1][perimiterScannerY].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'U';
                                perimiterScannerX--;
                                break;
                            }

                            if (perimiterScannerY < grid[perimiterScannerX].Count - 1 && grid[perimiterScannerX][perimiterScannerY + 1].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerY++;
                                break;
                            }

                            if (perimiterScannerX < grid.Count - 1 && grid[perimiterScannerX + 1][perimiterScannerY].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'D';
                                perimiterScannerX++;
                                break;
                            }

                            perimiterScannerSides += 2;
                            perimiterScannerDirection = 'L';
                            perimiterScannerY--;
                            break;
                        case 'L':
                            if (perimiterScannerX < grid.Count - 1 && grid[perimiterScannerX + 1][perimiterScannerY].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'D';
                                perimiterScannerX++;
                                break;
                            }

                            if (perimiterScannerY != 0 && grid[perimiterScannerX][perimiterScannerY - 1].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerY--;
                                break;
                            }

                            if (perimiterScannerX != 0 && grid[perimiterScannerX - 1][perimiterScannerY].RegionCharacter == perimiterScannerRegionCharacter)
                            {
                                perimiterScannerSides++;
                                perimiterScannerDirection = 'U';
                                perimiterScannerX--;
                                break;
                            }

                            perimiterScannerSides += 2;
                            perimiterScannerDirection = 'R';
                            perimiterScannerY++;
                            break;
                    }
                } while (perimiterScannerX != x || perimiterScannerY != y);

                //Fix for when the edge is just one high.
                if (perimiterScannerDirection == 'L') perimiterScannerSides++;

                perimiterScannerSides = int.Max(4, perimiterScannerSides);

                Console.WriteLine($"Area starting at X={x} and y={y} has {perimiterScannerSides} sides");
                totalCost += result.Item1 * perimiterScannerSides;
            }
        }
        
        Console.WriteLine($"Step two result: {totalCost}");
    }
}