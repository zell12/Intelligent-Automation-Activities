using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Utilities;
using Zell.MachineLearningModels;

namespace TestMachineLearningModels
{
    /// <summary>
    /// Test class for Email ticket classifier service
    /// </summary>
    [TestClass]
    public class TestEmailTicketClassifier
    {
        #region Method under test
        /// <summary>
        /// Main execution method for activity
        /// </summary>
        /// <param name="EmailSubject"></param>
        /// <param name="EmailDescription"></param>
        /// <param name="apiKey"></param>
        protected Dictionary<string, string> Execute(string EmailSubject, string EmailDescription, string apiKey = "")
        {
            var output = new Dictionary<string, string>();
            WebUtility webUtility = new WebUtility(EmailTicketClassifier.ApiEndpoint);
            var scoreRequest = new
            {
                Inputs = new Dictionary<string, List<Dictionary<string, string>>>()
                {
                    {
                        "input1",
                        new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                {
                                    "Email_Subject", EmailSubject
                                },
                                {
                                    "Email_Description", EmailDescription
                                },
                            }
                        }
                    },
                },
                GlobalParameters = new Dictionary<string, string>()
                {
                }
            };

            try
            {
                string key = EmailTicketClassifier.ApiKey;
                if (apiKey != "")
                    key = apiKey;
                string jsonStringInput = JsonConvert.SerializeObject(scoreRequest);
                var responseOutput = Task.Run(async () => await webUtility.PostAsync(bodyAsJsonString: jsonStringInput, token: key));
                JObject jsonResponse = JObject.Parse(responseOutput.Result);

                string predictedCaseType = jsonResponse.SelectToken("$..Case_CaseType").ToString();
                string predictedCaseSubject = jsonResponse.SelectToken("$..Case_Subject").ToString();
                string predictedCaseQueueName = jsonResponse.SelectToken("$..Queue_Name").ToString();

                output = new Dictionary<string, string>
                {
                    { "Case Type", predictedCaseType },
                    { "Case Subject", predictedCaseSubject },
                    { "Case Queue Name" , predictedCaseQueueName }
                };
                return output;
            }
            catch (Exception ex)
            {
                throw new System.Exception($"Actual Exception: {ex.Message}\nPossible cause: {CreditGrantAssessor.apiHostingPlanPrompt}");
            }
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// Test email - Credit support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Credit()
        {
            var actual = Execute("Total Accrued Incentives",
                                 "I am writing to see if you can let me know what the Total Accrued Incentives are for my account that have not yet been paid, and when they expect to be paid. ID: 123456 Regards, Jacob");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Credit", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Question", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<PI>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test email - Account support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Account()
        {
            var actual = Execute("URGENT:9548646 ABCsoft requesting a revised Invoice with correct Pricing",
                                 "Hello Team, Hope you are doing well. I would like to seek an assistance with regard to customer's request on revising the Invoice with correct pricing for invoice 123459875 at $2.75 instead of $5.00. Attached herewith are the email communication for your reference. ");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Account", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Request", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<MBS>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test email - Payment support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Payment()
        {
            var actual = Execute("RE: Assistance Needed: ExtraMile_7945663",
                                 "Hi Team, Hope all is well. Could you please assist us regarding our concern? Credits are already booked and applied to their October to December invoices but upon checking the revenue that should be booked to the right agency doesn't populate here on our end.");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Payment", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Question", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<PI>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test email - Billing support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Billing()
        {
            var actual = Execute("Product Token Billing - Electronic Arts - NHL'17 Content",
                                 "Please find the attached token request form and associated PO. Expected token delivery date is 8/12. Price is negotiated and agreed as an exception. $3/token. Qty. 61,000. Let me know if you require any further information. Vance");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Billing", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Question", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<MBS>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test email - Payment support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Payment2()
        {
            var actual = Execute("Queries regarding Partner cloud selling",
                                 "Hi, I would like to find out some information: ' Where do I provide account details for incentives ' What is the incentive percentage for every license of ABC Pro ' What are the incentives required to reach different accreditation levels ");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Payment", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Question", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<PI>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test email - Amendment support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Amendment()
        {
            var actual = Execute("Customer Professional's change doesn't work",
                                 "Dear Mrs./Mr. While trying several times to change List of customer's professionals I'm constantly rejected by partnersource, no matter that I've entered all necessary data and push Save button as well. You can check on picture below what I've entered ' and this is not reflected on list of...");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Amendment", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Question", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<PI>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test email - Shipment support ticket
        /// </summary>
        [TestMethod]
        public void Classify_Shipment()
        {
            var actual = Execute("5004567_Target PC_Shipment: For Credit Request",
                                 "Hello Team, Good day. Please process attached request with approval below. Thank you. Amount: $2,399.75 Let me know should you need further clarifications or questions. Regards, We strive to provide you the best possible customer service.");
            var predictedCaseSubject = actual["Case Subject"];
            var predictedCaseType = actual["Case Type"];
            var predictedCaseQueue = actual["Case Queue Name"];
            Assert.AreEqual("Shipment", predictedCaseSubject, "Incorrect Case subject Prediction");
            Assert.AreEqual("Question", predictedCaseType, "Incorrect Case type Prediction");
            Assert.AreEqual("<MBS>", predictedCaseQueue, "Incorrect Case queue Prediction");
        }

        /// <summary>
        /// Test exception encountered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Classify_WhenInvalidKey_ShouldThrowException()
        {
            var actual = Execute("Test subject", "Test description", "INVALID API KEY");
        }
        #endregion
    }
}