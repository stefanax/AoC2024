namespace AoC2024;

public class Day7
{
    private readonly InputFiles _inputFiles = new InputFiles();

    public struct Calculation
    {
        public List<long> Numbers { get; set; }
        public long ExpectedResult { get; set; }

        public Calculation(List<long> numbers, long expectedResult)
        {
            Numbers = numbers;
            ExpectedResult = expectedResult;
        }

        public bool IsValid()
        {
            if (Numbers.Count == 2)
            {
                if (Numbers[0] + Numbers[1] == ExpectedResult)
                {
                    return true;
                }
                if (Numbers[0] * Numbers[1] == ExpectedResult)
                {
                    return true;
                }

                return false;
            }

            var newList = Numbers.ToList();
            newList[0] = Numbers[0] + Numbers[1];
            newList.RemoveAt(1);
            var newCalculation = new Calculation(newList, ExpectedResult);
            var validCalculation = newCalculation.IsValid();
            if (validCalculation)
            {
                return true;
            }
            
            newList = Numbers.ToList();
            newList[0] = Numbers[0] * Numbers[1];
            newList.RemoveAt(1);
            newCalculation = new Calculation(newList, ExpectedResult);
            return newCalculation.IsValid();
        }

        public bool IsValidConcat()
        {
            if (Numbers.Count == 2)
            {
                if (Numbers[0] + Numbers[1] == ExpectedResult)
                {
                    return true;
                }
                if (Numbers[0] * Numbers[1] == ExpectedResult)
                {
                    return true;
                }
                if (long.Parse(Numbers[0].ToString() + Numbers[1].ToString()) == ExpectedResult)
                {
                    return true;
                }

                return false;
            }

            var newList = Numbers.ToList();
            newList[0] = Numbers[0] + Numbers[1];
            newList.RemoveAt(1);
            var newCalculation = new Calculation(newList, ExpectedResult);
            var validCalculation = newCalculation.IsValidConcat();
            if (validCalculation)
            {
                return true;
            }
            
            newList = Numbers.ToList();
            newList[0] = Numbers[0] * Numbers[1];
            newList.RemoveAt(1);
            newCalculation = new Calculation(newList, ExpectedResult);
            validCalculation = newCalculation.IsValidConcat();
            if (validCalculation)
            {
                return true;
            }
            
            newList = Numbers.ToList();
            newList[0] = long.Parse(Numbers[0].ToString() + Numbers[1].ToString());
            newList.RemoveAt(1);
            newCalculation = new Calculation(newList, ExpectedResult);
            return newCalculation.IsValidConcat();
        }
    }


    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(7, false);
        var inputList = _inputFiles.SplitString(input);

        var calculations = new List<Calculation>();

        foreach (var inputRow in inputList)
        {
            var inputRowItems = inputRow.Split(' ');
            inputRowItems[0] = inputRowItems[0].Trim(':');
            var expectedResult = long.Parse(inputRowItems[0]);
            var numbers = new List<long>();
            for (var i = 1; i < inputRowItems.Length; i++)
            {
                numbers.Add(int.Parse(inputRowItems[i]));
            }

            calculations.Add(new Calculation(numbers, expectedResult));
        }

        long totalSum = 0;
        foreach (var calculation in calculations)
        {
            if (calculation.IsValid())
            {
                totalSum += calculation.ExpectedResult;
            }
        }
        
        
        Console.WriteLine($"Step one result: {totalSum}");
    }

    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(7, false);
        var inputList = _inputFiles.SplitString(input);

        var calculations = new List<Calculation>();

        foreach (var inputRow in inputList)
        {
            var inputRowItems = inputRow.Split(' ');
            inputRowItems[0] = inputRowItems[0].Trim(':');
            var expectedResult = long.Parse(inputRowItems[0]);
            var numbers = new List<long>();
            for (var i = 1; i < inputRowItems.Length; i++)
            {
                numbers.Add(int.Parse(inputRowItems[i]));
            }

            calculations.Add(new Calculation(numbers, expectedResult));
        }

        long totalSum = 0;
        foreach (var calculation in calculations)
        {
            if (calculation.IsValidConcat())
            {
                totalSum += calculation.ExpectedResult;
            }
        }
        
        
        Console.WriteLine($"Step two result: {totalSum}");
    }

}