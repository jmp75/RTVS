﻿using Microsoft.R.Core.AST.Definitions;
using Microsoft.R.Core.AST.Statements.Definitions;
using Microsoft.R.Core.Parser;

namespace Microsoft.R.Core.AST.Statements
{
    /// <summary>
    /// Represents statement that consists of a simgle semicolon.
    /// </summary>
    public class EmptyStatement : AstNode, IStatement
    {
        public TokenNode Semicolon { get; private set; }

        public override bool Parse(ParseContext context, IAstNode parent)
        {
            this.Semicolon = RParser.ParseToken(context, this);
            return base.Parse(context, parent);
        }
    }
}