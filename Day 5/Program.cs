using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_5
{
	class Program
	{
		static void Main(string[] args)
		{
			StarOne();
		}

		private static void StarOne()
		{
			var lines = new List<Line>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var p = s.ReadLine().Split(" -> ");

						var p1 = p[0].Split(',').Select(int.Parse).ToArray();
						var p2 = p[1].Split(',').Select(int.Parse).ToArray();

						var line = new Line(p1[0], p1[1], p2[0], p2[1]);

						lines.Add(line);
					}
				}
			}

			var result = 0;

			for(int i = 0; i < 1000; i++)
			{
				for (int j = 0; j < 1000; j++)
				{
					var cur = 0;

					foreach(var line in lines)
					{
						if (line.IsInLine(i, j))
						{
							cur++;

							if (cur > 1)
							{
								result++;
								break;
							}
						}
					}
				}
			}

			Console.WriteLine(result);
		}

		private class Line
		{
			private int _x1;
			private int _y1;

			private int _x2;
			private int _y2;

			private bool _isHorOrver;

			public Line(int x1, int y1, int x2, int y2)
			{
				_x1 = x1;
				_x2 = x2;
				_y1 = y1;
				_y2 = y2;

				_isHorOrver = (_x1 == _x2) || (_y1 == _y2);
			}

			public bool IsHorOrVer()
			{
				return _isHorOrver;
			}

			public bool IsInLine(int x, int y)
			{
				return  ((_y1 - _y2) * (x - _x1) == (_x1 - _x2) * (y - _y1)) && 
					(_x1 < _x2 ? x >= _x1 && x <= _x2 : x >= _x2 && x <= _x1) &&
					(_y1 < _y2 ? y >= _y1 && y <= _y2 : y >= _y2 && y <= _y1);
			}
		}
	}
}
