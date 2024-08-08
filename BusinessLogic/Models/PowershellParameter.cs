namespace BusinessLogic.Models
{
    public class PowershellParameter
    {
        public PowershellParameter(string parameter, string argument)
        {
            Parameter = parameter;
            Argument = argument;
        }

        public string Parameter { get; }
        public string Argument { get; }
    }
}
