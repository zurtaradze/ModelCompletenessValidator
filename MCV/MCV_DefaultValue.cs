using System;

namespace MCV
{
    /// <summary>
    /// explicit default value when to consider property unassigned
    /// </summary>
    public class MCV_DefaultValue : Attribute
    {
        public readonly object obj;

        public MCV_DefaultValue(object obj)
        {
            this.obj = obj;
        }
    }
}
