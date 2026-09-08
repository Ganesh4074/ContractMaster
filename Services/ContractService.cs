using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Services;

public class ContractService
{
    private readonly ContractMasterContext _db;

    public ContractService(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task CreateContract(NewContractDTO newContract)
    {
        Contract contract = new()
        {
            ContractId = newContract.ContractId,
            Version = newContract.Version,
            ContracType = newContract.ContracType,
            Status = newContract.Status,
            StartDate = newContract.StartDate,
            EndDate = newContract.EndDate,
            CounterPartyName = newContract.CounterPartyName,
            CounterPartyEmail = newContract.CounterPartyEmail,
            CreatedById = newContract.CreatedById,
            CreatedAt = newContract.CreatedAt
        };
        await _db.Contracts.AddAsync(contract);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ContractDTO>> GetContracts()
    {
        return await _db.Contracts.Select(contract => new ContractDTO(
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
    }
}