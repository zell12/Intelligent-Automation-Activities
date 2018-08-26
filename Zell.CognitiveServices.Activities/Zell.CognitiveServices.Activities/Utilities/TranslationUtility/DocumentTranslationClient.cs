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
    /// <summary>
    /// Helper class for Document Translation cognitive services
    /// </summary>
    public class DocumentTranslationClient
    {
        #region Properties
        /// <summary>
        /// Assembly path
        /// </summary>
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
        #endregion

        #region Fields
        /// <summary>
        /// Path to document translator utilities
        /// </summary>
        static string _docTranslatorUtilityPath = $"{AssemblyDirectory}\\Utilities\\DocumentTranslator";

        /// <summary>
        /// Path to document translator
        /// </summary>
        static string _docTranslatorExe = $"{_docTranslatorUtilityPath}\\DocumentTranslatorCmd.exe";
        #endregion

        #region Public Methods
        /// <summary>
        /// Main method for document translation activity
        /// </summary>
        /// <param name="fileToBeTranslatedFullPath"></param>
        /// <param name="targetLanguage"></param>
        /// <returns></returns>
        public static List<string> TranslateDocument(string fileToBeTranslatedFullPath, string targetLanguage)
        {
            string TranslateDocumentParam = $"translatedocuments /documents:\"{fileToBeTranslatedFullPath}\" /to:{targetLanguage}";
            string SetCredentialsParam = $"setcredentials /apikey:{MicrosoftTranslationClient.ApiKey}";

            string command = $"/c \"cd /d \"{_docTranslatorUtilityPath}\" && \"{_docTranslatorExe}\" {SetCredentialsParam} && \"{_docTranslatorExe}\" {TranslateDocumentParam}\"";

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
        #endregion
    }
}