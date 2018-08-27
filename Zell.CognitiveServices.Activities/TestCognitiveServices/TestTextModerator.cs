using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using Zell.CognitiveServices;
using Zell.CognitiveServices.Activities.Utilities.ContentModeration.Models;

namespace TestCognitiveServices
{
    /// <summary>
    /// Test class for Text Moderator service
    /// </summary>
    [TestClass]
    public class TestTextModerator
    {
        #region Method under test
        /// <summary>
        /// Main execution method for activity
        /// </summary>
        /// <param name="context"></param>
        protected TextEvaluationOutputModel Execute(string Text, string apiKey = "")
        {
            TextEvaluationOutputModel _contentModeratorOutput = new TextEvaluationOutputModel();
            var textInput = Text;
            textInput = textInput.Replace(System.Environment.NewLine, " ");
            if (apiKey != "")
            {
                // Sets to the user's apiKey, if supplied; if not, defaults to a free key
                ContentModeratorHelper.ApiKey = apiKey;
            }

            try
            {
                // Screen the input text: check for profanity, classify the text into three categories
                // do autocorrect text, and check for personally identifying 
                // information (PII)
                _contentModeratorOutput = ContentModeratorHelper.GetTextEvaluationOutput(textInput);
                if (_contentModeratorOutput == null)
                {
                    throw new System.Exception("Null Output");
                }
                else
                {
                    return _contentModeratorOutput;
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine(ex.Message);
                throw new System.Exception($"Actual Exception Message: {ex.Message}\n{TextModerator.GeneralException}");
            }
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// Test - detailed input
        /// </summary>
        [TestMethod]
        public void TextEvaluation_DetailedInput_UtilizedAllOutputFields()
        {
            var actual = Execute(@"Is this a grabage or crap email abcdef@abcd.com, phone: 6657789887, IP: 255.255.255.255, 1 Microsoft Way, Redmond, WA 98052.
                                These are all UK phone numbers, the last two being Microsoft UK support numbers: +44 870 608 4000 or 0344 800 2400 or
                                0800 820 3300. Also, 999-99-9999 looks like a social security number (SSN).");
            Assert.IsNotNull(actual.AutoCorrectedText, "No result for auto-corrected text");
            Assert.IsNotNull(actual.FriendlyOutput, "No result for friendly output");
            Assert.IsNotNull(actual.JsonOutput, "No result for json output");
            Assert.IsNotNull(actual.NormalizedText, "No result for normalized text");
            Assert.IsNotNull(actual.ProfanityCategory1Score, "No profanity category 1 classification");
            Assert.IsNotNull(actual.ProfanityCategory2Score, "No profanity category 2 classification");
            Assert.IsNotNull(actual.ProfanityCategory3Score, "No profanity category 3 classification");
            Assert.IsNotNull(actual.ReviewRecommended, "No review recommendation");
        }

        /// <summary>
        /// Test - Other sample input
        /// </summary>
        [TestMethod]
        public void TextEvaluation_Sample2()
        {
            var actual = Execute(@"You are an asshole.");
            Assert.IsNotNull(actual.AutoCorrectedText, "No result for auto-corrected text");
            Assert.IsNotNull(actual.FriendlyOutput, "No result for friendly output");
            Assert.IsNotNull(actual.JsonOutput, "No result for json output");
            Assert.IsNotNull(actual.NormalizedText, "No result for normalized text");
            Assert.IsNotNull(actual.ProfanityCategory1Score, "No profanity category 1 classification");
            Assert.IsNotNull(actual.ProfanityCategory2Score, "No profanity category 2 classification");
            Assert.IsNotNull(actual.ProfanityCategory3Score, "No profanity category 3 classification");
            Assert.IsNotNull(actual.ReviewRecommended, "No review recommendation");
        }

        /// <summary>
        /// Test - less detailed input
        /// </summary>
        [TestMethod]
        public void TextEvaluation_LessDetailedInput_ExceptionNotEncounteredForNullFields()
        {
            var actual = Execute(@"Gago ka (!!profane language in filipino!!)");
            Assert.IsNotNull(actual.FriendlyOutput, "No result for friendly output");
            Assert.IsNotNull(actual.JsonOutput, "No result for json output");
            Assert.IsNotNull(actual.NormalizedText, "No result for normalized text");
            Assert.IsNull(actual.ProfanityCategory1Score);
            Assert.IsNull(actual.ProfanityCategory2Score);
            Assert.IsNull(actual.ProfanityCategory3Score);
            Assert.IsNull(actual.ReviewRecommended);
            Assert.IsNull(actual.AutoCorrectedText);
        }

        /// <summary>
        /// Test exception encountered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void TextEvaluation_WhenInvalidKey_ShouldThrowException()
        {
            var actual = Execute("Test text", "INVALID API KEY");
        }
        #endregion
    }
}
