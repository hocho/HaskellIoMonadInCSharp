# Understand the Haskell IO Monad using CSharp

[Check out this page, for an understand of Haskell IO Monads and their raison d'être](https://wiki.haskell.org/IO_inside#.27.3E.3E.3D.27_and_.27do.27_notation)

Shows the underpinning of how the IO Monad works. The key to understanding the monad is that it a *container for a function* that takes a Unit and returns a value. 

Take a look at the following artifacts, in order for a better understanding
1. **Unit** and **ReadWorld** are primitives; empty structures, filling in for similar concepts in Haskell.
2. **IoMonad** is a generic delegate, a function with takes a RealWorld and returns an IO Result.
3. **IoResult** is a generic tuple holding the RealWorld and the result of an IO Action. The result could be Unit for cases like writing to the console, which do not return a result.
4. The **Return** method, takes an IO function and returns an *IoMonad*, by wrapping it.
5. **IoFunctions** like **GetLn** and **PutStrLn** create and return the corresponding *IoMonads* using the *Return* method.
6. The **Bind** method is the meat of the matter. 
   * It invokes the *IoMonad* passed it to.
   * Gets the next *IoMonad* by invoking the continuation passed to it, with a result of the previous invocation.
   * Invokes the resulting *IoMonad*, and returns its result.

```
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
