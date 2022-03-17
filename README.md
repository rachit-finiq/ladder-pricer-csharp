# Ladder Pricer (C#)
## Problem Statement:

**PART 1**: Create a new model for pricing in which the banks(from here off denoted as suppliers) only send the prices/size for different "slabs" and the FINIQ server is responsible for getting the best price for the requested size.
E.g.: Suppose the bank gives the following slabs:
| Size | Price per size |
|------|-------|
| 1M | 1.033 |
| 5M | 1.032 |
| 10M | 1.034 |

For the request of 11M, the best possible price is 11.353 (Buy 2 5M and 1 1M).

**PART 2:** Using the above code as baseline, create a full supplier-client multi-threaded system which simulates the real life scenerio. Also load test the program with multiple suppliers and clients.

## Threading in C#
In C#, the System.Threading. Thread class is used for working with threads. It allows creating and accessing individual threads in a multithreaded application. The first thread to be executed in a process is called the **main thread**. When a C# program starts execution, the main thread is automatically created. The threads created using the Thread class are called the child threads of the main thread. You can access a thread using the **CurrentThread** property of the Thread class.

For more information refer to this link: https://www.c-sharpcorner.com/article/Threads-in-CSharp/

## Installing `pythonnet`
`pythonnet` is a Python.NET is a package that gives Python programmers nearly seamless integration with the .NET Common Language Runtime (CLR) and provides a powerful application scripting tool for .NET developers. It allows Python code to interact with the CLR, and may also be used to embed Python into a .NET application.

> To install `pythonnet`:

          pip install pythonnet

For running this file, we also need CLR which is a simple terminal string styling library. To install clr, run the following command:

          pip install clr
