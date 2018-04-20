using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace UiTesting.Source
{
    public class ContainerPanel : Entity
    {
        public ContainerPanel(EntityProps entityProps) : base(entityProps)
        {
            ClickThrough = true;
            IsTargetable = false;
        }

        public override bool IsNaturallyInteractable()
        {
            return false;
        }
    }
}
