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
    /// Interaction logic for TextTranslatorDesigner.xaml
    /// </summary>
    public partial class TextTranslatorDesigner
    {
        #region Constants
        /// <summary>
        /// Description showing on the activity explorer
        /// </summary>
        private const string activityDescription = "v11 - Cognitive service for natural language machine translation supporting over 60 + languages and dialects. This comes with a free text transltor api key for development purposes. If for extensive use, recommended to generate a personal subscription key.";
        #endregion

        #region Constructor
        /// <summary>
        /// Component initialization
        /// </summary>
        public TextTranslatorDesigner()
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
            // Text translator attribute
            string textTranslatorDescription = activityDescription;
            builder.AddCustomAttributes(typeof(TextTranslator), new DesignerAttribute(typeof(TextTranslatorDesigner)));
            builder.AddCustomAttributes(typeof(TextTranslator), new DescriptionAttribute(textTranslatorDescription));
            builder.AddCustomAttributes(typeof(TextTranslator), new CategoryAttribute(Properties.Resources.CognitiveActivitiesTranslationCategories));
        }
        #endregion
    }
}