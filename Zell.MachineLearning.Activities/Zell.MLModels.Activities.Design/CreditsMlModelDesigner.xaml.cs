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
    public partial class CreditsMlModelDesigner
    {
        public CreditsMlModelDesigner()
        {
            InitializeComponent();
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            // Text translator attribute
            string description = "v1 - Credit Risk models play a key role in the assessment of two main risk drivers. 1)Willingness to pay and 2) Ability to pay. Credit scoring algorithms, which make a guess at the probability of default, are the method banks use to determine whether or not a loan should be granted";
            builder.AddCustomAttributes(typeof(CreditGrantAssessor), new DesignerAttribute(typeof(CreditsMlModelDesigner)));
            builder.AddCustomAttributes(typeof(CreditGrantAssessor), new DescriptionAttribute(description));
            builder.AddCustomAttributes(typeof(CreditGrantAssessor), new CategoryAttribute(Properties.Resources.MachineLearningModelActivitesCategory));
        }
    }
}
