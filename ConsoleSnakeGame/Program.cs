namespace ConsoleSnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            SnakeGame game = new SnakeGame(40, 20, 1.5f);
            game.Start();
        }
    }
}
