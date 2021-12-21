// See https://aka.ms/new-console-template for more information
// target area: x=111..161, y=-154..-101

StarOne();
StarTwo();

void StarOne()
{
	Console.WriteLine((153 + 1) * 153 / 2);
}

void StarTwo()
{
    var area = (x1: 111, x2: 161, y1: -101 , y2: -154);
    var res = 0;
    
    for (var h = area.x2 + 1; h >= 1; h--)
    {
        for (var v = area.y2; v <= area.y2 * -2 + 1; v++)
        {
            var path = GetSpeeds(h, 0)
                .Zip(GetSpeeds(v, null))
                .TakeWhile(x => (x.First.dx > 0 || x.First.x >= area.x1) && x.First.x <= area.x2 && x.Second.x >= area.y2);

            if (path.Any(x => x.First.x >= area.x1 && x.Second.x <= area.y1))
            {
                res++;
            }
        }
    }

    Console.WriteLine(res);
}

IEnumerable<(int x, int dx)> GetSpeeds(int dx, int? minAcc)
{
    int x = 0;

    while (true)
    {
        x += dx;
        dx -= dx == minAcc ? 0 : 1;

        yield return (x, dx);
    }
}
