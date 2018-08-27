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
    /// Test class for Credit grant assessment service
    /// </summary>
    [TestClass]
    public class TestCreditGrantAssessor
    {
        #region Method under test
        /// <summary>
        /// Main execution method for activity
        /// </summary>
        /// <param name="Region"></param>
        /// <param name="Country"></param>
        /// <param name="Borrower"></param>
        /// <param name="ProjectName"></param>
        /// <param name="PrincipalAmount"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        protected string Execute(string Region, string Country, string Borrower, string ProjectName, double PrincipalAmount, string apiKey = "")
        {
            List<string> listApprovedCreditStatus = new List<string>
            {
                "Approved","Disbursed","Disbursing","Disbursing&Repaying","Effective","Fully Disbursed","Fully Repaid","Repaid","Repaying","Signed"
            };

            WebUtility webUtility = new WebUtility(CreditGrantAssessor.ApiEndpoint);
            var scoreRequest = new
            {
                Inputs = new Dictionary<string, List<Dictionary<string, string>>>()
                {
                    {
                        "input1",
                        new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                {
                                    "Region", Region.ToString().Replace("_"," ")
                                },
                                {
                                    "Country", Country
                                },
                                {
                                    "Borrower", Borrower
                                },
                                {
                                    "Project Name", ProjectName
                                },
                                {
                                    "Original Principal Amount", PrincipalAmount.ToString()
                                },
                                {
                                    "Credit Status", ""
                                }
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
                string key = CreditGrantAssessor.ApiKey;
                if (apiKey != "")
                    key = apiKey;
                string jsonStringInput = JsonConvert.SerializeObject(scoreRequest);
                var responseOutput = Task.Run(async () => await webUtility.PostAsync(bodyAsJsonString: jsonStringInput, token: key));
                JObject jsonResponse = JObject.Parse(responseOutput.Result);

                string predictedStatus = jsonResponse.SelectToken("$..output1[0]")["Scored Labels"].ToString();
                if (listApprovedCreditStatus.Contains(predictedStatus))
                    return "Approved";
                else
                    return "Rejected";
            }
            catch (Exception ex)
            {
                throw new System.Exception($"Actual Exception: {ex.InnerException}\nPossible cause: {CreditGrantAssessor.apiHostingPlanPrompt}");
            }
        }
        #endregion

        #region Test Methods
        /// <summary>
        /// Test Fully Cancelled category --> Outcome: Rejected
        /// </summary>
        [TestMethod]
        public void Assess_FullyCancelled_Rejected()
        {
            var actual = Execute("SOUTH_ASIA", "Pakistan", "MINISTRY OF FINANCE AND ECONOMIC AFFAIRS", 
                                 "INLAND WATER TRANSPO", 5250000);
            Assert.AreEqual("Rejected", actual, "Incorrect Prediction");
        }

        /// <summary>
        /// Test Terminated category --> Outcome: Rejected
        /// </summary>
        [TestMethod]
        public void Assess_Terminated_Rejected()
        {
            var actual = Execute("MIDDLE_EAST_AND_NORTH_AFRICA", "Djibouti", "Ministry of Economy and Finance",
                                 "DJ-FLOOD EMERGENCY REHABILITATION", 2000000);
            Assert.AreEqual("Rejected", actual, "Incorrect Prediction");
        }

        /// <summary>
        /// Test Approved category --> Outcome: Approved
        /// </summary>
        [TestMethod]
        public void Assess_Approved_Approved()
        {
            var actual = Execute("AFRICA", "Zambia", "Ministry of Finance & National Planning",
                                 "GLR:Displaced Persons and Border Comm", 20000000);
            Assert.AreEqual("Approved", actual, "Incorrect Prediction");
        }

        /// <summary>
        /// Test Disbursing category --> Outcome: Approved
        /// </summary>
        [TestMethod]
        public void Assess_Disbursing_Approved()
        {
            var actual = Execute("SOUTH_ASIA", "Bangladesh", "MINISTRY OF FINANCE",
                                 "BD: Siddhirganj Power Project", 350000000);
            Assert.AreEqual("Approved", actual, "Incorrect Prediction");
        }

        /// <summary>
        /// Test Full Repaid category --> Outcome: Approved
        /// </summary>
        [TestMethod]
        public void Assess_FullyRepaid_Approved()
        {
            var actual = Execute("LATIN AMERICA AND CARIBBEAN", "Chile", "LATIN AMERICA AND CARIBBEAN",
                                 "ROAD CONSTRUCTION", 22878919.07);
            Assert.AreEqual("Approved", actual, "Incorrect Prediction");
        }

        /// <summary>
        /// Test Effective category --> Outcome: Approved
        /// </summary>
        [TestMethod]
        public void Assess_Effective_Approved()
        {
            var actual = Execute("EAST_ASIA_AND_PACIFIC", "Solomon Islands", "Ministry of Finance and Treasury",
                                 "PROP Solomon Islands", 5950000);
            Assert.AreEqual("Approved", actual, "Incorrect Prediction");
        }

        /// <summary>
        /// Test exception encountered
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void Assess_WhenInvalidKey_ShouldThrowException()
        {
            var actual = Execute("EAST_ASIA_AND_PACIFIC", "Solomon Islands", "Ministry of Finance and Treasury",
                                  "PROP Solomon Islands", 5950000, "INVALID API KEY");
        }
        #endregion
    }
}