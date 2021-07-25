using Flamingo.Helpers.Types;
using System.Text;
using System.Text.Encodings.Web;

namespace Flamingo.Helpers
{
    /// <summary>
    /// Text parse ( using HTML ) to send through bot
    /// </summary>
    public class TextParser
    {
        /// <summary>
        /// <see cref="StringBuilder"/> of this parse to modify manually
        /// </summary>
        public StringBuilder StringBuilder => _builder;

        private readonly StringBuilder _builder;

        /// <summary>
        /// Text parse ( using HTML ) to send through bot
        /// </summary>
        public TextParser(string startingText = null)
        {
            if (string.IsNullOrEmpty(startingText))
                _builder = new StringBuilder();
            else
                _builder = new StringBuilder(startingText);
        }

        /// <summary>
        /// Encode an append string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendEncoded(string input)
        {
            _builder.Append(HtmlEncoder.Default.Encode(input));
            return this;
        }

        /// <summary>
        /// Encode an append string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendEncoded(ParseString input)
        {
            _builder.Append(HtmlEncoder.Default.Encode(input));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as code ( HTML tag )
        /// </summary>
        /// <param name="text">Input string to format</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendCode(string text, bool encode = true)
        {
            _builder.Append(HtmlCode(text, encode));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as italic ( HTML tag )
        /// </summary>
        /// <param name="text">Input string to format</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendItalic(string text, bool encode = true)
        {
            _builder.Append(HtmlItalic(text, encode));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as bold ( HTML tag )
        /// </summary>
        /// <param name="text">Input string to format</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendBold(string text, bool encode = true)
        {
            _builder.Append(HtmlBold(text, encode));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as under line ( HTML tag )
        /// </summary>
        /// <param name="text">Input string to format</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendUnderLine(string text, bool encode = true)
        {
            _builder.Append(HtmlUnderLine(text, encode));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as strike ( HTML tag )
        /// </summary>
        /// <param name="text">Input string to format</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendStrike(string text, bool encode = true)
        {
            _builder.Append(HtmlStrike(text, encode));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as a ( HTML tag )
        /// </summary>
        /// <param name="text">Input string to format</param>
        /// <param name="link">Link</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendHyperLink(string link, string text, bool encode = true)
        {
            _builder.Append(HyperHTML(text, link, encode));
            return this;
        }

        /// <summary>
        /// Append an string that should be formate as code ( HTML tag )
        /// </summary>
        /// <param name="name">Shown name of text-mention</param>
        /// <param name="userId">userId to create text mention of</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns><see cref="TextParser"/> instance itself</returns>
        public TextParser AppendTextMention(int userId, string name, bool encode = true)
        {
            _builder.Append(TextMentionHTML(name, userId, encode));
            return this;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _builder.ToString();
        }

        /// <summary>
        /// Append some string to the end of a <see cref="TextParser"/>
        /// </summary>
        public static TextParser operator +(TextParser tp, string st)
        {
            tp.StringBuilder.Append(st);
            return tp;
        }

        /// <summary>
        /// Append some string to the end of a <see cref="TextParser"/>
        /// </summary>
        public static TextParser operator +(TextParser tp, ParseString ps)
        {
            tp.StringBuilder.Append(ps);
            return tp;
        }

        /// <summary>
        /// Append an <see cref="TextParser"/> to the end of an string
        /// </summary>
        public static string operator +(string st, TextParser tp)
        {
            return st + tp;
        }

        /// <summary>
        /// To String implicit
        /// </summary>
        public static implicit operator string(TextParser tp) => tp.ToString();

        /// <summary>
        /// Returns and encoded HTML code tag
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString HtmlCode(string st, bool encode = true) => new ParseString("<code>{0}</code>", encode, st);

        /// <summary>
        /// Returns and encoded HTML italic tag
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString HtmlItalic(string st, bool encode = true) => new ParseString("<em>{0}</em>", encode, st);

        /// <summary>
        /// Returns and encoded HTML bold tag
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString HtmlBold(string st, bool encode = true) => new ParseString("<b>{0}</b>", encode, st);

        /// <summary>
        /// Returns and encoded HTML u tag
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString HtmlUnderLine(string st, bool encode = true) => new ParseString("<u>{0}</u>", encode, st);

        /// <summary>
        /// Returns and encoded HTML s tag 
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString HtmlStrike(string st, bool encode = true) => new ParseString("<s>{0}</s>", encode, st);

        /// <summary>
        /// Returns and encoded HTML a tag ( for hyper links )
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="link">input link</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString HyperHTML(string st, string link, bool encode = true) => new ParseString("<a href='{0}'>{1}</a>", encode, link, st);

        /// <summary>
        /// Returns and encoded HTML a tag ( for hyper links )
        /// </summary>
        /// <param name="st">input string</param>
        /// <param name="userId">userId to create text mention of</param>
        /// <param name="encode">If input HTML tags should be encoded, suggested for runtime inputs</param>
        /// <returns>HTML encoded string</returns>
        public static ParseString TextMentionHTML(string st, int userId, bool encode = true) => new ParseString("<a href='tg://user?id={0}'>{1}</a>", encode, userId.ToString(), st);

    }
}
