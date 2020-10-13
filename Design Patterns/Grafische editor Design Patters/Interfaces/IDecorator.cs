using Grafische_editor_Design_Patters.Figures;
using System.Collections.Generic;

namespace Design_Patters_Jaar2
{
    /// <summary>
    /// Interface for decorator patern
    /// </summary>
    interface IDec
    {
        void Decorate(Figure F, string text);
    }
}
