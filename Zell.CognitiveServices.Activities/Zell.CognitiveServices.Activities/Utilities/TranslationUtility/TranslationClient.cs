using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    /// <summary>
    /// Helper class for Text Translation cognitive services
    /// </summary>
    public class MicrosoftTranslationClient
    {
        #region Public Fields and Constants
        /// <summary>
        /// Error output for invalid key in cmd shell
        /// </summary>
        public const string InvalidCredentialsError = "credentials are invalid";

        /// <summary>
        /// Resolution instruction text for invalid key
        /// </summary>
        public const string InvalidApiKeyResolution = "\nMake sure target language code and api key is valid. IF error encountered is invalid api key, the default or current subscription key might have already expired or invalid. Follow the steps below to resolve invalid key:\n(1) Subscribe and generate a text translator API key in Azure.\n(2) Paste the subscription key generated to ApiKey field.";

        /// <summary>
        /// Subscription key
        /// </summary>
        public static string ApiKey = "YOUR_API_KEY_HERE";
        #endregion

        #region Public Methods
        /// <summary>
        /// Performs neural machine translation of input text via REST api call
        /// </summary>
        /// <param name="text"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string TranslateText(string text, string to)
        {
            string uri = "https://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + HttpUtility.UrlEncode(text) + "&to=" + to;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);
            using (WebResponse response = httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            {
                DataContractSerializer dcs = new DataContractSerializer(Type.GetType("System.String"));
                return (string)dcs.ReadObject(stream);
            }
        }

        /// <summary>
        /// Detects language of source text
        /// </summary>
        /// <param name="textToDetect"></param>
        /// <returns></returns>
        public static string Detect(string textToDetect)
        {
            string uri = "https://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Ocp-Apim-Subscription-Key", ApiKey);
            using (WebResponse response = httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            {
                DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                return (string)dcs.ReadObject(stream);
            }
        }
        #endregion
    }
}