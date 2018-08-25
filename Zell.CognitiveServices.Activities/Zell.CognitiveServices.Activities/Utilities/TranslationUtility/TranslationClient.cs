using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
    public class MicrosoftTranslationClient
    {
        public const string InvalidCredentialsError = "Credentials are invalid";
        public const string InvalidApiKeyResolution = "\nAn error is encountered.Subscription key might be invalid.Follow the steps below:\n(1) Subscribe and generate a text translator API key in Azure.\n(2) Paste the subscription key generated to ApiKey field.";
        public static string ApiKey = "59718ff5919f4a8781bdb9dc72fb15d8";

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
    }
}