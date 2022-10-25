using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToRSTClasses
{
    class TestCSharpClass
    {
        private string Property1;
        private List<string> Property2;

        public int Number1;

        public string Property12
        {
            get => Property1;

            set => Property1 = value;
        }

        TestCSharpClass()
        {

        }

        TestCSharpClass(string testString1, int testInt1)
        {

        }

        private void PrivateTestMethodWithTwoArgumentsAndNoReturnValue(string testString1, int testInt1)
        {

        }

        private int PrivateTestMethodWithTwoArgumentsAndReturnValue(string testString1, int testInt1)
        {
            return 1;
        }

        public void PublicTestMethodWithTwoArgumentsAndNoReturnValue(string testString1, int testInt1)
        {
            
        }

        public int PublicTestMethodWithTwoArgumentsAndReturnValue(string testString1, int testInt1)
        {
            return 1;
        }
    }
}
