using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class ContractService
{
    private readonly IContractRepository _repository;

    public ContractService(IContractRepository repository)
    {
        _repository = repository;
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

        await _repository.AddAsync(contract);
    }

    public async Task<List<ContractDTO>> GetContracts()
    {
        var contracts = await _repository.GetAllAsync();

        return contracts.Select(contract => new ContractDTO(
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
        )).ToList();
    }

    public async Task DeleteById(int id)
    {
        var contract = await _repository.GetByIdAsync(id);

        if (contract == null)
        {
            throw new FileNotFoundException();
        }

        await _repository.DeleteAsync(contract);
    }
}