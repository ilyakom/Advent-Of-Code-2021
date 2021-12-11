using System;
using System.Collections.Generic;
using System.IO;

namespace Day_3
{
	class Program
	{
		static void Main(string[] args)
		{
			StarTwo();
		}

		private static void StarOne()
		{
			var ones = new int[12];
			var zers = new int[12];

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var line = s.ReadLine();

						for(var i = 0; i < line.Length; i++)
						{
							if (line[i] == '0')
							{
								zers[i]++;
							}
							else
							{
								ones[i]++;
							}
						}
					}
				}
			}

			var most = string.Empty;
			var less = string.Empty;

			for(int i = 0; i < 12; i++)
			{
				if (zers[i] < ones[i])
				{
					most = most + "1";
					less = less + "0";
				}
				else
				{
					most = most + "0";
					less = less + "1";
				}

			}

			Console.WriteLine(Convert.ToInt32(string.Join("", most), 2) * Convert.ToInt32(string.Join("", less), 2));
		}

		private static void StarTwo()
		{
			var listOnes = new List<string>();
			var listZeroes = new List<string>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var str = s.ReadLine();

						if (str[0] == '0')
						{
							listZeroes.Add(str);
						}
						else
						{
							listOnes.Add(str);
						}
					}
				}
			}

			var most = listOnes.Count >= listZeroes.Count ? listOnes : listZeroes;
			var less = listOnes.Count >= listZeroes.Count ? listZeroes : listOnes;

			for(int i = 1; i < 12 && most.Count != 1; i++)
			{
				var (zeroes, ones) = Split(most, i);

				most = ones.Count >= zeroes.Count ? ones : zeroes;
			}

			for (int i = 1; i < 12 && less.Count != 1; i++)
			{
				var (zeroes, ones) = Split(less, i);

				less = ones.Count >= zeroes.Count ? zeroes : ones;
			}

			Console.WriteLine(Convert.ToInt32(string.Join("", most), 2) * Convert.ToInt32(string.Join("", less), 2));
		}

		private static (List<string>, List<string>) Split(List<string> lst, int i)
		{
			var listOnes = new List<string>();
			var listZeroes = new List<string>();

			foreach (var e in lst)
			{
				if (e[i] == '0')
				{
					listZeroes.Add(e);
				}
				else
				{
					listOnes.Add(e);
				}
			}

			return (listZeroes, listOnes);
		}
	}
}
