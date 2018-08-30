using Microsoft.Azure.CognitiveServices.ContentModerator;
using Microsoft.CognitiveServices.ContentModerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zell.CognitiveServices.Activities.Utilities.ContentModeration.Models;

namespace Utilities
{
    /// <summary>
    /// Helper class for Content Moderation cognitive services
    /// </summary>
    public class ContentModeratorHelper
    {
        #region Fields
        /// <summary>
        /// The region/location for your Content Moderator account, 
        /// for example, westus.
        /// </summary>
        private static readonly string AzureRegion = "eastasia";

        /// <summary>
        /// The base URL fragment for Content Moderator calls.
        /// </summary>
        private static readonly string AzureBaseURL =
            $"{AzureRegion}.api.cognitive.microsoft.com";

        /// <summary>
        /// This is a default FREE key and should NOT be used in production.
        /// </summary>
        public static string ApiKey = "YOUR_API_KEY_HERE";
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a new Content Moderator client for your subscription.
        /// </summary>
        /// <returns>The new client.</returns>
        /// <remarks>The <see cref="ContentModeratorHelper"/> is disposable.
        /// When you have finished using the client,
        /// you should dispose of it either directly or indirectly. </remarks>
        public static ContentModeratorClient NewModeratorClient()
        {
            // Create and initialize an instance of the Content Moderator API wrapper.
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(ApiKey));

            client.BaseUrl = AzureBaseURL;
            return client;
        }

        /// <summary>
        /// Returns the text evalulation output based on the input text
        /// </summary>
        /// <param name="textInput"></param>
        /// <returns>The text evaluation output</returns>
        public static TextEvaluationOutputModel GetTextEvaluationOutput(string textInput)
        {
            var output = new TextEvaluationOutputModel();
            StringBuilder friendlyOutput = new StringBuilder();
            string jsonOutput;
            using (var client = ContentModeratorHelper.NewModeratorClient())
            {
                // Screen the input text: check for profanity, classify the text into three categories
                // do autocorrect text, and check for personally identifying 
                // information (PII)
                using (var stream = GenerateStreamFromString(textInput))
                {
                    //var detectedLanguage = client.TextModeration.DetectLanguage("text/plain", stream).DetectedLanguageProperty;
                    var screenResult = client.TextModeration.ScreenText("text/plain", stream, null, true, true, null, true);
                    jsonOutput = Newtonsoft.Json.JsonConvert.SerializeObject(screenResult);
                    var autoCorrectedText = screenResult?.AutoCorrectedText;
                    var normalizedText = screenResult?.NormalizedText;
                    var profanityCategory1Score = screenResult?.Classification?.Category1?.Score;
                    var profanityCategory2Score = screenResult?.Classification?.Category2?.Score;
                    var profanityCategory3Score = screenResult?.Classification?.Category3?.Score;
                    var reviewRecommended = screenResult?.Classification?.ReviewRecommended;
                    friendlyOutput.AppendLine("Autocorrect typos, check for matching terms, PII, and classify.");
                    friendlyOutput.AppendLine($"===Original text: ");
                    friendlyOutput.AppendLine($"> {screenResult?.OriginalText}");
                    friendlyOutput.AppendLine($"===Normalized Text: ");
                    friendlyOutput.AppendLine($"> {normalizedText}");
                    friendlyOutput.AppendLine($"===Autocorrected Text: {autoCorrectedText}");
                    friendlyOutput.AppendLine($"> {autoCorrectedText}");
                    friendlyOutput.AppendLine($"===Classification:");
                    friendlyOutput.AppendLine($"> Category 1 Score (potential presence of language that may be considered sexually explicit or adult in certain situations): {profanityCategory1Score}");
                    friendlyOutput.AppendLine($"> Category 2 Score (potential presence of language that may be considered sexually suggestive or mature in certain situations): {profanityCategory2Score}");
                    friendlyOutput.AppendLine($"> Category 3 Score (potential presence of language that may be considered offensive in certain situations): {profanityCategory3Score}");
                    friendlyOutput.AppendLine($"> Review Recommended?: {reviewRecommended}");
                    friendlyOutput.AppendLine($"===Language: ");
                    friendlyOutput.AppendLine($"> {screenResult?.Language}");
                    friendlyOutput.AppendLine($"===Personally Identifiable Information:");
                    friendlyOutput.AppendLine($">Address(es): ");
                    foreach (var item in screenResult?.PII?.Address)
                    {
                        friendlyOutput.AppendLine($"  Index -- {item?.Index}");
                        friendlyOutput.AppendLine($"  Text -- {item?.Text}");
                    }
                    friendlyOutput.AppendLine($"> Email(s): ");
                    foreach (var item in screenResult?.PII?.Email)
                    {
                        friendlyOutput.AppendLine($"  Detected -- {item?.Detected}");
                        friendlyOutput.AppendLine($"  Subtype -- {item?.SubType}");
                        friendlyOutput.AppendLine($"  Index -- {item?.Index}");
                        friendlyOutput.AppendLine($"  Text -- {item?.Text}");
                    }
                    friendlyOutput.AppendLine($"> IPA(s): ");
                    foreach (var item in screenResult?.PII?.IPA)
                    {
                        friendlyOutput.AppendLine($"  Subtype -- {item?.SubType}");
                        friendlyOutput.AppendLine($"  Index -- {item?.Index}");
                        friendlyOutput.AppendLine($"  Text -- {item?.Text}");
                    }
                    friendlyOutput.AppendLine($"> Phone(s): ");
                    foreach (var item in screenResult?.PII?.Phone)
                    {
                        friendlyOutput.AppendLine($"  CountryCode -- {item?.CountryCode}");
                        friendlyOutput.AppendLine($"  Index -- {item?.Index}");
                        friendlyOutput.AppendLine($"  Text -- {item?.Text}");
                    }
                    friendlyOutput.AppendLine($"> Social Security Number(s): ");
                    foreach (var item in screenResult?.PII?.SSN)
                    {
                        friendlyOutput.AppendLine($"  Index -- {item?.Index}");
                        friendlyOutput.AppendLine($"  Text -- {item?.Text}");
                    }
                    friendlyOutput.AppendLine($"===Status: ");
                    friendlyOutput.AppendLine($"> Code: {screenResult?.Status?.Code}");
                    friendlyOutput.AppendLine($"> Description: {screenResult?.Status?.Description}");
                    friendlyOutput.AppendLine($"> Exception: {screenResult?.Status?.Exception}");
                    friendlyOutput.AppendLine($"===Terms: ");
                    if (screenResult?.Terms != null)
                    {
                        foreach (var item in screenResult?.Terms)
                        {
                            friendlyOutput.AppendLine($"> ListId -- {item?.ListId}");
                            friendlyOutput.AppendLine($"> Original Index -- {item?.OriginalIndex}");
                            friendlyOutput.AppendLine($"> Index -- {item?.Index}");
                            friendlyOutput.AppendLine($"> Term -- {item?.Term}");
                        }
                    }
                    friendlyOutput.AppendLine($"===Transaction Id: ");
                    friendlyOutput.AppendLine($"> {screenResult?.TrackingId}");

                    output.FriendlyOutput = friendlyOutput?.ToString();
                    output.JsonOutput = jsonOutput;
                    output.AutoCorrectedText = autoCorrectedText;
                    output.NormalizedText = normalizedText;
                    output.ProfanityCategory1Score = profanityCategory1Score;
                    output.ProfanityCategory2Score = profanityCategory2Score;
                    output.ProfanityCategory3Score = profanityCategory3Score;
                    output.ReviewRecommended = reviewRecommended;
                }
            }

            return output;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Stream generation from input string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        #endregion
    }
}