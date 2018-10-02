using DrugTest.Models;

namespace DrugTest.Helpers
{
    public class StatusCss
    {
        // Method used for dynamic color assignment in the view.
        public string GetCssClass(Status status)
        {
            switch (status)
            {
                case Status.Pulled:
                    return "pulled";
                case Status.Contacted:
                    return "info";
                case Status.AwaitingResult:
                    return "warning";
                case Status.Passed:
                    return "success";
                case Status.Failed:
                    return "danger";
                case Status.Canceled:
                    return "canceled";
                default:
                    return "";
            }
        }
    }
}