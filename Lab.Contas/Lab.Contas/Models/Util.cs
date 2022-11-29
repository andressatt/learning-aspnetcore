using System.ComponentModel;
using System.Reflection;

namespace Lab.Contas.Models
{
    public static class Util
    {
        public enum TipoConta
        {
            IPVA,
            Aluguel,
            Energia,
            Internet,
            [Description("Gás")]
            Gas,
            Escola,
            [Description("Cartão de Crédito")]
            CartaoCredito,
            Outros,
            [Description("Pedágio")]
            Pedagio,
            Clube
        }

        public enum StatusContaPagar
        {
            Pago,
            [Description("Não Pago")]
            NaoPago
        }

        public static string? GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            string? description = null;
            FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString() ?? string.Empty);

            if (fieldInfo != null)
            {
                var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                description = attribute?.Cast<DescriptionAttribute>().SingleOrDefault()?.Description;
            }
            return description ?? enumValue.ToString();
        }
    }
}
