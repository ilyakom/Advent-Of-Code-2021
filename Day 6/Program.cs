using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_6
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<int, long> dic = new Dictionary<int, long> 
			{
				{0,0},
				{1,0},
				{2,0},
				{3,0},
				{4,0},
				{5,0},
				{6,0},
				{7,0},
				{8,0},
			};

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					var nums = s.ReadToEnd().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

					foreach(var n in nums)
					{
						dic[n]++;
					}
				}
			}

			for (int i = 0; i < 256; i++)
			{
				var save = dic[0];

				for(int j = 0; j < 8; j++)
				{
					dic[j] = dic[j + 1];
				}

				dic[6] += save;

				dic[8] = save;
			}

			Console.WriteLine(dic.Sum(itm => itm.Value));
		}
	}
}
