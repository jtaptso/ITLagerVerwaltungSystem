using ITLagerVerwaltungSystem.Core.Domain;

namespace ITLagerVerwaltungSystem.Core.Validation
{
    public class MaterialIssueValidator
    {
        public static void Validate(Material material)
        {
            if (material.Status == MaterialStatus.Damaged || material.Status == MaterialStatus.Retired)
                throw new InvalidOperationException("Cannot issue damaged or retired material.");
            // Add more rules as needed
        }
    }
}
