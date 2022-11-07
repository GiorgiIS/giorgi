namespace UserManagement.Common.EfCore.Contracts
{
    public interface IDeletable
    { 
        public bool IsDeleted { get; set; }
    }
}
