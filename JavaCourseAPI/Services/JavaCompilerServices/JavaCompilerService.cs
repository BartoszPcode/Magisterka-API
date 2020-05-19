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

namespace JavaCourseAPI.Services
{
    public class JavaCompilerService : IJavaCompilerService
    {
        private readonly IMapper _mapper;

        public JavaCompilerService(IMapper mapper)
        {           
            _mapper = mapper;
        }

        public async Task<ActionResult<string>> JavaCompileAsync(CompileDTO compileDTO)
        {
            string startupPath = Environment.CurrentDirectory;
            string jdkJavac = @"Modules\jdk1.8.0_191\bin\javac";
            string jdkBin = @"Modules\jdk1.8.0_191\bin";
            string jdkJavaExe = @"Modules\jdk1.8.0_191\bin\java.exe";

            //znajduje nazwe klasy w kodzie - nazwa pomiedzy "class " a " " 
            string className = Regex.Match(compileDTO.code, @"class (.+?) ").Groups[1].Value;
            string classNameJava = className + ".java";

            string jdkBinPath = Path.Combine(startupPath, jdkBin);
            string javacPath = Path.Combine(startupPath, jdkJavac);
            string javaExePath = Path.Combine(startupPath, jdkJavaExe);
            string classPath = Path.Combine(jdkBinPath, classNameJava);

            bool wasCreated = this.CreateFile(classPath, compileDTO.code);

            Process compileJavaCodeProcess = new Process();
            compileJavaCodeProcess.StartInfo.FileName = javacPath;
            compileJavaCodeProcess.StartInfo.Arguments = classPath;
            compileJavaCodeProcess.Start();
            compileJavaCodeProcess.WaitForExit();

            string strOutput = "";
            string err = "";
            string exceptionMessage = "";

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = javaExePath;
                p.StartInfo.WorkingDirectory = jdkBinPath;
                p.StartInfo.Arguments = className;

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
                this.DeleteFile(classPath);
                this.DeleteFile(Path.Combine(jdkBinPath, className + ".class"));
            }

            return strOutput + err + exceptionMessage;
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

        private List<CodeSplitedDTO> CountCyclomaticComplexityInJavaCode(List<CodeSplitedDTO> codeSplited)
        {
            int functionCycloamticComplexityCounter = 1;
            int classCyclomaticComplexityCounter = 0;

            if (codeSplited.Count > 0)
            {
                for (int i = 0; i < codeSplited.Count; i++)
                {
                    codeSplited[i].functionsInClassWithoutWhiteSpaces = new List<string>();
                    codeSplited[i].functionsCyclomaticComplexity = new List<int>();

                    for (int j = 0; j < codeSplited[i].functionsInClass.Count; j++)
                    {

                        //usuwa spacje z kodu i inne przerwy
                        string funWithoutWhiteSpaces = codeSplited[i].functionsInClass[j];
                        
                        funWithoutWhiteSpaces = DeleteStringsInFunction(funWithoutWhiteSpaces);
                        funWithoutWhiteSpaces = Regex.Replace(funWithoutWhiteSpaces, @" ", "", RegexOptions.Singleline);
                        funWithoutWhiteSpaces = Regex.Replace(funWithoutWhiteSpaces, @"\r\n", "", RegexOptions.Singleline);
                        //zapisuje funkcje bez pustych znaków
                        codeSplited[i].functionsInClassWithoutWhiteSpaces.Add(funWithoutWhiteSpaces);

                        var ifStatements = CountAllIfs(funWithoutWhiteSpaces);

                        if (ifStatements.Count > 0)
                        {
                            functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + ifStatements.Count;

                            for (int x = 0; x < ifStatements.Count; x++)
                            {
                                var andInIfStatement = Regex.Matches(ifStatements[x].ToString(), @"&&");
                                functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + andInIfStatement.Count;

                                var orInIfStatement = Regex.Matches(ifStatements[x].ToString(), @"\|\|");
                                functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + orInIfStatement.Count;
                            }
                        }

                        //znajduje wsyzstkie petle for
                        var forLoops = Regex.Matches(funWithoutWhiteSpaces, @"for\((.+?)\)");
                        functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + forLoops.Count;

                        //znajduje wsyzstkie petle while
                        var whileLoops = Regex.Matches(funWithoutWhiteSpaces, @"while\((.+?)\)");
                        functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + whileLoops.Count;

                        //znajduje wsyzstkie wystapienia catch
                        var catchStatements = Regex.Matches(funWithoutWhiteSpaces, @"catch\((.+?)\)");
                        functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + catchStatements.Count;

                        //znajduje wsyzstkie wystapienia case
                        var caseStatements = Regex.Matches(funWithoutWhiteSpaces, @"case (\d+)");
                        functionCycloamticComplexityCounter = functionCycloamticComplexityCounter + caseStatements.Count;

                        //zapisuje złożoność cyklometryczną konkretnej funkcji w klasie
                        codeSplited[i].functionsCyclomaticComplexity.Add(functionCycloamticComplexityCounter);
                        classCyclomaticComplexityCounter = classCyclomaticComplexityCounter + functionCycloamticComplexityCounter;
                        functionCycloamticComplexityCounter = 1;
                    }

                    //zapisuje złożoność cyklometryczną klasy (jeśli nie miala zadnych funkcji to klasa defaultowo ma złożoność 1)
                    if (classCyclomaticComplexityCounter == 0)
                    {
                        classCyclomaticComplexityCounter = 1;
                    }

                    codeSplited[i].classCyclomaticComplexity = classCyclomaticComplexityCounter;
                    classCyclomaticComplexityCounter = 0;
                }
            }
            return codeSplited;
        }

        private List<string> CountAllIfs(string funWithoutWhiteSpaces)
        {
            StringBuilder ifSearching = new StringBuilder();
            StringBuilder ifStatement = new StringBuilder();
            int bracketCounter = 0;
            bool foundIf = false;

            List<string> foundIfs = new List<string>();

            foreach (char sign in funWithoutWhiteSpaces)
            {
                if (foundIf)
                {
                    if (sign == '(')
                    {
                        bracketCounter++;
                    } else if (sign == ')')
                    {
                        bracketCounter--;
                    }

                    //koniec szukanego ifa
                    if (bracketCounter == 0)
                    {
                        foundIfs.Add(ifStatement.ToString());
                        ifStatement.Clear();
                        foundIf = false;
                        ifSearching = ifSearching.Remove(0, 1);
                    }
                    else
                    {
                        ifStatement.Append(sign);
                    }

                }
                else
                {
                    ifSearching.Append(sign);

                    if ( ifSearching.Equals("if(") )
                    {
                        bracketCounter++;
                        foundIf = true;
                    }
                    else if (!ifSearching.Equals( "if(" ) && ifSearching.Length == 3)
                    {
                        ifSearching = ifSearching.Remove(0, 1);
                    }
                }
            }
            return foundIfs;
        }
        public async Task<List<CodeAnalyzeInfoDTO>> JavaCyclomaticComplexity(CompileDTO compileDTO)
        {   /*
                \n = CR (Carriage Return) // Used as a new line character in Unix
                \r = LF (Line Feed) // Used as a new line character in Mac OS
                \r\n =  LF + CR // Used as a new line character in Windows
                (char)13 = \n = CR // Same as \n
                nowa linia ze strony przychodzi jako \n a z postmana jako \r\n
            */

            //usuwa blokowe komentarze
            var code = Regex.Replace(compileDTO.code, @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/", "", RegexOptions.Singleline);

            //usuwa liniowe komentarze
            code = Regex.Replace(code, @"//(.+?)\n", "", RegexOptions.Singleline);

            //usuwa wszystkie nowe linie i tabulatory 
            code = Regex.Replace(code, @"\n", "", RegexOptions.Singleline);
            code = Regex.Replace(code, @"\t", "", RegexOptions.Singleline);
            code = Regex.Replace(code, @"\r", "", RegexOptions.Singleline);

            //dziele na funkcje zanim usune spacje z kodu
            var splitedCode = SearchForFunctions(code);

            splitedCode = CountCyclomaticComplexityInJavaCode(splitedCode);
            splitedCode = ClearClassAndFunctionsNameToDisplay(splitedCode);

            // return await Task.Run(() => _mapper.Map<List<CodeAnalyzeInfoDTO>>(splitedCode));

            return  _mapper.Map<List<CodeAnalyzeInfoDTO>>(splitedCode);
        }
     
        private string DeleteStringsInFunction(string funWithoutWhiteSpaces)
        {
            StringBuilder newFunWithoutWhiteSpaces = new StringBuilder();
            if (funWithoutWhiteSpaces.Length >= 4)
            {              
                int counter = 0;
                bool startFlag = false;
                bool stringEndedFlag = false;

                char firstChar;
                char secondChar;
                char beforeLast;
                char lastChar;

                newFunWithoutWhiteSpaces.Clear();
                newFunWithoutWhiteSpaces.Append(funWithoutWhiteSpaces.Substring(0, 2));

                for (int i = 2; i < funWithoutWhiteSpaces.Length - 2; i++)
                {
                    char sign = funWithoutWhiteSpaces.ElementAt(i);

                    if (counter + 4 > funWithoutWhiteSpaces.Length)
                    {
                        return newFunWithoutWhiteSpaces.ToString();
                    }
                    
                    beforeLast = funWithoutWhiteSpaces.ElementAt(counter + 3);
                    lastChar = funWithoutWhiteSpaces.ElementAt(counter + 4);

                    if(startFlag == false)
                    {
                        firstChar = funWithoutWhiteSpaces.ElementAt(counter);
                        secondChar = funWithoutWhiteSpaces.ElementAt(counter + 1);

                        if (stringEndedFlag == true)
                        {
                            stringEndedFlag = false;
                            newFunWithoutWhiteSpaces.Append(firstChar);
                            newFunWithoutWhiteSpaces.Append(secondChar);
                        }

                        if (firstChar != '\\' && secondChar == '"')
                        {
                            startFlag = true;
                        }
                        else
                        {
                            newFunWithoutWhiteSpaces.Append(sign);
                        }
                    }
                    else
                    {
                        if (beforeLast != '\\' && lastChar == '"')
                        {
                            if (beforeLast == '\\')
                            {
                                newFunWithoutWhiteSpaces.Append(beforeLast);
                                newFunWithoutWhiteSpaces.Append(lastChar);
                            }
                            else
                            {
                                newFunWithoutWhiteSpaces.Append(lastChar);
                            }
                            
                            i = i + 4;
                            counter = counter + 4;
                            startFlag = false;
                            stringEndedFlag = true;
                        }
                    }

                    counter++;
                }
                newFunWithoutWhiteSpaces.Append( funWithoutWhiteSpaces.Substring(funWithoutWhiteSpaces.Length - 3, 3) );
            }
            
            return newFunWithoutWhiteSpaces.ToString();
        }

        private List<CodeSplitedDTO> ClearClassAndFunctionsNameToDisplay(List<CodeSplitedDTO> splitedCode)
        {
            if (splitedCode.Count > 0)
            {
                for (int i = 0; i < splitedCode.Count; i++)
                { 
                    //usuwa ostatni znak w nazwie klasy - czyli {
                    splitedCode[i].classInCode = splitedCode[i].classInCode.Remove(splitedCode[i].classInCode.Length - 1);

                    if (i != 0)
                    {
                        //usuwa pierwszy znak w nazwie klasy oprocz klasy nr 1 czyli znak }
                        int signCounter = 0;

                        foreach ( char sign in splitedCode[i].classInCode)
                        {
                            signCounter++;
                            if (sign == '}')
                            {
                                splitedCode[i].classInCode = splitedCode[i].classInCode.Remove(0, signCounter);
                                break;
                            }
                        }
                    }

                    if (splitedCode[i].functionsInClass.Count > 0)
                    {
                        List<string> functionsNameToDisplay = new List<string>();

                        for (int j = 0; j < splitedCode[i].functionsInClass.Count; j++)
                        {
                            StringBuilder functionToDisplay = new StringBuilder();
                            bool letterAppeardFlag = false;
                            
                            foreach (char sign in splitedCode[i].functionsInClass[j])
                            {
                                if (letterAppeardFlag == true)
                                {
                                    if (sign != '{')
                                    {
                                        functionToDisplay.Append(sign);
                                    }
                                    else
                                    {
                                        functionsNameToDisplay.Add( functionToDisplay.ToString() );
                                        break;
                                    }
                                }
                                else
                                {
                                    if (sign != (char)32)
                                    {
                                        letterAppeardFlag = true;
                                        functionToDisplay.Append(sign);
                                    }
                                }
                            }                            
                        }
                        splitedCode[i].functionsInClassToDisplay = functionsNameToDisplay;
                    }
                }
            }

            return splitedCode;
        }

        private List<CodeSplitedDTO> SearchForFunctions(string code)
        {
            int counter = 0;
            bool startFlag = false;
            StringBuilder partCode = new StringBuilder();
            List<CodeSplitedDTO> functions = new List<CodeSplitedDTO>();

            foreach(char sign in code)
            {
                partCode.Append(sign);

                if( sign == '{')
                {
                    startFlag = true;
                    counter++;
                }else if( sign == '}')
                {
                    counter--;
                }

                if(counter == 1 && startFlag == true)
                {
                    //sprawdza czy znaleziono klase czy funkcje
                    //jeśli na końcu jest '{' to klase a jeśli '}' to funkcja
                    if(sign == '{')
                    {
                        //jeśli znaleziono nową klase to tworzy obiekt w liście z nagłówkiem tej klasy.
                        //W liście będą przechowywane funkcję tej klasy
                        CodeSplitedDTO csDTO = new CodeSplitedDTO();
                        csDTO.classInCode = partCode.ToString();
                        csDTO.functionsInClass = new List<string>();
                        functions.Add(csDTO);
                        partCode.Clear();
                        startFlag = false;
                    }
                    else
                    {
                        //dodaje funkcję do listy w danej klasie 
                        functions[functions.Count - 1].functionsInClass.Add(partCode.ToString());
                        partCode.Clear();
                        startFlag = false;
                    }                   
                }
            }

            return functions;
        }     

        private void DeleteFile(string pathToFile)
        {
            File.Delete(pathToFile);
        }
    }
}
