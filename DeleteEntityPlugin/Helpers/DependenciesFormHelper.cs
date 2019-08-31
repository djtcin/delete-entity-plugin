using DeleteEntityPlugin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeleteEntityPlugin.Helpers
{
    class DependenciesFormHelper
    {
        private Panel PanelDependencies;

        public DependenciesFormHelper(Panel panelDependencies)
        {
            this.PanelDependencies = panelDependencies;
        }

        public void InsertDependencies(List<Dependency> dependencies, bool addChangeRelationshipName)
        {
            this.PanelDependencies.Controls.Clear();
            List<Label> labels = new List<Label>();
            int currentY = 9;
            int diffY = 33;

            for (int i = 0; i < dependencies.Count; i++)
            {
                Dependency dependency = dependencies[i];
                Label label = new Label();
                label.AutoSize = false;
                label.Location = new System.Drawing.Point(3, currentY);
                label.Name = "labelDependency" + i;
                label.Size = new System.Drawing.Size(241, 23);
                label.TabIndex = 0;
                label.Text = "dependency" + i + " " + Enum.GetName(typeof(Dependency.ComponentType), dependency.DependentComponentType.Value);
                labels.Add(label);
                this.PanelDependencies.Controls.Add(label);
                currentY = currentY + diffY;



            }
        }
    }
}
