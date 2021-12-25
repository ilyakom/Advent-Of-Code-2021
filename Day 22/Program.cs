// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Star();

void Star()
{
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();

    var cubes = Read();
    Process(cubes);

    stopWatch.Stop();

    Console.WriteLine("TimeElapsed: " + stopWatch.ElapsedMilliseconds);
}

List<Cube> Read()
{
    using var reader = File.OpenRead("./input.txt");
    using var s = new StreamReader(reader);
    var cubes = new List<Cube>();

    while (!s.EndOfStream)
    {
        var cube = new Cube();

        var line = s.ReadLine().Split(" ");
        cube.Mode = line[0] == "on" ? Mode.On : Mode.Off;

        var coords = line[1].Split(",");

        foreach (var crd in coords)
        {
            var tmp = crd.Split(new[] { '=', '.' }, StringSplitOptions.RemoveEmptyEntries);

            switch (tmp[0])
            {
                case "x":
                    cube.X = (Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2]));
                    break;
                case "y":
                    cube.Y = (Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2]));
                    break;
                case "z":
                    cube.Z = (Convert.ToInt32(tmp[1]), Convert.ToInt32(tmp[2]));
                    break;
                default:
                    break;
            }
        }

        cubes.Add(cube);
    }

    return cubes;
}

long Process(List<Cube> cubes)
{
    var resultCubes = new List<Cube>();

    foreach (var cube in cubes)
    {
        AddCube(resultCubes, cube);
    }

    var stepCnt = resultCubes.Sum(x =>
        (Math.Abs(x.X.Item2 - x.X.Item1) + 1) *
        (Math.Abs(x.Y.Item2 - x.Y.Item1) + 1) *
        (Math.Abs(x.Z.Item2 - x.Z.Item1) + 1));

    Console.WriteLine(stepCnt);

    return stepCnt;
}

void AddCube(List<Cube> cubes, Cube cube)
{   
    for (int i = 0; i < cubes.Count; i++)
    {
        if (cubes[i].Intersecs(cube))
        {
            var nc = cubes[i].Substract(cube);
            cubes.RemoveAt(i);
            i--;
            cubes.AddRange(nc);
        }
    }

    if (cube.Mode == Mode.On)
    {
        cubes.Add(cube);
    }
}

enum Mode
{
    On,
    Off
}

class Cube
{
    public Mode Mode { get; set; }
    public (long, long) X { get; set; }
    public (long, long) Y { get; set; }
    public (long, long) Z { get; set; }

    public bool Intersecs(Cube cube)
    {
        var intersByX = 
            cube.X.Item1 >= X.Item1 && cube.X.Item1 <= X.Item2 || 
            cube.X.Item2 >= X.Item1 && cube.X.Item2 <= X.Item2 ||
            cube.X.Item1 <= X.Item1 && cube.X.Item2 >= X.Item2;

        var intersByY = 
            cube.Y.Item1 >= Y.Item1 && cube.Y.Item1 <= Y.Item2 || 
            cube.Y.Item2 >= Y.Item1 && cube.Y.Item2 <= Y.Item2 ||
            cube.Y.Item1 <= Y.Item1 && cube.Y.Item2 >= Y.Item2;

        var intersByZ = 
            cube.Z.Item1 >= Z.Item1 && cube.Z.Item1 <= Z.Item2 || 
            cube.Z.Item2 >= Z.Item1 && cube.Z.Item2 <= Z.Item2 ||
            cube.Z.Item1 <= Z.Item1 && cube.Z.Item2 >= Z.Item2;

        return intersByX && intersByY && intersByZ;
    }

    public bool Contains(Cube cube)
    {
        var intersByX = cube.X.Item1 >= X.Item1 && cube.X.Item2 <= X.Item2;
        var intersByY = cube.Y.Item1 >= Y.Item1 && cube.Y.Item2 <= Y.Item2;
        var intersByZ = cube.Z.Item1 >= Z.Item1 && cube.Z.Item2 <= Z.Item2;

        return intersByX && intersByY && intersByZ;
    }

    public List<Cube> Substract(Cube cube)
    {
        var newCubes = new List<Cube>();
        var queue = new Queue<Cube>();
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            var c = queue.Dequeue();

            if (cube.Contains(c))
            {
                continue;
            }

            if (!c.Intersecs(cube))
            {
                newCubes.Add(c);
                continue;
            }

            var cbs = c.SpliByX(cube);

            if (AddAny(queue, cbs))
            {
                continue;
            }

            cbs = c.SpliByY(cube);

            if (AddAny(queue, cbs))
            {
                continue;
            }

            cbs = c.SpliByZ(cube);

            if (AddAny(queue, cbs))
            {
                continue;
            }

            newCubes.Add(c);
        }

        return newCubes;
    }

    private (Cube, Cube) SpliByX(Cube cube)
    {
        if (cube.X.Item1 > X.Item1 && cube.X.Item1 <= X.Item2)
        {
            var lowCube = new Cube();

            lowCube.X = (X.Item1, cube.X.Item1-1);
            lowCube.Y = (Y.Item1, Y.Item2);
            lowCube.Z = (Z.Item1, Z.Item2);

            var highCube = new Cube();

            highCube.X = (cube.X.Item1, X.Item2);
            highCube.Y = (Y.Item1, Y.Item2);
            highCube.Z = (Z.Item1, Z.Item2);
            
            return (lowCube, highCube);
        }

        if (cube.X.Item2 >= X.Item1 && cube.X.Item2 < X.Item2)
        {
            var lowCube = new Cube();

            lowCube.X = (X.Item1, cube.X.Item2);
            lowCube.Y = (Y.Item1, Y.Item2);
            lowCube.Z = (Z.Item1, Z.Item2);

            var highCube = new Cube();

            highCube.X = (cube.X.Item2+1, X.Item2);
            highCube.Y = (Y.Item1, Y.Item2);
            highCube.Z = (Z.Item1, Z.Item2);

            return (lowCube, highCube);
        }

        return (null, null);
    }

    private (Cube, Cube) SpliByY(Cube cube)
    {
        if (cube.Y.Item1 > Y.Item1 && cube.Y.Item1 <= Y.Item2)
        {
            var lowCube = new Cube();

            lowCube.Y = (Y.Item1, cube.Y.Item1 - 1);
            lowCube.X = (X.Item1, X.Item2);
            lowCube.Z = (Z.Item1, Z.Item2);

            var highCube = new Cube();

            highCube.Y = (cube.Y.Item1, Y.Item2);
            highCube.X = (X.Item1, X.Item2);
            highCube.Z = (Z.Item1, Z.Item2);
           

            return (lowCube, highCube);
        }

        if (cube.Y.Item2 >= Y.Item1 && cube.Y.Item2 < Y.Item2)
        {
            var lowCube = new Cube();

            lowCube.Y = (Y.Item1, cube.Y.Item2);
            lowCube.X = (X.Item1, X.Item2);
            lowCube.Z = (Z.Item1, Z.Item2);
           

            var highCube = new Cube();

            highCube.Y = (cube.Y.Item2 + 1, Y.Item2);
            highCube.X = (X.Item1, X.Item2);
            highCube.Z = (Z.Item1, Z.Item2);

            return (lowCube, highCube);
        }

        return (null, null);
    }

    private (Cube, Cube) SpliByZ(Cube cube)
    {
        if (cube.Z.Item1 > Z.Item1 && cube.Z.Item1 <= Z.Item2)
        {
            var lowCube = new Cube();

            lowCube.Z = (Z.Item1, cube.Z.Item1 - 1);
            lowCube.Y = (Y.Item1, Y.Item2);
            lowCube.X = (X.Item1, X.Item2);

            var highCube = new Cube();

            highCube.Z = (cube.Z.Item1, Z.Item2);
            highCube.Y = (Y.Item1, Y.Item2);
            highCube.X = (X.Item1, X.Item2);

            return (lowCube, highCube);
        }

        if (cube.Z.Item2 >= Z.Item1 && cube.Z.Item2 < Z.Item2)
        {
            var lowCube = new Cube();

            lowCube.Z = (Z.Item1, cube.Z.Item2);
            lowCube.Y = (Y.Item1, Y.Item2);
            lowCube.X = (X.Item1, X.Item2);

            var highCube = new Cube();

            highCube.Z = (cube.Z.Item2 + 1, Z.Item2);
            highCube.Y = (Y.Item1, Y.Item2);
            highCube.X = (X.Item1, X.Item2);

            return (lowCube, highCube);
        }

        return (null, null);
    }

    private bool AddAny(Queue<Cube> queue, (Cube, Cube) pair)
    {
        var res = false;

        if(pair.Item1 != null)
        {
            queue.Enqueue(pair.Item1);
            res = true;
        }

        if (pair.Item2 != null)
        {
            queue.Enqueue(pair.Item2);
            res = true;
        }

        return res;
    }
}
