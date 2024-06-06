using System.Diagnostics;

namespace BusinessLogic
{
    public interface IAdbService
    {
        void StartAdbApplicationAsBackgroundProcess();

        void WriteInput(string input);

        void AttachHandlerToProcessOutputEvents(DataReceivedEventHandler handler);
    }
}
