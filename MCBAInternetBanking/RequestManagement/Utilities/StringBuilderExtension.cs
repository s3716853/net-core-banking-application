using System.Text;

namespace RequestManagement.Utilities
{
    public static class StringBuilderExtension
    {
        // Attempting to overload Append to take an array of strings would not work
        // as the Append(Object) method would always run instead.
        // This method will append each line in the array as a new line in the string
        public static StringBuilder AppendArray(this StringBuilder builder, string[] stringValues)
        {
            foreach (string stringValue in stringValues)
            {
                builder.AppendLine(stringValue);
            }

            return builder;
        }
    }
}
