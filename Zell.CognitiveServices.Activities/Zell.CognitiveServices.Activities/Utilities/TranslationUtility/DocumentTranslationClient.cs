using CommandLine.ToolKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Utilities;

namespace Utilities
{
    public class DocumentTranslationClient
    {
        static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        
        static string _docTranslatorUtilityPath = $"{AssemblyDirectory}\\Utilities\\DocumentTranslator";
        static string _docTranslatorExe = $"{_docTranslatorUtilityPath}\\DocumentTranslatorCmd.exe";

        public static List<string> TranslateDocument(string fileToBeTranslatedFullPath, string targetLanguage)
        {
            //string fileToBeTranslatedFullPath = @"C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche.docx";
            //string targetLanguage = "de";
            string TranslateDocumentParam = $"translatedocuments /documents:\"{fileToBeTranslatedFullPath}\" /to:{targetLanguage}";
            string SetCredentialsParam = $"setcredentials /apikey:{MicrosoftTranslationClient.ApiKey}";

            //string command = $"/k \"cd /d \"{_docTranslatorUtilityPath}\" && \"{_docTranslatorExe}\" {SetCredentialsParam} && \"{_docTranslatorExe}\" {TranslateDocumentParam}\"";
            string command = $"/c \"cd /d \"{_docTranslatorUtilityPath}\" && \"{_docTranslatorExe}\" {SetCredentialsParam} && \"{_docTranslatorExe}\" {TranslateDocumentParam}\"";
            //Console.WriteLine(command);

            List<string> outputFilesFullPath = new List<string>();
            foreach (var file in fileToBeTranslatedFullPath.Split(",".ToCharArray()))
            {
                if (File.Exists(file))
                {
                    string outputFileName = $"{Path.GetFileNameWithoutExtension(file)}.{targetLanguage}";
                    string outputDirectory = Path.GetDirectoryName(file);
                    outputFilesFullPath.Add($"{outputDirectory}\\{outputFileName}.docx");
                }
            }

            var cmdOutput = "";
            bool isSuccessful = CommandLineToolKit.ValidateAndRunAsync("cmd", out cmdOutput, command);
            if (isSuccessful)
                return outputFilesFullPath;
            else
            {
                if (cmdOutput.ToLower().Contains(MicrosoftTranslationClient.InvalidCredentialsError))
                    throw new System.UnauthorizedAccessException(cmdOutput);
                else
                    throw new System.Exception(cmdOutput);
            }
        }
    }
}