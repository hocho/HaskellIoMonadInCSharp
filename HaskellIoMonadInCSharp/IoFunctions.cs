using static HaskellIoMonadInCSharp.IoBuilder;
using static System.Console;

namespace HaskellIoMonadInCSharp
{
    public static class IoFunctions
    {
        /// <summary>
        /// Converts an read line, into an IoMonad, using return
        /// </summary>
        public static 
        Io<string>
        GetLn()
        {
            return 
                Return(
                    () => ReadLine());
        }

        /// <summary>
        /// Converts a write line, into an IoMonad, using return
        /// </summary>
        public static 
        Io<Unit>
        PutStrLn(
            string str)
        {
            return 
                Return(
                    () => 
                        {  
                            WriteLine(str); 

                            return UnitValue; 
                        });
        }
    }
}
