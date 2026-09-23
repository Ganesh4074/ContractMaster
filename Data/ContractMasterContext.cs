using Microsoft.EntityFrameworkCore;
using ContractMaster.Models;
namespace ContractMaster.Data;
public class ContractMasterContext(DbContextOptions<ContractMasterContext> options) : DbContext(options)
{
    public DbSet<Contract> Contracts=>Set<Contract>();
    public DbSet<Department> Departments=>Set<Department>();
    public DbSet<User> Users=>Set<User>();
    public DbSet<Approvals> Approvals=>Set<Approvals>();
    public DbSet<Signature> Signatures=>Set<Signature>();
    public DbSet<Comment> Comments { get; set; }

}