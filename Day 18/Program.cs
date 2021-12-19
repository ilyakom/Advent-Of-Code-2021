// See https://aka.ms/new-console-template for more information

StarOne();
StarTwo();

void StarTwo()
{
    using (var reader = File.OpenRead("./input.txt"))
    {
        using (var s = new StreamReader(reader))
        {
            var raw = new List<char[]>();

            while (!s.EndOfStream)
            {
                var line = s.ReadLine().Trim();
                raw.Add(line.ToCharArray());
            }

            int max = 0;

            for (int i = 0; i < raw.Count; i++)
            {
                for (int j = 0; j < raw.Count; j++)
                {
                    if (i == j) continue;

                    var curLeft = new Node();
                    var idx = 1;
                    Build(raw[i], ref idx, curLeft);

                    var curRight = new Node();
                    idx = 1;
                    Build(raw[j], ref idx, curRight);

                    curLeft = Add(curLeft, curRight);

                    while (true)
                    {
                        bool isExploded = false;

                        Explode(curLeft, 0, ref isExploded);

                        if (isExploded)
                        {
                            continue;
                        }

                        if (Split(curLeft))
                        {
                            continue;
                        }

                        break;
                    }

                    var mag = CalcMagnitude(curLeft);

                    if (mag > max)
                    {
                        max = mag;
                    }
                }
            }

            Console.WriteLine("Result: " + max);
        }
    }
}

void StarOne()
{
	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
            var q = new Queue<Node>();

            while (!s.EndOfStream)
            {
                var line = s.ReadLine().Trim();

                var head = new Node();
                var idx = 1;

                Build(line.ToCharArray(), ref idx, head);

                q.Enqueue(head);
			}

            var res = q.Dequeue();

            while(q.Count > 0)
            {
                res = Add(res, q.Dequeue());

                while(true)
                {
                    bool isExploded = false;

                    Explode(res, 0, ref isExploded);

                    if (isExploded)
                    {
                        continue;
                    }

                    if (Split(res))
                    {
                        continue;
                    }

                    break;
                }

               
            }

            Print(res);

            Console.WriteLine("Result: " + CalcMagnitude(res));
		}
	}
}

void Build(char[] str, ref int idx, Node cur)
{
    if (idx >= str.Length)
    {
        return;
    }

    cur.Left = new Node { Parent = cur };
    if (char.IsDigit(str[idx]))
    {
        cur.Left.Val = str[idx] - '0';
        idx += 2;
    }
    else
    {
        idx++;
        Build(str, ref idx, cur.Left);
        idx++;
    }

    cur.Right = new Node { Parent = cur };
    if (char.IsDigit(str[idx]))
    {
        cur.Right.Val = str[idx] - '0';
        idx += 2;
    }
    else
    {
        idx++;
        Build(str, ref idx, cur.Right);
        idx++;
    }
}

Node Add(Node node1, Node node2)
{
    var res = new Node() { Left = node1, Right = node2 };

    node1.Parent = res;
    node2.Parent = res;

    return res;
}

bool Split(Node node)
{
    if (node.Val > 9)
    {
        node.Left = new Node { 
            Val = node.Val / 2, 
            Parent = node
        };

        node.Right = new Node { 
            Val = node.Val % 2 > 0 ? (node.Val / 2 + 1) : (node.Val / 2),
            Parent = node
        };

        node.Val = 0;

        return true;
    }

    if (node.Left == null || node.Right == null)
    {
        return false;
    }

    return Split(node.Left) || Split(node.Right);
}

(int, int) Explode(Node node, int lvl, ref bool isExploded)
{
    if (!node.Left.IsLeaf)
    {
        var val = Explode(node.Left, lvl+1, ref isExploded);

        if (val.Item2 != 0)
        {
            AddToFirstLeft(val.Item2, node.Right);
        }

        if (val.Item1 != 0)
        {
            return (val.Item1, 0);
        }

        if (isExploded)
        {
            return (0, 0);
        }
    }

    if (!node.Right.IsLeaf)
    {
        var val = Explode(node.Right, lvl + 1, ref isExploded);

        if (val.Item1 != 0)
        {
            AddToFirstRight(val.Item1, node.Left);
        }

        if (val.Item2 != 0)
        {
            return (0, val.Item2);
        }

        if (isExploded)
        {
            return (0, 0);
        }
    }

    if (lvl >= 4)
    {
        var ret = (node.Left.Val, node.Right.Val);

        node.Left = null;
        node.Right = null;
        node.Val = 0;

        isExploded = true;
        return ret;
    }

    return (0, 0);
}

void AddToFirstLeft(int val, Node node)
{
    if (node.IsLeaf)
    {
        node.Val += val;
        return;
    }

    AddToFirstLeft(val, node.Left);
}

void AddToFirstRight(int val, Node node)
{
    if (node.IsLeaf)
    {
        node.Val += val;
        return;
    }

    AddToFirstRight(val, node.Right);
}

int CalcMagnitude(Node node)
{
    if (node.IsLeaf)
    {
        return node.Val;
    }

    return 3 * CalcMagnitude(node.Left) + 2 * CalcMagnitude(node.Right);
}

void Print(Node node)
{
    if(node.IsLeaf)
    {
        Console.Write(node.Val);
        return;
    }

    Console.Write("[");
    Print(node.Left);
    Console.Write(",");
    Print(node.Right);
    Console.Write("]");
}
class Node
{
    public int Val { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    public Node Parent { get; set; }

    public bool IsLeaf => Left == null && Right == null;
}
