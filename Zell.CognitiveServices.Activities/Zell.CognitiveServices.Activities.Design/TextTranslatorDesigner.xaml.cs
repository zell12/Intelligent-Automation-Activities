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
    public partial class TextTranslatorDesigner
    {
        public TextTranslatorDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            // Text translator attribute
            string textTranslatorDescription = "v9 - Cognitive service for natural language machine translation supporting over 60 + languages and dialects. This comes with a free text transltor api key for development purposes. If for extensive use, recommended to generate a personal subscription key.";
            builder.AddCustomAttributes(typeof(TextTranslator), new DesignerAttribute(typeof(TextTranslatorDesigner)));
            builder.AddCustomAttributes(typeof(TextTranslator), new DescriptionAttribute(textTranslatorDescription));
            builder.AddCustomAttributes(typeof(TextTranslator), new CategoryAttribute(Properties.Resources.CognitiveActivitiesTranslationCategories));
        }
}
}
