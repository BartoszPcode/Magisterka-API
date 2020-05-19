using AutoMapper;
using JavaCourseAPI.Services.TestCodeServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using JavaCourseAPI.DTOs;

namespace JavaCourseAPI.Services.TestCodeService
{
    public class TestCodeService : ITestCodeService
    {
        private readonly IMapper _mapper;

        public TestCodeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<TestCodeSendInfoDTO> getCodeInformations(string javaCode)
        {
            string startupPath = Environment.CurrentDirectory;
            //znajduje nazwe klasy w kodzie - nazwa pomiedzy "class " a " " 
            string className = Regex.Match(javaCode, @"class (.+?) ").Groups[1].Value;
            string classNameJava = className + ".java";
            string classPath = Path.Combine(startupPath, @"Files\" + classNameJava);
            bool wasCreated = this.CreateFile(classPath, javaCode);

            string base64ImageRepresentation = generateGraph(classPath, className);
            generateInformationFiles(classPath);
            CodeInfoFromCSVFilesDTO codeInfoFromCSV = readFromCSVFiles();
            TestCodeAllInfoDTO testCodeAllInfoDTO = new TestCodeAllInfoDTO(codeInfoFromCSV.classesInfo, 
                                                  codeInfoFromCSV.methodesInfo);

            string filesDirPath = Path.Combine(startupPath, @"Files\");
            List<CodeTestClassInfoDTO> codeTestClassInfo = mapAllInformations(testCodeAllInfoDTO);

            TestCodeSendInfoDTO testCodeSendInfo = new TestCodeSendInfoDTO(base64ImageRepresentation, codeTestClassInfo);
            //usunięcie wygenerowanych plików
            this.DeleteFile(classPath);
            this.DeleteFile(Path.Combine(filesDirPath, @"class.csv"));
            this.DeleteFile(Path.Combine(filesDirPath, @"method.csv"));
            this.DeleteFile(Path.Combine(filesDirPath, @"progex.log"));
            this.DeleteFile(Path.Combine(filesDirPath, className + @"-CFG.dot"));
            this.DeleteFile(Path.Combine(filesDirPath, className + @"Graph.png"));
            
            return testCodeSendInfo;
        }

        public async Task<TestCodeAllInfoDTO> GenerateAllInfo(string javaCode)
        {
            string startupPath = Environment.CurrentDirectory;
            //znajduje nazwe klasy w kodzie - nazwa pomiedzy "class " a " " 
            string className = Regex.Match(javaCode, @"class (.+?) ").Groups[1].Value;
            string classNameJava = className + ".java";
            string classPath = Path.Combine(startupPath, @"Files\" + classNameJava);
            bool wasCreated = this.CreateFile(classPath, javaCode);

            string base64ImageRepresentation = generateGraph(classPath, className);
            generateInformationFiles(classPath);
            CodeInfoFromCSVFilesDTO codeInfoFromCSV = readFromCSVFiles();
            TestCodeAllInfoDTO testCodeAllInfoDTO = new TestCodeAllInfoDTO(codeInfoFromCSV.classesInfo,
                                                  codeInfoFromCSV.methodesInfo);

            string filesDirPath = Path.Combine(startupPath, @"Files\");

            //usunięcie wygenerowanych plików
            this.DeleteFile(classPath);
            this.DeleteFile(Path.Combine(filesDirPath, @"class.csv"));
            this.DeleteFile(Path.Combine(filesDirPath, @"method.csv"));
            this.DeleteFile(Path.Combine(filesDirPath, @"progex.log"));
            this.DeleteFile(Path.Combine(filesDirPath, className + @"-CFG.dot"));
            this.DeleteFile(Path.Combine(filesDirPath, className + @"Graph.png"));

            return testCodeAllInfoDTO;
        }

        private List<CodeTestClassInfoDTO> mapAllInformations(TestCodeAllInfoDTO testCodeAllInfoDTO)
        {
            List<CodeTestClassInfoDTO> codeTestClassInfoList = new List<CodeTestClassInfoDTO>();

            for (int i = 1; i < testCodeAllInfoDTO.classesInfo.Count; i++)
            {
                CodeTestClassInfoDTO codeTestClassInfo = new CodeTestClassInfoDTO();
                //wczytanie info o klasie
                for (int j = 1; j < testCodeAllInfoDTO.classesInfo[i].Count(); j++) {
                   
                    SingleInformationDTO singleClassInfo = new SingleInformationDTO(testCodeAllInfoDTO.classesInfo[0][j], testCodeAllInfoDTO.classesInfo[i][j]);
                    codeTestClassInfo.classInformations.Add(singleClassInfo);
                }

                //wczytanie info o metodach klasy
                if (testCodeAllInfoDTO.methodesInfo.Count > 1)
                {
                    for (int x = 1; x < testCodeAllInfoDTO.methodesInfo.Count; x++)
                    {
                        if (testCodeAllInfoDTO.methodesInfo[x][1] == codeTestClassInfo.classInformations[0].parameterValue)
                        {
                            CodeTestMethodeInfoDTO codeTestMethodeInfoDTO = new CodeTestMethodeInfoDTO();
                            for (int y = 1; y < testCodeAllInfoDTO.methodesInfo[x].Count(); y++)
                            {
                                SingleInformationDTO singleMethodInfo = new SingleInformationDTO(testCodeAllInfoDTO.methodesInfo[0][y], testCodeAllInfoDTO.methodesInfo[x][y]);
                               
                                codeTestMethodeInfoDTO.methodeInformations.Add(singleMethodInfo);
                            }

                            //zmienia postać nazwy funkcji i jej parametrów
                            string methodeNameWithParameters = codeTestMethodeInfoDTO.methodeInformations[1].parameterValue;
                            var caseStatements = Regex.Matches(methodeNameWithParameters, @"/(\d+)\[");
                            methodeNameWithParameters = Regex.Replace(methodeNameWithParameters, @"/(\d+)\[", " (", RegexOptions.Singleline);
                            methodeNameWithParameters = Regex.Replace(methodeNameWithParameters, "\"", "", RegexOptions.Singleline);
                            methodeNameWithParameters = methodeNameWithParameters.Remove(methodeNameWithParameters.Length - 1, 1) + ")";
                            methodeNameWithParameters = Regex.Replace(methodeNameWithParameters, ",", ", ", RegexOptions.Singleline);
                            methodeNameWithParameters = Regex.Replace(methodeNameWithParameters, ";", ", ", RegexOptions.Singleline);
                           
                            codeTestMethodeInfoDTO.methodeInformations[1].parameterValue = methodeNameWithParameters;

                            codeTestClassInfo.methodesInformations.Add(codeTestMethodeInfoDTO);

                            
                        }
                    }
                }
                codeTestClassInfoList.Add(codeTestClassInfo);
            }

            return codeTestClassInfoList;
        }

        public void generateInformationFiles(string fileWithCodePath)
        {
            string startupPath = Environment.CurrentDirectory;
            string ckPath = Path.Combine(startupPath, @"Modules\CK-calculator\ck-0.4.5.jar");
            string javaExePath = Path.Combine(startupPath, @"Modules\jdk1.8.0_191\bin\java.exe");
            string filesDirPath = Path.Combine(startupPath, @"Files");

            string strOutput = "";
            string err = "";
            string exceptionMessage = "";

            //java -jar ck-0.4.5-SNAPSHOT-jar-with-dependencies.jar "D:\WAT\magisterka\nowe\Modules\test\classes" false 0   
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = javaExePath;
                p.StartInfo.WorkingDirectory = filesDirPath;
                p.StartInfo.Arguments = @" -jar " + ckPath + " " + filesDirPath + " false 0 false";

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;

                p.OutputDataReceived += (a, b) => strOutput = strOutput + "\n" + b.Data;
                p.ErrorDataReceived += (a, b) => err = err + "\n" + b.Data;

                p.Start();

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                p.WaitForExit(3000);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }
        }

        private CodeInfoFromCSVFilesDTO readFromCSVFiles()
        {
            string startupPath = Environment.CurrentDirectory;
            string classesPath = Path.Combine(startupPath, @"Files\class.csv");
            string methodsPath = Path.Combine(startupPath, @"Files\method.csv");

            List<string[]> classesInfo = new List<string[]>();
            var classesInfoLines = File.ReadLines(classesPath);

            foreach (string line in classesInfoLines)
            {
                if (line.Length > 0)
                {
                    classesInfo.Add(line.Split(","));
                }
            }

            List<string[]> methodesInfo = new List<string[]>();
            var methodesInfoLines = File.ReadLines(methodsPath);

            foreach (string line in methodesInfoLines)
            {
                if (line.Length > 0)
                {                 
                    //sprawdza czy są wyrażenia " "
                    //code = Regex.Replace(code, @"//(.+?)\n", "", RegexOptions.Singleline);
                    var textToReplace = Regex.Match(line, "\"(.+?)\"");
                    if (textToReplace.Value.Length > 0)
                    {
                        string textAfterReplace = textToReplace.Value.Replace(',', ';');
                        string lineAfterReplace = line.Replace(textToReplace.Value, textAfterReplace);
                        methodesInfo.Add(lineAfterReplace.Split(","));
                    }
                    else
                    {
                        methodesInfo.Add(line.Split(","));
                    }                                   
                }
            }

            CodeInfoFromCSVFilesDTO codeInfo = new CodeInfoFromCSVFilesDTO();
            codeInfo.classesInfo = classesInfo;
            codeInfo.methodesInfo = methodesInfo;

            return codeInfo;
        }

        private string generatePngFromDot(string fileName)
        {
            string dotFileName = fileName + "-CFG.dot";
            string startupPath = Environment.CurrentDirectory;
            string dotExePath = Path.Combine(startupPath, @"Modules\Graphviz2.38\bin\dot.exe");
            string dotFilePath = Path.Combine(startupPath, @"Files\" + dotFileName);

            string strOutput = "";
            string err = "";
            string exceptionMessage = "";
            
            //dot -Tpng -o graph.png Hello-CFG.dot
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = dotExePath;
                p.StartInfo.WorkingDirectory = Path.Combine(startupPath, @"Files");
                p.StartInfo.Arguments = /* /C */ @"  -Tpng -o  " + fileName + "Graph.png " + dotFilePath;

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;

                p.OutputDataReceived += (a, b) => strOutput = strOutput + "\n" + b.Data;
                p.ErrorDataReceived += (a, b) => err = err + "\n" + b.Data;

                p.Start();

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                p.WaitForExit(3000);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            string imagePath = Path.Combine(startupPath, @"Files\" + fileName + "Graph.png");
            return imagePath;
        }

        private string generateBase64StringOfGraphImage(string imagePath)
        {
            byte[] imageArray = File.ReadAllBytes(imagePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        public string generateGraph(string classPath, string className)
        {
            string startupPath = Environment.CurrentDirectory;
            string progexPath = Path.Combine(startupPath, @"Modules\progex-v3.4.5\progex.jar");
            string jdkJavaExe = @"Modules\jdk1.8.0_191\bin\java.exe";

            string javaExePath = Path.Combine(startupPath, jdkJavaExe);         

            string strOutput = "";
            string err = "";
            string exceptionMessage = "";

            //java -jar D:/WAT/magisterka/progex-v3.4.5/progex-v3.4.5/PROGEX.jar -cfg -lang java -format dot  D:/WAT/magisterka/progex-v3.4.5/progex-v3.4.5/Hello.java  
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = javaExePath;
                p.StartInfo.WorkingDirectory = Path.Combine(startupPath, @"Files");
                p.StartInfo.Arguments = @" -jar " + progexPath + " -cfg -lang java -format dot " + classPath;

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;

                p.OutputDataReceived += (a, b) => strOutput = strOutput + "\n" + b.Data;
                p.ErrorDataReceived += (a, b) => err = err + "\n" + b.Data;

                p.Start();

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                p.WaitForExit(3000);
            }
            catch (Exception e)
            {
                exceptionMessage = e.Message;
            }

            string imagePath = generatePngFromDot(className);
            return generateBase64StringOfGraphImage(imagePath);
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

        private void DeleteFile(string pathToFile)
        {
            File.Delete(pathToFile);
        }
    }
}
