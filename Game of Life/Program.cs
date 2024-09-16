namespace Game_of_Life;

class Program
{
    static void Main(string[] args)
    {
        var game = new GameOfLife();
        
        game.AddRuleset(new Ruleset(
            (c, x, y) =>
            {
                var neighbors = new[] {
                    c.Check(x - 1, y - 1), c.Check(x, y - 1), c.Check(x + 1, y - 1),
                    c.Check(x - 1, y),                        c.Check(x + 1, y),
                    c.Check(x - 1, y + 1), c.Check(x, y + 1), c.Check(x + 1, y + 1),
                }.ToList().FindAll((n) => n).ToArray();

                return c.Check(x, y) && neighbors.Length < 2;
            },
            (c, x, y) => c.Depopulate(x, y)
        ));
        
        game.AddRuleset(new Ruleset(
            (c, x, y) =>
            {
                var neighbors = new[] {
                    c.Check(x - 1, y - 1), c.Check(x, y - 1), c.Check(x + 1, y - 1),
                    c.Check(x - 1, y),                        c.Check(x + 1, y),
                    c.Check(x - 1, y + 1), c.Check(x, y + 1), c.Check(x + 1, y + 1),
                }.ToList().FindAll((n) => n).ToArray();

                return c.Check(x, y) && neighbors.Length is 2 or 3;
            },
            (c, x, y) => c.Populate(x, y)
        ));
        
        game.AddRuleset(new Ruleset(
            (c, x, y) =>
            {
                var neighbors = new[] {
                    c.Check(x - 1, y - 1), c.Check(x, y - 1), c.Check(x + 1, y - 1),
                    c.Check(x - 1, y),                        c.Check(x + 1, y),
                    c.Check(x - 1, y + 1), c.Check(x, y + 1), c.Check(x + 1, y + 1),
                }.ToList().FindAll((n) => n).ToArray();

                return c.Check(x, y) && neighbors.Length > 3;
            },
            (c, x, y) => c.Depopulate(x, y)
        ));
        
        game.AddRuleset(new Ruleset(
            (c, x, y) =>
            {
                var neighbors = new[] {
                    c.Check(x - 1, y - 1), c.Check(x, y - 1), c.Check(x + 1, y - 1),
                    c.Check(x - 1, y),                        c.Check(x + 1, y),
                    c.Check(x - 1, y + 1), c.Check(x, y + 1), c.Check(x + 1, y + 1),
                }.ToList().FindAll((n) => n).ToArray();

                return !c.Check(x, y) && neighbors.Length == 3;
            },
            (c, x, y) => c.Populate(x, y)
        ));
        
        game.Initialize();
        game.Start();

    }
}