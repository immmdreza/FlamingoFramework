namespace Flamingo.Exceptions
{
    class NotACallbackFunc : FlamingoUnExpected
    {
        public NotACallbackFunc(string name) 
            : base($"Function {name} is not a proper Callback function")
        { }
    }
}
