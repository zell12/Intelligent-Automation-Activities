using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zell.MLModels.Activities.Design
{
    /// <summary>
    /// Metadata class for machine learning models
    /// </summary>
    public sealed class ZellMLModelDesignerMetadata : IRegisterMetadata
    {
        #region Public Methods
        /// <summary>
        /// Main register method
        /// </summary>
        public void Register()
        {
            RegisterAll();
        }

        /// <summary>
        /// Internal method to register each enclosed activity designer to metadata class
        /// </summary>
        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            EmailTicketClassifierDesigner.RegisterMetadata(builder);
            CreditsMlModelDesigner.RegisterMetadata(builder);
            // TODO: Other activities can be added here
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        #endregion
    }
}