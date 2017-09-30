using System;

namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// The IoMonad is really a delegate - a function which performs IO
    /// </summary>
    public delegate IoResult<T> IoMonad<T>(RealWorld realWorld);

    /// <summary>
    /// The bind and result methods of the IO Monad
    /// </summary>
    public static class IoMonad 
    {
        public static Unit      UnitValue      = new Unit();
        public static RealWorld RealWorldValue = new RealWorld();

        public static 
        IoMonad<TOut>
        Bind<TIn, TOut>(
            IoMonad<TIn>        ioMonad,
            Func<
                TIn, 
                IoMonad<TOut>>  getNextIoMonad)
        {
            return 
                realWorld => 
                {
                    // invoke the ioMonad, by passing it the RealWorld
                    var resultIoMonad = ioMonad(realWorld);    

                    // get the next IO monad, by calling getNextIoMonad, with the result of the first IO action
                    var nextIoMonad = getNextIoMonad(resultIoMonad.Value);  

                    // invoke the nextiIoMonad, by passing it the RealWorld, returned by the first IO action 
                    var result = nextIoMonad(resultIoMonad.RealWorld);   

                    return result;
                };
        }

        /// <summary>
        /// Lift a function into an InMonad by wrapping it into another function which takes a real world and 
        /// returns an IoResult
        /// </summary>
        public static
        IoMonad<T>
        Return<T>(
            Func<T>           func)
        {
            return 
                realWorld => 
                    new IoResult<T>(
                        realWorld, 
                        func());
        }
    }
}
