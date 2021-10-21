namespace ConsoleSnakeGame
{
    public class Object
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public char Representation { get; set; }
        public Object(int xPosition, int yPosition, char representation)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Representation = representation;
        }
    }
}
