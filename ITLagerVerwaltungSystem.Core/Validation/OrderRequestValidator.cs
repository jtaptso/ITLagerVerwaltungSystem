using ITLagerVerwaltungSystem.Core.Domain;

namespace ITLagerVerwaltungSystem.Core.Validation
{
    public class OrderRequestValidator
    {
        public static void Validate(Order order)
        {
            if (order.Materials == null || !order.Materials.Any())
                throw new InvalidOperationException("Order must contain at least one material.");
            // Add more rules as needed
        }

        public static void ValidateAvailableMaterials(IEnumerable<Material> materials)
        {
            if (materials == null || !materials.Any(m => m.Status == MaterialStatus.New || m.Status == MaterialStatus.Used))
                throw new InvalidOperationException("No available materials to issue.");
        }
    }
}
