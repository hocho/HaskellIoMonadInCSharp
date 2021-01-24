using System;

namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// The bind and return methods of the IO Builder
    /// </summary>
    public static class IoBuilder 
    {
        public static Unit      UnitValue      = new Unit();
        public static RealWorld RealWorldValue = new RealWorld();

        public static 
        Io<TOut>
        Bind<TIn, TOut>(
            Io<TIn>        io,
            Func<
                TIn, 
                Io<TOut>>  getNextIo)
        {
            return 
                realWorld => 
                {
                    // invoke the IO, by passing it the RealWorld
                    IoResult<TIn> 
                    resultOfIo = io(realWorld);    

                    // get the next IO monad, by calling getNextIo, with the result of the first IO action
                    Io<TOut> 
                    nextIo = getNextIo(resultOfIo.Value);  

                    // invoke the nextIo, by passing it the RealWorld which was returned by the first IO action 
                    IoResult<TOut> 
                    result = nextIo(resultOfIo.RealWorld);   

                    return result;
                };

            // Could be compressed to
            //    return 
            //        realWorld => 
            //        {
            //            var resultOfIo = io(realWorld);    

            //            return getNextIo(resultOfIo.Value)(resultOfIo.RealWorld);
            //        };

        }

        /// <summary>
        /// Lift a function into an Io by wrapping it into another function which takes in a RealWorld  and 
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
