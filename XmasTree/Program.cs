namespace XmasTree;
class Program
{

    static void Main(string[] args)
    {
        Forms.Init();
        for (var i = 0; i < args.Length; i++)
        {
            var a = args[i];
            switch (args[i])
            {
                case "-p" when i + 1 < args.Length:
                case "--port" when i + 1 < args.Length:
                    {
                        int p;
                        if (int.TryParse(args[i + 1], out p))
                        {
                            UI.Port = p;
                        }
                        i++;
                    }
                    break;
            }
        }

        new Tree().Publish();

        UI.Present("/tree");

        Console.ReadLine();
    }
}
