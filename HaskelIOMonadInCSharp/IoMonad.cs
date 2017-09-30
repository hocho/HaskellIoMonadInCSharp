using System;

namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// The Io Monad is really a delegate - a function which performs IO
    /// </summary>
    public delegate 
    IoResult<T> 
    Io<T>(
        RealWorld   realWorld);

    /// <summary>
    /// The bind and result methods of the IO Monad
    /// </summary>
    public static class IoBuilder 
    {
        public static Unit      UnitValue      = new Unit();
        public static RealWorld RealWorldValue = new RealWorld();

        public static 
        Io<TOut>
        Bind<TIn, TOut>(
            Io<TIn>        ioMonad,
            Func<
                TIn, 
                Io<TOut>>  getNextIoMonad)
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
        /// Lift a function into an Io by wrapping it into another function which takes a real world and 
        /// returns an IoResult
        /// </summary>
        public static
        Io<T>
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
