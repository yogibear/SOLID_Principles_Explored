using System;

namespace OCP_Load_Class
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get Env 
            OCP_Reflection.RunConsumer(args[0]);
            OCP_Reflection.RunConsumer("TeamsConsumer");
        }
    }
}
