using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_9
{
	class Program
	{
		static void Main(string[] args)
		{
			StarOnePlusTwo();
		}

		private static void StarOnePlusTwo()
		{
			var cave = new List<List<int>>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					var result = 1;
					var lp = new List<(int,int)>();
					var basins = new List<int>();

					while (!s.EndOfStream)
					{
						var parts = s.ReadLine();

						cave.Add(parts.Select(x => x-'0').ToList());
					}

					for (int i = 0; i < cave.Count; i++)
					{
						for (int j = 0; j < cave[i].Count; j++)
						{
							var cur = cave[i][j];

							if (i > 0 && cave[i-1][j] <= cur
								|| j+1 < cave[i].Count && cave[i][j+1] <= cur
								|| i+1 < cave.Count && cave[i + 1][j] <= cur
								|| j > 0 && cave[i][j-1] <= cur)
							{
								continue;
							}

							lp.Add((i, j));
						}
					}

					foreach(var p in lp)
					{
						basins.Add(Calc(cave, p.Item1, p.Item2));
					}

					foreach(var b in basins.OrderByDescending(x => x).Take(3))
					{
						result *= b;
					}

					Console.WriteLine(result);
				}
			}
		}

		private static int Calc(List<List<int>> cave, int i, int j)
		{
			var pl = 1;
			var cur = cave[i][j];
			cave[i][j] = -1;

			if (i > 0 && cave[i - 1][j] > cur && cave[i - 1][j] != 9)
			{
				pl += Calc(cave, i - 1, j);
			}

			if (i+1 < cave.Count && cave[i+1][j] > cur && cave[i+1][j] != 9)
			{
				pl += Calc(cave, i+1, j);
			}

			if (j > 0 && cave[i][j-1] > cur && cave[i][j-1] != 9)
			{
				pl += Calc(cave, i, j-1);
			}

			if (j+1 < cave[0].Count && cave[i][j+1] > cur && cave[i][j+1] != 9)
			{
				pl += Calc(cave, i, j+1);
			}

			return pl;
		}
	}
}
