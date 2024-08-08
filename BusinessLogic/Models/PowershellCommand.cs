namespace BusinessLogic.Models
{
    public class PowershellCommand
    {
        public PowershellCommand(string command, List<PowershellParameter> parameters, string argument)
        {
            Command = command;
            Parameters = parameters;
            Argument = argument;
        }

        public string Command { get; }
        public List<PowershellParameter> Parameters { get; }
        public string Argument { get; }
    }
}
