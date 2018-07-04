using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1
{
    class Menu
    {
        public delegate void MenuMethod();
        private List<MenuMethod> Methods;
        private List<string> MenuNames { get; set; }
        private int currentItem;
        private ConsoleColor ItemsColor;
        private ConsoleColor CurrentColor;
        public Menu()
        {
            MenuNames = new List<string>
            {
                "Comments under users post",
                "Comments under post, where body more than 50",
                "Completed user`s ToDos",
                "Users with ToDos (Users sorted ascending, ToDos Descending",
               " User info",//User, Last post, Comments count, Not completed ToDos, Post with most likes and characters >80, Post with most likes
                "Post info"//Post, Largest comment, Most popular comment, count comments with 0 likes or text >80
            };
            currentItem = 0;
            ItemsColor = ConsoleColor.White;
            CurrentColor = ConsoleColor.Blue;
        }

        public void Input(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    if (SelectedItem != -1)
                        MoveDown();
                    break;
                case ConsoleKey.UpArrow:
                    if (SelectedItem != -1)
                        MoveUp();
                    break;
                case ConsoleKey.Enter:
                    Console.ResetColor();
                    if (SelectedItem >= 0 && SelectedItem < Methods.Count)
                    {
                        Methods[SelectedItem]();
                        SelectedItem = -1;
                    }
                    break;
                case ConsoleKey.Backspace:
                    if (SelectedItem == -1)
                        SelectedItem = 0;
                    BackSpaceMethod();
                    return;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public void CommentsUnderPost()
        {

        }

        public void CommentUnderPostBodyMoreThen50()
        {

        }

        public void CompletedTodosByUser()
        {

        }

        public void UsersACSWithTodosDSC()
        {

        }

        public 
    }
}
