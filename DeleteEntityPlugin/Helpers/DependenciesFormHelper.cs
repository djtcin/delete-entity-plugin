using DeleteEntityPlugin.Common;
using DeleteEntityPlugin.Entities;
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
        private List<Dependency> Dependencies;

        public DependenciesFormHelper(Panel panelDependencies)
        {
            this.PanelDependencies = panelDependencies;
        }

        public void InsertDependencies(List<Dependency> dependencies, bool addChangeRelationshipName = false)
        {
            this.Dependencies = dependencies;
            this.PanelDependencies.Controls.Clear();
            List<Label> labels = new List<Label>();
            int currentY = 9;
            int diffY = 33;

            for (int i = 0; i < dependencies.Count; i++)
            {
                this.AddLine(dependencies[i], currentY, i);
                currentY += diffY;
            }
        }

        public void AddNoDependencyMsg()
        {
            var labelNoDependency = new Label();
            labelNoDependency.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            labelNoDependency.AutoSize = true;
            labelNoDependency.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelNoDependency.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            labelNoDependency.Location = new System.Drawing.Point(260, 33);
            labelNoDependency.Name = "labelNoDependency";
            labelNoDependency.Size = new System.Drawing.Size(139, 17);
            labelNoDependency.TabIndex = 0;
            labelNoDependency.Text = "No dependencies ";
            this.PanelDependencies.Controls.Add(labelNoDependency);
        }

        private void AddLine(Dependency dependency, int positionY, int index)
        {
            var text = EnumHelper.GetEnumDescription((dependency.DependentComponentTypeValue));
            Label label = new Label();
            label.AutoSize = false;
            label.Location = new System.Drawing.Point(3, positionY);
            label.Name = "labelDependency" + index;
            label.Size = new System.Drawing.Size(241, 23);
            label.TabIndex = 0;

            if (dependency.ObjectEntity == null)
            {
                label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                text += " - Dependency not supported yet";
                label.AutoSize = true;
            } else
            {
                text += " - " + dependency.ObjectEntity.Name;
            }

            label.Text = text;
            this.PanelDependencies.Controls.Add(label);
        }
    }
}
