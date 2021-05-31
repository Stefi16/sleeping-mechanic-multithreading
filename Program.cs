using System;
using System.Threading;

namespace sleeping_mechanic__multithreading_
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread carMaker = new Thread(CarMakerProblem.CarMaker);
            carMaker.Start();

            Thread client1 = new Thread(CarMakerProblem.Client);
            client1.Start(1);


            Thread client2 = new Thread(CarMakerProblem.Client);
            client2.Start(2);

            Thread client3 = new Thread(CarMakerProblem.Client);
            client3.Start(3);
        }
    }
}
