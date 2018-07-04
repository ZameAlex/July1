using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1
{
    public sealed class MenuClass
    {
        public delegate void MenuMethod(int id);
        public delegate int EnterId();

        private List<MenuMethod> Methods;
        private List<string> DisplayItems;
        private EnterId GetID;

        public ConsoleColor ItemColor { get; private set; }

        public ConsoleColor SelectionColor { get; private set; }

        public int SelectedItem { get; private set; }

        public MenuClass(EnterId enterID,params MenuMethod[] methods)
        {
            GetID = enterID;
            Methods = new List<MenuMethod>();
            Methods.AddRange(methods);
            ItemColor = ConsoleColor.White;
            SelectionColor = ConsoleColor.Blue;
        }

        public void SetDisplayNames(string[] names)
        {
            DisplayItems = names.ToList();
        }

        private int top;
        private int currentTop;

        public void Show()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press Esc to exit");
            Console.WriteLine("Press Backspace to return back");
            Console.WriteLine("Warning, if it is first menu, program will closed");
            Console.ResetColor();
            currentTop = top = Console.CursorTop;
            for (int i = 0; i < DisplayItems.Count; i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = SelectionColor;
                }
                else {
                    Console.ResetColor();
                    Console.ForegroundColor = ItemColor;
                }
                Console.WriteLine(DisplayItems[i]);
            }
            Console.ResetColor();
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
                        if (SelectedItem == 3)
                            Methods[SelectedItem](-1);
                        else
                            Methods[SelectedItem](GetID());
                        SelectedItem = -1;
                    }
                    break;
                case ConsoleKey.Backspace:
                    if (SelectedItem == -1)
                        SelectedItem = 0;
                    Show();
                    return;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private void MoveDown()
        {
            Console.ForegroundColor = ItemColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(DisplayItems[SelectedItem]);
            SelectedItem = SelectedItem == Methods.Count - 1 ? 0 : SelectedItem + 1;
            if (SelectedItem == 0)
                currentTop = top;
            else
                currentTop++;
            Console.ForegroundColor = SelectionColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(DisplayItems[SelectedItem]);
            Console.SetCursorPosition(0, currentTop);
        }

        private void MoveUp()
        {
            Console.ForegroundColor = ItemColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(DisplayItems[SelectedItem]);
            SelectedItem = SelectedItem == 0 ? Methods.Count - 1 : SelectedItem - 1;
            if (Methods.Count - 1 == SelectedItem)
                currentTop = top + Methods.Count - 1;
            else
                currentTop--;
            Console.ForegroundColor = SelectionColor;
            Console.SetCursorPosition(0, currentTop);
            Console.WriteLine(DisplayItems[SelectedItem]);
            Console.SetCursorPosition(0, currentTop);

        }
    }
}
