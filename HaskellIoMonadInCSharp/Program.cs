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
        static void Main()
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
            // line <- GetLine 
            // PutStrLn("Hello Monad" + line)

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Example 1.");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("GetLine");
            Console.WriteLine(@"PutStrLn(""Monadic hello to "" + line)");
            Console.WriteLine();

            Bind(
                GetLine(),
                line => 
                    PutStrLn("Monadic hello to " + line))
            (RealWorldValue);
        }

        static void 
        Example2()
        {
            // line1 <- GetLine 
            // line2 <- GetLine
            // PutStrLn("Monadic hello to " + line1 + " " + line2)

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Example 2.");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("GetLine");
            Console.WriteLine("GetLine");
            Console.WriteLine(@"PutStrLn(""Monadic hello to "" + line1 + "" "" + line2)");
            Console.WriteLine();

            Bind(
                GetLine(),
                line1 =>
                        Bind(
                            GetLine(),
                            line2 => 
                                PutStrLn("Hello Monad " + line1 + " " + line2)))
            (RealWorldValue);
        }


        static void 
        Example3()
        {
            // PutStrLn("Enter your first name")
            // line1 <- GetLine 
            // PutStrLn("Enter your last name")
            // line2 <- GetLine 
            // PutStrLn("Monadic hello to " + line1 + " " + line2)

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Example 3.");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine(@"PutStrLn(""Enter your first name"")");
            Console.WriteLine("GetLine");
            Console.WriteLine(@"PutStrLn(""Enter your last name"")");
            Console.WriteLine("GetLine");
            Console.WriteLine(@"PutStrLn(""Monadic hello to "" + line1 + "" "" + line2)");
            Console.WriteLine();

            Bind(
                PutStrLn("Enter your first name"),
                _ =>
                    Bind(
                        GetLine(),
                        line1 =>
                            Bind(
                                PutStrLn("Enter your last name"),
                                __ =>
                                    Bind(
                                        GetLine(),
                                        line2 => 
                                            PutStrLn("Monadic Hello to " + line1 + " " + line2)))))
            (RealWorldValue);
        }
    }
}
