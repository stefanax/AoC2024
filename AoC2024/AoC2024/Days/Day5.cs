namespace AoC2024;

public class Day5
{
    private readonly InputFiles _inputFiles = new InputFiles();

    private bool matchesRules(int firstNumber, int secondNumber, List<Tuple<int, int>> ruleset)
    {
        foreach (var rule in ruleset)
        {
            //If this is true, we have stuff in the wrong order.
            if (firstNumber == rule.Item2 && secondNumber == rule.Item1) return false;
        }

        return true;
    }

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(5, false);
        var inputList = _inputFiles.SplitString(input);

        var rulesets = new List<Tuple<int, int>>();
        var pagesets = new List<List<int>>();

        foreach (var inputRow in inputList)
        {
            if (inputRow == "") continue;
            if (inputRow.Contains("|"))
            {
                var inputNumbersString = inputRow.Split("|");
                rulesets.Add(new Tuple<int, int>(int.Parse(inputNumbersString[0]), int.Parse(inputNumbersString[1])));
            }
            else
            {
                var inputNumbersString = inputRow.Split(",");
                var pageSetToAdd = new List<int>();
                foreach (var inputNumber in inputNumbersString)
                {
                    pageSetToAdd.Add(int.Parse(inputNumber));
                }
                pagesets.Add(pageSetToAdd);
            }
        }


        var middlePageValueSum = 0;

        foreach (var pageset in pagesets)
        {
            var isGoodPageset = true;
            for (var i = 0; i < pageset.Count - 1; i++)
            {
                for (var j = i + 1; j < pageset.Count; j++)
                {
                    if (!matchesRules(pageset[i], pageset[j], rulesets))
                    {
                        isGoodPageset = false;
                        break;
                    }
                }

                if (!isGoodPageset) break;
            }

            if (isGoodPageset)
            {
                middlePageValueSum += pageset[pageset.Count / 2];
            }
        }
        
        
        Console.WriteLine($"Step one result: {middlePageValueSum}");
    }

    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(5, false);
        var inputList = _inputFiles.SplitString(input);

        var rulesets = new List<Tuple<int, int>>();
        var pagesets = new List<List<int>>();

        foreach (var inputRow in inputList)
        {
            if (inputRow == "") continue;
            if (inputRow.Contains("|"))
            {
                var inputNumbersString = inputRow.Split("|");
                rulesets.Add(new Tuple<int, int>(int.Parse(inputNumbersString[0]), int.Parse(inputNumbersString[1])));
            }
            else
            {
                var inputNumbersString = inputRow.Split(",");
                var pageSetToAdd = new List<int>();
                foreach (var inputNumber in inputNumbersString)
                {
                    pageSetToAdd.Add(int.Parse(inputNumber));
                }
                pagesets.Add(pageSetToAdd);
            }
        }
        
        var middlePageValueSum = 0;

        foreach (var pageset in pagesets)
        {
            var isGoodPageset = true;
            for (var i = 0; i < pageset.Count - 1; i++)
            {
                for (var j = i + 1; j < pageset.Count; j++)
                {
                    if (!matchesRules(pageset[i], pageset[j], rulesets))
                    {
                        isGoodPageset = false;
                        break;
                    }
                }

                if (!isGoodPageset) break;
            }

            if (!isGoodPageset)
            {
                var tempPageset = new List<int>();
                //TODO: Reorder pageset.
                do
                {
                    for (var i = 0; i < pageset.Count; i++)
                    {
                        var wasFirstNumber = true;
                        for (var j = 0; j < pageset.Count; j++)
                        {
                            if (i == j) continue;
                            if (!matchesRules(pageset[i], pageset[j], rulesets))
                            {
                                //Console.WriteLine($"MEEP {i}:{j}");
                                wasFirstNumber = false;
                                break;
                            }
                        }

                        if (wasFirstNumber)
                        {
                            tempPageset.Add(pageset[i]);
                            pageset.RemoveAt(i);
                            // if (pageset.Count == 1)
                            // {
                            //     tempPageset.Add(pageset[0]);
                            // }
                            break;
                        }
                    }
                } while (pageset.Count > 0);

                middlePageValueSum += tempPageset[tempPageset.Count / 2];
            }
        }
        
        
        Console.WriteLine($"Step two result: {middlePageValueSum}");
    }
}