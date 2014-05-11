using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MainLib
{
    public class SpinExecutor
    {
        public static string SpinPath = @"C:\MinGW\bin\";
        public static string SpinProgram = SpinPath + "spin.exe";
        public static string PromelaFile = SpinPath + "my_test.pml";
        public static string GenerateCArg = "-a my_test.pml";
        public static string RunPromela = "my_test.pml";
        public static string RunContrExample = "-t -p my_test.pml";
        public static string CompillerProgram = SpinPath + "mingw32-gcc.exe";
        public static string CompileArg = "pan.c -o pan.exe";
        public static string RunCommand = SpinPath + "pan.exe";
        public static string TmpPath = @"C:\temp\";

        public static string Output = "";
        
        private static bool ExecCommand(string command, string arg = "")
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WorkingDirectory = SpinPath;
            p.StartInfo.FileName = command;
            p.StartInfo.Arguments = arg;
            p.Start();
            Output = p.StandardOutput.ReadToEnd();
            Output += p.StandardError.ReadToEnd();
            p.WaitForExit();
            return p.ExitCode == 0;
        }

        private static bool WritePromelaFile(string code)
        {
            try
            {
                File.WriteAllText(PromelaFile, code);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private static bool GenerateCFile()
        {
            return ExecCommand(SpinProgram,GenerateCArg);
        }

        private static bool CompineCFile()
        {
            return ExecCommand(CompillerProgram, CompileArg);
        }

        public static bool CheckPromelaCode(string code, ref string result)
        {
            if (WritePromelaFile(code) && GenerateCFile() && CompineCFile() && ExecCommand(RunCommand))
            {
                result = Output;
                return true;
            }
            result = Output;
            return false;
        }
        
        public static bool RunPromelaCode(string code, ref string result)
        {
            if (WritePromelaFile(code) && ExecCommand(SpinProgram, RunPromela))
            {
                result = Output.Replace("\t","");
                return true;
            }
            result = Output;
            return false;
        }
        
        public static bool ContrExamplePromelaCode(string code, ref string result)
        {
            if (WritePromelaFile(code) && ExecCommand(SpinProgram, RunContrExample))
            {
                result = Output;
                return true;
            }
            result = Output;
            return false;
        }
    }
}
