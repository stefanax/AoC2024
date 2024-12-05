namespace AoC2024;

public class Day2
{
    private readonly InputFiles _inputFiles = new InputFiles();

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(2, false);
        var inputList = _inputFiles.SplitString(input);

        var reports = new List<List<int>>();

        foreach (var inputItem in inputList)
        {
            var inputValues = inputItem.Split(" ").Select(int.Parse).ToList();
            reports.Add(inputValues);
        }

        var safeReportsCount = 0;

        foreach (var report in reports)
        {
            var directionIncrease = report[0] < report[1];

            var allGood = true;

            for (var i = 0; i < report.Count - 1; i++)
            {
                var result = 0;
                if (directionIncrease)
                {
                    result = report[i+1] - report[i];
                }
                else
                {
                    result = report[i] - report[i+1];
                }

                if (result < 1 || result > 3)
                {
                    allGood = false;
                    break;
                }
            }

            if (allGood)
            {
                safeReportsCount++;
            }
        }
        
        Console.WriteLine($"Step one result: {safeReportsCount}");
    }

    private bool reportIsValid(List<int> report, bool originalReport)
    {
        var directionIncrease = report[0] < report[report.Count-1];

        var allGood = true;
        var hadOneFailure = false;

        for (var i = 0; i < report.Count - 1; i++)
        {
            var result = 0;
            if (directionIncrease)
            {
                result = report[i+1] - report[i];
            }
            else
            {
                result = report[i] - report[i+1];
            }

            if (result < 1 || result > 3)
            {
                if (!originalReport) return false;
                List<int> tempReport;
                for (int j = 0; j < report.Count; j++)
                {
                    tempReport = report.ToList();
                    tempReport.RemoveAt(j);
                    if (reportIsValid(tempReport, false))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        return true;
    }
    
    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(2, false);
        var inputList = _inputFiles.SplitString(input);

        var reports = new List<List<int>>();

        foreach (var inputItem in inputList)
        {
            var inputValues = inputItem.Split(" ").Select(int.Parse).ToList();
            reports.Add(inputValues);
        }

        var safeReportsCount = 0;

        foreach (var report in reports)
        {
            var allGood = reportIsValid(report, true);

            if (allGood)
            {
                safeReportsCount++;
            }
        }
        
        Console.WriteLine($"Step two result: {safeReportsCount}");
    }
}