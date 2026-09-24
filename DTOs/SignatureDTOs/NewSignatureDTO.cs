using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record NewSignatureDTO(
    int ContractId,
    int Version,
    SignatureType SignatureType
);