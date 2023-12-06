namespace Cookiemonster.Infrastructure.EFRepository.Interfaces
{
    public interface IDeletable
    {
        public bool IsDeletable { get; }
        public bool IsDeleted { get; set; }
    }
}