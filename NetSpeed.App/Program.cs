// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using SpeedTester;
using Spectre.Console;

var speedTest = new SpeedTest();

int runTimes = 5;
List<double> downloadSpeeds = new List<double>();
List<double> uploadSpeeds = new List<double>();

for (int i = 0; i < runTimes; i++)
{
    Console.WriteLine($"Running test {i + 1}...");
    string result = speedTest.Test();
    ParseAndPrintSpeedTestResult(result,  downloadSpeeds, uploadSpeeds);
    Console.WriteLine($"Test {i + 1} completed.");
 
    System.Threading.Thread.Sleep(1000);
}

double downloadAvg = downloadSpeeds.Count > 0 ? downloadSpeeds.Average() : 0;
double uploadAvg = uploadSpeeds.Count > 0 ? uploadSpeeds.Average() : 0;

Console.WriteLine($"Average Download Speed: {downloadAvg} Mbps");
Console.WriteLine($"Average Upload Speed: {uploadAvg} Mbps");

static void ParseAndPrintSpeedTestResult(string result,  List<double> downloadSpeeds, List<double> uploadSpeeds)
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
                        //latency += resultValue;
                    }
                    else if (delimiter == "Download:")
                    {
                        downloadSpeeds.Add(resultValue);
                    }
                    else if (delimiter == "Upload:")
                    {
                        uploadSpeeds.Add(resultValue);
                    }
                }
            }
        }
    }

    
    
}