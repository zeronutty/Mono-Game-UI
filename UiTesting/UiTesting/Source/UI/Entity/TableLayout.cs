
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public class TableLayout : Entity
    {
        #region Fields

        internal int p_Column;
        internal int p_Row;
        internal int p_ElementWidth;
        internal int p_ElementHeight;

        private ContainerPanel[,] p_TableSlots;

        #endregion

        #region Properties

        #endregion

        #region Methods

        public TableLayout(TableLayoutProps tableLayoutProps) : base(tableLayoutProps)
        {
            p_Column = tableLayoutProps.Columns;
            p_Row = tableLayoutProps.Rows;

            p_TableSlots = new ContainerPanel[p_Column, p_Row];

            for (int x = 0; x < p_Row; x++)
            {
                for (int y = 0; y < p_Column; y++)
                {
                    p_TableSlots[x, y] = new ContainerPanel(new EntityProps { EntityAnchor = Anchor.TopLeft});
                    p_TableSlots[x, y].Padding = new Vector2(5, 5);
                    AddChild(p_TableSlots[x, y]);
                }
            }
        }

        public void CalculateQuadrants()
        {
            p_ElementWidth = p_InternalDrawArea.Width / p_Column;
            p_ElementHeight = p_InternalDrawArea.Height / p_Row;

            for (int x = 0; x < p_Column; x++)
            {
                for (int y = 0; y < p_Row; y++)
                {
                    p_TableSlots[x, y].Size = new Vector2(p_ElementWidth, p_ElementHeight);
                    p_TableSlots[x, y].SetOffset(new Vector2(x * p_ElementWidth, y * p_ElementHeight));
                }
            }
        }

        public void AddElement(Entity entity)
        {
            foreach(Entity e in p_TableSlots)
            {
                if(e.GetChildren().Count == 0)
                {
                    e.AddChild(entity);
                    return;
                }
            }
        }

        public void AddElementArray(Entity[,] entitiesToAdd)
        {
            if(entitiesToAdd.Length < p_TableSlots.Length)
            {
                for(int x = 0; x < entitiesToAdd.Length; x++)
                {
                    for (int y = 0; y < entitiesToAdd.Length; y++)
                    {
                        p_TableSlots[x, y].AddChild(entitiesToAdd[x, y]);
                    }
                }
            }
        }

        public void AddElementAtIndex(int x, int y, Entity entityToAdd)
        {
            if (x < p_Row && y < p_Column)
            {
                entityToAdd.Name = entityToAdd.Name + " Slot: " + (x * y);
                p_TableSlots[x, y].AddChild(entityToAdd);
            }
        }

        public void RemoveElementAtIndex(int x, int y, Entity entityToRemove)
        {
            if(x < p_Row && y < p_Column)
            {
                p_TableSlots[x, y].RemoveChild(entityToRemove);             
            }
        }

        public override void UpdateDestinationRects()
        {
            base.UpdateDestinationRects();

            CalculateQuadrants();
        }

        #endregion
    }
}
