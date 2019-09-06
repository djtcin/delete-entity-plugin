using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DeleteEntityPlugin.Entities
{
    class SystemForm : DependentEntity
    {
        enum GridStatus
        {
            Free, ComponentCell, EmptyCell
        }

        public Entity Entity;
        public string EntityLogicalName;

        public SystemForm(Entity entity)
        {
            this.EntityLogicalName = "systemform";
            this.Entity = entity;
            this.Name = entity.GetAttributeValue<string>("name");
        }

        public static SystemForm GetEntity(IOrganizationService service, Guid id)
        {
            var entity = service.Retrieve("systemform", id, new ColumnSet(new string[] { "formid", "name", "formxml" }));
            return new SystemForm(entity);
        }

        public override void ResolveDependency(IOrganizationService service, string entityLogicalName)
        {
            var xml = new XmlDocument();
            xml.LoadXml(this.Entity.GetAttributeValue<string>("formxml"));
            var sections = xml.SelectNodes("//section");
            var footer = xml.SelectSingleNode("//footer");
            var header = xml.SelectSingleNode("//header");
            var matchControlXPath = "control[contains(parameters/TargetEntityType, '" + entityLogicalName + "')]";
            var languagecode = xml.SelectSingleNode("//label").Attributes["languagecode"].Value;

            for (int i = 0; i < sections.Count; i++)
            {
                this.RemoveControlFromContainer(sections.Item(i), matchControlXPath, xml, languagecode);
            }
            if (footer != null)
            {
                this.RemoveControlFromContainer(footer, matchControlXPath, xml, languagecode);
            }
            if (header != null)
            {
                this.RemoveControlFromContainer(header, matchControlXPath, xml, languagecode);
            }

            this.Entity.Attributes["formxml"] = xml.InnerXml;
            service.Update(this.Entity);
        }

        private void RemoveControlFromContainer(XmlNode section, string matchControlXPath, XmlDocument xmlDoc, string languagecode)
        {
            var columnsAttribute = section.Attributes["columns"];
            var columnsCount = columnsAttribute != null ? columnsAttribute.Value.Length : 1;
            var rows = section.SelectNodes("rows/row");
            var cells = this.GetCellsList(rows, matchControlXPath);

            if (!cells.Any(c => c.ShouldBeDeleted))
            {
                return;
            }

            var grid = this.GetGrid(cells, rows.Count, columnsCount);

            cells.ForEach(c =>
            {
                if (c.ShouldBeDeleted)
                {
                    this.RemoveCellFromGrid(grid, c);
                } 
                else if (!c.IsEmpty)
                {
                    this.MoveCellUpOnGrid(grid, c);
                }
            });

            cells.RemoveAll(c => c.ShouldBeDeleted);

            var rowsNode = section.SelectSingleNode("rows");
            this.CreateNewRows(rowsNode, cells, grid, xmlDoc, languagecode);
        }

        private void CreateNewRows(XmlNode rowsNode, List<Cell> cells, List<List<GridStatus>> grid, XmlDocument xmlDoc, string languagecode)
        {
            rowsNode.RemoveAll();

            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].Any(g => g == GridStatus.ComponentCell) || i == 0)
                {
                    XmlNode row = xmlDoc.CreateElement("row");
                    rowsNode.AppendChild(row);
                    
                    for(int j = 0; j < grid[i].Count; j++)
                    {
                        if (grid[i][j] == GridStatus.ComponentCell)
                        {
                            var cell = cells.Find(c => c.Row == i && c.Column == j && !c.IsEmpty);
                            if (cell != null)
                            {
                                row.AppendChild(cell.Node);
                            }
                        } 
                        else
                        {
                            row.AppendChild(this.CreateEmptyCell(xmlDoc, languagecode));
                        }
                    }
                }
                else
                {
                    break;
                }
            }

        }

        private XmlNode CreateEmptyCell(XmlDocument xmlDoc, string languagecode)
        {
            var cell = xmlDoc.CreateElement("cell");
            cell.SetAttribute("id", Guid.NewGuid().ToString());
            cell.SetAttribute("showlabel", "false");
            cell.InnerXml = "<labels><label description =\"\" languagecode=\"" + languagecode + "\"/></labels>";
            return cell;
        }

        private List<Cell> GetCellsList(XmlNodeList rows, string matchControlXPath)
        {
            var cells = new List<Cell>();

            for (int i = 0; i < rows.Count; i++)
            {
                var cellNodes = rows.Item(i).SelectNodes("cell");

                for (int j = 0; j < cellNodes.Count; j++)
                {
                    var cellNode = cellNodes.Item(j);
                    var isEmpty = cellNode.SelectSingleNode("control") == null && cellNode.Attributes["userspacer"] == null;
                    var colSpanAttribute = cellNode.Attributes["colspan"];
                    var rowSpanAttribute = cellNode.Attributes["rowspan"];
                    var colSpan = colSpanAttribute != null ? Int32.Parse(colSpanAttribute.Value) : 1;
                    var rowSpan = rowSpanAttribute != null ? Int32.Parse(rowSpanAttribute.Value) : 1;

                    cells.Add(new Cell()
                    {
                        Node = cellNode,
                        Row = i,
                        ShouldBeDeleted = cellNode.SelectSingleNode(matchControlXPath) != null,
                        ColSpan = colSpan,
                        RowSpan = rowSpan,
                        IsEmpty = isEmpty
                    });
                }
            }

            return cells;
        }

        private List<List<GridStatus>> GetGrid(List<Cell> cells, int rowsCount, int columnsCount)
        {
            var grid = new List<List<GridStatus>>();
            for(int i = 0; i < rowsCount; i++)
            {
                grid.Add(new List<GridStatus>());
                for (int j = 0; j < columnsCount; j++)
                    grid[i].Add(GridStatus.Free);
            }

            cells.ForEach((cell) => {
                cell.Column = grid[cell.Row].IndexOf(GridStatus.Free);
                this.PlaceCellOnGrid(grid, cell);
            });

            return grid;
        }

        private void PlaceCellOnGrid(List<List<GridStatus>> grid, Cell cell)
        {
            for(int i = cell.Row; i < cell.Row + cell.RowSpan; i++)
            {
                for(int j = cell.Column; j < cell.Column + cell.ColSpan; j++)
                {
                    grid[i][j] = cell.IsEmpty ? GridStatus.EmptyCell : GridStatus.ComponentCell;
                }
            }
        }

        private void RemoveCellFromGrid(List<List<GridStatus>> grid, Cell cell)
        {
            for (int i = cell.Row; i < cell.Row + cell.RowSpan; i++)
            {
                for (int j = cell.Column; j < cell.Column + cell.ColSpan; j++)
                {
                    grid[i][j] = GridStatus.Free;
                }
            }
        }

        private void MoveCellUpOnGrid(List<List<GridStatus>> grid, Cell cell)
        {
            var newRow = cell.Row;

            while (newRow-1 >= 0 && grid[newRow-1].Skip(cell.Column).Take(cell.ColSpan).All(c => c != GridStatus.ComponentCell))
            {
                newRow -= 1;
            }

            if (newRow != cell.Row)
            {
                this.RemoveCellFromGrid(grid, cell);
                cell.Row = newRow;
                this.PlaceCellOnGrid(grid, cell);
            }
        }

        protected class Cell
        {
            public XmlNode Node;
            public int Row;
            public int Column;
            public bool ShouldBeDeleted;
            public int ColSpan;
            public int RowSpan;
            public bool IsEmpty;
        }
    }
}

