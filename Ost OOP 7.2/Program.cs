using System;
using System.IO;
using System.Collections;

namespace Ost_OOP_7._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Key.KKEY();
        }
    }
    class List : IComparable
    {
        public string Name;
        public string Roz;
        public int Rozmir;
        public DateTime Data;
        
       
        public string Atribut;
        public List(string name, string roz,int rozmir, DateTime date,  string atribut)
        {
            Roz = roz;

            Rozmir = rozmir;
            Data = date;
            Name = name;
            Atribut = atribut;
        }
        public int CompareTo(object obj)
        {
            List p = (List)obj;
            if (this.Rozmir > p.Rozmir) return 1;
            if (this.Rozmir < p.Rozmir) return -1;
            return 0;
        }
        public void Data1(List[] a)
        {
            Console.WriteLine("\nСортування за розміром:");
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", "Назва", "Розширення", "Розмір", "Дата", "Атрибути");


            Array.Sort(a);
            foreach (List elem in a) elem.Inf();
        }
        public void Inf()
        {
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", Name,Roz,Rozmir, Data.ToShortDateString(),   Atribut);
        }
        public class SortByDate : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                List p1 = (List)ob1;
                List p2 = (List)ob2;
                if (p1.Data > p2.Data) return 1;
                if (p1.Data < p2.Data) return -1;
                return 0;
            }
        }
        public void One(List[] a)
        {
            Console.WriteLine("\nСортування за датою:");
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", "Назва", "Розширення", "Розмір", "Дата", "Атрибути");
            Array.Sort(a, new List.SortByDate());
            foreach (List elem in a) elem.Info1();
        }
        public void Info1()
        {
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", Name, Roz, Rozmir, Data.ToShortDateString(), Atribut);
        }

        public class SortByNumber : IComparer
        {
            int IComparer.Compare(object ob1, object ob2)
            {
                List p1 = (List)ob1;
                List p2 = (List)ob2;
                if (p1.Rozmir > p2.Rozmir) return 1;
                if (p1.Rozmir < p2.Rozmir) return -1;
                return 0;
            }
        }
        public void Two(List[] a)
        {
            Console.WriteLine("\nСортування за розміром:");
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", "Назва", "Розширення", "Розмір", "Дата", "Атрибути");
            Array.Sort(a, new List.SortByNumber());
            foreach (List elem in a) elem.Info();
        }
        public void Info()
        {
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", Name, Roz, Rozmir, Data.ToShortDateString(), Atribut);
        }

        public void Add()
        {
            Console.WriteLine("Write data:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        }
    }
    class Key
    {
        public static void KKEY()
        {
            FileStream file1 = File.OpenRead("text.txt");
            byte[] array = new byte[file1.Length];
            file1.Read(array, 0, array.Length);
            string textfromfile = System.Text.Encoding.Default.GetString(array);
            string[] s = textfromfile.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            file1.Close();
            List[] a = new List[s.Length / 5];
            int c = 0;
            while (a[c] != null)
            {
                ++c;
            }
            for (int i = 0; i < s.Length; i += 5)
            {
                a[c + i / 5] = new List(s[i], s[i + 1], int.Parse(s[i + 2]),DateTime.Parse(s[i + 3]), s[i + 4]);
            }
            bool[] delete = new bool[100];
            Console.WriteLine("Add note: A");
            Console.WriteLine("Edit note: E");
            Console.WriteLine("Remove note: R");
            Console.WriteLine("Show notes: Enter");
            Console.WriteLine("Sort by date: N");
            Console.WriteLine("Sort by rozmir: D");
            Console.WriteLine("Sort by date: S");
            Console.WriteLine("Exit: Esc");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.E:
                    Key.Edit(a);
                    break;

                case ConsoleKey.N:
                    a[0].One(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.D:
                    a[0].Two(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.S:
                    a[0].Data1(a);
                    Key.KKEY();
                    break;

                case ConsoleKey.Enter:
                    Key.Show(a);
                    break;

                case ConsoleKey.A:
                    Key.Add(a, c);
                    break;

                case ConsoleKey.R:
                    Key.Remove(a, delete);
                    break;

                case ConsoleKey.Escape:
                    break;
            }

        }
        public static void Show(List[] a)
        {
            Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ", "Назва", "Розширення", "Розмір", "Дата", "Атрибути");

            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    Console.WriteLine("{0,-20} {1,-20}{2,-30}{3,-20}{4,-15} ",a[i].Name,a[i].Roz,a[i].Rozmir,a[i].Data.ToShortDateString(),a[i].Atribut);
                }
            }
            Key.KKEY();
        }
        public static void Add(List[] a, int c)
        {
            Console.WriteLine("\nWrite number:");

            string str = Console.ReadLine();

            string[] elements = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Key.Parse(elements, true, a, c);
            Key.KKEY();
        }

        private static void Save(List m)
        {
            StreamWriter save = new StreamWriter("text.txt", true);



            save.WriteLine(m.Rozmir);
            save.WriteLine(m.Data);
            save.WriteLine(m.Name);
            save.WriteLine(m.Roz);
            save.WriteLine(m.Atribut);

            save.Close();
        }

        public static void Parse(string[] elements, bool save, List[] a, int counter)
        {
            for (int i = 0; i < elements.Length; i += 5)
            {
                a[counter + i / 5] = new List(elements[i],elements[i + 1],int.Parse(elements[i + 2]),DateTime.Parse(elements[i + 3]), elements[i + 4]);
                if (save)
                {
                    Save(a[counter + i / 5]);
                }
            }
        }
        public static void Remove(List[] a, bool[] delete)
        {
            Console.Write("\nName: ");

            string name = Console.ReadLine();

            bool[] write = new bool[a.Length];
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] != null)
                {
                    if (a[i].Name == name)
                    {
                        Console.WriteLine("{0,-15} {1,-10}\t {2,-10} {3,-20} {4,-20}", a[i].Rozmir, a[i].Data.ToShortDateString(), a[i].Name, a[i].Roz, a[i].Atribut);

                        Console.WriteLine("\nDELETE? (Y/N)\n");

                        var key = Console.ReadKey().Key;

                        if (key == ConsoleKey.Y)
                        {
                            a[i] = null;
                            delete[i] = true;
                            Key.Show(a);
                        }
                        else
                        {
                            delete[i] = false;
                        }
                    }
                }
            }
            Key.KKEY();
        }
        public static void Edit(List[] a)
        {
            Console.WriteLine("\nWhat do you want to edit?");
            string what = Console.ReadLine();
            switch (what)
            {
                case "name":
                    Console.WriteLine("What surname: ");
                    string name1 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name1)
                            {
                                Console.WriteLine("{0,-15} {1,-10}\t {2,-10} {3,-20} {4,-20}", a[i].Rozmir, a[i].Data.ToShortDateString(), a[i].Name, a[i].Roz, a[i].Atribut);

                                Console.WriteLine("New name: ");

                                string str = Console.ReadLine();

                                a[i].Name = str;

                                Key.Show(a);
                            }
                        }
                    }
                    break;

                case "Date":
                    Console.WriteLine("What subject: ");
                    string name2 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name2)
                            {
                                Console.WriteLine("{0,-15} {1,-10}\t {2,-10} {3,-20} {4,-20}", a[i].Rozmir, a[i].Data.ToShortDateString(), a[i].Name, a[i].Roz, a[i].Atribut);

                                Console.WriteLine("New date: ");
                                string str = Console.ReadLine();
                                a[i].Data = DateTime.Parse(str);
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Subject":
                    Console.WriteLine("What subject: ");
                    string name3 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name3)
                            {
                                Console.WriteLine("{0,-15} {1,-10}\t {2,-10} {3,-20} {4,-20}", a[i].Rozmir, a[i].Data.ToShortDateString(), a[i].Name, a[i].Roz, a[i].Atribut);
                                Console.WriteLine("New subject: ");
                                string str = Console.ReadLine();
                                a[i].Name = str;
                                Key.Show(a);
                            }
                        }

                    }
                    break;

                case "Number":
                    Console.WriteLine("What subject: ");
                    string name5 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name5)
                            {
                                Console.WriteLine("{0,-15} {1,-10}\t {2,-10} {3,-20} {4,-20}", a[i].Rozmir, a[i].Data.ToShortDateString(), a[i].Name, a[i].Roz, a[i].Atribut);
                                Console.WriteLine("New number: ");
                                int str = int.Parse(Console.ReadLine());
                                a[i].Rozmir = str;
                                Key.Show(a);
                            }
                        }
                    }
                    break;
                case "Form":
                    Console.WriteLine("What form: ");
                    string name6 = Console.ReadLine();
                    for (int i = 0; i < a.Length; ++i)
                    {
                        if (a[i] != null)
                        {
                            if (a[i].Name == name6)
                            {
                                Console.WriteLine("{0,-15} {1,-10}\t {2,-10} {3,-20} {4,-20}", a[i].Rozmir, a[i].Data.ToShortDateString(), a[i].Name, a[i].Roz, a[i].Atribut);
                                Console.WriteLine("New login: ");
                                string str = Console.ReadLine();
                                a[i].Atribut = str;
                                Key.Show(a);
                            }
                        }

                    }
                    break;
            }
            Key.KKEY();
        }
    }
}
