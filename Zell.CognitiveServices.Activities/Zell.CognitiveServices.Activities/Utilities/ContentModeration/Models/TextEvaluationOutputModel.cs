using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zell.CognitiveServices.Activities.Utilities.ContentModeration.Models
{
    public class TextEvaluationOutputModel
    {
        public string FriendlyOutput { get; set; }
        public string JsonOutput { get; set; }
        public string AutoCorrectedText { get; set; }
        public string NormalizedText { get; set; }
        public double? ProfanityCategory1Score { get; set; }
        public double? ProfanityCategory2Score { get; set; }
        public double? ProfanityCategory3Score { get; set; }
        public bool? ReviewRecommended { get; set; }
    }
}
