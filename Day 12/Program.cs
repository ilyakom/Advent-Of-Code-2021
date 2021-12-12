// See https://aka.ms/new-console-template for more information

StarOneTwo();

void StarOneTwo()
{
	var dic = new Dictionary<string, Node>();

	using (var reader = File.OpenRead("./input.txt"))
	{
		using (var s = new StreamReader(reader))
		{
			while (!s.EndOfStream)
			{
				var line = s.ReadLine().Split('-');
				
				if (dic.TryGetValue(line[0], out var node))
                {
					if (!dic.TryGetValue(line[1], out var toNode))
                    {
						toNode = new Node(line[1]);
						dic.Add(line[1], toNode);
					}

					node.Children.Add(toNode);
					toNode.Children.Add(node);
                }
                else
                {
					node = new Node(line[0]);
					dic.Add(line[0], node);

					if (!dic.TryGetValue(line[1], out var toNode))
                    {
						toNode = new Node(line[1]);
						dic.Add(line[1], toNode);
					}

					node.Children.Add(toNode);
					toNode.Children.Add(node);
                }
			}
		}
	}

	var result = 0;

	foreach (var node in dic["start"].Children)
    {
		result += Go(node, false);
		node.Visited = false;
	}

	Console.WriteLine(result);
}

int Go(Node node, bool twiceUsed)
{
	if (node.Name == "end")
    {
		return 1;
    }

	if (node.Name == "start")
    {
		return 0;
    }

	if (node.Small)
	{
		node.Visited = true;
	}

	int res = 0;

	foreach (var child in node.Children)
    {
		if (!child.Visited)
        {
			res += Go(child, twiceUsed);
			child.Visited = false;
		}
        else if (!twiceUsed)
        {
			res += Go(child, true);
		}
    }

	return res;
}

class Node
{
	public string Name;
	public HashSet<Node> Children;
	public bool Visited;
	public bool Small;

    public Node(string name)
    {
		Name = name;
		Small = char.IsLower(name[0]);
		Children = new HashSet<Node>();
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Node nd && this.Name == nd.Name;
    }
}