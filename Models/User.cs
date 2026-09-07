namespace ContractMaster.Models;
public class User
{
    public int Id{get;set;}
    public required string Name{get;set;}
    public required string Department{get;set;}
    public required string Role{get;set;}
    public required string EMail{get;set;}
    public required string PasswordHash{get;set;}
}