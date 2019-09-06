using DeleteEntityPlugin.Common;
using DeleteEntityPlugin.Entities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Crm.Sdk.Messages;

namespace DeleteEntityPlugin.Services
{
    class DependencyService
    {
        private IOrganizationService Service;

        public DependencyService(IOrganizationService service)
        {
            this.Service = service;
        }

        public DependentEntity GetDependencyObject(Dependency dependency)
        {
            var objectId = dependency.DependentComponentObjectId.Value;

            switch (dependency.DependentComponentTypeValue)
            {
                case Dependency.ComponentType.SystemForm:
                    return SystemForm.GetEntity(this.Service, objectId);
                case Dependency.ComponentType.EntityRelationship:
                    return EntityRelationship.GetEntity(this.Service, objectId);
            }

            return null;
        }

        public List<Dependency> GetDependencies(Guid objectId, Dependency.ComponentType objectType)
        {
            RetrieveDependenciesForDeleteResponse response = (RetrieveDependenciesForDeleteResponse)Service.Execute(
                new RetrieveDependenciesForDeleteRequest() {
                    ObjectId = objectId,
                    ComponentType = (int)objectType
                }
            );

            List<Dependency> dependencies = response.EntityCollection.Entities.Select<Entity, Dependency>((entity) =>
            {
                var dependency = new Dependency(entity);
                dependency.ObjectEntity = this.GetDependencyObject(dependency);
                return dependency;
            }).ToList();
            List<Dependency> subDependencies = new List<Dependency>();

            dependencies.ForEach(d => {
                if (d.ObjectEntity?.HasSubdependencies == true)
                {
                    subDependencies.AddRange(this.GetDependencies(d.DependentComponentObjectId.Value, d.DependentComponentTypeValue));
                }
            });

            dependencies.AddRange(subDependencies);

            return dependencies;
        }
    }
}
