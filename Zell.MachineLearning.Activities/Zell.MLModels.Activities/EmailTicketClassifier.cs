using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Utilities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Zell.MachineLearningModels
{
    [Description("Email classification experiment to assign an email to one or more class(es) of predefined set of classes or work queues.")]
    public class EmailTicketClassifier : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Email Subject")]
        [Description("Subject of the email to be classified")]
        public InArgument<string> EmailSubject { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Email Description")]
        [Description("Brief description of the email to be classified")]
        public InArgument<string> EmailDescription { get; set; }

        [Category("Output")]
        [DisplayName("Case Type")]
        [Description("Predicted case type classification")]
        public OutArgument<string> CaseType { get; set; }

        [Category("Output")]
        [DisplayName("Case Subject")]
        [Description("Predicted case subject classification")]
        public OutArgument<string> CaseSubject { get; set; }

        [Category("Output")]
        [DisplayName("Queue Name")]
        [Description("Predicted queue name classification")]
        public OutArgument<string> CaseQueueName { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            string endpoint = "https://ussouthcentral.services.azureml.net/workspaces/3a11f67368b7437cb061c96b6ec25e5a/services/244aa5bbf1724649be54a010b10ffa2c/execute?api-version=2.0&format=swagger";
            string apiKey = "210eo81oY/BXmO1e6atv7GyKHPuolfMayLcITqO5STIjdMfg6Ja4aE4EtDCVH4Qeh2lVYwNNnh+aM2KuZwbAHw==";
            WebUtility webUtility = new WebUtility(endpoint);
            var scoreRequest = new
            {
                Inputs = new Dictionary<string, List<Dictionary<string, string>>>()
                {
                    {
                        "input1",
                        new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                {
                                    "Email_Subject", EmailSubject.Get(context)
                                },
                                {
                                    "Email_Description", EmailDescription.Get(context)
                                },
                            }
                        }
                    },
                },
                GlobalParameters = new Dictionary<string, string>()
                {
                }
            };

            string jsonStringInput = JsonConvert.SerializeObject(scoreRequest);
            //var responseOutput = Task.Run(async () => await webUtility.PostAsJsonAsync(body: scoreRequest, token: apiKey));
            var responseOutput = Task.Run(async () => await webUtility.PostAsync(bodyAsJsonString: jsonStringInput, token: apiKey));
            JObject jsonResponse = JObject.Parse(responseOutput.Result);

            string predictedCaseType = jsonResponse.SelectToken("$..Case_CaseType").ToString();
            string predictedCaseSubject = jsonResponse.SelectToken("$..Case_Subject").ToString();
            string predictedCaseQueueName = jsonResponse.SelectToken("$..Queue_Name").ToString();
            CaseType.Set(context, predictedCaseType);
            CaseSubject.Set(context, predictedCaseSubject);
            CaseQueueName.Set(context, predictedCaseQueueName);
        }
    }
}
