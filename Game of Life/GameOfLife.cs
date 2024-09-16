namespace Game_of_Life;

public class GameOfLife
{
    private List<Ruleset> rulesets = [];

    public void AddRuleset(Ruleset ruleset)
    {
        rulesets.Add(ruleset);
    }

    void Update()
    {
        List <Ruleset> appliedRules = [];
        List <int> appliedX = [];
        List <int> appliedY = [];
        foreach (var rule in rulesets)
        {
            for (var y = 0; y < state.Length; y++)
            {
                for (var x = 0; x < state[y].Length; x++)
                {
                    if (!rule.Condition(Controller!, x, y)) continue;
                    appliedRules.Add(rule);
                    appliedX.Add(x);
                    appliedY.Add(y);
                }
            }
        }

        for (var i = 0; i < appliedRules.Count; i++)
        {
            var rule = appliedRules[i];
            var x = appliedX[i];
            var y = appliedY[i];
            rule.Action(Controller!, x, y);
        }
    }
    
    // int screenWidth = Console.WindowWidth / 2;
    // int screenHeight = Console.WindowHeight;
    
    private static int xWidth = 2;
    
    public int screenWidth = Console.WindowWidth / xWidth - 1;
    public int screenHeight = Console.WindowHeight - 1;

    public bool[][] state;
    private bool[][]? lastState;

    public Controller? Controller { get; private set; }

    public void Initialize()
    {
        Controller = new Controller(this);
        
        // Initial state
        state = new bool[screenHeight][];
        for (var i = 0; i < state.Length; i++)
        {
            state[i] = new bool[screenWidth];
            for (var j = 0; j < state[i].Length; j++)
            {
                state[i][j] = false; // Dead
            }
        }
        
        var r = new Random();
        for (var y = 0; y < state.Length; y++)
        {
            for (var x = 0; x < state[y].Length; x++)
            {
                if (r.Next(0, 100) < 50) Controller!.Populate(x, y);
            }
        }
    }
    public void Start()
    {
        if (Controller == null)
        {
            Initialize();
        }
        
        while (true)
        {
            lastState = new bool[screenHeight][];
            // Copy the state
            for (var i = 0; i < state.Length; i++)
            {
                lastState[i] = new bool[screenWidth];
                for (var j = 0; j < lastState[i].Length; j++) lastState[i][j] = state[i][j];
            }
            Update();
            Render();
            Thread.Sleep(100);
        }
    }
    
    void Render()
    {
        var ogColor = Console.BackgroundColor;
        for (var y = 0; y < state.Length; y++)
        {
            var yCol = state[y];
            for (var x = 0; x < yCol.Length; x++)
            {
                var xCell = yCol[x];

                if (xCell == lastState![y][x]) continue;
                Console.SetCursorPosition(x * xWidth, y);
                    
                Console.BackgroundColor = xCell ? ConsoleColor.White : ogColor;

                Console.WriteLine("".PadRight(xWidth, ' '));
            }
        }

        Console.BackgroundColor = ogColor;
    }
}

public class Controller(GameOfLife game)
{
    private GameOfLife game = game;

    public bool Check(int x, int y)
    {
        if (x < 0 || y < 0 || x >= game.screenWidth || y >= game.screenHeight) return false;
        
        return game.state[y][x];
    }
    
    public void Populate(int x, int y) // Always returns true
    {
        if (x < 0 || y < 0 || x >= game.screenWidth || y >= game.screenHeight) return;
        game.state[y][x] = true;
    }

    public void Depopulate(int x, int y) // Always returns false 
    {
        if (x < 0 || y < 0 || x >= game.screenWidth || y >= game.screenHeight) return;        
        game.state[y][x] = false;
    }
}

public class Ruleset(Func<Controller, int, int, bool> condition, Action<Controller, int, int> action)
{   
    public readonly Func<Controller, int, int, bool> Condition = condition;
    public readonly Action<Controller, int, int> Action = action;
}