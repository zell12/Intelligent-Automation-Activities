using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zell.CognitiveServices.Activities.Design
{
    /// <summary>
    /// Metadata class for cognitive services
    /// </summary>
    public sealed class ZellCognitiveServicesMetadata : IRegisterMetadata
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
            TextTranslatorDesigner.RegisterMetadata(builder);
            DocumentTranslatorDesigner.RegisterMetadata(builder);
            TextModeratorDesigner.RegisterMetadata(builder);
            // TODO: Other activities can be added here
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
        #endregion
    }
}