// See https://aka.ms/new-console-template for more information

StarOne();
StarTwo();

void StarOne()
{
	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			var map = new List<List<DNode>>();
			var idx = 0;

			while (!s.EndOfStream)
            {
				var line = s.ReadLine().Trim();

				var tmp = new List<DNode>();

                for (int i = 0; i < line.Length; i++)
                {
					tmp.Add(new DNode { X = idx, Y = i, Value = line[i] - '0' });
                }

				map.Add(tmp);

				idx++;
			}

			var hs = new HashSet<DNode>();

			map[0][0].Weight = 0;
			hs.Add(map[0][0]);

			while(hs.Count > 0)
            {
				var cur = hs.Min();
				var t = hs.Remove(cur);

				if (!t)
                {
					Console.WriteLine();
                }

				if (cur.X+1 < map.Count && !map[cur.X+1][cur.Y].Visited)
				{
					var el = map[cur.X + 1][cur.Y];

					if (el.Weight > cur.Weight + el.Value)
                    {
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				if (cur.Y + 1 < map.Count && !map[cur.X][cur.Y + 1].Visited)
				{
					var el = map[cur.X][cur.Y + 1];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				if (cur.X - 1 >= 0 && !map[cur.X - 1][cur.Y].Visited)
				{
					var el = map[cur.X - 1][cur.Y];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				if (cur.Y - 1 >= 0 && !map[cur.X][cur.Y - 1].Visited)
				{
					var el = map[cur.X][cur.Y - 1];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				cur.Visited = true;
			}

			Console.WriteLine(map.Last().Last().Weight);
		}
	}
}

void StarTwo()
{
	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			var map = new List<List<DNode>>();
			var idx = 0;

			while (!s.EndOfStream)
			{
				var line = s.ReadLine().Trim();

				var tmp = new List<DNode>();

				for (int i = 0; i < line.Length; i++)
				{
					tmp.Add(new DNode { X = idx, Y = i, Value = line[i] - '0' });
				}

				for(int i = 0; i < 4; i++)
                {
					for(int j = 0; j < 100; j++)
                    {
						tmp.Add(new DNode { 
							X = idx, 
							Y = tmp[i * 100 + j].Y + 100, 
							Value = tmp[i * 100 + j].Value == 9 ? 1 : tmp[i * 100 + j].Value + 1 });
					}
				}

				map.Add(tmp);

				idx++;
			}

			for (int i = 0; i < 4; i++)
			{
				for (int k = 0; k < 100; k++)
				{
					var nr = new List<DNode>();

					for (int j = 0; j < 500; j++)
					{
						nr.Add(new DNode
						{
							X = 100 + 100 * i + k,
							Y = map[0][j].Y,
							Value = map[i * 100 + k][j].Value == 9 ? 1 : map[i * 100 + k][j].Value + 1
						});
					}

					map.Add(nr);
				}
			}

			var hs = new HashSet<DNode>();

			map[0][0].Weight = 0;
			hs.Add(map[0][0]);

			while (hs.Count > 0)
			{
				var cur = hs.Min();
				var t = hs.Remove(cur);

				if (!t)
				{
					Console.WriteLine();
				}

				if (cur.X + 1 < map.Count && !map[cur.X + 1][cur.Y].Visited)
				{
					var el = map[cur.X + 1][cur.Y];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				if (cur.Y + 1 < map.Count && !map[cur.X][cur.Y + 1].Visited)
				{
					var el = map[cur.X][cur.Y + 1];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				if (cur.X - 1 >= 0 && !map[cur.X - 1][cur.Y].Visited)
				{
					var el = map[cur.X - 1][cur.Y];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				if (cur.Y - 1 >= 0 && !map[cur.X][cur.Y - 1].Visited)
				{
					var el = map[cur.X][cur.Y - 1];

					if (el.Weight > cur.Weight + el.Value)
					{
						hs.Remove(el);
						el.Weight = cur.Weight + el.Value;
					}

					hs.Add(el);
				}

				cur.Visited = true;
			}

			Console.WriteLine(map.Last().Last().Weight);
		}
	}
}

internal class DNode : IComparable<DNode>
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Weight { get; set; }
    public bool Visited { get; set; }
    public int Value { get; set; }

    public DNode()
    {
		Weight = int.MaxValue;
    }

    public override int GetHashCode()
    {
		var hc = HashCode.Combine(X, Y);
		return hc;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (ReferenceEquals(obj, null))
        {
            return false;
        }

        return obj is DNode dnode && this.X == dnode.X && this.Y == dnode.Y;
    }

    public int CompareTo(DNode? other)
    {
		if (this.X == other.X && this.Y == other.Y)
        {
			return 0;
        }

		if (this.Weight - other.Weight == 0)
        {
			return 1;
        }
		else
        {
			return this.Weight - other.Weight;

		}
    }
}