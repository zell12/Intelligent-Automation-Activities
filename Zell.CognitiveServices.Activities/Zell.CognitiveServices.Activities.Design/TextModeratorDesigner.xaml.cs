using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zell.CognitiveServices.Activities.Design
{
    /// <summary>
    /// Interaction logic for TextModeratorDesigner.xaml
    /// </summary>
    public partial class TextModeratorDesigner
    {
        #region Constants
        /// <summary>
        /// Description showing on the activity explorer
        /// </summary>
        private const string activityDescription = "v3 - Helps you detect potential profanity in more than 100 languages and match text against your custom lists automatically. Content Moderator also checks for possible personally identifiable information (PII). This comes with a free text moderator api key for development purposes. If for extensive use, recommended to generate a personal subscription key.";
        #endregion

        #region Constructor
        /// <summary>
        /// Component initialization
        /// </summary>
        public TextModeratorDesigner()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The designer class' internal metadata registration method
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            // Text moderator attributes
            string description = activityDescription;
            builder.AddCustomAttributes(typeof(TextModerator), new DesignerAttribute(typeof(TextModeratorDesigner)));
            builder.AddCustomAttributes(typeof(TextModerator), new DescriptionAttribute(description));
            builder.AddCustomAttributes(typeof(TextModerator), new CategoryAttribute(Properties.Resources.CognitiveActivitiesContentModerationCategories));
        }
        #endregion
    }
}