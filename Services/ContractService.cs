using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class ContractService
{
    private readonly IContractRepository _repository;
    public ContractService(IContractRepository repository, IApprovalRepository approvalRepository)
    {
        _repository = repository;
    }

    public async Task CreateContract(NewContractDTO newContract, int userId)
{
    var exists = await _repository.ExistsAsync(newContract.ContractId, newContract.Version);

    if (exists)
    {
        throw new InvalidOperationException("Contract already exists for this version.");
    }

    Contract contract = new()
    {
        ContractId = newContract.ContractId,
        Version = newContract.Version,
        ContractType = newContract.ContractType,
        Status = newContract.Status,
        StartDate = newContract.StartDate,
        EndDate = newContract.EndDate,
        CounterPartyName = newContract.CounterPartyName,
        CounterPartyEmail = newContract.CounterPartyEmail,
        CreatedById = userId,
        CreatedAt = newContract.CreatedAt
    };

    await _repository.AddAsync(contract);

}

    public async Task<List<ContractDTO>> GetContracts()
    {
        var contracts = await _repository.GetAllAsync();

        return contracts.Select(contract => new ContractDTO(
            contract.Id,
            contract.ContractId,
            contract.Version,
            contract.ContractType,
            contract.Status,
            contract.StartDate,
            contract.EndDate,
            contract.CounterPartyName,
            contract.CounterPartyEmail,
            contract.CreatedAt
        )).ToList();
    }

    public async Task DeleteById(int id)
    {
        var contract = await _repository.GetByIdAsync(id);

        if(contract == null)
        {
            throw new FileNotFoundException();
        }

        await _repository.DeleteAsync(contract);
    }
    public async Task UpdateStatus(int contractId, int version, ContractStatus status)
    {
        var contract = await _repository.GetByContractAndVersionAsync(contractId, version);

        if(contract is null)
        {
            throw new BusinessRuleException(
                "Contract not found.");
        }

        contract.Status = status;

        await _repository.UpdateAsync(contract);
    }
}