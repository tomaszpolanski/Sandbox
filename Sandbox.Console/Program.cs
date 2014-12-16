using Sandbox.Linq;

namespace Sandbox.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var display = LinqSandbox.LinqBasics();

            foreach (int something in display)
            {
                System.Console.WriteLine(something);
            }


            LinqSandbox.LinqLessBasic();

            string testString = "    this is some   , string, that we  , would to test, with ; parsing";

            string statementBased = LinqSandbox.FoldTextToOneLine(testString);
            string expressionBased = LinqSandbox.FoldTextToOneLineLinq(testString);

            System.Console.ReadKey();
        }
    }
}
