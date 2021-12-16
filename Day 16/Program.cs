// See https://aka.ms/new-console-template for more information

using System.Text;

StarOne();

void StarOne()
{
	var dic = new Dictionary<char, string>
    {
		{'0', "0000"},
		{'1', "0001"},
		{'2', "0010"},
		{'3', "0011"},
		{'4', "0100"},
		{'5', "0101"},
		{'6', "0110"},
		{'7', "0111"},
		{'8', "1000"},
		{'9', "1001"},
		{'A', "1010"},
		{'B', "1011"},
		{'C', "1100"},
		{'D', "1101"},
		{'E', "1110"},
		{'F', "1111"}
	};

	string hexCode = null;

	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			hexCode = s.ReadLine();
		}
	}

	var sb = new StringBuilder();

    foreach (var c in hexCode)
    {
		sb.Append(dic[c]);
    }

	var binaryCode = sb.ToString();
	var sr = new Reader(binaryCode);

	Console.WriteLine(GetPacket(sr));	
}

long Go(Reader sr, Op op, int cnt = -1)
{
	var sum = op == Op.Sum ? 0L : 
		op == Op.Prod ? 1L : 
		op == Op.Min ? long.MaxValue :
		op == Op.Max ? long.MinValue : 0L;

	var idx = 0;

	while ((cnt == -1 && !sr.IsEnd) || (cnt != -1 && idx < cnt))
	{
		if (op == Op.Sum)
        {
			var cur = GetPacket(sr);

			if (cur != -1)
			{
				sum += cur;
			}
		}
		else if (op == Op.Prod)
        {
			var cur = GetPacket(sr);

			if (cur != -1)
            {
				sum *= cur;
            }
		}
        else if (op == Op.Min)
        {
			var cur = GetPacket(sr);

			if (cur < sum && cur != -1)
            {
				sum = cur;
            }
        }
        else if (op == Op.Max)
        {
			var cur = GetPacket(sr);

			if (cur > sum && cur != -1)
			{
				sum = cur;
			}
		}
		else if (op == Op.Gt)
        {
			var f = GetPacket(sr);
			var s = GetPacket(sr);

			return (f > s) ? 1 : 0;
        }
		else if (op == Op.Lt)
		{
			var f = GetPacket(sr);
			var s = GetPacket(sr);

			return (f < s) ? 1 : 0;
		}
		else if (op == Op.Eq)
		{
			var f = GetPacket(sr);
			var s = GetPacket(sr);

			return (f == s) ? 1 : 0;
		}

		idx++;
	}

	return sum;
}

long GetPacket(Reader sr)
{
	if (!sr.CanRead(3))
    {
		return -1;
    }

	var ver = sr.Read(3);

	if (!sr.CanRead(3))
	{
		return -1;
	}

	var pType = sr.Read(3);

	if (pType == 4)
	{
		var isEnd = false;
		var sb = new StringBuilder();

		do
		{
			isEnd = sr.Read(1) == 0;
			sb.Append(sr.ReadRaw(4));

		} while (!isEnd);

		return Convert.ToInt64(sb.ToString(), 2);
	}
	
	var lType = sr.Read(1);
	var len = lType == 0 ? 15 : 11;
	var num = sr.Read(len);

	if (lType == 0)
	{
		var newSr = new Reader(sr.ReadRaw(num));

		return Go(newSr, (Op)pType);
	}
	else
	{
		return Go(sr, (Op)pType, num);
	}
	
}

enum Op
{
	Sum = 0,
	Prod = 1,
	Min = 2,
	Max = 3,
	Gt = 5,
	Lt = 6,
	Eq = 7
}

class Reader
{
	private char[] str;
	private int i;

	public bool IsEnd => i >= str.Length;

    public Reader(string s)
    {
		str = s.ToCharArray();
		i = 0;
    }

	public bool CanRead(int l) => i + l <= str.Length;

	public int Read(int l)
    {
		var segment = new ArraySegment<char>(str, i, l);

		i += l;

		return Convert.ToInt32(new string(segment), 2);
	}

	public string ReadRaw(int l)
    {
		var segment = new ArraySegment<char>(str, i, l);

		i += l;

		return new string(segment);
	}
}
