using System;
using System.Collections.Generic;
using System.IO;

namespace Day_2
{
	class Program
	{
		static void Main(string[] args)
		{
			StarTwo();
		}

		private static void StarTwo()
		{
			var hor = 0;
			var ver = 0;
			var aim = 0;

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var line = s.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

						if (line[0] == "forward")
						{
							hor += int.Parse(line[1]);
							ver += aim * int.Parse(line[1]);
						}
						else if (line[0] == "up")
						{
							aim -= int.Parse(line[1]);
						}
						else
						{
							aim += int.Parse(line[1]);
						}

						if (ver < 0)
						{
							ver = 0;
						}

						
					}
				}
			}

			Console.WriteLine(hor * ver);
		}

		private static void StarOne()
		{
			var hor = 0;
			var ver = 0;

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var line = s.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

						if (line[0] == "forward")
						{
							hor += int.Parse(line[1]);
						}
						else if (line[0] == "up")
						{
							ver -= int.Parse(line[1]);
						}
						else
						{
							ver += int.Parse(line[1]);
						}

						if (ver < 0)
						{
							ver = 0;
						}
					}
				}
			}

			Console.WriteLine(hor * ver);
		}
	}
}
