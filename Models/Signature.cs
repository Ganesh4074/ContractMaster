namespace ContractMaster.Models;
public class Signature
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public int Version{get;set;}
    public int SignatureType{get;set;}
    public int SignatureStatus{get;set;}
}