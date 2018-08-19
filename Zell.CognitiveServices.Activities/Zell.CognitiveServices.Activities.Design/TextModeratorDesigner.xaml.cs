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
    // Interaction logic for TextTranslatorDesigner.xaml
    public partial class TextModeratorDesigner
    {
        public TextModeratorDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            // Text translator attribute
            string description = "v3 - Helps you detect potential profanity in more than 100 languages and match text against your custom lists automatically. Content Moderator also checks for possible personally identifiable information (PII). This comes with a free text moderator api key for development purposes. If for extensive use, recommended to generate a personal subscription key.";
            builder.AddCustomAttributes(typeof(TextModerator), new DesignerAttribute(typeof(TextModeratorDesigner)));
            builder.AddCustomAttributes(typeof(TextModerator), new DescriptionAttribute(description));
            builder.AddCustomAttributes(typeof(TextModerator), new CategoryAttribute(Properties.Resources.CognitiveActivitiesContentModerationCategories));
        }
}
}
