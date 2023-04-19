using AutoMapper;
using WalletServices.Dtos;
using WalletServices.Models;

namespace WalletServices.Profiles
{
    public class WalletProfiles : Profile
    {
        public WalletProfiles() 
        {
            // dari wallet ke edit wallet dto
            CreateMap<Wallet, EditWalletDto>();
            CreateMap<Wallet, ReadWalletDto>();
            // dari create wallet dto ke wallet
            CreateMap<CreateWalletDto, Wallet>();
            CreateMap<EditWalletDto, Wallet>();
        }
    }
}
