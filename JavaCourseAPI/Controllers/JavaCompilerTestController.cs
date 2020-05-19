using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JavaCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JavaCompilerTestController : ControllerBase
    {

        // GET api/values
        [HttpGet("test3")]
        public ActionResult<IEnumerable<string>> Test3()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\java.exe";
            p.StartInfo.WorkingDirectory = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\";
            p.StartInfo.Arguments = "Test";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();


            return new string[] { "koniec - " + strOutput };
        }

        // GET api/values
        [HttpGet("test4")]
        public ActionResult<IEnumerable<string>> Test4()
        {
            //kompilacja kodu - z plik.java --> plik.class
            //Process.Start(@"D:\WAT\Magisterka\jdk1.8.0_202\bin\javac", @"D:\WAT\Magisterka\jdk1.8.0_202\bin\TestImie.java");
            Process compileJavaCodeProcess = new Process();
            compileJavaCodeProcess.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\javac";
            compileJavaCodeProcess.StartInfo.Arguments = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\TestImie.java";
            compileJavaCodeProcess.Start();
            compileJavaCodeProcess.WaitForExit();

            //uruchomienie kodu (plik.class)
            Process p = new Process();
            p.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\java.exe";
            p.StartInfo.WorkingDirectory = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\";
            p.StartInfo.Arguments = "TestImie";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            p.StandardInput.WriteLine("Pumba");
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();


            return new string[] { "koniec - " + strOutput };
        }

        // GET api/values
        [HttpGet("test5")]
        public ActionResult<IEnumerable<string>> Test5()
        {
            Process compileJavaCodeProcess = new Process();
            compileJavaCodeProcess.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\javac";
            compileJavaCodeProcess.StartInfo.Arguments = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\TestImieNazwisko.java";
            compileJavaCodeProcess.Start();
            compileJavaCodeProcess.WaitForExit();

            Process p = new Process();
            p.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\java.exe";
            p.StartInfo.WorkingDirectory = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\";
            p.StartInfo.Arguments = "TestImieNazwisko";

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            p.StandardInput.WriteLine("Pumba");
            p.StandardInput.WriteLine("Guziec");

            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return new string[] { "koniec - " + strOutput };
        }

        // GET api/values
        [HttpGet("test6")]
        public ActionResult<IEnumerable<string>> Test6()
        {
            Process compileJavaCodeProcess = new Process();
            compileJavaCodeProcess.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\javac";
            compileJavaCodeProcess.StartInfo.Arguments = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\CalcAB.java";
            compileJavaCodeProcess.Start();
            compileJavaCodeProcess.WaitForExit();

            Process p = new Process();
            p.StartInfo.FileName = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\java.exe";
            p.StartInfo.WorkingDirectory = @"D:\WAT\Magisterka\jdk1.8.0_202\bin\";
            p.StartInfo.Arguments = "CalcAB";

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            p.StandardInput.WriteLine("5");
            p.StandardInput.WriteLine("6");

            //wszystkie outputy
            string strOutput = p.StandardOutput.ReadToEnd();

            //error jesli byl
            string err = p.StandardError.ReadToEnd();

            p.WaitForExit();

            return new string[] { "koniec - " + strOutput + err};
        }
    }
}