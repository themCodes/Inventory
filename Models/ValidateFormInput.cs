using Microsoft.IdentityModel.Tokens;

namespace Inventory.Models
{
    public class ValidateFormInput
    {
        // Validate form field input. messageOutputDestination is a reference to which message variable to put the resulting message in: messageAdd or messageEdit
        public static bool ValidateAddItemForm(string name, string numberOfItems, string numberOfItemsTrigger, string location, ref string messageOutputDestination)
        {
            if (string.IsNullOrEmpty(name))
            {
                messageOutputDestination = "Du måste skriva in ett namn.";
                return false;
            }

            if (string.IsNullOrEmpty(numberOfItems))
            {
                messageOutputDestination = "Du måste skriva in ett saldo.";
                return false;
            }

            if (!int.TryParse(numberOfItems, out _))
            {
                messageOutputDestination = "Saldot måste vara en siffra.";
                return false;
            }

            if (string.IsNullOrEmpty(numberOfItemsTrigger))
            {
                messageOutputDestination = "Du måste skriva in en trigger.";
                return false;
            }

            if (!int.TryParse(numberOfItemsTrigger, out _))
            {
                messageOutputDestination = "Triggern måste vara en siffra.";
                return false;
            }

            if (string.IsNullOrEmpty(location))
            {
                messageOutputDestination = "Du måste skriva in en plats.";
                return false;
            }

            messageOutputDestination = "";
            return true;
        }

        // Validate the input in form fields.
        public static bool ValidateInventoryCountForm(string checkThisField, ref string messageOutputDestination)
        {
            if (string.IsNullOrEmpty(checkThisField))
            {
                messageOutputDestination = "Du måste skriva in ett saldo.";
                return false;
            }

            if (!int.TryParse(checkThisField, out _))
            {
                messageOutputDestination = "Saldot måste vara en siffra.";
                return false;
            }

            messageOutputDestination = "";
            return true;
        }
    }
}
