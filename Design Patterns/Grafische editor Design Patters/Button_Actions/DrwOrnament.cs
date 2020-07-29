using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Patters_Jaar2
{
    /**
     *  CommandI for Drawing Ornament on Canvas
     *  Execute function handles everyting regarding drawing of the ornament
     */
    class DrwOrnament : CommandI
    {
        private readonly Figure Fig;
        private readonly string Txt;
        IDec Dec;

        /*
         * Ornament consist out of a Txt, Figure and Decorator
         */
        public DrwOrnament(Figure F, string T, IDec D)
        {
            Fig = F; 
            Txt = T;
            Dec = D;
        }
        public void Execute()
        {
            Dec.Decorate(Fig,Txt);
        }
    }
}
