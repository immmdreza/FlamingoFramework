namespace Flamingo.BotExtensions
{
    public struct FileToSend
    {
        public FileToSend(string input, InputType inputType)
        {
            Input = input;
            InputType = inputType;
        }

        public string Input { get; }

        public InputType InputType { get; }
    }

    public enum InputType
    {
        FileId,
        Url,
        Path
    }
}
