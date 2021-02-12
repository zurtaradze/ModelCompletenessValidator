using System;

namespace MCV
{
    [Flags]
    public enum StringOptions
    {
        /// <summary>
        /// indicates that model is not complete in case it contains empty strings
        /// </summary>
        DenyEmptyStrings = 1,
    }
}
