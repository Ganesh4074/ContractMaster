using ContractMaster.DTOs;

namespace ContractMaster.Services;

public interface ISignatureService
{
    Task<List<GetSignatureDTO>> GetSignatures();

    Task<GetSignatureDTO?> UpdateSignature(
        int id,
        UpdateSignatureDTO update,
        int userId,
        string userRole);

    Task<GetSignatureDTO> CreateSignature(
        NewSignatureDTO newSignature);
}