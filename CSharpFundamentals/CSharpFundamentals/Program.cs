using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSharpFundamentals.Examples.Async;
using CSharpFundamentals.Examples.LINQ;

namespace CSharpFundamentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomLinq linqExample = new CustomLinq();
            linqExample.RunExample();
            Console.ReadLine();
        }
    }




}
