using System;
using static HaskellIoMonadInCSharp.IoFunctions;
using static HaskellIoMonadInCSharp.IoBuilder;

namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// Inspired by https://wiki.haskell.org/IO_inside#.27.3E.3E.3D.27_and_.27do.27_notation
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Example3();
       
            Console.WriteLine();
            Console.WriteLine("Enter to End");
            Console.ReadLine();
        }

        static void 
        Example1()
        {
            // line <- GetLn 
            // PutStrLn("Hello Monad" + line)

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Example 1.");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("GetLn");
            Console.WriteLine(@"PutStrLn(""Monadic hello to "" + line");
            Console.WriteLine();

            Bind(
                GetLn(),
                line => 
                    PutStrLn("Monadic hello to " + line))
            (RealWorldValue);
        }

        static void 
        Example2()
        {
            // line1 <- GetLn 
            // line2 <- GetLn
            // PutStrLn("Monadic hello to " + line1 + " " + line2)

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Example 2.");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("GetLn");
            Console.WriteLine("GetLn");
            Console.WriteLine(@"PutStrLn(""Monadic hello to "" + line1 + "" "" + line2)");
            Console.WriteLine();

            Bind(
                GetLn(),
                line1 =>
                        Bind(
                            GetLn(),
                            line2 => 
                                PutStrLn("Hello Monad " + line1 + " " + line2)))
            (RealWorldValue);
        }


        static void 
        Example3()
        {
            // PutStrLn("Enter your first name")
            // line1 <- GetLn 
            // PutStrLn("Enter your last name")
            // line2 <- GetLn 
            // PutStrLn("Monadic hello to " + line1 + " " + line2)

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Example 3.");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(@"PutStrLn(""Enter your first name"")");
            Console.WriteLine("GetLn");
            Console.WriteLine(@"PutStrLn(""Enter your last name"")");
            Console.WriteLine("GetLn");
            Console.WriteLine(@"PutStrLn(""Monadic hello to "" + line1 + "" "" + line2)");
            Console.WriteLine();

            Bind(
                PutStrLn("Enter your first name"),
                _ =>
                    Bind(
                        GetLn(),
                        line1 =>
                            Bind(
                                PutStrLn("Enter your last name"),
                                __ =>
                                    Bind(
                                        GetLn(),
                                        line2 => 
                                            PutStrLn("Monadic Hello to " + line1 + " " + line2)))))
            (RealWorldValue);
        }
    }
}
