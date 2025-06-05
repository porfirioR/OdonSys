using Utilities.Extensions;

namespace Access.Contract.Azure;

public class UserExtensionAccessModel
{
    public UserExtensionAccessModel(string aadAppB2CId)
    {

        AadAppB2CId = aadAppB2CId.RemoveDash();
        Document = $"extension_{AadAppB2CId}_Document";
        Phone = $"extension_{AadAppB2CId}_Phone";
        SecondName = $"extension_{AadAppB2CId}_SecondName";
        SecondSurname = $"extension_{AadAppB2CId}_SecondSurname";
    }

    public string AadAppB2CId { get; }
    public string Document { get; }
    public string Phone { get; }
    public string SecondName { get; }
    public string SecondSurname { get; }
    public string EmailCode { get; } = "emailAddress";
}
