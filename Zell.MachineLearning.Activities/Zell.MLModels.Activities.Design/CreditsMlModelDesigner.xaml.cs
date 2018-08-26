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
    /// Interaction logic for CreditsMlModelDesigner.xaml
    /// </summary>
    public partial class CreditsMlModelDesigner
    {
        #region Constants
        /// <summary>
        /// Description showing on the activity explorer
        /// </summary>
        private const string activityDescription = "v1 - Credit Risk models play a key role in the assessment of two main risk drivers. 1)Willingness to pay and 2) Ability to pay. Credit scoring algorithms, which make a guess at the probability of default, are the method banks use to determine whether or not a loan should be granted";
        #endregion

        #region Constructor
        /// <summary>
        /// Component initialization
        /// </summary>
        public CreditsMlModelDesigner()
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
            // Attributes
            builder.AddCustomAttributes(typeof(CreditGrantAssessor), new DesignerAttribute(typeof(CreditsMlModelDesigner)));
            builder.AddCustomAttributes(typeof(CreditGrantAssessor), new DescriptionAttribute(activityDescription));
            builder.AddCustomAttributes(typeof(CreditGrantAssessor), new CategoryAttribute(Properties.Resources.MachineLearningModelActivitesCategory));
        }
        #endregion
    }
}
