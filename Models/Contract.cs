using ContractMaster.Models.Enums;

namespace ContractMaster.Models;

public class Contract
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public int Version{get;set;}
    public required ContractType ContractType { get; set; }
    public required ContractStatus Status { get; set; }
    public required DateOnly StartDate{get;set;}
    public required DateOnly EndDate{get;set;}
    public required string CounterPartyName{get;set;}
    public required string CounterPartyEmail{get;set;}
    public int CreatedById{get;set;}
    public User? User;
    public DateOnly CreatedAt{get;set;}
    
}