// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using SpeedTester;
using Spectre.Console;

string result = new SpeedTest().Test();
ParseAndPrintSpeedTestResult(result);

static void ParseAndPrintSpeedTestResult(string result)
{
    string[] delimiters = new string[]
    {
        "Speedtest by Ookla",
        "Server",
        "ISP",
        "Idle Latency",
        "Download",
        "Upload",
        "Packet Loss",
        "Result URL"
    };

    foreach (string delimiter in delimiters)
    {
        if (result.Contains(delimiter))
        {
            //Find the start of the result of delimiters
            int startIndex = result.IndexOf(delimiter, StringComparison.Ordinal) + delimiter.Length;
            //Find the end of the line \n
            int endIndex = result.IndexOf('\n', startIndex);
            if (endIndex == -1)
            {
                endIndex = result.Length;
            }
            // Deleting extra spaces
            string extractedValue = result[startIndex..endIndex].Trim();

            
            AnsiConsole.Markup($"[yellow]{delimiter}[/]: [green]{extractedValue}[/]\n");

            double latency = 0.0;
            double downloadSpeed = 0.0;
            double uploadSpeed = 0.0;
            if (delimiter == "Idle Latency:" || delimiter == "Download:" || delimiter == "Upload:")
            {
                string numericLatency = new string(extractedValue.Where(char.IsDigit).ToArray());
                if (Double.TryParse(numericLatency, out double resultValue))
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine($"{delimiter} Value:{resultValue}\n");
                    if (delimiter == "Idle Latency:")
                    {
                        latency += resultValue;
                    }
                    else if (delimiter == "Download:")
                    {
                        downloadSpeed += resultValue;
                    }
                    else if (delimiter == "Upload:")
                    {
                        uploadSpeed += resultValue;
                    }
                }
            }
        }
    }

    double downloadAvg = 0.0;
    double uploadAvg = 0.0;
    
}