using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace DeleteEntityPlugin.Entities
{
    public class Dependency
    {
        public enum ComponentType
        {
            [Description("Entity")]
            Entity = 1,
            [Description("Attribute")]
            Attribute = 2,
            [Description("Relationship")]
            Relationship = 3,
            [Description("Attribute Picklist Value")]
            AttributePicklistValue = 4,
            [Description("Attribute Lookup Value")]
            AttributeLookupValue = 5,
            [Description("View Attribute")]
            ViewAttribute = 6,
            [Description("Localized Label")]
            LocalizedLabel = 7,
            [Description("Relationship Extra Condition")]
            RelationshipExtraCondition = 8,
            [Description("Option Set")]
            OptionSet = 9,
            [Description("Entity Relationship")]
            EntityRelationship = 10,
            [Description("Entity Relationship Role")]
            EntityRelationshipRole = 11,
            [Description("Entity Relationship Relationships")]
            EntityRelationshipRelationships = 12,
            [Description("Managed Property")]
            ManagedProperty = 13,
            [Description("Entity Key")]
            EntityKey = 14,
            [Description("Privilege")]
            Privilege = 16,
            [Description("PrivilegeObjectTypeCode")]
            PrivilegeObjectTypeCode = 17,
            [Description("Role")]
            Role = 20,
            [Description("Role Privilege")]
            RolePrivilege = 21,
            [Description("Display String")]
            DisplayString = 22,
            [Description("Display String Map")]
            DisplayStringMap = 23,
            [Description("Form")]
            Form = 24,
            [Description("Organization")]
            Organization = 25,
            [Description("Saved Query")]
            SavedQuery = 26,
            [Description("Workflow")]
            Workflow = 29,
            [Description("Report")]
            Report = 31,
            [Description("Report Entity")]
            ReportEntity = 32,
            [Description("Report Category")]
            ReportCategory = 33,
            [Description("Report Visibility")]
            ReportVisibility = 34,
            [Description("Attachment")]
            Attachment = 35,
            [Description("Email Template")]
            EmailTemplate = 36,
            [Description("Contract Template")]
            ContractTemplate = 37,
            [Description("KB Article Template")]
            KBArticleTemplate = 38,
            [Description("Mail Merge Template")]
            MailMergeTemplate = 39,
            [Description("Duplicate Rule")]
            DuplicateRule = 44,
            [Description("Duplicate Rule Condition")]
            DuplicateRuleCondition = 45,
            [Description("Entity Map")]
            EntityMap = 46,
            [Description("Attribute Map")]
            AttributeMap = 47,
            [Description("Ribbon Command")]
            RibbonCommand = 48,
            [Description("Ribbon Context Group")]
            RibbonContextGroup = 49,
            [Description("Ribbon Customization")]
            RibbonCustomization = 50,
            [Description("Ribbon Rule")]
            RibbonRule = 52,
            [Description("Ribbon Tab To Command Map")]
            RibbonTabToCommandMap = 53,
            [Description("Ribbon Diff")]
            RibbonDiff = 55,
            [Description("Saved Query Visualization")]
            SavedQueryVisualization = 59,
            [Description("System Form")]
            SystemForm = 60,
            [Description("Web Resource")]
            WebResource = 61,
            [Description("Site Map")]
            SiteMap = 62,
            [Description("Connection Role")]
            ConnectionRole = 63,
            [Description("Complex Control")]
            ComplexControl = 64,
            [Description("Field Security Profile")]
            FieldSecurityProfile = 70,
            [Description("Field Permission")]
            FieldPermission = 71,
            [Description("Plugin Type")]
            PluginType = 90,
            [Description("Plugin Assembly")]
            PluginAssembly = 91,
            [Description("SDK Message Processing Step")]
            SDKMessageProcessingStep = 92,
            [Description("SDK Message Processing Step Image")]
            SDKMessageProcessingStepImage = 93,
            [Description("Service Endpoint")]
            ServiceEndpoint = 95,
            [Description("Routing Rule")]
            RoutingRule = 150,
            [Description("Routing Rule Item")]
            RoutingRuleItem = 151,
            [Description("SLA")]
            SLA = 152,
            [Description("SLA Item")]
            SLAItem = 153,
            [Description("Convert Rule")]
            ConvertRule = 154,
            [Description("Convert Rule Item")]
            ConvertRuleItem = 155,
            [Description("Hierarchy Rule")]
            HierarchyRule = 65,
            [Description("Mobile Offline Profile")]
            MobileOfflineProfile = 161,
            [Description("Mobile Offline Profile Item")]
            MobileOfflineProfileItem = 162,
            [Description("Similarity Rule")]
            SimilarityRule = 165,
            [Description("Custom Control")]
            CustomControl = 66,
            [Description("Custom Control Default Config")]
            CustomControlDefaultConfig = 68,
            [Description("Data Source Mapping")]
            DataSourceMapping = 166,
            [Description("SDKMessage")]
            SDKMessage = 201,
            [Description("SDKMessageFilter")]
            SDKMessageFilter = 202,
            [Description("SdkMessagePair")]
            SdkMessagePair = 203,
            [Description("SdkMessageRequest")]
            SdkMessageRequest = 204,
            [Description("SdkMessageRequestField")]
            SdkMessageRequestField = 205,
            [Description("SdkMessageResponse")]
            SdkMessageResponse = 206,
            [Description("SdkMessageResponseField")]
            SdkMessageResponseField = 207,
            [Description("WebWizard")]
            WebWizard = 210,
            [Description("Index")]
            Index = 18,
            [Description("Import Map")]
            ImportMap = 208,
        }
               
        private Entity DependencyEntity;
        public DependentEntity ObjectEntity { get; set; }

        public Dependency(Entity entity)
        {
            this.DependencyEntity = entity;
        }

        public const string EntityLogicalName = "dependency";

       public OptionSetValue DependencyType
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<OptionSetValue>("dependencytype");
            }
        }

        public Guid? DependentComponentBaseSolutionId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<Guid>>("dependentcomponentbasesolutionid");
            }
        }

        public EntityReference DependentComponentNodeId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<EntityReference>("dependentcomponentnodeid");
            }
        }

        public Nullable<Guid> DependentComponentObjectId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<Guid>>("dependentcomponentobjectid");
            }
        }

        public Nullable<Guid> DependentComponentParentId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<Guid>>("dependentcomponentparentid");
            }
        }

        public OptionSetValue DependentComponentType
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<OptionSetValue>("dependentcomponenttype");
            }
        }

        public ComponentType DependentComponentTypeValue
        {
            get
            {
                return (ComponentType) this.DependentComponentType.Value;
            }
        }

        public Nullable<Guid> RequiredComponentBaseSolutionId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<Guid>>("requiredcomponentbasesolutionid");
            }
        }

        public Nullable<double> RequiredComponentIntroducedVersion
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<double>>("requiredcomponentintroducedversion");
            }
        }

        public EntityReference RequiredComponentNodeId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<EntityReference>("requiredcomponentnodeid");
            }
        }

        public System.Nullable<Guid> RequiredComponentObjectId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<Guid>>("requiredcomponentobjectid");
            }
        }

        public Nullable<Guid> RequiredComponentParentId
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<Guid>>("requiredcomponentparentid");
            }
        }

        public OptionSetValue RequiredComponentType
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<OptionSetValue>("requiredcomponenttype");
            }
        }

        public Nullable<long> VersionNumber
        {
            get
            {
                return this.DependencyEntity.GetAttributeValue<Nullable<long>>("versionnumber");
            }
        }
    }

}
