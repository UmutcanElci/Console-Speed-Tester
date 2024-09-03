namespace SpeedTester;

using System.Diagnostics;

public class SpeedTest
{
    public string Test()
    {
        string absPath = "C:\\Users\\Malat\\Desktop\\Code\\NetSpeed\\SpeedTester\\speedtest.exe";
        try
        {
            using (Process process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = absPath; 
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}