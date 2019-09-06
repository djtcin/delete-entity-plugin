using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Messages;

using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Crm.Sdk.Messages;
using DeleteEntityPlugin.Common;
using DeleteEntityPlugin.Entities;

namespace DeleteEntityPlugin.Services
{
    class EntityService
    {
        private IOrganizationService Service;
        const int ObjectTypeEntiy = 1;

        public EntityService(IOrganizationService service)
        {
            this.Service = service;
        }

        public List<EntityMetadata> GetCustomizableEntities()
        {
            var request = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Entity,
                RetrieveAsIfPublished = true
            };
            var response = (RetrieveAllEntitiesResponse)Service.Execute(request);
            var list = response.EntityMetadata.ToList<EntityMetadata>();

            list.Sort((e1, e2) => e1.LogicalName.CompareTo(e2.LogicalName));

            return list.Where(e1 => e1.IsCustomizable.Value).ToList();
        }
    }
}
