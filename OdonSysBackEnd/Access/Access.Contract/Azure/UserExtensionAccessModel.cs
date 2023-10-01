using Utilities.Extensions;

namespace Access.Contract.Azure
{
    public class UserExtensionAccessModel
    {
        public UserExtensionAccessModel(string aadAppB2CId)
        {

            AadAppB2CId = aadAppB2CId.RemoveDash();

            Document = $"extension_{aadAppB2CId}_Document";
            Phone = $"extension_{aadAppB2CId}_Phone";
            SecondName = $"extension_{aadAppB2CId}_SecondName";
            SecondLastname = $"extension_{aadAppB2CId}_SecondLastname";
        }

        public string AadAppB2CId { get; }
        public string Document { get; }
        public string Phone { get; }
        public string SecondName { get; }
        public string SecondLastname { get; }
    }
}
