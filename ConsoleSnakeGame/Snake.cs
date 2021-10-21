using System.Collections.Generic;

namespace ConsoleSnakeGame
{
    public class Snake : Object
    {
        public List<Coordinates> body;
        public int Length { get; set; }
        public Snake(int xPosition, int yPosition) : base(xPosition, yPosition, '#')
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Length = 1;
            body = new List<Coordinates>();
            body.Add(new Coordinates(xPosition, yPosition));
        }
        public Snake(int xPosition, int yPosition, char representation) : base(xPosition, yPosition, representation)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Representation = representation;
            Length = 1;
            body = new List<Coordinates>();
            body.Add(new Coordinates(xPosition, yPosition));
        }
    }
}