using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    interface IFig
    {
        void Draw(Shape S);
        Shape GetShape();
    }
}
