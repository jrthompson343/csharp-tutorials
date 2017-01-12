using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFundamentalTests
{
    public class ClassFood
    {
        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            return ((ClassFood) obj).Name == Name;
        }
    }

    public struct StructFood
    {
        public string Name { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ClassEquality()
        {
            ClassFood toast = new ClassFood {Name = "Toast"};
            ClassFood toast2 = new ClassFood {Name = "Toast"};

            bool referenceEquality = toast == toast2;
            bool valueEquality = toast.Equals(toast2);   // <--- false since ClassFood does not override Equals

            Assert.AreEqual(referenceEquality, false);
            Assert.AreEqual(valueEquality, true);
        }

        [TestMethod]
        public void StructEquality()
        {
            StructFood toast = new StructFood { Name = "Toast" };
            StructFood toast2 = new StructFood { Name = "Toast" };

            //== operator does not work on structs.  Line will result in compiler error.
            //bool referenceEquality = toast == toast2;
            bool valueEquality = toast.Equals(toast2);   // <--- false since ClassFood does not override Equals
            Assert.AreEqual(valueEquality, true); // <--- Uses reflection to compare each field in the strcut
        }

        [TestMethod]
        public void TestStringEquality()
        {
            string A = "cat";
            string B = "cat";

            bool AequalB = A == B;
            Assert.AreEqual(AequalB, true);
        }

        [TestMethod]
        public void TestFloatEquality()
        {
            float A = 6.0000000f;
            float B = 6.0000001f;

            //Even though the numbers are visually different the runtime evaluates them to true, due to loss of precision.
            bool AequalB = A == B;
            Assert.AreEqual(AequalB, true);

            A = 5.05f;
            B = 0.95f;

            //Again rounding errors cause 6.0f to not equal 6.0f
            bool SumIsSix = (A + B) == 6.0f;
            Assert.AreEqual(SumIsSix, false);
        }
    }
}
