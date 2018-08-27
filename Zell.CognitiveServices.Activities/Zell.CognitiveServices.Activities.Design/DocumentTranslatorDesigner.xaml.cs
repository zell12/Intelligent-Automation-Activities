using Microsoft.Win32;
using System;
using System.Activities;
using System.Activities.Presentation.Metadata;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for DocumentTranslatorDesigner.xaml
    /// </summary>
    public partial class DocumentTranslatorDesigner
    {
        #region Constants
        /// <summary>
        /// Description showing on the activity explorer
        /// </summary>
        private const string activityDescription = "v32 - The Microsoft Document Translator translates Microsoft Office, plain text, HTML, PDF files and SRT caption files, from and to any of the 60+ languages supported by the Microsoft Translator web service. This comes with a free text transltor api key for development purposes. If for extensive use, recommended to generate a personal subscription key.";
        #endregion

        #region Constructor
        /// <summary>
        /// Component initialization
        /// </summary>
        public DocumentTranslatorDesigner()
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
            builder.AddCustomAttributes(typeof(DocumentTranslator), new DesignerAttribute(typeof(DocumentTranslatorDesigner)));
            builder.AddCustomAttributes(typeof(DocumentTranslator), new DescriptionAttribute(activityDescription));
            builder.AddCustomAttributes(typeof(DocumentTranslator), new CategoryAttribute(Properties.Resources.CognitiveActivitiesTranslationCategories));
        }
        #endregion

        #region Event Methods
        /// <summary>
        /// Method for file selection button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            string fileFilters = "*.doc; *.docx; *.pdf; *.xls; *.xlsx; *.ppt; *.pptx; *.txt; *.text; *.htm; *.html; *.srt";
            openFileDialog.Filter = $"Supported Files ({fileFilters})|{fileFilters}";
            openFileDialog.Multiselect = true;

            // Display OpenFileDialog by calling ShowDialog method
            bool? result = openFileDialog.ShowDialog();

            // Get the selected file name and display in a TextBox

            if (result == true)
            {
                // Open document
                var fileNames = openFileDialog.FileNames;
                InArgument<string> inArgument = new InArgument<string>(String.Join("|",fileNames));
                ModelItem.Properties["Document"].SetValue(inArgument);
                //ExpressionTextBox fileSelectionText = _contentPresenter.ContentTemplate.FindName("fileSelection",
                //                                      _contentPresenter) as ExpressionTextBox;
                //fileSelectionText.Content = $"\"{filename}\"";
            }
        }
        #endregion
    }
}