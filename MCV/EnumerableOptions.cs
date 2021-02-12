using System;

namespace MCV
{
    [Flags]
    public enum EnumerableOptions
    {
        /// <summary>
        /// indicates that model is not complete in case it contains empty enumerables
        /// </summary>
        DenyEmptyEnumerables = 1,
        /// <summary>
        /// checks whether enumerable elements are complete
        /// </summary>
        CheckEnumerableElements = 2
    }
}
