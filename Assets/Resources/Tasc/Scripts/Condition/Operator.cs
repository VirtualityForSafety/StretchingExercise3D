using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tasc
{
    public class Operator
    {
        public string name;
        public int code;

        public Operator(string name, int value)
        {
            this.name = name; this.code = value;
        }

        public int GetCode() { return code; }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (obj as Operator == null)
                throw new Exception("Error on Operation: type does not matched.");
            return code.Equals((obj as Operator).code);
        }
    }

    public class RelationalOperator: Operator
    {
        public static RelationalOperator Larger = new RelationalOperator("Larger",0);
        public static RelationalOperator LargerOrEqual = new RelationalOperator("LargerOrEqual", 1);
        public static RelationalOperator Equal = new RelationalOperator("Equal", 2);
        public static RelationalOperator SmallerOrEqual = new RelationalOperator("SmallerOrEqual", 3);
        public static RelationalOperator Smaller = new RelationalOperator("Smaller", 4);
        public static RelationalOperator NotEqual = new RelationalOperator("NotEqual", 5);

        public RelationalOperator(string name, int value) : base(name, value) { }

        public static RelationalOperator Code2Operator(int num)
        {
            if (num == 0)
                return Larger;
            else if (num == 1)
                return LargerOrEqual;
            else if (num == 2)
                return Equal;
            else if (num == 3)
                return SmallerOrEqual;
            else if (num == 4)
                return Smaller;
            else if (num == 5)
                return NotEqual;
            else
                return null;
        }

        public override string ToString()
        {
            if (code == 0)
                return ">";
            else if (code == 1)
                return ">=";
            else if (code == 2)
                return "=";
            else if (code == 3)
                return "<=";
            else if (code == 4)
                return "<";
            else if (code == 5)
                return "!=";
            else
                return null;
        }

        public static RelationalOperator Inverse(RelationalOperator ope)
        {
            return RelationalOperator.Code2Operator((ope.GetCode() + 3) % 6);
        }
    }

    public class LogicalOperator : Operator
    {
        public static LogicalOperator And = new LogicalOperator("And", 0);
        public static LogicalOperator Or = new LogicalOperator("Or", 1);

        public LogicalOperator(string name, int value) : base(name, value) { }

        public static LogicalOperator Code2Operator(int num)
        {
            if (num == 0)
                return And;
            else if (num == 1)
                return Or;
            else
                return null;
        }

        public override string ToString()
        {
            if (code == 0)
                return "AND";
            else if (code == 1)
                return "OR";
            else
                return "";
        }

        public static LogicalOperator Inverse(LogicalOperator ope)
        {
            return Code2Operator((ope.GetCode() + 1) % 2);
        }
    }
}
