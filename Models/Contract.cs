namespace ContractMaster.Models;

public class Contract
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public int Version{get;set;}
    public required string ContracType{get;set;}
    public required string Status{get;set;}
    public required DateOnly StartDate{get;set;}
    public required DateOnly EndDate{get;set;}
    public required string CounterPartyName{get;set;}
    public required string CounterPartyEmail{get;set;}
    public int CreatedById{get;set;}
    public User User=null!;
    public DateOnly CreatedAt{get;set;}
    
}