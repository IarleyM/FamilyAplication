using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;

namespace FamilyApplication.utils
{
    public class MemberValidation
    {
        public static ValidationMessages IsValidMember(CreateMemberDto createMemberDto)
        {

            if (createMemberDto.FamilyId == null || createMemberDto.FamilyId <= 0)
                return new ValidationMessages(false, "Informe sua família!");

            if(createMemberDto.Age <= 0 || createMemberDto.Age >= 120)
                return new ValidationMessages(false, "Informe sua data corretamente!");

            if(createMemberDto.MemberName.IsNullOrEmpty())
                return new ValidationMessages(false, "Informe seu nome!");

            if(createMemberDto.BirthDate.Equals(DateTime.Now) 
                || createMemberDto.BirthDate.Equals(DateTime.MinValue)) 
                return new ValidationMessages(false, "Informe uma data correta!");

            if (!Enum.IsDefined(typeof(FamilyCategory), createMemberDto.FamilyCategory))
                return new ValidationMessages(false, "Informe a sua categoria corretamente!");

            return new ValidationMessages(true, "Membro adicionado com sucesso!");
        }

        public static ValidationMessages IsValidUpdateMember(UpdateMemberDto updateFamilyDto)
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
