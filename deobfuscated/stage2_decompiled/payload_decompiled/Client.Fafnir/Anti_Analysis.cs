using System;
using System.Management;
using System.Threading;

namespace Client.Fafnir;

internal class Anti_Analysis
{
	public static void RunAntiAnalysis()
	{
		if (isVM_by_wim_temper())
		{
			Environment.FailFast(null);
		}
		Thread.Sleep(1000);
	}

	public static bool isVM_by_wim_temper()
	{
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new SelectQuery("Select * from Win32_CacheMemory"));
		int num = 0;
		foreach (ManagementObject item in managementObjectSearcher.Get())
		{
			_ = item;
			num++;
		}
		if (num == 0)
		{
			return true;
		}
		return false;
	}
}
