// See https://aka.ms/new-console-template for more information

Stars();

void Stars()
{
    using (var reader = File.OpenRead("./input.txt"))
    {
        using (var s = new StreamReader(reader))
        {
            List<List<int>> image = new List<List<int>>();
            var extendSizeBy = 100;
            var steps = 50;

            var fixer = s.ReadLine().Select(x => x == '.' ? 0 : 1).ToArray();
            s.ReadLine();

            var line = s.ReadLine().ToCharArray().Select(x => x == '.' ? 0 : 1).ToList();

            for (int i = 0; i < extendSizeBy; i++)
            {
                image.Add(Enumerable.Repeat(0, line.Count + extendSizeBy * 2).ToList());
            }

            var lst = Enumerable.Repeat(0, extendSizeBy).ToList();
            lst.AddRange(line);
            lst.AddRange(Enumerable.Repeat(0, extendSizeBy).ToList());

            image.Add(lst);

            while (!s.EndOfStream)
            {
                line = s.ReadLine().ToCharArray().Select(x => x == '.' ? 0 : 1).ToList();

                lst = Enumerable.Repeat(0, extendSizeBy).ToList();
                lst.AddRange(line);
                lst.AddRange(Enumerable.Repeat(0, extendSizeBy).ToList());

                image.Add(lst);
            }

            for (int i = 0; i < extendSizeBy; i++)
            {
                image.Add(Enumerable.Repeat(0, line.Count + extendSizeBy * 2).ToList());
            }

            var stepImage = new List<List<int>>(image.Count);
            for (int i = 0; i < image.Count; i++)
            {
                stepImage.Add(Enumerable.Repeat(0, image[0].Count).ToList());
            }

            //Draw(image);

            for (int k = 0; k < steps; k++)
            {
                for (int i = 0; i < image.Count; i++)
                {
                    for (int j = 0; j < image[0].Count; j++)
                    {
                        int window = 0;

                        // top line
                        if (i > 0 && j > 0)
                        {
                            window |= image[i - 1][j - 1];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        if (i > 0)
                        {
                            window |= image[i - 1][j];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        if (i > 0 && j + 1 < image[0].Count)
                        {
                            window |= image[i - 1][j + 1];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        // mid line
                        if (j > 0)
                        {
                            window |= image[i][j - 1];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        window |= image[i][j];
                        window <<= 1;

                        if (j + 1 < image[0].Count)
                        {
                            window |= image[i][j + 1];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        // bottom line
                        if (i + 1 < image.Count && j > 0)
                        {
                            window |= image[i + 1][j - 1];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        if (i + 1 < image.Count)
                        {
                            window |= image[i + 1][j];
                        }
                        else
                        {
                            window |= image[0][0];
                        }
                        window <<= 1;

                        if (i + 1 < image.Count && j + 1 < image[0].Count)
                        {
                            window |= image[i + 1][j + 1];
                        }
                        else
                        {
                            window |= image[0][0];
                        }

                        stepImage[i][j] = fixer[window];
                    }
                }

                var swap = stepImage;
                stepImage = image;
                image = swap;

                //Draw(image);
            }

            //Draw(image);
            Console.WriteLine(image.Sum(x => x.Sum()));
        }
    }
}

void Draw(List<List<int>> pic)
{
    for (int i = 0; i < pic.Count; i++)
    {
        for (int j = 0; j < pic[0].Count; j++)
        {
            Console.Write(pic[i][j] == 0 ? '.' : "#");
        }
        Console.WriteLine();
    }

    Console.WriteLine();
}
