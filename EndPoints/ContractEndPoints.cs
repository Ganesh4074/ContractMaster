

using System.Text.RegularExpressions;
using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.Models;
using Microsoft.EntityFrameworkCore;

public static class ContractEndPoints
{
    public static void GetContractEndPoints(this WebApplication app)
    {
        var group=app.MapGroup("/Contracts");

        //Add Contracts
        group.MapPost("/",async(NewContractDTO NewContract, ContractMasterContext DB) =>
        {
            Contract contract = new()
            {
                ContractId=NewContract.ContractId,
                ContracType=NewContract.ContracType,
                Status=NewContract.Status,
                StartDate=NewContract.StartDate,
                EndDate=NewContract.EndDate,
                CounterPartyName=NewContract.CounterPartyName,
                CounterPartyEmail=NewContract.CounterPartyEmail
            };
            await DB.AddAsync(contract);
            await DB.SaveChangesAsync();
        });

        //Get All Contracts
        group.MapGet("/",async(ContractMasterContext DB) =>
        {
            return await DB.Contracts.Select(contract=>new ContractDTO(
                contract.Id,
                contract.ContractId,
                contract.Version,
                contract.ContracType,
                contract.Status,
                contract.StartDate,
                contract.EndDate,
                contract.CounterPartyName,
                contract.CounterPartyEmail,
                contract.CreatedAt
                )).ToListAsync();
        });
    }

    
    
}