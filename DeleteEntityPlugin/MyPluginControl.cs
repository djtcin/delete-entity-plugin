using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using DeleteEntityPlugin.Services;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Messages;
using XrmToolBox.Extensibility.Interfaces;
using DeleteEntityPlugin.Common;
using DeleteEntityPlugin.Helpers;
using DeleteEntityPlugin.Entities;

namespace DeleteEntityPlugin
{
    public partial class MyPluginControl : PluginControlBase
    {
        #region Properties

        private Settings mySettings;
        private EntityMetadata SelectedEntity;
        private Actions.ActionType SelectedAction;
        private List<ComboItem<EntityMetadata>> EntitiesMetadataItems;
        private List<Dependency> Dependencies;

        private EntityService EntityService;
        private DependenciesFormHelper DependencieFormHelper;
        private DependencyService DependencyService;

        #endregion Properties

        #region XrmToolBox default methods

        public MyPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ExecuteMethod(WhoAmI);
            this.EntityService = new EntityService(Service);
            this.DependencieFormHelper = new DependenciesFormHelper(this.panelDependencies);
            this.DependencyService = new DependencyService(this.Service);
            ExecuteMethod(LoadEntities);
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            // ExecuteMethod(LoadEntities);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }


        #endregion

        #region Plugin Methods

        private void WhoAmI()
        {
            Service.Execute(new WhoAmIRequest());
        }

        private void LoadEntities()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting entities",
                Work = (worker, args) =>
                {
                    args.Result = this.EntityService.GetCustomizableEntities();
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as List<EntityMetadata>;
                    if (result != null)
                    {
                        this.EntitiesMetadataItems = new List<ComboItem<EntityMetadata>>();
                        this.cmbEntities.BeginUpdate();
                        this.cmbEntities.Items.Clear();

                        foreach (EntityMetadata entity in result)
                        {
                            var localizedLabel = entity.DisplayName.UserLocalizedLabel;
                            var item = new ComboItem<EntityMetadata>(
                                (localizedLabel != null ? localizedLabel.Label : "N/A") + " (" + entity.LogicalName + ")",
                                entity
                            );
                            this.EntitiesMetadataItems.Add(item);
                            this.cmbEntities.Items.Add(item);
                        }

                        this.cmbEntities.EndUpdate();
                        this.cmbEntities.Enabled = true;
                        
                    }
                }
            });
        }

        private void LoadEntityDependencies()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Looking for dependencies",
                Work = (worker, args) =>
                {
                    List<Dependency> dependencies = this.DependencyService.GetDependencies(this.SelectedEntity.MetadataId.Value, Dependency.ComponentType.Entity);
                    this.Dependencies = dependencies;
                    args.Result = dependencies;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var dependencies = args.Result as List<Dependency>;
                    if (dependencies.Any())
                    {
                        this.DependencieFormHelper.InsertDependencies(dependencies, false);
                        if (dependencies.TrueForAll(d => d.ObjectEntity != null))
                        {
                            this.btnExecute.Enabled = true;
                        }
                    }
                    else
                    {
                        this.DependencieFormHelper.AddNoDependencyMsg();
                        this.btnExecute.Enabled = true;
                    }
                }
            });
        }

        private void ExecuteAction()
        {
            var message = "";
            switch (this.SelectedAction)
            {
                case Actions.ActionType.DeleteEntity:
                    message = "Deleting entity";
                    break;
                case Actions.ActionType.RenameEntity:
                    message = "Renaming entity";
                    break;
                case Actions.ActionType.DeleteAttribute:
                    message = "Deleting attribute";
                    break;
                case Actions.ActionType.RenameAttribute:
                    message = "Renaming attribute";
                    break;
            }
            WorkAsync(new WorkAsyncInfo
            {
                Message = message,
                Work = (worker, args) =>
                {
                    this.Dependencies.ForEach((dependency) => {
                        dependency.ObjectEntity.ResolveDependency(this.Service, SelectedEntity.SchemaName);
                    });
                    // TODO main action
                },
                PostWorkCallBack = (args) =>
                {
                    // TODO
                }
            });
        }

        #endregion

        #region Components actions

        #region Combobox entities

        private void cmbEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbEntities.SelectedItem != null)
            {
                var entityMetadata = ((ComboItem<EntityMetadata>)this.cmbEntities.SelectedItem).Value;
                this.cmbActions.BeginUpdate();
                this.cmbActions.Items.Clear();
                this.cmbActions.Items.AddRange(
                    entityMetadata.IsCustomEntity.Value ?
                    Actions.GetCustomActions() :
                    Actions.GetNotCustomActions()
                );
                this.cmbActions.EndUpdate();
                this.cmbActions.Enabled = true;
                this.SelectedEntity = entityMetadata;
            }
        }

        private void cmbEntities_TextUpdate(object sender, EventArgs e)
        {
            string filterParam = this.cmbEntities.Text;

            List<ComboItem<EntityMetadata>> filteredItems = this.EntitiesMetadataItems.FindAll(item => 
                item.DisplayName.Contains(filterParam)
            );

            if (String.IsNullOrWhiteSpace(filterParam))
            {
                this.cmbEntities.DataSource = this.EntitiesMetadataItems;
            } 
            else
            {
                this.cmbEntities.DataSource = filteredItems;
            }

            this.cmbEntities.DroppedDown = true;

            // this will ensure that the drop down is as long as the list
            this.cmbEntities.IntegralHeight = true;

            // remove automatically selected first item
            //this.cmbEntities.SelectedIndex = -1;

            this.cmbEntities.Text = filterParam;

            // set the position of the cursor
            this.cmbEntities.SelectionStart = filterParam.Length;
            this.cmbEntities.SelectionLength = 0;
        }

        #endregion

        #region Combobox actions

        private void cmbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbActions.SelectedItem != null)
            {
                this.btnNext.Enabled = true;
                this.SelectedAction = ((ComboItem<Actions.ActionType>)this.cmbActions.SelectedItem).Value;
            }
        }

        #endregion

        #region Buttons

        private void btnNext_Click(object sender, EventArgs e)
        {
            // TODO
            //this.cmbEntities.Enabled = false;
            //this.cmbActions.Enabled = false;
            //this.btnNext.Enabled = false;

            switch (this.SelectedAction)
            {
                case Actions.ActionType.DeleteEntity:
                    ExecuteMethod(LoadEntityDependencies);
                    break;
                case Actions.ActionType.RenameEntity:
                    ExecuteMethod(LoadEntityDependencies);
                    break;
                    // TODO case attribute
            }
        }

        private void BtnExecute_Click(object sender, EventArgs e)
        {
            // TODO more msgs
            var confirmResult = MessageBox.Show("Are you sure to delete this entity? All data will be lost.",
                                     "Confirm",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                ExecuteMethod(ExecuteAction);
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }


        #endregion

        #endregion

    }
}