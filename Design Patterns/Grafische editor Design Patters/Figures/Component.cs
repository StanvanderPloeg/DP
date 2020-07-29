using Design_Patters_Jaar2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Grafische_editor_Design_Patters.Figures
{
    interface Component : IVisitable
    {
        void SetPosition(Point P);
        Point GetPosition();
        double GetWidth();
        double GetHeight();
        void Select();
        void Deselect();
        void Resize(Point Start, Point End);

    }
}
