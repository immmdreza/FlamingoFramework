using System.Linq;
using System.Text.Encodings.Web;

namespace Flamingo.Helpers.Types
{
    /// <summary>
    /// This structure should be used within <see cref="TextParser"/>
    /// </summary>
    public struct ParseString
    {
        /// <summary>
        /// This structure should be used within <see cref="TextParser"/>
        /// </summary>
        /// <param name="surroundingFormat">
        /// A format string that accepts <see cref="InputStrings"/> as arguments
        /// </param>
        /// <param name="shouldEncode">
        /// If the HTML tags in input text should be encoded
        /// </param>
        /// <param name="inputStrings">
        /// Your input texts ( this will be used in <c>string.Format</c> as args if <see cref="SurroundingFormat"/> is not null )
        /// </param>
        public ParseString(
            string[] inputStrings,
            string surroundingFormat = null,
            bool shouldEncode = true)
        {
            ShouldEncode = shouldEncode;
            InputStrings = inputStrings;
            SurroundingFormat = surroundingFormat;
        }


        /// <summary>
        /// This structure should be used within <see cref="TextParser"/>
        /// </summary>
        /// <param name="surroundingFormat">
        /// A format string that accepts <see cref="InputStrings"/> as arguments
        /// </param>
        /// <param name="shouldEncode">
        /// If the HTML tags in input text should be encoded
        /// </param>
        /// <param name="inputStrings">
        /// Your input texts ( this will be used in <c>string.Format</c> as args if <see cref="SurroundingFormat"/> is not null )
        /// </param>
        public ParseString(
            string surroundingFormat,
            bool shouldEncode = true,
            params string[] inputStrings)
        {
            ShouldEncode = shouldEncode;
            InputStrings = inputStrings;
            SurroundingFormat = surroundingFormat;
        }

        /// <summary>
        /// If the HTML tags in input text should be encoded
        /// </summary>
        public bool ShouldEncode { get; }

        /// <summary>
        /// Your input texts ( this will be used in <c>string.Format</c> as args if <see cref="SurroundingFormat"/> is not null )
        /// </summary>
        public string[] InputStrings { get; }

        /// <summary>
        /// A format string that accepts <see cref="InputStrings"/> as arguments
        /// </summary>
        public string SurroundingFormat { get; }


        public static implicit operator string(ParseString ps)
        {
            if(!string.IsNullOrEmpty(ps.SurroundingFormat))
            {
                if(ps.ShouldEncode)
                {
                    return string.Format(ps.SurroundingFormat, ps.InputStrings
                        .Select(x=> HtmlEncoder.Default.Encode(x)));
                }

                return string.Format(ps.SurroundingFormat, ps.InputStrings);
            }

            return ps.ShouldEncode ?
                HtmlEncoder.Default.Encode(string.Concat(ps.InputStrings)) :
                string.Concat(ps.InputStrings); ;
        }
    }
}
