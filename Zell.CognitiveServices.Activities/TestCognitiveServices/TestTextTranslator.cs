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
    /// Test class for Text Translator service
    /// </summary>
    [TestClass]
    public class TestTextTranslator
    {
        #region Method under test
        /// <summary>
        /// Main execution method for activity
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="TargetLanguageCode"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        protected Dictionary<string,string> Execute(string Text, string TargetLanguageCode, string apiKey = "")
        {
            var output = new Dictionary<string, string>();
            var textToTranslate = Text;
            var targetLanguageCode = TargetLanguageCode;
            if (apiKey != "")
            {
                // Sets to the user's apiKey, if supplied; if not, defaults to a free key
                MicrosoftTranslationClient.ApiKey = apiKey;
            }

            try
            {
                var translatedText = MicrosoftTranslationClient.TranslateText(textToTranslate, targetLanguageCode);
                var detectedSourceLanguage = MicrosoftTranslationClient.Detect(textToTranslate);
                output = new Dictionary<string, string>
                {
                    { "Translated Text", translatedText },
                    { "Detected Language", detectedSourceLanguage }
                };
                return output;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(MicrosoftTranslationClient.InvalidApiKeyResolution);
            }
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// Test valid text translation
        /// </summary>
        [TestMethod]
        public void Translate_ValidTextTranslation()
        {
            var text = "how are you?";
            var targetLanguage = "ro";

            var actual = Execute(text, targetLanguage);

            Assert.IsNotNull(actual["Translated Text"], "No translated text returned");
            Assert.IsNotNull(actual["Detected Language"], "No detected language");
        }

        /// <summary>
        /// Test invalid target language
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Translate_InvalidLanguage()
        {
            var text = "sample text";
            var invalidLanguageCode = "jp";
            Execute(text, invalidLanguageCode);
        }

        /// <summary>
        /// Test invalid key
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Translate_InvalidKey()
        {
            var text = "sample text";
            var targetLanguage = "fil";
            Execute(text, targetLanguage, TestDocTranslator.INVALIDKEY);
        }
        #endregion
    }
}
