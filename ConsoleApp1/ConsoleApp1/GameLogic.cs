using System.Security.Cryptography;

class GameLogic
{

    private readonly string[] moves;
    private readonly CryptoManager cryptoManager = new CryptoManager();
    public byte[]? Key { get; private set; }
    public string? HMAC { get; private set; }
    public int ComputerMove { get; private set; }

    public string[] Moves => moves;
    public GameLogic(string[] moves)
    {
        this.moves = moves;
        InitializeGame();
    }

    private void InitializeGame()
    {
        Key = cryptoManager.GenerateKey();
        ComputerMove = GenerateMove();
        HMAC = BitConverter.ToString(cryptoManager.ComputeHmac(Key, moves[ComputerMove])).Replace("-", "");
    }

    private int GenerateMove()
    {
        return RandomNumberGenerator.GetInt32(moves.Length);
    }

    public static bool ValidateArgs(string[] args)
    {
        return args.Length >= 3 && args.Length % 2 != 0 && new HashSet<string>(args).Count == args.Length;
    }

    public string GetMove(int index)
    {
        return moves[index];
    }

    public int MovesCount => moves.Length;
}
