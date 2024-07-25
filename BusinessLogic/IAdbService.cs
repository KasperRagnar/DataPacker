using System.Diagnostics;

namespace BusinessLogic
{
    public interface IAdbService
    {
        void StartAdbApplicationAsBackgroundProcess();

        string WriteInput(string input);

        void AttachHandlerToProcessOutputEvents(DataReceivedEventHandler handler);

        void AttachHandlerToProcessErrorEvents(DataReceivedEventHandler handler);
    }
}
