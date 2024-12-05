using System.Text.RegularExpressions;

namespace AoC2024;

public class Day3
{
    private readonly InputFiles _inputFiles = new InputFiles();

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(3, false);
        
        string pattern = @"(mul\(\d{1,3},\d{1,3}\))";

        // Use Regex to find matches
        MatchCollection matches = Regex.Matches(input, pattern);
        
        List<string> results = new List<string>();
        foreach (Match match in matches)
        {
            results.Add(match.Groups[1].Value); // Group 1 contains the content inside the brackets
        }

        var returnValue = 0;
        
        foreach (var result in results)
        {
            var trimmedResult = result.Replace("mul(", "").Replace(")", "");
            var splitResult = trimmedResult.Split(",");
            returnValue += int.Parse(splitResult[0]) * int.Parse(splitResult[1]);
        }
        
        Console.WriteLine($"Step one result: {returnValue}");
    }
    
    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(3, false);
        
        string pattern = @"(do\(\)|don't\(\)|mul\(\d{1,3},\d{1,3}\))";

        // Use Regex to find matches
        MatchCollection matches = Regex.Matches(input, pattern);
        
        List<string> results = new List<string>();
        foreach (Match match in matches)
        {
            results.Add(match.Groups[1].Value); // Group 1 contains the content inside the brackets
        }

        var returnValue = 0;
        var doEnabled = true;
        
        foreach (var result in results)
        {
            if (result == "do()")
            {
                doEnabled = true;
                continue;
            }

            if (result == "don't()")
            {
                doEnabled = false;
                continue;
            }

            if (!doEnabled)
            {
                continue;
            }
            
            var trimmedResult = result.Replace("mul(", "").Replace(")", "");
            var splitResult = trimmedResult.Split(",");
            returnValue += int.Parse(splitResult[0]) * int.Parse(splitResult[1]);
        }
        
        Console.WriteLine($"Step one result: {returnValue}");
    }
}