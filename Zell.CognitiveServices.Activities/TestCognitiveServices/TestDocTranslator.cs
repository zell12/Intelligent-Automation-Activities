using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using Zell.CognitiveServices;
using Zell.CognitiveServices.Activities.Utilities.ContentModeration.Models;

namespace TestCognitiveServices
{
    /// <summary>
    /// Test class for Document Translator service
    /// </summary>
    [TestClass]
    public class TestDocTranslator
    {
        #region Constants
        /// <summary>
        /// Invalid API key
        /// </summary>
        public const string INVALIDKEY = "eb54b140e6224107885efc361e02e70";
        #endregion

        #region Method under test
        /// <summary>
        /// Main execution method for activity
        /// </summary>
        /// <param name="Document"></param>
        /// <param name="TargetLanguageCode"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        protected List<string> Execute(string Document, string TargetLanguageCode, string apiKey = "")
        {
            var inputFileSelection = Document;
            string[] files = inputFileSelection.Split("|".ToCharArray());
            List<string> filesForTranslation = new List<string>();
            List<string> translatedFiles = new List<string>();

            if (files.Length == 1)
            {
                if (!File.Exists(files[0]))
                {
                    // Check if only one file is selected and if it exists
                    throw new System.IO.FileNotFoundException("The document specified is not found. Make sure the file exists or full path is typed correctly.");
                }
                else
                    filesForTranslation.Add(files[0]);
            }
            else
            {
                foreach (var selectedFile in files)
                {
                    if (File.Exists(selectedFile))
                        filesForTranslation.Add(selectedFile);
                }

                if (filesForTranslation.Count == 0)
                {
                    // All files specified are not found.
                    throw new System.IO.FileNotFoundException("All files specified are not found. Make sure the file exists or full path is typed correctly.");
                }

            }

            if (apiKey != "")
            {
                // Sets to the user's apiKey, if supplied; if not, defaults to a free key
                MicrosoftTranslationClient.ApiKey = apiKey;
            }
            var targetLanguageCode = TargetLanguageCode;

            try
            {
                translatedFiles = DocumentTranslationClient.TranslateDocument(String.Join(",", filesForTranslation), targetLanguageCode);
                return translatedFiles;
            }
            catch (System.UnauthorizedAccessException uex)
            {
                Debug.WriteLine(MicrosoftTranslationClient.InvalidApiKeyResolution);
                throw new System.UnauthorizedAccessException(MicrosoftTranslationClient.InvalidApiKeyResolution);
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine(ex.Message);
                throw new System.Exception(ex.Message);
            }
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// Test valid single file
        /// </summary>
        [TestMethod]
        public void Translate_ValidSingleFile()
        {
            var file = @"C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche.docx";
            var targetLanguage = "ro";
            string outputFileName = $"{Path.GetFileNameWithoutExtension(file)}.{targetLanguage}";
            string outputDirectory = Path.GetDirectoryName(file);
            var expectedOutput = $"{outputDirectory}\\{outputFileName}.docx";
            List<string> actual = Execute(file, targetLanguage);

            Assert.AreEqual(expectedOutput, actual[0], "Error in single file translation. ");
        }

        /// <summary>
        /// Test valid multiple files
        /// </summary>
        [TestMethod]
        public void Translate_ValidMultipleFiles()
        {
            var files = @"C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche.docx|C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche - Copy.docx";
            var targetLanguage = "ja";
            List<string> outputList = new List<string>();
            string expectedOutput = "";
            foreach (var file in files.Split("|".ToCharArray()))
            {
                if (File.Exists(file))
                {
                    string outputFileName = $"{Path.GetFileNameWithoutExtension(file)}.{targetLanguage}";
                    string outputDirectory = Path.GetDirectoryName(file);
                    outputList.Add($"{outputDirectory}\\{outputFileName}.docx");
                }
            }
            expectedOutput = String.Join(",", outputList);

            List<string> actual = Execute(files, targetLanguage);

            Assert.AreEqual(expectedOutput, $"{actual[0]},{actual[1]}", "Error in multiple file translation. ");
        }

        /// <summary>
        /// Test multiple files - one valid, one invalid
        /// </summary>
        [TestMethod]
        public void Translate_MultipleFiles_ValidAndInvalid()
        {
            var files = @"c:\path\to\invalid file|C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche - Copy.docx";
            var targetLanguage = "fil";
            List<string> outputList = new List<string>();
            string expectedOutput = "";
            foreach (var file in files.Split("|".ToCharArray()))
            {
                if(File.Exists(file))
                {
                    string outputFileName = $"{Path.GetFileNameWithoutExtension(file)}.{targetLanguage}";
                    string outputDirectory = Path.GetDirectoryName(file);
                    outputList.Add($"{outputDirectory}\\{outputFileName}.docx");
                }
            }
            expectedOutput = String.Join(",", outputList);

            List<string> actual = Execute(files, targetLanguage);

            Assert.AreEqual(expectedOutput, $"{actual[0]}", "Error in multiple (valid/invalid) file translation. ");
        }

        /// <summary>
        /// Test invalid single file
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void Translate_InvalidSingleFile()
        {
            List<string> actual = Execute("INVALID FILE", "fil");
        }

        /// <summary>
        /// Test invalid multiple files
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void Translate_InvalidMultipleFiles()
        {
            List<string> actual = Execute(@"c:\path\to\invalid file|c:\path\to\invalid file 2", "fil");
        }

        /// <summary>
        /// Test invalid language
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Translate_InvalidLanguage()
        {
            var file = @"C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche.docx";
            var targetLanguage = "jp - INVALID LANGUAGE CODE";
            Execute(file, targetLanguage);
        }

        /// <summary>
        /// Test invalid key
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.UnauthorizedAccessException))]
        public void Translate_InvalidKey()
        {
            var file = @"C:\TEMP\testDocTranslation\3M Enterprise Chatbot - Main - SnPA_RusselAlfeche.docx";
            var targetLanguage = "fil";
            Execute(file, targetLanguage, INVALIDKEY);
        }
        #endregion
    }
}
