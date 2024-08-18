// See https://aka.ms/new-console-template for more information


using SpeedTester;

string result = new SpeedTest().Test();

ParseAndPrintSpeedTestResult(result);

static void ParseAndPrintSpeedTestResult(string result)
{
    
    string[] delimiters = new string[]
    {
        "Speedtest by Ookla",
        "Server:",
        "ISP:",
        "Idle Latency:",
        "Download:",
        "Upload:",
        "Packet Loss:",
        "Result URL:"
    };

    foreach (string delimiter in delimiters)
    {
        int startIndex = result.IndexOf(delimiter, StringComparison.Ordinal);
        if (startIndex >= 0)
        {
            // Extract section text
            int endIndex = result.IndexOf('\n', startIndex);
            if (endIndex == -1)
                endIndex = result.Length;
            
            string line = result.Substring(startIndex, endIndex - startIndex).Trim();
            
            Console.WriteLine(line + "\n");
        }
    }
}

