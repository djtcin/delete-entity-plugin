using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace DeleteEntityPlugin.Entities
{
    public abstract class  DependentEntity
    {
        public string Name;
        public bool HasSubdependencies;

        public abstract void ResolveDependency(IOrganizationService service, string entityLogicalName);
    }
}
