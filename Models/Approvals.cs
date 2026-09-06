namespace ContractMaster.Models;
public class Approvals
{
    public int Id{get;set;}
    public int Version{get;set;}
    public int Sequence{get;set;}
    public int ContractId{get;set;}
    public Contract Contract{get;set;}=null!;
    public int ApproverId{get;set;}
    public User Approver{get;set;}=null!;
    public string? ApproverType{get;set;}
    public string? ApprovalStatus{get;set;}
    public DateOnly ApprovedAt{get;set;}
    public DateOnly RejectedAt{get;set;}
}