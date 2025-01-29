using System.ComponentModel.DataAnnotations;

namespace Banking_system.DTO_s.Custom_Validations
{
    public class CheckEnumValue : ValidationAttribute
    {
        private readonly Type type;
        public CheckEnumValue(Type type)
        {
            this.type = type;
        }
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            if (value.GetType() != type) return false;

            object val = Enum.Parse(type, value.ToString());

            return Enum.IsDefined(type, val);

        }
    }
}
