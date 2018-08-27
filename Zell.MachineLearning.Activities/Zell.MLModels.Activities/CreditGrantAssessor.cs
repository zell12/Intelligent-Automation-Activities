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
using System.Data;

namespace Zell.MachineLearningModels
{
    /// <summary>
    /// Activity class for credit assessor machine learning model
    /// </summary>
    [Description("Email classification experiment to assign an email to one or more class(es) of predefined set of classes or work queues.")]
    public class CreditGrantAssessor : CodeActivity
    {
        #region Enums
        /// <summary>
        /// Grant status categories
        /// </summary>
        public enum GrantStatus
        {
            Approved,
            Rejected
        }
        #endregion

        #region Constants
        public const string apiHostingPlanPrompt = "This model is hosted on a free azure workspace or staging api. If an error is encountered during execution, it might be that the transaction limit per month or one of the other governing limit ss already reached. Go to this page for more details: https://azure.microsoft.com/en-us/pricing/details/machine-learning-studio/";
        /// <summary>
        /// Web service endpoint url for the trained model
        /// </summary>
        private const string ApiEndpoint = "https://ussouthcentral.services.azureml.net/workspaces/3a11f67368b7437cb061c96b6ec25e5a/services/634c6348584946f99fd860cb7a1469ad/execute?api-version=2.0&format=swagger";

        /// <summary>
        /// Web service key for the trained model
        /// </summary>
        private const string ApiKey = "TwD5snN6/O3MaUvmQ03YXS911jTKtcwfCAUCt4ReWFHzMvtCW9/nkzLxTJk5A/snFOysIcPVt9vQSmwO6hskcA==";
        #endregion        

        #region Public Properties
        //[Category("Input")]
        //[RequiredArgument]
        //[DisplayName("Credit Data")]
        //[Description("Bulk Transaction - Input credit data for assessment")]
        //public InArgument<DataTable> InputCreditData { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("World Bank Region to which the country and loan belong. Country lending is grouped into regions based on the current World Bank administrative (rather than geographic) region where project implementation takes place. The “Other” Region is used for loans to the IFC.")]
        public Region Region { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("Country to which a loan has been issued. Loans to the IFC are included under the country “World”. e.g. Honduras, Costa Rica, Nigeria, etc")]
        public InArgument<string> Country { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [Description("The representative of the borrower to which the Bank loan is made. e.g. MINISTERIO DE HACIENDA Y CREDITO PUBLICO")]
        public InArgument<string> Borrower { get; set; }

        [Category("Input")]
        [DisplayName("Project Name")]
        [Description("Short descriptive project name.")]
        public InArgument<string> ProjectName { get; set; }

        [Category("Input")]
        [RequiredArgument]
        [DisplayName("Principal Amount")]
        [Description("The original US dollar amount of the loan that is committed and approved.")]
        public InArgument<int> PrincipalAmount { get; set; }

        [Category("Output")]
        [DisplayName("Grant Outcome")]
        [Description("Predicted grant outcome of the loan as learned from the model")]
        public OutArgument<string> Outcome { get; set; }
        #endregion

        #region Public Methods
        protected override void Execute(CodeActivityContext context)
        {
            //var inputData = InputCreditData.Get(context);

            List<string> listApprovedCreditStatus = new List<string>
            {
                "Approved","Disbursed","Disbursing","Disbursing&Repaying","Effective","Fully Disbursed","Fully Repaid","Repaid","Repaying","Signed"
            };

            WebUtility webUtility = new WebUtility(ApiEndpoint);
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
                                    "Country", Country.Get(context)
                                },
                                {
                                    "Borrower", Borrower.Get(context)
                                },
                                {
                                    "Project Name", ProjectName.Get(context)
                                },
                                {
                                    "Original Principal Amount", PrincipalAmount.Get(context).ToString()
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
                string jsonStringInput = JsonConvert.SerializeObject(scoreRequest);
                var responseOutput = Task.Run(async () => await webUtility.PostAsync(bodyAsJsonString: jsonStringInput, token: ApiKey));
                JObject jsonResponse = JObject.Parse(responseOutput.Result);

                string predictedStatus = jsonResponse.SelectToken("$..output1[0]")["Scored Labels"].ToString();
                string grantStatus = "";
                if (listApprovedCreditStatus.Contains(predictedStatus))
                    grantStatus = GrantStatus.Approved.ToString();
                else
                    grantStatus = GrantStatus.Rejected.ToString();

                Outcome.Set(context, grantStatus);
            }
            catch (Exception ex)
            {
                throw new System.Exception($"Actual Exception: {ex.InnerException}\nPossible cause: {apiHostingPlanPrompt}");
            }
        }
        #endregion
    }
}