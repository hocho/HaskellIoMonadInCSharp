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

Ultimately, IO actions are chained together by passing binds as continuation to other binds. 

