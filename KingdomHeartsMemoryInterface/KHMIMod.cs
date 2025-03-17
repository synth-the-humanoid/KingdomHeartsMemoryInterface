namespace KingdomHeartsMemoryInterface
{
    public abstract class KHMIMod
    {
        public KHMIMod()
        {
            Start();
        }

        protected abstract void Start();
        public abstract void Update();
    }
}
