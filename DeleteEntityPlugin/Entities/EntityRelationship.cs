using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static DeleteEntityPlugin.Entities.Dependency;
using Microsoft.Xrm.Sdk.Metadata;

namespace DeleteEntityPlugin.Entities
{
    class EntityRelationship : DependentEntity
    {
        public RelationshipMetadataBase RelationshipMetadata;

        public EntityRelationship(RelationshipMetadataBase relationship)
        {
            this.RelationshipMetadata = relationship;
            this.Name = relationship.SchemaName;
            this.HasSubdependencies = true;
        }

        public static EntityRelationship GetEntity(IOrganizationService service, Guid id)
        {
            RetrieveRelationshipResponse response = (RetrieveRelationshipResponse) service.Execute(
                new RetrieveRelationshipRequest(){ MetadataId = id }
            );
            return new EntityRelationship(response.RelationshipMetadata);
        }

        public override void ResolveDependency(IOrganizationService service, string entityLogicalName)
        {
            
        }
    }
}

