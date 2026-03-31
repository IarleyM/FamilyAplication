using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;

namespace FamilyApplication.utils
{
    public class UserValidation
    {
        public static ValidationMessages IsValidUser(CreateUserDTO createUserDto)
        {
            if(createUserDto.UserName.IsNullOrEmpty())
                return new ValidationMessages(false, "Informe seu nome!");

            if (createUserDto.Email.IsNullOrEmpty() ||
                !InfosValidation.IsValidEmail(createUserDto.Email))
                return new ValidationMessages(false, "Informe seu email corretamente!");

            if(createUserDto.Phone.IsNullOrEmpty() ||
                !InfosValidation.IsValidPhone(createUserDto.Phone))
                return new ValidationMessages(false, "Informe seu telefone corretamente!");

            if (createUserDto.BirthDate.Equals(DateTime.Now) 
                || createUserDto.BirthDate.Equals(DateTime.MinValue)
                || createUserDto.BirthDate > DateTime.Now) 
                return new ValidationMessages(false, "Informe uma data correta!");

            return new ValidationMessages(true, "Membro adicionado com sucesso!");
        }

        public static ValidationMessages IsValidUpdateUser(UpdateMemberDto updateFamilyDto)
        {
            if (updateFamilyDto.Age.HasValue) 
                if(updateFamilyDto.Age <= 0 || updateFamilyDto.Age >= 120) 
                    return new ValidationMessages(false, "Informe sua idade corretamente!");

            if(updateFamilyDto.FamilyCategory.HasValue)
                if(!Enum.IsDefined(typeof(FamilyCategory), updateFamilyDto.FamilyCategory))
                    return new ValidationMessages(false, "Informe a sua categoria corretamente!");

            if(updateFamilyDto.BirthDate.HasValue)
                if(updateFamilyDto.BirthDate.Equals(DateTime.Now) ||
                    updateFamilyDto.BirthDate.Equals(DateTime.MinValue))
                        return new ValidationMessages(false, "Informe uma data correta!");

            return new ValidationMessages(true, "Membro atualizado com sucesso!");
        }
    }
}
