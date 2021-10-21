using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleSnakeGame
{
    //TODO: make system for tracking high score
    public class SnakeGame
    {
        private const double speedGain = 1.05;
        private const short fontSize = 22;
        private const string font = "Roboto";
        private  int winWidth;
        private int winHeight;
        private double speed;
        private Snake snake;
        private Apple apple;
        private ConsoleKeyInfo input;
        private Random random = new Random();

        public SnakeGame(int winWidth, int winHeight, double speed)
        {
            ConsoleHelper.SetCurrentFont(font, fontSize);
            this.winWidth = winWidth;
            this.winHeight = winHeight;
            this.speed = speed;
            snake = new Snake(winWidth/2, winHeight/2);
            apple = new Apple(random.Next(0, winWidth), random.Next(0, winHeight));
        }
        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetWindowSize(winWidth+2, winHeight+2);
            Console.CursorVisible = false;
            StringBuilder builder = new StringBuilder();
            Console.Clear();
            int xStep = 1;
            int yStep = 0;
            for (int i = 0; i < winHeight; i++)
                builder.AppendLine(new string('*', winWidth));
            while (true)
            {
                snake.XPosition += xStep;
                snake.YPosition += yStep;

                
                //checking if snake is inside bounds
                if (snake.XPosition >= winWidth)
                    snake.XPosition = 0;
                if (snake.YPosition >= winHeight)
                    snake.YPosition = 0;
                if (snake.XPosition < 0)
                    snake.XPosition = winWidth-1;
                if (snake.YPosition < 0)
                    snake.YPosition = winHeight-1;

                //printing terrain
                Console.SetCursorPosition(0, 0);
                ConsoleHelper.PrintElementInColor(builder.ToString(), ConsoleColor.Gray);

                //Spawn first apple
                Console.SetCursorPosition(apple.XPosition, apple.YPosition);
                ConsoleHelper.PrintElementInColor(apple.Representation, ConsoleColor.Red);

                //eating apple and random respawn
                if (snake.XPosition == apple.XPosition && snake.YPosition == apple.YPosition)
                {
                    apple.SetPosition(random.Next(0, winWidth), random.Next(0, winHeight));
                    snake.Length++;
                    snake.body.Add(new Coordinates(0, 0));
                    speed *= speedGain;
                }
                //tracking snake coordinates and updating them
                for (int i = snake.Length-1; i >= 0; i--)
                {
                    if (i > 0 && snake.Length > 1)
                        snake.body[i] = snake.body[i - 1];
                    if (i == 0)
                        snake.body[0] = new Coordinates(snake.XPosition, snake.YPosition);
                }
                //drawing the snake
                for (int i = 0; i < snake.Length; i++)
                {
                    Console.SetCursorPosition(snake.body[i].XPosition, snake.body[i].YPosition);
                    ConsoleHelper.PrintElementInColor(snake.Representation, ConsoleColor.Green);
                }
                //if head collides with body, game over

                for (int i = 4; i < snake.Length; i++) //first 3 body parts will never collide with body
                {
                    if (snake.body[0].XPosition == snake.body[i].XPosition && snake.body[0].YPosition == snake.body[i].YPosition)
                        OnGameOver(snake.Length);
                }

                //printing score
                Console.SetCursorPosition(winWidth, winHeight);
                ConsoleHelper.PrintElementInColor($"\nScore: {snake.Length}", ConsoleColor.Black);

                //waiting for input in new thread
                Thread thread = new Thread(new ThreadStart(InputKey));
                thread.Start();
                if (input.Key == ConsoleKey.UpArrow && yStep != 1)
                {
                    xStep = 0;
                    yStep = -1;
                }
                if (input.Key == ConsoleKey.DownArrow && yStep != -1)
                {
                    xStep = 0;
                    yStep = 1;
                }
                if (input.Key == ConsoleKey.LeftArrow && xStep != 1)
                {
                    xStep = -1;
                    yStep = 0;
                }
                if (input.Key == ConsoleKey.RightArrow && xStep != -1)
                {
                    xStep = 1;
                    yStep = 0;
                }
                if (input.Key == ConsoleKey.Escape)
                {
                    //todo: on esc make pause and resume button
                    Console.Clear();
                    Environment.Exit(0);
                }
                Thread.Sleep((int)((1 / speed) * 200));
            }
        }

        private void OnGameOver(int score)
        {
            ConsoleHelper.SetCurrentFont(font, 50);
            Console.SetWindowSize(30, 10);
            Console.Clear();
            ConsoleHelper.PrintElementInColor($"    GAME OVER!\n    Your score is: {score}", ConsoleColor.Black);
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(0);
        }

        private void InputKey()
        {
            input = Console.ReadKey(true);
        }
    }
}