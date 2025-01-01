namespace KingdomHeartsMemoryInterface.Types
{
    public class InterfaceDisconnectedException : Exception
    {
        public InterfaceDisconnectedException() : base("Memory Interface unable to access process.") { }
    }
}
