namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class UnknownValueException: ValueException
    {
        public static string GetMessage(string value, int pos)
        {
            return $"Unknown value '{value}' in {pos} position.";
        }

        public UnknownValueException(string value, int pos)
            : base(GetMessage(value, pos))
        {
        }
    }
}
