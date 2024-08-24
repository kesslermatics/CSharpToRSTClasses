# CSharpToRSTClasses

## Overview

CSharpToRSTClasses is a simple command-line tool designed to convert C# class definitions into reStructuredText (RST) format. This tool parses C# code to extract class structures, including properties and methods, and generates corresponding RST diagrams. These diagrams can be integrated into Sphinx documentation or other RST-compatible platforms to visualize class hierarchies and relationships.

## Features

- **Automatic Parsing**: The tool automatically identifies classes, properties, and methods within a C# file.
- **Supports Public and Private Members**: It distinguishes between public and private properties and methods, marking them accordingly in the RST output.
- **Constructor Detection**: Automatically detects constructors, even when access modifiers are not explicitly stated.

## How to Use

### Prerequisites

- .NET SDK installed on your machine.

### Running the Tool

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/CSharpToRSTClasses.git
   cd CSharpToRSTClasses
   ```

2. **Build and Run the Project:**
   ```bash
   dotnet build
   dotnet run
   ```
   Execute the compiled binary or run the project using dotnet. It can also be run by opening the VS-Solution.

3. **Input the Path:**
   
   The tool will prompt you to enter the path to the C# file you want to convert.

4. **Output:**
   
   The tool will generate an RST file in the same directory as the input file, with the suffix RST.txt.
   For example, if your input file is MyClass.cs, the output will be MyClass.csRST.txt.

## Example

Given a simple C# class:

```csharp
public class MyClass
{
    public int MyProperty { get; set; }
    private string myField;

    public MyClass() { }

    public void MyMethod(int param) { }

    private void PrivateMethod() { }
}
```

The generated RST output would look like:

```rst
class MyClass {
    + MyProperty : int
    - myField : string
    + MyClass()
    + MyMethod(param : int)
    - PrivateMethod()
}
```
