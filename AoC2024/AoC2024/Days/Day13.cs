using System.Diagnostics;

namespace AoC2024;

public class Day13
{
    private readonly InputFiles _inputFiles = new InputFiles();

    private class GameMachine
    {
        public Tuple<int, int> ButtonA { get; set; }
        public Tuple<int, int> ButtonB { get; set; }
        public Tuple<long, long> Prize { get; set; }

        public GameMachine(Tuple<int, int> buttonA, Tuple<int, int> buttonB, Tuple<long, long> prize)
        {
            ButtonA = buttonA;
            ButtonB = buttonB;
            Prize = prize;
        }

        // -1 = Both are lower, go up on A
        //  1 = One is higher, go down on B
        //  0 = MATCH! WOOT!
        public int CheckPresses(long buttonA, long buttonB)
        {
            var resultX = ButtonA.Item1 * buttonA;
            var resultY = ButtonA.Item2 * buttonA;
            resultX += ButtonB.Item1 * buttonB;
            resultY += ButtonB.Item2 * buttonB;
            if (resultX < Prize.Item1 && resultY < Prize.Item2) return -1;
            if (resultX == Prize.Item1 && resultY == Prize.Item2) return 0;
            return 1;
        }
    }

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(13, false);
        var inputList = _inputFiles.SplitString(input).ToList();

        var gameMachines = new List<GameMachine>();

        while (inputList.Count > 0)
        {
            var buttonAString = inputList[0];
            var buttonASplitString = buttonAString.Split(' ');
            var buttonATuple = new Tuple<int, int>(int.Parse(buttonASplitString[2].Substring(2, buttonASplitString[2].Length-3)), int.Parse(buttonASplitString[3].Substring(2)));
            inputList.RemoveAt(0);
            var buttonBString = inputList[0];
            var buttonBSplitString = buttonBString.Split(' ');
            var buttonBTuple = new Tuple<int, int>(int.Parse(buttonBSplitString[2].Substring(2, buttonBSplitString[2].Length-3)), int.Parse(buttonBSplitString[3].Substring(2)));
            inputList.RemoveAt(0);
            var prizeString = inputList[0];
            var prizeSplitString = prizeString.Split(' ');
            var prizeTuple = new Tuple<long, long>(int.Parse(prizeSplitString[1].Substring(2, prizeSplitString[1].Length-3)), int.Parse(prizeSplitString[2].Substring(2)));
            inputList.RemoveAt(0);
            if (inputList.Count > 0) inputList.RemoveAt(0);
            
            gameMachines.Add(new GameMachine(buttonATuple, buttonBTuple, prizeTuple));
        }


        var totalTokens = 0;
        
        foreach (var gameMachine in gameMachines)
        {
            var pressesA = 0;
            var pressesB = 100;
            do
            {
                var result = gameMachine.CheckPresses(pressesA, pressesB);

                switch (result)
                {
                    case -1:
                        pressesA++;
                        break;
                    case 0:
                        totalTokens += pressesA * 3 + pressesB;
                        pressesA = 1000; //Will end the loop
                        break;
                    case 1:
                        pressesB--;
                        break;
                }
            } while (pressesA <= 100 && pressesB >= 0);
        }



        Console.WriteLine($"Step one result: {totalTokens}");
    }


    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(13, true);
        var inputList = _inputFiles.SplitString(input).ToList();
        
        var gameMachines = new List<GameMachine>();

        while (inputList.Count > 0)
        {
            var buttonAString = inputList[0];
            var buttonASplitString = buttonAString.Split(' ');
            var buttonATuple = new Tuple<int, int>(int.Parse(buttonASplitString[2].Substring(2, buttonASplitString[2].Length-3)), int.Parse(buttonASplitString[3].Substring(2)));
            inputList.RemoveAt(0);
            var buttonBString = inputList[0];
            var buttonBSplitString = buttonBString.Split(' ');
            var buttonBTuple = new Tuple<int, int>(int.Parse(buttonBSplitString[2].Substring(2, buttonBSplitString[2].Length-3)), int.Parse(buttonBSplitString[3].Substring(2)));
            inputList.RemoveAt(0);
            var prizeString = inputList[0];
            var prizeSplitString = prizeString.Split(' ');
            var prizeTuple = new Tuple<long, long>(int.Parse(prizeSplitString[1].Substring(2, prizeSplitString[1].Length-3)) + 10000000000000, int.Parse(prizeSplitString[2].Substring(2)) + 10000000000000);
            inputList.RemoveAt(0);
            if (inputList.Count > 0) inputList.RemoveAt(0);
            
            gameMachines.Add(new GameMachine(buttonATuple, buttonBTuple, prizeTuple));
        }


        var totalTokens = 0l;

        Stopwatch stopwatch = Stopwatch.StartNew();
        
        foreach (var gameMachine in gameMachines)
        {
            var iterations = 0l;
            var pressesA = 0l;
            var pressesB = 10000000000000 / long.Max(gameMachine.ButtonB.Item1, gameMachine.ButtonB.Item2);
            do
            {
                if (iterations++ % 100000000 == 0) Console.WriteLine($"Presses. A: {pressesA}   B: {pressesB}  Execution time: {stopwatch.Elapsed}");
                
                var result = gameMachine.CheckPresses(pressesA, pressesB);

                switch (result)
                {
                    case -1:
                        //Console.WriteLine($"Presses: A: {pressesA}  B: {pressesB}");
                        pressesA++;
                        break;
                    case 0:
                        totalTokens += pressesA * 3 + pressesB;
                        pressesA = 10000000000001; //Will end the loop
                        break;
                    case 1:
                        pressesB--;
                        break;
                }
            } while (pressesA <= 10000000000000 / long.Max(gameMachine.ButtonA.Item1, gameMachine.ButtonA.Item2) && pressesB >= 0);
        }



        Console.WriteLine($"Step two result: {totalTokens}");
    }

}