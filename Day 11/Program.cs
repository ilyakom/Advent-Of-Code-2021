using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_11
{
	class Program
	{
		static void Main(string[] args)
		{
			OneTwoStar();
		}

		private static void OneTwoStar()
		{
			var result = 0;
			var cave = new List<List<int>>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var line = s.ReadLine().Select(x => x-'0').ToList();
						cave.Add(line);
					}

					
				}
			}

            for (int step = 1; step < 1000; step++)
            {
				var flashed = new bool[10, 10];

				var cur = result;

				for (int i = 0; i < cave.Count; i++)
				{
					for (int j = 0; j < cave[0].Count; j++)
					{
						if (!flashed[i,j])
						{
							result += Inc(cave, flashed, i, j);
						}
					}
				}

				if (result - cur == 100)
                {
					Console.WriteLine("ALL: " + step);
                }
			}

			Console.WriteLine(result);
		}

		private static int Inc(List<List<int>> cave, bool[,] flashed, int i, int j)
        {
			if (i >= cave.Count || j >= cave[0].Count || i < 0 || j < 0 || flashed[i,j])
            {
				return 0;
            }

			cave[i][j]++;
			int res = 0;

			if (cave[i][j] > 9)
            {
				res++;
				flashed[i, j] = true;
				cave[i][j] = 0;

				res += Inc(cave, flashed, i - 1, j - 1) +
					Inc(cave, flashed, i - 1, j) +
					Inc(cave, flashed, i - 1, j + 1) +
					Inc(cave, flashed, i, j - 1) +
					Inc(cave, flashed, i, j + 1) +
					Inc(cave, flashed, i + 1, j - 1) +
					Inc(cave, flashed, i + 1, j) +
					Inc(cave, flashed, i + 1, j + 1);
			}

			return res;
        }
	}
}
