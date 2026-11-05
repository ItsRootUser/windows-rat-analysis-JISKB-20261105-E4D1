using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using D4HQJSCRM4;

namespace X6GVZ9YWFW;

[CompilerGenerated]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
internal class _30TATNR0TF
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				while (true)
				{
					int num = 1888579133;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x3F73F524)) % 3)
						{
						case 0u:
							break;
						case 2u:
							resourceMan = new ResourceManager(HE2HAIHY00.H3ALMMLJ7W<string>(1785990969u), typeof(_30TATNR0TF).Assembly);
							num = (int)(num2 * 1668808795) ^ -1998300969;
							continue;
						default:
							goto end_IL_0007;
						}
						break;
					}
					continue;
					end_IL_0007:
					break;
				}
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal _30TATNR0TF()
	{
	}
}
