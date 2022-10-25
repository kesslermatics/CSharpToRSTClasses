namespace CSharpToRSTClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the class path");
            string classPath = Console.ReadLine();

            string[] cSharpClass = File.ReadAllLines(classPath);

            while (true);

        }
    }
}
