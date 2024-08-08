namespace BusinessLogic.Models
{
    public class PowershellCommand
    {
        public PowershellCommand(string command, List<string> commandArguments, List<PowershellParameter>? parameters = null)
        {
            Command = command;
            CommandArguments = commandArguments;
            Parameters = parameters ?? new List<PowershellParameter>();
        }

        public string Command { get; }
        public List<string> CommandArguments { get; }
        public List<PowershellParameter> Parameters { get; }
    }
}
