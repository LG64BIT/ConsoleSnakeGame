namespace ConsoleSnakeGame
{
    public class Apple : Object
    {
        public Apple(int xPosition, int yPosition) : base(xPosition, yPosition, 'O')
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
        public Apple(int xPosition, int yPosition, char representation) : base(xPosition, yPosition, representation)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Representation = representation;
        }
        public void SetPosition(int xPosition, int yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}
