using ContractMaster.Models.Enums;

namespace ContractMaster.Models;
public class Signature
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public int Version{get;set;}
    public int SignatoryId{get;set;}
    public User? User{get;set;}=null;
    public SignatureType SignatureType { get; set; }
    public SignatureStatus SignatureStatus { get; set; }
}