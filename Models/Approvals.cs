using ContractMaster.Models.Enums;

namespace ContractMaster.Models;
public class Approvals
{
    public int Id{get;set;}
    public required int Version{get;set;}
    public required int Sequence{get;set;}
    public required int ContractId{get;set;}
    public Contract Contract{get;set;}=null!;
    public required int ApproverId{get;set;}
    public User Approver{get;set;}=null!;
    public ApproverType ApproverType{get;set;}
    public ApprovalStatus ApprovalStatus { get; set; }
    public DateOnly? ApprovedAt{get;set;}=null;
    public DateOnly? RejectedAt{get;set;}=null;
}