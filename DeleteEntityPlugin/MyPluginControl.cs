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

namespace DeleteEntityPlugin
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Settings mySettings;
        private EntityService EntityService;
        private EntityMetadata SelectedEntity;
        private Actions.ActionType SelectedAction;
        private DependenciesFormHelper dependencieFormHelper;

        public MyPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            ExecuteMethod(WhoAmI);
            this.EntityService = new EntityService(Service);
            this.dependencieFormHelper = new DependenciesFormHelper(this.panelDependencies);
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
                        this.cmbEntities.BeginUpdate();
                        this.cmbEntities.Items.Clear();
                        foreach (EntityMetadata entity in result)
                        {
                            var localizedLabel = entity.DisplayName.UserLocalizedLabel;
                            this.cmbEntities.Items.Add(
                                new ComboItem<EntityMetadata>(
                                    (localizedLabel != null ? localizedLabel.Label : "N/A") + " (" + entity.LogicalName + ")",
                                    entity
                                )
                            );
                        }
                        this.cmbEntities.EndUpdate();
                        this.cmbEntities.Enabled = true;
                        
                    }
                }
            });
        }

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

        private void cmbActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbActions.SelectedItem != null)
            {
                this.btnNext.Enabled = true;
                this.SelectedAction = ((ComboItem<Actions.ActionType>)this.cmbActions.SelectedItem).Value;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.cmbEntities.Enabled = false;
            this.cmbActions.Enabled = false;
            // TODO
            //this.btnNext.Enabled = false;
            
            switch(this.SelectedAction)
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

        private void LoadEntityDependencies()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Looking for dependencies",
                Work = (worker, args) =>
                {
                    args.Result = this.EntityService.GetEntityDependencies(this.SelectedEntity);
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
                        this.dependencieFormHelper.InsertDependencies(dependencies, false);
                    }
                    else
                    {
                        // TODO
                    }
                }
            });
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
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

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           // this.tableLayoutPanel1.ro
        }
    }
}