namespace ContractMaster.Models;
public class Signature
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public int Version{get;set;}
    public int SignatoryId{get;set;}
    public User? User{get;set;}=null;
    public int SignatureType{get;set;}
    public int SignatureStatus{get;set;}
}