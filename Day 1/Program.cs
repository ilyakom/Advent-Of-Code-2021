using System;
using System.Collections.Generic;
using System.IO;

namespace Days
{
	class Program
	{
		static void Main(string[] args)
		{
			StarOne();
			StarTwo();
		}

		private static void StarTwo()
		{
			var result = 0;
			var list = new List<int>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						list.Add(int.Parse(s.ReadLine()));
					}
				}
			}

			for (int i = 0; i < list.Count-3; i++)
			{
				if (list[i] < list[i+3])
				{
					result++;
				}
			}

			Console.WriteLine(result);
		}

		private static void StarOne()
		{
			var result = 0;

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					var prev = int.Parse(s.ReadLine());

					while (!s.EndOfStream)
					{
						var cur = int.Parse(s.ReadLine());

						if (cur > prev)
						{
							result++;
						}

						prev = cur;
					}
				}
			}

			Console.WriteLine(result);
		}
	}
}
