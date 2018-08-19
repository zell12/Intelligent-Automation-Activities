using System.Activities;
using System.ComponentModel;
using Utilities;
using System.Drawing;

namespace Zell.CognitiveServices
{
    [Description("Cognitive service for natural language machine translation supporting over 60+ languages and dialects.")]
    [ToolboxBitmap(typeof(TextTranslator),"translator-icon.png")]
    public class TextTranslator : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [Description("Put the text to translate here")]
        public InArgument<string> Text { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Translation target ISO Language code e.g. fil, en, pl, ja, th, etc")]
        [DefaultValue("\"en\"")]
        [DisplayName("Target Language ISO Code")]
        public InArgument<string> TargetLanguageCode { get; set; }

        [Category("Input")]
        [Description("Defaults to a free key if not supplied. Recommended to register your own key in Azure")]
        [DisplayName("API Key")]
        public InArgument<string> ApiKey { get; set; }

        [Category("Output")]
        [Description("This is the translated text")]
        [DisplayName("Translated Text")]
        public OutArgument<string> TranslatedText { get; set; }

        [Category("Output")]
        [Description("This is the detected source language")]
        [DisplayName("Detected Source Language")]
        public OutArgument<string> DetectedSourceLanguage { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var textToTranslate = Text.Get(context);
            var targetLanguageCode = TargetLanguageCode.Get(context);
            var apiKey = ApiKey.Get(context);
            if (apiKey != null)
            {
                // Sets to the user's apiKey, if supplied; if not, defaults to a free key
                MicrosoftTranslationClient.ApiKey = apiKey;
            }

            try
            {
                var translatedText = MicrosoftTranslationClient.TranslateText(textToTranslate, targetLanguageCode);
                var detectedSourceLanguage = MicrosoftTranslationClient.Detect(textToTranslate);

                TranslatedText.Set(context, translatedText);
                DetectedSourceLanguage.Set(context, detectedSourceLanguage);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("\nAn error is encountered. Subscription key might be invalid. Follow the steps below:\n(1) Subscribe and generate a text translator API key in Azure.\n(2) Paste the subscription key generated to ApiKey field.");
            }
        }
    }
}
