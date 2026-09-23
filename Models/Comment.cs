namespace ContractMaster.Models;

public class Comment
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public Contract? Contract{get;set;}
    public int Version{get;set;}
    public int UserId{get;set;}
    public User? User{get;set;}
    public required string CommentText{get;set;}
    public DateOnly CreatedAt{get;set;}
}