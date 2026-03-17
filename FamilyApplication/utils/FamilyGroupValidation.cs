using FamilyApplication.DTOs;
using FamilyApplication.Enums;
using Microsoft.IdentityModel.Tokens;

namespace FamilyApplication.utils
{
    public class FamilyGroupValidation
    {
        public static ValidationMessages IsValidFamilyGroup(CreateFamilyGroupDto familyGroupDto)
        {
            if (familyGroupDto.FamilyGroupName.IsNullOrEmpty())
                return new ValidationMessages(false, "Informe seu nome!");

            if ((familyGroupDto.Photo.ToString()).IsNullOrEmpty())
                return new ValidationMessages(false, "Informe uma foto!");

            return new ValidationMessages(true, "Família Adicionada com Sucesso!");
        }
    }
}
