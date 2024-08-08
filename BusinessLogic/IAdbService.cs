using BusinessLogic.Models;

namespace BusinessLogic
{
    public interface IAdbService
    {

        string WriteInput(PowershellCommand command);
    }
}
