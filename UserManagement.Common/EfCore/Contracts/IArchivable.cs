namespace UserManagement.Common.EfCore.Contracts
{
    public interface IArchivable
    {
        public bool IsArchived { get; set; }
    }
}
