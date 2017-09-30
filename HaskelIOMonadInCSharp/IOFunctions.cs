using System;
using static HaskellIoMonadInCSharp.IoMonad;

namespace HaskellIoMonadInCSharp
{
    public static class IoFunctions
    {
        /// <summary>
        /// Converts an read line, into an IoMonad, using return
        /// </summary>
        public static 
        IoMonad<string>
        GetLn()
        {
            return 
                Return(
                    () => Console.ReadLine());
        }

        /// <summary>
        /// Converts a write line, into an IoMonad, using return
        /// </summary>
        public static 
        IoMonad<Unit>
        PutStrLn(
            string str)
        {
            return 
                Return(
                    () => 
                        {  
                            Console.WriteLine(str); 

                            return UnitValue; 
                        });
        }
    }
}
