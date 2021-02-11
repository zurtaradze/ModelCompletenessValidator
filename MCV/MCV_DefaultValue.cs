using System;

namespace MCV
{
    public class MCV_DefaultValue : Attribute
    {
        public readonly object obj;

        public MCV_DefaultValue(object obj)
        {
            this.obj = obj;
        }
    }
}
