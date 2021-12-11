using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_7
{
	class Program
	{
		static void Main(string[] args)
		{
			StarTwo();
		}

		private static void StarTwo()
		{
			List<int> lst = null;//new List<int> { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					lst = s.ReadToEnd().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
				}
			}

			lst.Sort();

			var dist = CalcDist(lst, lst[0]);

			var min = dist;
			var minPoint = lst[0];

			for (int i = lst[0] + 1; i < lst[lst.Count - 1]; i++)
			{
				dist = CalcDist(lst, i);

				if (dist < min)
				{
					min = dist;
					minPoint = i;
				}
			}

			Console.WriteLine(min + " " + minPoint);

			int CalcDist(List<int> lst, int mid)
			{
				return lst.Select(x => Math.Abs(x - mid)).Sum(y => (1 + y) * y / 2);
			}
		}

		private static void StarOne()
		{
			List<int> lst = null;//new List<int> { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					lst = s.ReadToEnd().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
				}
			}

			lst.Sort();

			var dist = CalcDist(lst, lst[0]);

			var min = dist;
			var minPoint = lst[0];

			for (int i = lst[0] + 1; i < lst[lst.Count - 1]; i++)
			{
				var l = lst.Count(x => x < i);

				dist += l;
				dist -= lst.Count - l;

				if (dist < min)
				{
					min = dist;
					minPoint = i;
				}
			}

			Console.WriteLine(min + " " + minPoint);

			int CalcDist(List<int> lst, int mid)
			{
				return lst.Select(x => Math.Abs(x - mid)).Sum();
			}
		}
	}
}
