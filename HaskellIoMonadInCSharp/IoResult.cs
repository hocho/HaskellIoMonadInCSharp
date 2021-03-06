﻿
namespace HaskellIoMonadInCSharp
{
    /// <summary>
    /// The result of invoking an IO
    /// </summary>
    public struct IoResult<T>
    {
        public readonly RealWorld   RealWorld;
        public readonly T           Value;

        public  
        IoResult(
            RealWorld   realWorld,
            T           value)
        {
            RealWorld = realWorld;
            Value     = value;
        }
    }
}
