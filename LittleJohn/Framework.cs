using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleJohn
{
	public static class Framework
	{
		public static int ToInt32(this string str, string encodingName = "ASCII")
		{
			System.Text.Encoding encoding = System.Text.Encoding.GetEncoding(encodingName);

			if (string.IsNullOrEmpty(str))
				return 0;

			return encoding.GetBytes(str).Sum(b => b);
		}

		public static string Reverse(this string str)
		{
			char[] charArray = str.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
