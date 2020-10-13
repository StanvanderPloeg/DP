using Grafische_editor_Design_Patters.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Design_Patters_Jaar2
{
    /**
     * Control class for Commandpattern 
     * All Commands are being made here, kept updated and executed
     */
    class Invoker
    {
        private List<CommandI> ComI = new List<CommandI>();
        private int Counter = 0;
        private int CommandExecuted = 0;
        
        /**
         * Draw Ellipse on point and add add it to command List
         * 
         * Ellipse uses the following:
         * Point S - Start Point
         * Point E - End Point
         * Canvas C - Canvas
         * List<Figure> FA - Figures List
         */
        public void Ellipse(Point S, Point E, Canvas C, List<Component> FA)
        {
            DrwEll Ell = new DrwEll(S, E, C, FA);
            if (Counter <= ComI.Count()) {
                ComI.Insert(Counter, Ell);
            } else {
                ComI.Add(Ell);
            }
            Counter++;
        }

        /**
        * Draw Rectangle on point and add add it to command List
        * 
        * Rectangle uses the following:
        * Point S - Start Point
        * Point E - End Point
        * Canvas C - Canvas
        * List<Figure> FA - Figures List
        */
        public void Rectangle(Point S, Point E, Canvas C, List<Component> FA)
        {
            DrwRec Rec = new DrwRec(S, E, C, FA);
            if (Counter <= ComI.Count()) {
                ComI.Insert(Counter, Rec);
            } else {
                ComI.Add(Rec);
            }
            Counter++;
        }

        /**
         * Select Shapes on a certain point
         */
        public void SelectShape(Point S, Point E, Canvas C, List<Component> FA, ref List<Component> FS, Border B)
        {
            Selection Sh = new Selection(S, E, FA, ref FS, B);
            if (Counter <= ComI.Count()) {
                ComI.Insert(Counter, Sh);
            } else {
                ComI.Add(Sh);
            }
            Counter++;
        }

        /**
         *  Add GroupCommand to Command List
         */
        public void Group(Point S, Point E, List<Component> FA, ref List<Component> FS, Border B)
        {

            GroupCommand G = new GroupCommand(S, E, FS, FA, B);
            if (Counter <= ComI.Count()) {
                ComI.Insert(Counter, G);
            } else {
                ComI.Add(G);
            }
            Counter++;

        }

        /**
         *  Add UnGroup to Command List
         */
        public void DeGroup(Point S, Point E, List<Component> FA, ref List<Component> FS, Border B)
        {
            UnGroup Go = new UnGroup(S, E, FS, FA, B);
            if (Counter <= ComI.Count()) {
                ComI.Insert(Counter, Go);
            } else {
                ComI.Add(Go);
            }
            Counter++;
        }

        /**
         * Add Ornament on a Figure
         */
        public void AddOrnament(ref List<Component> FS, string Or, IDec Dec)
        {
            foreach (Figure F in FS)
            {
                DrwOrnament AddOr = new DrwOrnament(F, Or, Dec);
                if (Counter <= ComI.Count()) {
                    ComI.Insert(Counter, AddOr);
                } else {
                    ComI.Add(AddOr);
                }
                Counter++;
            }
        }

        /**
         * UNDO
         * Redo all commands except last one
         * 
         * Clear Canvas
         * Execute all Commands in ComI
         */
        public void Undo(Canvas C, List<Component> AF, Border SB, Border GB)
        {
            C.Children.Clear();
            C.Children.Add(SB);
            C.Children.Add(GB);
            AF.Clear();
            CommandExecuted = 0;
            if (Counter > 0) { Counter--; }
            for (int i = 0; i < Counter; i++) {
                ComI[i].Execute();
            }
        }

        /**
         * REDO
         * 
         * Execute all existing undo's 
         */
        public void Redo()
        {
            if (Counter < ComI.Count()) {
                Counter++;
                CommandExecuted++;
                ComI[Counter - 1].Execute();
            }
        }

        /**
         * Execute different commands in the ComI list.
         */
        public void ExecuteCommands()
        {
            while (CommandExecuted < Counter) {
                ComI[CommandExecuted].Execute();
                CommandExecuted++;
            }
        }

    }
}
