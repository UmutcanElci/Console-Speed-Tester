using System;
using System.Collections.Generic;
using System.Linq;
using SpeedTester;
using Spectre.Console;

var speedTest = new SpeedTest();

int runTimes = 5;
List<double> downloadSpeeds = new List<double>();
List<double> uploadSpeeds = new List<double>();
List<double> latencyValues = new List<double>(); // For Latency

for (int i = 0; i < runTimes; i++)
{
    Console.WriteLine($"Running test {i + 1}...");
    string result = speedTest.Test();
    Console.WriteLine("Raw Result:");
    Console.WriteLine(result);
    
    ParseAndPrintSpeedTestResult(result, downloadSpeeds, uploadSpeeds, latencyValues);
    Console.WriteLine($"Test {i + 1} completed.");

    System.Threading.Thread.Sleep(1000);
}

double downloadAvg = downloadSpeeds.Count > 0 ? downloadSpeeds.Average() : 0;
double uploadAvg = uploadSpeeds.Count > 0 ? uploadSpeeds.Average() : 0;
double latencyAvg = latencyValues.Count > 0 ? latencyValues.Average() : 0;

Console.WriteLine($"Average Download Speed: {downloadAvg} Mbps");
Console.WriteLine($"Average Upload Speed: {uploadAvg} Mbps");
Console.WriteLine($"Average Latency: {latencyAvg} ms");

static void ParseAndPrintSpeedTestResult(string result, List<double> downloadSpeeds, List<double> uploadSpeeds, List<double> latencyValues)
{
    // Delimiters to look for in the result string
    string[] delimiters = new string[]
    {
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
        Console.WriteLine($"Looking for delimiter: {delimiter}");

        if (result.Contains(delimiter))
        {
            Console.WriteLine($"{delimiter} found in the result string.");

            int startIndex = result.IndexOf(delimiter, StringComparison.Ordinal) + delimiter.Length;
            int endIndex = result.IndexOf("ms", startIndex);
            if (delimiter == "Download:" || delimiter == "Upload:")
            {
                endIndex = result.IndexOf("Mbps", startIndex);
            }

            if (endIndex != -1)
            {
                string extractedValue = result.Substring(startIndex, endIndex - startIndex).Trim();
                //Console.WriteLine($"Extracted Value: {extractedValue}");

                if (double.TryParse(extractedValue, out double resultValue))
                {
                    if (delimiter == "Download:")
                    {
                        downloadSpeeds.Add(resultValue);
                        //Console.WriteLine($"Parsed Download Speed: {resultValue} Mbps");
                    }
                    else if (delimiter == "Upload:")
                    {
                        uploadSpeeds.Add(resultValue);
                        //Console.WriteLine($"Parsed Upload Speed: {resultValue} Mbps");
                    }
                    else if (delimiter == "Idle Latency:")
                    {
                        latencyValues.Add(resultValue);
                        //Console.WriteLine($"Parsed Latency: {resultValue} ms");
                    }
                }
            }         
        }
       
    }
}
