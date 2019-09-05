using DeleteEntityPlugin.Common;
using DeleteEntityPlugin.Entities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteEntityPlugin.Services
{
    class DependencyService
    {
        private IOrganizationService Service;

        public DependencyService(IOrganizationService service)
        {
            this.Service = service;
        }

        public bool IsObjectTypeSupported(Dependency.ComponentType objectType)
        {
            var supportedTypes = new Dependency.ComponentType[] { 
                Dependency.ComponentType.SystemForm
            };
            return supportedTypes.Contains(objectType);
        }

        public DependentEntity GetDependencyObject(Dependency dependency)
        {
            var objectId = dependency.DependentComponentObjectId.Value;

            switch (dependency.DependentComponentTypeValue)
            {
                case Dependency.ComponentType.SystemForm:
                    return SystemForm.GetEntity(this.Service, objectId);
            }

            return null;
        }

     /*   public void SolveDependency(DependentEntity dependentEntity)
        {
            switch (dependency.DependentComponentTypeValue)
            {
                case Dependency.ComponentType.SystemForm:
                    return SystemForm.ResolveDependency(this.Service, objectId);
            }
        }*/
    }
}
