namespace MCV
{
    /// <summary>
    /// configuration for model validation
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// represents an identification property (i.e. "Id") by which objects of non-built in type will be tracked and circular dependency will be avoided. When null, object's hashcode is used for identification
        /// </summary>
        public string CustomIdentificationProperty { get; set; }
        /// <summary>
        /// options for working with strings. Represents flagged enum and many options can be passed at the same time using bitwise operation: or
        /// </summary>
        public StringOptions StringOptions { get; set; }
        /// <summary>
        /// options for working with enumerables. Represents flagged enum and many options can be passed at the same time using bitwise operation: or
        /// </summary>
        public EnumerableOptions EnumerableOptions { get; set; }
    }
}
