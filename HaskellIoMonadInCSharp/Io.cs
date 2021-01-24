
namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// The Io Monad is really a delegate - a function which performs IO
    /// </summary>
    public 
    delegate 
    IoResult<T> 
    Io<T>(
        RealWorld   realWorld);
}
