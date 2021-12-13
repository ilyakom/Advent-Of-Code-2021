// See https://aka.ms/new-console-template for more information


Stars();

void Stars()
{
	var points = new List<Point>();
	var folds = new List<string[]>();

    using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			while (!s.EndOfStream)
			{
				var line = s.ReadLine();

				if (!string.IsNullOrWhiteSpace(line))
                {
					var arr = line.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
					points.Add(new Point(arr[0], arr[1]));
                }
				else
                {
					break;
                }
			}

			while (!s.EndOfStream)
			{
				var line = s.ReadLine();

				folds.Add(line.Remove(0, "fold along ".Length).Split("="));
			}
		}
	}

    foreach (var fold in folds)
    {
		var axis = fold[0];
		var axisVal = int.Parse(fold[1]);

        foreach (var point in points)
        {
			if (axis == "y")
            {
				if (point.y > axisVal)
                {
					point.y = axisVal - (point.y - axisVal);
                }
            }
			else
            {
				if (point.x > axisVal)
				{
					point.x = axisVal - (point.x - axisVal);
				}
			}
        }
    }

	var hs = new HashSet<Point>();

	// STAR ONE (BUT FOR ALL FOLDS)
	foreach(var p in points)
    {
		hs.Add(p);
    }

	Console.WriteLine(hs.Count);

	// STAR TWO
	var pts = new bool[hs.Max(x => x.x)+1,hs.Max(y => y.y)+1];

	foreach(var point in points)
    {
		pts[point.x, point.y] = true;
    }

	for(int i = 0; i < hs.Max(y => y.y)+1; i++)
    {
        for (int j = 0; j < hs.Max(x => x.x)+1; j++)
        {
			if (pts[j,i])
            {
				Console.Write("#");
            }
            else
            {
				Console.Write(".");
            }
        }
		Console.WriteLine();
    }
}

class Point
{
	public int x;
	public int y;

    public Point(int x, int y)
    {
		this.x = x;
		this.y = y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override bool Equals(object? obj)
    {
        return obj is Point pt && pt.x == x && pt.y == y;
    }
}
