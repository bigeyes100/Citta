﻿using Citta_T1.Core;

namespace Citta_T1.Controls.MapViews
{
    class ChangeTextCommand : Command
    {
        private ITextObject TheObject;
        private string OldText;
        private string NewText;

        public ChangeTextCommand(ITextObject tobj, string newText)
        {
            TheObject = tobj;
            NewText = newText;
        }

        public override string Name
        {
            get { return "ChangeText"; }
        }

        public override bool Rollback()
        {
            TheObject.Text = OldText;
            return true;
        }

        public override bool Execute()
        {
            OldText = TheObject.Text;
            TheObject.Text = NewText;
            return true;
        }
    }
}