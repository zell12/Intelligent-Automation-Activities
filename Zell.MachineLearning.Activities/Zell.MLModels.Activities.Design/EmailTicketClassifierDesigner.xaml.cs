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
    // Interaction logic for ActivityDesigner1.xaml
    public partial class EmailTicketClassifierDesigner
    {
        public EmailTicketClassifierDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            // Text translator attribute
            string description = "v11 - Email classification experiment to assign an email to one or more class(es) of predefined set of classes or work queues.";
            builder.AddCustomAttributes(typeof(EmailTicketClassifier), new DesignerAttribute(typeof(EmailTicketClassifierDesigner)));
            builder.AddCustomAttributes(typeof(EmailTicketClassifier), new DescriptionAttribute(description));
            builder.AddCustomAttributes(typeof(EmailTicketClassifier), new CategoryAttribute(Properties.Resources.MachineLearningModelActivitesCategory));
        }
    }
}
