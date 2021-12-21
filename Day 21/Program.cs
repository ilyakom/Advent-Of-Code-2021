// See https://aka.ms/new-console-template for more information

StarOne();

void StarOne()
{
    var p1 = 4;
    var p2 = 3;

    var wins = GetWins(p1, p2, 0, 0, true);

    Console.WriteLine(Math.Max(wins.Item1, wins.Item2)  + Environment.NewLine + 444356092776315);
}

(long, long) GetWins(int p1, int p2, long ts1, long ts2, bool isFPTurn)
{
    if (ts1 >= 21)
    {
        return (1, 0);
    }

    if (ts2 >= 21)
    {
        return (0, 1);
    }

    long resP1 = 0;
    long resP2 = 0;
    var dic = new Dictionary<int, (long, long)>(5);

    if (isFPTurn)
    {
        for (var i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                for (int k = 1; k <= 3; k++)
                {
                    var sum = i + j + k;

                    if (dic.TryGetValue(sum, out var res))
                    {
                        resP1 += res.Item1;
                        resP2 += res.Item2;
                        continue;
                    }

                    var save = p1;
                    p1 = (p1 + i + j + k) == 10 ? 10 : ((p1 + i + j + k) % 10);
                    ts1 += p1;

                    res = GetWins(p1, p2, ts1, ts2, false);

                    dic.Add(sum, res);

                    resP1 += res.Item1;
                    resP2 += res.Item2;

                    ts1 -= p1;
                    p1 = save;
                }
            }
        }
    }
    else
    {
        for (var i = 1; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                for (int k = 1; k <= 3; k++)
                {
                    var sum = i + j + k;

                    if (dic.TryGetValue(sum, out var res))
                    {
                        resP1 += res.Item1;
                        resP2 += res.Item2;
                        continue;
                    }

                    var save = p2;
                    p2 = (p2 + i + j + k)  == 10 ? 10 : ((p2 + i + j + k) % 10);
                    ts2 += p2;

                    res = GetWins(p1, p2, ts1, ts2, true);

                    dic.Add(sum, res);

                    resP1 += res.Item1;
                    resP2 += res.Item2;

                    ts2 -= p2;
                    p2 = save;
                }
            }
        }
    }
    
    return (resP1, resP2);
}