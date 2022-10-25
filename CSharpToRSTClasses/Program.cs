namespace CSharpToRSTClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the class path");
            //string classPath = Console.ReadLine();
            string classPath = "D:\\Forschung\\CSharpToRSTClasses\\CSharpToRSTClasses\\CSharpToRSTClasses\\TestCSharpClass.cs";

            string[] cSharpClass = File.ReadAllLines(classPath);

            List<string> newRstFile = new List<string>();

            bool isInClass = false;
            int openBrackets = 0;

            List<string> allPublicProperties = new List<string>();
            List<string> allPrivateProperties = new List<string>();

            foreach (string line in cSharpClass)
            {
                List<string> listStrLineElements = line.Replace(";", "").Split(' ').ToList();

                for (int i = 0; i < listStrLineElements.Count; i++)
                {
                    string keyword = listStrLineElements[i];
                    if (!isInClass)
                    {
                        if (keyword == "class")
                        {
                            newRstFile.Add(keyword + " " + listStrLineElements[i + 1] + " {");
                            isInClass = true;
                        }
                    } else
                    {
                        switch (keyword)
                        {
                            case "{":
                                openBrackets++;
                                break;

                            case "}":
                                openBrackets--;
                                if (openBrackets == 0)
                                {
                                    newRstFile.AddRange(allPublicProperties);
                                    newRstFile.AddRange(allPrivateProperties);
                                    newRstFile.Add("}");
                                    isInClass = false;
                                }
                                break;

                            case "public":
                                if (!line.Contains("("))
                                {
                                    allPublicProperties.Add("    + " + listStrLineElements[i + 2] + " : " + listStrLineElements[i + 1]);
                                }
                                break;

                            case "private":
                                if (!line.Contains("("))
                                {
                                    allPrivateProperties.Add("    - " + listStrLineElements[i + 2] + " : " + listStrLineElements[i + 1]);
                                }
                                break;
                        }
                    }
                }
            }

            Console.WriteLine("EOL");
        }
    }
}
