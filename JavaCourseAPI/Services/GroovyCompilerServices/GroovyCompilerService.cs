using AutoMapper;
using JavaCourseAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JavaCourseAPI.Services.GroovyCompilerServices
{
    public class GroovyCompilerService : IGroovyCompilerService
    {
        private readonly IMapper _mapper;

        public GroovyCompilerService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ActionResult<string>> GroovyCompileAsync(CompileDTO compileDTO)
        {
            string startupPath = Environment.CurrentDirectory;
            string groovyc = Path.Combine(startupPath, @"Modules\groovy-3.0.1\bin\groovyc");
            string groovy = Path.Combine(startupPath, @"Modules\groovy-3.0.1\bin\groovy");
            string filesDir = Path.Combine(startupPath, @"Files");

            //znajduje nazwe klasy w kodzie - nazwa pomiedzy "class " a " " 
            string className = Regex.Match(compileDTO.code, @"class (.+?) ").Groups[1].Value;
            //string classNameGroovy = className + ".groovy";
            string classNamePath = Path.Combine(startupPath, className);
            string classNameGroovyPath = Path.Combine(filesDir, className + ".groovy");

            bool wasCreated = this.CreateFile(classNameGroovyPath, compileDTO.code);

            Process compileJavaCodeProcess = new Process();
            compileJavaCodeProcess.StartInfo.FileName = "cmd.exe";
            compileJavaCodeProcess.StartInfo.WorkingDirectory = filesDir;
            compileJavaCodeProcess.StartInfo.Arguments = @"/C " + groovyc + " " + classNameGroovyPath;   // classNameGroovyPath;
            compileJavaCodeProcess.Start();
            compileJavaCodeProcess.WaitForExit();

            string strOutput = "";
            string err = "";
            string exceptionMessage = "";

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.WorkingDirectory = filesDir;
                p.StartInfo.Arguments = @"/C " + groovy + " " + className;

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;

                p.OutputDataReceived += (a, b) => strOutput = strOutput + "\n" + b.Data;
                p.ErrorDataReceived += (a, b) => err = err + "\n" + b.Data;

                p.Start();
                //var x = p.;

                if (compileDTO.parameters.Count > 0)
                {
                    for (int i = 0; i < compileDTO.parameters.Count; i++)
                    {
                        p.StandardInput.WriteLine(compileDTO.parameters[i]);
                    }
                }

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                p.WaitForExit(3000);
                //wszystkie outputy
                //strOutput = p.StandardOutput.ReadToEnd();
                //p.CancelOutputRead();
                //error jesli byl
                //err = p.StandardError.ReadToEnd();
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            if (wasCreated == true)
            {
                this.DeleteFile(Path.Combine(filesDir, @"GroovyDateInitialization.class"));
                this.DeleteFile(classNamePath + ".class");
                this.DeleteFile(classNameGroovyPath);
            }

            return strOutput + err + exceptionMessage;
        }


        private void DeleteFile(string pathToFile)
        {
            File.Delete(pathToFile);
        }

        private bool CreateFile(string pathToFile, string code)
        {
            bool created = true;

            try
            {
                using (FileStream fs = File.Create(pathToFile))
                {
                    byte[] userCode = new UTF8Encoding(true).GetBytes(code);
                    fs.Write(userCode, 0, userCode.Length);
                }
            }

            catch (Exception ex)
            {
                created = false;
            }

            return created;
        }
    }
}
