using Microsoft.CognitiveServices.ContentModerator;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Zell.CognitiveServices.Activities.Utilities.ContentModeration.Models;

namespace Zell.CognitiveServices
{
    /// <summary>
    /// Activity class for Text Moderator cognitive services
    /// </summary>
    [Description("Helps you detect potential profanity in more than 100 languages and match text against your custom lists automatically. Content Moderator also checks for possible personally identifiable information (PII). This comes with a free text moderator api key for development purposes. If for extensive use, recommended to generate a personal subscription key.")]
    [ToolboxBitmap(typeof(TextModerator), "textmoderator-icon.png")]
    public class TextModerator : CodeActivity
    {
        #region Fields and Constants
        /// <summary>
        /// Exception string for invald keys
        /// </summary>
        public const string GeneralException = "An error is encountered. Your subscription key might have expired, invalid or hit a governing limit. Follow the steps below:\n(1) Subscribe and generate a content API key in Azure.\n(2) Paste the subscription key generated to ApiKey field.";

        /// <summary>
        /// Output from text evaluation
        /// </summary>
        private TextEvaluationOutputModel _contentModeratorOutput = new TextEvaluationOutputModel();
        #endregion

        #region Public Properties
        [Category("Input")]
        [RequiredArgument]
        [Description("The text to evaluate.")]
        public InArgument<string> Text { get; set; }

        [Category("Input")]
        [Description("Defaults to a free key if not supplied. Recommended to register your own key in Azure")]
        [DisplayName("API Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Output")]
        [Description("Full friendly output of the text screening evaluation")]
        [DisplayName("Full Friendly Output")]
        public OutArgument<string> FriendlyOutput { get; set; }

        [Category("Output")]
        [Description("Auto-corrected Text")]
        [DisplayName("Auto-corrected Text")]
        public OutArgument<string> AutoCorrectedText { get; set; }

        [Category("Output")]
        [Description("Normalized Text")]
        [DisplayName("Normalized Text")]
        public OutArgument<string> NormalizedText { get; set; }

        [Category("Output")]
        [Description("Potential presence of language that may be considered sexually explicit or adult in certain situations")]
        [DisplayName("Profanity Score - Category 1")]
        public OutArgument<double?> ProfanityCategory1Score { get; set; }

        [Category("Output")]
        [Description("Potential presence of language that may be considered sexually suggestive or mature in certain situations")]
        [DisplayName("Profanity Score - Category 2")]
        public OutArgument<double?> ProfanityCategory2Score { get; set; }

        [Category("Output")]
        [Description("Potential presence of language that may be considered offensive in certain situations")]
        [DisplayName("Profanity Score - Category 3")]
        public OutArgument<double?> ProfanityCategory3Score { get; set; }

        [Category("Output")]
        [Description("Determine if content is recommended for further review - Boolean")]
        [DisplayName("Review Recommended")]
        public OutArgument<bool?> ReviewRecommended { get; set; }

        [Category("Output")]
        [Description("Full JSON output of the text screening evaluation")]
        [DisplayName("Full JSON Output")]
        public OutArgument<string> JsonOutput { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Main activity method
        /// </summary>
        /// <param name="context"></param>
        protected override void Execute(CodeActivityContext context)
        {
            var textInput = Text.Get(context);
            textInput = textInput.Replace(System.Environment.NewLine, " ");
            var apiKey = ApiKey.Get(context);
            if (apiKey != null)
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
                    FriendlyOutput.Set(context, _contentModeratorOutput.FriendlyOutput);
                    JsonOutput.Set(context, _contentModeratorOutput.JsonOutput);
                    AutoCorrectedText.Set(context, _contentModeratorOutput.AutoCorrectedText);
                    NormalizedText.Set(context, _contentModeratorOutput.NormalizedText);
                    ProfanityCategory1Score.Set(context, _contentModeratorOutput.ProfanityCategory1Score);
                    ProfanityCategory2Score.Set(context, _contentModeratorOutput.ProfanityCategory2Score);
                    ProfanityCategory3Score.Set(context, _contentModeratorOutput.ProfanityCategory3Score);
                    ReviewRecommended.Set(context, _contentModeratorOutput.ReviewRecommended);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Actual Exception Message: {ex.Message}\n{GeneralException}");
            }
        }
        #endregion
    }
}
