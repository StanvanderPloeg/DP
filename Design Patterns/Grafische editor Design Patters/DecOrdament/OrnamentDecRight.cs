using Grafische_editor_Design_Patters.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patters_Jaar2
{
    class OrnamentDecRight : IDec
    {
        public void Decorate(Figure F, string text)
        {
            Ornament Or = new Ornament(F.DepPat, text, "Right", F.Fig);
            F.Ornaments.Add(Or);
        }
    }
}
