using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteEntityPlugin.Common
{
    public class Dependency
    {
        public enum ComponentType
        {
            Entity = 1,
            Attribute = 2,
            Relationship = 3,
            Form = 24,
            SavedQuery = 26,
            SystemForm = 60
        }



        private Microsoft.Xrm.Sdk.Entity Entity;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Dependency(Microsoft.Xrm.Sdk.Entity entity)
        {
            this.Entity = entity;
        }

        public const string EntityLogicalName = "dependency";

        /// <summary>
        /// The dependency type of the dependency.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dependencytype")]
        public Microsoft.Xrm.Sdk.OptionSetValue DependencyType
        {
            get
            {
                return this.Entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("dependencytype");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dependentcomponentbasesolutionid")]
        public System.Nullable<System.Guid> DependentComponentBaseSolutionId
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<System.Guid>>("dependentcomponentbasesolutionid");
            }
        }

        /// <summary>
        /// Unique identifier of the dependent component's node.
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dependentcomponentnodeid")]
        public Microsoft.Xrm.Sdk.EntityReference DependentComponentNodeId
        {
            get
            {
                return this.Entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("dependentcomponentnodeid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dependentcomponentobjectid")]
        public System.Nullable<System.Guid> DependentComponentObjectId
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<System.Guid>>("dependentcomponentobjectid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dependentcomponentparentid")]
        public System.Nullable<System.Guid> DependentComponentParentId
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<System.Guid>>("dependentcomponentparentid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("dependentcomponenttype")]
        public Microsoft.Xrm.Sdk.OptionSetValue DependentComponentType
        {
            get
            {
                return this.Entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("dependentcomponenttype");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("requiredcomponentbasesolutionid")]
        public System.Nullable<System.Guid> RequiredComponentBaseSolutionId
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<System.Guid>>("requiredcomponentbasesolutionid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("requiredcomponentintroducedversion")]
        public System.Nullable<double> RequiredComponentIntroducedVersion
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<double>>("requiredcomponentintroducedversion");
            }
        }

        /// <summary>
        /// Unique identifier of the required component's node
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("requiredcomponentnodeid")]
        public Microsoft.Xrm.Sdk.EntityReference RequiredComponentNodeId
        {
            get
            {
                return this.Entity.GetAttributeValue<Microsoft.Xrm.Sdk.EntityReference>("requiredcomponentnodeid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("requiredcomponentobjectid")]
        public System.Nullable<System.Guid> RequiredComponentObjectId
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<System.Guid>>("requiredcomponentobjectid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("requiredcomponentparentid")]
        public System.Nullable<System.Guid> RequiredComponentParentId
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<System.Guid>>("requiredcomponentparentid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("requiredcomponenttype")]
        public Microsoft.Xrm.Sdk.OptionSetValue RequiredComponentType
        {
            get
            {
                return this.Entity.GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValue>("requiredcomponenttype");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Microsoft.Xrm.Sdk.AttributeLogicalNameAttribute("versionnumber")]
        public System.Nullable<long> VersionNumber
        {
            get
            {
                return this.Entity.GetAttributeValue<System.Nullable<long>>("versionnumber");
            }
        }
    }

}
