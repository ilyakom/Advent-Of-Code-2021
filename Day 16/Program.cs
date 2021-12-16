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

int Go(Reader sr)
{
	var sum = 0;

	while (!sr.IsEnd)
	{
		sum += GetPacket(sr);
	}

	return sum;
}

int GetPacket(Reader sr)
{
	if (!sr.CanRead(3))
    {
		return 0;
    }

	var ver = sr.Read(3);

	var sum = ver;

	if (!sr.CanRead(3))
	{
		return ver;
	}

	var pType = sr.Read(3);

	Console.WriteLine((pType == 4 ? "Literal: " : "Operator: ") + ver);

	if (pType == 4)
	{
		var isEnd = false;
		var sb = new StringBuilder();

		do
		{
			isEnd = sr.Read(1) == 0;
			sb.Append(sr.ReadRaw(4));

		} while (!isEnd);

		var val = Convert.ToInt64(sb.ToString(), 2);

		return sum;
	}
	else
	{
		var lType = sr.Read(1);
		var len = lType == 0 ? 15 : 11;
		var num = sr.Read(len);

		if (lType == 0)
		{
			var newSr = new Reader(sr.ReadRaw(num));

			return Go(newSr) + sum;
		}
		else
		{
            for (int i = 0; i < num; i++)
            {
				sum += GetPacket(sr);
            }

			return sum;
		}
	}
}

void StarTwo()
{

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
