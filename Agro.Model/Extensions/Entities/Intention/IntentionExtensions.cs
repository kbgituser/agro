using Agro.Model.Entities;
using Agro.Model.Enums;


public static class IntentionExtensions
{
    public static void ChangeStatusToSelected(this Intention intention)
    {
        intention.IntentionStatus = IntentionStatus.Selected;
    }
}
