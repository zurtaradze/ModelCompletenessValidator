using System;

namespace MCV
{
    [Flags]
    public enum EnumerableOptions
    {
        DenyEmptyEnumerables = 1,
        CheckEnumerableElements = 2
    }
}
