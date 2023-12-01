
namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// The result of invoking an IO
    /// </summary>
    public record struct
    IoResult<T>(
        RealWorld   RealWorld,
        T           Value)
    {
    }
}
