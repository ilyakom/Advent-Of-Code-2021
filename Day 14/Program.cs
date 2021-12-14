// See https://aka.ms/new-console-template for more information
StarOne();
StarTwo();

// let's bruteforce it
void StarOne()
{
	var result = 0;

	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			var temp = s.ReadLine();

			s.ReadLine();

			var inses = new List<List<string>>();

			while (!s.EndOfStream)
			{
				var line = s.ReadLine().Split("->", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
				inses.Add(line);
			}

			for (int i = 0; i < 10; i++)
			{
				var toApply = new List<(string, int)>();
				var applied = 0;

				for (int j = 1; j < temp.Length; j++)
				{
					foreach (var pair in inses)
					{
						if (temp[j-1] == pair[0][0] && temp[j] == pair[0][1])
						{
							toApply.Add((pair[1], j + applied));
							applied++;
							break;
						}
					}
				}

				for (int k = 0; k < toApply.Count; k++)
				{
					var pair = toApply[k];
					temp = temp.Insert(pair.Item2, pair.Item1);
				}
			}

			var dic = new Dictionary<char, int>();

			foreach(var c in temp)
			{
				if (dic.ContainsKey(c))
				{
					dic[c]++;
				}
				else
				{
					dic.Add(c, 1);
				}
			}

			Console.WriteLine(dic.Max(x => x.Value) - dic.Min(x => x.Value));
		}
	}

	
}

// sequence is not important, we just need pairs
void StarTwo()
{
	var result = 0;

	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			var temp = s.ReadLine().Trim().ToCharArray();
			var pairs = new Dictionary<string, long>();

			for (int i = 1; i < temp.Length; i++)
			{
				var pair = new string(new ReadOnlySpan<char>(temp, i - 1, 2));

				if (pairs.ContainsKey(pair))
				{
					pairs[pair]++;
				}
				else
				{
					pairs.Add(pair, 1);
				}
			}

			s.ReadLine();

			var inses = new List<List<string>>();

			while (!s.EndOfStream)
			{
				var line = s.ReadLine().Split("->", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
				inses.Add(line);
			}

			for (int i = 0; i < 40; i++)
			{
				var toAdd = new List<(string, long)>();
				var toRemove = new List<string>();

				foreach (var ins in inses)
				{
					if (pairs.TryGetValue(ins[0], out var cnt))
					{
						toAdd.Add((new string(new char[] {ins[0][0], ins[1][0]}), cnt));
						toAdd.Add((new string(new char[] {ins[1][0], ins[0][1]}), cnt));

						toRemove.Add(ins[0]);
					}
				}
				

				foreach(var pair in toRemove)
				{
					pairs.Remove(pair);
				}

				foreach(var pair in toAdd)
				{
					if (pairs.ContainsKey(pair.Item1))
					{
						pairs[pair.Item1] += pair.Item2;
					}
					else
					{
						pairs[pair.Item1] = pair.Item2;
					}
				}
			}

			var dic = new Dictionary<char, long>();

			foreach (var pair in pairs)
			{
				if (dic.ContainsKey(pair.Key[0]))
				{
					dic[pair.Key[0]] += pair.Value;
				}
				else
				{
					dic.Add(pair.Key[0], pair.Value);
				}

				if (dic.ContainsKey(pair.Key[1]))
				{
					dic[pair.Key[1]] += pair.Value;
				}
				else
				{
					dic.Add(pair.Key[1], pair.Value);
				}
			}

			// every char contains in 2 adjacent pairs
			Console.WriteLine(Math.Ceiling((dic.Max(x => x.Value) - dic.Min(x => x.Value)) / 2.0));
		}
	}
}