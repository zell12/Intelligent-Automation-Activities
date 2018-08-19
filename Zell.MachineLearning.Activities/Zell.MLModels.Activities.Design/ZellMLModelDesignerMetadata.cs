using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zell.MLModels.Activities.Design
{
    public sealed class ZellMLModelDesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            RegisterAll();
        }

        public static void RegisterAll()
        {
            var builder = new AttributeTableBuilder();
            EmailTicketClassifierDesigner.RegisterMetadata(builder);
            CreditsMlModelDesigner.RegisterMetadata(builder);
            // TODO: Other activities can be added here
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
