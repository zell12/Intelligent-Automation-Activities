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
using Zell.MachineLearningModels;

namespace Zell.MLModels.Activities.Design
{
    /// <summary>
    /// Interaction logic for EmailTicketClassifierDesigner.xaml
    /// </summary>
    public partial class EmailTicketClassifierDesigner
    {
        #region Constants
        /// <summary>
        /// Description showing on the activity explorer
        /// </summary>
        private const string activityDescription = "v11 - Email classification experiment to assign an email to one or more class(es) of predefined set of classes or work queues.";
        #endregion

        #region Constructor
        /// <summary>
        /// Component initialization
        /// </summary>
        public EmailTicketClassifierDesigner()
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
            builder.AddCustomAttributes(typeof(EmailTicketClassifier), new DesignerAttribute(typeof(EmailTicketClassifierDesigner)));
            builder.AddCustomAttributes(typeof(EmailTicketClassifier), new DescriptionAttribute(activityDescription));
            builder.AddCustomAttributes(typeof(EmailTicketClassifier), new CategoryAttribute(Properties.Resources.MachineLearningModelActivitesCategory));
        }
        #endregion
    }
}
