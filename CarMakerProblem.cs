using System;
using System.Threading;

namespace sleeping_mechanic__multithreading_
{
    public class CarMakerProblem
    {
        static Semaphore customers = new Semaphore(0, CAPACITY); // дали има хора които чакат
        static Semaphore carMaker = new Semaphore(0, 1);  //ако е готов механика клиента отива ако не чака
        static Semaphore acess = new Semaphore(1, 1); // provides single acess to waiting
        const int CAPACITY = 2;
        static Random random = new Random();
        static int waiting = 0;

        public static void CarMaker()
        {

            while (true)
            {
                Thread.Sleep(random.Next(1000, 2000));

                if (waiting <= 0)
                {

                    Console.WriteLine("CarMaker sleeping..");

                }
                else
                {
                    customers.WaitOne();
                    acess.WaitOne();
                    waiting--;

                    Console.WriteLine("  CarMaker working...");
                    Thread.Sleep(random.Next(1000, 2000));
                    try { carMaker.Release(); }
                    catch { };
                    try { acess.Release(); }
                    catch { };
                    Console.WriteLine("  CarMaker done.");
                }
            }
        }

        public static void Client(Object o)
        {
            var n = (int)o;
            while (true)
            {

                Thread.Sleep(random.Next(1000, 20000));
                acess.WaitOne();
                if (waiting < CAPACITY)
                {

                    Console.WriteLine("  Client " + n + " waiting...");
                    waiting++;
                    try { customers.Release(); }
                    catch { };
                    try { acess.Release(); }
                    catch { };
                    carMaker.WaitOne();
                    Console.WriteLine("  Client " + n + " served.");
                }
                else
                {
                    try { acess.Release(); }
                    catch { };
                    Console.WriteLine("  Client " + n + " no space.");

                }
            }
        }
    }
}
