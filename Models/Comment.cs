namespace ContractMaster.Models;

public class Comment
{
    public int Id{get;set;}
    public int ContractId{get;set;}
    public Contract Contract{get;set;}=null!;
    public int UserId{get;set;}
    public User User{get;set;}=null!;
    public required string CommentText{get;set;}
    public required string CommentType{get;set;}
    public DateOnly CreatedAt{get;set;}
}