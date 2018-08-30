using System.Activities;
using System.ComponentModel;
using Utilities;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Zell.CognitiveServices
{
    /// <summary>
    /// Activity class for Document Translator cognitive services
    /// </summary>
    [Description("The Microsoft Document Translator translates Microsoft Office, plain text, HTML, PDF files and SRT caption files, from and to any of the 60+ languages supported by the Microsoft Translator web service.")]
    [ToolboxBitmap(typeof(DocumentTranslator), "doctranslator-icon.png")]
    public class DocumentTranslator : CodeActivity
    {
        #region Public Properties
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("File/s")]
        [Description("Specify full path of document/s to translate (pdf, docx, txt, html)")]
        public InArgument<string> Document { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Target Language ISO Code")]
        [Description("Translation target ISO Language code e.g.fil, en, pl, ja, th, etc")]
        [DefaultValue("\"en\"")]
        public InArgument<string> TargetLanguageCode { get; set; }

        [Category("Input")]
        [DisplayName("API Key")]
        [Description("Defaults to a free key if not supplied. Recommended to register your own key in Azure")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Output")]
        [DisplayName("Translated file/s")]
        [Description("This is the translated document")]
        public OutArgument<List<string>> TranslatedDoc { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Main activity method
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            var inputFileSelection = Document.Get(context);
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

            var targetLanguageCode = TargetLanguageCode.Get(context);
            var apiKey = ApiKey.Get(context);
            if (apiKey != null && apiKey != "")
            {
                // Sets to the user's apiKey, if supplied; if not, defaults to a free key
                MicrosoftTranslationClient.ApiKey = apiKey;
            }
            try
            {
                translatedFiles = DocumentTranslationClient.TranslateDocument(String.Join(",", filesForTranslation), targetLanguageCode);
                TranslatedDoc.Set(context, translatedFiles);
            }
            catch (System.UnauthorizedAccessException uex)
            {
                throw new System.UnauthorizedAccessException($"FULL OUTPUT: {uex.Message}\n{MicrosoftTranslationClient.InvalidApiKeyResolution}");
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        #endregion
    }
}