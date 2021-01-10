# Emulating the Haskell IO Monad in CSharp

[Check out this page, if you are unfamiliar with Haskell IO Monads and their raison d'être](https://wiki.haskell.org/IO_inside#.27.3E.3E.3D.27_and_.27do.27_notation)

**Caveat emptor: I am not an expert in Haskell, this is my attempt to understand Monads in general and the IO monad in particular.**

This CSharp solution shows the underpinning of how the IO Monad works. The key to understanding the monad is that it is a *container/context which wraps a function that perform IO* and that the *container/context itself is a function*, which when unwrapped, invokes the wrapped function.

Hera are the main artifacts, briefly explained in sequence
1. **Unit** and **RealWorld** are primitives; empty structures, filling in for similar concepts in Haskell. *Unit* is a tuple of 0 arity, representing a type that only allows one value. *RealWorld* is the baton passed between IO function to force sequencing.
2. **Io** is a generic delegate, a function with takes a RealWorld and returns an IO Result.
3. **IoResult** is a generic tuple holding the RealWorld and the result of an IO Action. The result could be *Unit* for cases like writing to the console, which does not return a result.
4. The **Return** method, takes an IO function and creates an *Io*, by wrapping it.
5. **IoFunctions** like **GetLn** and **PutStrLn** create corresponding *IoMonads* using the *Return* method.
6. The **Bind** method is the meat of the matter. 
   * It invokes the *Io* **(a)** passed to it.
   * Gets the next *Io* **(b)** by invoking the continuation passed to it, with the result of the previous invocation.
   * Invokes the resulting *Io* **(b)**, and returns its result.
   * *RealWorld* is passed around, to mimic how Haskell uses it in theory; in practice it is optimized away by the Haskell compiler.

```
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
}
```

**Finally, IO actions are chained together by passing binds as continuation to other binds.**

```
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
```




