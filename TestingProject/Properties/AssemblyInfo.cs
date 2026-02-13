using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// General Information about an assembly
[assembly: AssemblyTitle("courseproject")]
[assembly: AssemblyDescription("Core project for CourseProject application")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("courseproject")]
[assembly: AssemblyCopyright("Copyright ©  2026")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// COM visibility
[assembly: ComVisible(false)]

// GUID for typelib if exposed to COM
[assembly: Guid("d06edcac-01fa-419a-8d69-d8258c3831b6")]

// Version information
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

//Force MSTest to run tests sequentially
[assembly: DoNotParallelize]

// Optional: allow test project to access internal members
[assembly: InternalsVisibleTo("courseproject.Tests")]
