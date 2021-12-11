using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_8
{
	class Program
	{
		private static Dictionary<int, int> map = new Dictionary<int, int>
		{
			{2, 1},
			{4, 4},
			{3, 7},
			{7, 8}
		};

		static void Main(string[] args)
		{
			//StarOne();
			StarTwo();
		}

		private static void StarOne()
		{
			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					var result = 0;

					while (!s.EndOfStream)
					{
						var parts = s.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);

						var output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

						foreach (var str in output)
						{
							if (map.ContainsKey(str.Length))
							{
								result++;
							}
						}

						
					}

					Console.WriteLine(result);
				}
			}
		}

		private static void StarTwo()
		{
			var dic = new HashSet<char>[10];

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					var result = 0;

					while (!s.EndOfStream)
					{
						var parts = s.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);

						var input = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

						for (int i = 0; i < input.Count; i++)
						{
							var str = input[i];

							switch (str.Length)
							{
								case 2:
									dic[1] = new HashSet<char>(str);
									input.RemoveAt(i);
									i--;
									break;
								case 4:
									dic[4] = new HashSet<char>(str);
									input.RemoveAt(i);
									i--;
									break;
								case 3:
									dic[7] = new HashSet<char>(str);
									input.RemoveAt(i);
									i--;
									break;
								case 7:
									dic[8] = new HashSet<char>(str);
									input.RemoveAt(i);
									i--;
									break;
								default:
									break;
							}
						}

						// TOP
						char top = '_';

						foreach(var c in dic[7])
						{
							if (!dic[1].Contains(c))
							{
								top = c;
								break;
							}
						}


						// NINE + DOWN
						dic[9] = new HashSet<char>(dic[4]);
						dic[9].Add(top);

						char down = '_';

						for (int i = 0; i < input.Count; i++)
						{
							var str = input[i];

							if (str.Length == 6)
							{
								var cnt = str.Count(x => dic[9].Contains(x));

								if (cnt == 5)
								{
									down = str.First(x => !dic[9].Contains(x));
									dic[9].Add(down);
									input.RemoveAt(i);
									break;
								}
							}
						}

						// DOWN LEFT
						var downLeft = '_';

						foreach(var c in dic[8])
						{
							if (!dic[9].Contains(c))
							{
								downLeft = c;
								break;
							}
						}

						// THREE
						var topLeft = '_';

						for (int i = 0; i < input.Count; i++)
						{
							var str = input[i];

							if (str.Length == 5)
							{
								var cnt9 = str.Count(x => dic[9].Contains(x));
								var cnt1 = str.Count(x => dic[1].Contains(x));

								if (cnt9 == 5 && cnt1 == 2)
								{
									topLeft = dic[9].First(x => !str.Contains(x));
									dic[3] = new HashSet<char>(str);
									input.RemoveAt(i);
									break;
								}
							}
						}

						// MID
						var mid = '_';

						var tmp = new HashSet<char>(dic[3]);

						foreach(var c in dic[7])
						{
							tmp.Remove(c);
						}

						tmp.Remove(down);

						mid = tmp.First();

						// ZERO
						dic[0] = new HashSet<char>(dic[8]);
						dic[0].Remove(mid);

						// SIX
						for (int i = 0; i < input.Count; i++)
						{
							var str = input[i];

							if (str.Length == 6)
							{
								var cnt = str.Count(x => dic[0].Contains(x));

								if (cnt == 6)
								{
									input.RemoveAt(i);
									i--;
								}
								else
								{
									dic[6] = new HashSet<char>(str);
									input.RemoveAt(i);
									i--;
								}
							}
						}



						// TOP RIGHT
						var topRight = dic[8].First(x => !dic[6].Contains(x));

						// FIVE
						dic[5] = new HashSet<char>(dic[6]);
						dic[5].Remove(downLeft);

						//DOWN RIGHT
						var downRight = dic[1].First(x => x != topRight);

						// TWO
						dic[2] = new HashSet<char>(dic[8]);
						dic[2].Remove(topLeft);
						dic[2].Remove(downRight);

						// MAP SORTED STRING TO DIGIT
						var dict = new Dictionary<string, int>();

						for(int i = 0; i < dic.Length; i++)
						{
							var r = dic[i].ToList();
							r.Sort();

							dict.Add(string.Join("", r), i);
						}

						// PROCESS OUTPUT
						var output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
						var sr = string.Empty;

						foreach(var str in output)
						{
							var t = str.ToList();
							t.Sort();

							dict.TryGetValue(string.Join("",t), out var elem);

							sr += elem;
						}

						result += int.Parse(sr);
					}

					Console.WriteLine(result);
				}
			}
		}
	}
}
