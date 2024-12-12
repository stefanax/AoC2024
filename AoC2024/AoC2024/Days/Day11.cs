namespace AoC2024;

public class Day11
{
    private readonly InputFiles _inputFiles = new InputFiles();

    private Dictionary<(string, int), long> _cache = new Dictionary<(string, int), long>();

    private long ParseTheStone(string stoneValue, int blinks)
    {
        if (blinks == 75) return 1;

        if (_cache.ContainsKey((stoneValue, blinks))) return _cache[(stoneValue, blinks)];
        
        if (stoneValue.Length % 2 == 0)
        {
            var splitStoneLeft = stoneValue.Substring(0, stoneValue.Length / 2);
            var splitStoneRight = stoneValue.Substring(stoneValue.Length / 2);

            var stoneCount = ParseTheStone(long.Parse(splitStoneLeft).ToString(), blinks+1);  // Best way for converting "000" to "0".
            stoneCount += ParseTheStone(long.Parse(splitStoneRight).ToString(), blinks+1); //Compensate pos for new stone. Beautiful, right?
            
            _cache.Add((stoneValue, blinks), stoneCount);
            
            return stoneCount;
        }

        if (stoneValue == "0")
        {
            return ParseTheStone("1", blinks + 1);
        }

        var newStoneValue = long.Parse(stoneValue) * 2024;
        return ParseTheStone(newStoneValue.ToString(), blinks + 1);
    }


    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(11, false);

        var stones = input.Split(' ').ToList();

        for (var i = 0; i < 25; i++)
        {
            //stones.ForEach(position => Console.Write($"{position}|"));
            //Console.WriteLine();
            //Console.WriteLine();
            
            for (var stonePos = 0; stonePos < stones.Count; stonePos++)
            {
                if (stones[stonePos].Length % 2 == 0)
                {
                    var splitStoneLeft = stones[stonePos].Substring(0, stones[stonePos].Length / 2);
                    var splitStoneRight = stones[stonePos].Substring(stones[stonePos].Length / 2);

                    stones[stonePos] = long.Parse(splitStoneLeft).ToString();  // Best way for converting "000" to "0".
                    stones.Insert(++stonePos, long.Parse(splitStoneRight).ToString()); //Compensate pos for new stone. Beautiful, right?
                    
                    continue;
                }

                if (stones[stonePos] == "0")
                {
                    stones[stonePos] = "1";
                    continue;
                }

                var newStoneValue = long.Parse(stones[stonePos]) * 2024;
                stones[stonePos] = newStoneValue.ToString();
            }
        }
        
        
        Console.WriteLine($"Step one result: {stones.Count}");
    }


    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(11, false);

        var stones = input.Split(' ').ToList();
        var stoneCount = 0l;

        for (var stonePos = 0; stonePos < stones.Count; stonePos++)
        {
            stoneCount += ParseTheStone(stones[stonePos], 0);
        }
        
        
        Console.WriteLine($"Step two result: {stoneCount}");
    }


}