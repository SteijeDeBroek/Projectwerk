namespace Cookiemonster.Interfaces
{
    public interface IDeletable
    {
        public bool isDeletable { get; }

        public bool Delete();
    }
}