using BusinessLogic.Models;
using System.Management.Automation;

namespace BusinessLogic.Extensions
{
    public static class PowershellGeneration
    {
        public static PowerShell BuildPowershellCommand(this PowerShell instance, PowershellCommand powershellCommand)
        {
            instance.AddCommand(powershellCommand.Command);

            foreach (PowershellParameter parameter in powershellCommand.Parameters)
            {
                instance.AddParameter(parameter.Parameter, parameter.Argument);
            }

            foreach (string argument in powershellCommand.CommandArguments)
            {
                instance.AddArgument(powershellCommand.CommandArguments);
            }

            return instance;
        }
    }
}
