using System.Text.RegularExpressions;

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
            string activeClassName = "";

            // Store all properties here to organize them afterwards
            List<string> allPublicProperties = new List<string>();
            List<string> allPrivateProperties = new List<string>();
            List<string> allPublicMethods = new List<string>();
            List<string> allPrivateMethods = new List<string>();

            foreach (string line in cSharpClass)
            {
                // Remove all semicolons and split all keywords into a list
                List<string> listStrLineElements = line.Replace(";", "").Split(' ').ToList();

                for (int i = 0; i < listStrLineElements.Count; i++)
                {
                    string keyword = listStrLineElements[i];
                    if (!isInClass)
                    {
                        if (keyword == "class") // When class starts
                        {
                            newRstFile.Add(keyword + " " + listStrLineElements[i + 1] + " {");
                            activeClassName = listStrLineElements[i + 1];
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
                                if (openBrackets == 0) // When class ends
                                {
                                    newRstFile.AddRange(allPublicProperties);
                                    newRstFile.AddRange(allPrivateProperties);
                                    newRstFile.AddRange(allPublicMethods);
                                    newRstFile.AddRange(allPrivateMethods);
                                    newRstFile.Add("} \n");
                                    isInClass = false;
                                    activeClassName = "";
                                }
                                break;

                            case "public":
                                if (!line.Contains("(")) // When property and not a method
                                {
                                    allPublicProperties.Add("    + " + listStrLineElements[i + 2] + " : " + listStrLineElements[i + 1]);
                                } else
                                {
                                    if (line.Contains(activeClassName)) allPublicMethods.Add(ExtractMethod(keyword, line, false, false));
                                    if (!line.Contains(activeClassName)) allPublicMethods.Add(ExtractMethod(keyword, line, false, true));
                                }
                                break;

                            case "private":
                                if (!line.Contains("(")) // When property and not a method
                                {
                                    allPrivateProperties.Add("    - " + listStrLineElements[i + 2] + " : " + listStrLineElements[i + 1]);
                                } else
                                {
                                    allPrivateMethods.Add(ExtractMethod(keyword, line, false, true));
                                }
                                break;
                        }

                        if (keyword.Contains(activeClassName) && line.Contains("(")) // Check for constructor when privacy not set
                        {
                            allPublicMethods.Add(ExtractMethod(keyword, line, true, false));
                        }
                    }
                }
            }

            Console.WriteLine("EOL");
        }

        static string ExtractMethod(string keyword, string line, bool isPrivacyLess, bool hasReturnValue)
        {
            string methodName = "";
            if (isPrivacyLess) methodName = keyword.Split('(').ToList()[0];
            if (!isPrivacyLess) {
                while (line.StartsWith(" "))
                {
                    line = line.Remove(0, 1);
                }
                methodName = line.Split(' ').ToList()[1]; 
                if (hasReturnValue) methodName = line.Split(' ').ToList()[2];
                methodName = methodName.Split('(').ToList()[0]; 
            }

            string[] variables = line.Split('(', ')')[1].Split(",");
            string endPublicProperty = "";

            endPublicProperty += "    + " + methodName + "(";


            if (variables.Length > 0 && !variables[0].Equals(""))
            {
                int cnt = 1;
                foreach (string element in variables)
                {
                    string newElement = element;
                    if (element.StartsWith(" ")) newElement = element.Remove(0, 1);
                    string[] elements = newElement.Split(" ");
                    endPublicProperty += elements[1] + " : " + elements[0];
                    if (cnt < variables.Length) endPublicProperty += ", ";
                    cnt++;
                }
            }
            endPublicProperty += ")";

            if (!isPrivacyLess)
            {
                endPublicProperty += " : " + keyword.Split('(').ToList()[0];
            }
            return endPublicProperty;
        }
    }
}
