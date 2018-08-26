using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zell.CognitiveServices.Activities.Utilities.ContentModeration.Models
{
    /// <summary>
    /// Model class for text moderator cognitive services
    /// </summary>
    public class TextEvaluationOutputModel
    {
        #region Public Properties
        /// <summary>
        /// Full friendly output of the text screening evaluation
        /// </summary>
        public string FriendlyOutput { get; set; }

        /// <summary>
        /// Full JSON output of the text screening evaluation
        /// </summary>
        public string JsonOutput { get; set; }

        /// <summary>
        /// Auto-corrected Text
        /// </summary>
        public string AutoCorrectedText { get; set; }

        /// <summary>
        /// Normalized Text
        /// </summary>
        public string NormalizedText { get; set; }

        /// <summary>
        /// Potential presence of language that may be considered sexually explicit or adult in certain situations
        /// </summary>
        public double? ProfanityCategory1Score { get; set; }

        /// <summary>
        /// Potential presence of language that may be considered sexually suggestive or mature in certain situations
        /// </summary>
        public double? ProfanityCategory2Score { get; set; }

        /// <summary>
        /// Potential presence of language that may be considered offensive in certain situations
        /// </summary>
        public double? ProfanityCategory3Score { get; set; }

        /// <summary>
        /// Determine if content is recommended for further review - Boolean
        /// </summary>
        public bool? ReviewRecommended { get; set; }
        #endregion
    }
}
