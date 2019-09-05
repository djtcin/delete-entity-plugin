using Microsoft.Xrm.Sdk;

namespace DeleteEntityPlugin.Entities
{
    public abstract class  DependentEntity
    {
        public string EntityLogicalName;
        public string Name;
        public Entity Entity;

        public abstract void ResolveDependency(IOrganizationService service, string entityLogicalName);
    }
}
