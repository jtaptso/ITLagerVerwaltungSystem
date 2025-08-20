using ITLagerVerwaltungSystem.Core.Domain;

namespace ITLagerVerwaltungSystem.Core.Validation
{
    public class MaterialReturnValidator
    {
        public static void Validate(Material material, string userId)
        {
            // Example: Only allow return if user is the one who checked out the material
            if (material.Order?.UserId != userId)
                throw new InvalidOperationException("User is not authorized to return this material.");
            // Add more rules as needed
        }
    }
}
