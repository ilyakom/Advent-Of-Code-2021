using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_10
{
	class Program
	{
		static void Main(string[] args)
		{
			OneStar();
			TwoStar();
		}

		private static void OneStar()
		{
			Dictionary<char, int> dic = new Dictionary<char, int>
			{
				{ ')', 3 },
				{ ']', 57 },
				{ '}', 1197 },
				{ '>', 25137 }
			};

			var result = 0;

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						var line = s.ReadLine();
						var st = new Stack<char>();

						foreach (var c in line)
						{
							if (c == '(' || c == '[' || c == '{' || c == '<')
							{
								st.Push(c);
							}
							else
							{
								var cur = st.Pop();

								if (c == ')' && cur == '(' ||
									c == '}' && cur == '{' ||
									c == ']' && cur == '[' ||
									c == '>' && cur == '<')
								{
									continue;
								}

								result += dic[c];
								break;
							}
						}
					}

					Console.WriteLine(result);
				}
			}
		}

		private static void TwoStar()
		{
			Dictionary<char, int> dic = new Dictionary<char, int>
			{
				{ '(', 1 },
				{ '[', 2 },
				{ '{', 3 },
				{ '<', 4 }
			};

			
			var lines = new List<long>();

			using (var reader = File.OpenRead("./input.txt"))
			{
				using (var s = new StreamReader(reader))
				{
					while (!s.EndOfStream)
					{
						long result = 0;
						var line =  s.ReadLine();
						var st = new Stack<char>();

						var isCorrect = true;

						foreach (var c in line)
						{
							if (c == '(' || c == '[' || c == '{' || c == '<')
							{
								st.Push(c);
							}
							else
							{
								var cur = st.Pop();

								if (c == ')' && cur == '(' ||
									c == '}' && cur == '{' ||
									c == ']' && cur == '[' ||
									c == '>' && cur == '<')
								{
									continue;
								}

								isCorrect = false;
								break;
							}
						}

						if (!isCorrect)
						{
							continue;
						}
						
						while(st.Count != 0)
						{
							result = result * 5 + dic[st.Pop()];
						}

						lines.Add(result);
					}

					lines.Sort();

					Console.WriteLine(lines[lines.Count / 2]);
				}
			}
		}
	}
}
