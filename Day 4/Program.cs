using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_4
{
	class Program
	{
		static void Main(string[] args)
		{
			StarOneAndTwo();
		}

		private static void StarOneAndTwo()
		{
			List<int> numbers = null;

			var wins = new List<int>();
			var boards = new List<Board>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					numbers = s.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

					s.ReadLine();

					Board brd = new Board();
					boards.Add(brd);

					while (!s.EndOfStream)
					{
						var line = s.ReadLine();

						if(string.IsNullOrWhiteSpace(line))
						{
							brd = new Board();
							boards.Add(brd);
						}

						foreach(var n in line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
						{
							brd.Add(n);
						}
					}
				}
			}

			//int min = int.MaxValue;
			int max = 0;
			Board minBoard = null;

			foreach(var brd in boards)
			{
				var cur = brd.WhenWins(numbers);

				if (cur.HasValue && cur.Value > max)
				{
					max = cur.Value;
					minBoard = brd;
				}
			}

			Console.WriteLine(numbers[max] * minBoard.GetUnmarkedSum());
		}
	}
}
