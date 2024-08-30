namespace SpeedTester;

using System.Diagnostics;

public class SpeedTest
{
    public string Test()
    {
        try
        {
            using (Process process = new Process())
            {
                
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = "speedtest.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                
                //
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                List<char> results = result.ToList();

                return result.ToString();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
