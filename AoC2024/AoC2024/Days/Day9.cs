namespace AoC2024;

public class Day9
{
    private readonly InputFiles _inputFiles = new InputFiles();

    public void Step1()
    {
        var input = _inputFiles.ReadInputFileForDay(9, false);

        var harddrive = new List<int>();
        var inputChars = input.ToCharArray();
        var isFile = true;
        var currentFileId = 0;

        foreach (var inputChar in inputChars)
        {
            for (var i = 0; i < int.Parse(inputChar.ToString()); i++) // Yes, this is what is known as most efficient code ever
            {
                if (isFile)
                {
                    harddrive.Add(currentFileId);
                }
                else
                {
                    harddrive.Add(-1);
                }
            }

            isFile = !isFile;
            if (isFile) currentFileId++;
        }

        var leftPos = 0;
        var rightPos = harddrive.Count - 1;

        while (leftPos < rightPos)
        {
            if (harddrive[leftPos] >= 0)
            {
                leftPos++;
                continue;
            }

            if (harddrive[rightPos] < 0)
            {
                rightPos--;
                continue;
            }

            harddrive[leftPos] = harddrive[rightPos];
            harddrive[rightPos] = -1;
            leftPos++;
            rightPos--;
        
            //harddrive.ForEach(position => Console.Write($"{position}|"));
            //Console.WriteLine();
        }

        var checksum = 0l;
        for (var i = 0; i < harddrive.Count; i++)
        {
            if (harddrive[i] < 1) continue;
            checksum += harddrive[i] * i;
        }
        
        
        Console.WriteLine($"Step one result: {checksum}");
    }
    
    
    
    public void Step2()
    {
        var input = _inputFiles.ReadInputFileForDay(9, false);

        var harddrive = new List<int>();
        var inputChars = input.ToCharArray();
        var isFile = true;
        var currentFileId = 0;

        foreach (var inputChar in inputChars)
        {
            for (var i = 0; i < int.Parse(inputChar.ToString()); i++) // Yes, this is what is known as most efficient code ever
            {
                if (isFile)
                {
                    harddrive.Add(currentFileId);
                }
                else
                {
                    harddrive.Add(-1);
                }
            }

            isFile = !isFile;
            if (isFile) currentFileId++;
        }

        var leftPos = 0;
        var rightPos = harddrive.Count - 1;
        var lastSeenFile = 99999999;

        while (rightPos > 2)
        {
            leftPos = 0;

            if (harddrive[rightPos] < 0)
            {
                rightPos--;
                continue;
            }

            var fileNumber = harddrive[rightPos];
            if (fileNumber >= lastSeenFile)
            {
                rightPos--; 
                continue;
            }

            lastSeenFile = fileNumber;

            var fileLength = 1;
            try
            {
                for (var i = rightPos - 1; fileNumber == harddrive[i]; i--)
                {
                    fileLength++;
                }
            }
            catch
            {
                // The beauty of not having to put this code into production is...this...
                rightPos = 0;
                continue;
            }

            for (var i = 0; i < rightPos; i++)
            {
                var fileWillFit = true;
                if (harddrive[i] >= 0) continue;

                for (var j = i; j < i + fileLength; j++)
                {
                    if (harddrive[j] >= 0)
                    {
                        fileWillFit = false;
                        break;
                    }
                }

                if (fileWillFit)
                {
                    for (var j = i; j < i + fileLength; j++)
                    {
                        harddrive[j] = fileNumber;
                    }

                    for (var j = rightPos; j > rightPos - fileLength; j--)
                    {
                        harddrive[j] = -1;
                    }

                    break;
                }
            }
            
            rightPos--;
        
            //harddrive.ForEach(position => Console.Write($"{position}|"));
            //Console.WriteLine();
        }

        var checksum = 0l;
        for (var i = 0; i < harddrive.Count; i++)
        {
            if (harddrive[i] < 1) continue;
            checksum += harddrive[i] * i;
        }
        
        
        Console.WriteLine($"Step two result: {checksum}");
    }
}